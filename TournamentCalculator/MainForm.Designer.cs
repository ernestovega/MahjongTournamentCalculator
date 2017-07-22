namespace TournamentCalculator
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lblRounds = new System.Windows.Forms.Label();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.numUpDownRounds = new System.Windows.Forms.NumericUpDown();
            this.lblTriesMax = new System.Windows.Forms.Label();
            this.numUpDownTriesMax = new System.Windows.Forms.NumericUpDown();
            this.numUpDownPlayers = new System.Windows.Forms.NumericUpDown();
            this.labelPlayers = new System.Windows.Forms.Label();
            this.imgLogoMM = new System.Windows.Forms.PictureBox();
            this.imgLogoEMA = new System.Windows.Forms.PictureBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnMinimize = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.lblAuthor = new System.Windows.Forms.Label();
            this.progressBar = new MahjongTournamentCalculator.CustomViews.CustomProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownRounds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownTriesMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownPlayers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogoMM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogoEMA)).BeginInit();
            this.SuspendLayout();
            // 
            // lblRounds
            // 
            this.lblRounds.AutoSize = true;
            this.lblRounds.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblRounds.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.lblRounds.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblRounds.Location = new System.Drawing.Point(148, 249);
            this.lblRounds.Margin = new System.Windows.Forms.Padding(50, 10, 50, 10);
            this.lblRounds.Name = "lblRounds";
            this.lblRounds.Size = new System.Drawing.Size(55, 17);
            this.lblRounds.TabIndex = 3;
            this.lblRounds.Text = "Rounds";
            // 
            // btnCalculate
            // 
            this.btnCalculate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(106)))));
            this.btnCalculate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCalculate.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnCalculate.FlatAppearance.BorderSize = 0;
            this.btnCalculate.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnCalculate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.btnCalculate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(127)))), ((int)(((byte)(56)))));
            this.btnCalculate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCalculate.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnCalculate.ForeColor = System.Drawing.Color.White;
            this.btnCalculate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCalculate.Location = new System.Drawing.Point(129, 295);
            this.btnCalculate.Margin = new System.Windows.Forms.Padding(50, 10, 50, 10);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(174, 50);
            this.btnCalculate.TabIndex = 3;
            this.btnCalculate.Text = "Go!";
            this.btnCalculate.UseVisualStyleBackColor = false;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // numUpDownRounds
            // 
            this.numUpDownRounds.BackColor = System.Drawing.Color.White;
            this.numUpDownRounds.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numUpDownRounds.Cursor = System.Windows.Forms.Cursors.Default;
            this.numUpDownRounds.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.numUpDownRounds.Location = new System.Drawing.Point(212, 250);
            this.numUpDownRounds.Margin = new System.Windows.Forms.Padding(50, 10, 50, 10);
            this.numUpDownRounds.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numUpDownRounds.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpDownRounds.Name = "numUpDownRounds";
            this.numUpDownRounds.Size = new System.Drawing.Size(71, 19);
            this.numUpDownRounds.TabIndex = 2;
            this.numUpDownRounds.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numUpDownRounds.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // lblTriesMax
            // 
            this.lblTriesMax.AutoSize = true;
            this.lblTriesMax.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblTriesMax.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.lblTriesMax.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTriesMax.Location = new System.Drawing.Point(126, 363);
            this.lblTriesMax.Margin = new System.Windows.Forms.Padding(50, 10, 50, 10);
            this.lblTriesMax.Name = "lblTriesMax";
            this.lblTriesMax.Size = new System.Drawing.Size(73, 17);
            this.lblTriesMax.TabIndex = 21;
            this.lblTriesMax.Text = "Tries max.:";
            // 
            // numUpDownTriesMax
            // 
            this.numUpDownTriesMax.BackColor = System.Drawing.Color.White;
            this.numUpDownTriesMax.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numUpDownTriesMax.Cursor = System.Windows.Forms.Cursors.Default;
            this.numUpDownTriesMax.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.numUpDownTriesMax.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numUpDownTriesMax.Location = new System.Drawing.Point(203, 364);
            this.numUpDownTriesMax.Margin = new System.Windows.Forms.Padding(50, 10, 50, 10);
            this.numUpDownTriesMax.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numUpDownTriesMax.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpDownTriesMax.Name = "numUpDownTriesMax";
            this.numUpDownTriesMax.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numUpDownTriesMax.Size = new System.Drawing.Size(100, 19);
            this.numUpDownTriesMax.TabIndex = 4;
            this.numUpDownTriesMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numUpDownTriesMax.ThousandsSeparator = true;
            this.numUpDownTriesMax.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // numUpDownPlayers
            // 
            this.numUpDownPlayers.BackColor = System.Drawing.Color.White;
            this.numUpDownPlayers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numUpDownPlayers.Cursor = System.Windows.Forms.Cursors.Default;
            this.numUpDownPlayers.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.numUpDownPlayers.Increment = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numUpDownPlayers.Location = new System.Drawing.Point(212, 213);
            this.numUpDownPlayers.Margin = new System.Windows.Forms.Padding(50, 10, 50, 10);
            this.numUpDownPlayers.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numUpDownPlayers.Minimum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.numUpDownPlayers.Name = "numUpDownPlayers";
            this.numUpDownPlayers.Size = new System.Drawing.Size(71, 19);
            this.numUpDownPlayers.TabIndex = 1;
            this.numUpDownPlayers.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numUpDownPlayers.Value = new decimal(new int[] {
            80,
            0,
            0,
            0});
            // 
            // labelPlayers
            // 
            this.labelPlayers.AutoSize = true;
            this.labelPlayers.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelPlayers.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.labelPlayers.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelPlayers.Location = new System.Drawing.Point(148, 212);
            this.labelPlayers.Margin = new System.Windows.Forms.Padding(50, 50, 50, 10);
            this.labelPlayers.Name = "labelPlayers";
            this.labelPlayers.Size = new System.Drawing.Size(51, 17);
            this.labelPlayers.TabIndex = 46;
            this.labelPlayers.Text = "Players";
            // 
            // imgLogoMM
            // 
            this.imgLogoMM.ErrorImage = ((System.Drawing.Image)(resources.GetObject("imgLogoMM.ErrorImage")));
            this.imgLogoMM.Image = ((System.Drawing.Image)(resources.GetObject("imgLogoMM.Image")));
            this.imgLogoMM.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.imgLogoMM.Location = new System.Drawing.Point(53, 30);
            this.imgLogoMM.Margin = new System.Windows.Forms.Padding(3, 50, 3, 3);
            this.imgLogoMM.Name = "imgLogoMM";
            this.imgLogoMM.Size = new System.Drawing.Size(150, 150);
            this.imgLogoMM.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgLogoMM.TabIndex = 48;
            this.imgLogoMM.TabStop = false;
            // 
            // imgLogoEMA
            // 
            this.imgLogoEMA.ErrorImage = ((System.Drawing.Image)(resources.GetObject("imgLogoEMA.ErrorImage")));
            this.imgLogoEMA.Image = ((System.Drawing.Image)(resources.GetObject("imgLogoEMA.Image")));
            this.imgLogoEMA.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.imgLogoEMA.Location = new System.Drawing.Point(232, 30);
            this.imgLogoEMA.Margin = new System.Windows.Forms.Padding(3, 50, 3, 3);
            this.imgLogoEMA.Name = "imgLogoEMA";
            this.imgLogoEMA.Size = new System.Drawing.Size(150, 150);
            this.imgLogoEMA.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgLogoEMA.TabIndex = 62;
            this.imgLogoEMA.TabStop = false;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(106)))));
            this.btnExit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnExit.Location = new System.Drawing.Point(411, -1);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(20, 30);
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "X";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnMinimize
            // 
            this.btnMinimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMinimize.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnMinimize.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnMinimize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimize.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnMinimize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(106)))));
            this.btnMinimize.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMinimize.Location = new System.Drawing.Point(388, -3);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(20, 30);
            this.btnMinimize.TabIndex = 5;
            this.btnMinimize.Text = "_";
            this.btnMinimize.UseVisualStyleBackColor = true;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // lblAuthor
            // 
            this.lblAuthor.AutoSize = true;
            this.lblAuthor.BackColor = System.Drawing.Color.White;
            this.lblAuthor.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblAuthor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(106)))));
            this.lblAuthor.Location = new System.Drawing.Point(0, 408);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Padding = new System.Windows.Forms.Padding(6, 0, 0, 6);
            this.lblAuthor.Size = new System.Drawing.Size(272, 19);
            this.lblAuthor.TabIndex = 63;
            this.lblAuthor.Text = "Designed and developed by Ernesto Vega de la Iglesia";
            // 
            // progressBar
            // 
            this.progressBar.BackColor = System.Drawing.Color.White;
            this.progressBar.CustomText = null;
            this.progressBar.DisplayStyle = MahjongTournamentCalculator.CustomViews.ProgressBarDisplayText.Percentage;
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.progressBar.Location = new System.Drawing.Point(0, 427);
            this.progressBar.Maximum = 10000;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(434, 23);
            this.progressBar.Step = 1;
            this.progressBar.TabIndex = 61;
            this.progressBar.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(434, 450);
            this.Controls.Add(this.lblAuthor);
            this.Controls.Add(this.btnMinimize);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.imgLogoEMA);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.imgLogoMM);
            this.Controls.Add(this.numUpDownPlayers);
            this.Controls.Add(this.labelPlayers);
            this.Controls.Add(this.numUpDownTriesMax);
            this.Controls.Add(this.lblTriesMax);
            this.Controls.Add(this.numUpDownRounds);
            this.Controls.Add(this.btnCalculate);
            this.Controls.Add(this.lblRounds);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(434, 400);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mahjong Tournament Calculator";
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownRounds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownTriesMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownPlayers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogoMM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogoEMA)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblRounds;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.NumericUpDown numUpDownRounds;
        private System.Windows.Forms.Label lblTriesMax;
        private System.Windows.Forms.NumericUpDown numUpDownTriesMax;
        private System.Windows.Forms.NumericUpDown numUpDownPlayers;
        private System.Windows.Forms.Label labelPlayers;
        private System.Windows.Forms.PictureBox imgLogoMM;
        private MahjongTournamentCalculator.CustomViews.CustomProgressBar progressBar;
        private System.Windows.Forms.PictureBox imgLogoEMA;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnMinimize;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label lblAuthor;
    }
}

