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
    class Draw
    {
        public void drawCoordiantes(Graphics g, float panel_height, float panel_width)
        {
            // X acis
            float distanceY = (float)panel_height / 10;
            for (int i = 0; i < 10; i++)
            {
                g.DrawLine(Pens.Black, (float)0, (float)((float)panel_height - (float)i * (float)distanceY), (float)panel_width, (float)((float)panel_height - (float)i * (float)distanceY));
            }


            // Y ordinate
            float distanceX = (float)panel_width / 24;
            for (int i = 0; i < 24; i++)
            {
                g.DrawLine(Pens.Black, (float)((float)i * (float)distanceX), (float)((float)panel_height), (float)((float)i * (float)distanceX), (float)0);
            }

        }


        public void drawNumbersY(Graphics g, float panel_height)
        {
            SolidBrush s = new SolidBrush(Color.Black);
            FontFamily ff = new FontFamily("Arial");
            System.Drawing.Font font = new System.Drawing.Font(ff, 8);

            float distanceX = (float)panel_height / 10;

            for (int i = 1; i < 10; i++)
            {
                g.DrawString(Convert.ToString(i * 10), font, s, new PointF(0, (float)((float)panel_height - (float)i * distanceX - 5)));
            }
        }

        public void drawNumbersX(Graphics g, float panel_width)
        {
            SolidBrush s = new SolidBrush(Color.Black);
            FontFamily ff = new FontFamily("Arial");
            System.Drawing.Font font = new System.Drawing.Font(ff, 8);

            float distanceY = (float)panel_width / 24;

            for (int i = 1; i < 24; i++)
            {
                g.DrawString(Convert.ToString(i), font, s, new PointF((float)((float)i * distanceY - 5), 0));
            }
        }

        #region drawing graphs
        public void drawingGraphs(float[] values_probe, int[] hour_probe, int[] minutes_probe, int i_probe,
            Pen p, Graphics g, float panel_height, float panel_width)
        {

            if (i_probe >= 2)
            {
                float distanceY = (float)((float)panel_height / 10);
                float distanceX = (float)((float)panel_width / 24);
                float point1Y = (float)((float)((float)values_probe[i_probe - 1] * (float)distanceY) / (float)10);
                float point2Y = (float)((float)((float)values_probe[i_probe] * (float)distanceY) / (float)10);
                float time1_proportion = (float)hour_probe[i_probe - 1] + (float)((float)((100 * minutes_probe[i_probe - 1]) / 60) / 100);
                float time2_proportion = (float)hour_probe[i_probe] + (float)((float)((100 * minutes_probe[i_probe]) / 60) / 100);
                float point1X = (float)(time1_proportion * distanceX);
                float point2X = (float)(time2_proportion * distanceX);

                g.DrawLine(p, point1X, (float)panel_height - point1Y, point2X, (float)panel_height - point2Y);
            }

        }
        #endregion
    }
}
