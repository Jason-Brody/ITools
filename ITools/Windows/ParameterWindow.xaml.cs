using SAPGuiAutomationLib;
using SAPTestRunTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for ParameterWindow.xaml
    /// </summary>
    public partial class ParameterWindow : Window
    {

     

        public ParameterWindow(IEnumerable<RecordStep> Steps,IList<SAPDataParameter> Paras)
        {
            InitializeComponent();
            SetParameters(Steps,Paras);
        }

        public void SetParameters(IEnumerable<RecordStep> Steps, IList<SAPDataParameter> Paras)
        {
            foreach (var step in Steps)
            {
                if (step.ActionParams != null && step.ActionParams.Count() > 0)
                {
                    if (step.Action == BindingFlags.InvokeMethod)
                    {
                        MethodInfo method = SAPAutomationHelper.Current.GetSAPTypeInfo<MethodInfo>(step.CompInfo.Type, t => t.GetMethod(step.ActionName));
                        int index = 0;
                        foreach (var pInfo in method.GetParameters())
                        {
                            SAPDataParameter p = new SAPDataParameter();
                            p.Id = step.StepId;
                            p.Name = pInfo.Name;
                            p.Comment = pInfo.Name;
                            p.Type = pInfo.ParameterType;
                            p.Value = step.ActionParams[index];
                            Paras.Add(p);
                            index++;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < step.ActionParams.Count(); i++)
                        {
                            SAPDataParameter p = new SAPDataParameter();
                            p.Id = step.StepId;
                            if (step.CompInfo.Tip != null)
                            {
                                p.Name = step.CompInfo.Tip.Replace(" ", "_");
                                p.Comment = step.CompInfo.Tip;
                            }
                            p.Value = step.ActionParams[i];
                            p.Type = step.ActionParams[i].GetType();
                            Paras.Add(p);
                        }
                    }
                    step.IsParameterize = true;


                }
            }
            dg_Parameters.DataContext = Paras;
        }

        

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_OK_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
