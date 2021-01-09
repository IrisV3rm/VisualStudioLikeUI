using ScintillaNET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualStudioUI.Controls;

namespace VisualStudioUI.Controls
{
    public partial class IrisTabControl : UserControl
    {
        public class NoFocusCueButton : Button
        {
            protected override bool ShowFocusCues
            {
                get
                {
                    return false;
                }
            }

            public override void NotifyDefault(bool value)
            {
                base.NotifyDefault(false);
            }
        }

        private int TabIndexImpl = 0;
        private int ribbonheight = 3;
        private DockStyle dockmethod = DockStyle.Bottom;
        private Point contextpoint;
        public int RibbonHeight { get { return ribbonheight; } set { ribbonheight = value; Invalidate(); } }
        public DockStyle RibbonDock { get { return dockmethod; } set { dockmethod = value; Invalidate(); } }

        private NoFocusCueButton PreDraggedTab;
        public static NoFocusCueButton TBBb = new NoFocusCueButton();
        public static NoFocusCueButton SelectedTab { get { return TBBb; } set { TBBb = value; } }
        public static NoFocusCueButton TBBbd = new NoFocusCueButton();
        public static NoFocusCueButton BackupSelectedTab { get { return TBBbd; } set { TBBbd = value; } }

        public static List<NoFocusCueButton> closeButtons = new List<NoFocusCueButton> { };

        public static Dictionary<NoFocusCueButton, Panel> Tabs = new Dictionary<NoFocusCueButton, Panel>();

        private Editor editor = new Editor();

        public IrisTabControl()
        {
            InitializeComponent();
            ButtonHolder.HorizontalScroll.Maximum = 500;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
            DoubleBuffered = true;
        }

        public NoFocusCueButton CreateTab(string Name = "Script", string Script = "")
        {
            IrisScintillaFindReplace FindReplace = new IrisScintillaFindReplace();
            if (Name == "Script")
            {
                Name = $"{Name} {TabIndexImpl}.lua";
            }
            else if (!Name.Contains(".lua"))
            {
                Name = $"{Name}.lua";
            }
            else
            {
                Name = $"{Name}";
            }

            NoFocusCueButton NewButton = new NoFocusCueButton();
            NewButton.BackColor = Color.FromArgb(0, 122, 204);
            NewButton.FlatAppearance.BorderSize = 0;
            NewButton.FlatStyle = FlatStyle.Flat;
            NewButton.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            NewButton.ForeColor = Color.White;
            NewButton.TextAlign = ContentAlignment.MiddleLeft;
            NewButton.Size = new Size(91, 21);
            NewButton.Dock = DockStyle.Left;
            NewButton.AutoSize = true;
            NewButton.Padding = new Padding(0, 0, 4, 0);
            NewButton.Text = Name + "   ";
            NewButton.TabIndex = TabIndexImpl++;
            NewButton.ContextMenuStrip = MainStrip;
            NewButton.SizeChanged += NewButton_SizeChanged;
            NewButton.MouseUp += NewButton_MouseUp;
            NewButton.Click += NewButton_Click;
            NewButton.MouseDown += NewButton_MouseDown;
            NewButton.MouseClick += NewButton_MouseClick;
            NewButton.KeyDown += NewButton_KeyDown;
            NewButton.MouseMove += NewButton_MouseMove;

            NoFocusCueButton CloseButton = new NoFocusCueButton();
            CloseButton.Size = new Size(15, 15);
            CloseButton.Dock = DockStyle.Right;
            CloseButton.FlatStyle = FlatStyle.Flat;
            CloseButton.FlatAppearance.BorderSize = 0;
            CloseButton.Paint += NewButton_Paint;
            CloseButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 122, 204);
            CloseButton.MouseClick += CloseButton_Click;
            NewButton.Controls.Add(CloseButton);

            Panel Ribbon = new Panel();
            NewButton.Controls.Add(Ribbon);
            Ribbon.Location = new Point(635, 365);
            Ribbon.Name = "Ribbon";
            Ribbon.Size = new Size(NewButton.Width, ribbonheight);
            Ribbon.Dock = dockmethod;

