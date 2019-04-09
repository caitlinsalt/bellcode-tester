namespace BellTest
{
    partial class CodeHelplistForm
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
            this.btnOk = new System.Windows.Forms.Button();
            this.lblCodeListTitle = new System.Windows.Forms.Label();
            this.dgvCodeList = new System.Windows.Forms.DataGridView();
            this.colCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCodeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblDescription = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCodeList)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(309, 502);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lblCodeListTitle
            // 
            this.lblCodeListTitle.AutoSize = true;
            this.lblCodeListTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodeListTitle.Location = new System.Drawing.Point(12, 9);
            this.lblCodeListTitle.Name = "lblCodeListTitle";
            this.lblCodeListTitle.Size = new System.Drawing.Size(127, 20);
            this.lblCodeListTitle.TabIndex = 1;
            this.lblCodeListTitle.Text = "Title of Code List";
            // 
            // dgvCodeList
            // 
            this.dgvCodeList.AllowUserToAddRows = false;
            this.dgvCodeList.AllowUserToDeleteRows = false;
            this.dgvCodeList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCodeList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;
            this.dgvCodeList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dgvCodeList.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dgvCodeList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCodeList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCode,
            this.colCodeName});
            this.dgvCodeList.Location = new System.Drawing.Point(16, 77);
            this.dgvCodeList.Name = "dgvCodeList";
            this.dgvCodeList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvCodeList.RowHeadersVisible = false;
            this.dgvCodeList.Size = new System.Drawing.Size(665, 409);
            this.dgvCodeList.TabIndex = 2;
            // 
            // colCode
            // 
            this.colCode.HeaderText = "Bell Signal";
            this.colCode.Name = "colCode";
            this.colCode.ReadOnly = true;
            this.colCode.Width = 5;
            // 
            // colCodeName
            // 
            this.colCodeName.HeaderText = "Description";
            this.colCodeName.Name = "colCodeName";
            this.colCodeName.ReadOnly = true;
            this.colCodeName.Width = 5;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(13, 32);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(35, 13);
            this.lblDescription.TabIndex = 3;
            this.lblDescription.Text = "label1";
            // 
            // CodeHelplistForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 537);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.dgvCodeList);
            this.Controls.Add(this.lblCodeListTitle);
            this.Controls.Add(this.btnOk);
            this.Name = "CodeHelplistForm";
            this.Text = "Bell Codes";
            ((System.ComponentModel.ISupportInitialize)(this.dgvCodeList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblCodeListTitle;
        private System.Windows.Forms.DataGridView dgvCodeList;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCodeName;
        private System.Windows.Forms.Label lblDescription;
    }
}