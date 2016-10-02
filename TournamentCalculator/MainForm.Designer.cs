﻿namespace TournamentCalculator
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
            this.progressBar = new MahjongTournamentCalculator.CustomViews.CustomProgressBar();
            this.imgLogoEMA = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownRounds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownTriesMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownPlayers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogoMM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogoEMA)).BeginInit();
            this.SuspendLayout();
            // 
            // lblRounds
            // 
            resources.ApplyResources(this.lblRounds, "lblRounds");
            this.lblRounds.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblRounds.Name = "lblRounds";
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
            resources.ApplyResources(this.btnCalculate, "btnCalculate");
            this.btnCalculate.ForeColor = System.Drawing.Color.White;
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.UseVisualStyleBackColor = false;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // numUpDownRounds
            // 
            this.numUpDownRounds.BackColor = System.Drawing.Color.White;
            this.numUpDownRounds.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numUpDownRounds.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.numUpDownRounds, "numUpDownRounds");
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
            this.numUpDownRounds.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // lblTriesMax
            // 
            resources.ApplyResources(this.lblTriesMax, "lblTriesMax");
            this.lblTriesMax.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblTriesMax.Name = "lblTriesMax";
            // 
            // numUpDownTriesMax
            // 
            this.numUpDownTriesMax.BackColor = System.Drawing.Color.White;
            this.numUpDownTriesMax.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numUpDownTriesMax.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.numUpDownTriesMax, "numUpDownTriesMax");
            this.numUpDownTriesMax.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
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
            resources.ApplyResources(this.numUpDownPlayers, "numUpDownPlayers");
            this.numUpDownPlayers.Increment = new decimal(new int[] {
            4,
            0,
            0,
            0});
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
            this.numUpDownPlayers.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // labelPlayers
            // 
            resources.ApplyResources(this.labelPlayers, "labelPlayers");
            this.labelPlayers.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelPlayers.Name = "labelPlayers";
            // 
            // imgLogoMM
            // 
            resources.ApplyResources(this.imgLogoMM, "imgLogoMM");
            this.imgLogoMM.Name = "imgLogoMM";
            this.imgLogoMM.TabStop = false;
            // 
            // progressBar
            // 
            resources.ApplyResources(this.progressBar, "progressBar");
            this.progressBar.MarqueeAnimationSpeed = 1;
            this.progressBar.Maximum = 10;
            this.progressBar.Name = "progressBar";
            this.progressBar.Step = 1;
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            // 
            // imgLogoEMA
            // 
            resources.ApplyResources(this.imgLogoEMA, "imgLogoEMA");
            this.imgLogoEMA.Name = "imgLogoEMA";
            this.imgLogoEMA.TabStop = false;
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
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
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.TopMost = true;
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
    }
}

