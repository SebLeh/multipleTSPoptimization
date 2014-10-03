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

        int[][] positionMatrix = new int[0][];
        Point[] allPoints = new Point[1];
        double[][] distanceMatrix = new double[0][];

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
            allPoints[0] = new Point(mid.X, mid.Y);

            gv_draw_counter = 0;

            allPoints[0] = mid;

        }

        public void paint(object sender, PaintEventArgs e)
        {
            gv_totalLength = 0;
            gv_avgLength = 0;
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
                gv_totalLength += evaluation.evalTour(allTours[i]);
            }
            gv_avgLength = gv_totalLength / gv_tours;
            if (gv_totalLength != 0)
            {
                gui_avgLength.Text = gv_avgLength.ToString();
            }
            gui_totalLength.Text = gv_totalLength.ToString();

        }

        public void panel_click(object sender, EventArgs e)
        {
            if (drawMode == true)
            {
                Array.Resize(ref allTours[gv_draw_counter], allTours[gv_draw_counter].Length + 1);
                allTours[gv_draw_counter][allTours[gv_draw_counter].Length - 1] = paintPanel.PointToClient(Cursor.Position);

                Array.Resize(ref allPoints, allPoints.Length + 1);
                allPoints[allPoints.Length - 1] = allTours[gv_draw_counter][allTours[gv_draw_counter].Length - 1];

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
            draw_button.Enabled = true;
            button_localOpt.Enabled = false;
            Array.Resize(ref allTours, 0);
            this.Refresh();

            Array.Resize(ref allTours, 1);
            Array.Resize(ref allTours[0], 1);
            Array.Resize(ref positionMatrix, 0);
            //Array.Resize(ref positionMatrix[0], 1);
            Array.Resize(ref distanceMatrix, 0);
            //Array.Resize(ref distanceMatrix[0], 1);
            Array.Resize(ref allPoints, 1);
            //Array.Resize(ref points[0], 0);
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
            draw_button.Enabled = false;
            paintPanel.Cursor = System.Windows.Forms.Cursors.Arrow;
            generate newTour = new generate();
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
                    allTours = newTour.random(gv_points, gv_tours, new Point(paintPanel.Width, paintPanel.Height), mid);
                    allPoints = newTour.allPoints;
                    this.initMatrixes();
                    this.Refresh();
                }
                button_localOpt.Enabled = true;
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
            paintPanel.Cursor = System.Windows.Forms.Cursors.Default;
            button_next_tour.Enabled = false;
            draw_complete.Enabled = false;
            this.initMatrixes();
        }

        private void button_next_tour_Click(object sender, EventArgs e)
        {
            gv_draw_counter++;
            Array.Resize(ref allTours, allTours.Length + 1);
            allTours[gv_draw_counter] = new Point[] { mid };
        }

        private void button_localOpt_Click(object sender, EventArgs e)
        {
            localOptimization localOpt = new localOptimization(gv_points);
            for (int i = 0; i < allTours.Length; i++)
            {
                Point[] newTour = localOpt.tour2opt(allTours[i]);
                //if (evaluation.evalTour(newTour) < evaluation.evalTour(allTours[i]))
                //{
                allTours[i] = newTour;
                //}
            }
            this.Refresh();
        }

        private void initMatrixes()
        {
            // set matrix size 0 (clear matrix if not initial)
            Array.Resize(ref distanceMatrix, 0);
            Array.Resize(ref positionMatrix, 0);

            for (int i = 0; i < allPoints.Length; i++)  //Abstands-Matrix
            {
                Array.Resize(ref distanceMatrix, distanceMatrix.Length + 1);
                distanceMatrix[i] = new double[0];
                for (int j = 0; j < allPoints.Length; j++)
                {
                    Array.Resize(ref distanceMatrix[i], distanceMatrix[i].Length + 1);
                    distanceMatrix[i][j] = evaluation.twoPoints(allPoints[i], allPoints[j]);
                }
            }

            for (int i = 0; i < allPoints.Length; i++)  //Rang-Matrix
            {
                Array.Resize(ref positionMatrix, positionMatrix.Length + 1);
                positionMatrix[i] = new int[0];
                for (int j = 0; j < allPoints.Length; j++)
                {
                    if (i != j)
                    {
                        insertPosition(i, j);
                    }
                }
            }
        }

        private void insertPosition(int index, int pointIndex)
        {
            Array.Resize(ref positionMatrix[index], positionMatrix[index].Length + 1);
            for (int i = 0; i < positionMatrix[index].Length; i++)
            {
                if (positionMatrix[index].Length == 1)
                {
                    // initialize with first index
                    positionMatrix[index][i] = pointIndex;
                    return;
                }
                if (evaluation.twoPoints(allPoints[index], allPoints[pointIndex]) > evaluation.twoPoints(allPoints[index], allPoints[positionMatrix[index][i]]))
                {

                }
                else
                //if (evaluation.twoPoints(allPoints[index], allPoints[positionMatrix[index][i]]) >= evaluation.twoPoints(allPoints[index], allPoints[pointIndex]))
                {
                    // now insert at correct position: i-1
                    int[] dummy = new int[positionMatrix[index].Length - (i + 1)];
                    Array.Copy(positionMatrix[index], i, dummy, 0, dummy.Length);
                    positionMatrix[index][i] = pointIndex;
                    Array.Copy(dummy, 0, positionMatrix[index], i + 1, dummy.Length);
                    return;
                }
                if ( (positionMatrix[index].Length - 1) == i)
                {
                    positionMatrix[index][i] = pointIndex;
                    return;
                }
            }
        }
    }

    public class generate
    {
        public Point[] allPoints = new Point[1];

        public Point[][] random(int points, int tours, Point max, Point mid)
        {
            allPoints[0] = new Point(mid.X, mid.Y);
            Point[][] randomTour = new Point[0][];
            Random pointRandom = new Random();

            for (int i = 0; i < tours; i++)
            {
                Array.Resize(ref randomTour, randomTour.Length + 1);
                randomTour[i] = new Point[1];
                randomTour[i][0] = mid;
                for (int j = 1; j <= points; j++)
                {
                    bool rnd = true;
                    Array.Resize(ref randomTour[i], randomTour[i].Length + 1);
                    while (rnd)
                    {
                        rnd = false;
                        Point newPoint = new Point();
                        newPoint.X = pointRandom.Next(0, max.X);
                        newPoint.Y = pointRandom.Next(0, max.Y);
                        if (newPoint == mid)
                        {
                            rnd = true;
                        }
                        if (contains(randomTour, newPoint))
                        {
                            rnd = true;
                        }
                        if ( !rnd )
                        {
                            randomTour[i][j] = newPoint;
                            Array.Resize(ref allPoints, allPoints.Length + 1);
                            allPoints[allPoints.Length -1] = newPoint;
                        }
                    }
                }
            }
            
                return randomTour;
        }

        private bool contains(Point[][] newTour, Point newPoint)
        {
            for (int i = 0; i < newTour.Length; i++)
            {
                for (int j = 0; j < newTour[i].Length; j++)
                {
                    if (newTour[i][j] == newPoint)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }

    public class localOptimization
    {
        int points;
        public localOptimization(int points){
            this.points = points;
        }

        public Point[] tour2opt(Point[] singleTour)
        {
            Point[] newTour = new Point[singleTour.Length];
            //newTour = singleTour;
            Array.Copy(singleTour, newTour, singleTour.Length);
            bool opt_found = true;
            bool breakLoop = false;

            while (opt_found)
            {
                opt_found = false;

                for (int i = 1; i < newTour.Length; i++)
                {
                    Point dummy = new Point();
                    dummy = newTour[i];
                    if (breakLoop)
                    {
                        breakLoop = false;
                        break;
                    }
                    for (int j = 1; j < newTour.Length; j++)
                    {
                        if (i != j)
                        {
                            newTour[i] = newTour[j];
                            newTour[j] = dummy;
                            double evalNew = 0;
                            double evalOld = 0;
                            evalNew = evaluation.evalTour(newTour);
                            evalOld = evaluation.evalTour(singleTour);
                            if (evalNew < evalOld)
                            {
                                opt_found = true;
                                breakLoop = true;
                                Array.Copy(newTour, singleTour, singleTour.Length);
                                break;
                            }
                            else
                            {
                                Array.Copy(singleTour, newTour, singleTour.Length);
                            }
                        }
                    }
                }
            }

            //int k = 0;
            //while (k < ( points * 10 ))
            //{
            //    //for (int i = 0; i < singleTour.Length; i++)
            //    //{
            //    Point[] newTour = newNeighbor2opt(singleTour);
            //    double eval_new = evaluation.evalTour(newTour);
            //    double eval_old = evaluation.evalTour(singleTour);
            //    if (eval_new < eval_old)
            //    {
            //        singleTour = newTour;
            //        k = 0;
            //        //break;
            //    }
            //    else 
            //    {
            //        k++;
            //    }
            //    //}
            //}

            return singleTour;
        }

        //public Point[] newNeighbor2opt (Point[] singleTour)
        //{
            //Point[] newTour = new Point[singleTour.Length];
            
                //int rndIndexA = 0;
                //int rndIndexB = 0;
                //Random intRandom = new Random();

                //Array.Copy(singleTour, newTour, singleTour.Length);
                //rndIndexA = intRandom.Next(0, singleTour.Length - 1);

                //bool rnd = true;
                //while (rnd)
                //{
                //    rnd = false;
                //    rndIndexB = intRandom.Next(0, singleTour.Length - 1);
                //    if (rndIndexA == rndIndexB)
                //    {
                //        rnd = true;
                //    }
                //    if (rndIndexA + 1 == rndIndexB | rndIndexA - 1 == rndIndexB)
                //    {
                //        rnd = true;
                //    }
                //}
                ////now invert array between A+1 and B-1 / B+1 and A-1
                //if (rndIndexA < rndIndexB)
                //{
                //    Array.Reverse(newTour, rndIndexA + 1, (rndIndexB - rndIndexA - 1));
                //}
                //else
                //{
                //    Array.Reverse(newTour, rndIndexB + 1, (rndIndexA - rndIndexB - 1));
                //}

                //return newTour;
        //}

        //public Point[] newNeighbor3opt(Point[] singleTour)
        //{
        //    Point[] newTour = new Point[singleTour.Length];

        //    return newTour;
        //}

        //public bool isBetter(Point[] oldTour, Point[] newTour)
        //{

        //    return true;
        //}
    }

    public class evaluation
    {
        public static double evalTour(Point[] singleTour)
        {
            double length = 0;

            for (int i = 0; i < singleTour.Length - 1; i++)
            {
                length += Math.Sqrt(Math.Pow(singleTour[i].X - singleTour[i + 1].X, 2) + Math.Pow(singleTour[i].Y - singleTour[i + 1].Y, 2));
            }

                length += Math.Sqrt(Math.Pow(singleTour[singleTour.Length - 1].X - singleTour[0].X, 2) + Math.Pow(singleTour[singleTour.Length - 1].Y - singleTour[0].Y, 2));

                return length;
        }

        public static double twoPoints(Point pointA, Point pointB)
        {
            double distance = 0;

            if (pointA == pointB)
            {
                return 0;
            }

            distance = Math.Sqrt(Math.Pow(pointA.X - pointB.X, 2) + Math.Pow(pointA.Y - pointB.Y, 2));

            return distance;
        }
    }
}
