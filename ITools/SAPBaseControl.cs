using SAPFEWSELib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ITools
{
    public class SAPBaseControl:UserControl
    {
        protected GuiSession _session;
        protected Action _afterGetSession = null;
        public SAPBaseControl()
        {
            (App.Current.MainWindow as MainWindow).OnSetSession += SAPBaseUserControl_OnSetSession;
            this.IsEnabled = false;
        }

        void SAPBaseUserControl_OnSetSession(GuiSession session)
        {
            _session = session;
            this.IsEnabled = true;
            if (_afterGetSession != null)
                _afterGetSession();
        }
    }
}
