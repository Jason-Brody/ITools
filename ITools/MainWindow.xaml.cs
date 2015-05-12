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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SAPGuiAutomationLib;
using SAPTestRunTime;
using System.Reflection;
using ITools.ViewModel;
using SAPFEWSELib;

namespace ITools
{
    public delegate void OnSessionSettingHanlder(GuiSession session);

    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public event OnSessionSettingHanlder OnSetSession;

        private GuiSession _session;
        private GuiApplication _app;
        public MainWindow()
        {
            InitializeComponent();
            SAPAutomationHelper.Current.SetSAPApiAssembly();
        }

        private void setSession(GuiSession session)
        {
            _session = session;
            SAPAutomationHelper.Current.SetSession(_session);
            if (OnSetSession != null)
            {
                OnSetSession(_session);
            }
        }

        private void btn_OK_Click(object sender, RoutedEventArgs e)
        {
            if(lv_Sessions.SelectedItem != null && _app != null)
            {
                var vm = lv_Sessions.SelectedItem as SAPSessionVM;
                GuiSession session = _app.FindById<GuiSession>(vm.Id);
                setSession(session);
            }
            
            
            ep_Session.IsExpanded = false;
            
        }

        private void ep_Session_Expanded(object sender, RoutedEventArgs e)
        {
            _app = null;
            try
            {
                _app = SAPTestHelper.GetSAPGuiApp(1);
                List<SAPSessionVM> sessions = new List<SAPSessionVM>();
                for (int i = 0; i < _app.Children.Count; i++)
                {
                    GuiConnection cn = _app.Children.ElementAt(i) as GuiConnection;
                    for (int j = 0; j < cn.Children.Count; j++)
                    {
                        GuiSession s = cn.Children.ElementAt(j) as GuiSession;
                        sessions.Add(new SAPSessionVM()
                        {
                            Id = s.Id,
                            System = s.Info.SystemName,
                            Transaction = s.Info.Transaction
                        });
                    }
                }
                lv_Sessions.ItemsSource = sessions;
            }
            catch { }
        }

        private void ep_Session_Collapsed(object sender, RoutedEventArgs e)
        {
            lv_Sessions.ItemsSource = null;
        }

        


    }
}
