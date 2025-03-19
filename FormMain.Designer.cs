namespace FileCompress
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tbFolder = new System.Windows.Forms.TextBox();
            btnSelectFolder = new System.Windows.Forms.Button();
            btnStartJob = new System.Windows.Forms.Button();
            pbFiles = new System.Windows.Forms.ProgressBar();
            tbExtensions = new System.Windows.Forms.TextBox();
            cbInOldFolder = new System.Windows.Forms.CheckBox();
            nudQuality = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)nudQuality).BeginInit();
            SuspendLayout();
            // 
            // tbFolder
            // 
            tbFolder.Location = new System.Drawing.Point(12, 12);
            tbFolder.Name = "tbFolder";
            tbFolder.Size = new System.Drawing.Size(387, 27);
            tbFolder.TabIndex = 0;
            // 
            // btnSelectFolder
            // 
            btnSelectFolder.Location = new System.Drawing.Point(405, 12);
            btnSelectFolder.Name = "btnSelectFolder";
            btnSelectFolder.Size = new System.Drawing.Size(124, 27);
            btnSelectFolder.TabIndex = 1;
            btnSelectFolder.Text = "Select folder";
            btnSelectFolder.UseVisualStyleBackColor = true;
            btnSelectFolder.Click += btnSelectFolder_Click;
            // 
            // btnStartJob
            // 
            btnStartJob.Location = new System.Drawing.Point(405, 55);
            btnStartJob.Name = "btnStartJob";
            btnStartJob.Size = new System.Drawing.Size(124, 50);
            btnStartJob.TabIndex = 2;
            btnStartJob.Text = "Start Job";
            btnStartJob.UseVisualStyleBackColor = true;
            btnStartJob.Click += btnStartJob_Click;
            // 
            // pbFiles
            // 
            pbFiles.Dock = System.Windows.Forms.DockStyle.Bottom;
            pbFiles.Location = new System.Drawing.Point(0, 116);
            pbFiles.Name = "pbFiles";
            pbFiles.Size = new System.Drawing.Size(543, 13);
            pbFiles.TabIndex = 7;
            // 
            // tbExtensions
            // 
            tbExtensions.Location = new System.Drawing.Point(12, 45);
            tbExtensions.Name = "tbExtensions";
            tbExtensions.Size = new System.Drawing.Size(387, 27);
            tbExtensions.TabIndex = 8;
            tbExtensions.Text = "*.pdf";
            // 
            // cbInOldFolder
            // 
            cbInOldFolder.AutoSize = true;
            cbInOldFolder.Location = new System.Drawing.Point(12, 78);
            cbInOldFolder.Name = "cbInOldFolder";
            cbInOldFolder.Size = new System.Drawing.Size(113, 24);
            cbInOldFolder.TabIndex = 9;
            cbInOldFolder.Text = "In old folder";
            cbInOldFolder.UseVisualStyleBackColor = true;
            // 
            // nudQuality
            // 
            nudQuality.Increment = new decimal(new int[] { 5, 0, 0, 0 });
            nudQuality.Location = new System.Drawing.Point(335, 75);
            nudQuality.Minimum = new decimal(new int[] { 50, 0, 0, 0 });
            nudQuality.Name = "nudQuality";
            nudQuality.Size = new System.Drawing.Size(64, 27);
            nudQuality.TabIndex = 10;
            nudQuality.Value = new decimal(new int[] { 90, 0, 0, 0 });
            // 
            // FormMain
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(543, 129);
            Controls.Add(nudQuality);
            Controls.Add(cbInOldFolder);
            Controls.Add(tbExtensions);
            Controls.Add(pbFiles);
            Controls.Add(btnStartJob);
            Controls.Add(btnSelectFolder);
            Controls.Add(tbFolder);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Name = "FormMain";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "FormMain";
            ((System.ComponentModel.ISupportInitialize)nudQuality).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox tbFolder;
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.Button btnStartJob;
        private System.Windows.Forms.ProgressBar pbFiles;
        private System.Windows.Forms.TextBox tbExtensions;
        private System.Windows.Forms.CheckBox cbInOldFolder;
        private System.Windows.Forms.NumericUpDown nudQuality;
    }
}
