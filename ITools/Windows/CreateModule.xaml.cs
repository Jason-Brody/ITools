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
    /// Interaction logic for CreateModule.xaml
    /// </summary>
    public partial class CreateModule : Window
    {
        public bool IsCancel { get; set; }
        public string ModuleName { get; set; }
        public CreateModule()
        {
            InitializeComponent();
            IsCancel = true;
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            IsCancel = true;
            this.Close();
        }

        private void btn_OK_Click(object sender, RoutedEventArgs e)
        {
            if(checkModuleName())
            {
                IsCancel = false;
                this.Close();
            }
        }

        private bool checkModuleName()
        {
            ModuleName = tb_ModuleName.Text;
            if (!CodeHelper.IsValidVariable(ModuleName))
            {
                MessageBox.Show(string.Format("Module name {0} is not a valid name,please choose another name.", ModuleName));
                return false;
            }
            return true;
        }
        
    }
}