            Panel Tab = new Panel();
            Container.Controls.Add(Tab);
            Tab.Dock = DockStyle.Fill;

            Tab.Controls.Add(editor.NewScintilla(Script));

            ButtonHolder.Controls.Add(NewButton);
            ButtonHolder.Controls.SetChildIndex(NewButton, 0);
            ButtonHolder.Controls.SetChildIndex(AddTab, 0);

            Tabs.Add(NewButton, Tab);

            closeButtons.Add(CloseButton);

            NewButton.PerformClick();
            Tab.Visible = true;

            var scintilla = GetWorkingEditor();
            var lineCount = GetWorkingEditor().Lines.Count.ToString().Length;
            scintilla.Margins[0].Width = scintilla.TextWidth(10, new string('9', lineCount + 1)) + 1;

            FindReplace.SetupTextBox(scintilla);
            scintilla.Controls.Add(FindReplace);
            FindReplace.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            FindReplace.Location = new Point(GetWorkingEditor().Right - FindReplace.Size.Width, 0);
            FindReplace.Visible = false;
            FindReplace.Cursor = Cursors.Default;
            FindReplace.TabC = this;

            scintilla.SizeChanged += (e, r) =>
            {
                if (scintilla.Width > scintilla.ClientRectangle.Width)
                {
                    FindReplace.Location = new Point(GetWorkingEditor().Right - FindReplace.Size.Width - 20, 0);
                }
                else
                {
                    FindReplace.Location = new Point(GetWorkingEditor().Right - FindReplace.Size.Width, 0);
                }
            };

            scintilla.TextChanged += (e, r) =>
            {
                var Scint = GetWorkingEditor();
                lineCount = Scint.Lines.Count.ToString().Length;
                Scint.Margins[0].Width = Scint.TextWidth(10, new string('9', lineCount + 1)) + 1;
            };



            return NewButton;
        }

        private void NewButton_Paint(object sender, PaintEventArgs e)
        {
            SolidBrush drawBrush = new SolidBrush(Color.White);

            FontFamily fontFamily = new FontFamily("Arial");
            Font font = new Font(
               fontFamily,
               20,
               FontStyle.Regular,
               GraphicsUnit.Pixel);

            StringFormat drawformat = new StringFormat();
            drawformat.Alignment = StringAlignment.Center;
            drawformat.LineAlignment = StringAlignment.Center;

            e.Graphics.DrawString("×", font, drawBrush, -1.25F, -1F);
        }

        private void NewButton_MouseMove(object sender, MouseEventArgs e)
        {
            if (PreDraggedTab == null || e.Button != MouseButtons.Left) return;

            ButtonHolder.DoDragDrop(PreDraggedTab, DragDropEffects.All);
        }

        private Button GetHoverTab()
        {
            foreach (object Button in ButtonHolder.Controls)
            {
                if (Button is Button)
                {
                    Button ButtonObj = Button as Button;

                    if (ButtonObj.ClientRectangle.Contains(ButtonObj.PointToClient(Cursor.Position)))
                    {
                        return ButtonObj;
                    }

                }
            }

            return null;
        }

