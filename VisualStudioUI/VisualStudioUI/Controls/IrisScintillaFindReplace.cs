using ScintillaNET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualStudioUI.Controls
{
    public partial class IrisScintillaFindReplace : UserControl
    {
        public IrisTabControl TabC { get; set; }

        public IrisScintillaFindReplace()
        {
            InitializeComponent();
        }

        Scintilla TextEditor;

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            if (height == 79)
            {
                base.SetBoundsCore(x, y, 266, 79, specified);
            }
            else if (height == 55)
            {
                base.SetBoundsCore(x, y, 266, 55, specified);
            }
            else
            {
                base.SetBoundsCore(x, y, 266, 79, specified);
            }
        }

        private void ExpandShrink_Click(object sender, EventArgs e)
        {
            if (ExpandShrink.Text == "+")
            {
                Expand();
            }
            else if (ExpandShrink.Text == "-")
            {
                Shrink();
            }
        }

        private void Expand()
        {
            ReplaceAll.Visible = true;
            ReplaceBox.Visible = true;
            ReplaceButton.Visible = true;

            CountLabel.Location = new Point(34, 61);
            ExpandShrink.Location = new Point(3, 58);
            CloseFindReplace.Location = new Point(CloseFindReplace.Location.X, 58);

            ExpandShrink.Text = "-";

            Size = new Size(Width, 79);
        }

        private void Shrink()
        {
            ReplaceAll.Visible = false;
            ReplaceBox.Visible = false;
            ReplaceButton.Visible = false;

            CountLabel.Location = new Point(34, 34);
            ExpandShrink.Location = new Point(3, 31);
            CloseFindReplace.Location = new Point(CloseFindReplace.Location.X, 31);

            ExpandShrink.Text = "+";

            Size = new Size(Width, 55);

        }

        Dictionary<Keys, bool> VerifiedKey = new Dictionary<Keys, bool>
        {
            [Keys.M] = true,
            [Keys.C] = true,
            [Keys.V] = true,
            [Keys.X] = true,
            [Keys.T] = true,
            [Keys.I] = true,
            [Keys.Enter] = true,
            [Keys.Up] = true,
            [Keys.Down] = true,
            [Keys.Left] = true,
            [Keys.Right] = true,
            [Keys.L] = true,
            [Keys.A] = true,
            [Keys.Back] = true,
            [Keys.Z] = true,
        };

        Dictionary<Keys, bool> NewHandles = new Dictionary<Keys, bool>
        {
            [Keys.F] = true,
            [Keys.H] = true,
            [Keys.N] = true,
        };

        private void Handler(Keys Key)
        {
            switch (Key)
            {
                case Keys.F:
                    Shrink();
                    break;
                case Keys.H:
                    Expand();
                    break;
                case Keys.N:
                    (Parent.Parent.Parent.Parent.Parent as IrisTabControl).CreateTab();
                    (Parent.Parent.Parent.Parent.Parent.Parent as Main).ActiveControl = (Parent.Parent.Parent.Parent.Parent as IrisTabControl).GetWorkingEditor();
                    break;
            }
        }

        public void SetupTextBox(Scintilla box)
        {
            bool KeyFound = false;

            try
            {
                TextEditor = box;
                box.KeyDown += (object sender, KeyEventArgs e) =>
                {
                    if (e.KeyCode == Keys.Tab)
                    {
                        box.AddText("\t");
                    }
                    else if (!e.Control)
                        return;
                    else if (e.Control && VerifiedKey.TryGetValue(e.KeyCode, out KeyFound))
                        return;
                    else if (e.Control && NewHandles.TryGetValue(e.KeyCode, out KeyFound))
                    {
                        Visible = true;
                        Handler(e.KeyCode);
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    }
                    else
                        e.Handled = true; e.SuppressKeyPress = true;
                };
            }

            catch (NotSupportedException)
            {
                new NotSupportedException("Control does not have a valid text property.");
            }
        }

        private void FindBox_TextChanged(object sender, EventArgs e)
        {
            if (FindBox.Text == "")
            {
                CountLabel.Text = "0 Results Found";
                TextEditor.SetSel(0, 0);
            }

            if (System.Text.RegularExpressions.Regex.Split(TextEditor.Text, FindBox.Text).Count() > 1)
            {
                FindNextHandler(FindBox.Text, SearchFlags.None);
                CountLabel.Text = $"{System.Text.RegularExpressions.Regex.Split(TextEditor.Text, FindBox.Text).Count() - 1} Results Found";
            }
        }

        private void SelectLast_Click(object sender, EventArgs e)
        {
            FindPreviousHandler(FindBox.Text, SearchFlags.None);
            CountLabel.Text = $"{System.Text.RegularExpressions.Regex.Split(TextEditor.Text, FindBox.Text).Count() - 1} Results Found";
        }

        private void SelectRight_Click(object sender, EventArgs e)
        {
            CountLabel.Text = $"{System.Text.RegularExpressions.Regex.Split(TextEditor.Text, FindBox.Text).Count() - 1} Results Found";
            FindNextHandlerPlz(FindBox.Text, SearchFlags.None);
        }

        private void FindAll_Click(object sender, EventArgs e)
        {
            FindNextHandler(FindBox.Text, SearchFlags.None);
        }

        private void ReplaceButton_Click(object sender, EventArgs e)
        {
            ReplaceNextHandler(FindBox.Text, ReplaceBox.Text);
        }

        private void ReplaceAll_Click(object sender, EventArgs e)
        {
            ReplaceAllHandler(FindBox.Text, ReplaceBox.Text);
        }

        private void FindBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                FindNextHandler(FindBox.Text, SearchFlags.None);
            }
        }

        /* Credits to mroshaw for the handlers https://github.com/mroshaw/ScintillaFindReplace/*/
        public int FindNextHandler(string text, SearchFlags searchFlags, int StartingLoc = 0)
        {
            if (StartingLoc != 0)
            {
                StartingLoc = Math.Max(TextEditor.CurrentPosition, TextEditor.AnchorPosition);
            }
            TextEditor.SearchFlags = searchFlags;
            TextEditor.TargetStart = StartingLoc;
            TextEditor.TargetEnd = TextEditor.TextLength;

            var pos = TextEditor.SearchInTarget(text);
            if (pos >= 0)
                TextEditor.SetSel(TextEditor.TargetStart, TextEditor.TargetEnd);

            return pos;
        }

        public int FindNextHandlerPlz(string text, SearchFlags searchFlags)
        {
            TextEditor.SearchFlags = searchFlags;
            TextEditor.TargetStart = Math.Max(TextEditor.CurrentPosition, TextEditor.AnchorPosition);
            TextEditor.TargetEnd = TextEditor.TextLength;

            var pos = TextEditor.SearchInTarget(text);
            if (pos >= 0)
                TextEditor.SetSel(TextEditor.TargetStart, TextEditor.TargetEnd);

            return pos;
        }

        public int FindPreviousHandler(string text, SearchFlags searchFlags)
        {
            TextEditor.SearchFlags = searchFlags;
            TextEditor.TargetStart = Math.Min(TextEditor.CurrentPosition, TextEditor.AnchorPosition);
            TextEditor.TargetEnd = 0;

            var pos = TextEditor.SearchInTarget(text);
            if (pos >= 0)
                TextEditor.SetSel(TextEditor.TargetStart, TextEditor.TargetEnd);

            return pos;
        }

        private int ReplaceNextHandler(string findText, string replaceText)
        {
            var pos = FindNextHandler(findText, SearchFlags.None);
            if (pos >= 0)
                TextEditor.ReplaceSelection(replaceText);
            return pos;
        }

        private void ReplaceAllHandler(string findText, string replaceText)
        {
            var pos = 0;
            while (pos >= 0)
            {
                pos = ReplaceNextHandler(findText, replaceText);
            }
        }

        private void IrisScintillaFindReplace_Load(object sender, EventArgs e)
        {
            Shrink();
        }

        private void CloseFindReplace_Click(object sender, EventArgs e)
        {
            Visible = false;
        }
    }
}
