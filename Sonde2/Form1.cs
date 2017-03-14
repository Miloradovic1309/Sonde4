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
        private System.Object lockThis = new System.Object();

        const float graph_move = (float)0;

        int start_monotoring;
        int timer_counter;
        int probe_chosen;
        byte ADR1 = 10;
        byte ADR2 = 20;
        byte ADR3 = 30;
        byte ADR4 = 40;
        int probe1_value;
        int probe2_value;
        int probe3_value;
        int probe4_value;
        int[] temperature_probe1;
        int[] temperature_probe2;
        int[] temperature_probe3;
        int[] temperature_probe4;
        string time_probe1;
        string time_probe2;
        string time_probe3;
        string time_probe4;

        #region Coordinate drawing methods
        public void drawCoordinateSystem()
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
            drawCoordiantes(p1);
            drawCoordiantes(p2);
            drawCoordiantes(p3);
            drawCoordiantes(p4);

            drawNumbersY(p5);
            drawNumbersY(p6);
            drawNumbersY(p7);
            drawNumbersY(p8);

            drawNumbersX(p9);
            drawNumbersX(p10);
            drawNumbersX(p11);
            drawNumbersX(p12);
        }

        public void drawCoordiantes(Graphics g)
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
        
        public void drawNumbersY(Graphics g)
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

        public void drawNumbersX(Graphics g)
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

        #endregion

        #region drawing graphs
        public void drawingGraphs(int[] temperature_probe, string time_probe, Graphics g)
        {
            Pen p = new Pen(Color.Red, 6);
        }
        #endregion

        #region Constructor
        public Sonde2()
        {
            start_monotoring = 0;
            timer_counter = 0;
            probe_chosen = 0;

            InitializeComponent();
            thread = new Thread(ThreadJob);

            //ForCommunication();
            thread.Start();
            timer1.Start();
            timer2.Start();
        }
        #endregion

        #region Destructor
        ~Sonde2(){
            thread.Abort();
        }
        #endregion

        #region Thread
        private void ThreadJob()
        {
            while(true){
                lock (lockThis)
                {
                    
                    
                }
            }
        }
        #endregion

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
                    comPort.Handshake = Handshake.None;
                    comPort.PortName = cbPort.Text;

                    comPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
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
            start_monotoring = 1;
            drawCoordinateSystem();           

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
           
            if (start_monotoring == 1)
            {
                tbSonda1.Text = Convert.ToString(probe1_value);
                tbSonda2.Text = Convert.ToString(probe2_value);
                tbSonda3.Text = Convert.ToString(probe3_value);
                tbSonda4.Text = Convert.ToString(probe4_value);
                timer_counter++;
                if(timer_counter >= 60)
                {
                    timer_counter = 0;
                    
                }
            }
        }


        #region Resize events
        private void panel1_Resize_1(object sender, EventArgs e)
        {
            Graphics p1 = panel1.CreateGraphics();
            p1.Clear(Color.FloralWhite);
            drawCoordiantes(p1);
        }

        private void panel2_Resize_1(object sender, EventArgs e)
        {
            Graphics p2 = panel2.CreateGraphics();
            p2.Clear(Color.FloralWhite);
            drawCoordiantes(p2);
        }

        private void panel3_Resize_1(object sender, EventArgs e)
        {
            Graphics p3 = panel3.CreateGraphics();
            p3.Clear(Color.FloralWhite);
            drawCoordiantes(p3);
        }

        private void panel4_Resize_1(object sender, EventArgs e)
        {
            Graphics p4 = panel4.CreateGraphics();
            p4.Clear(Color.FloralWhite);
            drawCoordiantes(p4);
        }

        private void panel5_Resize(object sender, EventArgs e)
        {
            Graphics p5 = panel5.CreateGraphics();
            p5.Clear(Color.FromArgb(240, 240, 240));
            drawNumbersY(p5);
        }

        private void panel6_Resize(object sender, EventArgs e)
        {
            Graphics p6 = panel6.CreateGraphics();
            p6.Clear(Color.FromArgb(240, 240, 240));
            drawNumbersY(p6);
        }

        private void panel7_Resize(object sender, EventArgs e)
        {
            Graphics p7 = panel7.CreateGraphics();
            p7.Clear(Color.FromArgb(240, 240, 240));
            drawNumbersY(p7);
        }

        private void panel8_Resize(object sender, EventArgs e)
        {
            Graphics p8 = panel8.CreateGraphics();
            p8.Clear(Color.FromArgb(240, 240, 240));
            drawNumbersY(p8);
        }

        private void panel9_Resize(object sender, EventArgs e)
        {
            Graphics p9 = panel9.CreateGraphics();
            p9.Clear(Color.FromArgb(240, 240, 240));
            drawNumbersX(p9);
        }

        private void panel10_Resize(object sender, EventArgs e)
        {
            Graphics p10 = panel10.CreateGraphics();
            p10.Clear(Color.FromArgb(240, 240, 240));
            drawNumbersX(p10);
        }

        private void panel11_Resize(object sender, EventArgs e)
        {
            Graphics p11 = panel11.CreateGraphics();
            p11.Clear(Color.FromArgb(240, 240, 240));
            drawNumbersX(p11);
        }

        private void panel12_Resize(object sender, EventArgs e)
        {
            Graphics p12 = panel12.CreateGraphics();
            p12.Clear(Color.FromArgb(240, 240, 240));
            drawNumbersX(p12);
        }

        private void Sonde2_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                drawCoordinateSystem();
            }

        }
        #endregion


        public void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(150);
            //SerialPort sp = (SerialPort)sender;
            int bytestoread = comPort.BytesToRead;
            byte[] received_buffer = new byte[9];

            comPort.Read(received_buffer, 0, bytestoread);

            if ((received_buffer[0] == 0x21) && (received_buffer[8] == 0x1B))
            {
                if(received_buffer[1] == ADR1)
                {
                    probe1_value = received_buffer[3] * 100 + received_buffer[4];
                }
                else if(received_buffer[1] == ADR2)
                {
                    probe2_value = received_buffer[3] * 100 + received_buffer[4];
                }
                else if(received_buffer[1] == ADR3)
                {
                    probe3_value = received_buffer[3] * 100 + received_buffer[4];
                }
                else if(received_buffer[1] == ADR4)
                {
                    probe4_value = received_buffer[3] * 100 + received_buffer[4];
                }                

            }

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (probe_chosen == 0)
            {
                comPort.Write("1");
                probe_chosen++;
            }
            else if (probe_chosen == 1)
            {
                comPort.Write("2");
                probe_chosen++;
            }
            else if (probe_chosen == 2)
            {
                comPort.Write("3");
                probe_chosen++;
            }
            else if (probe_chosen == 3)
            {
                comPort.Write("4");
                probe_chosen = 0;
            }
        }
    }
}
