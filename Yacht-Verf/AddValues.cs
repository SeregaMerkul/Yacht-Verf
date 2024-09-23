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
using WindowsFormsApp1;

namespace Yacht_Verf
{
    public partial class AddValues : Form
    {
        public string value_type;
        public int id_type;

        public AddValues(string value)
        {         
            InitializeComponent();
            value_type = value;
        }

        private void bunifuDropdown1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AddValues_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "yacht_VerfDataSet17.Materials". При необходимости она может быть перемещена или удалена.
            this.materialsTableAdapter1.Fill(this.yacht_VerfDataSet17.Materials);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "yacht_VerfDataSet5.Materials". При необходимости она может быть перемещена или удалена.
            //this.materialsTableAdapter.Fill(this.yacht_VerfDataSet5.Materials);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "yacht_VerfDataSet4.Units". При необходимости она может быть перемещена или удалена.
            this.unitsTableAdapter.Fill(this.yacht_VerfDataSet4.Units);
            Get_list_DropDown1();
            Get_list_DropDown2();
            switch (value_type)
            {
                case "Units":
                    bunifuPages1.SelectedTab = UnitsPage;
                    break;
                case "Materials":
                    bunifuPages1.SelectedTab = MaterialPage;
                    break;
                case "Price_Of_Unit":
                    bunifuPages1.SelectedTab = PriceOfUnitPage;
                    break;
                case "TypeBoat":
                    bunifuPages1.SelectedTab = TypeBoatPage;
                    break;
                case "Providers":
                    bunifuPages1.SelectedTab = ProvidersPage;
                    break;
                case "Warehouses":
                    bunifuPages1.SelectedTab = WarehousePage;
                    break;
                case "OrderOfProvider":
                    bunifuPages1.SelectedTab = OrderOfProviderPage;
                    break;
                case "Engine":
                    bunifuPages1.SelectedTab = EnginePage;
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
        void Get_list_DropDown2()
        {
            try
            {
                SqlConnection con = new SqlConnection(DataBaseWorker.ConnectString);
                SqlDataAdapter da = new SqlDataAdapter("select * from Materials", con);
                DataSet ds = new DataSet();
                con.Open();
                {
                    SqlCommand command = new SqlCommand("SELECT * FROM Materials", con);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable table = new DataTable();
                    adapter.Fill(ds, "Materials");
                    bunifuDropdown2.DataSource = null;
                    bunifuDropdown2.DataSource = ds.Tables["Materials"];
                    bunifuDropdown2.DisplayMember = "Name_Material";
                    bunifuDropdown2.ValueMember = "Id_Material";
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
                SqlDataAdapter da = new SqlDataAdapter("select * from Units", con);
                DataSet ds = new DataSet();
                con.Open();
                {
                    SqlCommand command = new SqlCommand("SELECT * FROM Units", con);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable table = new DataTable();
                    adapter.Fill(ds, "Units");
                    bunifuDropdown1.DataSource = null;
                    bunifuDropdown1.DataSource = ds.Tables["Units"];
                    bunifuDropdown1.DisplayMember = "Name_Unit";
                    bunifuDropdown1.ValueMember = "Id_Unit";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
        void Get_list_DropDown3()
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
                    bunifuDropdown0.DataSource = null;
                    bunifuDropdown0.DataSource = ds.Tables["Types_Boat"];
                    bunifuDropdown0.DisplayMember = "Name_Type_Boat";
                    bunifuDropdown0.ValueMember = "Id_Type_Boat";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
        private void Get_List()
        {

        }
        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            switch (value_type)
            {
                case "Units":
                    bunifuPages1.SelectedTab = UnitsPage;
                    using (SqlConnection connection = new SqlConnection(DataBaseWorker.ConnectString))
                    {
                        connection.Open();

                        using (SqlCommand cmd = new SqlCommand("Insert into Units (Name_Unit) VALUES (@Name_Unit)", connection))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@Name_Unit", bunifuTextBox2.Text.ToString());                          
                            cmd.ExecuteNonQuery();
                        }
                        connection.Close();
                    }
                    this.Hide();
                    break;
                case "Materials":
                    bunifuPages1.SelectedTab = MaterialPage;
                    using (SqlConnection connection = new SqlConnection(DataBaseWorker.ConnectString))
                    {
                        connection.Open();

                        using (SqlCommand cmd = new SqlCommand("Insert into Materials (Name_Material, Id_Unit) VALUES (@Name_Material, @Id_Unit)", connection))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@Name_Material", bunifuTextBoxMaterial.Text.ToString());
                            cmd.Parameters.AddWithValue("@Id_Unit", bunifuDropdown1.SelectedValue);
                            cmd.ExecuteNonQuery();
                        }
                        connection.Close();
                    }
                    this.Hide();
                    break;
                case "Price_Of_Unit":
                    bunifuPages1.SelectedTab = PriceOfUnitPage;
                    using (SqlConnection connection = new SqlConnection(DataBaseWorker.ConnectString))
                    {
                        connection.Open();

                        using (SqlCommand cmd = new SqlCommand("Insert into Price_Of_Unit (Id_Material, Id_Unit, Price) VALUES (@Id_Material, (SELECT Id_Unit FROM Materials WHERE Id_Material = @Id_Material), @Price)", connection))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@Id_Material", bunifuDropdown2.SelectedValue);
                            cmd.Parameters.AddWithValue("@Price", bunifuTextBox1.Text.ToString());
                            cmd.ExecuteNonQuery();
                        }
                        connection.Close();
                    }
                    this.Hide();
                    break;
                case "TypeBoat":
                    bunifuPages1.SelectedTab = TypeBoatPage;
                    using (SqlConnection connection = new SqlConnection(DataBaseWorker.ConnectString))
                    {
                        connection.Open();

                        using (SqlCommand cmd = new SqlCommand("Insert into Types_Boat (Name_Type_Boat) VALUES (@Name_Type_Boat)", connection))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@Name_Type_Boat", bunifuTextBox4.Text.ToString());
                            cmd.ExecuteNonQuery();
                        }
                        connection.Close();
                    }
                    this.Hide();
                    break;
                case "Providers":
                    bunifuPages1.SelectedTab = ProvidersPage;
                    using (SqlConnection connection = new SqlConnection(DataBaseWorker.ConnectString))
                    {
                        connection.Open();

                        using (SqlCommand cmd = new SqlCommand("Insert into Providers (Name_Provider) VALUES (@Name_Provider)", connection))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@Name_Provider", bunifuTextBox3.Text.ToString());
                            cmd.ExecuteNonQuery();
                        }
                        connection.Close();
                    }
                    this.Hide();
                    break;
                case "Warehouses":
                    bunifuPages1.SelectedTab = WarehousePage;
                    using (SqlConnection connection = new SqlConnection(DataBaseWorker.ConnectString))
                    {
                        connection.Open();

                        using (SqlCommand cmd = new SqlCommand("Insert into Warehouses (Name_Warehouse) VALUES (@Name_Warehouse)", connection))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@Name_Warehouse", bunifuTextBox5.Text.ToString());
                            cmd.ExecuteNonQuery();
                        }
                        connection.Close();
                    }
                    this.Hide();
                    break;
                case "OrderOfProvider":
                    bunifuPages1.SelectedTab = OrderOfProviderPage;

