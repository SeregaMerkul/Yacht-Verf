using Bunifu.Framework.UI;
using Bunifu.UI.WinForms;
using Bunifu.UI.WinForms.BunifuButton;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1;
using Yacht_Verf.Properties;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using Button = System.Windows.Forms.Button;
using Label = System.Windows.Forms.Label;
using Bunifu.UI.WinForms.BunifuTextbox;
using System.Net.NetworkInformation;
using static Yacht_Verf.Yacht_VerfDataSet;

namespace Yacht_Verf
{
    public partial class GlavAdmin : Form
    {//string rol, int Id_User
        private bool isZayavkaView = true; // Флаг для отслеживания текущего представления
        public GlavAdmin()
        {
            InitializeComponent();

            zayavkaDataAdapter = new SqlDataAdapter("SELECT * FROM Zayavka_RU", DataBaseWorker.ConnectString);
            zayavkaDataTable = new DataTable();
            zayavkaDataAdapter.Fill(zayavkaDataTable);

            orderDataAdapter = new SqlDataAdapter("SELECT * FROM Orders_RU", DataBaseWorker.ConnectString);
            orderDataTable = new DataTable();
            orderDataAdapter.Fill(orderDataTable);

            // Установка начальных данных для DataGridView
            bunifuDataGridView4.DataSource = zayavkaDataTable;
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


            // Делайте что-то с полученным значением прокрутки, например, изменяйте положение элемента управления
            // или выполняйте другие действия в зависимости от направления прокрутки.
            // Например, если delta положительное, значит, колесо прокручено вперед, если отрицательное - назад.

            Console.WriteLine($"Scroll_Maximum: {bunifuVScrollBar1.Maximum}\n" +
                $"Original_Maximum: {CatalogPage.VerticalScroll.Maximum}\n" +
                $"Scroll_Value: {bunifuVScrollBar1.Value}\n" +
                $"Original_Value: {flowLayoutPanel1.VerticalScroll.Value}\n" +
                $"Thumn_Length: {bunifuVScrollBar1.ThumbLength}\n"); 
        }
        #region Tabi
        private void bunifuButton1_Click(object sender, EventArgs e)
        {

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

            bunifuPages1.SelectedTab = AddYachtsPage;
        }
        private void CoordsToNull()
        {
            count = 0;
            LastCount = 0;
            x = 475;
            y = 0;
            lasty = 0;
            lastPanel = false;
    }
        private void bunifuButton3_Click(object sender, EventArgs e)
        {

            bunifuPages1.SelectedTab = OrdersPage;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            bunifuPages1.SelectedTab = GlavPage;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            bunifuPages1.SelectedTab = GlavPage;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            bunifuPages1.SelectedTab = GlavPage;
        }
        #endregion
        public int count = 0;
        public int LastCount = 0;
        public int x = 475;
        public int y = 0;
        public int lasty = 0;
        public bool lastPanel = false;

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
                SqlCommand command = new SqlCommand("delete from [Boats] where Id_Boat= @Id_Boat",connection);
                command.Parameters.AddWithValue("@Id_Boat", valueToRemove);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();


            }

            

