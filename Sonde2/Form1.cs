using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Threading;
using System.Globalization;

namespace Sonde2
{
    public partial class Sonde2 : Form
    {
        
        SerialPort comPort = new SerialPort();
        int number_of_ports = 100;
        Thread thread;
        const float graph_move = (float)0;


        public void DrawCoordiantes(Graphics g)
        {

            
            // X acis
            float distanceX = (float)panel1.Height / 10;
            for(int i = 0; i < 10; i++)
            {
                g.DrawLine(Pens.Black, (float)0, (float)((float)panel1.Height - (float)i * (float)distanceX), (float)panel1.Width, (float)((float)panel1.Height - (float)i * (float)distanceX + graph_move));
            }
            
            
            // Y ordinate
            float distanceY = (float)panel1.Width / 24;
            for(int i = 0; i < 24; i++)
            {
                g.DrawLine(Pens.Black, (float)((float)i * (float)distanceY), (float)((float)panel1.Height), (float)((float)i * (float)distanceY), (float)0);
            }

            

        }
        
        public void DrawNumbersY(Graphics g)
        {
            SolidBrush s = new SolidBrush(Color.Black);
            FontFamily ff = new FontFamily("Arial");
            System.Drawing.Font font = new System.Drawing.Font(ff, 8);

            float distanceX = (float)panel5.Height / 10;

            for (int i = 1; i < 10; i++)
            {
                g.DrawString(Convert.ToString(i * 10), font, s, new PointF(0, (float)((float)panel5.Height - (float)i * distanceX - 5)));
            }
        }

        public void DrawNumbersX(Graphics g)
        {
            SolidBrush s = new SolidBrush(Color.Black);
            FontFamily ff = new FontFamily("Arial");
            System.Drawing.Font font = new System.Drawing.Font(ff, 8);

            float distanceY = (float)panel10.Width / 24;

            for (int i = 1; i < 24; i++)
            {
                g.DrawString(Convert.ToString(i), font, s, new PointF((float)((float)i * distanceY - 5), 0));
            }
        }

        public Sonde2()
        {
            InitializeComponent();
            thread = new Thread(ThreadJob);

            //ForCommunication();
            thread.Start();
            timer1.Start();
        }
        ~Sonde2(){
            thread.Abort();
        }

        private void ThreadJob()
        {
            while(true){
                tbVremePocetkaOcitavanja.Text = "Tred radi";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 1; i <= number_of_ports; i++)
            {
                cbPort.Items.Add("COM" + Convert.ToString(i));
            }
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            bStart.Enabled = false;
            


        }

        private void bPort_Click(object sender, EventArgs e)
        {
            try
            {
                if (comPort.IsOpen)
                {
                    comPort.Close();
                    rbPort.Checked = false;
                    bStart.Enabled = false;
                }
                else
                {
                    comPort.DataBits = 8;
                    comPort.Parity = Parity.None;
                    comPort.StopBits = StopBits.One;
                    comPort.BaudRate = 9600;
                    comPort.PortName = cbPort.Text;
                    comPort.Open();
                    rbPort.Checked = true;
                    bStart.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Došlo je do greške.");
            }
        }

        private void bStart_Click(object sender, EventArgs e)
        {
            Graphics p1 = panel1.CreateGraphics();
            Graphics p2 = panel2.CreateGraphics();
            Graphics p3 = panel3.CreateGraphics();
            Graphics p4 = panel4.CreateGraphics();
            Graphics p5 = panel5.CreateGraphics();
            Graphics p6 = panel6.CreateGraphics();
            Graphics p7 = panel7.CreateGraphics();
            Graphics p8 = panel8.CreateGraphics();
            Graphics p9 = panel9.CreateGraphics();
            Graphics p10 = panel10.CreateGraphics();
            Graphics p11 = panel11.CreateGraphics();
            Graphics p12 = panel12.CreateGraphics();
            p1.Clear(Color.FloralWhite);
            p2.Clear(Color.FloralWhite);
            p3.Clear(Color.FloralWhite);
            p4.Clear(Color.FloralWhite);
            DrawCoordiantes(p1);
            DrawCoordiantes(p2);
            DrawCoordiantes(p3);
            DrawCoordiantes(p4);

            DrawNumbersY(p5);
            DrawNumbersY(p6);
            DrawNumbersY(p7);
            DrawNumbersY(p8);

            DrawNumbersX(p9);
            DrawNumbersX(p10);
            DrawNumbersX(p11);
            DrawNumbersX(p12);

        }

        private void bClose_Click(object sender, EventArgs e)
        {
            this.Close();
            thread.Abort();
        }

        private void Sonde2_FormClosing(object sender, FormClosingEventArgs e)
        {
            thread.Abort();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime localDate = DateTime.Now;
            tbVremeDatum.Text = localDate.ToString();
        }

        private void panel1_Resize_1(object sender, EventArgs e)
        {
            Graphics p1 = panel1.CreateGraphics();
            p1.Clear(Color.FloralWhite);
            DrawCoordiantes(p1);
        }

        private void panel2_Resize_1(object sender, EventArgs e)
        {
            Graphics p2 = panel2.CreateGraphics();
            p2.Clear(Color.FloralWhite);
            DrawCoordiantes(p2);
        }

        private void panel3_Resize_1(object sender, EventArgs e)
        {
            Graphics p3 = panel3.CreateGraphics();
            p3.Clear(Color.FloralWhite);
            DrawCoordiantes(p3);
        }

        private void panel4_Resize_1(object sender, EventArgs e)
        {
            Graphics p4 = panel4.CreateGraphics();
            p4.Clear(Color.FloralWhite);
            DrawCoordiantes(p4);
        }

        private void panel5_Resize(object sender, EventArgs e)
        {
            Graphics p5 = panel5.CreateGraphics();
            p5.Clear(Color.FromArgb(240, 240, 240));
            DrawNumbersY(p5);
        }

        private void panel6_Resize(object sender, EventArgs e)
        {
            Graphics p6 = panel6.CreateGraphics();
            p6.Clear(Color.FromArgb(240, 240, 240));
            DrawNumbersY(p6);
        }

        private void panel7_Resize(object sender, EventArgs e)
        {
            Graphics p7 = panel7.CreateGraphics();
            p7.Clear(Color.FromArgb(240, 240, 240));
            DrawNumbersY(p7);
        }

        private void panel8_Resize(object sender, EventArgs e)
        {
            Graphics p8 = panel8.CreateGraphics();
            p8.Clear(Color.FromArgb(240, 240, 240));
            DrawNumbersY(p8);
        }

        private void panel9_Resize(object sender, EventArgs e)
        {
            Graphics p9 = panel9.CreateGraphics();
            p9.Clear(Color.FromArgb(240, 240, 240));
            DrawNumbersX(p9);
        }

        private void panel10_Resize(object sender, EventArgs e)
        {
            Graphics p10 = panel10.CreateGraphics();
            p10.Clear(Color.FromArgb(240, 240, 240));
            DrawNumbersX(p10);
        }

        private void panel11_Resize(object sender, EventArgs e)
        {
            Graphics p11 = panel11.CreateGraphics();
            p11.Clear(Color.FromArgb(240, 240, 240));
            DrawNumbersX(p11);
        }

        private void panel12_Resize(object sender, EventArgs e)
        {
            Graphics p12 = panel12.CreateGraphics();
            p12.Clear(Color.FromArgb(240, 240, 240));
            DrawNumbersX(p12);
        }

        private void Sonde2_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                bStart_Click(sender, e);
            }

        }


    }
}
