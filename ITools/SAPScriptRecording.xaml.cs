using SAPFEWSELib;
using SAPGuiAutomationLib;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

namespace ITools
{
    /// <summary>
    /// Interaction logic for SAPScriptRecording.xaml
    /// </summary>
    public partial class SAPScriptRecording : UserControl,IWorking
    {
        ObservableCollection<RecordStep> steps = new ObservableCollection<RecordStep>();
        private List<RecordStep> mySteps;
        private GuiSession _session;

        
        public SAPScriptRecording()
        {
            InitializeComponent();
            (App.Current.MainWindow as MainWindow).OnSetSession += SAPScriptRecording_OnSetSession;
            dg_Step.DataContext = steps;
            this.IsEnabled = false;
            setRecordStatus(false);

            steps.CollectionChanged += steps_CollectionChanged;
        }

        void steps_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            for(int i = 0;i<steps.Count ;i++)
            {
                steps[i].StepId = i + 1;
            }
        }

        void SAPScriptRecording_OnSetSession(SAPFEWSELib.GuiSession session)
        {
            _session = session;
            this.IsEnabled = true;
        }

        private string upperFirstChar(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        private void setRecordStatus(bool isRecord)
        {
            btn_Record.IsEnabled = !isRecord;
            btn_Stop.IsEnabled = isRecord;
        }

        private void btn_Record_Click(object sender, RoutedEventArgs e)
        {
            setRecordStatus(true);
            SAPAutomationHelper.Current.StartRecording((r) =>
            {
                dg_Step.Dispatcher.BeginInvoke(new Action(() =>
                {
                    r.ActionName = upperFirstChar(r.ActionName);
                    r.CompInfo.Id = r.CompInfo.Id.Substring(19);
                    steps.Add(r);
                }));
            });
        }

        private void btn_Stop_Click(object sender, RoutedEventArgs e)
        {
            setRecordStatus(false);
            SAPAutomationHelper.Current.StopRecording();
        }

        private void mi_Run_Click(object sender, RoutedEventArgs e)
        {
            SAPAutomationHelper.Current.StopRecording();
            setRecordStatus(false);
            if (dg_Step.SelectedItems != null)
            {
                foreach (RecordStep step in dg_Step.SelectedItems)
                {
                    SAPAutomationHelper.Current.RunAction(step);
                }
            }
        }

      

        private void dg_Step_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            if (dg_Step.SelectedItems != null)
            {
                mySteps = dg_Step.SelectedItems.Cast<RecordStep>().ToList();
            }
        }

        private void dg_Step_MouseMove(object sender, MouseEventArgs e)
        {
            
            if (mySteps != null && mySteps.Count > 0 && e.LeftButton == MouseButtonState.Pressed )
            {

                //if (p.X > 0 && p.Y > 0 && p.X < dg.ActualWidth && p.Y < dg.ActualHeight)
                //    return;

                CodeMemberMethod runMethod = new CodeMemberMethod();
                runMethod.Attributes = MemberAttributes.Static | MemberAttributes.Public;
                runMethod.Name = "recordAction";

                runMethod.Statements.Add(new CodeExpressionStatement(
                    new CodeMethodInvokeExpression(
                        new CodeVariableReferenceExpression("SAPTestHelper.Current"),
                        "SetSession", new CodeExpression[0])));


                foreach (RecordStep step in mySteps)
                {
                    runMethod.Statements.Add(step.GetCodeDetailStatement());
                }

                CodeDomProvider provider = CodeDomProvider.CreateProvider("c#");
                CodeGeneratorOptions options = new CodeGeneratorOptions();
                options.BracingStyle = "C";
                StringBuilder sb = new StringBuilder();

                using (TextWriter sourceWriter = new StringWriter(sb))
                {
                    provider.GenerateCodeFromMember(runMethod, sourceWriter, options);
                }
                DragDrop.DoDragDrop(dg_Step, sb.ToString(), DragDropEffects.Copy);
            }
            
        }

        public event OnWorkingHanlder OnWorking;

        private void btn_Run_Click(object sender, RoutedEventArgs e)
        {
            OnWorking(this, null);
        }
    }
}
