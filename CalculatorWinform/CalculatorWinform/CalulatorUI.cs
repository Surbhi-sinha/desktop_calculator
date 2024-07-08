//using CalculatorApp;
using desktop_calculator;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;

namespace CalculatorWinform
{
    public partial class Calculator : Form
    {
        private TableLayoutPanel _calculatorWindowLayout;
        private TableLayoutPanel _memorySetLayout;
        private Button _memoryRead;
        private Button _memoryStore;
        private Button _memoryAdd;
        private Button _memoryClear;
        private Button _memorySubtract;
        private TabControl _tabControl;
        private TabPage _basicCalculator;
        private TabPage _advancedCalculator;
        private TextBox _calculatorTextBox;
        private Label _memoryLabel;
        private TableLayoutPanel _basicCalculatorLayout;
        private TableLayoutPanel _basicCalculatorButtonLayout;
        private TableLayoutPanel _advanceCalculatorLayout;
        private TableLayoutPanel _advanceCalculatorButtonLayout;
        private MenuStrip _menuStrip;
        private ToolStripMenuItem _editToolStripMenuItem;
        private ToolStripMenuItem _copyToolStripMenuItem;
        private ToolStripMenuItem _pasteToolStripMenuItem;
        private ToolStripMenuItem _helpToolStripMenuItem;
        private ToolStripMenuItem _exitToolStripMenuItem;
        private void InitializeComponent()
        {
            this._calculatorWindowLayout = new TableLayoutPanel();
            this._tabControl = new TabControl();
            this._basicCalculator = new TabPage();
            this._basicCalculatorLayout = new TableLayoutPanel();
            this._basicCalculatorButtonLayout = new TableLayoutPanel();

            this._menuStrip = new MenuStrip();
            this._editToolStripMenuItem = new ToolStripMenuItem();
            this._copyToolStripMenuItem = new ToolStripMenuItem();
            this._pasteToolStripMenuItem = new ToolStripMenuItem();
            this._helpToolStripMenuItem = new ToolStripMenuItem();
            this._exitToolStripMenuItem = new ToolStripMenuItem();

            this._memorySetLayout = new TableLayoutPanel();
            this._memoryRead = new Button();
            this._memoryStore = new Button();
            this._memoryAdd = new Button();
            this._memorySubtract = new Button();
            this._memoryClear = new Button();

            this._advancedCalculator = new TabPage();
            this._advanceCalculatorLayout = new TableLayoutPanel();
            this._calculatorTextBox = new TextBox();
            this._memoryLabel = new Label();
            this._advanceCalculatorButtonLayout = new TableLayoutPanel();

            this._calculatorWindowLayout.SuspendLayout();
            this._tabControl.SuspendLayout();
            this._basicCalculator.SuspendLayout();
            this._basicCalculatorLayout.SuspendLayout();
            this._basicCalculatorButtonLayout.SuspendLayout();
            this._advancedCalculator.SuspendLayout();
            this._advanceCalculatorLayout.SuspendLayout();
            this._advanceCalculatorButtonLayout.SuspendLayout();
            this.SuspendLayout();

            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(OnKeyDown);

            CreateButtonsfromKeys(_basicCalculatorButtonLayout, _basicKeys);
            CreateButtonsfromKeys(_advanceCalculatorButtonLayout, _advanceKeys);


            // Calculator Window
            this._calculatorWindowLayout.ColumnCount = 1;
            this._calculatorWindowLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this._calculatorWindowLayout.RowCount = 5;
            float[] layoutHeights = { 5F, 25F, 7F, 58F, 5F };
            foreach (float height in layoutHeights)
            {

                this._calculatorWindowLayout.RowStyles.Add(new RowStyle(SizeType.Percent, height));
            }
            this._calculatorWindowLayout.Controls.Add(this._menuStrip, 0, 0);
            this._calculatorWindowLayout.Controls.Add(this._calculatorTextBox, 0, 1);
            this._calculatorWindowLayout.Controls.Add(this._memorySetLayout, 0, 2);
            this._calculatorWindowLayout.Controls.Add(this._tabControl, 0, 3);
            this._calculatorWindowLayout.Controls.Add(this._memoryLabel, 0, 4);
            this._calculatorWindowLayout.Dock = DockStyle.Fill;

            this.CreateMenuLayout();

            this.CreateMemoryLayout();

            // _tabControl
            this._tabControl.Controls.Add(this._basicCalculator);
            this._tabControl.Controls.Add(this._advancedCalculator);
            this._tabControl.Dock = DockStyle.Fill;
            this._tabControl.Name = "TabControl";
            _tabControl.SelectedIndexChanged += OnTabSwitch;


            // _basicCalculatorLayout
            this.CreateCalculatorLayout(_basicCalculatorLayout, "BasicCalculatorLayout", _basicCalculatorButtonLayout);
            // _basicCalculatorTab
            this.CalculatorTabLayout(_basicCalculator, _basicCalculatorLayout, ButtonTextData.BasicCalculatorText, "BasicCalculator");
            // _basicCalculatorButtonLayout
            this.CreateButtonLayout(4, 5, _basicCalculatorButtonLayout, "BasicCalculatorButtonLayout");


            // _advanceCalculatorLayout            
            this.CreateCalculatorLayout(_advanceCalculatorLayout, "AdvanceCalculatorLayout", _advanceCalculatorButtonLayout);
            // _advancedCalculatorTab
            this.CalculatorTabLayout(_advancedCalculator, _advanceCalculatorLayout, ButtonTextData.AdvancedCalculatorText, "AdvancedCalculator");
            // _advanceCalculatorbuttonlayout
            this.CreateButtonLayout(5, 6, _advanceCalculatorButtonLayout, "_advanceCalculatorButtonLayout");


            // 
            // _CalculatorText
            // 
            this._calculatorTextBox.Dock = DockStyle.Fill;
            this._calculatorTextBox.Text = Constants.IntChar0.ToString();
            this._calculatorTextBox.Font = Properties.Settings.Default.CalculatorTextBoxFont;
            this._calculatorTextBox.TextAlign = HorizontalAlignment.Right;
            this._calculatorTextBox.Multiline = true;
            this._calculatorTextBox.BackColor = Color.WhiteSmoke;
            this._calculatorTextBox.BorderStyle = BorderStyle.None;
            this._calculatorTextBox.ReadOnly = true;
            this._calculatorTextBox.Name = "CalculatorTextLabel";

            //Memory Label

            this._memoryLabel.Dock = DockStyle.Fill;
            this._memoryLabel.Text = ButtonTextData.MemoryValue;
            this._memoryLabel.Font = new Font("arial", 15, FontStyle.Regular);
            this._memoryLabel.TextAlign = ContentAlignment.MiddleRight;
            this._memoryLabel.Padding = new Padding(1);
            this._memoryLabel.Name = "_memoryLabel";

            // Calculator
            this.MinimumSize = new Size(500, 650);
            this.Controls.Add(_calculatorWindowLayout);
            //this.Icon = new Icon(ConfigurationManager.AppSettings["CalculatorIcon"]);
            this.Name = "Calculator";
            this.Text = ButtonTextData.Calculator;

            this._calculatorWindowLayout.ResumeLayout(false);
            this._tabControl.ResumeLayout(false);
            this._calculatorWindowLayout.PerformLayout();
            this._basicCalculator.ResumeLayout(false);
            this._basicCalculatorLayout.ResumeLayout(false);
            this._basicCalculatorButtonLayout.ResumeLayout(false);

            this._menuStrip.ResumeLayout(false);
            this._menuStrip.PerformLayout();

            this._advancedCalculator.ResumeLayout(false);
            this._advanceCalculatorLayout.ResumeLayout(false);
            this._basicCalculatorLayout.PerformLayout();
            this._advanceCalculatorLayout.PerformLayout();
            this._advanceCalculatorButtonLayout.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private void CreateMenuLayout()
        {
            // _menuStrip
            this._menuStrip.Items.AddRange(new ToolStripItem[]
            {
                this._editToolStripMenuItem,
                this._helpToolStripMenuItem,
                this._exitToolStripMenuItem
            });
            this._menuStrip.Name = "MenuStrip";
            this._menuStrip.Dock = DockStyle.Fill;
            this._menuStrip.BackColor = Color.LightGray;

            // _editToolStripMenuItem
            this._editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
            this._copyToolStripMenuItem,
            this._pasteToolStripMenuItem});
            this._editToolStripMenuItem.Name = "EditToolStripMenuItem";
            this._editToolStripMenuItem.Text = ButtonTextData.Edit;
            this._editToolStripMenuItem.Font = Properties.Settings.Default.MenuItemFont;

