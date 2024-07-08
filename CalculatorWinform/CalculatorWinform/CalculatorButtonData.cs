using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculatorWinform
{
    public class CalculatorButtonData

    {
        public string KeyDisplaySymbol { get; set; }
        public string KeyValue { get; set; }
        public string KeyName { get; set; }
        public Point KeyLocation { get; set; }
        public Color ForeColor { get; set; }
        public Color BackColor { get; set; }
        public string KeyType { get; set; }
        public string CalculatorType { get; set; }


        public FlatStyle FlatStyle = Properties.Settings.Default.FlatStyle;
        public int BorderSize = Properties.Settings.Default.BorderSize;
        public Font Font = Properties.Settings.Default.Font;
    }

}
