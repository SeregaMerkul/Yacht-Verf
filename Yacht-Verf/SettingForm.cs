using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yacht_Verf
{
    public partial class SettingForm : Form
    {
        public SettingForm()
        {
            InitializeComponent();
        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            AddValuesToSetting addValuesToSetting = new AddValuesToSetting("Units");
            addValuesToSetting.ShowDialog();

        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            AddValuesToSetting addValuesToSetting = new AddValuesToSetting("Materials");
            addValuesToSetting.ShowDialog();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            AddValuesToSetting addValuesToSetting = new AddValuesToSetting("Price_Of_Unit");
            addValuesToSetting.ShowDialog();
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            AddValuesToSetting addValuesToSetting = new AddValuesToSetting("TypeBoat");
            addValuesToSetting.ShowDialog();
        }

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            AddValuesToSetting addValuesToSetting = new AddValuesToSetting("Providers");
            addValuesToSetting.ShowDialog();
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            AddValuesToSetting addValuesToSetting = new AddValuesToSetting("Warehouses");
            addValuesToSetting.ShowDialog();
        }

        private void bunifuButton7_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void bunifuButton8_Click(object sender, EventArgs e)
        {
            AddValuesToSetting addValuesToSetting = new AddValuesToSetting("Engine");
            addValuesToSetting.ShowDialog();
        }
    }
}
