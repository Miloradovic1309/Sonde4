using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sonde2
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            cbColor1.Items.Add("Crvena");
            cbColor1.Items.Add("Plava");
            cbColor1.Items.Add("Zelena");
            cbColor1.Items.Add("Žuta");
            cbColor1.Items.Add("Narandžasta");
            cbColor1.Items.Add("Ljubičasta");
            cbColor1.Items.Add("Braon");
            cbColor1.Items.Add("Crna");
            cbColor2.Items.Add("Crvena");
            cbColor2.Items.Add("Plava");
            cbColor2.Items.Add("Zelena");
            cbColor2.Items.Add("Žuta");
            cbColor2.Items.Add("Narandžasta");
            cbColor2.Items.Add("Ljubičasta");
            cbColor2.Items.Add("Braon");
            cbColor2.Items.Add("Crna");
            cbColor3.Items.Add("Crvena");
            cbColor3.Items.Add("Plava");
            cbColor3.Items.Add("Zelena");
            cbColor3.Items.Add("Žuta");
            cbColor3.Items.Add("Narandžasta");
            cbColor3.Items.Add("Ljubičasta");
            cbColor3.Items.Add("Braon");
            cbColor3.Items.Add("Crna");
            cbColor4.Items.Add("Crvena");
            cbColor4.Items.Add("Plava");
            cbColor4.Items.Add("Zelena");
            cbColor4.Items.Add("Žuta");
            cbColor4.Items.Add("Narandžasta");
            cbColor4.Items.Add("Ljubičasta");
            cbColor4.Items.Add("Braon");
            cbColor4.Items.Add("Crna");

            cbWidth1.Items.Add("0,5");
            cbWidth1.Items.Add("1");
            cbWidth1.Items.Add("1,5");
            cbWidth1.Items.Add("2");
            cbWidth1.Items.Add("2,5");
            cbWidth1.Items.Add("3");
            cbWidth2.Items.Add("0,5");
            cbWidth2.Items.Add("1");
            cbWidth2.Items.Add("1,5");
            cbWidth2.Items.Add("2");
            cbWidth2.Items.Add("2,5");
            cbWidth2.Items.Add("3");
            cbWidth3.Items.Add("0,5");
            cbWidth3.Items.Add("1");
            cbWidth3.Items.Add("1,5");
            cbWidth3.Items.Add("2");
            cbWidth3.Items.Add("2,5");
            cbWidth3.Items.Add("3");
            cbWidth4.Items.Add("0,5");
            cbWidth4.Items.Add("1");
            cbWidth4.Items.Add("1,5");
            cbWidth4.Items.Add("2");
            cbWidth4.Items.Add("2,5");
            cbWidth4.Items.Add("3");

            cbWidth1.Text = Convert.ToString(Properties.Settings.Default.width1);
            cbWidth2.Text = Convert.ToString(Properties.Settings.Default.width2);
            cbWidth3.Text = Convert.ToString(Properties.Settings.Default.width3);
            cbWidth4.Text = Convert.ToString(Properties.Settings.Default.width4);

            cbColor1.Text = Properties.Settings.Default.ColorProbe1;
            cbColor2.Text = Properties.Settings.Default.ColorProbe2;
            cbColor3.Text = Properties.Settings.Default.ColorProbe3;
            cbColor4.Text = Properties.Settings.Default.ColorProbe4;
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.width1 = float.Parse(cbWidth1.Text);
            Properties.Settings.Default.width2 = float.Parse(cbWidth2.Text);
            Properties.Settings.Default.width3 = float.Parse(cbWidth3.Text);
            Properties.Settings.Default.width4 = float.Parse(cbWidth4.Text);
            Properties.Settings.Default.ColorProbe1 = cbColor1.Text;
            Properties.Settings.Default.ColorProbe2 = cbColor2.Text;
            Properties.Settings.Default.ColorProbe3 = cbColor3.Text;
            Properties.Settings.Default.ColorProbe4 = cbColor4.Text;
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void bClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
