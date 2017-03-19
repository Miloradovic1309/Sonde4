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
        Draw draw = new Draw();

        System.IO.StreamWriter file;

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
        int real_time_graph;
        int b_start;

        float[] values_probe1_saved = new float[6000];
        float[] values_probe2_saved = new float[6000];
        float[] values_probe3_saved = new float[6000];
        float[] values_probe4_saved = new float[6000];
        int[] hours_probe1_saved = new int[6000];
        int[] hours_probe2_saved = new int[6000];
        int[] hours_probe3_saved = new int[6000];
        int[] hours_probe4_saved = new int[6000];
        int[] minutes_probe1_saved = new int[6000];
        int[] minutes_probe2_saved = new int[6000];
        int[] minutes_probe3_saved = new int[6000];
        int[] minutes_probe4_saved = new int[6000];
        int[] seconds_probe1_saved = new int[6000];
        int[] seconds_probe2_saved = new int[6000];
        int[] seconds_probe3_saved = new int[6000];
        int[] seconds_probe4_saved = new int[6000];
        #endregion

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

            draw.drawCoordiantes(p1, panel1.Height, panel1.Width);
            draw.drawCoordiantes(p2, panel2.Height, panel2.Width);
            draw.drawCoordiantes(p3, panel3.Height, panel3.Width);
            draw.drawCoordiantes(p4, panel4.Height, panel4.Width);

            draw.drawNumbersY(p5, panel5.Height);
            draw.drawNumbersY(p6, panel6.Height);
            draw.drawNumbersY(p7, panel7.Height);
            draw.drawNumbersY(p8, panel8.Height);

            draw.drawNumbersX(p9, panel9.Width);
            draw.drawNumbersX(p10, panel10.Width);
            draw.drawNumbersX(p11, panel11.Width);
            draw.drawNumbersX(p12, panel12.Width);
        }

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
            real_time_graph = 0;
            b_start = 0;

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
            cbPort.Text = Properties.Settings.Default.comPortSave;
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
                    cbPort.Enabled = true;
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
                    cbPort.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Došlo je do greške.");
            }
        }

        private void bStart_Click(object sender, EventArgs e)
        {
            if (b_start == 0)
            {
                start_monotoring = 1;
                real_time_graph = 1;
                b_start = 1;
                bStart.Text = "Stop";
                drawCoordinateSystem();

                Directory.CreateDirectory("database\\" + folder);

                //textfileprobe1 = new StreamWriter(path1, true);
                //textfileprobe2 = new StreamWriter(path2, true);
                //textfileprobe3 = new StreamWriter(path3, true);
                //textfileprobe4 = new StreamWriter(path4, true);
                //textfileprobe1.Close();
                //textfileprobe2.Close();
                //textfileprobe3.Close();
                //textfileprobe4.Close();
            }
            else
            {
                start_monotoring = 0;
                real_time_graph = 0;
                b_start = 0;
                bStart.Text = "Start";
            }

        }

        private void bClose_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            Properties.Settings.Default.comPortSave = cbPort.Text;
            Properties.Settings.Default.Save();
            this.Close();
            thread.Abort();
            settings.Close();
            Application.Exit();
        }

        private void Sonde2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings settings = new Settings();
            Properties.Settings.Default.comPortSave = cbPort.Text;
            Properties.Settings.Default.Save();
            thread.Abort();
            settings.Close();
            Application.Exit();

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

                    if (real_time_graph == 1)
                    {
                        Pen p1 = new Pen(Color.Red, 1);
                        Graphics g1 = panel1.CreateGraphics();
                        draw.drawingGraphs(values_probe1, hours_probe1, minutes_probe1, i_probe1, p1, g1, panel1.Height, panel1.Width);
                    }

                    //   textfileprobe1.WriteLine(Convert.ToString(hour) + ":" + Convert.ToString(minutes) + ":" 
                    //       + Convert.ToString(seconds) + "|" + Convert.ToString((float)probe1_value));

                    using (file = new System.IO.StreamWriter(path1, true))
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

                    if (real_time_graph == 1)
                    {
                        Pen p2 = new Pen(Color.Blue, 1);
                        Graphics g2 = panel2.CreateGraphics();
                        draw.drawingGraphs(values_probe2, hours_probe2, minutes_probe2, i_probe2, p2, g2, panel2.Height, panel2.Width);
                    }

                    //    textfileprobe2.WriteLine(Convert.ToString(hour) + ":" + Convert.ToString(minutes) + ":"
                    //        + Convert.ToString(seconds) + "|" + Convert.ToString((float)probe2_value));
                    using (file = new System.IO.StreamWriter(path2, true))
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

                    if (real_time_graph == 1)
                    {
                        Pen p3 = new Pen(Color.Green, 1);
                        Graphics g3 = panel3.CreateGraphics();
                        draw.drawingGraphs(values_probe3, hours_probe3, minutes_probe3, i_probe3, p3, g3, panel3.Height, panel3.Width);
                    }

                    //  textfileprobe3.WriteLine(Convert.ToString(hour) + ":" + Convert.ToString(minutes) + ":"
                    //      + Convert.ToString(seconds) + "|" + Convert.ToString((float)probe3_value));

                    using (file = new System.IO.StreamWriter(path3, true))
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

                    if (real_time_graph == 1)
                    {
                        Pen p4 = new Pen(Color.Yellow, 1);
                        Graphics g4 = panel4.CreateGraphics();
                        draw.drawingGraphs(values_probe4, hours_probe4, minutes_probe4, i_probe4, p4, g4, panel4.Height, panel4.Width);
                    }

                    //textfileprobe4.WriteLine(Convert.ToString(hour) + ":" + Convert.ToString(minutes) + ":"
                    //    + Convert.ToString(seconds) + "|" + Convert.ToString((float)probe4_value));

                    using (file = new System.IO.StreamWriter(path4, true))
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
            draw.drawCoordiantes(p1, panel1.Height, panel1.Width);
        }

        private void panel2_Resize_1(object sender, EventArgs e)
        {
            Graphics p2 = panel2.CreateGraphics();
            p2.Clear(Color.FloralWhite);
            draw.drawCoordiantes(p2, panel2.Height, panel2.Width);
        }

        private void panel3_Resize_1(object sender, EventArgs e)
        {
            Graphics p3 = panel3.CreateGraphics();
            p3.Clear(Color.FloralWhite);
            draw.drawCoordiantes(p3, panel3.Height, panel3.Width);
        }

        private void panel4_Resize_1(object sender, EventArgs e)
        {
            Graphics p4 = panel4.CreateGraphics();
            p4.Clear(Color.FloralWhite);
            draw.drawCoordiantes(p4, panel4.Height, panel4.Width);
        }

        private void panel5_Resize(object sender, EventArgs e)
        {
            Graphics p5 = panel5.CreateGraphics();
            p5.Clear(Color.FromArgb(240, 240, 240));
            draw.drawNumbersY(p5, panel5.Height);
        }

        private void panel6_Resize(object sender, EventArgs e)
        {
            Graphics p6 = panel6.CreateGraphics();
            p6.Clear(Color.FromArgb(240, 240, 240));
            draw.drawNumbersY(p6, panel6.Height);
        }

        private void panel7_Resize(object sender, EventArgs e)
        {
            Graphics p7 = panel7.CreateGraphics();
            p7.Clear(Color.FromArgb(240, 240, 240));
            draw.drawNumbersY(p7, panel7.Height);
        }

        private void panel8_Resize(object sender, EventArgs e)
        {
            Graphics p8 = panel8.CreateGraphics();
            p8.Clear(Color.FromArgb(240, 240, 240));
            draw.drawNumbersY(p8, panel8.Height);
        }

        private void panel9_Resize(object sender, EventArgs e)
        {
            Graphics p9 = panel9.CreateGraphics();
            p9.Clear(Color.FromArgb(240, 240, 240));
            draw.drawNumbersX(p9, panel9.Width);
        }

        private void panel10_Resize(object sender, EventArgs e)
        {
            Graphics p10 = panel10.CreateGraphics();
            p10.Clear(Color.FromArgb(240, 240, 240));
            draw.drawNumbersX(p10, panel10.Width);
        }

        private void panel11_Resize(object sender, EventArgs e)
        {
            Graphics p11 = panel11.CreateGraphics();
            p11.Clear(Color.FromArgb(240, 240, 240));
            draw.drawNumbersX(p11, panel11.Width);
        }

        private void panel12_Resize(object sender, EventArgs e)
        {
            Graphics p12 = panel12.CreateGraphics();
            p12.Clear(Color.FromArgb(240, 240, 240));
            draw.drawNumbersX(p12, panel12.Width);
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

        private void bPodesavanja_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.Size = new Size(this.Width - this.Width/5, this.Height - this.Width/5);
            settings.StartPosition = FormStartPosition.CenterScreen;
            settings.ShowDialog();
        }

        private void bDraw_Click(object sender, EventArgs e)
        {
            real_time_graph = 0;
            drawCoordinateSystem();

            string date_string = dateTimePicker1.Value.ToShortDateString();
            int index_point1 = date_string.IndexOf('.');
            string date_snippet1 = date_string.Substring(0, index_point1);
            string date_substring1 = date_string.Substring(index_point1+1);
            int index_point2 = date_substring1.IndexOf('.');
            string date_snippet2 = date_substring1.Substring(0, index_point2);
            string date_substring2 = date_substring1.Substring(index_point2 + 1);

            if ((index_point1 == 2) && (index_point2 == 2))
            {
                date_string = date_string;
            }
            else if((index_point1 == 1) && (index_point2 == 2))
            {
                date_string = "0" + date_snippet1 + "." + date_snippet2 + "." + date_substring2;
            }
            else if ((index_point1 == 2) && (index_point2 == 1))
            {
                date_string = date_snippet1 + "." + "0" + date_snippet2 + "." + date_substring2;
            }
            else if ((index_point1 == 1) && (index_point2 == 1))
            {
                date_string = "0" + date_snippet1 + "." + "0" + date_snippet2 + "." + date_substring2;
            }

            int counter = 0;
            int counter1;
            int counter2;
            int counter3;
            int counter4;
            string line;
            string[] line1 = new string[6000];
            string[] line2 = new string[6000];
            string[] line3 = new string[6000];
            string[] line4 = new string[6000];
            // Read the file and display it line by line.
            System.IO.StreamReader file1 =
               new System.IO.StreamReader(current_directory + "\\database\\" + date_string + "\\sonda1.txt");
            while ((line = file1.ReadLine()) != null)
            {
                line1[counter] = line;
                counter++;
            }
            counter1 = counter;
            counter = 0;
            file1.Close();

            System.IO.StreamReader file2 =
               new System.IO.StreamReader(current_directory + "\\database\\" + date_string + "\\sonda2.txt");
            while ((line = file2.ReadLine()) != null)
            {
                line2[counter] = line;
                counter++;
            }
            counter2 = counter;
            counter = 0;
            file2.Close();

            System.IO.StreamReader file3 =
               new System.IO.StreamReader(current_directory + "\\database\\" + date_string + "\\sonda3.txt");
            while ((line = file3.ReadLine()) != null)
            {
                line3[counter] = line;
                counter++;
            }
            counter3 = counter;
            counter = 0;
            file3.Close();

            System.IO.StreamReader file4 =
               new System.IO.StreamReader(current_directory + "\\database\\" + date_string + "\\sonda4.txt");
            while ((line = file4.ReadLine()) != null)
            {
                line4[counter] = line;
                counter++;
            }
            counter4 = counter;
            counter = 0;
            file4.Close();

            string[] hours_string1 = new string[600];
            string[] hours_string2 = new string[600];
            string[] hours_string3 = new string[600];
            string[] hours_string4 = new string[600];
            string[] minutes_string1 = new string[600];
            string[] minutes_string2 = new string[600];
            string[] minutes_string3 = new string[600];
            string[] minutes_string4 = new string[600];
            string[] values_probe_string1 = new string[6000];
            string[] values_probe_string2 = new string[6000];
            string[] values_probe_string3 = new string[6000];
            string[] values_probe_string4 = new string[6000];

            for (int i=0; i<counter1; i++)
            {
                int index_point = line1[i].IndexOf('|');
                values_probe_string1[i] = line1[i].Substring(index_point+1);
                int index_h = line1[i].IndexOf(':');
                hours_string1[i] = line1[i].Substring(0, index_h);
                string string_help1 = line1[i].Substring(index_h + 1);
                int index_m = string_help1.IndexOf(':');
                minutes_string1[i] = string_help1.Substring(0, index_m);

                values_probe1_saved[i] = float.Parse(values_probe_string1[i]);
                hours_probe1_saved[i] = int.Parse(hours_string1[i]);
                minutes_probe1_saved[i] = int.Parse(minutes_string1[i]);

                Pen p1 = new Pen(Color.Red, 1);
                Graphics g1 = panel1.CreateGraphics();
                draw.drawingGraphs(values_probe1_saved, hours_probe1_saved, minutes_probe1_saved, i, p1, g1, panel1.Height, panel1.Width);
            }
            for (int i = 0; i < counter2; i++)
            {
                int index_point = line2[i].IndexOf('|');
                values_probe_string2[i] = line2[i].Substring(index_point + 1);
                int index_h = line2[i].IndexOf(':');
                hours_string2[i] = line2[i].Substring(0, index_h);
                string string_help2 = line2[i].Substring(index_h + 1);
                int index_m = string_help2.IndexOf(':');
                minutes_string2[i] = string_help2.Substring(0, index_m);

                values_probe2_saved[i] = float.Parse(values_probe_string2[i]);
                hours_probe2_saved[i] = int.Parse(hours_string2[i]);
                minutes_probe2_saved[i] = int.Parse(minutes_string2[i]);

                Pen p2 = new Pen(Color.Blue, 1);
                Graphics g2 = panel2.CreateGraphics();
                draw.drawingGraphs(values_probe2_saved, hours_probe2_saved, minutes_probe2_saved, i, p2, g2, panel2.Height, panel2.Width);
            }
            for (int i = 0; i < counter3; i++)
            {
                int index_point = line3[i].IndexOf('|');
                values_probe_string3[i] = line3[i].Substring(index_point + 1);
                int index_h = line3[i].IndexOf(':');
                hours_string3[i] = line3[i].Substring(0, index_h);
                string string_help3 = line3[i].Substring(index_h + 1);
                int index_m = string_help3.IndexOf(':');
                minutes_string3[i] = string_help3.Substring(0, index_m);

                values_probe3_saved[i] = float.Parse(values_probe_string3[i]);
                hours_probe3_saved[i] = int.Parse(hours_string3[i]);
                minutes_probe3_saved[i] = int.Parse(minutes_string3[i]);

                Pen p3 = new Pen(Color.Green, 1);
                Graphics g3 = panel3.CreateGraphics();
                draw.drawingGraphs(values_probe3_saved, hours_probe3_saved, minutes_probe3_saved, i, p3, g3, panel3.Height, panel3.Width);
            }
            for (int i = 0; i < counter4; i++)
            {
                int index_point = line4[i].IndexOf('|');
                values_probe_string4[i] = line4[i].Substring(index_point + 1);
                int index_h = line4[i].IndexOf(':');
                hours_string4[i] = line4[i].Substring(0, index_h);
                string string_help4 = line4[i].Substring(index_h + 1);
                int index_m = string_help4.IndexOf(':');
                minutes_string4[i] = string_help4.Substring(0, index_m);

                values_probe4_saved[i] = float.Parse(values_probe_string4[i]);
                hours_probe4_saved[i] = int.Parse(hours_string4[i]);
                minutes_probe4_saved[i] = int.Parse(minutes_string4[i]);

                Pen p4 = new Pen(Color.Yellow, 1);
                Graphics g4 = panel4.CreateGraphics();
                draw.drawingGraphs(values_probe4_saved, hours_probe4_saved, minutes_probe4_saved, i, p4, g4, panel4.Height, panel4.Width);
            }


        }
    }
}
