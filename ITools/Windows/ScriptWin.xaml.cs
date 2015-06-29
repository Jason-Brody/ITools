using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ITools.Windows
{
    /// <summary>
    /// Interaction logic for ScriptWin.xaml
    /// </summary>
    public partial class ScriptWin : Window
    {
        public ScriptWin(string Script)
        {
            InitializeComponent();
            tb_Script.Text = Script;
        }
    }
}
