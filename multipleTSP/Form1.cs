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
        public int[][] PiAllTours = new int[1][];         //##
        private Point mid = new Point(0,0);
        int gv_points = 0;
        int gv_tours = 0;
        int gv_draw_counter;
        int gv_draw_point = 0;
        double gv_totalLength = 0;
        double gv_avgLength = 0;

        int[][] positionMatrix = new int[0][];
        Point[] allPoints = new Point[1];
        double[][] distanceMatrix = new double[0][];

        Boolean drawMode = false;

        evaluation evaluate = new evaluation();

        public multipleTSP()
        {
            InitializeComponent();
            loclOptBox.SelectedIndex = 1;
            loclOptBoxAll.SelectedIndex = 1;
            paintPanel.Paint += new PaintEventHandler(paint);
            paintPanel.Click += new EventHandler(panel_click);
            paintPanel.Controls.Clear();

            mid.X = paintPanel.Width / 2;
            mid.Y = paintPanel.Height / 2;
            allTours[0] = new Point[] { mid };
            allPoints[0] = new Point(mid.X, mid.Y);

            gv_draw_counter = 0;
            gv_draw_point = 0;

            allPoints[0] = mid;
            PiAllTours[0] = new int[1]{0};

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
            Pen bluePen = new Pen(Color.Blue);

            //evaluation evaluate = new evaluation(allPoints, distanceMatrix);
            evaluate.allPoints = allPoints;
            evaluate.distanceMatrix = distanceMatrix;

            g.FillEllipse(redBrush, mid.X - 2, mid.Y - 2, 5, 5);

            for (int i = 0; i < allTours.Length; i++)
            {
                if (allTours[i].Length > 1)
                {
                    g.DrawPolygon(blackPen, allTours[i]);
                }
                gv_totalLength += evaluate.evalTour(allTours[i]);
            }

            for (int i = 0; i < PiAllTours.Length; i++)
            {
                if (PiAllTours[i].Length > 1)
                {
                    for (int j = 1; j < PiAllTours[i].Length; j++)
                    {
                        g.DrawLine(bluePen, allPoints[PiAllTours[i][j - 1]], allPoints[PiAllTours[i][j]]);
                    }
                    g.DrawLine(bluePen, allPoints[PiAllTours[i][PiAllTours[i].Length - 1]], mid);
                }
            }

            gv_totalLength = evaluate.evalAll(PiAllTours);

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
                gv_draw_point++;
                Array.Resize(ref allTours[gv_draw_counter], allTours[gv_draw_counter].Length + 1);
                allTours[gv_draw_counter][allTours[gv_draw_counter].Length - 1] = paintPanel.PointToClient(Cursor.Position);

                Array.Resize(ref PiAllTours[gv_draw_counter], PiAllTours[gv_draw_counter].Length + 1);  //##
                PiAllTours[gv_draw_counter][PiAllTours[gv_draw_counter].Length - 1] = gv_draw_point;    //##

                Array.Resize(ref allPoints, allPoints.Length + 1);
                allPoints[allPoints.Length - 1] = allTours[gv_draw_counter][allTours[gv_draw_counter].Length - 1];

                this.initMatrixes();

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

            Array.Resize(ref PiAllTours, 1);              //##
            Array.Resize(ref PiAllTours[0], 1);           //##
            PiAllTours[0] = new int[] { 0 };              //##

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
            gv_draw_point = 0;

            this.Refresh();
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
                    PiAllTours = newTour.PiAllTours;
                    evaluate.allPoints = allPoints;
                    this.initMatrixes();
                    evaluate.distanceMatrix = distanceMatrix;
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
            button_localOpt.Enabled = true;
            this.initMatrixes();
        }

        private void button_next_tour_Click(object sender, EventArgs e)
        {
            gv_draw_counter++;
            Array.Resize(ref allTours, allTours.Length + 1);
            allTours[gv_draw_counter] = new Point[] { mid };
            Array.Resize(ref PiAllTours, PiAllTours.Length + 1);                //##
            PiAllTours[PiAllTours.Length - 1] = new int[] { 0 };                //##

        }

        private void button_localOpt_Click(object sender, EventArgs e)
        {
            localOptimization localOpt = new localOptimization(allPoints, distanceMatrix);

            for (int i = 0; i < allTours.Length; i++)
            {
                Point[] newTour = new Point[allTours[i].Length];
                int[] PiNewTour = new int[PiAllTours[i].Length];
                if (loclOptBox.SelectedIndex == 0)      // no Optimization on single tours
                {
                    Array.Copy(allTours[i], newTour, allTours[i].Length);
                }
                else if (loclOptBox.SelectedIndex == 1) // 2-Opt
                {
                    newTour = localOpt.tour2opt(allTours[i]);
                    PiNewTour = localOpt.tour2opt(PiAllTours[i]);

                }
                else if (loclOptBox.SelectedIndex == 2) // OR-Opt (3, 2 and 1)
                {
                    newTour = localOpt.tourORopt(allTours[i], 3); // OR3-Opt
                    newTour = localOpt.tourORopt(allTours[i], 2); // OR2-Opt
                    newTour = localOpt.tourORopt(allTours[i], 1); // OR1-Opt
                }
                else if (loclOptBox.SelectedIndex == 3) // OR3-Opt
                {
                    newTour = localOpt.tourORopt(allTours[i], 3);
                }
                else if (loclOptBox.SelectedIndex == 4) // OR2-Opt
                {
                    newTour = localOpt.tourORopt(allTours[i], 2);
                }
                else if (loclOptBox.SelectedIndex == 5) // OR1-Opt
                {
                    newTour = localOpt.tourORopt(allTours[i], 1);
                }
                Array.Copy(newTour, allTours[i], allTours[i].Length);
                //allTours[i] = newTour;
            }
            if (loclOptBoxAll.SelectedIndex == 0)      // no optimization among tours
            {

            }
            else if (loclOptBoxAll.SelectedIndex == 1) // OR-Opt (3, 2 and 1)
            {
                allTours = localOpt.allORopt(allTours, 3);
                allTours = localOpt.allORopt(allTours, 2);
                allTours = localOpt.allORopt(allTours, 1);
                //for (int i = 0; i < allTours.Length; i++)
                //{
                //    allTours[i] = localOpt.tour2opt(allTours[i]);
                //}
            }
            else if (loclOptBoxAll.SelectedIndex == 2) // OR3-Opt 
            {
                allTours = localOpt.allORopt(allTours, 3);
            }
            else if (loclOptBoxAll.SelectedIndex == 3) // OR2-Opt
            {
                allTours = localOpt.allORopt(allTours, 2);
            }
            else if (loclOptBoxAll.SelectedIndex == 4) // OR1-Opt
            {
                allTours = localOpt.allORopt(allTours, 1);
            }

            this.Refresh();
        }

        private void initMatrixes()
        {
            // set matrix size 0 (clear matrix if not initial)
            //evaluation evaluate = new evaluation(allPoints, distanceMatrix);

            Array.Resize(ref distanceMatrix, 0);
            Array.Resize(ref positionMatrix, 0);

            for (int i = 0; i < allPoints.Length; i++)  //Abstands-Matrix
            {
                Array.Resize(ref distanceMatrix, distanceMatrix.Length + 1);
                distanceMatrix[i] = new double[0];
                for (int j = 0; j < allPoints.Length; j++)
                {
                    Array.Resize(ref distanceMatrix[i], distanceMatrix[i].Length + 1);
                    distanceMatrix[i][j] = evaluate.twoPoints(allPoints[i], allPoints[j]);
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
            //evaluation evaluate = new evaluation(allPoints, distanceMatrix);
            Array.Resize(ref positionMatrix[index], positionMatrix[index].Length + 1);
            for (int i = 0; i < positionMatrix[index].Length; i++)
            {
                if (positionMatrix[index].Length == 1)
                {
                    // initialize with first index
                    positionMatrix[index][i] = pointIndex;
                    return;
                }
                if (evaluate.twoPoints(allPoints[index], allPoints[pointIndex]) > evaluate.twoPoints(allPoints[index], allPoints[positionMatrix[index][i]]))
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
        public int[][] PiAllTours = new int[0][];

        public Point[][] random(int points, int tours, Point max, Point mid)
        {
            allPoints[0] = new Point(mid.X, mid.Y);
            Point[][] randomTour = new Point[0][];
            //int[][] PiAllTours = new int[0][];                                  //##
            int k = 0;
            Random pointRandom = new Random();

            for (int i = 0; i < tours; i++)
            {
                Array.Resize(ref randomTour, randomTour.Length + 1);
                Array.Resize(ref PiAllTours, PiAllTours.Length + 1);            //##
                randomTour[i] = new Point[1];
                randomTour[i][0] = mid;
                PiAllTours[i] = new int[1];                                     //##
                PiAllTours[i][0] = 0;                                           //##
                for (int j = 1; j <= points; j++)
                {
                    k++;                                                        //##
                    bool rnd = true;
                    Array.Resize(ref randomTour[i], randomTour[i].Length + 1);
                    Array.Resize(ref PiAllTours[i], PiAllTours[i].Length + 1);  //##
                    PiAllTours[i][j] = k;                                       //##
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
        private evaluation evaluate;
        private Point[] allPoints = new Point[0];
        private double[][] distanceMatrix = new double[0][];

        public localOptimization(Point[] points, double[][] distMatrix){
            Array.Resize(ref this.allPoints, points.Length);
            Array.Copy(points, this.allPoints, points.Length);

            Array.Resize(ref distanceMatrix, distMatrix.Length);
            for (int i = 0; i < distMatrix.Length; i++)
            {
                Array.Resize(ref distanceMatrix[i], distMatrix[i].Length);
                Array.Copy(distMatrix[i], distanceMatrix[i], distMatrix[i].Length);
            }

                evaluate = new evaluation(allPoints, distanceMatrix);
        }

        public int[] tour2opt(int[] PiSingleTour)
        {
            int[] newTour = new int[PiSingleTour.Length];
            Array.Copy(PiSingleTour, newTour, PiSingleTour.Length);

            bool optFound = true;
            bool loopBreak = false;

            while (optFound)
            {
                optFound = false;
                for (int i = 1; i < newTour.Length; i++)
                {
                    if (loopBreak)
                    {
                        loopBreak = false;
                        break;
                    }
                    for (int j = 1; j < newTour.Length; j++)
                    {
                        if (i != j)
                        {
                            newTour = invert(PiSingleTour, i, j);
                            if (evaluate.evalTour(newTour) < evaluate.evalTour(PiSingleTour))
                            {
                                Array.Copy(newTour, PiSingleTour, newTour.Length);
                                //loopBreak = true;
                                optFound = true;
                                //break;
                            }
                            else
                            {
                                Array.Copy(PiSingleTour, newTour, newTour.Length);
                            }
                            
                        }
                    }
                }
            }

            return PiSingleTour;
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
                    if (breakLoop)
                    {
                        breakLoop = false;
                        break;
                    }
                    for (int j = 1; j < newTour.Length; j++)
                    {
                        if (i != j)
                        {
                            newTour = invert(singleTour, i, j);
                            double evalNew = evaluate.evalTour(newTour);
                            double evalOld = evaluate.evalTour(singleTour);
                            if (evalNew < evalOld)
                            {
                                opt_found = true;
                                //breakLoop = true;
                                Array.Copy(newTour, singleTour, singleTour.Length);
                                //break;
                            }
                            else
                            {
                                Array.Copy(singleTour, newTour, singleTour.Length);
                            }
                        }
                    }
                }
            }

            return singleTour;
        }

        public int[] invert(int[] singleTour, int indexA, int indexB)
        {
            // invert string between indexA and indexB (including)
            int[] newTour = new int[singleTour.Length];
            Array.Copy(singleTour, newTour, singleTour.Length);
            int[] extract = new int[0];
            if (indexA < indexB)
            {
                Array.Resize(ref extract, indexB - indexA + 1);
                Array.Copy(singleTour, indexA, extract, 0, extract.Length);
                Array.Reverse(extract);
                Array.Copy(extract, 0, newTour, indexA, extract.Length);
            }
            else
            {
                Array.Resize(ref extract, indexA - indexB + 1);
                Array.Copy(singleTour, indexB, extract, 0, extract.Length);
                Array.Reverse(extract);
                Array.Copy(extract, 0, newTour, indexB, extract.Length);
            }

            return newTour;
        }

        public Point[] invert(Point[] singleTour, int indexA, int indexB)
        {
            // invert string between indexA and indexB (including)
            Point[] newTour = new Point[singleTour.Length];
            Array.Copy(singleTour, newTour, singleTour.Length);
            Point[] extract = new Point[0];
            if (indexA < indexB)
            {
                Array.Resize(ref extract, indexB - indexA + 1);
                Array.Copy(singleTour, indexA, extract, 0, extract.Length);
                Array.Reverse(extract);
                Array.Copy(extract, 0, newTour, indexA, extract.Length);
            }
            else
            {
                Array.Resize(ref extract, indexA - indexB + 1);
                Array.Copy(singleTour, indexB, extract, 0, extract.Length);
                Array.Reverse(extract);
                Array.Copy(extract, 0, newTour, indexB, extract.Length);
            }
            return newTour;
        }

        public Point[] tourORopt(Point[] singleTour, int extractLength)
        {
            Point[] newTour = new Point[singleTour.Length];
            Array.Copy(singleTour, newTour, singleTour.Length);

            bool optFound = true;
            bool loopbreak = false;

            while (optFound)
            {
                optFound = false;
                for (int i = 1; i < singleTour.Length - (extractLength -1); i++) // extractLength-1, because we dont want outOfRange exception
                                                                // cannot start at 0, because mid is at 0
                {
                    if (loopbreak)
                    {
                        loopbreak = false;
                        break;
                    }
                    Point[] extract = new Point[extractLength];
                    Array.Copy(singleTour, i, extract, 0, extractLength); // Length 3, because OR3
                    for (int j = 1; j < singleTour.Length - (extractLength - 1); j++)
                    {
                        if (i == j)
                        {
                            //do nothing (insert would be at same position)
                        }
                        else
                        {
                            newTour = insertExtract(singleTour, extract, i, j);
                            if (evaluate.evalTour(newTour) < evaluate.evalTour(singleTour))
                            {
                                Array.Copy(newTour, singleTour, singleTour.Length);
                                optFound = true;
                                loopbreak = true;
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
            return singleTour;
        }

        public int[] tourORopt(int[] piTour, int extractLength)
        {

            return piTour;
        }

        public Point[][] crossedORopt(Point[][] allTours, int indexA, int indexB, int length)
        {
            Point[][] newTours = new Point[allTours.Length][];
            for (int i = 0; i < allTours.Length; i++)
            {
                Array.Resize(ref newTours[i], allTours[i].Length);
                Array.Copy(allTours[i], newTours[i], allTours[i].Length);
            }

            bool optFound = true;
            bool loopbreak = false;

            while (optFound)
            {
                optFound = false;
                for (int i = 1; i < newTours[indexA].Length - (length - 1); i++)
                {
                    if (loopbreak)
                    {
                        loopbreak = false;
                        break;
                    }
                    Point[] extract = new Point[length];
                    Array.Copy(newTours[indexA], i, extract, 0, length);

                    for (int j = 1; j < newTours[indexB].Length - (length - 1); j++)
                    {
                        Array.Copy(newTours[indexB], j, newTours[indexA], i, length);
                        Array.Copy(extract, 0, newTours[indexB], j, length);
                        //newTours[indexA][i] = newTours[indexB][j];
                        //newTours[indexB][j] = dummy;

                        if (evaluate.evalAll(newTours) < evaluate.evalAll(allTours))
                        {
                            for (int k = 0; k < newTours.Length; k++)
                            {
                                Array.Copy(newTours[k], allTours[k], allTours[k].Length);
                            }
                            optFound = true;
                            loopbreak = true;
                            break;
                        }
                        else
                        {
                            for (int k = 0; k < allTours.Length; k++)
                            {
                                Array.Copy(allTours[k], newTours[k], allTours[k].Length);
                            }
                        }
                    }
                }
            }
            return allTours;
        }

        public Point[][] allORopt(Point[][] allTours, int extractLength)
        {
            for (int i = 0; i < allTours.Length; i++)
            {
                for (int j = 0; j < allTours.Length; j++)
                {
                    if (i != j)
                    {
                        allTours = crossedORopt(allTours, i, j, extractLength);
                    }
                }
            }
            return allTours;
        }

        public Point[] insertExtract(Point[] toBeAltered, Point[] extract, int oldIndex, int index)
        {
            Point[] inserted = new Point[toBeAltered.Length];
            Point[] extracted = new Point[toBeAltered.Length - extract.Length];
            //extracting
            Array.Copy(toBeAltered, 0, extracted, 0, oldIndex);
            Array.Copy(toBeAltered, oldIndex + extract.Length, extracted, oldIndex, toBeAltered.Length - extract.Length - oldIndex);
            //extract removed: array smaller ("extracted")
            //insert extract at "index" (this is the new position)
            Array.Copy(extracted, 0, inserted, 0, index);
            Array.Copy(extract, 0, inserted, index, extract.Length);
            Array.Copy(extracted, index, inserted, index + extract.Length, inserted.Length - extract.Length - index);

            return inserted;
        }
    }

    public class evaluation
    {
        public Point[] allPoints;
        public double[][] distanceMatrix;

        public evaluation()
        {
        }

        public evaluation(Point[] Points, double[][] distMatrix)
        {
            Array.Resize(ref this.allPoints, Points.Length);
            Array.Copy(Points, this.allPoints, Points.Length);

            Array.Resize(ref this.distanceMatrix, distMatrix.Length);
            for (int i = 0; i < distanceMatrix.Length; i++)
            {
                Array.Resize(ref distanceMatrix[i], distMatrix[i].Length);
                Array.Copy(distMatrix[i], distanceMatrix[i], distMatrix[i].Length);
            }
        }

        public double evalTour(Point[] singleTour)
        {
            double length = 0;

            for (int i = 0; i < singleTour.Length - 1; i++)
            {
                length += Math.Sqrt(Math.Pow(singleTour[i].X - singleTour[i + 1].X, 2) + Math.Pow(singleTour[i].Y - singleTour[i + 1].Y, 2));
            }

                length += Math.Sqrt(Math.Pow(singleTour[singleTour.Length - 1].X - singleTour[0].X, 2) + Math.Pow(singleTour[singleTour.Length - 1].Y - singleTour[0].Y, 2));

                return length;
        }

        public double evalTour(int[] singleTour)
        {
            double length = 0;

            if (singleTour.Length > 1)
            {
                for (int i = 1; i < singleTour.Length; i++)
                {
                    length += distanceMatrix[singleTour[i - 1]][singleTour[i]];             //all points from start to last
                }
                length += distanceMatrix[singleTour[singleTour.Length - 1]][0]; //back to mid
            }
            return length;
        }

        public double evalAll(Point[][] allTours)
        {
            double length = 0;
            for (int i = 0; i < allTours.Length; i++)
            {
                length += evalTour(allTours[i]);
            }
            return length;
        }

        public double evalAll(int[][] PiAllTours)
        {
            double length = 0;
            for (int i = 0; i < PiAllTours.Length; i++)
            {
                length += evalTour(PiAllTours[i]);
            }
            return length;
        }

        public double twoPoints(Point pointA, Point pointB)
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
