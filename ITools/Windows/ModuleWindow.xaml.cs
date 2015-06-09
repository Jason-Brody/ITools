using ITools.ViewModel;
using SAPGuiAutomationLib;
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
    /// Interaction logic for ModuleWindow.xaml
    /// </summary>
    public partial class ModuleWindow : Window
    {
        private bool _isSet;
        private SAPModuleVM _vm;
        public ModuleWindow(out SAPModuleVM attribute)
        {
            InitializeComponent();
            attribute = new SAPModuleVM();
            GD_Module.DataContext = attribute;
            _vm = attribute;
            
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            _isSet = false;
            this.Close();
        }

        private void btn_OK_Click(object sender, RoutedEventArgs e)
        {
            _isSet = true;
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _vm.IsSet = _isSet;
        }

        
    }
}
