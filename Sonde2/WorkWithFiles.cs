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
    class WorkWithFiles
    {
        //private string[] line = new string[6000];
        //private int counter;

        private string[] hours_string = new string[6000];
        private string[] minutes_string = new string[6000];
        private string[] values_probe_string = new string[6000];
        private float[] values_probe_saved = new float[6000];
        private int[] hours_probe_saved = new int[6000];
        private int[] minutes_probe_saved = new int[6000];
        private int[] seconds_probe_saved = new int[6000];
        public float[] takeProbeValuesFromFile(int counter, string[] line)
        {
            for (int i = 0; i < counter; i++)
            {
                int index_point = line[i].IndexOf('|');
                values_probe_string[i] = line[i].Substring(index_point + 1);
                int index_h = line[i].IndexOf(':');
                hours_string[i] = line[i].Substring(0, index_h);
                string string_help1 = line[i].Substring(index_h + 1);
                int index_m = string_help1.IndexOf(':');
                minutes_string[i] = string_help1.Substring(0, index_m);
                values_probe_saved[i] = float.Parse(values_probe_string[i]);
            }
                return values_probe_saved;
        }
        public int[] takeHoursFromFile(int counter, string[] line)
        {
            for (int i = 0; i < counter; i++)
            {
                int index_point = line[i].IndexOf('|');
                values_probe_string[i] = line[i].Substring(index_point + 1);
                int index_h = line[i].IndexOf(':');
                hours_string[i] = line[i].Substring(0, index_h);
                string string_help1 = line[i].Substring(index_h + 1);
                int index_m = string_help1.IndexOf(':');
                minutes_string[i] = string_help1.Substring(0, index_m);
                hours_probe_saved[i] = int.Parse(hours_string[i]);
            }
            return hours_probe_saved;

        }
        public int[] takeMinutesFromFile(int counter, string[] line)
        {
            for (int i = 0; i < counter; i++)
            {
                int index_point = line[i].IndexOf('|');
                values_probe_string[i] = line[i].Substring(index_point + 1);
                int index_h = line[i].IndexOf(':');
                hours_string[i] = line[i].Substring(0, index_h);
                string string_help1 = line[i].Substring(index_h + 1);
                int index_m = string_help1.IndexOf(':');
                minutes_string[i] = string_help1.Substring(0, index_m);
                values_probe_saved[i] = float.Parse(values_probe_string[i]);
                minutes_probe_saved[i] = int.Parse(minutes_string[i]);
            }
            return minutes_probe_saved;

        }

        ~WorkWithFiles(){
            
        }



        //public void readLinesFromFile(System.IO.StreamReader file, string current_directory, string date_string, string sondatext)
        //{
        //    file = new System.IO.StreamReader(current_directory + "\\database\\" + date_string + "\\" + sondatext);
        //    this.counter = 0;
        //    while ((this.line[counter] = file.ReadLine()) != null)
        //    {
        //        this.counter++;
        //    }
        //    file.Close();
        //}
        //public string[] getLines(System.IO.StreamReader file, string current_directory,
        //    string date_string, string sondatext)
        //{
        //    readLinesFromFile(file, current_directory, date_string, sondatext);
        //    return line;
        //}
        //public int getCounter(System.IO.StreamReader file, string current_directory,
        //    string date_string, string sondatext)
        //{
        //    readLinesFromFile(file, current_directory, date_string, sondatext);
        //    return counter;
        //}



    }
}
