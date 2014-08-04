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
            this.menuStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            this.draw_button.TabIndex = 16;
            this.draw_button.Text = "draw Tours";
            this.draw_button.UseVisualStyleBackColor = true;
            this.draw_button.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.gui_points);
            this.groupBox2.Controls.Add(this.gui_tours);
            this.groupBox2.Controls.Add(this.button_generate);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.groupBox2.Location = new System.Drawing.Point(12, 31);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(290, 80);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Initialization";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Number of Points";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Number of Tours";
            // 
            // gui_points
            // 
            this.gui_points.Location = new System.Drawing.Point(141, 21);
            this.gui_points.Name = "gui_points";
            this.gui_points.Size = new System.Drawing.Size(68, 21);
            this.gui_points.TabIndex = 3;
            this.gui_points.Text = "10";
            // 
            // gui_tours
            // 
            this.gui_tours.Location = new System.Drawing.Point(141, 47);
            this.gui_tours.Name = "gui_tours";
            this.gui_tours.Size = new System.Drawing.Size(68, 21);
            this.gui_tours.TabIndex = 5;
            this.gui_tours.Text = "2";
            // 
            // button_generate
            // 
            this.button_generate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.button_generate.Location = new System.Drawing.Point(221, 45);
            this.button_generate.Name = "button_generate";
            this.button_generate.Size = new System.Drawing.Size(63, 23);
            this.button_generate.TabIndex = 1;
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
            this.button_next_tour.TabIndex = 15;
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
            this.gui_totalLength.Size = new System.Drawing.Size(130, 20);
            this.gui_totalLength.TabIndex = 6;
            // 
            // gui_avgLength
            // 
            this.gui_avgLength.Location = new System.Drawing.Point(435, 447);
            this.gui_avgLength.Name = "gui_avgLength";
            this.gui_avgLength.ReadOnly = true;
            this.gui_avgLength.Size = new System.Drawing.Size(130, 20);
            this.gui_avgLength.TabIndex = 19;
            // 
            // draw_complete
            // 
            this.draw_complete.Enabled = false;
            this.draw_complete.Location = new System.Drawing.Point(621, 388);
            this.draw_complete.Name = "draw_complete";
            this.draw_complete.Size = new System.Drawing.Size(151, 23);
            this.draw_complete.TabIndex = 20;
            this.draw_complete.Text = "completed";
            this.draw_complete.UseVisualStyleBackColor = true;
            this.draw_complete.Click += new System.EventHandler(this.draw_complete_Click);
            // 
            // multipleTSP
            // 
            this.AccessibleDescription = "Layout for Main Windoq";
            this.AccessibleName = "multipleTSPlayout";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 556);
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
    }
}

