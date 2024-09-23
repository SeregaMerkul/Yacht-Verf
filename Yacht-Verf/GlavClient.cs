using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using WindowsFormsApp1;
using System.IO;
using System.Runtime.InteropServices;
using Bunifu.UI.WinForms.BunifuButton;
using Bunifu.UI.WinForms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using System.Reflection;
using Image = System.Drawing.Image;
using System.Net.Mail;
using System.Net;
using Bunifu.Framework.UI;
using Bunifu.UI.WinForms.BunifuTextbox;
using ZXing.Common;
using ZXing;
using System.Diagnostics;

namespace Yacht_Verf
{
    public partial class GlavClient : Form
    {
        public int Id_User;
        public string Email_User;
        public int User_ID;
        public GlavClient(int idUser, string email)
        {
            InitializeComponent();
            flowLayoutPanel1.MouseWheel += GlavAdmin_MouseWheel;
            Id_User = idUser;
            Email_User = email;
        }
        void TakeIdClient()
        {
            using (SqlConnection connection = new SqlConnection(DataBaseWorker.ConnectString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT Id_Client FROM Clients Where Id_User=@IdUser", connection))
                {
                    cmd.Parameters.AddWithValue("@IdUser", Id_User);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            User_ID = Convert.ToInt32(reader["Id_Client"]);

                        }
                        // Если нет данных, выполните необходимые действия или бросьте исключение.
                    }
                }

                connection.Close();
            }
        }
        private void GlavAdmin_MouseWheel(object sender, MouseEventArgs e)
        {


            bunifuVScrollBar1.Value = flowLayoutPanel1.VerticalScroll.Value;
            try
            {
                bunifuVScrollBar1.Maximum = flowLayoutPanel1.VerticalScroll.Maximum - 637;
            }
            catch
            {

            }
        }
        private void bunifuLabel1_Click(object sender, EventArgs e)
        {
            bunifuPages1.SelectedTab = YachtsPage;
        }

        private void bunifuLabel1_MouseEnter(object sender, EventArgs e)
        {
            bunifuLabel1.ForeColor = Color.White;
        }

        private void bunifuLabel1_MouseLeave(object sender, EventArgs e)
        {
            bunifuLabel1.ForeColor = Color.Gray;
        }

        private void bunifuLabel2_MouseEnter(object sender, EventArgs e)
        {
            bunifuLabel2.ForeColor = Color.White;
        }

        private void bunifuLabel2_MouseLeave(object sender, EventArgs e)
        {
            bunifuLabel2.ForeColor = Color.Gray;
        }

        private void bunifuLabel3_MouseEnter(object sender, EventArgs e)
        {
            bunifuLabel3.ForeColor = Color.White;
        }

        private void bunifuLabel3_MouseLeave(object sender, EventArgs e)
        {
            bunifuLabel3.ForeColor = Color.Gray;
        }