            // _copyToolStripMenuItem
            CreateToolStripMenuItem(_copyToolStripMenuItem, ButtonTextData.Copy, OnCopyClick, "CopyToolStripMenuItem");
            // _pasteToolStripMenuItem
            CreateToolStripMenuItem(_pasteToolStripMenuItem, ButtonTextData.Paste, OnPasteClick, "PasteToolStripMenuItem");

            // _helpToolStripMenuItem
            this._helpToolStripMenuItem.Name = "HelpToolStripMenuItem";
            this._helpToolStripMenuItem.Text = ButtonTextData.Help;
            this._helpToolStripMenuItem.Font = Properties.Settings.Default.MenuItemFont;
            this._helpToolStripMenuItem.Click += OnHelpClick;

            // exitToolStripMenuItem
            this._exitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this._exitToolStripMenuItem.Text = ButtonTextData.Exit;
            this._exitToolStripMenuItem.Font = Properties.Settings.Default.MenuItemFont;
            this._exitToolStripMenuItem.ForeColor = Color.Red;
            this._exitToolStripMenuItem.Click += OnExitClick;

        }

        private void CreateToolStripMenuItem(ToolStripMenuItem parent, string text, EventHandler onClick, string name)
        {
            parent.Name = name;
            parent.Text = text;
            parent.Click += onClick;
        }

