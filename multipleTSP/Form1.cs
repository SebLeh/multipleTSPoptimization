using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace multipleTSP
{

    public partial class multipleTSP : Form
    {
        public Point[][] allTours = new Point[1][];
        private Point mid = new Point(0,0);
        int gv_points = 0;
        int gv_tours = 0;
        int gv_draw_counter; 
        double gv_totalLength = 0;
        double gv_avgLength = 0;

        Boolean drawMode = false;

        public multipleTSP()
        {
            InitializeComponent();
            paintPanel.Paint += new PaintEventHandler(paint);
            paintPanel.Click += new EventHandler(panel_click);
            paintPanel.Controls.Clear();

            mid.X = paintPanel.Width / 2;
            mid.Y = paintPanel.Height / 2;
            allTours[0] = new Point[] { mid };

            gv_draw_counter = 0;

        }

        public void paint(object sender, PaintEventArgs e)
        {

            var p = sender as Panel;
            var g = e.Graphics;

            Pen blackPen = new Pen(Color.Black);
            Brush redBrush = new SolidBrush(Color.Red);
            Brush blackBrush = new SolidBrush(Color.Black);

            g.FillEllipse(redBrush, mid.X - 2, mid.Y - 2, 5, 5);

            for (int i = 0; i < allTours.Length; i++)
            {
                if (allTours[i].Length > 1)
                {
                    g.DrawPolygon(blackPen, allTours[i]);
                }
            }

        }

        public void panel_click(object sender, EventArgs e)
        {
            if (drawMode == true)
            {
                Array.Resize(ref allTours[gv_draw_counter], allTours[gv_draw_counter].Length + 1);
                allTours[gv_draw_counter][allTours[gv_draw_counter].Length - 1] = paintPanel.PointToClient(Cursor.Position);
                this.Refresh();
            }
            else
            {
                return;
            }

        }

        private void clear_all()
        {
            paintPanel.Controls.Clear();
            gui_points.Text = "";
            gui_tours.Text = "";
            gv_points = 0;
            gv_tours = 0;
            gui_avgLength.Text = "";
            gui_totalLength.Text = "";
            gv_avgLength = 0;
            gv_totalLength = 0;
            button_generate.Enabled = true;
            Array.Resize(ref allTours, 0);
            this.Refresh();

            Array.Resize(ref allTours, 1);
            Array.Resize(ref allTours[0], 1);
            allTours[0] = new Point[] { mid };
            gv_draw_counter = 0;
        }

        private void sAVEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Evo-Path|*.evo";
            saveFile.Title = "Save current Path";
            saveFile.ShowDialog();

            try
            {
                if (saveFile.FileName != "")
                {
                    System.IO.FileStream fs = null;
                    System.IO.StreamWriter fw = null;

                    path_format format = new path_format();

                    using (fs = (System.IO.FileStream)saveFile.OpenFile())
                    using (fw = new System.IO.StreamWriter(fs))
                    {
                        fw.Write(format.toString(allTours));
                    }
                    fw.Close();
                    fs.Close();
                }
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show("Fehler beim Speichern!");
            }

        }

        private void button_generate_Click(object sender, EventArgs e)
        {
            paintPanel.Cursor = System.Windows.Forms.Cursors.Arrow;
            try
            {
                gv_points = int.Parse(gui_points.Text);
                gv_tours = int.Parse(gui_tours.Text);
            }
            catch (Exception)
            {
                //gv_cities = 0;
                //gv_workers = 0;
                MessageBox.Show("Please only insert positive Numbers!");
                return;
            }
            //finally
            //{
                if (gv_points <= 0 | gv_tours <= 0)
                {
                    MessageBox.Show("Please only insert positive Numbers!");
                }

                else if (gv_tours > gv_points)
                {
                    MessageBox.Show("Numbers of Points should be higher or equal to the number of Tours");
                }
                else
                {

                    //allTours = initialization.path(gv_points, gv_tours, paintPanel.Width, paintPanel.Height);

                    gv_totalLength = 0;
                    gv_avgLength = 0;
                    //localGo.Enabled = true;
                    //evoGo.Enabled = true;
                    this.Refresh();
                }
            //}

        }

        private void oPENToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.IO.Stream fs = null;
            System.IO.StreamReader fr = null;

            path_format format = new path_format();

            String pathString = null;

            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Evo-Path|*.evo";
            openFile.Title = "Open Path";
            openFile.RestoreDirectory = false;
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                //localGo.Enabled = true;
                try
                {
                    if (openFile.OpenFile() != null)
                    {
                        using (fs = openFile.OpenFile())
                        {
                            fr = new System.IO.StreamReader(openFile.FileName);
                            pathString = fr.ReadToEnd();
                            allTours = format.toPoints(pathString);
                            gv_tours = 0;
                            gv_points = 0;
                            //for (int i = 0; i < allTours.Length; i++)
                            //{
                            //    if (fullPath[i].X == midX && fullPath[i].Y == midY)
                            //    {
                            //        gv_workers++;
                            //    }
                            //    else
                            //    {
                            //        gv_cities++;
                            //    }
                            //}
                            //gv_workers--;
                            //gui_cities.Text = gv_cities.ToString();
                            //gui_workers.Text = gv_workers.ToString();
                            this.Refresh();
                        }
                    }

                }
                catch (System.IO.IOException ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. " + "Original error: " + ex.Message);

                }
            }
            if (fs != null)
            {
                fs.Close();
            }
            if (fr != null)
            {
                fr.Close();
            }
        }


        public class path_format
        {
            public string toString(Point[][] points)
            {
                String tourString = null;

                try
                {
                    for (int i = 0; i < points.Length; i++)
                    {
                        for (int j = 0; j < points[i].Length; j++)
                        {
                            if (j == points[i].Length - 1)
                            {
                                tourString += points[i][j].X.ToString() + "," + points[i][j].Y.ToString() + ";";
                            }
                            else
                            {
                                tourString += points[i][j].X.ToString() + "," + points[i][j].Y.ToString() + "|";
                            }
                        }
                    }
                }
                catch (System.IndexOutOfRangeException)
                {

                }

                return tourString;
            }

            public Point[][] toPoints(String tourString)
            {
                Point[][] fullPath = new Point[1][];
                fullPath[0] = new Point[0];
                string point = "";

                bool newPoint = true;
                int i = 0;
                int j = 0;

                while (newPoint)
                {
                    if (tourString.Length != 0)
                    {
                        try
                        {
                            char[] token = new char[1];
                            tourString.CopyTo(0, token, 0, 1);
                            tourString = tourString.Remove(0, 1);
                            if (token[0].ToString() == ",")
                            {
                                Array.Resize(ref fullPath[i], fullPath[i].Length + 1);
                                fullPath[i][j].X = int.Parse(point);
                                point = "";
                            }
                            else if (token[0].ToString() == "|")
                            {
                                fullPath[i][j].Y = int.Parse(point);
                                point = "";
                                j++;
                                //i++;
                                //Array.Resize(ref fullPath[i], i + 1);
                            }
                            else if (token[0].ToString() == ";")
                            {
                                fullPath[i][j].Y = int.Parse(point);
                                if (tourString.Length != 0)
                                {
                                    Array.Resize(ref fullPath, fullPath.Length + 1);
                                    fullPath[fullPath.Length - 1] = new Point[0];
                                    i++;
                                    point = "";
                                    j = 0;
                                }
                                else
                                {
                                    newPoint = false;
                                }
                            }
                            else
                            {
                                point += token[0].ToString();
                            }
                            //if (tourString.Length == 0)
                            //{
                            //    newPoint = false;
                            //}
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Problem beim Parsen vom String: " + ex.Message);
                        }
                    }
                }

                return fullPath;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            drawMode = true;
            paintPanel.Cursor = System.Windows.Forms.Cursors.Cross;
            button_generate.Enabled = false;
            button_next_tour.Enabled = true;
            draw_complete.Enabled = true;

        }

        private void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clear_all();
        }

        private void draw_complete_Click(object sender, EventArgs e)
        {
            gv_draw_counter = 0;
            button_next_tour.Enabled = false;
            draw_complete.Enabled = false;
        }

        private void button_next_tour_Click(object sender, EventArgs e)
        {
            gv_draw_counter++;
            Array.Resize(ref allTours, allTours.Length + 1);
            allTours[gv_draw_counter] = new Point[] { mid };
        }

    }
}
