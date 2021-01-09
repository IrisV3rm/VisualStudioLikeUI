using MenuStripStuff;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualStudioUI
{
    public partial class Main : Form
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public Main()
        {
            InitializeComponent();
            MainMenuStrip.Renderer = new BrowserMenuRenderer();
            OtherStrip.Renderer = new BrowserMenuRenderer();
        }

        private void TopBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }

        private void gitToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/IrisV3rm/VisualStudioLikeUI");
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            irisTabControl1.OpenFile();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            irisTabControl1.SavePointedTab();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            irisTabControl1.SavePointedTab();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            irisTabControl1.GetWorkingEditor().Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            irisTabControl1.GetWorkingEditor().Redo();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void viewToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory($"{AppDomain.CurrentDomain.BaseDirectory}\\Scripts");

            scriptsToolStripMenuItem.DropDownItems.Clear();

            foreach (string file in Directory.GetFiles($"{AppDomain.CurrentDomain.BaseDirectory}\\Scripts"))
            {
                ToolStripItem Item = scriptsToolStripMenuItem.DropDownItems.Add(file.Replace($"{AppDomain.CurrentDomain.BaseDirectory}\\Scripts\\", ""));
                Item.ForeColor = Color.White;
                Item.Click += (er, err) =>
                {
                    ExecuteScript(File.ReadAllText(file));
                };
            }
            
        }

        private void ExecuteScript(string Script)
        {
            Console.WriteLine(Script);
        }
        string ScriptData = @"
true false nil
while wait() do end
'yeet'
[[yeet]]
-- yeet
--[[yeet]]
123456
+-=
asdasd
__mod
";
        private void Main_Load(object sender, EventArgs e)
        {
            irisTabControl1.CreateTab("Script", ScriptData, Editor.SyntaxCopyTypes.Sentinel);
        }
    }
}
