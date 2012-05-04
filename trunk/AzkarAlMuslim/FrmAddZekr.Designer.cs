namespace AzkarAlMuslim
{
    partial class FrmAddZekr
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAddZekr));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.cmbAzkarTypes = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddNewItem = new System.Windows.Forms.Button();
            this.txtZekrContent = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnClose);
            this.groupBox2.Controls.Add(this.cmbAzkarTypes);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btnAddNewItem);
            this.groupBox2.Controls.Add(this.txtZekrContent);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(447, 205);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "–ﬂ— ÃœÌœ";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(113, 169);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(101, 26);
            this.btnClose.TabIndex = 18;
            this.btnClose.Text = "≈€·«ﬁ";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmbAzkarTypes
            // 
            this.cmbAzkarTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAzkarTypes.FormattingEnabled = true;
            this.cmbAzkarTypes.Items.AddRange(new object[] {
            "–ﬂ—",
            "ÕœÌÀ",
            "√ÌÂ ﬁ—√‰ÌÂ",
            "·ÿ«∆›"});
            this.cmbAzkarTypes.Location = new System.Drawing.Point(152, 22);
            this.cmbAzkarTypes.Name = "cmbAzkarTypes";
            this.cmbAzkarTypes.Size = new System.Drawing.Size(200, 26);
            this.cmbAzkarTypes.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(372, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 18);
            this.label2.TabIndex = 16;
            this.label2.Text = "«·–ﬂ— ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(373, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 18);
            this.label1.TabIndex = 15;
            this.label1.Text = "«·›∆Â";
            // 
            // btnAddNewItem
            // 
            this.btnAddNewItem.Location = new System.Drawing.Point(220, 168);
            this.btnAddNewItem.Name = "btnAddNewItem";
            this.btnAddNewItem.Size = new System.Drawing.Size(113, 28);
            this.btnAddNewItem.TabIndex = 14;
            this.btnAddNewItem.Text = "√÷›";
            this.btnAddNewItem.UseVisualStyleBackColor = true;
            this.btnAddNewItem.Click += new System.EventHandler(this.btnAddNewItem_Click);
            // 
            // txtZekrContent
            // 
            this.txtZekrContent.Location = new System.Drawing.Point(6, 59);
            this.txtZekrContent.Multiline = true;
            this.txtZekrContent.Name = "txtZekrContent";
            this.txtZekrContent.Size = new System.Drawing.Size(354, 90);
            this.txtZekrContent.TabIndex = 7;
            // 
            // FrmAddZekr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 220);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmAddZekr";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "≈÷«›Â ⁄‰’— ÃœÌœ";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmAddZekr_FormClosed);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddNewItem;
        private System.Windows.Forms.TextBox txtZekrContent;
        private System.Windows.Forms.ComboBox cmbAzkarTypes;
        private System.Windows.Forms.Button btnClose;
    }
}