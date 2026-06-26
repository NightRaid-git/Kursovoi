using DocumentFormat.OpenXml.Presentation;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace sklad
{
    public partial class frmLogin : Form
    {
        private class PuzzlePiece
        {
            public PictureBox Box;
            public int CorrectIndex;
        }
        private List<PuzzlePiece> pieces = new List<PuzzlePiece>();
        private int gridX = 2;
        private int gridY = 2;
        private bool puzzleSolved = false;

        private PictureBox selectedPiece = null;

        private int failedAttempts = 0;
        private int lockSeconds = 0;
        private string currentCaptchaCode;
        private int[] correctOrder;          // правильный порядок исходных индексов частей (0..3)
        private int[] selectedOrder;         // порядок, в котором пользователь кликает (максимум 4)
        private int selectedCount;            // количество уже выбранных частей
        private bool captchaPassed = false;

        public frmLogin()
        {
            InitializeComponent();
            txtPassword.UseSystemPasswordChar = true;
            LoadRemembered();
            GeneratePuzzleCaptcha();
        }

        private void LoadRemembered()
        {
            string savedLogin = Properties.Settings.Default.RememberedLogin;
            string savedPass = Properties.Settings.Default.RememberedPassword;
            if (!string.IsNullOrEmpty(savedLogin))
            {
                txtLogin.Text = savedLogin;
                txtPassword.Text = savedPass;
                chkRemember.Checked = true;
            }
        }
        // -------------------- PUZZЛ-КАПЧА (КЛИК-ОБМЕН) --------------------

        private void GeneratePuzzleCaptcha()
        {
            puzzlePanel.Controls.Clear();
            pieces.Clear();
            puzzleSolved = false;
            selectedPiece = null;

            string path = Path.Combine(Application.StartupPath, "Images", "captcha.png");
            if (!File.Exists(path))
            {
                MessageBox.Show("Файл капчи не найден: " + path);
                return;
            }

            Bitmap sourceOriginal = new Bitmap(path);
            Bitmap source = new Bitmap(sourceOriginal, puzzlePanel.Width, puzzlePanel.Height);


            int pieceWidth = puzzlePanel.Width / gridX;
            int pieceHeight = puzzlePanel.Height / gridY;

            List<Bitmap> fragments = new List<Bitmap>();

            for (int y = 0; y < gridY; y++)
            {
                for (int x = 0; x < gridX; x++)
                {
                    Rectangle rect = new Rectangle(x * pieceWidth, y * pieceHeight, pieceWidth, pieceHeight);
                    fragments.Add(source.Clone(rect, source.PixelFormat));
                }
            }

            Random rnd = new Random();
            List<int> order = Enumerable.Range(0, fragments.Count).OrderBy(i => rnd.Next()).ToList();

            for (int i = 0; i < order.Count; i++)
            {
                PictureBox pb = new PictureBox();
                pb.Width = pieceWidth;
                pb.Height = pieceHeight;
                pb.BorderStyle = BorderStyle.FixedSingle;
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                pb.Image = fragments[order[i]];
                pb.Tag = i; // текущая позиция в сетке (по месту, а не по правильности)

                pb.Click += PuzzlePiece_Click;

                puzzlePanel.Controls.Add(pb);

                pieces.Add(new PuzzlePiece
                {
                    Box = pb,
                    CorrectIndex = order[i] // правильный индекс фрагмента
                });
            }

            LayoutPieces();
        }

        private void LayoutPieces()
        {
            int pieceWidth = puzzlePanel.Width / gridX;
            int pieceHeight = puzzlePanel.Height / gridY;

            for (int i = 0; i < pieces.Count; i++)
            {
                int x = i % gridX;
                int y = i / gridX;
                pieces[i].Box.Left = x * pieceWidth;
                pieces[i].Box.Top = y * pieceHeight;
            }
        }

        private void PuzzlePiece_Click(object sender, EventArgs e)
        {
            PictureBox clicked = sender as PictureBox;

            if (selectedPiece == null)
            {
                selectedPiece = clicked;
                selectedPiece.BorderStyle = BorderStyle.Fixed3D;
            }
            else if (selectedPiece == clicked)
            {
                // повторный клик по тому же — снимаем выделение
                selectedPiece.BorderStyle = BorderStyle.FixedSingle;
                selectedPiece = null;
            }
            else
            {
                // обмен местами
                int tempLeft = selectedPiece.Left;
                int tempTop = selectedPiece.Top;

                selectedPiece.Left = clicked.Left;
                selectedPiece.Top = clicked.Top;

                clicked.Left = tempLeft;
                clicked.Top = tempTop;

                selectedPiece.BorderStyle = BorderStyle.FixedSingle;
                selectedPiece = null;

                CheckPuzzleSolved();
            }
        }

        private void CheckPuzzleSolved()
        {
            int pieceWidth = puzzlePanel.Width / gridX;
            int pieceHeight = puzzlePanel.Height / gridY;

            for (int i = 0; i < pieces.Count; i++)
            {
                int x = pieces[i].Box.Left / pieceWidth;
                int y = pieces[i].Box.Top / pieceHeight;
                int index = y * gridX + x;

                if (pieces[i].CorrectIndex != index)
                    return;
            }

            puzzleSolved = true;
            btnLogin.Enabled = true;
        }
        private void SaveRemembered()
        {
            if (chkRemember.Checked)
            {
                Properties.Settings.Default.RememberedLogin = txtLogin.Text;
                Properties.Settings.Default.RememberedPassword = txtPassword.Text;
            }
            else
            {
                Properties.Settings.Default.RememberedLogin = "";
                Properties.Settings.Default.RememberedPassword = "";
            }
            Properties.Settings.Default.Save();
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!puzzleSolved)
            {
                lblStatus.Text = "Сначала соберите капчу";
                return;
            }

            string login = txtLogin.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string hash = PasswordHelper.HashPassword(password);
            string role = "";
            int userId = 0, employeeId = 0;
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = @"SELECT u.ID_User, u.Role, u.EmployeeID FROM Users u 
                                 WHERE u.Login = @login AND u.PasswordHash = @hash AND u.IsActive = 1";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@hash", hash);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            userId = reader.GetInt32(0);
                            role = reader.GetString(1);
                            employeeId = reader.GetInt32(2);
                        }
                        else
                        {
                            MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
            }
            SaveRemembered();
            frmMain main = new frmMain(role, userId, employeeId);
            main.Show();
            this.Hide();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}