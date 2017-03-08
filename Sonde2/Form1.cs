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
            p1.Clear(Color.FloralWhite);
            p2.Clear(Color.FloralWhite);
            p3.Clear(Color.FloralWhite);
            p4.Clear(Color.FloralWhite);
            DrawCoordiantes(p1);
            DrawCoordiantes(p2);
            DrawCoordiantes(p3);
            DrawCoordiantes(p4);

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
    }
}
