using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1;
using ZXing;
using ZXing.Common;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Yacht_Verf
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
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

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButtonRegister_Click(object sender, EventArgs e)
        {
            
        }
        
        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            

        }




        public string code;
        private void bunifuButtonLogin_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(DataBaseWorker.ConnectString))
            {
                connection.Open();

                string query = "SELECT Id_User FROM Users WHERE Login = @Email";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", (bunifuTextBox1.Text.Trim()).ToString());

                    // Выполнение запроса и проверка наличия пользователя
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        MessageBox.Show("Пользователь с данным email уже существует");
                    }
                    else
                    {
                        if (bunifuCheckBoxApprove.Checked)
                        {
                            Random r = new Random();

                            string uniqueCode = r.Next(000000, 999999).ToString();
                            code = uniqueCode;
                            // Адрес отправителя и получателя
                            string fromEmail = "wyacht@mail.ru"; // Замените на ваш реальный адрес отправителя
                            string toEmail = bunifuTextBox1.Text ; // Замените на адрес получателя


                            string smtpServer = "smtp.mail.ru"; // Замените на SMTP-сервер вашего почтового провайдера
                            int smtpPort = 587; // Порт для сервера Gmail
                            string smtpUsername = "wyacht@mail.ru"; // Замените на ваш реальный адрес отправителя
                            string smtpPassword = "f3MV0M1aNhPxMM1xxPfY"; // Замените на пароль вашего почтового аккаунта

                            // Создание сообщения
                            MailMessage message = new MailMessage(fromEmail, toEmail);
                            message.Subject = "Подтверждение заказа";
                            message.Body = $"Ваш уникальный 6-ти значный код: {uniqueCode}";

                            // Настройка клиента SMTP
                            SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort);
                            smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                            smtpClient.EnableSsl = true;

                            try
                            {
                                // Отправка сообщения
                                smtpClient.Send(message);
                                MessageBox.Show("Уникальный 6-ти значный код отправлен по электронной почте.");
                                bunifuPages1.SelectedTab = tabPage2;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Ошибка при отправке почты: {ex.Message}");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Подтвердите согласие на обработку персональных данных!");
                        }
                    }
                }
            }
            
        }

        private void TogglePasswordChar()
        {
            if (string.IsNullOrEmpty(bunifuTextBox2.Text))
            {
                // Если текстовое поле пустое, не используем SystemPasswordChar
                bunifuTextBox2.UseSystemPasswordChar = false;
            }
            else
            {
                // Если есть значение в текстовом поле, используем SystemPasswordChar
                bunifuTextBox2.UseSystemPasswordChar = true;
            }
            if (string.IsNullOrEmpty(bunifuTextBox3.Text))
            {
                // Если текстовое поле пустое, не используем SystemPasswordChar
                bunifuTextBox3.UseSystemPasswordChar = false;
            }
            else
            {
                // Если есть значение в текстовом поле, используем SystemPasswordChar
                bunifuTextBox3.UseSystemPasswordChar = true;
            }
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            if (bunifuTextBox12.Text == code.ToString())
            {
                bunifuPages1.SelectedTab = tabPage3;
            }
            else
            {
                MessageBox.Show("Код неверный!");
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            bunifuPages1.SelectedTab = tabPage1;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
            
        }
        private Point mouseOffset;
        private bool isMouseDown = false;
        private void RegisterForm_MouseDown(object sender, MouseEventArgs e)
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

        private void RegisterForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouseOffset.X, mouseOffset.Y);
                Location = mousePos;
            }
        }

        private void RegisterForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = false;
            }
        }

        private void bunifuTextBox2_Enter(object sender, EventArgs e)
        {
            bunifuTextBox2.UseSystemPasswordChar = true;

        }

        private void bunifuTextBox2_Leave(object sender, EventArgs e)
        {
            TogglePasswordChar();

        }

        private void bunifuButton1_Click_1(object sender, EventArgs e)
        {
            
                con = new SqlConnection(DataBaseWorker.GetConnectString());
                con.Open();
                ArrayList Parameters = new ArrayList
                {
                 "@Login", bunifuTextBox1.Text,
                "@Password",bunifuTextBox2.Text,
                "@Role", "Клиент"
                };
                DataBaseWorker.ExecuteStoredProcedure("AddClient", Parameters);

            using (SqlConnection connection = new SqlConnection(DataBaseWorker.ConnectString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("INSERT INTO Clients (Lastname, Name, Secondname, Birthday, Phone, Passport, Seria_Passport, Number_Passport, Email, Id_User) VALUES (@Lastname, @Name, @Secondname, @Birthday, @Phone, @Passport, @Seria_Passport, @Number_Passport, @Email, (SELECT Id_User FROM Users WHERE Login = @Email))", connection))
                {


                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Lastname", (bunifuTextBox7.Text.Trim()).ToString());
                    cmd.Parameters.AddWithValue("@Name", (bunifuTextBox5.Text.Trim()).ToString());
                    cmd.Parameters.AddWithValue("@Secondname", (bunifuTextBox6.Text.Trim()).ToString());
                    cmd.Parameters.AddWithValue("@Birthday", bunifuDatePicker1.Value);
                    cmd.Parameters.AddWithValue("@Phone", (bunifuTextBox10.Text.Trim()).ToString());
                    cmd.Parameters.AddWithValue("@Passport", passport);
                    cmd.Parameters.AddWithValue("@Seria_Passport", (bunifuTextBox8.Text.Trim()).ToString());
                    cmd.Parameters.AddWithValue("@Number_Passport", (bunifuTextBox11.Text.Trim()).ToString());
                    cmd.Parameters.AddWithValue("@Email", (bunifuTextBox1.Text.Trim()).ToString());
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }

            MessageBox.Show("Регистрация прошла успешно.");
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
                this.Hide();
        }
        public string passport;
        private void bunifuRadioButton1_CheckedChanged2(object sender, Bunifu.UI.WinForms.BunifuRadioButton.CheckedChangedEventArgs e)
        {
            if (bunifuRadioButton1.Checked)
            {
                bunifuRadioButton2.Checked = false;
                passport = "РФ";
            }
        }

        private void bunifuRadioButton2_CheckedChanged2(object sender, Bunifu.UI.WinForms.BunifuRadioButton.CheckedChangedEventArgs e)
        {
            if (bunifuRadioButton2.Checked)
            {
                bunifuRadioButton1.Checked = false;
                passport = "Заграничный";
            }
        }

        private void bunifuTextBox3_Enter(object sender, EventArgs e)
        {
            bunifuTextBox3.UseSystemPasswordChar = true;
        }
    }
}