                    GlavAdmin.Id_Material_Nakladnaya = Convert.ToInt32(bunifuDropdown0.SelectedValue.ToString());
                    GlavAdmin.kol_vo_material_Nakladnaya = Convert.ToInt32(bunifuTextBox6.Text.ToString());

                    DialogResult = DialogResult.OK;
                    Close();

                    this.Hide();                 
                    break;
                case "Engine":
                    bunifuPages1.SelectedTab = EnginePage;
                    using (SqlConnection connection = new SqlConnection(DataBaseWorker.ConnectString))
                    {
                        connection.Open();

                        using (SqlCommand cmd = new SqlCommand("Insert into Engines (Name_Engine, Power_Engine, Price_Engine) VALUES (@Name_engine, @Power_Engine, @Price_Engine)", connection))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@Name_Engine", bunifuTextBox8.Text.ToString());
                            cmd.Parameters.AddWithValue("@Power_Engine", Convert.ToInt32(bunifuTextBox7.Text.ToString()));
                            cmd.Parameters.AddWithValue("@Price_Engine", Convert.ToInt32(bunifuTextBox9.Text.ToString()));
                            cmd.ExecuteNonQuery();
                        }
                        connection.Close();
                    }
                    this.Hide();
                    break;


                default:
                    MessageBox.Show("Ты как это сделал?!");
                    break;
            }
        }
    }
}
