namespace multipleTSP
{
    partial class multipleTSP
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(multipleTSP));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fILEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sAVEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oPENToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fUNCTIONSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.paintPanel = new System.Windows.Forms.Panel();
            this.draw_button = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.generateBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gui_points = new System.Windows.Forms.TextBox();
            this.gui_tours = new System.Windows.Forms.TextBox();
            this.button_generate = new System.Windows.Forms.Button();
            this.button_next_tour = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.gui_totalLength = new System.Windows.Forms.TextBox();
            this.gui_avgLength = new System.Windows.Forms.TextBox();
            this.draw_complete = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.button_insert = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.tb_insert_percent = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cb_neigh = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.loclOptBoxAll = new System.Windows.Forms.ComboBox();
            this.button_localOpt = new System.Windows.Forms.Button();
            this.loclOptBox = new System.Windows.Forms.ComboBox();
            this.menuStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fILEToolStripMenuItem,
            this.fUNCTIONSToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(772, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fILEToolStripMenuItem
            // 
            this.fILEToolStripMenuItem.AccessibleName = "menuItemFile";
            this.fILEToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sAVEToolStripMenuItem,
            this.oPENToolStripMenuItem});
            this.fILEToolStripMenuItem.Name = "fILEToolStripMenuItem";
            this.fILEToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.fILEToolStripMenuItem.Text = "FILE";
            // 
            // sAVEToolStripMenuItem
            // 
            this.sAVEToolStripMenuItem.AccessibleName = "menuItemSave";
            this.sAVEToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("sAVEToolStripMenuItem.Image")));
            this.sAVEToolStripMenuItem.Name = "sAVEToolStripMenuItem";
            this.sAVEToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.sAVEToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.sAVEToolStripMenuItem.Text = "Save";
            this.sAVEToolStripMenuItem.ToolTipText = "Save Points and routes";
            this.sAVEToolStripMenuItem.Click += new System.EventHandler(this.sAVEToolStripMenuItem_Click);
            // 
            // oPENToolStripMenuItem
            // 
            this.oPENToolStripMenuItem.AccessibleName = "menuItemOpen";
            this.oPENToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("oPENToolStripMenuItem.Image")));
            this.oPENToolStripMenuItem.Name = "oPENToolStripMenuItem";
            this.oPENToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.oPENToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.oPENToolStripMenuItem.Text = "Open";
            this.oPENToolStripMenuItem.ToolTipText = "Open points and routes from file";
            this.oPENToolStripMenuItem.Click += new System.EventHandler(this.oPENToolStripMenuItem_Click);
            // 
            // fUNCTIONSToolStripMenuItem
            // 
            this.fUNCTIONSToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearAllToolStripMenuItem});
            this.fUNCTIONSToolStripMenuItem.Name = "fUNCTIONSToolStripMenuItem";
            this.fUNCTIONSToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.A)));
            this.fUNCTIONSToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
            this.fUNCTIONSToolStripMenuItem.Text = "FUNCTIONS";
            this.fUNCTIONSToolStripMenuItem.ToolTipText = "clear sketch and input fields";
            // 
            // clearAllToolStripMenuItem
            // 
            this.clearAllToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("clearAllToolStripMenuItem.Image")));
            this.clearAllToolStripMenuItem.Name = "clearAllToolStripMenuItem";
            this.clearAllToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.clearAllToolStripMenuItem.Text = "clear all";
            this.clearAllToolStripMenuItem.Click += new System.EventHandler(this.clearAllToolStripMenuItem_Click);
            // 
            // paintPanel
            // 
            this.paintPanel.AccessibleName = "paintPanel";
            this.paintPanel.BackColor = System.Drawing.Color.White;
            this.paintPanel.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.paintPanel.Location = new System.Drawing.Point(308, 28);
            this.paintPanel.Name = "paintPanel";
            this.paintPanel.Size = new System.Drawing.Size(464, 363);
            this.paintPanel.TabIndex = 1;
            // 
            // draw_button
            // 
            this.draw_button.Location = new System.Drawing.Point(308, 388);
            this.draw_button.Name = "draw_button";
            this.draw_button.Size = new System.Drawing.Size(151, 23);
            this.draw_button.TabIndex = 5;
            this.draw_button.Text = "draw Tours";
            this.draw_button.UseVisualStyleBackColor = true;
            this.draw_button.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.generateBox);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.gui_points);
            this.groupBox2.Controls.Add(this.gui_tours);
            this.groupBox2.Controls.Add(this.button_generate);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.groupBox2.Location = new System.Drawing.Point(12, 31);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(290, 126);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Initialization";
            // 
            // generateBox
            // 
            this.generateBox.FormattingEnabled = true;
            this.generateBox.Items.AddRange(new object[] {
            "Random",
            "Greedy (alternate)",
            "Greedy (consecutive)",
            "Radial"});
            this.generateBox.Location = new System.Drawing.Point(6, 82);
            this.generateBox.Name = "generateBox";
            this.generateBox.Size = new System.Drawing.Size(121, 23);
            this.generateBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.label1.Location = new System.Drawing.Point(8, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Points per Tour";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.label2.Location = new System.Drawing.Point(8, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Number of Tours";
            // 
            // gui_points
            // 
            this.gui_points.Location = new System.Drawing.Point(141, 49);
            this.gui_points.Name = "gui_points";
            this.gui_points.Size = new System.Drawing.Size(68, 21);
            this.gui_points.TabIndex = 2;
            this.gui_points.Text = "10";
            // 
            // gui_tours
            // 
            this.gui_tours.Location = new System.Drawing.Point(141, 23);
            this.gui_tours.Name = "gui_tours";
            this.gui_tours.Size = new System.Drawing.Size(68, 21);
            this.gui_tours.TabIndex = 1;
            this.gui_tours.Text = "2";
            // 
            // button_generate
            // 
            this.button_generate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.button_generate.Location = new System.Drawing.Point(221, 96);
            this.button_generate.Name = "button_generate";
            this.button_generate.Size = new System.Drawing.Size(63, 23);
            this.button_generate.TabIndex = 4;
            this.button_generate.Text = "generate";
            this.button_generate.UseVisualStyleBackColor = true;
            this.button_generate.Click += new System.EventHandler(this.button_generate_Click);
            // 
            // button_next_tour
            // 
            this.button_next_tour.Enabled = false;
            this.button_next_tour.Location = new System.Drawing.Point(465, 388);
            this.button_next_tour.Name = "button_next_tour";
            this.button_next_tour.Size = new System.Drawing.Size(151, 23);
            this.button_next_tour.TabIndex = 6;
            this.button_next_tour.Text = "next Tour";
            this.button_next_tour.UseVisualStyleBackColor = true;
            this.button_next_tour.Click += new System.EventHandler(this.button_next_tour_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label3.Location = new System.Drawing.Point(305, 426);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 16);
            this.label3.TabIndex = 17;
            this.label3.Text = "Total Length";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label4.Location = new System.Drawing.Point(305, 451);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 16);
            this.label4.TabIndex = 18;
            this.label4.Text = "Average Length";
            // 
            // gui_totalLength
            // 
            this.gui_totalLength.Location = new System.Drawing.Point(435, 422);
            this.gui_totalLength.Name = "gui_totalLength";
            this.gui_totalLength.ReadOnly = true;
            this.gui_totalLength.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.gui_totalLength.Size = new System.Drawing.Size(130, 20);
            this.gui_totalLength.TabIndex = 6;
            this.gui_totalLength.Text = "0";
            // 
            // gui_avgLength
            // 
            this.gui_avgLength.Location = new System.Drawing.Point(435, 447);
            this.gui_avgLength.Name = "gui_avgLength";
            this.gui_avgLength.ReadOnly = true;
            this.gui_avgLength.Size = new System.Drawing.Size(130, 20);
            this.gui_avgLength.TabIndex = 19;
            this.gui_avgLength.Text = "0";
            // 
            // draw_complete
            // 
            this.draw_complete.Enabled = false;
            this.draw_complete.Location = new System.Drawing.Point(621, 388);
            this.draw_complete.Name = "draw_complete";
            this.draw_complete.Size = new System.Drawing.Size(151, 23);
            this.draw_complete.TabIndex = 7;
            this.draw_complete.Text = "completed";
            this.draw_complete.UseVisualStyleBackColor = true;
            this.draw_complete.Click += new System.EventHandler(this.draw_complete_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.button_insert);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.tb_insert_percent);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cb_neigh);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.loclOptBoxAll);
            this.groupBox1.Controls.Add(this.button_localOpt);
            this.groupBox1.Controls.Add(this.loclOptBox);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.groupBox1.Location = new System.Drawing.Point(12, 177);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(290, 164);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Local Optimization";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.label10.Location = new System.Drawing.Point(8, 120);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(116, 16);
            this.label10.TabIndex = 13;
            this.label10.Text = "Insert Optimization";
            // 
            // button_insert
            // 
            this.button_insert.Enabled = false;
            this.button_insert.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.button_insert.Location = new System.Drawing.Point(221, 135);
            this.button_insert.Name = "button_insert";
            this.button_insert.Size = new System.Drawing.Size(63, 23);
            this.button_insert.TabIndex = 13;
            this.button_insert.Text = "insert";
            this.button_insert.UseVisualStyleBackColor = true;
            this.button_insert.Click += new System.EventHandler(this.button_insert_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.label9.Location = new System.Drawing.Point(148, 143);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(20, 16);
            this.label9.TabIndex = 11;
            this.label9.Text = "%";
            // 
            // tb_insert_percent
            // 
            this.tb_insert_percent.Location = new System.Drawing.Point(112, 140);
            this.tb_insert_percent.Name = "tb_insert_percent";
            this.tb_insert_percent.Size = new System.Drawing.Size(30, 21);
            this.tb_insert_percent.TabIndex = 12;
            this.tb_insert_percent.Text = "10";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.label8.Location = new System.Drawing.Point(15, 142);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(92, 16);
            this.label8.TabIndex = 7;
            this.label8.Text = "Tour Variation";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label7.Location = new System.Drawing.Point(130, 122);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(118, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "(varitate points per tour)";
            // 
            // cb_neigh
            // 
            this.cb_neigh.AutoSize = true;
            this.cb_neigh.Location = new System.Drawing.Point(7, 81);
            this.cb_neigh.Name = "cb_neigh";
            this.cb_neigh.Size = new System.Drawing.Size(122, 20);
            this.cb_neigh.TabIndex = 10;
            this.cb_neigh.Text = "use Neighbours";
            this.cb_neigh.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label6.Location = new System.Drawing.Point(135, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "(among tours)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label5.Location = new System.Drawing.Point(135, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "(single tours)";
            // 
            // loclOptBoxAll
            // 
            this.loclOptBoxAll.FormattingEnabled = true;
            this.loclOptBoxAll.Items.AddRange(new object[] {
            "-",
            "OR-Opt (3, 2 and 1)",
            "OR3-Opt",
            "OR2-Opt",
            "OR1-Opt"});
            this.loclOptBoxAll.Location = new System.Drawing.Point(7, 51);
            this.loclOptBoxAll.Name = "loclOptBoxAll";
            this.loclOptBoxAll.Size = new System.Drawing.Size(121, 23);
            this.loclOptBoxAll.TabIndex = 9;
            // 
            // button_localOpt
            // 
            this.button_localOpt.Enabled = false;
            this.button_localOpt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.button_localOpt.Location = new System.Drawing.Point(221, 51);
            this.button_localOpt.Name = "button_localOpt";
            this.button_localOpt.Size = new System.Drawing.Size(63, 23);
            this.button_localOpt.TabIndex = 11;
            this.button_localOpt.Text = "start";
            this.button_localOpt.UseVisualStyleBackColor = true;
            this.button_localOpt.Click += new System.EventHandler(this.button_localOpt_Click);
            // 
            // loclOptBox
            // 
            this.loclOptBox.FormattingEnabled = true;
            this.loclOptBox.Items.AddRange(new object[] {
            "-",
            "2-Opt",
            "OR-Opt (3, 2 and 1)",
            "OR3-Opt",
            "OR2-Opt",
            "OR1-Opt"});
            this.loclOptBox.Location = new System.Drawing.Point(7, 21);
            this.loclOptBox.Name = "loclOptBox";
            this.loclOptBox.Size = new System.Drawing.Size(121, 23);
            this.loclOptBox.TabIndex = 8;
            // 
            // multipleTSP
            // 
            this.AccessibleDescription = "Layout for Main Windoq";
            this.AccessibleName = "multipleTSPlayout";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 556);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button_next_tour);
            this.Controls.Add(this.draw_button);
            this.Controls.Add(this.draw_complete);
            this.Controls.Add(this.gui_avgLength);
            this.Controls.Add(this.gui_totalLength);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.paintPanel);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "multipleTSP";
            this.Text = "Evolutionary Optimization on multipleTSP";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fILEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sAVEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oPENToolStripMenuItem;
        private System.Windows.Forms.Panel paintPanel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox gui_points;
        private System.Windows.Forms.TextBox gui_tours;
        private System.Windows.Forms.Button button_generate;
        private System.Windows.Forms.Button button_next_tour;
        private System.Windows.Forms.Button draw_button;
        private System.Windows.Forms.ToolStripMenuItem fUNCTIONSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearAllToolStripMenuItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox gui_totalLength;
        private System.Windows.Forms.TextBox gui_avgLength;
        private System.Windows.Forms.Button draw_complete;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button_localOpt;
        private System.Windows.Forms.ComboBox loclOptBox;
        private System.Windows.Forms.ComboBox loclOptBoxAll;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cb_neigh;
        private System.Windows.Forms.ComboBox generateBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tb_insert_percent;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button_insert;
    }
}

