namespace NagelCup
{
    partial class MainWindow
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
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.txtChunks = new System.Windows.Forms.TextBox();
            this.lblChunks = new System.Windows.Forms.Label();
            this.lblPersonsPerChunk = new System.Windows.Forms.Label();
            this.txtPersonsPerChunk = new System.Windows.Forms.TextBox();
            this.btnNewRound = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtFilePath
            // 
            this.txtFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilePath.Enabled = false;
            this.txtFilePath.Location = new System.Drawing.Point(13, 13);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(699, 20);
            this.txtFilePath.TabIndex = 0;
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoad.Location = new System.Drawing.Point(718, 11);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(35, 23);
            this.btnLoad.TabIndex = 1;
            this.btnLoad.Text = "...";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Location = new System.Drawing.Point(13, 39);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(740, 348);
            this.tabControl.TabIndex = 2;
            // 
            // openFileDialog
            // 
            this.openFileDialog.CheckFileExists = false;
            this.openFileDialog.DefaultExt = "xml";
            this.openFileDialog.FileName = "NagelCup.xml";
            this.openFileDialog.Filter = "xml|*.xml";
            // 
            // txtChunks
            // 
            this.txtChunks.Location = new System.Drawing.Point(117, 393);
            this.txtChunks.Name = "txtChunks";
            this.txtChunks.Size = new System.Drawing.Size(85, 20);
            this.txtChunks.TabIndex = 3;
            this.txtChunks.Leave += new System.EventHandler(this.txtChunks_Leave);
            // 
            // lblChunks
            // 
            this.lblChunks.AutoSize = true;
            this.lblChunks.Location = new System.Drawing.Point(12, 396);
            this.lblChunks.Name = "lblChunks";
            this.lblChunks.Size = new System.Drawing.Size(39, 13);
            this.lblChunks.TabIndex = 4;
            this.lblChunks.Text = "Klötze:";
            // 
            // lblPersonsPerChunk
            // 
            this.lblPersonsPerChunk.AutoSize = true;
            this.lblPersonsPerChunk.Location = new System.Drawing.Point(12, 422);
            this.lblPersonsPerChunk.Name = "lblPersonsPerChunk";
            this.lblPersonsPerChunk.Size = new System.Drawing.Size(99, 13);
            this.lblPersonsPerChunk.TabIndex = 5;
            this.lblPersonsPerChunk.Text = "Personen pro Klotz:";
            // 
            // txtPersonsPerChunk
            // 
            this.txtPersonsPerChunk.Location = new System.Drawing.Point(117, 419);
            this.txtPersonsPerChunk.Name = "txtPersonsPerChunk";
            this.txtPersonsPerChunk.Size = new System.Drawing.Size(85, 20);
            this.txtPersonsPerChunk.TabIndex = 6;
            this.txtPersonsPerChunk.Leave += new System.EventHandler(this.txtPersonsPerChunk_Leave);
            // 
            // btnNewRound
            // 
            this.btnNewRound.Location = new System.Drawing.Point(117, 445);
            this.btnNewRound.Name = "btnNewRound";
            this.btnNewRound.Size = new System.Drawing.Size(85, 23);
            this.btnNewRound.TabIndex = 7;
            this.btnNewRound.Text = "Neue Runde!";
            this.btnNewRound.UseVisualStyleBackColor = true;
            this.btnNewRound.Click += new System.EventHandler(this.btnNewRound_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(765, 475);
            this.Controls.Add(this.btnNewRound);
            this.Controls.Add(this.txtPersonsPerChunk);
            this.Controls.Add(this.lblPersonsPerChunk);
            this.Controls.Add(this.lblChunks);
            this.Controls.Add(this.txtChunks);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.txtFilePath);
            this.Name = "MainWindow";
            this.Text = "NagelCup";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TextBox txtChunks;
        private System.Windows.Forms.Label lblChunks;
        private System.Windows.Forms.Label lblPersonsPerChunk;
        private System.Windows.Forms.TextBox txtPersonsPerChunk;
        private System.Windows.Forms.Button btnNewRound;
    }
}