        private void ButtonHolder_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(typeof(Button)) == null) return;

            Button dragTab = (Button)e.Data.GetData(typeof(Button));

            int dragTabIndex = PreDraggedTab.TabIndex;
            int hoverTabIndex;
            if (GetHoverTab() != null)
            {
                hoverTabIndex = GetHoverTab().TabIndex;
            }
            else return;

            if (hoverTabIndex < 0) { e.Effect = DragDropEffects.None; return; }
            Button hoverTab = GetHoverTab();
            e.Effect = DragDropEffects.Move;

            if (dragTab == hoverTab) return;

            Rectangle dragTabRect = dragTab.ClientRectangle;
            Rectangle hoverTabRect = hoverTab.ClientRectangle;

            if (dragTabRect.Width < hoverTabRect.Width)
            {
                Point TabHolderLoc = new Point(ButtonHolder.ClientRectangle.X, ButtonHolder.ClientRectangle.Y);

                if (dragTabIndex < hoverTabIndex)
                {
                    if ((e.X - TabHolderLoc.X) > (hoverTabRect.X + hoverTabRect.Width) - dragTabRect.Width)
                        SwapTabs(dragTab, hoverTab);

                }
                else if (dragTabIndex > hoverTabIndex)
                {
                    if ((e.X - TabHolderLoc.X) < (hoverTabRect.X + dragTabRect.Width))
                        SwapTabs(dragTab, hoverTab);
                }
            }
            else SwapTabs(dragTab, hoverTab);

            SelectedTab.PerformClick();
        }

        private void SwapTabs(Button DragTab, Button HoverTab)
        {

            int OldDragIndex = ButtonHolder.Controls.IndexOf(DragTab);
            int OldHoverIndex = ButtonHolder.Controls.IndexOf(HoverTab);

            ButtonHolder.Controls.SetChildIndex(DragTab, OldHoverIndex);
            ButtonHolder.Controls.SetChildIndex(HoverTab, OldDragIndex);

            ButtonHolder.Refresh();
        }

        private void NewButton_Click(object sender, EventArgs e)
        {
            bool ActivePage = false;
            NoFocusCueButton ButtonClicked = (NoFocusCueButton)sender;
            SelectedTab = ButtonClicked as NoFocusCueButton;


            foreach (Panel Page in Tabs.Values)
            {
                Page.Visible = false;
            }

            foreach (NoFocusCueButton TabButton in Tabs.Keys)
            {

                if (TabButton != ButtonClicked)
                {
                    try
                    {
                        TabButton.Controls[1].Visible = false;
                        TabButton.BackColor = Color.FromArgb(45,45,48);

                        NoFocusCueButton CB = TabButton.Controls[0] as NoFocusCueButton;
                        CB.BackColor = Color.FromArgb(45, 45, 48);

                        Tabs[ButtonClicked].Visible = true;
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        Console.WriteLine(TabButton.Controls.Count);
                    }
                }
                else
                {
                    TabButton.Controls[1].Visible = true;
                    TabButton.BackColor = Color.FromArgb(0, 122, 204);

                    NoFocusCueButton CB = TabButton.Controls[0] as NoFocusCueButton;
                    CB.BackColor = Color.FromArgb(0, 122, 204);
                }
            }

            foreach (Panel page in Tabs.Values)
            {
                if (page.Visible)
                {
                    ActivePage = true;
                    return;
                }
            }
            if (!ActivePage)
            {
                foreach (NoFocusCueButton tabbutton in Tabs.Keys)
                {
                    if (tabbutton.Controls[1].Visible)
                    {
                        Tabs[tabbutton].Visible = true;
                    }
                }
            }
        }
        private void NewButton_SizeChanged(object sender, EventArgs e)
        {
            int ButtonHeight = (sender as Button).Height;

            if (ButtonHeight == 13)
            {
                ButtonHolder.Size = new Size(ButtonHolder.Width, 50);
            }
            else if (ButtonHeight == 50)
            {
                ButtonHolder.Size = new Size(ButtonHolder.Width, 30);
                foreach (Control Curr in ButtonHolder.Controls)
                {
                    Curr.Height = ButtonHolder.Height;
                }

            }

            if (AddTab.Height == 50)
            {
                AddTab.Size = new Size(30, 30);
            }
        }

        private void NewButton_MouseUp(object sender, MouseEventArgs e)
        {
            NewButton_Click(sender, e);
            SelectedTab = (NoFocusCueButton)sender;
            PreDraggedTab = null;
            contextpoint = Cursor.Position;

        }

        private void NewButton_MouseDown(object sender, MouseEventArgs e)
        {
            (sender as NoFocusCueButton).PerformClick();
            PreDraggedTab = (NoFocusCueButton)sender;
            BackupSelectedTab = (NoFocusCueButton)sender;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            try
            {
                int indexofbutton = ButtonHolder.Controls.IndexOf(SelectedTab);
                TabIndexImpl--;
                Tabs[SelectedTab].Dispose();
                Tabs.Remove(SelectedTab);
                SelectedTab.Dispose();

                if (ButtonHolder.Controls.Count > 1)
                {
                    if (ButtonHolder.Controls[indexofbutton] != null)
                    {
                        (ButtonHolder.Controls[indexofbutton] as NoFocusCueButton).PerformClick();
                    }
                }
                else
                {
                    CreateTab();
                }
            }
            catch
            {
                if (ButtonHolder.Controls.Count > 1)
                {
                    (ButtonHolder.Controls[1] as NoFocusCueButton).PerformClick();
                }
            }

            if (TabIndexImpl < 0)
                TabIndexImpl = 0;
        }

        public void ReName(string Text)
        {
            SelectedTab.Text = Text + "   ";
        }

        public Scintilla GetWorkingEditor()
        {
            Scintilla Editor = new Scintilla();

            foreach (Panel TabPage in Tabs.Values)
            {
                if (TabPage.Visible)
                {


                    Editor = TabPage.Controls[0] as Scintilla;
                    break;
                }
            }

            return Editor;
        }

        private void NewButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Delete)
            {
                try
                {
                    int indexofbutton = ButtonHolder.Controls.IndexOf(SelectedTab);
                    TabIndexImpl--;
                    Tabs[SelectedTab].Dispose();
                    Tabs.Remove(SelectedTab);
                    SelectedTab.Dispose();

                    if (ButtonHolder.Controls.Count > 1)
                    {
                        if (ButtonHolder.Controls[indexofbutton] != null)
                        {
                            (ButtonHolder.Controls[indexofbutton] as NoFocusCueButton).PerformClick();
                        }
                    }
                    else
                    {
                        CreateTab();
                    }
                }
                catch { }
            }

            if (TabIndexImpl < 0)
                TabIndexImpl = 0;
        }

        public void SavePointedTab()
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Lua Files (*.lua)|*.lua|Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                string selection = SelectedTab.Text.Replace("   ", "");
                saveFileDialog.FileName = selection;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(saveFileDialog.FileName, GetWorkingEditor().Text);
                }
            }
        }

        public string GetTabName()
        {
            return SelectedTab.Text;
        }

        public void OpenFile()
        {
            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.Filter = "Lua Files (*.lua)|*.lua|Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                open.RestoreDirectory = true;

                if (open.ShowDialog() == DialogResult.OK)
                {
                    using (System.IO.StreamReader Reader = new System.IO.StreamReader(open.FileName))
                    {
                        NoFocusCueButton Tab = CreateTab(open.SafeFileName, Reader.ReadToEnd());
                        Tab.PerformClick();
                        SelectedTab = Tab;

                    }
                }
            }
        }

        public void SaveAllTabs(string Dir)
        {

            try
            {
                DirectoryInfo DInfo = Directory.CreateDirectory(Dir);
                DInfo.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }
            catch { }

            try
            {
                foreach (string File in Directory.GetFiles(Dir))
                {
                    System.IO.File.Delete(File);
                }
            }
            catch { }

            foreach (NoFocusCueButton Tab in Tabs.Keys)
            {
                if (Tabs[Tab].Controls[0].Text.Length == 0 || Tabs[Tab].Controls[0].Text == "")
                    return;

                using (StreamWriter writer = new StreamWriter($"{Dir}\\{Tab.Text}"))
                {
                    writer.Write(Tabs[Tab].Controls[0].Text);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NoFocusCueButton Button = CreateTab();
            SelectedTab = Button;
            BackupSelectedTab = Button;
            Button.PerformClick();
        }

        private void NewButton_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                SelectedTab = (sender as NoFocusCueButton);
                BackupSelectedTab = SelectedTab;
                MainStrip.Show(Cursor.Position);
                contextpoint = Cursor.Position;
            }
        }


        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseButton_Click(sender, e);
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            renameToolStripMenuItem.Visible = false;
            RenameBox.Visible = true;
            MainStrip.Show(Cursor.Position);
        }

        private void RenameBox_TextChanged(object sender, EventArgs e)
        {
            if (RenameBox.Text != string.Empty)
                SelectedTab.Text = RenameBox.Text + ".lua";
        }

        private void RenameBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                renameToolStripMenuItem.Visible = true;
                RenameBox.Visible = false;
                RenameBox.Text = "";
            }
            if (RenameBox.Text.Length > 15 && !(e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back))
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        private void MainStrip_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            RenameBox.Visible = false;
            renameToolStripMenuItem.Visible = true;
        }

        private void AddTab_Paint(object sender, PaintEventArgs e)
        {
            string drawString = "+";
            Font DrawFont = AddTab.Font;
            SolidBrush drawBrush = new SolidBrush(Color.FromArgb(158, 158, 158));

            StringFormat drawformat = new StringFormat();
            drawformat.Alignment = StringAlignment.Center;
            drawformat.LineAlignment = StringAlignment.Center;

            float X = 1F;
            float Y = -1F;

            e.Graphics.DrawString(drawString, DrawFont, drawBrush, new PointF(X, Y));
        }
    }
}

    public class Editor
    {

        public Scintilla NewScintilla(string Content)
        {
            var scintilla = new Scintilla();
            scintilla.AllowDrop = true;
            scintilla.BorderStyle = BorderStyle.None;
            scintilla.Lexer = Lexer.Lua;
            scintilla.Dock = DockStyle.Fill;
            scintilla.Styles[Style.Default].Font = "Consolas";
            scintilla.Styles[Style.Default].Size = 10;
            scintilla.Styles[Style.Default].BackColor = Color.FromArgb(30, 30, 30);
            scintilla.Styles[Style.Default].ForeColor = Color.FromArgb(255, 255, 255);
            scintilla.StyleClearAll();

            scintilla.Styles[Style.Lua.Identifier].ForeColor = Color.FromArgb(255, 255, 255);
            scintilla.Styles[Style.Lua.Comment].ForeColor = Color.FromArgb(140, 140, 140);
            scintilla.Styles[Style.Lua.CommentLine].ForeColor = Color.FromArgb(140, 140, 140);
            scintilla.Styles[Style.Lua.CommentDoc].ForeColor = Color.FromArgb(58, 64, 34);
            scintilla.Styles[Style.Lua.Number].ForeColor = Color.FromArgb(249, 145, 87);
            scintilla.Styles[Style.Lua.String].ForeColor = Color.FromArgb(143, 189, 143);
            scintilla.Styles[Style.Lua.Character].ForeColor = Color.FromArgb(143, 189, 143);
            scintilla.Styles[Style.Lua.LiteralString].ForeColor = Color.FromArgb(143, 189, 143);
            scintilla.Styles[Style.Lua.Operator].ForeColor = Color.FromArgb(102, 204, 204);
            scintilla.Styles[Style.LineNumber].BackColor = Color.FromArgb(30, 30, 30);

            scintilla.SetProperty("fold", "1");
            scintilla.SetProperty("fold.compact", "0");

            scintilla.Margins[0].Width = 19;
            scintilla.Margins[0].Type = MarginType.Number;
            scintilla.Margins[1].Type = MarginType.Symbol;
            scintilla.Margins[1].Mask = 4261412864u;
            scintilla.Margins[1].Sensitive = true;
            scintilla.Margins[1].Width = 8;

            for (var i = 0; i <= 31; i++)
            {
                scintilla.Markers[i].SetForeColor(Color.White);
                scintilla.Markers[i].SetBackColor(Color.White);
            }

            scintilla.Markers[Marker.Folder].Symbol = MarkerSymbol.BoxPlus;
            scintilla.Markers[Marker.FolderOpen].Symbol = MarkerSymbol.BoxMinus;
            scintilla.Markers[Marker.FolderEnd].Symbol = MarkerSymbol.BoxPlusConnected;
            scintilla.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
            scintilla.Markers[Marker.FolderOpenMid].Symbol = MarkerSymbol.BoxMinusConnected;
            scintilla.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
            scintilla.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;


            scintilla.AutomaticFold = AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change;
            scintilla.SetSelectionBackColor(true, Color.FromArgb(17, 177, 255));
            scintilla.SetSelectionForeColor(true, Color.Black);
            scintilla.SetFoldMarginColor(true, Color.FromArgb(30, 30, 30));
            scintilla.SetFoldMarginHighlightColor(true, Color.FromArgb(30, 30, 30));

            scintilla.Styles[Style.Lua.Word].ForeColor = Color.FromArgb(197, 148, 197); // Keywords
            scintilla.Styles[Style.Lua.Word2].ForeColor = Color.FromArgb(91, 132, 174); // Globals
            scintilla.Styles[Style.Lua.Word3].ForeColor = Color.FromArgb(91, 132, 174); // Functions
            scintilla.Styles[Style.Lua.Word4].ForeColor = Color.FromArgb(184, 215, 163); // Enums
            scintilla.Styles[Style.Lua.Word5].ForeColor = Color.FromArgb(91, 132, 174); // Exploit Funcs
            scintilla.Styles[Style.Lua.Word6].ForeColor = Color.FromArgb(249, 145, 87); // Logic
            scintilla.Styles[Style.Lua.Word7].ForeColor = Color.White; // Forced White
            scintilla.SetKeywords((int)KeywordTypes.Keywords, "and break do else elseif end for function if in local not or repeat return then until while continue [nonamecall]");
            scintilla.SetKeywords((int)KeywordTypes.Globals, "__add __call __concat __div __eq __index __le __len __lt __metatable __mod __mode __mul __newindex __pow __sub __tonumber __tostring __unm _G _VERSION assert collectgarbage dofile error getfenv getmetatable ipairs load loadfile loadstring module next pairs pcall print rawequal rawget rawset require select setfenv setmetatable tonumber tostring type unpack xpcall coroutine coroutine.create coroutine.resume coroutine.running coroutine.status create resume running status wrap yield debug getfenv gethook getinfo getlocal getmetatable getregistry getupvalue setfenv sethook setlocal setmetatable setupvalue traceback byte char dump find format gmatch gsub close flush input lines open output popen read stderr stdin stdout tmpfile type write abs acos asin atan atan2 ceil cos cosh deg exp floor fmod frexp huge ldexp log log10 max min modf pi pow rad random randomseed sin sinh sqrt tan tanh clock date difftime execute exit getenv remove rename setlocale time tmpname cpath loaded loaders loadlib path preload seeall len lower match rep reverse sub upper concat insert maxn remove sort coroutine.wrap coroutine.yield debug debug.debug debug.getfenv debug.gethook debug.getinfo debug.getlocal debug.getmetatable debug.getregistry debug.getupvalue debug.getupvalues debug.setfenv debug.sethook debug.setlocal debug.setmetatable debug.setupvalue debug.traceback io io.close io.flush io.input io.lines io.open io.output io.popen io.read io.stderr io.stdin io.stdout io.tmpfile io.type io.write math math.abs math.acos math.asin math.atan math.atan2 math.ceil math.cos math.cosh math.deg math.exp math.floor math.fmod math.frexp math.huge math.ldexp math.log math.log10 math.max math.min math.modf math.pi math.pow math.rad math.random math.randomseed math.sin math.sinh math.sqrt math.tan math.tanh os os.clock os.date os.difftime os.execute os.exit os.getenv os.remove os.rename os.setlocale os.time os.tmpname string string.byte string.char string.dump string.find string.format string.gmatch string.gsub string.len string.lower string.match string.rep string.reverse string.sub string.upper table table.concat table.insert table.maxn table.remove table.sort delay DebuggerManager elapsedTime LoadLibrary PluginManager printidentity require settings spawn stats tick time typeof UserSettings version wait warn bit32 bit32.arshift bit32.band bit32.bnot bit32.bor bit32.btest bit32.bxor bit32.extract bit32.replace bit32.lrotate bit32.lshift bi32.rrotate bit32.rshift utf8 utf8.codes utf8.char utf8.codepoint utf8.len utf8.offset utf8.graphemes utf8.nfcnormalize utf8.nfdnromalize utf8.charpattern Axes.new Axes BrickColor.Blue BrickColor.White BrickColor.Yellow BrickColor.Red BrickColor.Gray BrickColor.palette BrickColor.New BrickColor.Black BrickColor.Green BrickColor.Random BrickColor.DarkGray BrickColor.random BrickColor.new CFrame.lookAt CFrame.fromMatrix CFrame.fromAxisAngle CFrame.fromOrientation CFrame.fromEulerAnglesXYZ CFrame.Angles CFrame.fromEulerAnglesYXZ CFrame.new Color3.fromHSV Color3.toHSV Color3.fromRGB Color3.new ColorSequence.new DateTime.fromUnixTimestamp DateTime.now DateTime.fromIsoDate DateTime.fromUnixTimestampMillis DateTime.fromLocalTime DateTime.fromUniversalTime DockWidgetPluginGuiInfo.new Faces.new Instance.new NumberRange.new NumberSequence.new NumberSequenceKeypoint.new PathWaypoint.new PhysicalProperties.new Random.new Ray.new RaycastParams.new Rect.new RBXScriptSignal RBXScriptConnection Region3.new Region3int16.new TweenInfo.new UDim.new UDim2.fromOffset UDim2.fromScale UDim2.new Vector2.new Vector2int16.new Vector3.FromNormalId Vector3.FromAxis Vector3.fromAxis Vector3.fromNormalId Vector3.new Vector3int16.new Axes BrickColor CFrame Color3 ColorSequence ColorSequenceKeypoint DateTime DockWidgetPluginGuiInfo Enum EnumItem Enums Faces Instance NumberRange NumberSequence NumberSequenceKeypoint PathWaypoint PhysicalProperties Random Ray RaycastParams RaycastResult RBXScriptConnection RBXScriptSignal Rect Region3 Region3int16 TweenInfo UDim UDim2 Vector2 Vector2int16 Vector3 Vector3int16");
            scintilla.SetKeywords((int)KeywordTypes.Functions, "setnamecallmethod getnamecallmethod setfflag setclipboard appendfile writefile readfile checkcaller islclosure mousemoveabs mousemoverel mouse1release mouse1press mouse2click mouse1release mouse1press mouse1click isreadonly setreadonly setrawmetatable setrawmetatable debug.setmetatable getrawmetatable debug.getrawmetatable firetouchinterest fireclickdetector firesignal getconnections getnilinstances getgc getreg getrenv getgenv hookfunction hookfunc getregistry setstack getstack setconstant getconstant getconstants setupvalues getupvalues");
            scintilla.SetKeywords((int)KeywordTypes.Enums, "ABTestLoadingStatus ActionType ActuatorRelativeTo ActuatorType AdornCullingMode AlignType AlphaMode AnimationPriority AppShellActionType AspectType AssetFetchStatus AssetType AutoIndentRule AutomaticSize AvatarAssetType AvatarContextMenuOption AvatarItemType AvatarJointPositionType Axis BinType BodyPart BodyPartR15 BorderMode BreakReason BulkMoveMode BundleType Button ButtonStyle CameraMode CameraPanMode CameraType CatalogCategoryFilter CatalogSortType CellBlock CellMaterial CellOrientation CenterDialogType ChatColor ChatMode ChatPrivacyMode ChatStyle CollisionFidelity ComputerCameraMovementMode ComputerMovementMode ConnectionError ConnectionState ContextActionPriority ContextActionResult ControlMode CoreGuiType CreatorType CurrencyType CustomCameraModeDataStoreRequestType DevCameraOcclusionMode DevComputerCameraMovementMode DevComputerMovementMode DeveloperMemoryTag DeviceType DevTouchCameraMovementMode DevTouchMovementMode DialogBehaviorType DialogPurpose DialogTone DominantAxis DraftStatusCode EasingDirection EasingStyle ElasticBehavior EnviromentalPhysicsThrottle ExplosionType FieldOfViewMode FillDirection FilterResult Font FontSize FormFactor FramerateManagerMode FrameStyle FriendRequestEvent FriendStatus FunctionalTestResult GameAvatarType GearGenreSetting GearType Genre GraphicsMode HandlesStyle HorizontalAlignment HoverAnimateSpeed HttpCachePolicy HttpContentType HttpError HttpRequestType HumanoidCollisionType HumanoidDisplayDistanceType HumanoidHealthDisplayType HumanoidRigType HumanoidStateType IKCollisionsMode InfoType InitialDockState InOut InputType InterpolationThrottlingMode JointCreationMode KeyCode KeywordFilterType Language LanguagePreference LeftRight LevelOfDetailSetting Limb ListDisplayMode ListenerType Material MembershipType MeshPartDetailLevel MeshPartHeads MeshType MessageType ModelLevelOfDetail ModifierKey MouseBehavior MoveState NameOcclusion NetworkOwnership NormalId OutputLayoutMode OverrideMouseIconBehavior PacketPriority PartType PathStatus PathWaypointAction PermissionLevelShown PhysicsSimulationRate Platform PlaybackState PlayerActions PlayerChatType PoseEasingDirection PoseEasingStyle PrivilegeType ProductPurchaseDecision QualityLevel R15CollisionType RaycastFilterType RenderFidelity RenderingTestComparisonMethod RenderPriority ReturnKeyType ReverbType RibbonTool RollOffMode RotationType RuntimeUndoBehavior SavedQualitySetting SaveFilter ScaleType ScreenOrientation ScrollBarInset ScrollingDirection ServerAudioBehavior SizeConstraint SortOrder SoundType SpecialKey StartCorner Status StreamingPauseMode StudioDataModelType StudioScriptEditorColorCategories StudioScriptEditorColorPresets StudioStyleGuideColor StudioStyleGuideModifier Style SurfaceConstraint SurfaceGuiSizingMode SurfaceType SwipeDirection TableMajorAxis Technology TeleportMethod TeleportResult TeleportState TeleportType TextFilterContext TextInputType TextTruncate TextureMode TextureQueryType TextXAlignment TextYAlignment ThreadPoolConfig ThrottlingPriority ThumbnailSize ThumbnailType TickCountSampleMethod TopBottom TouchCameraMovementMode TouchMovementMode TriStateBoolean TweenStatus UiMessageType UITheme UserCFrame UserInputState UserInputType VerticalAlignment VerticalScrollBarPosition VibrationMotor VirtualInputMode VRTouchpad VRTouchpadMode WaterDirection WaterForce ZIndexBehavior");
            scintilla.SetKeywords((int)KeywordTypes.Logic, "true false nil");
            scintilla.SetKeywords((int)KeywordTypes.ExploitFuncs, "");
            scintilla.SetKeywords(6, "( )");


        scintilla.TabWidth = 0;
            scintilla.CaretForeColor = Color.White;

            scintilla.BackColor = Color.White;
            scintilla.Text = Content ?? "";

            scintilla.AllowDrop = true;


            scintilla.DragEnter += Scintilla_DragEnter;
            scintilla.DragDrop += Scintilla_DragDrop;

            return scintilla;
        }

        private void Scintilla_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void Scintilla_DragDrop(object sender, DragEventArgs e)
        {
            Scintilla editor = (sender as Scintilla);

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                IrisTabControl TabC = (editor.Parent.Parent.Parent.Parent as IrisTabControl);

                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string file in files)
                {
                    string ScriptName = file.Substring(file.LastIndexOf("\\"), file.Length - file.LastIndexOf("\\")).Replace("\\", "");
                    TabC.CreateTab(ScriptName, new StreamReader(file).ReadToEnd());
                }
            }
            else
            {
                // Console.WriteLine("yowtf");
            }
        }

        enum KeywordTypes
        {
            Keywords,
            Globals,
            Functions,
            Enums,
            ExploitFuncs,
            Logic
        }

    }

