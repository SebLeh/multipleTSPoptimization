using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

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

        double insertPercentage = 0;

        int popSize = 0;
        int popGrowth = 0;
        int generations = 0;
        int bombSize = 0;

        int[][] positionMatrix = new int[0][];
        Point[] allPoints = new Point[1];
        double[][] distanceMatrix = new double[0][];

        double[] bestIndividuals = new double[0];
        double[] worstIndividuals = new double[0];
        int[] bestArray = new int[0];
        int[] worstArray = new int[0];

        Boolean drawMode = false;

        evaluation evaluate = new evaluation();

        evoOpt evoOptimization;

        public multipleTSP()
        {
            InitializeComponent();
            loclOptBox.SelectedIndex = 1;
            loclOptBoxAll.SelectedIndex = 0;
            generateBox.SelectedIndex = 0;
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

            this.chart1.Palette = ChartColorPalette.SeaGreen;
            this.chart1.Titles.Add("Improvement of best and worst Individuals");
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

            Pen[] pens = new Pen[PiAllTours.Length];

            //evaluation evaluate = new evaluation(allPoints, distanceMatrix);
            evaluate.allPoints = allPoints;
            evaluate.distanceMatrix = distanceMatrix;

            g.FillEllipse(blackBrush, mid.X - 2, mid.Y - 2, 5, 5);

            if (PiAllTours.Length < 10)
            {
                pens[0] = blackPen;
                if (PiAllTours.Length > 1)
                {
                    pens[1] = bluePen;

                }
                if (PiAllTours.Length > 2)
                {
                    pens[2] = new Pen(Color.Red);
                }
                if (PiAllTours.Length > 3)
                {
                    pens[3] = new Pen(Color.Green);
                }
                if (PiAllTours.Length > 4)
                {
                    pens[4] = new Pen(Color.Yellow);
                }
                if (PiAllTours.Length > 5)
                {
                    pens[5] = new Pen(Color.Pink);
                }
                if (PiAllTours.Length > 6)
                {
                    pens[6] = new Pen(Color.Violet);
                }
                if (PiAllTours.Length > 7)
                {
                    pens[7] = new Pen(Color.Cyan);
                }
                if (PiAllTours.Length > 8)
                {
                    pens[8] = new Pen(Color.LightGreen);
                } 
                if (PiAllTours.Length > 9)
                {
                    pens[9] = new Pen(Color.Brown);
                }

            }
            else
            {
                for (int i = 0; i < PiAllTours.Length; i++)
                {
                    pens[i] = blackPen;
                }
            }

            for (int i = 0; i < PiAllTours.Length; i++)
            {
                if (PiAllTours[i].Length > 1)
                {
                    for (int j = 1; j < PiAllTours[i].Length; j++)
                    {
                        g.DrawLine(pens[i], allPoints[PiAllTours[i][j - 1]], allPoints[PiAllTours[i][j]]);
                    }
                    g.DrawLine(pens[i], allPoints[PiAllTours[i][PiAllTours[i].Length - 1]], mid);
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
                //Cursor paintCursor = new System.Windows.Forms.Cursor(paintPanel);

                gv_draw_point++;
                Array.Resize(ref allTours[gv_draw_counter], allTours[gv_draw_counter].Length + 1);
                //allTours[gv_draw_counter][allTours[gv_draw_counter].Length - 1] = paintPanel.PointToClient(Cursor.Position);

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
            button_insert.Enabled = false;
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

            Array.Resize(ref bestArray, 0);
            Array.Resize(ref bestIndividuals, 0);
            Array.Resize(ref worstArray, 0);
            Array.Resize(ref worstIndividuals, 0);

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
                        //fw.Write(format.toString(allTours));
                        fw.Write(format.toString(PiAllTours, allPoints));
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
            button_greedy.Enabled = true;
            draw_button.Enabled = false;
            button_insert.Enabled = true;
            button_loclCompl.Enabled = true;
            button_evoStart.Enabled = true;
            paintPanel.Cursor = System.Windows.Forms.Cursors.Arrow;
            generate newTour = new generate();
            try
            {
                gv_points = int.Parse(gui_points.Text);
                gv_tours = int.Parse(gui_tours.Text);                
            }
            catch (SystemException)
            {
                MessageBox.Show("Please only insert positive Numbers!");
                return;
            }
            if (gv_tours <= 0 || gv_points <= 0)
            {
                MessageBox.Show("Please only insert positive Numbers!");
                return;
            }

            if (gv_tours > gv_points)
            {
                MessageBox.Show("Numbers of Points should be higher or equal to the number of Tours");
                return;
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

                if (generateBox.SelectedIndex == 1)
                {
                    PiAllTours = newTour.greedyAlt(positionMatrix);
                }
                else if (generateBox.SelectedIndex == 2)
                {
                    PiAllTours = newTour.greedyCon(positionMatrix);
                }
                else if (generateBox.SelectedIndex == 3)
                {
                    PiAllTours = newTour.radial();
                }

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
                            //allTours = format.toPoints(pathString);
                            format.toPoints(pathString);
                            allPoints = format.allPoints;
                            PiAllTours = format.PiAllTours;
                            gv_tours = PiAllTours.Length;
                            gv_points = PiAllTours[0].Length - 1;
                            gui_points.Text = gv_tours.ToString();
                            gui_points.Text = gv_points.ToString();
                            this.initMatrixes();
                            this.button_localOpt.Enabled = true;
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
            public int[][] PiAllTours = new int[1][];
            public Point[] allPoints = new Point[0];

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
            public string toString(int[][] points, Point[] allPoints)
            {
                string tourString = null;
                try
                {
                    // write allPoints
                    for (int i = 0; i < allPoints.Length; i++)
                    {
                        if (i == allPoints.Length - 1)
                        {
                            tourString += allPoints[i].X.ToString() + "|" + allPoints[i].Y.ToString() + ";";
                        }
                        else
                        {
                            tourString += allPoints[i].X.ToString() + "|" + allPoints[i].Y.ToString() + ",";
                        }
                    }
                    //tourString += Environment.NewLine;

                    for (int i = 0; i < points.Length; i++)
                    {
                        for (int j = 0; j < points[i].Length; j++)
                        {
                            if (j == points[i].Length - 1)
                            {
                                tourString += points[i][j].ToString() + "_";
                            }
                            else
                            {
                                tourString += points[i][j].ToString() + "-";
                            }
                        }
                    }
                }
                catch (System.IndexOutOfRangeException)
                {

                }
                return tourString;
            }

            public void toPoints(string tourString)
            {
                char[] token = new char[1];
                //int number;
                string word = null;
                bool letterLeft = true;
                bool step2 = false;
                
                while (letterLeft)
                {
                    letterLeft = false;
                    //try 
                    //{
                        //if (tourString.Length > 0)
                        //{
                    tourString.CopyTo(0, token, 0, 1);
                    tourString = tourString.Remove(0, 1);
                    if (tourString.Length > 0)
                    {
                        letterLeft = true;
                    }

                    if (token[0].ToString() == "," && !step2)
                    {
                        allPoints[allPoints.Length - 1].Y = int.Parse(word);
                        word = null;
                    }
                    else if (token[0].ToString() == "|" && !step2)
                    {
                        Array.Resize(ref allPoints, allPoints.Length + 1);
                        allPoints[allPoints.Length - 1].X = int.Parse(word);
                        word = null;
                    }
                    else if (token[0].ToString() == "-" && step2)
                    {
                        Array.Resize(ref PiAllTours[PiAllTours.Length - 1], PiAllTours[PiAllTours.Length - 1].Length + 1);
                        PiAllTours[PiAllTours.Length - 1][PiAllTours[PiAllTours.Length - 1].Length - 1] = int.Parse(word);
                        word = null;
                    }

                    else if (token[0].ToString() == "_" && step2)
                    {
                        Array.Resize(ref PiAllTours[PiAllTours.Length - 1], PiAllTours[PiAllTours.Length - 1].Length + 1);
                        PiAllTours[PiAllTours.Length - 1][PiAllTours[PiAllTours.Length - 1].Length - 1] = int.Parse(word);
                        if ( tourString.Length > 1)
                        {
                            Array.Resize(ref PiAllTours, PiAllTours.Length + 1);
                            PiAllTours[PiAllTours.Length - 1] = new int[0];
                        }
                        word = null;
                    }

                    else if (token[0].ToString() == ";" && !step2)
                    {
                        allPoints[allPoints.Length - 1].Y = int.Parse(word);
                        PiAllTours[PiAllTours.Length - 1] = new int[0];
                        step2 = true;
                        word = null;
                    }
                    else
                    {
                        word += token[0].ToString();
                    }
                }
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
            Array.Resize(ref PiAllTours, PiAllTours.Length + 1);
            PiAllTours[PiAllTours.Length - 1] = new int[] { 0 };
            gv_tours = gv_draw_counter + 1;

        }

        private void button_localOpt_Click(object sender, EventArgs e)
        {
            localOptimization localOpt = new localOptimization(allPoints, distanceMatrix, positionMatrix);

            for (int i = 0; i < allTours.Length; i++)
            {
                Point[] newTour = new Point[allTours[i].Length];
                int[] PiNewTour = new int[PiAllTours[i].Length];
                if (loclOptBox.SelectedIndex == 0)      // no Optimization on single tours
                {
                    Array.Copy(PiAllTours[i], PiNewTour, PiAllTours[i].Length);
                }
                else if (loclOptBox.SelectedIndex == 1) // 2-Opt
                {
                    PiNewTour = localOpt.tour2opt(PiAllTours[i]);

                }
                else if (loclOptBox.SelectedIndex == 2) // OR-Opt (3, 2 and 1)
                {
                    PiNewTour = localOpt.tourORopt(PiAllTours[i], 3);
                    PiNewTour = localOpt.tourORopt(PiAllTours[i], 2);
                    PiNewTour = localOpt.tourORopt(PiAllTours[i], 1);
                }
                else if (loclOptBox.SelectedIndex == 3) // OR3-Opt
                {
                    PiNewTour = localOpt.tourORopt(PiAllTours[i], 3);
                }
                else if (loclOptBox.SelectedIndex == 4) // OR2-Opt
                {
                    PiNewTour = localOpt.tourORopt(PiAllTours[i], 2);
                }
                else if (loclOptBox.SelectedIndex == 5) // OR1-Opt
                {
                    PiNewTour = localOpt.tourORopt(PiAllTours[i], 1);
                }
                Array.Copy(newTour, allTours[i], allTours[i].Length);
                //allTours[i] = newTour;
            }
            if (loclOptBoxAll.SelectedIndex == 0)      // no optimization among tours
            {

            }
            else if (loclOptBoxAll.SelectedIndex == 1) // OR-Opt (3, 2 and 1)
            {
                PiAllTours = localOpt.allORopt(PiAllTours, 3, cb_neigh.Checked);
                PiAllTours = localOpt.allORopt(PiAllTours, 2, cb_neigh.Checked);
                PiAllTours = localOpt.allORopt(PiAllTours, 1, cb_neigh.Checked);
                if (loclOptBox.SelectedIndex == 1) // if both are chosen, 2-opt after opt among tours
                {
                    for (int i = 0; i < PiAllTours.Length; i++)
                    {
                        int[] newTour = localOpt.tour2opt(PiAllTours[i]);
                        Array.Copy(newTour, PiAllTours[i], PiAllTours[i].Length);
                    }
                }
            }
            else if (loclOptBoxAll.SelectedIndex == 2) // OR3-Opt 
            {
                PiAllTours = localOpt.allORopt(PiAllTours, 3, cb_neigh.Checked);
                if (loclOptBox.SelectedIndex == 1) // if both are chosen, 2-opt after opt among tours
                {
                    for (int i = 0; i < PiAllTours.Length; i++)
                    {
                        int[] newTour = localOpt.tour2opt(PiAllTours[i]);
                        Array.Copy(newTour, PiAllTours[i], PiAllTours[i].Length);
                    }
                }
            }
            else if (loclOptBoxAll.SelectedIndex == 3) // OR2-Opt
            {
                PiAllTours = localOpt.allORopt(PiAllTours, 2, cb_neigh.Checked);
                if (loclOptBox.SelectedIndex == 1) // if both are chosen, 2-opt after opt among tours
                {
                    for (int i = 0; i < PiAllTours.Length; i++)
                    {
                        int[] newTour = localOpt.tour2opt(PiAllTours[i]);
                        Array.Copy(newTour, PiAllTours[i], PiAllTours[i].Length);
                    }
                }
            }
            else if (loclOptBoxAll.SelectedIndex == 4) // OR1-Opt
            {
                PiAllTours = localOpt.allORopt(PiAllTours, 1, cb_neigh.Checked);
                if (loclOptBox.SelectedIndex == 1) // if both are chosen, 2-opt after opt among tours
                {
                    for (int i = 0; i < PiAllTours.Length; i++)
                    {
                        int[] newTour = localOpt.tour2opt(PiAllTours[i]);
                        Array.Copy(newTour, PiAllTours[i], PiAllTours[i].Length);
                    }
                }
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

        private void button_insert_Click(object sender, EventArgs e)
        {
            bool optFound = true;
            int[][] newTours = new int[PiAllTours.Length][];
            localOptimization loclOpt = new localOptimization(allPoints, distanceMatrix, positionMatrix);
            try
            {
                insertPercentage = double.Parse(tb_insert_percent.Text);
            }

            catch (System.Exception)
            {
                MessageBox.Show("Please only insert positive numbers");
                return;
            }
            while (optFound)
            {
                optFound = false;
                for (int i = 0; i < PiAllTours.Length; i++)
                {
                    for (int j = 0; j < PiAllTours.Length; j++)
                    {
                        if (i != j)
                        {
                            newTours = loclOpt.insert(PiAllTours, i, j, insertPercentage);
                            for (int k = 0; k < PiAllTours.Length; k++)
                            {
                                loclOpt.tour2opt(newTours[k]);
                            }
                            if (evaluate.evalAll(newTours) < evaluate.evalAll(PiAllTours))
                            {
                                optFound = true;
                                for (int l = 0; l < newTours.Length; l++)
                                {
                                    Array.Copy(newTours[l], PiAllTours[l], newTours[l].Length);
                                }

                            }
                        }
                    }
                }                
            }
            this.Refresh();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            generate newTour = new generate();
            newTour.allPoints = allPoints;
            newTour.PiAllTours = PiAllTours;

            if (generateBox.SelectedIndex == 1)
            {
                PiAllTours = newTour.greedyAlt(positionMatrix);
            }
            else if (generateBox.SelectedIndex == 2)
            {
                PiAllTours = newTour.greedyCon(positionMatrix);
            }
            else if (generateBox.SelectedIndex == 3)
            {
                PiAllTours = newTour.radial();
            }

            this.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            localOptimization localOpt = new localOptimization(allPoints, distanceMatrix, positionMatrix);

            PiAllTours = localOpt.fullyOpt(PiAllTours);
            this.Refresh();
        }

        private void button_evoStart_Click(object sender, EventArgs e)
        {
            try
            {
                popSize = int.Parse(ui_popSize.Text);
                popGrowth = int.Parse(ui_popGrowth.Text);
                generations = int.Parse(ui_generations.Text);
                bombSize = int.Parse(ui_bombSize.Text);
            }
            catch (System.Exception)
            {
                MessageBox.Show("Please only insert positive numbers!");
                return;
            }
            if (popGrowth < popSize)
            {
                MessageBox.Show("Number of Offsprings (Growth) must be higher than or equal to the Population Size");
                return;
            }
            double initialLength = gv_totalLength;
            evoOptimization = new evoOpt(allPoints, distanceMatrix, positionMatrix);
            evoOptimization.initPopulation(PiAllTours, popSize);
            evoOptimization.evoRun(popGrowth, cb_evoStrategy.SelectedIndex, bombSize, generations);

            PiAllTours = evoOptimization.getBest();
            this.bestIndividuals = evoOptimization.bestIndividuals;
            this.worstIndividuals = evoOptimization.worstIndividuals;
            
            this.chart1.Series.Clear();
            //string[] bestSeries = { "Best", "Worst" };
            int bestLength = bestArray.Length;
            int worstLength = worstArray.Length;
            Array.Resize(ref bestArray, bestArray.Length + bestIndividuals.Length);
            Array.Resize(ref worstArray, worstArray.Length + worstIndividuals.Length);
            bestIndividuals[0] = initialLength;
            worstIndividuals[0] = initialLength;
            for (int i = 1; i < bestIndividuals.Length; i++)
            {
                bestArray[bestLength + i] = Convert.ToInt32(bestIndividuals[i - 1] - bestIndividuals[i]);
            }
            for (int i = 1; i < worstIndividuals.Length; i++)
            {
                worstArray[worstLength + i] = Convert.ToInt32(worstIndividuals[i - 1] - worstIndividuals[i]);
            }
            Series best = this.chart1.Series.Add("Best");
            for (int i = 0; i < bestArray.Length; i++)
            {
                best.Points.Add(bestArray[i]);
            }
            Series worst = this.chart1.Series.Add("Worst");
            for (int i = 0; i < worstArray.Length; i++)
            {
                worst.Points.Add(worstArray[i]);
            }
            this.Refresh();
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
            //int[][] PiAllTours = new int[0][];                                  
            int k = 0;
            Random pointRandom = new Random();

            for (int i = 0; i < tours; i++)
            {
                Array.Resize(ref randomTour, randomTour.Length + 1);
                Array.Resize(ref PiAllTours, PiAllTours.Length + 1);            
                randomTour[i] = new Point[1];
                randomTour[i][0] = mid;
                PiAllTours[i] = new int[1];                                     
                PiAllTours[i][0] = 0;                                           
                for (int j = 1; j <= points; j++)
                {
                    k++;                                                        
                    bool rnd = true;
                    Array.Resize(ref randomTour[i], randomTour[i].Length + 1);
                    Array.Resize(ref PiAllTours[i], PiAllTours[i].Length + 1);  
                    PiAllTours[i][j] = k;                                       
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

        public int[][] greedyAlt(int[][] posMatrix, int startingPoint)
        {
            int[][] greedyTour = new int[0][];
            int j = 1;

            Array.Resize(ref greedyTour, PiAllTours.Length);

            for (int i = 0; i < greedyTour.Length; i++)
            {
                Array.Resize(ref greedyTour[i], PiAllTours[i].Length);
                greedyTour[i][0] = PiAllTours[i][0];
            }
            for (int i=0; i<greedyTour.Length; i++)
            {
                bool Continue = true;
                int k = 0;
                while (Continue)
                {
                    Continue = false;
                    int point = posMatrix[startingPoint][k];
                    if (contains(greedyTour, point))
                    {
                        Continue = true;
                        k++;
                    }
                    else
                    {
                        greedyTour[i][1] = point;
                    }
                }
            }

            while (j < PiAllTours[0].Length - 1)
            {
                j++;
                for (int i = 0; i < PiAllTours.Length; i++)
                {
                    int k = 0;
                    bool Continue = true;
                    while (Continue)
                    {
                        int point = posMatrix[greedyTour[i][j - 1]][k];
                        Continue = false;
                        if (contains(greedyTour, point))
                        {
                            Continue = true;
                            k++;
                        }
                        else
                        {
                            greedyTour[i][j] = point;
                        }
                    }
                }
            }

                return greedyTour;
        }

        public int[][] greedyAlt(int[][] posMatrix)
        {
            int[][] greedyTour = new int[0][];
            int j = 0;

            Array.Resize(ref greedyTour, PiAllTours.Length);

            for (int i = 0; i < PiAllTours.Length; i++)
            {
                Array.Resize(ref greedyTour[i], PiAllTours[i].Length);
                greedyTour[i][0] = PiAllTours[i][0];
            }

            while (j < PiAllTours[0].Length - 1)
            {
                j++;
                for (int i = 0; i < PiAllTours.Length; i++)
                {
                    int k = 0;
                    bool Continue = true;
                    while (Continue)
                    {
                        int point = posMatrix[greedyTour[i][j-1]][k];
                        Continue = false;
                        if (contains(greedyTour, point))
                        {
                            Continue = true;
                            k++;
                        }
                        else
                        {
                            greedyTour[i][j] = point;
                        }

                    }
                }


            }

                return greedyTour;
        }

        public int[][] greedyCon(int[][] posMatrix, int startingPoint)
        {
            int[][] greedyTour = new int[PiAllTours.Length][];

            for (int i = 0; i < greedyTour.Length; i++)
            {
                Array.Resize(ref greedyTour[i], PiAllTours[i].Length);
                greedyTour[i][0] = PiAllTours[i][0];
            }
            for (int i = 0; i < greedyTour.Length; i++)
            {
                bool Continue = true;
                int k = 0;
                while (Continue)
                {
                    Continue = false;
                    int point = posMatrix[startingPoint][k];
                    if (contains(greedyTour, point))
                    {
                        Continue = true;
                        k++;
                    }
                    else
                    {
                        greedyTour[i][1] = point;
                    }
                }
            }

            for (int i = 0; i < PiAllTours.Length; i++)
            {
                for (int j = 2; j < PiAllTours[i].Length; j++)
                {
                    int k = 0;
                    bool Continue = true;
                    while (Continue)
                    {
                        Continue = false;
                        int point = posMatrix[greedyTour[i][j - 1]][k];
                        if (contains(greedyTour, point))
                        {
                            Continue = true;
                            k++;
                        }
                        else
                        {
                            greedyTour[i][j] = point;
                        }

                    }
                }
            }

                return greedyTour;
        }        

        public int[][] greedyCon(int[][] posMatrix)
        {
            int[][] greedyTour = new int[PiAllTours.Length][];
            for (int i = 0; i < greedyTour.Length; i++)
            {
                Array.Resize(ref greedyTour[i], PiAllTours[i].Length);
                greedyTour[i][0] = PiAllTours[i][0];
            }

            for (int i = 0; i < PiAllTours.Length; i++)
            {
                for (int j = 1; j < PiAllTours[i].Length; j++)
                {
                    int k = 0;
                    bool Continue = true;
                    while (Continue)
                    {
                        Continue = false;
                        int point = posMatrix[greedyTour[i][j - 1]][k];
                        if (contains(greedyTour, point))
                        {
                            Continue = true;
                            k++;
                        }
                        else
                        {
                            greedyTour[i][j] = point;
                        }

                    }
                }
            }

                return greedyTour;
        }

        public int[][] radial()
        {
            int[][] radialTour = new int[0][];
            Array.Resize(ref radialTour, PiAllTours.Length);
            for (int i = 0; i < PiAllTours.Length; i++)
            {
                Array.Resize(ref radialTour, PiAllTours.Length);
                radialTour[i] = new int[PiAllTours[i].Length];
                radialTour[i][0] = PiAllTours[i][0];
            }

            double[] angles = new double[allPoints.Length];
            evaluation eval = new evaluation();

            int[] all = new int[allPoints.Length];
            for (int i = 0; i < all.Length; i++)
            {
                all[i] = i;
            }

            for (int i = 0; i < allPoints.Length; i++)
            {
                if (allPoints[i].X <= allPoints[0].X && allPoints[i].Y <= allPoints[0].Y) // x< ; y< /Q3
                {
                    angles[i] = 180 + Math.Abs(Math.Asin((allPoints[0].Y - allPoints[i].Y) / eval.twoPoints(allPoints[0], allPoints[i])) * (180 / Math.PI));
                }
                else if (allPoints[i].X > allPoints[0].X && allPoints[i].Y < allPoints[0].Y) // x> ; y< /Q4
                {
                    angles[i] = 360 - (Math.Asin((allPoints[0].Y - allPoints[i].Y) / eval.twoPoints(allPoints[0], allPoints[i])) * (180 / Math.PI));
                }
                else if (allPoints[i].X < allPoints[0].X && allPoints[i].Y > allPoints[0].Y) // x< ; y> /Q2
                {
                    angles[i] = 180 - Math.Abs(Math.Asin((allPoints[0].Y - allPoints[i].Y) / eval.twoPoints(allPoints[0], allPoints[i])) * (180 / Math.PI));
                }
                else //Q1
                {
                    angles[i] = Math.Abs(Math.Asin((allPoints[0].Y - allPoints[i].Y) / eval.twoPoints(allPoints[0], allPoints[i])) * (180 / Math.PI));
                }
            }

            bool swap = true;
            while (swap)    //sorting by angle (bubblesort)
            {
                swap = false;
                for (int i = 1; i < allPoints.Length - 1; i++)
                {
                    int dummy = 0;
                    double angle = 0;
                    if (angles[i] < angles[i + 1])
                    {
                        swap = true;
                        dummy = all[i];
                        angle = angles[i];
                        all[i] = all[i + 1];
                        all[i + 1] = dummy;
                        angles[i] = angles[i + 1];
                        angles[i + 1] = angle;
                    }
                }
            }

            int k = 0;
            for (int i = 0; i < radialTour.Length; i++)
            {
                for (int j = 1; j < radialTour[i].Length; j++)
                {
                    k++;
                    radialTour[i][j] = all[k];
                }
            }

                return radialTour;
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

        private bool contains(int[][] piPoints, int Point)
        {
            for (int i = 0; i < piPoints.Length; i++)
            {
                for (int j = 0; j < piPoints[i].Length; j++)
                {
                    if (piPoints[i][j] == Point)
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
        private int[][] positionMatrix = new int[0][];

        public localOptimization(Point[] points, double[][] distMatrix, int[][] posMatrix){
            Array.Resize(ref this.allPoints, points.Length);
            Array.Copy(points, this.allPoints, points.Length);

            Array.Resize(ref distanceMatrix, distMatrix.Length);
            for (int i = 0; i < distMatrix.Length; i++)
            {
                Array.Resize(ref distanceMatrix[i], distMatrix[i].Length);
                Array.Copy(distMatrix[i], distanceMatrix[i], distMatrix[i].Length);
            }

            Array.Resize(ref positionMatrix, posMatrix.Length);
            for (int i = 0; i < posMatrix.Length; i++)
            {
                Array.Resize(ref positionMatrix[i], posMatrix[i].Length);
                Array.Copy(posMatrix[i], positionMatrix[i], posMatrix[i].Length);
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
        public int[] tourORopt(int[] piTour, int extractLength)
        {
            int[] newTour = new int[piTour.Length];

            bool optFound = true;
            bool loopBreak = false;

            while (optFound)
            {
                optFound = false;
                for (int i = 1; i < piTour.Length - (extractLength - 1); i++) // extractLength-1, because we dont want outOfRange exception
                // cannot start at 0, because mid is at 0
                {
                    if (loopBreak)
                    {
                        loopBreak = false;
                        break;
                    }
                    int[] extract = new int[extractLength];
                    Array.Copy(piTour, i, extract, 0, extractLength); // Length 3, because OR3
                    for (int j = 1; j < piTour.Length - (extractLength - 1); j++)
                    {
                        if (i == j)
                        {
                            //do nothing (insert would be at same position)
                        }
                        else
                        {
                            newTour = insertExtract(piTour, extract, i, j);
                            if (evaluate.evalTour(newTour) < evaluate.evalTour(piTour))
                            {
                                Array.Copy(newTour, piTour, piTour.Length);
                                optFound = true;
                                loopBreak = true;
                                break;
                            }
                            else
                            {
                                Array.Copy(piTour, newTour, piTour.Length);
                            }
                        }
                    }
                }
            }

            return piTour;
        }

        public int[][] crossedORopt(int[][] piAllTours, int indexA, int indexB, int length)
        {
            int[][] newTours = new int[piAllTours.Length][];
            for (int i = 0; i < piAllTours.Length; i++)
            {
                Array.Resize(ref newTours[i], piAllTours[i].Length);
                Array.Copy(piAllTours[i], newTours[i], piAllTours[i].Length);
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
                    int[] extract = new int[length];
                    Array.Copy(newTours[indexA], i, extract, 0, length);

                    for (int j = 1; j < newTours[indexB].Length - (length - 1); j++)
                    {
                        Array.Copy(newTours[indexB], j, newTours[indexA], i, length);
                        Array.Copy(extract, 0, newTours[indexB], j, length);
                        //newTours[indexA][i] = newTours[indexB][j];
                        //newTours[indexB][j] = dummy;

                        if (evaluate.evalAll(newTours) < evaluate.evalAll(piAllTours))
                        {
                            for (int k = 0; k < newTours.Length; k++)
                            {
                                Array.Copy(newTours[k], piAllTours[k], piAllTours[k].Length);
                            }
                            optFound = true;
                            loopbreak = true;
                            break;
                        }
                        else
                        {
                            for (int k = 0; k < piAllTours.Length; k++)
                            {
                                Array.Copy(piAllTours[k], newTours[k], piAllTours[k].Length);
                            }
                        }
                    }
                }
            }

            return piAllTours;
        }

        public int[][] allORopt(int[][] piAllTours, int extractLength, bool neighbours)
        {
            for (int i = 0; i < piAllTours.Length; i++)
            {
                for (int j = 0; j < piAllTours.Length; j++)
                {
                    if (i != j)
                    {
                        if (neighbours)
                        {
                            piAllTours = neighbourOpt(piAllTours, i, j, extractLength);
                        }
                        else
                        {
                            piAllTours = crossedORopt(piAllTours, i, j, extractLength);
                        }
                    }
                }
            }
            return piAllTours;
        }

        public int[][] neighbourOpt(int[][] piAllTours, int indexA, int indexB, int amount)
        {
            int[] neighboursA = new int[5];
            int[] neighboursB = new int[5];
            double[] evalNeighA = new double[5];
            double[] evalNeighB = new double[5];

            for (int i = 1; i < piAllTours[indexA].Length; i++)
            {
                neighboursA = getNeighbours(piAllTours[indexA], piAllTours[indexB], i, 5);
                evalNeighA = evaluate.neighbours(piAllTours[indexA], i, neighboursA);

                for (int j = 1; j < piAllTours[indexB].Length; j++)
                {
                    neighboursB = getNeighbours(piAllTours[indexB], piAllTours[indexA], j, 5);
                    evalNeighB = evaluate.neighbours(piAllTours[indexB], j, neighboursB);
                }
            }
                return piAllTours;
        }

        public int[] insertExtract(int[] toBeAltered, int[] extract, int oldIndex, int index)
        {
            int[] inserted = new int[toBeAltered.Length];
            int[] extracted = new int[toBeAltered.Length - extract.Length];
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

        public int[] getNeighbours(int[] piTour, int[] otherTour, int index, int neighAmount)
        {
            int[] neighbours = new int[neighAmount];
            int[] tour = new int[piTour.Length];
            Array.Copy(piTour, tour, piTour.Length);
            int j = 0;
            int point = 0;

            for (int i = 0; i < neighAmount; i++)
            {
                bool Continue = true;
                while (Continue)
                {
                    Continue = false;
                    point = positionMatrix[piTour[index]][j];
                    if (contains(tour, point))
                    {
                        Continue = true;
                    }
                    else if (contains(otherTour, point))
                    {
                        neighbours[i] = point;
                    }
                    else
                    {
                        Continue = true; 
                    }
                    j++;
                }
            }
            return neighbours;
        }

        public int[][] insert(int[][] PiAllPoints, int indexA, int indexB, double percentage)
        {
            int[][] oldPOints = new int[PiAllPoints.Length][];
            for (int i=0; i< PiAllPoints.Length; i++)
            {
                oldPOints[i] = new int[PiAllPoints[i].Length];
                Array.Copy(PiAllPoints[i], oldPOints[i], PiAllPoints[i].Length);
            }
            int[] pointsAOld = new int[PiAllPoints[indexA].Length];
            Array.Copy(PiAllPoints[indexA], pointsAOld, PiAllPoints[indexA].Length);
            int[] pointsBOld = new int[PiAllPoints[indexB].Length];
            Array.Copy(PiAllPoints[indexB], pointsBOld, PiAllPoints[indexB].Length);

            bool optFound = true;
            bool loopBreak = false;

            while (optFound)
            {
                optFound = false;
                for (int i = 1; i < PiAllPoints[indexA].Length; i++)
                {
                    if (loopBreak)
                    {
                        loopBreak = false;
                        break;
                    }
                    for (int j = 1; j < PiAllPoints[indexB].Length; j++)
                    {
                        int insertPoint = PiAllPoints[indexA][i];
                        int[] tempA = new int[PiAllPoints[indexA].Length - (i + 1)];
                        Array.Copy(PiAllPoints[indexA], i + 1, tempA, 0, PiAllPoints[indexA].Length - (i + 1));
                        Array.Resize(ref PiAllPoints[indexA], PiAllPoints[indexA].Length - 1);
                        Array.Copy(tempA, 0, PiAllPoints[indexA], i, tempA.Length);

                        Array.Resize(ref PiAllPoints[indexB], PiAllPoints[indexB].Length + 1);
                        int[] tempB = new int[PiAllPoints[indexB].Length - (j+1)];
                        Array.Copy(PiAllPoints[indexB],j ,tempB ,0 , tempB.Length);
                        PiAllPoints[indexB][j] = insertPoint;
                        Array.Copy(tempB, 0, PiAllPoints[indexB], j + 1, tempB.Length);
                        if (evaluate.evalAll(PiAllPoints) >= evaluate.evalAll(oldPOints))
                        {
                            //Änderungen verwerfen
                            Array.Resize(ref PiAllPoints[indexA], oldPOints[indexA].Length);
                            Array.Resize(ref PiAllPoints[indexB], oldPOints[indexB].Length);
                            Array.Copy(oldPOints[indexA], PiAllPoints[indexA], PiAllPoints[indexA].Length);
                            Array.Copy(oldPOints[indexB], PiAllPoints[indexB], PiAllPoints[indexB].Length);
                        }
                        else
                        {
                            //Änderungen übernehmen
                            optFound = true;
                            for (int k=0; k< PiAllPoints.Length; k++)
                            {
                                Array.Resize(ref oldPOints[k], PiAllPoints[k].Length);
                                Array.Copy(PiAllPoints[k], oldPOints[k], PiAllPoints[k].Length);
                            }
                            break;
                        }
                    }
                }
            }
            return PiAllPoints;
        }

        private bool contains(int[] piTour, int point)
        {
            for (int i = 0; i < piTour.Length; i++)
            {
                if (piTour[i] == point)
                {
                    return true;
                }
            }
            return false;
        }

        public int[][] fullyOpt(int[][] PiAllTours)
        {
            bool optFound = true;
            //localOptimization localOpt = new localOptimization(allPoints, distanceMatrix, positionMatrix);
            int[][] newTour = new int[PiAllTours.Length][];
            for (int i = 0; i < PiAllTours.Length; i++)
            {
                Array.Resize(ref newTour[i], PiAllTours[i].Length);
                Array.Copy(PiAllTours[i], newTour[i], PiAllTours[i].Length);
            }

            while (optFound)
            {
                optFound = false;
                for (int i = 0; i < newTour.Length; i++)
                {
                    newTour[i] = tour2opt(newTour[i]);
                }
                newTour = allORopt(newTour, 3, false);
                newTour = allORopt(newTour, 2, false);
                newTour = allORopt(newTour, 1, false);
                for (int i = 0; i < PiAllTours.Length; i++)
                {
                    newTour[i] = tourORopt(newTour[i], 3);
                    newTour[i] = tourORopt(newTour[i], 2);
                    newTour[i] = tourORopt(newTour[i], 1);
                }
                if (evaluate.evalAll(newTour) < evaluate.evalAll(PiAllTours))
                {
                    optFound = true;
                    for (int i = 0; i < PiAllTours.Length; i++)
                    {
                        Array.Copy(newTour[i], PiAllTours[i], newTour[i].Length);
                    }
                }
            }
            return newTour;
        }
    }

    public class evoOpt
    {
        private Point[] allPoints = new Point[0];
        private double[][] distanceMatrix = new double[0][];
        private int[][] positionMatrix = new int[0][];

        private int[][][] population = new int[0][][];
        private int[][][] newGeneration = new int[0][][];

        private int[] removedPoints = new int[0];

        public double[] bestIndividuals = new double[0];
        public double[] worstIndividuals = new double[0];

        generate gen = new generate();

        evaluation evaluate;

        public evoOpt(Point[] points, double[][] distMatrix, int[][] posMatrix)
        {
            Array.Resize(ref allPoints, points.Length);
            Array.Copy(points, allPoints, points.Length);
            Array.Resize(ref distanceMatrix, distMatrix.Length);
            Array.Resize(ref positionMatrix, posMatrix.Length);
            for (int i = 0; i < distMatrix.Length; i++)
            {
                Array.Resize(ref distanceMatrix[i], distMatrix[i].Length);
                Array.Copy(distMatrix[i], this.distanceMatrix[i], distMatrix[i].Length);
            }
            for (int i = 0; i < posMatrix.Length; i++)
            {
                Array.Resize(ref positionMatrix[i], posMatrix[i].Length);
                Array.Copy(posMatrix[i], this.positionMatrix[i], posMatrix[i].Length);
            }

            gen.allPoints = allPoints;

            evaluate = new evaluation(allPoints, distanceMatrix);
        }

        //private evaluation evaluate = new evaluation(allPoints, distanceMatrix);

        public void initPopulation(int[][] PiAllPoints, int populationSize)
        {
            gen.PiAllTours = PiAllPoints;

            Array.Resize(ref population, populationSize);
            for (int i = 0; i < populationSize; i++)
            {
                population[i] = new int[0][];
                Array.Resize(ref population[i], PiAllPoints.Length);
            }

            for (int i = 0; i < population[0].Length; i++)
            {
                Array.Resize(ref population[0][i], PiAllPoints[i].Length);
                Array.Copy(PiAllPoints[i], population[0][i], PiAllPoints[i].Length);
            }

            int k = 1;
            bool alt = true;
            for (int i = 1; i < population.Length; i++)
            {
                if (i == 1)
                {
                    population[i] = gen.greedyAlt(positionMatrix);
                }
                else if (i == 2)
                {
                    population[i] = gen.greedyCon(positionMatrix);
                }
                else if (i == 3)
                {
                    population[i] = gen.radial();
                }
                else if (alt)
                {
                    alt = false;
                    population[i] = gen.greedyAlt(positionMatrix, k);
                }
                else if (!alt)
                {
                    alt = true;
                    population[i] = gen.greedyCon(positionMatrix, k);
                    k++;
                }
            }
            localOptPop();      //make a complete run of local optimization algorithms for each individual
        }

        public void localOptPop()
        {
            localOptimization localOpt = new localOptimization(allPoints, distanceMatrix, positionMatrix);
            for (int i = 0; i < population.Length; i++)
            {
                population[i] = localOpt.fullyOpt(population[i]);
            }
        }

        public int[][] bomb(int[][] PiAllTours, int bombSize)
        {
            int[] initialSizes = new int[PiAllTours.Length];
            Random bombRandom = new Random();
            int bombSpot = 0;
            localOptimization localOpt = new localOptimization(allPoints, distanceMatrix, positionMatrix);

            for (int i = 0; i < PiAllTours.Length; i++)
            {
                initialSizes[i] = PiAllTours[i].Length;
            }

            bombSpot = bombRandom.Next(0, allPoints.Length - 1);
            PiAllTours = remove(PiAllTours, bombSpot);

            for (int i = 0; i < bombSize; i++)
            {
                PiAllTours = remove(PiAllTours, positionMatrix[bombSpot][i]);
            }

            PiAllTours = reconnect(PiAllTours, initialSizes, removedPoints);
            Array.Resize(ref removedPoints, 0);
            PiAllTours = localOpt.fullyOpt(PiAllTours);

                return PiAllTours;
        }

        public void evoRun(int populationGrowth, int strategy, int bombSize, int runs)
        {
            //strategy can take the values 0 and 1 (comboBox index)
            //0 stands for "[my] + [lambda]"    -> don't discard parents
            //1 stands for "[my], [lambda]"     -> discard parents            
            int[] childsPerParent = new int[population.Length];
            int size = populationGrowth / population.Length;
            int rest = populationGrowth % population.Length;
            for (int i = 0; i < population.Length; i++)
            {
                childsPerParent[i] = size;
            }

            if (runs == 0)
            {
                // number of runs is needed
                return;
            }

            if (bombSize > allPoints.Length - 1)
            {
                MessageBox.Show("Bomb Size is greater than (or equal to) the number of Points");
                return;
            }

            for (int i=0; i< rest; i++)
            {
                childsPerParent[i]++;
            }


            if (strategy != 0 && strategy != 1)
            {
                MessageBox.Show("Please chose a strategy for Optimization");
                return;
            }

            int n = 0;
            int k = 0;
            Array.Resize(ref newGeneration, populationGrowth);
            for (int m = 0; m < runs; m++)
            {
                while (k < childsPerParent.Length)                  //building offsprings
                {
                    for (int j = 0; j < childsPerParent[k]; j++)
                    {
                        newGeneration[n] = bomb(population[k], bombSize);
                        n++;
                    }
                    k++;
                }
                k = 0;
                n = 0;

                if (strategy == 0)       //(my + lambda)
                {
                    Array.Resize(ref population, population.Length + newGeneration.Length);
                    Array.Copy(newGeneration, 0, population, population.Length - newGeneration.Length, newGeneration.Length);
                    population = evalSort(population);
                    Array.Resize(ref population, populationGrowth);
                }
                else if (strategy == 1)  //(my, lambda)
                {
                    newGeneration = evalSort(newGeneration);
                    Array.Copy(newGeneration, 0, population, 0, population.Length);
                }
                Array.Resize(ref bestIndividuals, bestIndividuals.Length + 1);
                bestIndividuals[bestIndividuals.Length - 1] = evaluate.evalAll(getBest());
                Array.Resize(ref worstIndividuals, worstIndividuals.Length + 1);
                worstIndividuals[worstIndividuals.Length - 1] = evaluate.evalAll(getWorst());
            }
        }

        private int[][][] evalSort(int[][][] pop)
        {
            bool cont = true;
            while (cont)
            {
                cont = false;
                for (int i = 1; i < pop.Length; i++)
                {
                    if (evaluate.evalAll(pop[i - 1]) > evaluate.evalAll(pop[i]))
                    {
                        cont = true;
                        int[][] temp = pop[i - 1];
                        pop[i - 1] = pop[i];
                        pop[i] = temp;
                    }
                }

            }
            return pop;
        }

        private int[][] remove(int[][] PiAllTours, int point)
        {
            int[][] newTour = new int[PiAllTours.Length][];
            for (int i = 0; i < PiAllTours.Length; i++)
            {
                Array.Resize(ref newTour[i], PiAllTours[i].Length);
                Array.Copy(PiAllTours[i], newTour[i], PiAllTours[i].Length);
            }

            bool loopBreak = false;
            for (int i = 0; i < PiAllTours.Length; i++)
            {
                if (loopBreak)
                {
                    break;
                }
                for (int j = 1; j < PiAllTours[i].Length; j++)
                {
                    if (PiAllTours[i][j] == point)
                    {
                        int[] temp = new int[newTour[i].Length - j - 1];
                        Array.Resize(ref removedPoints, removedPoints.Length + 1);
                        removedPoints[removedPoints.Length - 1] = point;
                        Array.Copy(newTour[i], j + 1, temp, 0, temp.Length);
                        Array.Resize(ref newTour[i], newTour[i].Length - 1);
                        Array.Copy(temp, 0, newTour[i], j, temp.Length);
                        loopBreak = true;
                        break;
                    }
                }
            }
                return newTour;
        }

        private int[][] reconnect(int[][] PiAllTours, int[] initialSizes, int[] removedPoints)
        {
            for (int i = 0; i < removedPoints.Length; i++)
            {
                int j = 0;
                bool cont = true;
                while (cont)
                {
                    cont = false;
                    bool Contains = false;
                    bool loopbreak = false;
                    for (int l = 0; l < PiAllTours.Length; l++)
                    {
                        if (loopbreak)
                        {
                            loopbreak = false;
                            break;
                        }
                        //if (PiAllTours[l].Length >= initialSizes[l])
                        //{
                        //    //this array already has enough points

                        //    break;
                        //}
                        for (int k = 0; k < PiAllTours[l].Length; k++)
                        {
                            if (PiAllTours[l][k] == positionMatrix[removedPoints[i]][j]) // && !contains(PiAllTours, removedPoints[i]))
                            {
                                if (PiAllTours[l].Length >= initialSizes[l])
                                {
                                    //contains = true;
                                    loopbreak = true;
                                    break;
                                }
                                Contains = true;
                                int[] temp = new int[PiAllTours[l].Length - k - 1];
                                Array.Resize(ref PiAllTours[l], PiAllTours[l].Length + 1);
                                Array.Copy(PiAllTours[l], k + 1, temp, 0, temp.Length);
                                PiAllTours[l][k+1] = removedPoints[i];
                                Array.Copy(temp, 0, PiAllTours[l], k + 2, temp.Length);
                                loopbreak = true;
                                break;
                            }
                        }
                    }
                    if (!Contains)
                    {
                        cont = true;
                        j++;
                    }
                }
            }
            Array.Resize(ref removedPoints, 0);
            return PiAllTours;
        }

        private bool contains(int[][] PiAllTours, int point)
        {
            for (int i = 0; i < PiAllTours.Length; i++)
            {
                if (singleContains(PiAllTours[i], point))
                {
                    return true;
                }
            }
            return false;
        }

        private bool singleContains(int[] points, int point)
        {
            for (int i = 0; i < points.Length; i++ )
            {
                if (points[i] == point)
                {
                    return true;
                }
            }
            return false;
        }

        public int[][] getBest()
        {
            int[][] PiAllTours = new int[population[0].Length][];
            population = evalSort(population);
            PiAllTours = population[0];
            return PiAllTours;
        }

        public int[][] getWorst()
        {
            int[][] PiAllTours = new int[population[0].Length][];
            population = evalSort(population);
            PiAllTours = population[population.Length - 1];
            return PiAllTours;
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
        public double[] neighbours(int[] piTour, int index, int[] neighbours)
        {
            double[] evalNeigh = new double[neighbours.Length];
            double startingEval = evalTour(piTour);

            for (int i = 0; i < neighbours.Length; i++)
            {
                piTour[index] = neighbours[i];
                evalNeigh[i] = evalTour(piTour) - startingEval;
            }

            return evalNeigh;
        }
    }
}
