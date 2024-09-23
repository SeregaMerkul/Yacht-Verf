using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1;

namespace Yacht_Verf
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommand cmd;
        DataSet ds;
        private void bunifuFormDock1_FormDragging(object sender, Bunifu.UI.WinForms.BunifuFormDock.FormDraggingEventArgs e)
        {

        }

        private void bunifuButtonLogin_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(DataBaseWorker.ConnectString);

                string Sql = "Select * from Users where Login='" + bunifuTextBoxEmail.Text.Trim() + "'" +
                    " and Password='" + bunifuTextBoxPassword.Text.Trim() + "'";

                SqlDataAdapter sda = new SqlDataAdapter(Sql, conn);
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                sda.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    string userRole = dt.Rows[0]["Role"].ToString();
                    int idUser = Convert.ToInt32(dt.Rows[0]["Id_User"]);// Get the role value from the DataTable
                    string email = dt.Rows[0]["Login"].ToString().Trim();
                    this.Hide();
                    Console.WriteLine(idUser);
                    if (userRole == "Клиент")
                    {
                        GlavClient glavClient = new GlavClient(idUser, email);
                        glavClient.Show();
                    }
                    else if (userRole == "admin")
                    {
                        GlavAdmin glavAdmin = new GlavAdmin();
                        glavAdmin.Show();
                    }
                    else
                    {
                        MessageBox.Show("Неподдерживаемая роль: " + userRole);
                    }
                }
                else
                {
                    MessageBox.Show("Введите правильно логин и пароль.");
                }
            }
            catch
            {
                MessageBox.Show("Заполните все поля!");
            }
        }
        private void bunifuLabelRegister_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
        }

        private void bunifuLabelRegister_MouseHover(object sender, EventArgs e)
        {

        }

        private void bunifuLabelRegister_MouseEnter(object sender, EventArgs e)
        {
            bunifuLabelRegister.ForeColor = Color.Black;
        }

        private void bunifuLabelRegister_MouseLeave(object sender, EventArgs e)
        {
            bunifuLabelRegister.ForeColor = Color.Gray;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
        private Point mouseOffset;
        private bool isMouseDown = false;
        private void LoginForm_MouseDown(object sender, MouseEventArgs e)
        {
            {
                int xOffset;
                int yOffset;

                if (e.Button == MouseButtons.Left)
                {
                    xOffset = -e.X - SystemInformation.FrameBorderSize.Width;
                    yOffset = -e.Y - SystemInformation.CaptionHeight -
                    SystemInformation.FrameBorderSize.Height;
                    mouseOffset = new Point(xOffset, yOffset);
                    isMouseDown = true;
                }
            }
        }

        private void LoginForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouseOffset.X, mouseOffset.Y);
                Location = mousePos;
            }
        }

        private void LoginForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = false;
            }
        }

        private void bunifuTextBoxPassword_Enter(object sender, EventArgs e)
        {
            bunifuTextBoxPassword.UseSystemPasswordChar = true;
        }

        private void bunifuTextBoxPassword_Leave(object sender, EventArgs e)
        {
            bunifuTextBoxPassword.UseSystemPasswordChar = false;

        }
    }
}
