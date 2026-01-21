using System.Drawing;
using System.Windows.Forms;

namespace Vivy
{
    partial class FrmAddGame
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private Label lblName;
        private TextBox txtGameName;
        private Label lblRules;
        private RichTextBox rtbRules;
        private Button btnAdd;
        private Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblName = new Label();
            txtGameName = new TextBox();
            lblRules = new Label();
            rtbRules = new RichTextBox();
            btnAdd = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(0, 126, 249);
            lblTitle.Location = new Point(18, 15);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(306, 30);
            lblTitle.TabIndex = 6;
            lblTitle.Text = "Neues Brettspiel hinzufügen";
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 10F);
            lblName.ForeColor = Color.White;
            lblName.Location = new Point(18, 52);
            lblName.Name = "lblName";
            lblName.Size = new Size(74, 19);
            lblName.TabIndex = 5;
            lblName.Text = "Spielname:";
            // 
            // txtGameName
            // 
            txtGameName.Font = new Font("Segoe UI", 10F);
            txtGameName.Location = new Point(18, 71);
            txtGameName.Margin = new Padding(3, 2, 3, 2);
            txtGameName.Name = "txtGameName";
            txtGameName.Size = new Size(350, 25);
            txtGameName.TabIndex = 4;
            // 
            // lblRules
            // 
            lblRules.AutoSize = true;
            lblRules.Font = new Font("Segoe UI", 10F);
            lblRules.ForeColor = Color.White;
            lblRules.Location = new Point(18, 101);
            lblRules.Name = "lblRules";
            lblRules.Size = new Size(78, 19);
            lblRules.TabIndex = 3;
            lblRules.Text = "Spielregeln:";
            // 
            // rtbRules
            // 
            rtbRules.Font = new Font("Segoe UI", 10F);
            rtbRules.Location = new Point(18, 120);
            rtbRules.Margin = new Padding(3, 2, 3, 2);
            rtbRules.Name = "rtbRules";
            rtbRules.Size = new Size(350, 114);
            rtbRules.TabIndex = 2;
            rtbRules.Text = "";
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.FromArgb(0, 126, 249);
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(141, 244);
            btnAdd.Margin = new Padding(3, 2, 3, 2);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(105, 26);
            btnAdd.TabIndex = 1;
            btnAdd.Text = "Hinzufügen";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += BtnAdd_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.Gray;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 10F);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(263, 244);
            btnCancel.Margin = new Padding(3, 2, 3, 2);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(105, 26);
            btnCancel.TabIndex = 0;
            btnCancel.Text = "Abbrechen";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += BtnCancel_Click;
            // 
            // FrmAddGame
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(24, 30, 54);
            ClientSize = new Size(394, 300);
            Controls.Add(btnCancel);
            Controls.Add(btnAdd);
            Controls.Add(rtbRules);
            Controls.Add(lblRules);
            Controls.Add(txtGameName);
            Controls.Add(lblName);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            Name = "FrmAddGame";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Neues Spiel hinzufügen";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}