            RemovePanel(lastindex);
            
        }
        public void CreateNewPanel(Image picture, int id_boat, int price)
        {
            CustomButtonData buttonDataDescription = new CustomButtonData("Описание", id_boat);
            CustomButtonData buttonDataDelete = new CustomButtonData("Удалить", id_boat);
            CustomButtonData buttonDataEdit = new CustomButtonData("Редактировать", id_boat);

            Console.WriteLine("Я в создании!");
           
            Take_Last_Boat();
            Console.WriteLine($"Count: {count}\n" +
                $"LastCount: {LastCount} \n" +
                $"x: {x}  \n" +
                $"y: {y} \n" +
                $"lastPanel: {lastPanel}\n" +
                $"AutoScroll.X: {flowLayoutPanel1.VerticalScroll.Value} \n" +
                $"AutoScroll.Y: {bunifuVScrollBar1.Value} ");
          

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

            // Создаем новую кнопку
            BunifuButton newbuttonDelete = new BunifuButton();
            newbuttonDelete.Visible = false;
            newbuttonDelete.Tag = buttonDataDelete;
            newbuttonDelete.Size = new Size(154, 46);
            newbuttonDelete.Location = new Point(253, 242);
            newbuttonDelete.Text = buttonDataDelete.DisplayText;
            newbuttonDelete.FocusState = BunifuButton.ButtonStates.Idle;
            newbuttonDelete.Font = new Font("Segoe UI Semibold", 14, FontStyle.Bold);
            newbuttonDelete.ForeColor = Color.Red;
            newbuttonDelete.IdleBorderColor = Color.Red;
            newbuttonDelete.IdleBorderRadius = 25;
            newbuttonDelete.IdleBorderThickness = 2;
            newbuttonDelete.IdleFillColor = Color.Transparent;
            //onHoverState
            newbuttonDelete.onHoverState.BorderColor = Color.Red;
            newbuttonDelete.onHoverState.BorderRadius = 25;
            newbuttonDelete.onHoverState.BorderThickness = 2;
            newbuttonDelete.onHoverState.ForeColor = Color.Black;
            newbuttonDelete.onHoverState.FillColor = Color.Tomato;
            //onIdleS1tate
            newbuttonDelete.OnIdleState.BorderColor = Color.Red;
            newbuttonDelete.OnIdleState.BorderRadius = 25;
            newbuttonDelete.OnIdleState.BorderThickness = 2;
            newbuttonDelete.OnIdleState.ForeColor = Color.Red;
            newbuttonDelete.OnIdleState.FillColor = Color.Transparent;
            //onPressedState
            newbuttonDelete.OnPressedState.BorderColor = Color.Red;
            newbuttonDelete.OnPressedState.BorderRadius = 25;
            newbuttonDelete.OnPressedState.BorderThickness = 2;
            newbuttonDelete.OnPressedState.ForeColor = Color.Black;
            newbuttonDelete.OnPressedState.FillColor = Color.Tomato;
            newbuttonDelete.Click += (sender, e) => bunifuButtonBuy_Click_Delete(sender, e, buttonDataDelete.Value);
            Console.WriteLine("Я создал кнопку " + buttonDataDelete.Value);
            // Создаем новую кнопку
            BunifuButton newbuttonEdit = new BunifuButton();
            newbuttonEdit.Visible = false;
            newbuttonEdit.Tag = buttonDataEdit;
            newbuttonEdit.Size = new Size(154, 46);
            newbuttonEdit.Location = new Point(253, 242);
            newbuttonEdit.Text = buttonDataEdit.DisplayText;
            newbuttonEdit.FocusState = BunifuButton.ButtonStates.Idle;
            newbuttonEdit.Font = new Font("Segoe UI Semibold", 14, FontStyle.Bold);
            newbuttonEdit.ForeColor = Color.Green;
            newbuttonEdit.IdleBorderColor = Color.Green;
            newbuttonEdit.IdleBorderRadius = 25;
            newbuttonEdit.IdleBorderThickness = 2;
            newbuttonEdit.IdleFillColor = Color.Transparent;
            //onHoverState
            newbuttonEdit.onHoverState.BorderColor = Color.Green;
            newbuttonEdit.onHoverState.BorderRadius = 25;
            newbuttonEdit.onHoverState.BorderThickness = 2;
            newbuttonEdit.onHoverState.ForeColor = Color.Black;
            newbuttonEdit.onHoverState.FillColor = Color.Lime;
            //onIdleS1tate
            newbuttonEdit.OnIdleState.BorderColor = Color.Green;
            newbuttonEdit.OnIdleState.BorderRadius = 25;
            newbuttonEdit.OnIdleState.BorderThickness = 2;
            newbuttonEdit.OnIdleState.ForeColor = Color.Green;
            newbuttonEdit.OnIdleState.FillColor = Color.Transparent;
            //onPressedState
            newbuttonEdit.OnPressedState.BorderColor = Color.Green;
            newbuttonEdit.OnPressedState.BorderRadius = 25;
            newbuttonEdit.OnPressedState.BorderThickness = 2;
            newbuttonEdit.OnPressedState.ForeColor = Color.Black;
            newbuttonEdit.OnPressedState.FillColor = Color.Lime;
            newbuttonEdit.Click += (sender, e) => editButton_Click(sender, e, buttonDataEdit.Value);



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

            bunifuLabel4.Click += (sender, e) => DeleteLabelClick(newbuttonDescription, newbuttonDelete, newbuttonEdit);
            bunifuLabel3.Click += (sender, e) => EditLabelClick(newbuttonDescription, newbuttonDelete, newbuttonEdit);

            index.Add(id_boat);
            pictureBoxes.Add(newPictureBox);
            labelPrice.Add(newLabelPrice);
            indexPoPoryadku.Add(id_boat);
            // Добавляем созданные элементы на форму
            flowLayoutPanel1.Controls.Add(newPanel);
            newPanel.Controls.Add(newbuttonDescription);
            newPanel.Controls.Add(newbuttonDelete);
            newPanel.Controls.Add(newbuttonEdit);
            newPanel.Controls.Add(newPictureBox);
            newPanel.Controls.Add(newLabelPrice);
        }
        private void RemovePanel(int panelIndex)
        {
                              // Удаляем панель по индексу
        flowLayoutPanel1.Controls.RemoveAt(panelIndex);
                      
            
        }
        public List<PictureBox> pictureBoxes = new List<PictureBox>();
        public List<Label> labelPrice = new List<Label>();
        public List<int> indexPoPoryadku = new List<int>();
        private void editButton_Click(object sender, EventArgs e, int id_panel)
        {
            EditPanels editPanels = new EditPanels(id_panel);

            DialogResult result = editPanels.ShowDialog();
            Take_Last_Boat();

            // Проверьте результат
            if (result == DialogResult.OK || result == DialogResult.None)
            {

                TakeBoatFromId(id_panel);

                PictureBox pictureBox = pictureBoxes[indexPoPoryadku.IndexOf(id_panel)];
                pictureBox.Image = image;
                Label label = labelPrice[indexPoPoryadku.IndexOf(id_panel)];
                label.Text = "$" + FormatNumberWithSeparators(priceWithoutAccessory);


            }
            else
            {
                MessageBox.Show("Что-то пошло не так...");
            }




        }
        private void bunifuLabel1_Click(object sender, EventArgs e)
        {
            AddPanels addPanels = new AddPanels();

            
            DialogResult result = addPanels.ShowDialog();
            Take_Last_Boat();
                
            // Проверьте результат
            if (result == DialogResult.OK)
            {
                CreateNewPanel(image, idBoat, priceWithoutAccessory);
                
                
            }
            else
            {
                MessageBox.Show("Что-то пошло не так...");
            }

            
            
           
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
        public Image image;
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

        private void bunifuButtonBuy_Click_1(object sender, EventArgs e)
        {
            BunifuButton clickedButton = (BunifuButton)sender;
            if (clickedButton.Tag is CustomButtonData buttonData)
            {
                int value = buttonData.Value;
                TakeBoatFromId(value);
            }
           bunifuVScrollBar1.Visible = false;
            bunifuLabel1.Visible = false;
           
            bunifuLabel3.Visible = false;
            bunifuLabel4.Visible = false;
            bunifuPages2.SelectedTab = Yacht1Page;
            bunifuLabel9.Text = name;
            pictureBox6.Image = image;
            
            
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

        private string FormatNumberWithSeparators(long number)
        {
            
            return string.Format("{0:#,0}", number);
        }
        #region Animation
        private void bunifuLabel1_MouseEnter(object sender, EventArgs e)
        {
            bunifuLabel1.ForeColor = Color.White;
        }

        private void bunifuLabel1_MouseLeave(object sender, EventArgs e)
        {
            bunifuLabel1.ForeColor = Color.Gray;
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

        private void bunifuLabel4_MouseLeave(object sender, EventArgs e)
        {
            bunifuLabel4.ForeColor = Color.Gray;
        }

        

        #endregion

        private void bunifuLabelTitle_Click(object sender, EventArgs e)
        {
            bunifuPages1.SelectedTab = GlavPage;
        }

        private void bunifuVScrollBar1_Scroll(object sender, BunifuVScrollBar.ScrollEventArgs e)
        {
            
        }
       
        private void GlavAdmin_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "yacht_VerfDataSet20.Zayavka_RU". При необходимости она может быть перемещена или удалена.
            this.zayavka_RUTableAdapter.Fill(this.yacht_VerfDataSet20.Zayavka_RU);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "yacht_VerfDataSet16.Warehouses_Materials_RU". При необходимости она может быть перемещена или удалена.
            // this.warehouses_Materials_RUTableAdapter.Fill(this.yacht_VerfDataSet16.Warehouses_Materials_RU);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "yacht_VerfDataSet15.Materials". При необходимости она может быть перемещена или удалена.
            this.materialsTableAdapter3.Fill(this.yacht_VerfDataSet15.Materials);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "yacht_VerfDataSet14.Materials". При необходимости она может быть перемещена или удалена.
            this.materialsTableAdapter2.Fill(this.yacht_VerfDataSet14.Materials);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "yacht_VerfDataSet13.Materials". При необходимости она может быть перемещена или удалена.
            this.materialsTableAdapter1.Fill(this.yacht_VerfDataSet13.Materials);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "yacht_VerfDataSet12.Technology_RU". При необходимости она может быть перемещена или удалена.
            this.technology_RUTableAdapter.Fill(this.yacht_VerfDataSet12.Technology_RU);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "yacht_VerfDataSet11.Types_Boat". При необходимости она может быть перемещена или удалена.
            this.types_BoatTableAdapter.Fill(this.yacht_VerfDataSet11.Types_Boat);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "yacht_VerfDataSet10.Prihodnaya_Nakladnaya_RU". При необходимости она может быть перемещена или удалена.
            this.prihodnaya_Nakladnaya_RUTableAdapter1.Fill(this.yacht_VerfDataSet10.Prihodnaya_Nakladnaya_RU);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "yacht_VerfDataSet9.Prihodnaya_Nakladnaya_RU". При необходимости она может быть перемещена или удалена.
            //this.prihodnaya_Nakladnaya_RUTableAdapter.Fill(this.yacht_VerfDataSet9.Prihodnaya_Nakladnaya_RU);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "yacht_VerfDataSet8.Providers". При необходимости она может быть перемещена или удалена.
            this.providersTableAdapter.Fill(this.yacht_VerfDataSet8.Providers);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "yacht_VerfDataSet7.Materials". При необходимости она может быть перемещена или удалена.
            this.materialsTableAdapter.Fill(this.yacht_VerfDataSet7.Materials);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "yacht_VerfDataSet6.Warehouses". При необходимости она может быть перемещена или удалена.
            //this.warehousesTableAdapter.Fill(this.yacht_VerfDataSet6.Warehouses);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "yacht_VerfDataSet1.Clients_RU". При необходимости она может быть перемещена или удалена.
            this.clients_RUTableAdapter.Fill(this.yacht_VerfDataSet1.Clients_RU);
            flowLayoutPanel1.Padding = new Padding(10);
            bunifuVScrollBar1.ThumbLength = 59;
            this.MouseWheel += new MouseEventHandler(MainForm_MouseWheel);
            
        }

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
                                image = Image.FromStream(ms);
                            }
                        }
                        // Если нет данных, выполните необходимые действия или бросьте исключение.
                    }
                }

                connection.Close();
            }
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

        private void bunifuPanel2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuVScrollBar1_Scroll_1(object sender, BunifuVScrollBar.ScrollEventArgs e)

        {

            flowLayoutPanel1.VerticalScroll.Value = bunifuVScrollBar1.Value;
                Console.WriteLine($"Scroll_Maximum: {bunifuVScrollBar1.Maximum}\n" +
                $"Original_Maximum: {bunifuVScrollBar1.Maximum}\n" +
                $"Scroll_Value: {bunifuVScrollBar1.Value}\n" +
                $"Original_Value: {flowLayoutPanel1.VerticalScroll.Value}\n" +
                $"Thumn_Length: {bunifuVScrollBar1.ThumbLength}\n");
                  
        }
        

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            
        }

        private void CatalogPage_Scroll(object sender, ScrollEventArgs e)
        {
            bunifuVScrollBar1.Value = flowLayoutPanel1.VerticalScroll.Value ;
            Console.WriteLine($"Scroll_Maximum: {bunifuVScrollBar1.Maximum}\n" +
                $"Original_Maximum: {bunifuVScrollBar1.Maximum}\n" +
                $"Scroll_Value: {bunifuVScrollBar1.Value}\n" +
                $"Original_Value: {flowLayoutPanel1.VerticalScroll.Value}\n" +
                $"Thumn_Length: {bunifuVScrollBar1.ThumbLength}\n"); 
        }

        private void CatalogPage_MouseDown(object sender, MouseEventArgs e)
        {
            bunifuVScrollBar1.Value = flowLayoutPanel1.VerticalScroll.Value;
            Console.WriteLine($"Scroll_Maximum: {bunifuVScrollBar1.Maximum}\n" +
                $"Original_Maximum: {bunifuVScrollBar1.Maximum}\n" +
                $"Scroll_Value: {bunifuVScrollBar1.Value}\n" +
                $"Original_Value: {flowLayoutPanel1.VerticalScroll.Value}\n" +
                $"Thumn_Length: {bunifuVScrollBar1.ThumbLength}\n");
        }
        private void MainForm_MouseWheel(object sender, MouseEventArgs e)
        {
            // Обработка события прокрутки колеса мыши
           
        }

        private void CatalogPage_MouseEnter(object sender, EventArgs e)
        {
            
        }

        

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            bunifuVScrollBar1.Visible = true;
            bunifuLabel1.Visible = true;
            bunifuLabel3.Visible = true;
            bunifuLabel4.Visible = true;
            bunifuPages2.SelectedTab = CatalogPage;
        }

        private void bunifuLabel3_Click(object sender, EventArgs e)
        {
            
        }
        private void DeleteLabelClick(BunifuButton buttonDescription, BunifuButton buttonDelete, BunifuButton buttonEdit)
        {
           if (buttonDelete.Visible)
            {
                buttonDescription.Visible = true;
                buttonDelete.Visible = false;
                buttonEdit.Visible = false;
            }
           else
            {
                buttonDescription.Visible = false;
                buttonDelete.Visible = true;
                buttonEdit.Visible = false;
            }
            
        }
        private void EditLabelClick(BunifuButton buttonDescription, BunifuButton buttonDelete, BunifuButton buttonEdit)
        {
            if (buttonEdit.Visible)
            {
                buttonDescription.Visible = true;
                buttonDelete.Visible = false;
                buttonEdit.Visible = false;
            }
            else
            {
                buttonDescription.Visible = false;
                buttonDelete.Visible = false;
                buttonEdit.Visible = true;
            }
            

            
        }
        
        private void bunifuLabel4_Click(object sender, EventArgs e)
        {
           

        }
        


        private void bunifuButton2_Click(object sender, EventArgs e) 
        {    
            bunifuPages1.SelectedTab = ClientsPage;
        }

        private void bunifuLabel3_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }

        private void GlavAdmin_KeyUp(object sender, KeyEventArgs e)
        {
            {
                if (e.KeyCode == Keys.Escape) this.Close();
            }
        }
        private Point mouseOffset;
        private bool isMouseDown = false;
        private void GlavAdmin_MouseDown(object sender, MouseEventArgs e)
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

        private void GlavAdmin_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouseOffset.X, mouseOffset.Y);
                Location = mousePos;
            }
        }
        private void Vhod_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }

        private void GlavAdmin_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = false;
            }
        }

        private void bunifuDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuButtonBuy_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            SettingForm settingForm = new SettingForm();
            settingForm.ShowDialog();
        }
        public string test_value;


        void Get_list_DropDown3()
        {
            try
            {
                SqlConnection con = new SqlConnection(DataBaseWorker.ConnectString);
                SqlDataAdapter da = new SqlDataAdapter("select * from Providers", con);
                DataSet ds = new DataSet();
                con.Open();
                {
                    SqlCommand command = new SqlCommand("SELECT * FROM Providers", con);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable table = new DataTable();
                    adapter.Fill(ds, "Providers");
                    bunifuDropdown3.DataSource = null;
                    bunifuDropdown3.DataSource = ds.Tables["Providers"];
                    bunifuDropdown3.DisplayMember = "Name_Provider";
                    bunifuDropdown3.ValueMember = "Id_Provider";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        void Get_list_DropDown2()
        {
            try
            {
                SqlConnection con = new SqlConnection(DataBaseWorker.ConnectString);
                SqlDataAdapter da = new SqlDataAdapter("select * from Warehouses", con);
                DataSet ds = new DataSet();
                con.Open();
                {
                    SqlCommand command = new SqlCommand("SELECT * FROM Warehouses", con);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable table = new DataTable();
                    adapter.Fill(ds, "Warehouses");
                    //bunifuDropdown2.DataSource = null;
                    bunifuDropdown2.DataSource = ds.Tables["Warehouses"];
                    bunifuDropdown2.DisplayMember = "Name_Warehouse";
                    bunifuDropdown2.ValueMember = "Id_Warehouse";
                    bunifuDropdown2.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        void Get_list_DropDown1()
        {
            try
            {
                SqlConnection con = new SqlConnection(DataBaseWorker.ConnectString);
                SqlDataAdapter da = new SqlDataAdapter("select * from Warehouses", con);
                DataSet ds = new DataSet();
                con.Open();
                {
                    SqlCommand command = new SqlCommand("SELECT * FROM Warehouses", con);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable table = new DataTable();
                    adapter.Fill(ds, "Warehouses");
                    bunifuDropdown1.DataSource = null;
                    bunifuDropdown1.DataSource = ds.Tables["Warehouses"];
                    bunifuDropdown1.DisplayMember = "Name_Warehouse";
                    bunifuDropdown1.ValueMember = "Id_Warehouse";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            Get_list_DropDown1();
            Get_list_DropDown2();
            Get_list_DropDown3();

            Console.WriteLine(bunifuDropdown2.SelectedIndex);
            using (SqlConnection connection = new SqlConnection(DataBaseWorker.ConnectString))
            {
                connection.Open();

                test_value = bunifuDropdown2.SelectedText;

                // Используйте параметры для предотвращения SQL-инъекций
                string query = "SELECT * FROM Warehouses_Materials_RU Where [Название склада] = N'Главный'";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    // Добавляем параметр
                    cmd.Parameters.AddWithValue("@TestValue", test_value);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);


                    bunifuDataGridView2.DataSource = dataTable;
                }
            }
         
            bunifuPages1.SelectedTab = WarehousePage;
        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            bunifuPages1.SelectedTab = TechnologyPage;
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            bunifuPages1.SelectedTab = GlavPage;
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            bunifuPages1.SelectedTab = WarehousePage;
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            bunifuPages1.SelectedTab = GlavPage;
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            bunifuPages1.SelectedTab = ReportsPage;
        }

        private void GlavPage_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton8_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void bunifuButtonLogin_Click(object sender, EventArgs e)
        {

            bunifuPages1.SelectedTab = OrderOfProviderPage;
        }
        public static int Id_Material_Nakladnaya;
        public static int kol_vo_material_Nakladnaya;
        private void bunifuButton7_Click(object sender, EventArgs e)
        {            
            AddValues addValues = new AddValues("OrderOfProvider");

            // Отобразить диалоговое окно
            DialogResult result = addValues.ShowDialog();

            // Проверить результат диалога
            if (result == DialogResult.OK)
            {
                using (SqlConnection connection = new SqlConnection(DataBaseWorker.ConnectString))
                {
                    connection.Open();
                    Console.WriteLine(Id_Material_Nakladnaya);
                    using (SqlCommand cmd = new SqlCommand("Insert into Prihodnaya_Nakladnaya (Id_Provider, Id_warehouse, Id_Material, Count_Material) VALUES (@Id_Provider, @Id_warehouse, @Id_Material, @Count_Material)", connection))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Id_Provider", bunifuDropdown3.SelectedValue);
                        cmd.Parameters.AddWithValue("@Id_warehouse", bunifuDropdown1.SelectedValue);
                        cmd.Parameters.AddWithValue("@Id_Material", Id_Material_Nakladnaya);
                        cmd.Parameters.AddWithValue("@Count_Material", kol_vo_material_Nakladnaya);
                        cmd.ExecuteNonQuery();
                    }
                    
               
                    SqlDataAdapter da = new SqlDataAdapter("select * from Prihodnaya_Nakladnaya_RU", connection);
                    DataSet ds = new DataSet();
                    
                        SqlCommand command = new SqlCommand("SELECT * FROM Prihodnaya_Nakladnaya_RU", connection);
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable table = new DataTable();
                        adapter.Fill(ds, "Prihodnaya_Nakladnaya_RU");
                        bunifuDataGridView3.DataSource = ds.Tables["Prihodnaya_Nakladnaya_RU"];
                    
                    connection.Close();
                }
            }
            else
            {
                MessageBox.Show("Что-то пошло не так?");
            }

           
        }

        private void OrderOfProviderPage_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCheckBox1_CheckedChanged(object sender, BunifuCheckBox.CheckedChangedEventArgs e)
        {
            if(!bunifuCheckBox1.Checked) {
                bunifuLabel20.Visible = false;
                bunifuDropdown8.Visible = false;
                bunifuLabel23.Visible = false;
                bunifuTextBox3.Visible = false;
            }
            else
            {
                bunifuLabel20.Visible = true;
                bunifuDropdown8.Visible = true;
                bunifuLabel23.Visible = true;
                bunifuTextBox3.Visible = true;
            }
        }
        public string insertQuery;
        private void bunifuButton9_Click(object sender, EventArgs e)
        {
            // Получаем значения из компонентов формы
            

            using (SqlConnection connection = new SqlConnection(DataBaseWorker.ConnectString))
            {
                connection.Open();

                string insertQuery = "INSERT INTO Technology (Id_Type_Boat, Id_Deck_Material, Id_Hull_Material, Id_Sail_Material, Count_Material_Hull, Count_Material_Deck, Count_Material_Sail, Height, Width, Length, Mast, Count_Engine, Power_Engine, Price_Boat) " +
                    "VALUES (@selectedTypeId, @selectedDeckMaterialId, @selectedHullMaterialId, @selectedSailMaterialId, @countMaterialHull, @countMaterialDeck, @countMaterialSail, @Height, @Width, @Length, @Mast, @countEngine, @powerEngine, @priceBoat)";

                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    int selectedTypeId = Convert.ToInt32(bunifuDropdown4.SelectedValue);
                    int selectedDeckMaterialId = Convert.ToInt32(bunifuDropdown5.SelectedValue);
                    int selectedHullMaterialId = Convert.ToInt32(bunifuDropdown6.SelectedValue);
                    int selectedSailMaterialId = Convert.ToInt32(bunifuDropdown8.SelectedValue);
                    int countMaterialHull = Convert.ToInt32(bunifuTextBox6.Text);
                    int countMaterialDeck = Convert.ToInt32(bunifuTextBox2.Text);
                    int countMaterialSail = Convert.ToInt32(bunifuTextBox3.Text);
                    int Height = Convert.ToInt32(bunifuTextBox7.Text);
                    int Width = Convert.ToInt32(bunifuTextBox5.Text);
                    int Length = Convert.ToInt32(bunifuTextBox4.Text);
                    int countEngine = Convert.ToInt32(bunifuTextBox8.Text);
                    int priceBoat = Convert.ToInt32(bunifuTextBox9.Text);
                    int powerEngine = Convert.ToInt32(bunifuTextBox10.Text);

                    command.Parameters.AddWithValue("@selectedTypeId", selectedTypeId);
                    command.Parameters.AddWithValue("@selectedDeckMaterialId", selectedDeckMaterialId);
                    command.Parameters.AddWithValue("@selectedHullMaterialId", selectedHullMaterialId);
                    command.Parameters.AddWithValue("@selectedSailMaterialId", bunifuCheckBox1.Checked ? selectedSailMaterialId : (object)DBNull.Value);
                    command.Parameters.AddWithValue("@countMaterialHull", countMaterialHull);
                    command.Parameters.AddWithValue("@countMaterialDeck", countMaterialDeck);
                    command.Parameters.AddWithValue("@countMaterialSail", bunifuCheckBox1.Checked ? countMaterialSail : (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Height", Height);
                    command.Parameters.AddWithValue("@Width", Width);
                    command.Parameters.AddWithValue("@Length", Length);
                    command.Parameters.AddWithValue("@Mast", bunifuCheckBox1.Checked);
                    command.Parameters.AddWithValue("@countEngine", countEngine);
                    command.Parameters.AddWithValue("@powerEngine", powerEngine);
                    command.Parameters.AddWithValue("@priceBoat", priceBoat);

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
            
           
 
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
        public bool Mast;
        public int priceBoat;
        private void bunifuDropdown4_SelectedIndexChanged(object sender, EventArgs e)
        {
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


                            bunifuLabel37.Text = $"Тип судна: {nameOfType}";
                            bunifuLabel29.Text = $"Размеры: {Height}м X {Width}м X {Length}м";
                            bunifuLabel35.Text = $"Материалы корпуса: {MaterialHull}, {CountMaterialHull}";
                            bunifuLabel36.Text = $"Материалы палубы: {MaterialDeck}, {CountMaterialDeck}";
                            bunifuLabel28.Text = $"Кол-во двигателей: {CountEngines} шт.";
                            bunifuLabel39.Text = $"Мощность двигателей: менее {powerEngine} л.с.";
                            bunifuLabel31.Text = $"Стоимость судна: {priceBoat} y.e.";
                            if (Mast)
                            {
                                bunifuLabel33.Text = $"Наличие мачты: имеется ";
                                bunifuLabel34.Text = $"Материал паруса: {MaterialSail}, {CountMaterialSail} ";
                            }
                            else
                            {
                                bunifuLabel33.Text = $"Наличие мачты: отсутствует";
                                bunifuLabel34.Text = $"Материал паруса: -";
                            }
                            
                            
                            

                        }
                        else
                        {
                            bunifuLabel37.Text = "Тип судна: ";
                            bunifuLabel29.Text = "Размеры: ";
                            bunifuLabel35.Text = "Материалы корпуса: ";
                            bunifuLabel36.Text = "Материалы палубы: ";
                            bunifuLabel28.Text = "Кол-во двигателей: ";
                            bunifuLabel39.Text = "Мощность двигателей: ";
                            bunifuLabel31.Text = "Стоимость судна: ";
                            bunifuLabel33.Text = "Наличие мачты: ";
                            bunifuLabel34.Text = "Материал паруса: ";
                            MessageBox.Show("У данного типа судна нет технологии.", "Технология", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }


                connection.Close();
            }
        }

        public string name_warehouse;
        public int idWarehouse;
        private SqlDataAdapter zayavkaDataAdapter;
        private DataTable zayavkaDataTable;
        private SqlDataAdapter orderDataAdapter;
        private DataTable orderDataTable;

        private void bunifuDropdown2_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(DataBaseWorker.ConnectString))
            { 
                try {
                    connection.Open();
                    string query1 = "SELECT * FROM Warehouses WHERE Id_Warehouse = @IdWarehouse";

                    using (SqlCommand cmd = new SqlCommand(query1, connection))
                    {
                        idWarehouse = Convert.ToInt32(bunifuDropdown2.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@IdWarehouse", idWarehouse);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                name_warehouse = reader["Name_Warehouse"].ToString();
                            }
                        }
                    }
                    test_value = bunifuDropdown2.SelectedItem.ToString();

                    string query = "SELECT * FROM Warehouses_Materials_RU WHERE [Название склада] = @TestValue";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@TestValue", name_warehouse);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        bunifuDataGridView2.DataSource = dataTable;
                    }
                }
                catch
                {

                }
            }
        }
        void Get_list()
        {
            try
            {
                SqlConnection con = new SqlConnection(DataBaseWorker.ConnectString);
                SqlDataAdapter da = new SqlDataAdapter("select * from Prihodnaya_Nakladnaya_RU", con);
                DataSet ds = new DataSet();
                con.Open();
                {
                    SqlCommand command = new SqlCommand("SELECT * FROM Prihodnaya_Nakladnaya_RU", con);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable table = new DataTable();
                    adapter.Fill(ds, "Prihodnaya_Nakladnaya_RU");
                    bunifuDataGridView3.DataSource = ds.Tables["Prihodnaya_Nakladnaya_RU"];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        void Get_list2()
        {
            try
            {
                SqlConnection connection = new SqlConnection(DataBaseWorker.ConnectString);
                connection.Open();

                test_value = bunifuDropdown2.SelectedText;

                // Используйте параметры для предотвращения SQL-инъекций
                string query = "SELECT * FROM Warehouses_Materials_RU Where [Название склада] = N'Главный'";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    // Добавляем параметр
                    cmd.Parameters.AddWithValue("@TestValue", test_value);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);


                    bunifuDataGridView2.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void bunifuButton10_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(DataBaseWorker.ConnectString))
            {
                connection.Open();
                foreach (DataGridViewRow row in bunifuDataGridView3.Rows)
                {
                    if (row.Cells["названиеМатериалаDataGridViewTextBoxColumn"].Value != null)
                    {
                        // Используйте точные имена столбцов, как они заданы в вашем DataGridView
                        string nameMaterial = row.Cells["названиеМатериалаDataGridViewTextBoxColumn"].Value.ToString();
                        int countMaterial = Convert.ToInt32(row.Cells["колвоМатериалаDataGridViewTextBoxColumn"].Value);

                        int idWarehouse = Convert.ToInt32(bunifuDropdown1.SelectedValue.ToString());
                        Console.WriteLine(idWarehouse);
                        // Получаем Id_Material по имени материала
                        string getIdMaterialQuery = $"SELECT Id_Material FROM Materials WHERE Name_Material = N'{nameMaterial}'";
                        using (SqlCommand getIdMaterialCommand = new SqlCommand(getIdMaterialQuery, connection))
                        {
                            

                            int idMaterial = Convert.ToInt32(getIdMaterialCommand.ExecuteScalar());
                            Console.WriteLine(idMaterial);
                            // Проверяем наличие материала в Warehouse_Materials
                            string checkMaterialQuery = $"SELECT COUNT(*) FROM Warehouses_Materials WHERE Id_Material = {idMaterial} AND Id_Warehouse = {idWarehouse}";
                            using (SqlCommand checkMaterialCommand = new SqlCommand(checkMaterialQuery, connection))
                            {
                                int materialCount = Convert.ToInt32(checkMaterialCommand.ExecuteScalar());
                                Console.WriteLine(materialCount);
                                if (materialCount > 0 )
                                {
                                    // Если материал уже существует, обновляем значение count_Material
                                    string updateWarehouseMaterialsQuery = "UPDATE Warehouses_Materials SET count_Material = count_Material + @CountMaterial WHERE Id_Material = @IdMaterial AND Id_Warehouse = @IdWarehouse";

                                    using (SqlCommand updateCommand = new SqlCommand(updateWarehouseMaterialsQuery, connection))
                                    {
                                        updateCommand.Parameters.AddWithValue("@CountMaterial", countMaterial);
                                        updateCommand.Parameters.AddWithValue("@IdMaterial", idMaterial);
                                        updateCommand.Parameters.AddWithValue("@IdWarehouse", idWarehouse);

                                        updateCommand.ExecuteNonQuery();
                                    }

                                    Console.WriteLine("Я обновляю");

                                }
                                else
                                {
                                    // Если материал отсутствует, добавляем новую запись
                                    string insertWarehouseMaterialsQuery = $"INSERT INTO Warehouses_Materials (Id_Material, count_Material, Id_Warehouse) VALUES ({idMaterial}, {countMaterial}, {idWarehouse})";
                                    using (SqlCommand insertCommand = new SqlCommand(insertWarehouseMaterialsQuery, connection))
                                    {
                                        insertCommand.ExecuteNonQuery();
                                    }
                                    Console.WriteLine("Я добавил");
                                }
                            }
                        }
                    }
                }


                string query = "DELETE FROM Prihodnaya_Nakladnaya";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();

                
                connection.Close();
                Get_list();
                Get_list2();
            }
        }

        private void WarehousePage_Click(object sender, EventArgs e)
        {

        }

        private void bunifuDropdown1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void bunifuButton11_Click(object sender, EventArgs e)
        {
            if (isZayavkaView)
            {
                bunifuDataGridView4.DataSource = orderDataTable;
                bunifuButton11.Text = "Верфь";
            }
            else
            {
                
                bunifuDataGridView4.DataSource = zayavkaDataTable;
                bunifuButton11.Text = "Заявка";
            }

            // Инвертируем флаг для следующего нажатия
            isZayavkaView = !isZayavkaView;
        }
    }
}
