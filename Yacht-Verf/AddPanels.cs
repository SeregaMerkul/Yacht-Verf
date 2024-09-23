using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Yacht_Verf
{
    public partial class AddPanels : Form
    {
        public AddPanels()
        {
            InitializeComponent();
        }
        public class CustomPictureBox : PictureBox
        {
            private Color borderColor = Color.Black;
            private int borderWidth = 2;

            public Color BorderColor
            {
                get { return borderColor; }
                set { borderColor = value; Invalidate(); }
            }

            public int BorderWidth
            {
                get { return borderWidth; }
                set { borderWidth = value; Invalidate(); }
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);

                ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
                                        borderColor, borderWidth, ButtonBorderStyle.Solid,
                                        borderColor, borderWidth, ButtonBorderStyle.Solid,
                                        borderColor, borderWidth, ButtonBorderStyle.Solid,
                                        borderColor, borderWidth, ButtonBorderStyle.Solid);
            }
        }
        private void AddPanels_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "yacht_VerfDataSet.Types_Boat". При необходимости она может быть перемещена или удалена.
            //this.types_BoatTableAdapter.Fill(this.yacht_VerfDataSet.Types_Boat);

            bunifuTextBox7.Visible = false;
            Get_list_DropDown();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Создаем экземпляр OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Устанавливаем фильтры для файлов (например, изображений)
            openFileDialog.Filter = "Изображения|*.png;*.jpg;*.jpeg;*.gif;*.bmp|Все файлы|*.*";

            // Показываем диалоговое окно и проверяем, был ли выбран файл
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Загружаем изображение из выбранного файла
                    pictureBox1.Image = Image.FromFile(openFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки изображения: {ex.Message}");
                }
            }

            Image image = pictureBox1.Image;

            // Проверка, что изображение не пустое
            if (image != null)
            {
                // Преобразование изображения в массив байтов
                imageData = ImageToByteArray(image);

            }
            else
            {
                MessageBox.Show("Изображение отсутствует.");
            }

        }

        public byte[] imageData;
    private byte[] ImageToByteArray(Image image)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }
    }
        private void bunifuButtonBuy_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(bunifuTextBox1.Text) || string.IsNullOrWhiteSpace(bunifuTextBox2.Text) || string.IsNullOrWhiteSpace(bunifuTextBox3.Text) || string.IsNullOrWhiteSpace(bunifuTextBox4.Text) || string.IsNullOrWhiteSpace(bunifuTextBox5.Text) || string.IsNullOrWhiteSpace(bunifuTextBox6.Text))
            {
                using (SqlConnection connection = new SqlConnection(DataBaseWorker.ConnectString))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("AddBoat", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Name_boat", bunifuTextBox1.Text.ToString());
                        cmd.Parameters.AddWithValue("@Id_Type_boat", bunifuDropdown1.SelectedValue);
                        cmd.Parameters.AddWithValue("@Mast", bunifuCheckBox1.Checked);
                        cmd.Parameters.AddWithValue("@Price_without_Accessory", Convert.ToInt32(bunifuTextBox6.Text.ToString()));
                        cmd.Parameters.AddWithValue("@Number_of_Seats", Convert.ToInt32(bunifuTextBox2.Text.ToString()));
                        cmd.Parameters.AddWithValue("@Discription", bunifuTextBox5.Text.ToString());
                        cmd.Parameters.AddWithValue("@Power", Convert.ToInt32(bunifuTextBox3.Text.ToString()));
                        cmd.Parameters.AddWithValue("@Tank_volume", Convert.ToInt32(bunifuTextBox4.Text.ToString()));
                        cmd.Parameters.AddWithValue("@Photo", imageData);
                        cmd.ExecuteNonQuery();
                    }
                    connection.Close();
                }
                this.DialogResult = DialogResult.OK;

                // Закройте форму
                this.Close();
            }
            else
            {
                MessageBox.Show("Заполните все поля!");
            }
       
        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Если символ не цифра, и не управляющий, то предотвращаем его ввод
                e.Handled = true;
            }
        }
        void Get_list_DropDown()
        {
            try
            {
                SqlConnection con = new SqlConnection(DataBaseWorker.ConnectString);
                SqlDataAdapter da = new SqlDataAdapter("select * from Types_Boat", con);
                DataSet ds = new DataSet();
                con.Open();
                {
                    SqlCommand command = new SqlCommand("SELECT * FROM Types_Boat", con);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable table = new DataTable();
                    adapter.Fill(ds, "Types_Boat");
                    bunifuDropdown1.DataSource = null;
                    bunifuDropdown1.DataSource = ds.Tables["Types_Boat"];
                    bunifuDropdown1.DisplayMember = "Name_Type_Boat";
                    bunifuDropdown1.ValueMember = "Id_Type_Boat";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
        private void bunifuPictureBox1_Click(object sender, EventArgs e)
        {
            if (!bunifuTextBox7.Visible)
            {
                bunifuTextBox7.Visible = true;
            }
            else if (bunifuTextBox7.Visible)
            {
                if (string.IsNullOrWhiteSpace(bunifuTextBox7.Text))
                {
                    MessageBox.Show("Вы не ввели значение!");
                    bunifuTextBox7.Visible = false;
                }
                else { 
                using (SqlConnection connection = new SqlConnection(DataBaseWorker.ConnectString))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("AddTypeBoat", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Name_type_boat", bunifuTextBox7.Text.ToString());
                        cmd.ExecuteNonQuery();
                    }
                        connection.Close();
                }
                  Get_list_DropDown();
                bunifuTextBox7.Visible = false;
            }
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private Point mouseOffset;
        private bool isMouseDown = false;
        private void AddPanels_MouseDown(object sender, MouseEventArgs e)
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

        private void AddPanels_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouseOffset.X, mouseOffset.Y);
                Location = mousePos;
            }
        }

        private void AddPanels_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = false;
            }
        }
    }
}
