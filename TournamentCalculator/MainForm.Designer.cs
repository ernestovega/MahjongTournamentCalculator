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
            this.btnExport = new System.Windows.Forms.Button();
            this.btnShowNames = new System.Windows.Forms.Button();
            this.btnShowTeams = new System.Windows.Forms.Button();
            this.btnShowCountries = new System.Windows.Forms.Button();
            this.btnShowAll = new System.Windows.Forms.Button();
            this.lblTriesMax = new System.Windows.Forms.Label();
            this.lblTriesNeeded = new System.Windows.Forms.Label();
            this.numUpDownTriesMax = new System.Windows.Forms.NumericUpDown();
            this.btnShowPlayers = new System.Windows.Forms.Button();
            this.chckBxNames = new System.Windows.Forms.CheckBox();
            this.chckBxTeams = new System.Windows.Forms.CheckBox();
            this.chckBxCountries = new System.Windows.Forms.CheckBox();
            this.chckBxIds = new System.Windows.Forms.CheckBox();
            this.lblInclude = new System.Windows.Forms.Label();
            this.btnGetExcelTemplate = new System.Windows.Forms.Button();
            this.btnShowByPlayers = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownRounds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownTriesMax)).BeginInit();
            this.SuspendLayout();
            // 
            // btnImportExcel
            // 
            this.btnImportExcel.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.btnImportExcel, "btnImportExcel");
            this.btnImportExcel.Name = "btnImportExcel";
            this.btnImportExcel.UseVisualStyleBackColor = true;
            this.btnImportExcel.Click += new System.EventHandler(this.btnImportExcel_Click);
            // 
            // lblPlayers
            // 
            resources.ApplyResources(this.lblPlayers, "lblPlayers");
            this.lblPlayers.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblPlayers.Name = "lblPlayers";
            // 
            // lblTables
            // 
            resources.ApplyResources(this.lblTables, "lblTables");
            this.lblTables.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblTables.Name = "lblTables";
            // 
            // lblRounds
            // 
            resources.ApplyResources(this.lblRounds, "lblRounds");
            this.lblRounds.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblRounds.Name = "lblRounds";
            // 
            // lblStep1
            // 
            resources.ApplyResources(this.lblStep1, "lblStep1");
            this.lblStep1.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblStep1.Name = "lblStep1";
            // 
            // lblStep2
            // 
            resources.ApplyResources(this.lblStep2, "lblStep2");
            this.lblStep2.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblStep2.Name = "lblStep2";
            // 
            // btnCalculate
            // 
            this.btnCalculate.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.btnCalculate, "btnCalculate");
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // lblStep3
            // 
            resources.ApplyResources(this.lblStep3, "lblStep3");
            this.lblStep3.Cursor = System.Windows.Forms.Cursors.Default;
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
            this.dataGridView.Cursor = System.Windows.Forms.Cursors.Default;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            // 
            // numUpDownRounds
            // 
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
            // lblStep4
            // 
            resources.ApplyResources(this.lblStep4, "lblStep4");
            this.lblStep4.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblStep4.Name = "lblStep4";
            // 
            // btnExport
            // 
            this.btnExport.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.btnExport, "btnExport");
            this.btnExport.Name = "btnExport";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnShowNames
            // 
            this.btnShowNames.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.btnShowNames, "btnShowNames");
            this.btnShowNames.Name = "btnShowNames";
            this.btnShowNames.UseVisualStyleBackColor = true;
            this.btnShowNames.Click += new System.EventHandler(this.btnShowNames_Click);
            // 
            // btnShowTeams
            // 
            this.btnShowTeams.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.btnShowTeams, "btnShowTeams");
            this.btnShowTeams.Name = "btnShowTeams";
            this.btnShowTeams.UseVisualStyleBackColor = true;
            this.btnShowTeams.Click += new System.EventHandler(this.btnShowTeams_Click);
            // 
            // btnShowCountries
            // 
            this.btnShowCountries.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.btnShowCountries, "btnShowCountries");
            this.btnShowCountries.Name = "btnShowCountries";
            this.btnShowCountries.UseVisualStyleBackColor = true;
            this.btnShowCountries.Click += new System.EventHandler(this.btnShowCountries_Click);
            // 
            // btnShowAll
            // 
            this.btnShowAll.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.btnShowAll, "btnShowAll");
            this.btnShowAll.Name = "btnShowAll";
            this.btnShowAll.UseVisualStyleBackColor = true;
            this.btnShowAll.Click += new System.EventHandler(this.btnShowAll_Click);
            // 
            // lblTriesMax
            // 
            resources.ApplyResources(this.lblTriesMax, "lblTriesMax");
            this.lblTriesMax.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblTriesMax.Name = "lblTriesMax";
            // 
            // lblTriesNeeded
            // 
            resources.ApplyResources(this.lblTriesNeeded, "lblTriesNeeded");
            this.lblTriesNeeded.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblTriesNeeded.Name = "lblTriesNeeded";
            // 
            // numUpDownTriesMax
            // 
            this.numUpDownTriesMax.Cursor = System.Windows.Forms.Cursors.Default;
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
            this.btnShowPlayers.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.btnShowPlayers, "btnShowPlayers");
            this.btnShowPlayers.Name = "btnShowPlayers";
            this.btnShowPlayers.UseVisualStyleBackColor = true;
            this.btnShowPlayers.Click += new System.EventHandler(this.btnShowPlayers_Click);
            // 
            // chckBxNames
            // 
            resources.ApplyResources(this.chckBxNames, "chckBxNames");
            this.chckBxNames.Checked = true;
            this.chckBxNames.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chckBxNames.Cursor = System.Windows.Forms.Cursors.Default;
            this.chckBxNames.Name = "chckBxNames";
            this.chckBxNames.UseVisualStyleBackColor = true;
            this.chckBxNames.CheckedChanged += new System.EventHandler(this.chckBxNames_CheckedChanged);
            // 
            // chckBxTeams
            // 
            resources.ApplyResources(this.chckBxTeams, "chckBxTeams");
            this.chckBxTeams.Checked = true;
            this.chckBxTeams.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chckBxTeams.Cursor = System.Windows.Forms.Cursors.Default;
            this.chckBxTeams.Name = "chckBxTeams";
            this.chckBxTeams.UseVisualStyleBackColor = true;
            this.chckBxTeams.CheckedChanged += new System.EventHandler(this.chckBxTeams_CheckedChanged);
            // 
            // chckBxCountries
            // 
            resources.ApplyResources(this.chckBxCountries, "chckBxCountries");
            this.chckBxCountries.Checked = true;
            this.chckBxCountries.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chckBxCountries.Cursor = System.Windows.Forms.Cursors.Default;
            this.chckBxCountries.Name = "chckBxCountries";
            this.chckBxCountries.UseVisualStyleBackColor = true;
            this.chckBxCountries.CheckedChanged += new System.EventHandler(this.chckBxCountries_CheckedChanged);
            // 
            // chckBxIds
            // 
            resources.ApplyResources(this.chckBxIds, "chckBxIds");
            this.chckBxIds.Checked = true;
            this.chckBxIds.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chckBxIds.Cursor = System.Windows.Forms.Cursors.Default;
            this.chckBxIds.Name = "chckBxIds";
            this.chckBxIds.UseVisualStyleBackColor = true;
            this.chckBxIds.CheckedChanged += new System.EventHandler(this.chckBxIds_CheckedChanged);
            // 
            // lblInclude
            // 
            resources.ApplyResources(this.lblInclude, "lblInclude");
            this.lblInclude.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblInclude.Name = "lblInclude";
            // 
            // btnGetExcelTemplate
            // 
            this.btnGetExcelTemplate.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.btnGetExcelTemplate, "btnGetExcelTemplate");
            this.btnGetExcelTemplate.Name = "btnGetExcelTemplate";
            this.btnGetExcelTemplate.UseVisualStyleBackColor = true;
            this.btnGetExcelTemplate.Click += new System.EventHandler(this.btnGetExcelTemplate_Click);
            // 
            // btnShowByPlayers
            // 
            this.btnShowByPlayers.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.btnShowByPlayers, "btnShowByPlayers");
            this.btnShowByPlayers.Name = "btnShowByPlayers";
            this.btnShowByPlayers.UseVisualStyleBackColor = true;
            this.btnShowByPlayers.Click += new System.EventHandler(this.btnShowByPlayers_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.label1.Name = "label1";
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnShowByPlayers);
            this.Controls.Add(this.btnGetExcelTemplate);
            this.Controls.Add(this.lblInclude);
            this.Controls.Add(this.chckBxIds);
            this.Controls.Add(this.chckBxCountries);
            this.Controls.Add(this.chckBxTeams);
            this.Controls.Add(this.chckBxNames);
            this.Controls.Add(this.btnShowPlayers);
            this.Controls.Add(this.numUpDownTriesMax);
            this.Controls.Add(this.lblTriesNeeded);
            this.Controls.Add(this.lblTriesMax);
            this.Controls.Add(this.btnShowAll);
            this.Controls.Add(this.btnShowCountries);
            this.Controls.Add(this.btnShowTeams);
            this.Controls.Add(this.btnShowNames);
            this.Controls.Add(this.lblStep4);
            this.Controls.Add(this.btnExport);
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
            this.Cursor = System.Windows.Forms.Cursors.Default;
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
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnShowNames;
        private System.Windows.Forms.Button btnShowTeams;
        private System.Windows.Forms.Button btnShowCountries;
        private System.Windows.Forms.Button btnShowAll;
        private System.Windows.Forms.Label lblTriesMax;
        private System.Windows.Forms.Label lblTriesNeeded;
        private System.Windows.Forms.NumericUpDown numUpDownTriesMax;
        private System.Windows.Forms.Button btnShowPlayers;
        private System.Windows.Forms.CheckBox chckBxNames;
        private System.Windows.Forms.CheckBox chckBxTeams;
        private System.Windows.Forms.CheckBox chckBxCountries;
        private System.Windows.Forms.CheckBox chckBxIds;
        private System.Windows.Forms.Label lblInclude;
        private System.Windows.Forms.Button btnGetExcelTemplate;
        private System.Windows.Forms.Button btnShowByPlayers;
        private System.Windows.Forms.Label label1;
    }
}

