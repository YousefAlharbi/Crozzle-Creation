namespace Assignment1
{
    partial class Main
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
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.buttonLoadWordList = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.openWordListMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openCrozzleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createCrozzleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wordDataGridView = new System.Windows.Forms.DataGridView();
            this.buttonLoadCrozzleList = new System.Windows.Forms.Label();
            this.crozzleDataGridView = new System.Windows.Forms.DataGridView();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.messageLabel = new System.Windows.Forms.Label();
            this.pointLabel = new System.Windows.Forms.Label();
            this.crozzleProgressBar = new System.Windows.Forms.ProgressBar();
            this.createNewCrozzle = new System.Windows.Forms.Label();
            this.secondPassedLabel = new System.Windows.Forms.Label();
            this.buttonSaveCrozzle = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wordDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.crozzleDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // buttonLoadWordList
            // 
            this.buttonLoadWordList.BackColor = System.Drawing.Color.CornflowerBlue;
            this.buttonLoadWordList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.buttonLoadWordList.Cursor = System.Windows.Forms.Cursors.Default;
            this.buttonLoadWordList.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLoadWordList.ForeColor = System.Drawing.Color.Black;
            this.buttonLoadWordList.Location = new System.Drawing.Point(5, 37);
            this.buttonLoadWordList.Name = "buttonLoadWordList";
            this.buttonLoadWordList.Size = new System.Drawing.Size(343, 70);
            this.buttonLoadWordList.TabIndex = 1;
            this.buttonLoadWordList.Text = "Load New Word List";
            this.buttonLoadWordList.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.buttonLoadWordList.Click += new System.EventHandler(this.buttonLoadWordList_Click);
            this.buttonLoadWordList.MouseLeave += new System.EventHandler(this.buttonLoadWordList_MouseLeave);
            this.buttonLoadWordList.MouseHover += new System.EventHandler(this.buttonLoadWordList_MouseHover);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.BackColor = System.Drawing.SystemColors.HotTrack;
            this.menuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(904, 35);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileMenu
            // 
            this.fileMenu.AutoSize = false;
            this.fileMenu.BackColor = System.Drawing.Color.CornflowerBlue;
            this.fileMenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openWordListMenuItem,
            this.openCrozzleMenuItem,
            this.createCrozzleMenuItem,
            this.exitMenuItem});
            this.fileMenu.Font = new System.Drawing.Font("Cooper Black", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileMenu.ForeColor = System.Drawing.Color.Black;
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(94, 30);
            this.fileMenu.Text = "File";
            // 
            // openWordListMenuItem
            // 
            this.openWordListMenuItem.Font = new System.Drawing.Font("Comic Sans MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openWordListMenuItem.Name = "openWordListMenuItem";
            this.openWordListMenuItem.Size = new System.Drawing.Size(188, 24);
            this.openWordListMenuItem.Text = "Open Word List";
            this.openWordListMenuItem.Click += new System.EventHandler(this.openWordListMenuItem_Click);
            // 
            // openCrozzleMenuItem
            // 
            this.openCrozzleMenuItem.Font = new System.Drawing.Font("Comic Sans MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openCrozzleMenuItem.Name = "openCrozzleMenuItem";
            this.openCrozzleMenuItem.Size = new System.Drawing.Size(188, 24);
            this.openCrozzleMenuItem.Text = "Open Crozzle";
            this.openCrozzleMenuItem.Click += new System.EventHandler(this.openCrozzleMenuItem_Click);
            // 
            // createCrozzleMenuItem
            // 
            this.createCrozzleMenuItem.Font = new System.Drawing.Font("Comic Sans MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createCrozzleMenuItem.Name = "createCrozzleMenuItem";
            this.createCrozzleMenuItem.Size = new System.Drawing.Size(188, 24);
            this.createCrozzleMenuItem.Text = "Create Crozzle";
            this.createCrozzleMenuItem.Click += new System.EventHandler(this.createCrozzleMenuItem_Click);
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Font = new System.Drawing.Font("Comic Sans MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitMenuItem.Name = "exitMenuItem";
            this.exitMenuItem.Size = new System.Drawing.Size(188, 24);
            this.exitMenuItem.Text = "Exit";
            // 
            // wordDataGridView
            // 
            this.wordDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.wordDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.wordDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.wordDataGridView.Location = new System.Drawing.Point(5, 110);
            this.wordDataGridView.Name = "wordDataGridView";
            this.wordDataGridView.RowHeadersWidth = 60;
            this.wordDataGridView.Size = new System.Drawing.Size(343, 403);
            this.wordDataGridView.TabIndex = 3;
            this.wordDataGridView.Visible = false;
            this.wordDataGridView.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dataGridView1_ColumnAdded);
            this.wordDataGridView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            // 
            // buttonLoadCrozzleList
            // 
            this.buttonLoadCrozzleList.BackColor = System.Drawing.Color.CornflowerBlue;
            this.buttonLoadCrozzleList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.buttonLoadCrozzleList.Cursor = System.Windows.Forms.Cursors.Default;
            this.buttonLoadCrozzleList.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLoadCrozzleList.ForeColor = System.Drawing.Color.Black;
            this.buttonLoadCrozzleList.Location = new System.Drawing.Point(354, 37);
            this.buttonLoadCrozzleList.Name = "buttonLoadCrozzleList";
            this.buttonLoadCrozzleList.Size = new System.Drawing.Size(288, 70);
            this.buttonLoadCrozzleList.TabIndex = 1;
            this.buttonLoadCrozzleList.Text = "Load New Crozzle";
            this.buttonLoadCrozzleList.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.buttonLoadCrozzleList.Click += new System.EventHandler(this.buttonLoadCrozzleList_Click);
            this.buttonLoadCrozzleList.MouseLeave += new System.EventHandler(this.buttonLoadCrozzleList_MouseLeave);
            this.buttonLoadCrozzleList.MouseHover += new System.EventHandler(this.buttonLoadCrozzleList_MouseHover);
            // 
            // crozzleDataGridView
            // 
            this.crozzleDataGridView.BackgroundColor = System.Drawing.SystemColors.HighlightText;
            this.crozzleDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.crozzleDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.crozzleDataGridView.Location = new System.Drawing.Point(5, 110);
            this.crozzleDataGridView.Name = "crozzleDataGridView";
            this.crozzleDataGridView.RowHeadersWidth = 60;
            this.crozzleDataGridView.Size = new System.Drawing.Size(595, 403);
            this.crozzleDataGridView.TabIndex = 3;
            this.crozzleDataGridView.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.crozzleDataGridView_ColumnAdded);
            this.crozzleDataGridView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.crozzleDataGridView_DataBindingComplete);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(110, 5);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(782, 23);
            this.progressBar.TabIndex = 4;
            // 
            // messageLabel
            // 
            this.messageLabel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.messageLabel.Location = new System.Drawing.Point(12, 516);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(880, 23);
            this.messageLabel.TabIndex = 5;
            this.messageLabel.Text = "Message:";
            this.messageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pointLabel
            // 
            this.pointLabel.BackColor = System.Drawing.Color.Salmon;
            this.pointLabel.Font = new System.Drawing.Font("Maiandra GD", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pointLabel.Location = new System.Drawing.Point(606, 110);
            this.pointLabel.Name = "pointLabel";
            this.pointLabel.Size = new System.Drawing.Size(286, 63);
            this.pointLabel.TabIndex = 6;
            this.pointLabel.Text = "Points: ";
            this.pointLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // crozzleProgressBar
            // 
            this.crozzleProgressBar.Location = new System.Drawing.Point(5, 150);
            this.crozzleProgressBar.Name = "crozzleProgressBar";
            this.crozzleProgressBar.Size = new System.Drawing.Size(887, 23);
            this.crozzleProgressBar.TabIndex = 1;
            this.crozzleProgressBar.Visible = false;
            // 
            // createNewCrozzle
            // 
            this.createNewCrozzle.BackColor = System.Drawing.Color.CornflowerBlue;
            this.createNewCrozzle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.createNewCrozzle.Cursor = System.Windows.Forms.Cursors.Default;
            this.createNewCrozzle.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createNewCrozzle.ForeColor = System.Drawing.Color.Black;
            this.createNewCrozzle.Location = new System.Drawing.Point(648, 37);
            this.createNewCrozzle.Name = "createNewCrozzle";
            this.createNewCrozzle.Size = new System.Drawing.Size(244, 70);
            this.createNewCrozzle.TabIndex = 1;
            this.createNewCrozzle.Text = "Create New Crozzle";
            this.createNewCrozzle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.createNewCrozzle.Click += new System.EventHandler(this.createNewCrozzle_Click);
            this.createNewCrozzle.MouseLeave += new System.EventHandler(this.createNewCrozzle_MouseLeave);
            this.createNewCrozzle.MouseHover += new System.EventHandler(this.createNewCrozzle_MouseHover);
            // 
            // secondPassedLabel
            // 
            this.secondPassedLabel.BackColor = System.Drawing.Color.Salmon;
            this.secondPassedLabel.Font = new System.Drawing.Font("Maiandra GD", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.secondPassedLabel.Location = new System.Drawing.Point(606, 182);
            this.secondPassedLabel.Name = "secondPassedLabel";
            this.secondPassedLabel.Size = new System.Drawing.Size(286, 42);
            this.secondPassedLabel.TabIndex = 6;
            this.secondPassedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonSaveCrozzle
            // 
            this.buttonSaveCrozzle.BackColor = System.Drawing.Color.CornflowerBlue;
            this.buttonSaveCrozzle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.buttonSaveCrozzle.Cursor = System.Windows.Forms.Cursors.Default;
            this.buttonSaveCrozzle.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSaveCrozzle.ForeColor = System.Drawing.Color.Black;
            this.buttonSaveCrozzle.Location = new System.Drawing.Point(606, 256);
            this.buttonSaveCrozzle.Name = "buttonSaveCrozzle";
            this.buttonSaveCrozzle.Size = new System.Drawing.Size(286, 70);
            this.buttonSaveCrozzle.TabIndex = 1;
            this.buttonSaveCrozzle.Text = "Save this Crozzle";
            this.buttonSaveCrozzle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.buttonSaveCrozzle.Click += new System.EventHandler(this.buttonSaveCrozzle_Click);
            this.buttonSaveCrozzle.MouseLeave += new System.EventHandler(this.buttonSaveCrozzle_MouseLeave);
            this.buttonSaveCrozzle.MouseHover += new System.EventHandler(this.buttonSaveCrozzle_MouseHover);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(904, 542);
            this.Controls.Add(this.crozzleProgressBar);
            this.Controls.Add(this.secondPassedLabel);
            this.Controls.Add(this.pointLabel);
            this.Controls.Add(this.messageLabel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.crozzleDataGridView);
            this.Controls.Add(this.wordDataGridView);
            this.Controls.Add(this.buttonSaveCrozzle);
            this.Controls.Add(this.createNewCrozzle);
            this.Controls.Add(this.buttonLoadCrozzleList);
            this.Controls.Add(this.buttonLoadWordList);
            this.Controls.Add(this.menuStrip1);
            this.Location = new System.Drawing.Point(200, 100);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wordDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.crozzleDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem openWordListMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openCrozzleMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
        private System.Windows.Forms.Label buttonLoadWordList;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.DataGridView wordDataGridView;
        private System.Windows.Forms.Label buttonLoadCrozzleList;
        private System.Windows.Forms.DataGridView crozzleDataGridView;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label messageLabel;
        private System.Windows.Forms.Label pointLabel;
        private System.Windows.Forms.ProgressBar crozzleProgressBar;
        private System.Windows.Forms.Label createNewCrozzle;
        private System.Windows.Forms.ToolStripMenuItem createCrozzleMenuItem;
        private System.Windows.Forms.Label secondPassedLabel;
        private System.Windows.Forms.Label buttonSaveCrozzle;
    }
}