        private void CreateMemoryLayout()
        {
            //memory buttons strip
            this._memorySetLayout.Name = "MemorySet";
            this._memorySetLayout.RowCount = 1;
            this._memorySetLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this._memorySetLayout.ColumnCount = 5;
            for (int i = 0; i < 5; i++)
            {
                this._memorySetLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            }

            this._memorySetLayout.Controls.Add(this._memoryStore, 0, 0);
            this._memorySetLayout.Controls.Add(this._memoryRead, 1, 0);
            this._memorySetLayout.Controls.Add(this._memoryAdd, 2, 0);
            this._memorySetLayout.Controls.Add(this._memorySubtract, 3, 0);
            this._memorySetLayout.Controls.Add(this._memoryClear, 4, 0);

            //memory buttons;
            this.CreateMemorySetButton(_memoryAdd, "MemoryAdd", ButtonTextData.MemoryAdd, false, OnMemoryAddClick);
            this.CreateMemorySetButton(_memoryRead, "MemoryRead", ButtonTextData.MemoryRead, false, OnMemoryReadClick);
            this.CreateMemorySetButton(_memoryStore, "MemoryStore", ButtonTextData.MemoryStore, true, OnMemoryStoreClick);
            this.CreateMemorySetButton(_memoryClear, "MemoryClear", ButtonTextData.MemoryClear, false, OnMemoryClearClick);
            this.CreateMemorySetButton(_memorySubtract, "MemorySubtract", ButtonTextData.MemorySubtract, false, OnMemorySubtractClick);

        }

        private void CreateCalculatorLayout(TableLayoutPanel layout, string layoutName, TableLayoutPanel childControl)
        {
            layout.ColumnCount = 1;
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layout.RowCount = 1;
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layout.Controls.Add(childControl, 0, 0);
            layout.Dock = DockStyle.Fill;
            layout.Name = layoutName;
        }

        private void CreateButtonLayout(int colCount, int rowCount, TableLayoutPanel parent, string name)
        {
            parent.ColumnCount = colCount;
            float colWidth = 100F / colCount;
            for (int i = 0; i < colCount; i++)
            {
                parent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, colWidth));
            }
            parent.Dock = DockStyle.Fill;
            parent.Name = name;
            parent.RowCount = rowCount;
            float rowHeight = 100F / rowCount;
            for (int i = 0; i < rowCount; i++)
            {
                parent.RowStyles.Add(new RowStyle(SizeType.Percent, rowHeight));
            }
        }

        private void CreateMemorySetButton(Button parent, string name, string text, bool enabled, EventHandler onClick)
        {
            parent.Name = name;
            parent.Text = text;
            parent.FlatStyle = FlatStyle.Flat;
            parent.FlatAppearance.BorderSize = 0;
            parent.Click += onClick;
            parent.Enabled = enabled;

        }

        private void CalculatorTabLayout(TabPage parent, TableLayoutPanel child, string text, string name)
        {
            parent.Controls.Add(child);
            parent.Dock = DockStyle.Fill;
            parent.Name = name;
            parent.Text = text;
        }

        private void CreateButtonsfromKeys(TableLayoutPanel parent, List<CalculatorButtonData> buttonKeys)
        {
            foreach (CalculatorButtonData key in buttonKeys)
            {
                //intialise button
                Button button = CreateButton(key);
                //Adding in ButtonLayoutControls
                parent.Controls.Add(button, key.KeyLocation.Y, key.KeyLocation.X);
            }
        }
    }
}
