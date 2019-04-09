namespace BellTest
{
    partial class Instrument
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
            this.lblInstruction = new System.Windows.Forms.Label();
            this.lblResult = new System.Windows.Forms.Label();
            this.lblScore = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboModeSelect = new System.Windows.Forms.ComboBox();
            this.comboCodeList = new System.Windows.Forms.ComboBox();
            this.btnCodes = new System.Windows.Forms.Button();
            this.comboListSelect = new System.Windows.Forms.ComboBox();
            this.plungerButton1 = new BellTest.PlungerButton();
            this.needle = new BellTest.Needle();
            this.SuspendLayout();
            // 
            // lblInstruction
            // 
            this.lblInstruction.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInstruction.ForeColor = System.Drawing.Color.White;
            this.lblInstruction.Location = new System.Drawing.Point(12, 242);
            this.lblInstruction.Name = "lblInstruction";
            this.lblInstruction.Size = new System.Drawing.Size(593, 134);
            this.lblInstruction.TabIndex = 3;
            this.lblInstruction.Text = "Please send:";
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResult.ForeColor = System.Drawing.Color.White;
            this.lblResult.Location = new System.Drawing.Point(12, 419);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(73, 20);
            this.lblResult.TabIndex = 4;
            this.lblResult.Text = "Correct!";
            this.lblResult.Visible = false;
            // 
            // lblScore
            // 
            this.lblScore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblScore.AutoSize = true;
            this.lblScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScore.ForeColor = System.Drawing.Color.White;
            this.lblScore.Location = new System.Drawing.Point(501, 419);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(91, 20);
            this.lblScore.TabIndex = 5;
            this.lblScore.Text = "Score: 0/0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(13, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Mode:";
            // 
            // comboModeSelect
            // 
            this.comboModeSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboModeSelect.FormattingEnabled = true;
            this.comboModeSelect.Location = new System.Drawing.Point(65, 16);
            this.comboModeSelect.Name = "comboModeSelect";
            this.comboModeSelect.Size = new System.Drawing.Size(121, 21);
            this.comboModeSelect.TabIndex = 7;
            this.comboModeSelect.SelectedIndexChanged += new System.EventHandler(this.comboModeSelect_SelectedIndexChanged);
            // 
            // comboCodeList
            // 
            this.comboCodeList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboCodeList.FormattingEnabled = true;
            this.comboCodeList.Location = new System.Drawing.Point(12, 242);
            this.comboCodeList.Name = "comboCodeList";
            this.comboCodeList.Size = new System.Drawing.Size(593, 21);
            this.comboCodeList.TabIndex = 8;
            this.comboCodeList.Visible = false;
            this.comboCodeList.SelectedValueChanged += new System.EventHandler(this.comboCodeList_SelectedValueChanged);
            // 
            // btnCodes
            // 
            this.btnCodes.Location = new System.Drawing.Point(530, 14);
            this.btnCodes.Name = "btnCodes";
            this.btnCodes.Size = new System.Drawing.Size(75, 23);
            this.btnCodes.TabIndex = 9;
            this.btnCodes.Text = "View Codes";
            this.btnCodes.UseVisualStyleBackColor = true;
            this.btnCodes.Click += new System.EventHandler(this.btnCodes_Click);
            // 
            // comboListSelect
            // 
            this.comboListSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboListSelect.DropDownWidth = 400;
            this.comboListSelect.FormattingEnabled = true;
            this.comboListSelect.Location = new System.Drawing.Point(318, 15);
            this.comboListSelect.Name = "comboListSelect";
            this.comboListSelect.Size = new System.Drawing.Size(206, 21);
            this.comboListSelect.TabIndex = 10;
            this.comboListSelect.SelectedIndexChanged += new System.EventHandler(this.comboListSelect_SelectedIndexChanged);
            // 
            // plungerButton1
            // 
            this.plungerButton1.BackColor = System.Drawing.Color.Goldenrod;
            this.plungerButton1.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.plungerButton1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.PaleGoldenrod;
            this.plungerButton1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gold;
            this.plungerButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plungerButton1.Location = new System.Drawing.Point(382, 135);
            this.plungerButton1.Name = "plungerButton1";
            this.plungerButton1.Size = new System.Drawing.Size(32, 32);
            this.plungerButton1.TabIndex = 2;
            this.plungerButton1.Text = "plungerButton1";
            this.plungerButton1.UseVisualStyleBackColor = false;
            this.plungerButton1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button1_MouseDown);
            this.plungerButton1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button1_MouseUp);
            // 
            // needle
            // 
            this.needle.Location = new System.Drawing.Point(203, 76);
            this.needle.Name = "needle";
            this.needle.NeedleRotation = 0.5F;
            this.needle.Size = new System.Drawing.Size(150, 150);
            this.needle.TabIndex = 0;
            // 
            // Instrument
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Red;
            this.ClientSize = new System.Drawing.Size(617, 451);
            this.Controls.Add(this.comboListSelect);
            this.Controls.Add(this.btnCodes);
            this.Controls.Add(this.comboCodeList);
            this.Controls.Add(this.comboModeSelect);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.lblInstruction);
            this.Controls.Add(this.plungerButton1);
            this.Controls.Add(this.needle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Instrument";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Bell Code Tester";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Needle needle;
        private PlungerButton plungerButton1;
        private System.Windows.Forms.Label lblInstruction;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboModeSelect;
        private System.Windows.Forms.ComboBox comboCodeList;
        private System.Windows.Forms.Button btnCodes;
        private System.Windows.Forms.ComboBox comboListSelect;
    }
}

