
namespace VisualStudioUI.Controls
{
    partial class IrisTabControl
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
            this.components = new System.ComponentModel.Container();
            this.BackDrop = new System.Windows.Forms.Panel();
            this.Container = new System.Windows.Forms.Panel();
            this.ButtonHolder = new System.Windows.Forms.Panel();
            this.AddTab = new System.Windows.Forms.Button();
            this.ScrollBar = new System.Windows.Forms.Panel();
            this.MainStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RenameBox = new System.Windows.Forms.ToolStripTextBox();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BigRibbon = new System.Windows.Forms.Panel();
            this.BackDrop.SuspendLayout();
            this.ButtonHolder.SuspendLayout();
            this.MainStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // BackDrop
            // 
            this.BackDrop.Controls.Add(this.Container);
            this.BackDrop.Controls.Add(this.BigRibbon);
            this.BackDrop.Controls.Add(this.ButtonHolder);
            this.BackDrop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BackDrop.Location = new System.Drawing.Point(0, 0);
            this.BackDrop.Name = "BackDrop";
            this.BackDrop.Size = new System.Drawing.Size(368, 290);
            this.BackDrop.TabIndex = 0;
            // 
            // Container
            // 
            this.Container.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Container.Location = new System.Drawing.Point(0, 32);
            this.Container.Name = "Container";
            this.Container.Size = new System.Drawing.Size(368, 258);
            this.Container.TabIndex = 0;
            // 
            // ButtonHolder
            // 
            this.ButtonHolder.AllowDrop = true;
            this.ButtonHolder.AutoScroll = true;
            this.ButtonHolder.Controls.Add(this.AddTab);
            this.ButtonHolder.Dock = System.Windows.Forms.DockStyle.Top;
            this.ButtonHolder.Location = new System.Drawing.Point(0, 0);
            this.ButtonHolder.Name = "ButtonHolder";
            this.ButtonHolder.Size = new System.Drawing.Size(368, 30);
            this.ButtonHolder.TabIndex = 1;
            this.ButtonHolder.DragOver += new System.Windows.Forms.DragEventHandler(this.ButtonHolder_DragOver);
            // 
            // AddTab
            // 
            this.AddTab.Dock = System.Windows.Forms.DockStyle.Left;
            this.AddTab.FlatAppearance.BorderSize = 0;
            this.AddTab.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddTab.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddTab.ForeColor = System.Drawing.Color.Coral;
            this.AddTab.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.AddTab.Location = new System.Drawing.Point(0, 0);
            this.AddTab.MaximumSize = new System.Drawing.Size(35, 35);
            this.AddTab.Name = "AddTab";
            this.AddTab.Size = new System.Drawing.Size(35, 30);
            this.AddTab.TabIndex = 99;
            this.AddTab.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.AddTab.UseVisualStyleBackColor = true;
            this.AddTab.Click += new System.EventHandler(this.button1_Click);
            this.AddTab.Paint += new System.Windows.Forms.PaintEventHandler(this.AddTab_Paint);
            // 
            // ScrollBar
            // 
            this.ScrollBar.Location = new System.Drawing.Point(0, 0);
            this.ScrollBar.Name = "ScrollBar";
            this.ScrollBar.Size = new System.Drawing.Size(200, 100);
            this.ScrollBar.TabIndex = 0;
            // 
            // MainStrip
            // 
            this.MainStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.MainStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.renameToolStripMenuItem,
            this.RenameBox,
            this.closeToolStripMenuItem});
            this.MainStrip.Name = "MainStrip";
            this.MainStrip.ShowImageMargin = false;
            this.MainStrip.Size = new System.Drawing.Size(136, 73);
            this.MainStrip.Closing += new System.Windows.Forms.ToolStripDropDownClosingEventHandler(this.MainStrip_Closing);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.renameToolStripMenuItem.Text = "Rename";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
            // 
            // RenameBox
            // 
            this.RenameBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.RenameBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.RenameBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RenameBox.ForeColor = System.Drawing.Color.White;
            this.RenameBox.Name = "RenameBox";
            this.RenameBox.Size = new System.Drawing.Size(100, 23);
            this.RenameBox.Visible = false;
            this.RenameBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RenameBox_KeyDown);
            this.RenameBox.TextChanged += new System.EventHandler(this.RenameBox_TextChanged);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // BigRibbon
            // 
            this.BigRibbon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.BigRibbon.Dock = System.Windows.Forms.DockStyle.Top;
            this.BigRibbon.Location = new System.Drawing.Point(0, 30);
            this.BigRibbon.Name = "BigRibbon";
            this.BigRibbon.Size = new System.Drawing.Size(368, 2);
            this.BigRibbon.TabIndex = 0;
            // 
            // IrisTabControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.BackDrop);
            this.Name = "IrisTabControl";
            this.Size = new System.Drawing.Size(368, 290);
            this.BackDrop.ResumeLayout(false);
            this.ButtonHolder.ResumeLayout(false);
            this.MainStrip.ResumeLayout(false);
            this.MainStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel BackDrop;
        private new System.Windows.Forms.Panel Container;
        private System.Windows.Forms.Panel ScrollBar;
        public System.Windows.Forms.Panel ButtonHolder;
        public System.Windows.Forms.Button AddTab;
        private System.Windows.Forms.ContextMenuStrip MainStrip;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox RenameBox;
        private System.Windows.Forms.Panel BigRibbon;
    }
}
