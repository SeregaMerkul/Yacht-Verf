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
using static System.Net.Mime.MediaTypeNames;

namespace Yacht_Verf
{
    public partial class EditValues : Form
    {
        public string value_type;
        public EditValues(string value)
        {
            InitializeComponent();
            value_type = value;
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
                    bunifuDropdown3.DataSource = null;
                    bunifuDropdown3.DataSource = ds.Tables["Types_Boat"];
                    bunifuDropdown3.DisplayMember = "Name_Type_Boat";
                    bunifuDropdown3.ValueMember = "Id_Type_Boat";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
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

                        using (SqlCommand cmd = new SqlCommand("UPDATE Units SET Name_Unit = @Name_Unit", connection))
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

                        using (SqlCommand cmd = new SqlCommand("UPDATE Materials SET Name_Material=@Name_Material, Id_Unit = @Id_Unit", connection))
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

                        using (SqlCommand cmd = new SqlCommand("UPDATE Price_Of_Unit SET Id_Material =@Id_Material, Id_Unit = (SELECT Id_Unit FROM Materials WHERE Id_Material = @Id_Material), Price = @Price", connection))
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

                        using (SqlCommand cmd = new SqlCommand("UPDATE Types_Boat SET Name_Type_Boat = @Name_Type_Boat", connection))
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

                        using (SqlCommand cmd = new SqlCommand("UPDATE Providers SET Name_Provider= @Name_Provider", connection))
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

                        using (SqlCommand cmd = new SqlCommand("UPDATE Warehouses SET Name_Warehouse = @Name_Warehouse)", connection))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@Name_Warehouse", bunifuTextBox5.Text.ToString());
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

        private void EditValues_Load(object sender, EventArgs e)
        {
            Get_list_DropDown1();
            Get_list_DropDown2();
            Get_list_DropDown3();
        }
    }
}
