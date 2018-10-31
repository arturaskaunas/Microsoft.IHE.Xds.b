namespace Microsoft.XDS.Test
{
    partial class frmMetadataConversion
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtFileToTransform = new System.Windows.Forms.TextBox();
            this.rtfResult = new System.Windows.Forms.RichTextBox();
            this.btnTranform = new System.Windows.Forms.Button();
            this.btnFindFile = new System.Windows.Forms.Button();
            this.dlgFolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Folder:";
            // 
            // txtFileToTransform
            // 
            this.txtFileToTransform.Location = new System.Drawing.Point(58, 13);
            this.txtFileToTransform.Name = "txtFileToTransform";
            this.txtFileToTransform.Size = new System.Drawing.Size(368, 20);
            this.txtFileToTransform.TabIndex = 1;
            // 
            // rtfResult
            // 
            this.rtfResult.Location = new System.Drawing.Point(16, 40);
            this.rtfResult.Name = "rtfResult";
            this.rtfResult.Size = new System.Drawing.Size(445, 200);
            this.rtfResult.TabIndex = 2;
            this.rtfResult.Text = "";
            // 
            // btnTranform
            // 
            this.btnTranform.Location = new System.Drawing.Point(386, 247);
            this.btnTranform.Name = "btnTranform";
            this.btnTranform.Size = new System.Drawing.Size(75, 23);
            this.btnTranform.TabIndex = 3;
            this.btnTranform.Text = "Transform";
            this.btnTranform.UseVisualStyleBackColor = true;
            this.btnTranform.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnFindFile
            // 
            this.btnFindFile.Location = new System.Drawing.Point(432, 13);
            this.btnFindFile.Name = "btnFindFile";
            this.btnFindFile.Size = new System.Drawing.Size(29, 23);
            this.btnFindFile.TabIndex = 4;
            this.btnFindFile.Text = "...";
            this.btnFindFile.UseVisualStyleBackColor = true;
            this.btnFindFile.Click += new System.EventHandler(this.btnFindFile_Click);
            // 
            // frmMetadataConversion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 280);
            this.Controls.Add(this.btnFindFile);
            this.Controls.Add(this.btnTranform);
            this.Controls.Add(this.rtfResult);
            this.Controls.Add(this.txtFileToTransform);
            this.Controls.Add(this.label1);
            this.Name = "frmMetadataConversion";
            this.Text = "ebRIM Metadata Conversion";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFileToTransform;
        private System.Windows.Forms.RichTextBox rtfResult;
        private System.Windows.Forms.Button btnTranform;
        private System.Windows.Forms.Button btnFindFile;
        private System.Windows.Forms.FolderBrowserDialog dlgFolderBrowser;
    }
}

