
namespace Spreadsheet_Benjamin_Hoover
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridMain = new System.Windows.Forms.DataGridView();
            this.ButtonDemo = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cellMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeColorMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridMain)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridMain
            // 
            this.dataGridMain.AllowUserToAddRows = false;
            this.dataGridMain.AllowUserToDeleteRows = false;
            this.dataGridMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridMain.Location = new System.Drawing.Point(0, 36);
            this.dataGridMain.Name = "dataGridMain";
            this.dataGridMain.RowHeadersWidth = 62;
            this.dataGridMain.RowTemplate.Height = 28;
            this.dataGridMain.Size = new System.Drawing.Size(800, 380);
            this.dataGridMain.TabIndex = 0;
            this.dataGridMain.Tag = "dataGridMain";
            this.dataGridMain.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.DataGridMain_CellBeginEdit);
            this.dataGridMain.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridMain_CellContentClick);
            this.dataGridMain.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridMain_CellEndEdit);
            // 
            // ButtonDemo
            // 
            this.ButtonDemo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ButtonDemo.Location = new System.Drawing.Point(0, 416);
            this.ButtonDemo.Name = "ButtonDemo";
            this.ButtonDemo.Size = new System.Drawing.Size(800, 34);
            this.ButtonDemo.TabIndex = 1;
            this.ButtonDemo.Tag = "ButtonDemo";
            this.ButtonDemo.Text = "Demo";
            this.ButtonDemo.UseVisualStyleBackColor = true;
            this.ButtonDemo.Click += new System.EventHandler(this.ButtonDemo_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuItem,
            this.editMenuItem,
            this.cellMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 33);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileMenuItem
            // 
            this.fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveMenuItem,
            this.loadMenuItem});
            this.fileMenuItem.Name = "fileMenuItem";
            this.fileMenuItem.Size = new System.Drawing.Size(54, 29);
            this.fileMenuItem.Tag = "fileMenuItem";
            this.fileMenuItem.Text = "File";
            // 
            // editMenuItem
            // 
            this.editMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoMenuItem,
            this.redoMenuItem});
            this.editMenuItem.Name = "editMenuItem";
            this.editMenuItem.Size = new System.Drawing.Size(58, 29);
            this.editMenuItem.Tag = "editMenuItem";
            this.editMenuItem.Text = "Edit";
            // 
            // undoMenuItem
            // 
            this.undoMenuItem.Enabled = false;
            this.undoMenuItem.Name = "undoMenuItem";
            this.undoMenuItem.Size = new System.Drawing.Size(270, 34);
            this.undoMenuItem.Tag = "undoMenuItem";
            this.undoMenuItem.Text = "Undo";
            this.undoMenuItem.Click += new System.EventHandler(this.UndoMenuItem_Click);
            // 
            // redoMenuItem
            // 
            this.redoMenuItem.Enabled = false;
            this.redoMenuItem.Name = "redoMenuItem";
            this.redoMenuItem.Size = new System.Drawing.Size(270, 34);
            this.redoMenuItem.Tag = "redoMenuItem";
            this.redoMenuItem.Text = "Redo";
            this.redoMenuItem.Click += new System.EventHandler(this.RedoMenuItem_Click);
            // 
            // cellMenuItem
            // 
            this.cellMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeColorMenuItem});
            this.cellMenuItem.Name = "cellMenuItem";
            this.cellMenuItem.Size = new System.Drawing.Size(56, 29);
            this.cellMenuItem.Tag = "cellMenuItem";
            this.cellMenuItem.Text = "Cell";
            // 
            // changeColorMenuItem
            // 
            this.changeColorMenuItem.Name = "changeColorMenuItem";
            this.changeColorMenuItem.Size = new System.Drawing.Size(332, 34);
            this.changeColorMenuItem.Tag = "changeColorMenuItem";
            this.changeColorMenuItem.Text = "Change background color...";
            this.changeColorMenuItem.Click += new System.EventHandler(this.ChangeColorMenuItem_Click);
            // 
            // saveMenuItem
            // 
            this.saveMenuItem.Name = "saveMenuItem";
            this.saveMenuItem.Size = new System.Drawing.Size(270, 34);
            this.saveMenuItem.Tag = "saveMenuItem";
            this.saveMenuItem.Text = "Save";
            this.saveMenuItem.Click += new System.EventHandler(this.SaveMenuItem_Click);
            // 
            // loadMenuItem
            // 
            this.loadMenuItem.Name = "loadMenuItem";
            this.loadMenuItem.Size = new System.Drawing.Size(270, 34);
            this.loadMenuItem.Tag = "loadMenuItem";
            this.loadMenuItem.Text = "Load";
            this.loadMenuItem.Click += new System.EventHandler(this.LoadMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ButtonDemo);
            this.Controls.Add(this.dataGridMain);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Tag = "FormMain";
            this.Text = "Spreadsheet_Benjamin_Hoover";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridMain)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridMain;
        private System.Windows.Forms.Button ButtonDemo;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cellMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeColorMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadMenuItem;
    }
}