        private void bunifuLabel4_MouseEnter(object sender, EventArgs e)
        {
            bunifuLabel4.ForeColor = Color.White;
        }
        public int idBoat;
        public string name;
        public int idType;
        public bool mast;
        public int priceWithoutAccessory;
        public int numberOfSeats;
        public string description;
        public int power;
        public int tankVolume;
        public byte[] photo;
        public System.Drawing.Image image;
        private void TakeBoatFromId(int id_test)
        {
            using (SqlConnection connection = new SqlConnection(DataBaseWorker.ConnectString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT  * FROM boats Where Id_boat=@Id_boat", connection))
                {
                    cmd.Parameters.AddWithValue("@Id_boat", id_test);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            idBoat = Convert.ToInt32(reader["Id_Boat"]);
                            name = reader["Name_boat"].ToString();
                            idType = Convert.ToInt32(reader["Id_Type_boat"]);
                            mast = Convert.ToBoolean(reader["Mast"]);
                            priceWithoutAccessory = Convert.ToInt32(reader["Price_without_Accessory"]);
                            numberOfSeats = Convert.ToInt32(reader["Number_of_Seats"]);
                            description = reader["Discription"].ToString();
                            power = Convert.ToInt32(reader["Power"]);
                            tankVolume = Convert.ToInt32(reader["Tank_volume"]);
                            photo = (byte[])reader["Photo"];

                            using (MemoryStream ms = new MemoryStream(photo))
                            {
                                image = System.Drawing.Image.FromStream(ms);
                            }
                        }
                        // Если нет данных, выполните необходимые действия или бросьте исключение.
                    }
                }

                connection.Close();
            }
        }
        private void bunifuLabel4_MouseLeave(object sender, EventArgs e)
        {
            bunifuLabel4.ForeColor = Color.Gray;
        }
        public int count_boats;
        public void Test()
        {
            using (SqlConnection con = new SqlConnection(DataBaseWorker.ConnectString))
            {
                con.Open();
                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Boats", con);
                count_boats = (int)command.ExecuteScalar();
                con.Close();
            }

        }
        private void GlavClient_Load(object sender, EventArgs e)
        {
            TakeIdClient();
            Console.WriteLine(User_ID);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "yacht_VerfDataSet19.Engines". При необходимости она может быть перемещена или удалена.
            this.enginesTableAdapter.Fill(this.yacht_VerfDataSet19.Engines);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "yacht_VerfDataSet18.Types_Boat". При необходимости она может быть перемещена или удалена.
            this.types_BoatTableAdapter.Fill(this.yacht_VerfDataSet18.Types_Boat);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "yacht_VerfDataSet3.Orders_RU". При необходимости она может быть перемещена или удалена.
            this.orders_RUTableAdapter1.Fill(this.yacht_VerfDataSet3.Orders_RU);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "yacht_VerfDataSet2.Orders_RU". При необходимости она может быть перемещена или удалена.
            this.orders_RUTableAdapter.Fill(this.yacht_VerfDataSet2.Orders_RU);

            if (bunifuDataGridView1.Rows.Count > 0)
            {
                bunifuDataGridView1.Rows[0].Selected = true;
                // Вызов обработчика события CellClick, чтобы обработать выбор первой строки
                bunifuDataGridView1_CellClick(sender, new DataGridViewCellEventArgs(0, 0));
            }

            Test();
            Console.WriteLine("А может так?" + count_boats);

            string connectionString = DataBaseWorker.ConnectString; // Замените на свою строку подключения
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = "SELECT * FROM boats";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Получаем id_boat для каждой строки
                            int idBoat = reader.GetInt32(reader.GetOrdinal("id_boat"));

                            Console.WriteLine("Я тута!");
                            TakeBoatFromId(idBoat);
                            CreateNewPanel(image, idBoat, priceWithoutAccessory);
                        }
                    }
                }
            }

        }

        private void bunifuLabel3_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }

        private void bunifuLabel4_Click(object sender, EventArgs e)
        {
            bunifuPages1.SelectedTab = OrdersPage;
            string query = $"SELECT * FROM Orders_RU where [номер_клиента] = (SELECT Id_Client FROM Clients where id_user = {Id_User})";
            // Здесь вы можете использовать ваш способ подключения к базе данных (например, через SqlConnection)

            using (SqlConnection connection = new SqlConnection(DataBaseWorker.ConnectString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        bunifuDataGridView1.DataSource = dataTable;
                    }
                }
            }
        }
        private void bunifuLabel2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButtonBuy_Click(object sender, EventArgs e)
        {
            bunifuPages2.SelectedTab = Yacht1Page;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            bunifuVScrollBar1.Visible = true;
            bunifuLabel1.Visible = true;
            bunifuLabel2.Visible = true;
            bunifuLabel3.Visible = true;
            bunifuLabel4.Visible = true;
            bunifuPages2.SelectedTab = CatalogPage;
        }

        private void Yacht1Page_Click(object sender, EventArgs e)
        {

        }
        private Point mouseOffset;
        private bool isMouseDown = false;
        private void GlavClient_MouseDown(object sender, MouseEventArgs e)
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
        public void Take_Last_Boat()
        {
            using (SqlConnection connection = new SqlConnection(DataBaseWorker.ConnectString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 * FROM boats ORDER BY Id_Boat DESC", connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            idBoat = Convert.ToInt32(reader["Id_Boat"]);
                            name = reader["Name_boat"].ToString();
                            idType = Convert.ToInt32(reader["Id_Type_boat"]);
                            mast = Convert.ToBoolean(reader["Mast"]);
                            priceWithoutAccessory = Convert.ToInt32(reader["Price_without_Accessory"]);
                            numberOfSeats = Convert.ToInt32(reader["Number_of_Seats"]);
                            description = reader["Discription"].ToString();
                            power = Convert.ToInt32(reader["Power"]);
                            tankVolume = Convert.ToInt32(reader["Tank_volume"]);
                            photo = (byte[])reader["Photo"];


                        }

                        using (MemoryStream ms = new MemoryStream(photo))
                        {
                            image = Image.FromStream(ms);
                        }



                    }
                }

                connection.Close();
            }

        }
        public int last_id_boat;
        private void bunifuButtonBuy_Click_1(object sender, EventArgs e)
        {
            BunifuButton clickedButton = (BunifuButton)sender;
            if (clickedButton.Tag is CustomButtonData buttonData)
            {
                int value = buttonData.Value;
                last_id_boat = value;
                TakeBoatFromId(value);
            }

            bunifuVScrollBar1.Visible = false;
            bunifuLabel1.Visible = false;
            bunifuLabel2.Visible = false;
            bunifuLabel3.Visible = false;
            bunifuLabel4.Visible = false;
            bunifuPages2.SelectedTab = Yacht1Page;
            bunifuLabel9.Text = name;
            pictureBox4.Image = image;



            int maxCharsPerLine = 75;

            string formattedDescription = "";

            for (int i = 0; i < description.Length; i += maxCharsPerLine)
            {
                int charsLeft = Math.Min(maxCharsPerLine, description.Length - i);
                formattedDescription += description.Substring(i, charsLeft) + Environment.NewLine;
            }
            bunifuLabel10.Text = formattedDescription;

            string formattedNumber = FormatNumberWithSeparators(priceWithoutAccessory);


            bunifuLabel8.Text = "$ " + formattedNumber;
            double percentagePower = (power / 4000.0) * 100;
            int roundedPercentagePower = (int)Math.Round(percentagePower);

            double percentageNumberOfSeats = (numberOfSeats / 72.0) * 100;
            int roundedPercentageNumberOfSeats = (int)Math.Round(percentageNumberOfSeats);

            double percentageTankVolume = (tankVolume / 5000.0) * 100;
            int roundedPercentageTankVolume = (int)Math.Round(percentageTankVolume);

            Random r = new Random();

            // Power
            if (power >= 4000)
            {
                bunifuProgressBar4.Value = 100;
                bunifuProgressBar1.Value = 85;
            }
            else if (power <= 0)
            {
                bunifuProgressBar4.Value = 0;
                bunifuProgressBar1.Value = 0;
            }
            else
            {
                bunifuProgressBar4.Value = roundedPercentagePower;
                bunifuProgressBar1.Value = Math.Min(100, roundedPercentagePower + 5);
            }

            // Passengers
            if (numberOfSeats >= 72)
            {
                bunifuProgressBar2.Value = 100;
            }
            else if (numberOfSeats <= 0)
            {
                bunifuProgressBar2.Value = 0;
            }
            else
            {
                bunifuProgressBar2.Value = roundedPercentageNumberOfSeats;
            }

            // Gas
            if (tankVolume >= 5000)
            {
                bunifuProgressBar3.Value = 100;
            }
            else if (tankVolume <= 0)
            {
                bunifuProgressBar3.Value = 0;
            }
            else
            {
                bunifuProgressBar3.Value = roundedPercentageTankVolume;
            }






        }
        private void RemovePanel(int panelIndex)
        {
            // Удаляем панель по индексу
            flowLayoutPanel1.Controls.RemoveAt(panelIndex);


        }
        public List<int> index = new List<int>();
        private void bunifuButtonBuy_Click_Delete(object sender, EventArgs e, int test)
        {

            int valueToRemove = test;
            int lastindex = index.IndexOf(valueToRemove);
            // Проверяем наличие значения в списке перед удалением
            if (index.Contains(valueToRemove))
            {
                index.Remove(valueToRemove);
                SqlConnection connection = new SqlConnection(DataBaseWorker.ConnectString);
                SqlCommand command = new SqlCommand("delete from [Boats] where Id_Boat= @Id_Boat", connection);
                command.Parameters.AddWithValue("@Id_Boat", valueToRemove);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();


            }



            RemovePanel(lastindex);

        }
        private string FormatNumberWithSeparators(long number)
        {

            return string.Format("{0:#,0}", number);
        }
        public void CreateNewPanel(Image picture, int id_boat, int price)
        {
            CustomButtonData buttonDataDescription = new CustomButtonData("Описание", id_boat);
            CustomButtonData buttonDataDelete = new CustomButtonData("Удалить", id_boat);
            CustomButtonData buttonDataEdit = new CustomButtonData("Редактировать", id_boat);

            Console.WriteLine("Я в создании!");

            Take_Last_Boat();


            BunifuPanel newPanel = new BunifuPanel();
            newPanel.Size = new System.Drawing.Size(431, 295);
            newPanel.BorderColor = Color.Gray;
            newPanel.BorderRadius = 10;
            newPanel.BorderThickness = 2;
            newPanel.ShowBorders = true;
            newPanel.Margin = new Padding(0, 17, 25, 0);


            // Создаем новую кнопку
            BunifuButton newbuttonDescription = new BunifuButton();
            newbuttonDescription.Tag = buttonDataDescription;
            newbuttonDescription.Visible = true;
            newbuttonDescription.Size = new Size(154, 46);
            newbuttonDescription.Location = new Point(253, 242);
            newbuttonDescription.Text = buttonDataDescription.DisplayText;
            Console.WriteLine("Я создал кнопку " + newbuttonDescription);
            newbuttonDescription.FocusState = BunifuButton.ButtonStates.Idle;
            newbuttonDescription.Font = new Font("Segoe UI Semibold", 14, FontStyle.Bold);
            newbuttonDescription.ForeColor = Color.FromArgb(255, 221, 180);
            newbuttonDescription.IdleBorderColor = Color.FromArgb(255, 221, 180);
            newbuttonDescription.IdleBorderRadius = 25;
            newbuttonDescription.IdleBorderThickness = 2;
            newbuttonDescription.IdleFillColor = Color.Transparent;
            //onHoverState
            newbuttonDescription.onHoverState.BorderColor = Color.FromArgb(255, 221, 140);
            newbuttonDescription.onHoverState.BorderRadius = 25;
            newbuttonDescription.onHoverState.BorderThickness = 2;
            newbuttonDescription.onHoverState.ForeColor = Color.Black;
            newbuttonDescription.onHoverState.FillColor = Color.FromArgb(255, 221, 180);
            //onIdleState
            newbuttonDescription.OnIdleState.BorderColor = Color.FromArgb(255, 221, 180);
            newbuttonDescription.OnIdleState.BorderRadius = 25;
            newbuttonDescription.OnIdleState.BorderThickness = 2;
            newbuttonDescription.OnIdleState.ForeColor = Color.FromArgb(255, 221, 180);
            newbuttonDescription.OnIdleState.FillColor = Color.Transparent;
            //onPressedState
            newbuttonDescription.OnPressedState.BorderColor = Color.FromArgb(255, 221, 180);
            newbuttonDescription.OnPressedState.BorderRadius = 25;
            newbuttonDescription.OnPressedState.BorderThickness = 2;
            newbuttonDescription.OnPressedState.ForeColor = Color.Black;
            newbuttonDescription.OnPressedState.FillColor = Color.FromArgb(255, 221, 180);
            newbuttonDescription.Click += bunifuButtonBuy_Click_1;

            // Создаем новый PictureBox
            PictureBox newPictureBox = new PictureBox();
            newPictureBox.Size = new Size(384, 237);
            newPictureBox.Location = new Point(23, 5);
            newPictureBox.Image = picture;
            newPictureBox.SizeMode = PictureBoxSizeMode.Zoom;


            //Создание нового label
            Label newLabelPrice = new Label();
            newLabelPrice.Text = ("$" + FormatNumberWithSeparators(price));
            newLabelPrice.Location = new Point(15, 248);
            newLabelPrice.Font = new Font("Segoe UI Semibold", 18, FontStyle.Bold);
            newLabelPrice.ForeColor = Color.FromArgb(255, 221, 180);
            newLabelPrice.Size = new Size(200, 32);
            Console.WriteLine(priceWithoutAccessory);


            index.Add(id_boat);
            pictureBoxes.Add(newPictureBox);
            labelPrice.Add(newLabelPrice);
            indexPoPoryadku.Add(id_boat);
            // Добавляем созданные элементы на форму
            flowLayoutPanel1.Controls.Add(newPanel);
            newPanel.Controls.Add(newbuttonDescription);
            newPanel.Controls.Add(newPictureBox);
            newPanel.Controls.Add(newLabelPrice);
        }




        public List<PictureBox> pictureBoxes = new List<PictureBox>();
        public List<Label> labelPrice = new List<Label>();
        public List<int> indexPoPoryadku = new List<int>();

        private void GlavClient_MouseMove(object sender, MouseEventArgs e)
        {
            {
                if (isMouseDown)
                {
                    Point mousePos = Control.MousePosition;
                    mousePos.Offset(mouseOffset.X, mouseOffset.Y);
                    Location = mousePos;
                }
            }
        }

        private void GlavClient_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = false;
            }
        }
        private string GenerateUniqueCode()
        {
            Random random = new Random();
            code = random.Next(100000, 999999);
            return code.ToString();
        }
        public int code;
        private void bunifuButtonBuy_Click_2(object sender, EventArgs e)
        {
            bunifuTextBox4.Visible = true;
            pictureBox1.Visible = true;
            QRCodePictureBox.Visible = true;
            bunifuLabel19.Visible = true;

            string registrationCode = GenerateUniqueCode();

            // Создаем QR-код
            Bitmap qrCodeBitmap = GenerateQRCode(registrationCode);

            // Отображаем QR-код
            QRCodePictureBox.Image = qrCodeBitmap;



        }
        private Bitmap GenerateQRCode(string data)
        {
            BarcodeWriter barcodeWriter = new BarcodeWriter();
            barcodeWriter.Format = BarcodeFormat.QR_CODE;

            // Устанавливаем параметры QR-кода (размер, цвет и т.д.)
            EncodingOptions encodingOptions = new ZXing.QrCode.QrCodeEncodingOptions
            {
                Width = 200,
                Height = 200,
                Margin = 0,
                ErrorCorrection = ZXing.QrCode.Internal.ErrorCorrectionLevel.H
            };

            barcodeWriter.Options = encodingOptions;

            // Генерируем QR-код из переданных данных
            Bitmap qrCodeBitmap = barcodeWriter.Write(data);

            return qrCodeBitmap;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (bunifuTextBox4.Text == code.ToString())
            {
                using (SqlConnection connection = new SqlConnection(DataBaseWorker.ConnectString))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Orders (Id_Client, Date_Order, Id_Boat, Stage_Order) VALUES ((SELECT Id_Client FROM Clients WHERE Id_User = @Id_User), @Date_Order, @Id_Boat, @Stage_Order)", connection))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Id_User", Id_User);
                        cmd.Parameters.AddWithValue("@Date_Order", DateTime.Now);
                        cmd.Parameters.AddWithValue("@Id_Boat", last_id_boat);
                        cmd.Parameters.AddWithValue("@Stage_Order", "Заявка подана");

                        cmd.ExecuteNonQuery();
                    }
                    connection.Close();
                }
                MessageBox.Show("Заказ подтвержден!");
            }
            else
            {
                MessageBox.Show("Код неверный!");
            }
        }
        public int id_order;
        public string name_type;
        private void bunifuDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Проверка, что индекс строки действителен
            {
                DataGridViewRow selectedRow = bunifuDataGridView1.Rows[e.RowIndex];
                // Теперь у вас есть доступ к данным выбранной строки
                id_order = Convert.ToInt32(selectedRow.Cells["номерзаказаDataGridViewTextBoxColumn"].Value.ToString());
                Console.WriteLine(id_order);
                // Обработайте данные по вашему усмотрению
            }
            using (SqlConnection connection = new SqlConnection(DataBaseWorker.ConnectString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT Id_boat from Orders WHERE Id_Order = @Id_Order", connection))
                {
                    cmd.Parameters.AddWithValue("@Id_Order", id_order);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            idBoat = Convert.ToInt32(reader["Id_Boat"]);
                        }
                    }
                }
                connection.Close();
            }
            TakeBoatFromId(idBoat);

            using (SqlConnection connection = new SqlConnection(DataBaseWorker.ConnectString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT Name_type_boat from Types_Boat WHERE Id_Type_Boat = @Id_Type_Boat", connection))
                {
                    cmd.Parameters.AddWithValue("@Id_Type_Boat", idType);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            name_type = reader["Name_type_boat"].ToString();
                        }
                    }
                }
                connection.Close();
            }

            bunifuLabel5.Text = name_type.ToString();
            bunifuLabel6.Text = power.ToString();
            bunifuLabel7.Text = name.ToString();
            bunifuLabel14.Text = tankVolume.ToString();
            bunifuLabel15.Text = numberOfSeats.ToString();
            pictureBox2.Image = image;
            if (mast)
            {
                bunifuLabel21.Text = "Имеется";
            }
            else
            {
                bunifuLabel21.Text = "Отсутствует";
            }
        }

        private void NewsPage_Click(object sender, EventArgs e)
        {

        }
        void Get_list_DropDown1()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(DataBaseWorker.ConnectString))
                {
                    con.Open();
                    string query = "SELECT * FROM engines WHERE Power_Engine < (SELECT power_engine FROM Technology WHERE id_type_boat = @IdTypeBoat)";
                    using (SqlDataAdapter da = new SqlDataAdapter(query, con))
                    {
                        da.SelectCommand.Parameters.AddWithValue("@IdTypeBoat", Convert.ToInt32(bunifuDropdown4.SelectedValue.ToString()));

                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds, "engines");

                            bunifuDropdown1.BeginUpdate();
                            bunifuDropdown1.DataSource = ds.Tables["engines"];
                            bunifuDropdown1.DisplayMember = "Name_Engine";
                            bunifuDropdown1.ValueMember = "Id_Engine";
                            bunifuDropdown1.EndUpdate();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void bunifuLabel2_Click_1(object sender, EventArgs e)
        {
            bunifuPages1.SelectedTab = VerfPage;
        }
        public string nameOfType;
        public string MaterialHull;
        public int CountMaterialHull;
        public string MaterialDeck;
        public int CountMaterialDeck;
        public string MaterialSail;
        public int CountMaterialSail;
        public int Height;
        public int Width;
        public int Length;
        public int CountEngines;
        public int powerEngine;
        public int priceEngine;
        public bool Mast;
        public int priceBoat = 0;
        private void bunifuDropdown4_SelectedIndexChanged(object sender, EventArgs e)
        {
            Get_list_DropDown1();
            using (SqlConnection connection = new SqlConnection(DataBaseWorker.ConnectString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Types_Boat WHERE id_type_boat = @idTypeBoat", connection))
                {
                    cmd.Parameters.AddWithValue("@idTypeBoat", bunifuDropdown4.SelectedValue);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            nameOfType = reader["Name_Type_Boat"].ToString();
                        }

                    }
                }

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Technology_RU WHERE [Тип судна] = @TypeBoat", connection))
                {
                    cmd.Parameters.AddWithValue("@TypeBoat", bunifuDropdown4.SelectedValue);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                MaterialHull = reader["Материал корпуса"].ToString();
                                CountMaterialHull = Convert.ToInt32(reader["Кол-во материала (корпус)"]);
                                MaterialDeck = reader["Материал палуба"].ToString();
                                CountMaterialDeck = Convert.ToInt32(reader["Кол-во материала (палуба)"]);
                                MaterialSail = reader["Материал паруса"].ToString();
                                CountMaterialSail = Convert.ToInt32(reader["Кол-во материала (парус)"]);
                                Height = Convert.ToInt32(reader["Высота"]);
                                Width = Convert.ToInt32(reader["Ширина"]);
                                Length = Convert.ToInt32(reader["Длина"]);
                                Mast = Convert.ToBoolean(reader["Наличие мачты"].ToString());
                                CountEngines = Convert.ToInt32(reader["Кол-во двигателей"]);
                                powerEngine = Convert.ToInt32(reader["Мощность двигателей"]);
                                priceBoat = Convert.ToInt32(reader["Стоимость судна"]);
                            }
                        }
                        bunifuLabel50.Text = $"${priceBoat}";
                    }
                }


            }
        }

        private void bunifuButton9_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(DataBaseWorker.ConnectString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("INSERT INTO Zayavka (Id_Client, Id_type_boat, Color, Date_Zayavka, Status_zayavka, Id_Technology, Id_Engine, Price_boat) VALUES (@Id_Client, @Id_type_boat, @Color, @Date_Zayavka, N'Заявка подана', (SELECT Id_technology From Technology WHERE Id_type_boat = @Id_type_boat), @Id_Engine, @Price_boat)", connection))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Id_Client", User_ID);
                    cmd.Parameters.AddWithValue("@Id_type_boat", Convert.ToInt32(bunifuDropdown4.SelectedValue.ToString()));
                    cmd.Parameters.AddWithValue("@Color", bunifuDropdown2.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Date_Zayavka", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Id_Engine", Convert.ToInt32(bunifuDropdown1.SelectedValue.ToString()));
                    cmd.Parameters.AddWithValue("@Price_boat", priceBoat);

                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
            MessageBox.Show("Заказ подтвержден!");
        }

        private void bunifuDropdown1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(DataBaseWorker.ConnectString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Engines WHERE Id_Engine = @Engine", connection))
                {
                    cmd.Parameters.AddWithValue("@Engine", bunifuDropdown1.SelectedValue);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                priceEngine = Convert.ToInt32(reader["Price_Engine"].ToString());

                            }
                        }
                        priceBoat += priceEngine;
                        bunifuLabel50.Text = $"${priceBoat}";
                    }
                }
            }
        }

        private void bunifuDropdown3_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = bunifuDropdown3.SelectedIndex;
            
            // Обработка изменения с 3-го на 4-ый и наоборот
            if (selectedIndex == 2) // Переход с 3-го на 4-ый
            {
                priceBoat -= 50000;
                priceBoat += 100000;
            }
            else if (selectedIndex == 3) // Переход с 4-го на 3-ий
            {
                priceBoat += 50000;
                priceBoat -= 100000;
            }
            bunifuLabel50.Text = $"${priceBoat}";
        }

        private void UpdateCheckBox(BunifuCheckBox checkBox, int price)
        {
            if (checkBox.Checked)
            {
                priceBoat += price;
            }
            else
            {
                priceBoat -= price;
            }

            bunifuLabel50.Text = $"${priceBoat}";
        }

        private void bunifuCheckBox1_CheckedChanged(object sender, BunifuCheckBox.CheckedChangedEventArgs e)
        {
            UpdateCheckBox(bunifuCheckBox1, 25000);
        }

        private void bunifuCheckBox2_CheckedChanged(object sender, BunifuCheckBox.CheckedChangedEventArgs e)
        {
            UpdateCheckBox(bunifuCheckBox2, 20000);
        }

        private void bunifuCheckBox3_CheckedChanged(object sender, BunifuCheckBox.CheckedChangedEventArgs e)
        {
            UpdateCheckBox(bunifuCheckBox3, 15000);
        }

        private void bunifuCheckBox5_CheckedChanged(object sender, BunifuCheckBox.CheckedChangedEventArgs e)
        {
            UpdateCheckBox(bunifuCheckBox5, 8000);
        }

        private void bunifuCheckBox6_CheckedChanged(object sender, BunifuCheckBox.CheckedChangedEventArgs e)
        {
            UpdateCheckBox(bunifuCheckBox6, 12000);
        }

        private void bunifuCheckBox4_CheckedChanged(object sender, BunifuCheckBox.CheckedChangedEventArgs e)
        {
            UpdateCheckBox(bunifuCheckBox4, 12000);
        }

        private void bunifuCheckBox7_CheckedChanged(object sender, BunifuCheckBox.CheckedChangedEventArgs e)
        {
            UpdateCheckBox(bunifuCheckBox7, 20000);
        }

        private void bunifuCheckBox8_CheckedChanged(object sender, BunifuCheckBox.CheckedChangedEventArgs e)
        {
            UpdateCheckBox(bunifuCheckBox8, 30000);
        }

        private void bunifuCheckBox9_CheckedChanged(object sender, BunifuCheckBox.CheckedChangedEventArgs e)
        {
            UpdateCheckBox(bunifuCheckBox9, 15000);
        }

        private void bunifuCheckBox10_CheckedChanged(object sender, BunifuCheckBox.CheckedChangedEventArgs e)
        {
            UpdateCheckBox(bunifuCheckBox10, 30000);
        }

        private void bunifuDropdown2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine(bunifuDropdown2.SelectedItem.ToString());
        }

        private void bunifuLabel2_MouseEnter_1(object sender, EventArgs e)
        {
            bunifuLabel2.ForeColor = Color.White;
        }

        private void bunifuLabel2_MouseLeave_1(object sender, EventArgs e)
        {
            bunifuLabel2.ForeColor = Color.Gray;
        }
    }
}
