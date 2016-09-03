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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblPlayers = new System.Windows.Forms.Label();
            this.lblTables = new System.Windows.Forms.Label();
            this.lblRounds = new System.Windows.Forms.Label();
            this.lblStep1 = new System.Windows.Forms.Label();
            this.lblStep2 = new System.Windows.Forms.Label();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.numUpDownRounds = new System.Windows.Forms.NumericUpDown();
            this.lblStep3 = new System.Windows.Forms.Label();
            this.btnExportTournament = new System.Windows.Forms.Button();
            this.btnShowNames = new System.Windows.Forms.Button();
            this.btnShowTeams = new System.Windows.Forms.Button();
            this.btnShowCountries = new System.Windows.Forms.Button();
            this.btnShowIds = new System.Windows.Forms.Button();
            this.lblTriesMax = new System.Windows.Forms.Label();
            this.lblTriesNeeded = new System.Windows.Forms.Label();
            this.numUpDownTriesMax = new System.Windows.Forms.NumericUpDown();
            this.btnShowPlayers = new System.Windows.Forms.Button();
            this.chckBxNames = new System.Windows.Forms.CheckBox();
            this.chckBxTeams = new System.Windows.Forms.CheckBox();
            this.chckBxCountries = new System.Windows.Forms.CheckBox();
            this.chckBxIds = new System.Windows.Forms.CheckBox();
            this.btnGetExcelTemplate = new System.Windows.Forms.Button();
            this.btnImportExcel = new System.Windows.Forms.Button();
            this.btnCheckDuplicateRivals = new System.Windows.Forms.Button();
            this.customProgressBar = new MahjongTournamentCalculator.CustomViews.CustomProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.btnExportScoringTables = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownRounds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownTriesMax)).BeginInit();
            this.SuspendLayout();
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
            this.btnCalculate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnCalculate.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnCalculate, "btnCalculate");
            this.btnCalculate.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnCalculate.FlatAppearance.BorderSize = 0;
            this.btnCalculate.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnCalculate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.btnCalculate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(127)))), ((int)(((byte)(56)))));
            this.btnCalculate.ForeColor = System.Drawing.SystemColors.GrayText;
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.UseVisualStyleBackColor = false;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(106)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            this.dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.dataGridView, "dataGridView");
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(106)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(106)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView.GridColor = System.Drawing.Color.White;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.NullValue = null;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(106)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(106)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White;
            this.dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGridView.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(106)))));
            this.dataGridView.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            this.dataGridView.RowTemplate.Height = 24;
            this.dataGridView.RowTemplate.ReadOnly = true;
            this.dataGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.ShowCellErrors = false;
            this.dataGridView.ShowCellToolTips = false;
            this.dataGridView.ShowEditingIcon = false;
            this.dataGridView.ShowRowErrors = false;
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
            // lblStep3
            // 
            resources.ApplyResources(this.lblStep3, "lblStep3");
            this.lblStep3.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblStep3.Name = "lblStep3";
            // 
            // btnExportTournament
            // 
            this.btnExportTournament.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnExportTournament.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnExportTournament, "btnExportTournament");
            this.btnExportTournament.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnExportTournament.FlatAppearance.BorderSize = 0;
            this.btnExportTournament.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnExportTournament.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.btnExportTournament.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(127)))), ((int)(((byte)(56)))));
            this.btnExportTournament.ForeColor = System.Drawing.SystemColors.GrayText;
            this.btnExportTournament.Name = "btnExportTournament";
            this.btnExportTournament.UseVisualStyleBackColor = false;
            this.btnExportTournament.Click += new System.EventHandler(this.btnExportTournament_Click);
            // 
            // btnShowNames
            // 
            resources.ApplyResources(this.btnShowNames, "btnShowNames");
            this.btnShowNames.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnShowNames.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShowNames.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnShowNames.FlatAppearance.BorderSize = 0;
            this.btnShowNames.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.btnShowNames.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(127)))), ((int)(((byte)(56)))));
            this.btnShowNames.ForeColor = System.Drawing.SystemColors.GrayText;
            this.btnShowNames.Name = "btnShowNames";
            this.btnShowNames.UseVisualStyleBackColor = false;
            this.btnShowNames.Click += new System.EventHandler(this.btnShowNames_Click);
            // 
            // btnShowTeams
            // 
            resources.ApplyResources(this.btnShowTeams, "btnShowTeams");
            this.btnShowTeams.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnShowTeams.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShowTeams.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnShowTeams.FlatAppearance.BorderSize = 0;
            this.btnShowTeams.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.btnShowTeams.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(127)))), ((int)(((byte)(56)))));
            this.btnShowTeams.ForeColor = System.Drawing.SystemColors.GrayText;
            this.btnShowTeams.Name = "btnShowTeams";
            this.btnShowTeams.UseVisualStyleBackColor = false;
            this.btnShowTeams.Click += new System.EventHandler(this.btnShowTeams_Click);
            // 
            // btnShowCountries
            // 
            resources.ApplyResources(this.btnShowCountries, "btnShowCountries");
            this.btnShowCountries.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnShowCountries.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShowCountries.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnShowCountries.FlatAppearance.BorderSize = 0;
            this.btnShowCountries.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.btnShowCountries.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(127)))), ((int)(((byte)(56)))));
            this.btnShowCountries.ForeColor = System.Drawing.SystemColors.GrayText;
            this.btnShowCountries.Name = "btnShowCountries";
            this.btnShowCountries.UseVisualStyleBackColor = false;
            this.btnShowCountries.Click += new System.EventHandler(this.btnShowCountries_Click);
            // 
            // btnShowIds
            // 
            resources.ApplyResources(this.btnShowIds, "btnShowIds");
            this.btnShowIds.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnShowIds.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShowIds.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnShowIds.FlatAppearance.BorderSize = 0;
            this.btnShowIds.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.btnShowIds.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(127)))), ((int)(((byte)(56)))));
            this.btnShowIds.ForeColor = System.Drawing.SystemColors.GrayText;
            this.btnShowIds.Name = "btnShowIds";
            this.btnShowIds.UseVisualStyleBackColor = false;
            this.btnShowIds.Click += new System.EventHandler(this.btnShowIds_Click);
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
            this.lblTriesNeeded.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblTriesNeeded.ForeColor = System.Drawing.Color.Black;
            this.lblTriesNeeded.Name = "lblTriesNeeded";
            // 
            // numUpDownTriesMax
            // 
            resources.ApplyResources(this.numUpDownTriesMax, "numUpDownTriesMax");
            this.numUpDownTriesMax.BackColor = System.Drawing.Color.White;
            this.numUpDownTriesMax.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numUpDownTriesMax.Cursor = System.Windows.Forms.Cursors.Default;
            this.numUpDownTriesMax.Maximum = new decimal(new int[] {
            999999999,
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
            this.btnShowPlayers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnShowPlayers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShowPlayers.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnShowPlayers.FlatAppearance.BorderSize = 0;
            this.btnShowPlayers.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.btnShowPlayers.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(127)))), ((int)(((byte)(56)))));
            this.btnShowPlayers.ForeColor = System.Drawing.SystemColors.GrayText;
            this.btnShowPlayers.Name = "btnShowPlayers";
            this.btnShowPlayers.UseVisualStyleBackColor = false;
            this.btnShowPlayers.Click += new System.EventHandler(this.btnShowPlayers_Click);
            // 
            // chckBxNames
            // 
            resources.ApplyResources(this.chckBxNames, "chckBxNames");
            this.chckBxNames.Checked = true;
            this.chckBxNames.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chckBxNames.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chckBxNames.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chckBxNames.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chckBxNames.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chckBxNames.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chckBxNames.Name = "chckBxNames";
            this.chckBxNames.UseVisualStyleBackColor = true;
            this.chckBxNames.CheckedChanged += new System.EventHandler(this.chckBxNames_CheckedChanged);
            // 
            // chckBxTeams
            // 
            resources.ApplyResources(this.chckBxTeams, "chckBxTeams");
            this.chckBxTeams.Checked = true;
            this.chckBxTeams.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chckBxTeams.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chckBxTeams.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chckBxTeams.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chckBxTeams.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chckBxTeams.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chckBxTeams.Name = "chckBxTeams";
            this.chckBxTeams.UseVisualStyleBackColor = true;
            this.chckBxTeams.CheckedChanged += new System.EventHandler(this.chckBxTeams_CheckedChanged);
            // 
            // chckBxCountries
            // 
            resources.ApplyResources(this.chckBxCountries, "chckBxCountries");
            this.chckBxCountries.Checked = true;
            this.chckBxCountries.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chckBxCountries.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chckBxCountries.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chckBxCountries.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chckBxCountries.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chckBxCountries.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chckBxCountries.Name = "chckBxCountries";
            this.chckBxCountries.UseVisualStyleBackColor = true;
            this.chckBxCountries.CheckedChanged += new System.EventHandler(this.chckBxCountries_CheckedChanged);
            // 
            // chckBxIds
            // 
            resources.ApplyResources(this.chckBxIds, "chckBxIds");
            this.chckBxIds.Checked = true;
            this.chckBxIds.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chckBxIds.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chckBxIds.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chckBxIds.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chckBxIds.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chckBxIds.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chckBxIds.Name = "chckBxIds";
            this.chckBxIds.UseVisualStyleBackColor = true;
            this.chckBxIds.CheckedChanged += new System.EventHandler(this.chckBxIds_CheckedChanged);
            // 
            // btnGetExcelTemplate
            // 
            this.btnGetExcelTemplate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(106)))));
            this.btnGetExcelTemplate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGetExcelTemplate.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnGetExcelTemplate.FlatAppearance.BorderSize = 0;
            this.btnGetExcelTemplate.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnGetExcelTemplate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.btnGetExcelTemplate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(127)))), ((int)(((byte)(56)))));
            resources.ApplyResources(this.btnGetExcelTemplate, "btnGetExcelTemplate");
            this.btnGetExcelTemplate.ForeColor = System.Drawing.Color.White;
            this.btnGetExcelTemplate.Name = "btnGetExcelTemplate";
            this.btnGetExcelTemplate.UseVisualStyleBackColor = false;
            this.btnGetExcelTemplate.Click += new System.EventHandler(this.btnGetExcelTemplate_Click);
            // 
            // btnImportExcel
            // 
            this.btnImportExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(106)))));
            this.btnImportExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImportExcel.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnImportExcel.FlatAppearance.BorderSize = 0;
            this.btnImportExcel.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnImportExcel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.btnImportExcel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(127)))), ((int)(((byte)(56)))));
            resources.ApplyResources(this.btnImportExcel, "btnImportExcel");
            this.btnImportExcel.ForeColor = System.Drawing.Color.White;
            this.btnImportExcel.Name = "btnImportExcel";
            this.btnImportExcel.UseVisualStyleBackColor = false;
            this.btnImportExcel.Click += new System.EventHandler(this.btnImportExcel_Click);
            // 
            // btnCheckDuplicateRivals
            // 
            this.btnCheckDuplicateRivals.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnCheckDuplicateRivals.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnCheckDuplicateRivals, "btnCheckDuplicateRivals");
            this.btnCheckDuplicateRivals.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnCheckDuplicateRivals.FlatAppearance.BorderSize = 0;
            this.btnCheckDuplicateRivals.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnCheckDuplicateRivals.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.btnCheckDuplicateRivals.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(127)))), ((int)(((byte)(56)))));
            this.btnCheckDuplicateRivals.ForeColor = System.Drawing.SystemColors.GrayText;
            this.btnCheckDuplicateRivals.Name = "btnCheckDuplicateRivals";
            this.btnCheckDuplicateRivals.UseVisualStyleBackColor = false;
            this.btnCheckDuplicateRivals.Click += new System.EventHandler(this.btnCheckDuplicateRivals_Click);
            // 
            // customProgressBar
            // 
            this.customProgressBar.CustomText = null;
            this.customProgressBar.DisplayStyle = MahjongTournamentCalculator.CustomViews.ProgressBarDisplayText.Percentage;
            resources.ApplyResources(this.customProgressBar, "customProgressBar");
            this.customProgressBar.Maximum = 10000;
            this.customProgressBar.Name = "customProgressBar";
            this.customProgressBar.Step = 1;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.label1.Name = "label1";
            // 
            // btnExportScoringTables
            // 
            this.btnExportScoringTables.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnExportScoringTables.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnExportScoringTables, "btnExportScoringTables");
            this.btnExportScoringTables.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnExportScoringTables.FlatAppearance.BorderSize = 0;
            this.btnExportScoringTables.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnExportScoringTables.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.btnExportScoringTables.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(127)))), ((int)(((byte)(56)))));
            this.btnExportScoringTables.ForeColor = System.Drawing.SystemColors.GrayText;
            this.btnExportScoringTables.Name = "btnExportScoringTables";
            this.btnExportScoringTables.UseVisualStyleBackColor = false;
            this.btnExportScoringTables.Click += new System.EventHandler(this.btnExportScoringTables_Click);
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnExportScoringTables);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCheckDuplicateRivals);
            this.Controls.Add(this.customProgressBar);
            this.Controls.Add(this.btnGetExcelTemplate);
            this.Controls.Add(this.chckBxIds);
            this.Controls.Add(this.chckBxCountries);
            this.Controls.Add(this.chckBxTeams);
            this.Controls.Add(this.chckBxNames);
            this.Controls.Add(this.btnShowPlayers);
            this.Controls.Add(this.numUpDownTriesMax);
            this.Controls.Add(this.lblTriesNeeded);
            this.Controls.Add(this.lblTriesMax);
            this.Controls.Add(this.btnShowIds);
            this.Controls.Add(this.btnShowCountries);
            this.Controls.Add(this.btnShowTeams);
            this.Controls.Add(this.btnShowNames);
            this.Controls.Add(this.lblStep3);
            this.Controls.Add(this.btnExportTournament);
            this.Controls.Add(this.numUpDownRounds);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.btnCalculate);
            this.Controls.Add(this.lblStep2);
            this.Controls.Add(this.lblStep1);
            this.Controls.Add(this.lblRounds);
            this.Controls.Add(this.lblTables);
            this.Controls.Add(this.lblPlayers);
            this.Controls.Add(this.btnImportExcel);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownRounds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownTriesMax)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblPlayers;
        private System.Windows.Forms.Label lblTables;
        private System.Windows.Forms.Label lblRounds;
        private System.Windows.Forms.Label lblStep1;
        private System.Windows.Forms.Label lblStep2;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.NumericUpDown numUpDownRounds;
        private System.Windows.Forms.Label lblStep3;
        private System.Windows.Forms.Button btnExportTournament;
        private System.Windows.Forms.Button btnShowNames;
        private System.Windows.Forms.Button btnShowTeams;
        private System.Windows.Forms.Button btnShowCountries;
        private System.Windows.Forms.Button btnShowIds;
        private System.Windows.Forms.Label lblTriesMax;
        private System.Windows.Forms.Label lblTriesNeeded;
        private System.Windows.Forms.NumericUpDown numUpDownTriesMax;
        private System.Windows.Forms.Button btnShowPlayers;
        private System.Windows.Forms.CheckBox chckBxNames;
        private System.Windows.Forms.CheckBox chckBxTeams;
        private System.Windows.Forms.CheckBox chckBxCountries;
        private System.Windows.Forms.CheckBox chckBxIds;
        private System.Windows.Forms.Button btnGetExcelTemplate;
        private System.Windows.Forms.Button btnImportExcel;
        private MahjongTournamentCalculator.CustomViews.CustomProgressBar customProgressBar;
        private System.Windows.Forms.Button btnCheckDuplicateRivals;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnExportScoringTables;
    }
}

