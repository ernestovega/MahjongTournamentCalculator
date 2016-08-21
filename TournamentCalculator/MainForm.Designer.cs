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
            this.btnImportExcel = new System.Windows.Forms.Button();
            this.lblPlayers = new System.Windows.Forms.Label();
            this.lblTables = new System.Windows.Forms.Label();
            this.lblRounds = new System.Windows.Forms.Label();
            this.lblStep1 = new System.Windows.Forms.Label();
            this.lblStep2 = new System.Windows.Forms.Label();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.lblStep3 = new System.Windows.Forms.Label();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.numUpDownRounds = new System.Windows.Forms.NumericUpDown();
            this.lblStep4 = new System.Windows.Forms.Label();
            this.btnExportar = new System.Windows.Forms.Button();
            this.btnShowNames = new System.Windows.Forms.Button();
            this.btnShowTeams = new System.Windows.Forms.Button();
            this.btnShowCountries = new System.Windows.Forms.Button();
            this.btnShowAll = new System.Windows.Forms.Button();
            this.lblTriesMax = new System.Windows.Forms.Label();
            this.lblTriesNeeded = new System.Windows.Forms.Label();
            this.numUpDownTriesMax = new System.Windows.Forms.NumericUpDown();
            this.btnShowPlayers = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownRounds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownTriesMax)).BeginInit();
            this.SuspendLayout();
            // 
            // btnImportExcel
            // 
            resources.ApplyResources(this.btnImportExcel, "btnImportExcel");
            this.btnImportExcel.Name = "btnImportExcel";
            this.btnImportExcel.UseVisualStyleBackColor = true;
            this.btnImportExcel.Click += new System.EventHandler(this.btnImportExcel_Click);
            // 
            // lblPlayers
            // 
            resources.ApplyResources(this.lblPlayers, "lblPlayers");
            this.lblPlayers.Name = "lblPlayers";
            // 
            // lblTables
            // 
            resources.ApplyResources(this.lblTables, "lblTables");
            this.lblTables.Name = "lblTables";
            // 
            // lblRounds
            // 
            resources.ApplyResources(this.lblRounds, "lblRounds");
            this.lblRounds.Name = "lblRounds";
            // 
            // lblStep1
            // 
            resources.ApplyResources(this.lblStep1, "lblStep1");
            this.lblStep1.Name = "lblStep1";
            // 
            // lblStep2
            // 
            resources.ApplyResources(this.lblStep2, "lblStep2");
            this.lblStep2.Name = "lblStep2";
            // 
            // btnCalculate
            // 
            resources.ApplyResources(this.btnCalculate, "btnCalculate");
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // lblStep3
            // 
            resources.ApplyResources(this.lblStep3, "lblStep3");
            this.lblStep3.Name = "lblStep3";
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToOrderColumns = true;
            resources.ApplyResources(this.dataGridView, "dataGridView");
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            // 
            // numUpDownRounds
            // 
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
            // lblStep4
            // 
            resources.ApplyResources(this.lblStep4, "lblStep4");
            this.lblStep4.Name = "lblStep4";
            // 
            // btnExportar
            // 
            resources.ApplyResources(this.btnExportar, "btnExportar");
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.UseVisualStyleBackColor = true;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // btnShowNames
            // 
            resources.ApplyResources(this.btnShowNames, "btnShowNames");
            this.btnShowNames.Name = "btnShowNames";
            this.btnShowNames.UseVisualStyleBackColor = true;
            this.btnShowNames.Click += new System.EventHandler(this.btnShowNames_Click);
            // 
            // btnShowTeams
            // 
            resources.ApplyResources(this.btnShowTeams, "btnShowTeams");
            this.btnShowTeams.Name = "btnShowTeams";
            this.btnShowTeams.UseVisualStyleBackColor = true;
            this.btnShowTeams.Click += new System.EventHandler(this.btnShowTeams_Click);
            // 
            // btnShowCountries
            // 
            resources.ApplyResources(this.btnShowCountries, "btnShowCountries");
            this.btnShowCountries.Name = "btnShowCountries";
            this.btnShowCountries.UseVisualStyleBackColor = true;
            this.btnShowCountries.Click += new System.EventHandler(this.btnShowCountries_Click);
            // 
            // btnShowAll
            // 
            resources.ApplyResources(this.btnShowAll, "btnShowAll");
            this.btnShowAll.Name = "btnShowAll";
            this.btnShowAll.UseVisualStyleBackColor = true;
            this.btnShowAll.Click += new System.EventHandler(this.btnShowAll_Click);
            // 
            // lblTriesMax
            // 
            resources.ApplyResources(this.lblTriesMax, "lblTriesMax");
            this.lblTriesMax.Name = "lblTriesMax";
            // 
            // lblTriesNeeded
            // 
            resources.ApplyResources(this.lblTriesNeeded, "lblTriesNeeded");
            this.lblTriesNeeded.Name = "lblTriesNeeded";
            // 
            // numUpDownTriesMax
            // 
            resources.ApplyResources(this.numUpDownTriesMax, "numUpDownTriesMax");
            this.numUpDownTriesMax.Maximum = new decimal(new int[] {
            1000000000,
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
            // btnShowPlayers
            // 
            resources.ApplyResources(this.btnShowPlayers, "btnShowPlayers");
            this.btnShowPlayers.Name = "btnShowPlayers";
            this.btnShowPlayers.UseVisualStyleBackColor = true;
            this.btnShowPlayers.Click += new System.EventHandler(this.btnShowPlayers_Click);
            // 
            // checkBox1
            // 
            resources.ApplyResources(this.checkBox1, "checkBox1");
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            resources.ApplyResources(this.checkBox2, "checkBox2");
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            resources.ApplyResources(this.checkBox3, "checkBox3");
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            resources.ApplyResources(this.checkBox4, "checkBox4");
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBox4);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.btnShowPlayers);
            this.Controls.Add(this.numUpDownTriesMax);
            this.Controls.Add(this.lblTriesNeeded);
            this.Controls.Add(this.lblTriesMax);
            this.Controls.Add(this.btnShowAll);
            this.Controls.Add(this.btnShowCountries);
            this.Controls.Add(this.btnShowTeams);
            this.Controls.Add(this.btnShowNames);
            this.Controls.Add(this.lblStep4);
            this.Controls.Add(this.btnExportar);
            this.Controls.Add(this.numUpDownRounds);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.lblStep3);
            this.Controls.Add(this.btnCalculate);
            this.Controls.Add(this.lblStep2);
            this.Controls.Add(this.lblStep1);
            this.Controls.Add(this.lblRounds);
            this.Controls.Add(this.lblTables);
            this.Controls.Add(this.lblPlayers);
            this.Controls.Add(this.btnImportExcel);
            this.DoubleBuffered = true;
            this.Name = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownRounds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownTriesMax)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnImportExcel;
        private System.Windows.Forms.Label lblPlayers;
        private System.Windows.Forms.Label lblTables;
        private System.Windows.Forms.Label lblRounds;
        private System.Windows.Forms.Label lblStep1;
        private System.Windows.Forms.Label lblStep2;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.Label lblStep3;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.NumericUpDown numUpDownRounds;
        private System.Windows.Forms.Label lblStep4;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.Button btnShowNames;
        private System.Windows.Forms.Button btnShowTeams;
        private System.Windows.Forms.Button btnShowCountries;
        private System.Windows.Forms.Button btnShowAll;
        private System.Windows.Forms.Label lblTriesMax;
        private System.Windows.Forms.Label lblTriesNeeded;
        private System.Windows.Forms.NumericUpDown numUpDownTriesMax;
        private System.Windows.Forms.Button btnShowPlayers;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox4;
    }
}

