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
            this.btnFindDuplicates = new System.Windows.Forms.Button();
            this.btnPlayerRivals = new System.Windows.Forms.Button();
            this.lblOptional = new System.Windows.Forms.Label();
            this.btnShowNames = new System.Windows.Forms.Button();
            this.btnShowTeams = new System.Windows.Forms.Button();
            this.btnShowCountries = new System.Windows.Forms.Button();
            this.btnShowAll = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownRounds)).BeginInit();
            this.SuspendLayout();
            // 
            // btnImportExcel
            // 
            this.btnImportExcel.Location = new System.Drawing.Point(78, 15);
            this.btnImportExcel.Name = "btnImportExcel";
            this.btnImportExcel.Size = new System.Drawing.Size(157, 50);
            this.btnImportExcel.TabIndex = 0;
            this.btnImportExcel.Text = "Import players from Excel ";
            this.btnImportExcel.UseVisualStyleBackColor = true;
            this.btnImportExcel.Click += new System.EventHandler(this.btnImportExcel_Click);
            // 
            // lblPlayers
            // 
            this.lblPlayers.AutoSize = true;
            this.lblPlayers.Location = new System.Drawing.Point(75, 68);
            this.lblPlayers.Name = "lblPlayers";
            this.lblPlayers.Size = new System.Drawing.Size(0, 13);
            this.lblPlayers.TabIndex = 1;
            // 
            // lblTables
            // 
            this.lblTables.AutoSize = true;
            this.lblTables.Location = new System.Drawing.Point(155, 68);
            this.lblTables.Name = "lblTables";
            this.lblTables.Size = new System.Drawing.Size(0, 13);
            this.lblTables.TabIndex = 2;
            // 
            // lblRounds
            // 
            this.lblRounds.AutoSize = true;
            this.lblRounds.Location = new System.Drawing.Point(75, 102);
            this.lblRounds.Name = "lblRounds";
            this.lblRounds.Size = new System.Drawing.Size(98, 13);
            this.lblRounds.TabIndex = 3;
            this.lblRounds.Text = "How many rounds?";
            // 
            // lblStep1
            // 
            this.lblStep1.AutoSize = true;
            this.lblStep1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStep1.Location = new System.Drawing.Point(15, 15);
            this.lblStep1.Name = "lblStep1";
            this.lblStep1.Size = new System.Drawing.Size(44, 13);
            this.lblStep1.TabIndex = 5;
            this.lblStep1.Text = "Step 1";
            // 
            // lblStep2
            // 
            this.lblStep2.AutoSize = true;
            this.lblStep2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStep2.Location = new System.Drawing.Point(15, 102);
            this.lblStep2.Name = "lblStep2";
            this.lblStep2.Size = new System.Drawing.Size(44, 13);
            this.lblStep2.TabIndex = 6;
            this.lblStep2.Text = "Step 2";
            // 
            // btnCalculate
            // 
            this.btnCalculate.Enabled = false;
            this.btnCalculate.Location = new System.Drawing.Point(78, 135);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(157, 50);
            this.btnCalculate.TabIndex = 7;
            this.btnCalculate.Text = "Calculate";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // lblStep3
            // 
            this.lblStep3.AutoSize = true;
            this.lblStep3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStep3.Location = new System.Drawing.Point(15, 135);
            this.lblStep3.Name = "lblStep3";
            this.lblStep3.Size = new System.Drawing.Size(44, 13);
            this.lblStep3.TabIndex = 8;
            this.lblStep3.Text = "Step 3";
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToOrderColumns = true;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(279, 44);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridView.Size = new System.Drawing.Size(522, 363);
            this.dataGridView.TabIndex = 10;
            // 
            // numUpDownRounds
            // 
            this.numUpDownRounds.Location = new System.Drawing.Point(185, 99);
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
            this.numUpDownRounds.Size = new System.Drawing.Size(50, 20);
            this.numUpDownRounds.TabIndex = 11;
            this.numUpDownRounds.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // lblStep4
            // 
            this.lblStep4.AutoSize = true;
            this.lblStep4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStep4.Location = new System.Drawing.Point(15, 376);
            this.lblStep4.Name = "lblStep4";
            this.lblStep4.Size = new System.Drawing.Size(44, 13);
            this.lblStep4.TabIndex = 13;
            this.lblStep4.Text = "Step 4";
            // 
            // btnExportar
            // 
            this.btnExportar.Enabled = false;
            this.btnExportar.Location = new System.Drawing.Point(78, 357);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(157, 50);
            this.btnExportar.TabIndex = 12;
            this.btnExportar.Text = "Export tables to Excel";
            this.btnExportar.UseVisualStyleBackColor = true;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // btnFindDuplicates
            // 
            this.btnFindDuplicates.Enabled = false;
            this.btnFindDuplicates.Location = new System.Drawing.Point(78, 201);
            this.btnFindDuplicates.Name = "btnFindDuplicates";
            this.btnFindDuplicates.Size = new System.Drawing.Size(157, 23);
            this.btnFindDuplicates.TabIndex = 14;
            this.btnFindDuplicates.Text = "Find duplicates";
            this.btnFindDuplicates.UseVisualStyleBackColor = true;
            this.btnFindDuplicates.Click += new System.EventHandler(this.btnFindDuplicates_Click);
            // 
            // btnPlayerRivals
            // 
            this.btnPlayerRivals.Enabled = false;
            this.btnPlayerRivals.Location = new System.Drawing.Point(78, 230);
            this.btnPlayerRivals.Name = "btnPlayerRivals";
            this.btnPlayerRivals.Size = new System.Drawing.Size(157, 23);
            this.btnPlayerRivals.TabIndex = 15;
            this.btnPlayerRivals.Text = "Find rivals by player";
            this.btnPlayerRivals.UseVisualStyleBackColor = true;
            this.btnPlayerRivals.Click += new System.EventHandler(this.btnPlayerRivals_Click);
            // 
            // lblOptional
            // 
            this.lblOptional.AutoSize = true;
            this.lblOptional.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOptional.Location = new System.Drawing.Point(15, 201);
            this.lblOptional.Name = "lblOptional";
            this.lblOptional.Size = new System.Drawing.Size(54, 13);
            this.lblOptional.TabIndex = 16;
            this.lblOptional.Text = "Optional";
            // 
            // btnShowNames
            // 
            this.btnShowNames.Enabled = false;
            this.btnShowNames.Location = new System.Drawing.Point(279, 15);
            this.btnShowNames.Name = "btnShowNames";
            this.btnShowNames.Size = new System.Drawing.Size(126, 23);
            this.btnShowNames.TabIndex = 17;
            this.btnShowNames.Text = "Show names";
            this.btnShowNames.UseVisualStyleBackColor = true;
            this.btnShowNames.Click += new System.EventHandler(this.btnShowNames_Click);
            // 
            // btnShowTeams
            // 
            this.btnShowTeams.Enabled = false;
            this.btnShowTeams.Location = new System.Drawing.Point(411, 15);
            this.btnShowTeams.Name = "btnShowTeams";
            this.btnShowTeams.Size = new System.Drawing.Size(126, 23);
            this.btnShowTeams.TabIndex = 18;
            this.btnShowTeams.Text = "Show Teams";
            this.btnShowTeams.UseVisualStyleBackColor = true;
            this.btnShowTeams.Click += new System.EventHandler(this.btnShowTeams_Click);
            // 
            // btnShowCountries
            // 
            this.btnShowCountries.Enabled = false;
            this.btnShowCountries.Location = new System.Drawing.Point(543, 15);
            this.btnShowCountries.Name = "btnShowCountries";
            this.btnShowCountries.Size = new System.Drawing.Size(126, 23);
            this.btnShowCountries.TabIndex = 19;
            this.btnShowCountries.Text = "Show countries";
            this.btnShowCountries.UseVisualStyleBackColor = true;
            this.btnShowCountries.Click += new System.EventHandler(this.btnShowCountries_Click);
            // 
            // btnShowAll
            // 
            this.btnShowAll.Enabled = false;
            this.btnShowAll.Location = new System.Drawing.Point(675, 15);
            this.btnShowAll.Name = "btnShowAll";
            this.btnShowAll.Size = new System.Drawing.Size(126, 23);
            this.btnShowAll.TabIndex = 20;
            this.btnShowAll.Text = "Show all";
            this.btnShowAll.UseVisualStyleBackColor = true;
            this.btnShowAll.Click += new System.EventHandler(this.btnShowAll_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(835, 435);
            this.Controls.Add(this.btnShowAll);
            this.Controls.Add(this.btnShowCountries);
            this.Controls.Add(this.btnShowTeams);
            this.Controls.Add(this.btnShowNames);
            this.Controls.Add(this.lblOptional);
            this.Controls.Add(this.btnPlayerRivals);
            this.Controls.Add(this.btnFindDuplicates);
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
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(851, 474);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MM Tournament Calculator";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownRounds)).EndInit();
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
        private System.Windows.Forms.Button btnFindDuplicates;
        private System.Windows.Forms.Button btnPlayerRivals;
        private System.Windows.Forms.Label lblOptional;
        private System.Windows.Forms.Button btnShowNames;
        private System.Windows.Forms.Button btnShowTeams;
        private System.Windows.Forms.Button btnShowCountries;
        private System.Windows.Forms.Button btnShowAll;
    }
}

