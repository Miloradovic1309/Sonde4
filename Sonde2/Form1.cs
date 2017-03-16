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
        DateTime dt = DateTime.Now;
        StreamWriter textfileprobe1;
        StreamWriter textfileprobe2;
        StreamWriter textfileprobe3;
        StreamWriter textfileprobe4;



        int number_of_ports = 100;
        Thread thread;
        private System.Object lockThis = new System.Object();

        #region def

        const float graph_move = (float)0;

        int start_monotoring;
        int timer_counter;
        int probe_chosen;
        byte ADR1 = 10;
        byte ADR2 = 20;
        byte ADR3 = 30;
        byte ADR4 = 40;
        float probe1_value;
        float probe2_value;
        float probe3_value;
        float probe4_value;
        float[] values_probe1 = new float[6000];
        float[] values_probe2 = new float[6000];
        float[] values_probe3 = new float[6000];
        float[] values_probe4 = new float[6000];
        string time_probe1;
        string time_probe2;
        string time_probe3;
        string time_probe4;
        int i_probe1;
        int i_probe2;
        int i_probe3;
        int i_probe4;
        int add_value_to_graphic;
        int hour;
        int minutes;
        int seconds;
        int[] hours_probe1 = new int[6000];
        int[] hours_probe2 = new int[6000];
        int[] hours_probe3 = new int[6000];
        int[] hours_probe4 = new int[6000];
        int[] minutes_probe1 = new int[6000];
        int[] minutes_probe2 = new int[6000];
        int[] minutes_probe3 = new int[6000];
        int[] minutes_probe4 = new int[6000];
        int[] seconds_probe1 = new int[6000];
        int[] seconds_probe2 = new int[6000];
        int[] seconds_probe3 = new int[6000];
        int[] seconds_probe4 = new int[6000];
        string folder;
        string path1;
        string path2;
        string path3;
        string path4;
        string now_or_previous_day;
        string current_directory = Directory.GetCurrentDirectory();

        #endregion

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
            float distanceY = (float)panel1.Height / 10;
            for(int i = 0; i < 10; i++)
            {
                g.DrawLine(Pens.Black, (float)0, (float)((float)panel1.Height - (float)i * (float)distanceY), (float)panel1.Width, (float)((float)panel1.Height - (float)i * (float)distanceY + graph_move));
            }
            
            
            // Y ordinate
            float distanceX = (float)panel1.Width / 24;
            for(int i = 0; i < 24; i++)
            {
                g.DrawLine(Pens.Black, (float)((float)i * (float)distanceX), (float)((float)panel1.Height), (float)((float)i * (float)distanceX), (float)0);
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
        public void drawingGraphs(float[] values_probe, int[] hour_probe, int[] minutes_probe, int i_probe,
            Pen p, Graphics g)
        {      

            if (i_probe >= 2)
            {
                float distanceY = (float)((float)panel1.Height / 10);
                float distanceX = (float)((float)panel1.Width / 24);
                float point1Y = (float)((float)(values_probe[i_probe - 1] / 10) * distanceY);
                float point2Y = (float)((float)(values_probe[i_probe] / 10) * distanceY);
                float time1_proportion = (float)hour_probe[i_probe - 1] + (float)((float)((100 * minutes_probe[i_probe - 1]) / 60)/100);
                float time2_proportion = (float)hour_probe[i_probe] + (float)((float)((100 * minutes_probe[i_probe]) / 60) /100);
                float point1X = (float)(time1_proportion * distanceX);
                float point2X = (float)(time2_proportion * distanceX);

                g.DrawLine(Pens.Red, point1X, point1Y, point2X, point2Y);
            }
            
        }
        #endregion

        #region Constructor
        public Sonde2()
        {
            start_monotoring = 0;
            timer_counter = 0;
            probe_chosen = 0;
            add_value_to_graphic = 0;
            i_probe1 = 0;
            i_probe2 = 0;
            i_probe3 = 0;
            i_probe4 = 0;

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
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd.MM.yyyy";
            dateTimePicker1.Value = new DateTime(dt.Year, dt.Month, dt.Day);
            bStart.Enabled = false;

            folder = dateTimePicker1.Text;
            now_or_previous_day = dateTimePicker1.Text;
            path1 = current_directory + "\\database\\" + folder + "\\sonda1.txt";
            path2 = current_directory + "\\database\\" + folder + "\\sonda2.txt";
            path3 = current_directory + "\\database\\" + folder + "\\sonda3.txt";
            path4 = current_directory + "\\database\\" + folder + "\\sonda4.txt";

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

            Directory.CreateDirectory("database\\" + folder);
            
            textfileprobe1 = new StreamWriter(path1, true);
            textfileprobe2 = new StreamWriter(path2, true);
            textfileprobe3 = new StreamWriter(path3, true);
            textfileprobe4 = new StreamWriter(path4, true);
            textfileprobe1.Close();
            textfileprobe2.Close();
            textfileprobe3.Close();
            textfileprobe4.Close();

        }

        private void bClose_Click(object sender, EventArgs e)
        {
            this.Close();
            thread.Abort();
            textfileprobe1.Close();
            textfileprobe2.Close();
            textfileprobe3.Close();
            textfileprobe4.Close();
        }

        private void Sonde2_FormClosing(object sender, FormClosingEventArgs e)
        {
            thread.Abort();
            textfileprobe1.Close();
            textfileprobe2.Close();
            textfileprobe3.Close();
            textfileprobe4.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime localDate = DateTime.Now;
            tbVremeDatum.Text = localDate.ToString("dd.MM.yyyy.  HH:mm:ss");
            hour = localDate.Hour;
            minutes = localDate.Minute;
            seconds = localDate.Second;
            

            if (start_monotoring == 1)
            {
                tbSonda1.Text = Convert.ToString(probe1_value) + "°C";
                tbSonda2.Text = Convert.ToString(probe2_value) + "°C";
                tbSonda3.Text = Convert.ToString(probe3_value) + "°C";
                tbSonda4.Text = Convert.ToString(probe4_value) + "°C";

                if (add_value_to_graphic == 1)
                {
                    values_probe1[i_probe1] = probe1_value;
                    hours_probe1[i_probe1] = hour;
                    minutes_probe1[i_probe1] = minutes;
                    seconds_probe1[i_probe1] = seconds;

                    Pen p1 = new Pen(Color.Red, 6);
                    Graphics g1 = panel1.CreateGraphics();
                    drawingGraphs(values_probe1, hours_probe1, minutes_probe1, i_probe1, p1, g1);

                    //   textfileprobe1.WriteLine(Convert.ToString(hour) + ":" + Convert.ToString(minutes) + ":" 
                    //       + Convert.ToString(seconds) + "|" + Convert.ToString((float)probe1_value));

                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(path1, true))
                    {
                        file.WriteLine(Convert.ToString(hour) + ":" + Convert.ToString(minutes) + ":"
                           + Convert.ToString(seconds) + "|" + Convert.ToString((float)probe1_value));
                    }

                    i_probe1++;
                    add_value_to_graphic = 0;
                }
                else if (add_value_to_graphic == 2)
                {
                    values_probe2[i_probe2] = probe2_value;
                    hours_probe2[i_probe2] = hour;
                    minutes_probe2[i_probe2] = minutes;
                    seconds_probe2[i_probe2] = seconds;

                    Pen p2 = new Pen(Color.Blue, 5);
                    Graphics g2 = panel2.CreateGraphics();
                    drawingGraphs(values_probe2, hours_probe2, minutes_probe2, i_probe2, p2, g2);

                    //    textfileprobe2.WriteLine(Convert.ToString(hour) + ":" + Convert.ToString(minutes) + ":"
                    //        + Convert.ToString(seconds) + "|" + Convert.ToString((float)probe2_value));
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(path2, true))
                    {
                        file.WriteLine(Convert.ToString(hour) + ":" + Convert.ToString(minutes) + ":"
                           + Convert.ToString(seconds) + "|" + Convert.ToString((float)probe2_value));
                    }

                    i_probe2++;
                    add_value_to_graphic = 0;
                }
                else if (add_value_to_graphic == 3)
                {
                    values_probe3[i_probe3] = probe3_value;
                    hours_probe3[i_probe3] = hour;
                    minutes_probe3[i_probe3] = minutes;
                    seconds_probe3[i_probe3] = seconds;

                    Pen p3 = new Pen(Color.Green, 4);
                    Graphics g3 = panel3.CreateGraphics();
                    drawingGraphs(values_probe3, hours_probe3, minutes_probe3, i_probe3, p3, g3);

                    //  textfileprobe3.WriteLine(Convert.ToString(hour) + ":" + Convert.ToString(minutes) + ":"
                    //      + Convert.ToString(seconds) + "|" + Convert.ToString((float)probe3_value));

                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(path3, true))
                    {
                        file.WriteLine(Convert.ToString(hour) + ":" + Convert.ToString(minutes) + ":"
                           + Convert.ToString(seconds) + "|" + Convert.ToString((float)probe3_value));
                    }

                    i_probe3++;
                    add_value_to_graphic = 0;
                }
                else if (add_value_to_graphic == 4)
                {
                    values_probe4[i_probe4] = (float)probe4_value;
                    hours_probe4[i_probe4] = hour;
                    minutes_probe4[i_probe4] = minutes;
                    seconds_probe4[i_probe4] = seconds;

                    Pen p4 = new Pen(Color.Yellow, 3);
                    Graphics g4 = panel4.CreateGraphics();
                    drawingGraphs(values_probe4, hours_probe4, minutes_probe4, i_probe4, p4, g4);

                    //textfileprobe4.WriteLine(Convert.ToString(hour) + ":" + Convert.ToString(minutes) + ":"
                    //    + Convert.ToString(seconds) + "|" + Convert.ToString((float)probe4_value));

                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(path4, true))
                    {
                        file.WriteLine(Convert.ToString(hour) + ":" + Convert.ToString(minutes) + ":"
                           + Convert.ToString(seconds) + "|" + Convert.ToString((float)probe4_value));
                    }

                    i_probe4++;
                    add_value_to_graphic = 0;
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
                    probe1_value = (float)(((float)received_buffer[3] * 100 + (float)received_buffer[4])/10);
                    add_value_to_graphic = 1;
                }
                else if(received_buffer[1] == ADR2)
                {
                    probe2_value = (float)(((float)received_buffer[3] * 100 + (float)received_buffer[4])/10);
                    add_value_to_graphic = 2;
                }
                else if(received_buffer[1] == ADR3)
                {
                    probe3_value = (float)(((float)received_buffer[3] * 100 + (float)received_buffer[4])/10);
                    add_value_to_graphic = 3;
                }
                else if(received_buffer[1] == ADR4)
                {
                    probe4_value = (float)(((float)received_buffer[3] * 100 + (float)received_buffer[4])/10);
                    add_value_to_graphic = 4;
                }                

            }

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (comPort.IsOpen)
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
}
