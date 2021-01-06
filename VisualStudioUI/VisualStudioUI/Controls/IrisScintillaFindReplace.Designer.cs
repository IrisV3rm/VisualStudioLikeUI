
namespace VisualStudioUI.Controls
{
    partial class IrisScintillaFindReplace
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.FindBox = new System.Windows.Forms.TextBox();
            this.ReplaceBox = new System.Windows.Forms.TextBox();
            this.CountLabel = new System.Windows.Forms.Label();
            this.SelectLast = new System.Windows.Forms.Button();
            this.SelectRight = new System.Windows.Forms.Button();
            this.ReplaceButton = new System.Windows.Forms.Button();
            this.FindAll = new System.Windows.Forms.Button();
            this.ReplaceAll = new System.Windows.Forms.Button();
            this.ExpandShrink = new System.Windows.Forms.Button();
            this.CloseFindReplace = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // FindBox
            // 
            this.FindBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.FindBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FindBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.FindBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FindBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(207)))));
            this.FindBox.Location = new System.Drawing.Point(3, 3);
            this.FindBox.Name = "FindBox";
            this.FindBox.Size = new System.Drawing.Size(170, 16);
            this.FindBox.TabIndex = 0;
            this.FindBox.TextChanged += new System.EventHandler(this.FindBox_TextChanged);
            this.FindBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FindBox_KeyDown);
            // 
            // ReplaceBox
            // 
            this.ReplaceBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.ReplaceBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ReplaceBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ReplaceBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ReplaceBox.ForeColor = System.Drawing.Color.White;
            this.ReplaceBox.Location = new System.Drawing.Point(3, 31);
            this.ReplaceBox.Name = "ReplaceBox";
            this.ReplaceBox.Size = new System.Drawing.Size(170, 16);
            this.ReplaceBox.TabIndex = 1;
            // 
            // CountLabel
            // 
            this.CountLabel.AutoSize = true;
            this.CountLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.CountLabel.Location = new System.Drawing.Point(34, 61);
            this.CountLabel.Name = "CountLabel";
            this.CountLabel.Size = new System.Drawing.Size(84, 13);
            this.CountLabel.TabIndex = 2;
            this.CountLabel.Text = "0 Results Found";
            // 
            // SelectLast
            // 
            this.SelectLast.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.SelectLast.FlatAppearance.BorderSize = 0;
            this.SelectLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SelectLast.ForeColor = System.Drawing.Color.White;
            this.SelectLast.Location = new System.Drawing.Point(175, 3);
            this.SelectLast.Name = "SelectLast";
            this.SelectLast.Size = new System.Drawing.Size(26, 22);
            this.SelectLast.TabIndex = 3;
            this.SelectLast.Text = "<";
            this.SelectLast.UseVisualStyleBackColor = false;
            this.SelectLast.Click += new System.EventHandler(this.SelectLast_Click);
            // 
            // SelectRight
            // 
            this.SelectRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.SelectRight.FlatAppearance.BorderSize = 0;
            this.SelectRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SelectRight.ForeColor = System.Drawing.Color.White;
            this.SelectRight.Location = new System.Drawing.Point(203, 3);
            this.SelectRight.Name = "SelectRight";
            this.SelectRight.Size = new System.Drawing.Size(26, 22);
            this.SelectRight.TabIndex = 4;
            this.SelectRight.Text = ">";
            this.SelectRight.UseVisualStyleBackColor = false;
            this.SelectRight.Click += new System.EventHandler(this.SelectRight_Click);
            // 
            // ReplaceButton
            // 
            this.ReplaceButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.ReplaceButton.FlatAppearance.BorderSize = 0;
            this.ReplaceButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ReplaceButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F);
            this.ReplaceButton.ForeColor = System.Drawing.Color.White;
            this.ReplaceButton.Location = new System.Drawing.Point(176, 31);
            this.ReplaceButton.Name = "ReplaceButton";
            this.ReplaceButton.Size = new System.Drawing.Size(54, 22);
            this.ReplaceButton.TabIndex = 5;
            this.ReplaceButton.Text = "Replace";
            this.ReplaceButton.UseVisualStyleBackColor = false;
            this.ReplaceButton.Click += new System.EventHandler(this.ReplaceButton_Click);
            // 
            // FindAll
            // 
            this.FindAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.FindAll.FlatAppearance.BorderSize = 0;
            this.FindAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FindAll.ForeColor = System.Drawing.Color.White;
            this.FindAll.Location = new System.Drawing.Point(231, 3);
            this.FindAll.Name = "FindAll";
            this.FindAll.Size = new System.Drawing.Size(30, 22);
            this.FindAll.TabIndex = 6;
            this.FindAll.Text = "All";
            this.FindAll.UseVisualStyleBackColor = false;
            this.FindAll.Click += new System.EventHandler(this.FindAll_Click);
            // 
            // ReplaceAll
            // 
            this.ReplaceAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.ReplaceAll.FlatAppearance.BorderSize = 0;
            this.ReplaceAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ReplaceAll.ForeColor = System.Drawing.Color.White;
            this.ReplaceAll.Location = new System.Drawing.Point(231, 31);
            this.ReplaceAll.Name = "ReplaceAll";
            this.ReplaceAll.Size = new System.Drawing.Size(30, 22);
            this.ReplaceAll.TabIndex = 7;
            this.ReplaceAll.Text = "All";
            this.ReplaceAll.UseVisualStyleBackColor = false;
            this.ReplaceAll.Click += new System.EventHandler(this.ReplaceAll_Click);
            // 
            // ExpandShrink
            // 
            this.ExpandShrink.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.ExpandShrink.FlatAppearance.BorderSize = 0;
            this.ExpandShrink.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExpandShrink.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ExpandShrink.ForeColor = System.Drawing.Color.White;
            this.ExpandShrink.Location = new System.Drawing.Point(3, 58);
            this.ExpandShrink.Name = "ExpandShrink";
            this.ExpandShrink.Size = new System.Drawing.Size(19, 17);
            this.ExpandShrink.TabIndex = 8;
            this.ExpandShrink.Text = "+";
            this.ExpandShrink.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ExpandShrink.UseVisualStyleBackColor = false;
            this.ExpandShrink.Click += new System.EventHandler(this.ExpandShrink_Click);
            // 
            // CloseFindReplace
            // 
            this.CloseFindReplace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.CloseFindReplace.FlatAppearance.BorderSize = 0;
            this.CloseFindReplace.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseFindReplace.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CloseFindReplace.ForeColor = System.Drawing.Color.White;
            this.CloseFindReplace.Location = new System.Drawing.Point(242, 58);
            this.CloseFindReplace.Name = "CloseFindReplace";
            this.CloseFindReplace.Size = new System.Drawing.Size(19, 17);
            this.CloseFindReplace.TabIndex = 9;
            this.CloseFindReplace.Text = "x";
            this.CloseFindReplace.UseVisualStyleBackColor = false;
            this.CloseFindReplace.Click += new System.EventHandler(this.CloseFindReplace_Click);
            // 
            // IrisScintillaFindReplace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.Controls.Add(this.CloseFindReplace);
            this.Controls.Add(this.ExpandShrink);
            this.Controls.Add(this.ReplaceAll);
            this.Controls.Add(this.FindAll);
            this.Controls.Add(this.ReplaceButton);
            this.Controls.Add(this.SelectRight);
            this.Controls.Add(this.SelectLast);
            this.Controls.Add(this.CountLabel);
            this.Controls.Add(this.ReplaceBox);
            this.Controls.Add(this.FindBox);
            this.Name = "IrisScintillaFindReplace";
            this.Size = new System.Drawing.Size(266, 78);
            this.Load += new System.EventHandler(this.IrisScintillaFindReplace_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox FindBox;
        private System.Windows.Forms.TextBox ReplaceBox;
        private System.Windows.Forms.Label CountLabel;
        private System.Windows.Forms.Button SelectLast;
        private System.Windows.Forms.Button SelectRight;
        private System.Windows.Forms.Button ReplaceButton;
        private System.Windows.Forms.Button FindAll;
        private System.Windows.Forms.Button ReplaceAll;
        private System.Windows.Forms.Button ExpandShrink;
        private System.Windows.Forms.Button CloseFindReplace;
    }
}
