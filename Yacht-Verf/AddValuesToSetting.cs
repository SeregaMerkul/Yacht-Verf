using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1;
using static System.Net.Mime.MediaTypeNames;

namespace Yacht_Verf
{
    public partial class AddValuesToSetting : Form
    {
        public string value_type;
        public int id_type;   
        public AddValuesToSetting(string value)
        {
            InitializeComponent();
            value_type = value;
        }
        private void FillDataGrid(string Setting)
        {
            using (SqlConnection connection = new SqlConnection(DataBaseWorker.ConnectString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand($"SELECT * FROM {value_type}_RU", connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                    // Создаем таблицу данных
                    DataTable dataTable = new DataTable();

                    // Заполняем таблицу данными из базы данных
                    adapter.Fill(dataTable);

                    // Закрываем соединение
                    connection.Close();

                    // Задаем источник данных для DataGridView
                    bunifuDataGridView1.DataSource = dataTable;


                }

                connection.Close();
            }
        }
        private void AddValuesToSetting_Load(object sender, EventArgs e)
        {

            switch (value_type)
            {
                case "Units":
                    FillDataGrid(value_type);
                    break;
                case "Materials":
                    FillDataGrid(value_type);
                    break;
                case "Price_Of_Unit":
                    FillDataGrid(value_type);
                    break;
                case "TypeBoat":
                    FillDataGrid(value_type);
                    break;
                case "Providers":
                    FillDataGrid(value_type);
                    break;
                case "Warehouses":
                    FillDataGrid(value_type);
                    break;
                case "Engine":
                    FillDataGrid(value_type);
                    break;
                default:
                    MessageBox.Show("Ты как это сделал?!");
                    break;
            }
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            AddValues addValues = new AddValues(value_type);
            addValues.ShowDialog();
            FillDataGrid(value_type);
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            switch (value_type)
            {
                case "Units":
                    using (SqlConnection connection = new SqlConnection(DataBaseWorker.ConnectString))
                    {
                        connection.Open();

                        // Создайте SQL-запрос для удаления записи по ID
                        string query = "DELETE FROM Units WHERE Id_Unit = @Id_Unit";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            // Передайте значение параметра ID
                            command.Parameters.AddWithValue("@Id_Unit", Convert.ToInt32(bunifuDataGridView1.Rows[bunifuDataGridView1.CurrentRow.Index].Cells["Номер единицы измерения"].Value));

                            command.ExecuteNonQuery();
                        }
                    }

                    FillDataGrid(value_type);
                    break;
                case "Materials":
                    using (SqlConnection connection = new SqlConnection(DataBaseWorker.ConnectString))
                    {
                        connection.Open();

                        // Создайте SQL-запрос для удаления записи по ID
                        string query = "DELETE FROM Materials WHERE Id_Material = @Id_Material";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            // Передайте значение параметра ID
                            command.Parameters.AddWithValue("@Id_Material", Convert.ToInt32(bunifuDataGridView1.Rows[bunifuDataGridView1.CurrentRow.Index].Cells["Номер материала"].Value));

                            command.ExecuteNonQuery();
                        }
                    }
                    FillDataGrid(value_type);
                    break;
                case "Price_Of_Unit":
                    using (SqlConnection connection = new SqlConnection(DataBaseWorker.ConnectString))
                    {
                        connection.Open();

                        // Создайте SQL-запрос для удаления записи по ID
                        string query = "DELETE FROM Price_Of_Unit WHERE Id_Price = @Id_Price";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            // Передайте значение параметра ID
                            command.Parameters.AddWithValue("@Id_Price", Convert.ToInt32(bunifuDataGridView1.Rows[bunifuDataGridView1.CurrentRow.Index].Cells["Номер цены"].Value));

                            command.ExecuteNonQuery();
                        }
                    }
                    FillDataGrid(value_type);
                    break;
                case "TypeBoat":
                    using (SqlConnection connection = new SqlConnection(DataBaseWorker.ConnectString))
                    {
                        connection.Open();

                        // Создайте SQL-запрос для удаления записи по ID
                        string query = "DELETE FROM Types_Boat WHERE Id_Type_Boat = @Id_Type_Boat";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            // Передайте значение параметра ID
                            command.Parameters.AddWithValue("@Id_Type_Boat", Convert.ToInt32(bunifuDataGridView1.Rows[bunifuDataGridView1.CurrentRow.Index].Cells["Номер типа судна"].Value));

                            command.ExecuteNonQuery();
                        }
                    }
                    FillDataGrid(value_type);
                    break;
                case "Providers":
                    using (SqlConnection connection = new SqlConnection(DataBaseWorker.ConnectString))
                    {
                        connection.Open();

                        // Создайте SQL-запрос для удаления записи по ID
                        string query = "DELETE FROM Providers WHERE Id_Provider = @Id_Provider";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            // Передайте значение параметра ID
                            command.Parameters.AddWithValue("@Id_Provider", Convert.ToInt32(bunifuDataGridView1.Rows[bunifuDataGridView1.CurrentRow.Index].Cells["Номер поставщика"].Value));

                            command.ExecuteNonQuery();
                        }
                    }
                    FillDataGrid(value_type);
                    break;
                case "Warehouses":
                    using (SqlConnection connection = new SqlConnection(DataBaseWorker.ConnectString))
                    {
                        connection.Open();

                        // Создайте SQL-запрос для удаления записи по ID
                        string query = "DELETE FROM Warehouses WHERE Id_Warehouse = @Id_Warehouse";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            // Передайте значение параметра ID
                            command.Parameters.AddWithValue("@Id_Warehouse", Convert.ToInt32(bunifuDataGridView1.Rows[bunifuDataGridView1.CurrentRow.Index].Cells["Номер склада"].Value));

                            command.ExecuteNonQuery();
                        }
                    }
                    FillDataGrid(value_type);
                    break;
                case "Engines":
                    using (SqlConnection connection = new SqlConnection(DataBaseWorker.ConnectString))
                    {
                        connection.Open();

                        // Создайте SQL-запрос для удаления записи по ID
                        string query = "DELETE FROM Engines WHERE Id_Engine = @Id_Engine";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            // Передайте значение параметра ID
                            command.Parameters.AddWithValue("@Id_Engine", Convert.ToInt32(bunifuDataGridView1.Rows[bunifuDataGridView1.CurrentRow.Index].Cells["Номер двигателя"].Value));

                            command.ExecuteNonQuery();
                        }
                    }
                    FillDataGrid(value_type);
                    break;
                default:
                    MessageBox.Show("Ты как это сделал?!");
                    break;
            }
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            EditValues editValues = new EditValues(value_type);
            editValues.ShowDialog();
            FillDataGrid(value_type);
        }
    }
}
