using ITools.ViewModel;
using ITools.Windows;
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
using System.Reflection.Emit;
using System.Data;
using System.Reflection;

namespace ITools
{
    /// <summary>
    /// Interaction logic for SAPScriptRecording.xaml
    /// </summary>
    public partial class SAPScriptRecording : UserControl, IWorking
    {
        ObservableCollection<RecordStep> steps = new ObservableCollection<RecordStep>();
        //private List<RecordStep> mySteps;
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
            for (int i = 0; i < steps.Count; i++)
            {
                steps[i].StepId = i + 1;
                steps[i].IsParameterize = true;
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
                    //Step s = new Step(r);
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

        private async void mi_Run_Click(object sender, RoutedEventArgs e)
        {
            if (dg_Step.SelectedItems != null)
            {
                SAPAutomationHelper.Current.StopRecording();
                setRecordStatus(false);
                var runSteps = dg_Step.SelectedItems.Cast<RecordStep>();
                await Task.Run(() =>
                {
                    WorkingEventArgs ae = new WorkingEventArgs();
                    ae.IsProcessKnow = true;
                    ae.Max = runSteps.Count();

                    for (int i = 0; i < runSteps.Count(); i++)
                    {
                        ae.Current = i + 1;
                        SAPAutomationHelper.Current.RunAction(runSteps.ElementAt(i));
                        if (OnWorking != null)
                        {
                            OnWorking(this, ae);
                        }
                    }

                });
                if (AfterWorking != null)
                    AfterWorking(this, null);

            }





        }



        private void dg_Step_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            //if (dg_Step.SelectedItems != null)
            //{
            //    mySteps = dg_Step.SelectedItems.Cast<RecordStep>().ToList();
            //}
        }

        private void dg_Step_MouseMove(object sender, MouseEventArgs e)
        {

            //if (mySteps != null && mySteps.Count > 0 && e.LeftButton == MouseButtonState.Pressed && e.OriginalSource.GetType() != typeof(System.Windows.Controls.Primitives.Thumb))
            //{
            //    var source = e;
            //    //if (p.X > 0 && p.Y > 0 && p.X < dg.ActualWidth && p.Y < dg.ActualHeight)
            //    //    return;

            //    CodeMemberMethod runMethod = new CodeMemberMethod();
            //    runMethod.Attributes = MemberAttributes.Static | MemberAttributes.Public;
            //    runMethod.Name = "recordAction";

            //    runMethod.Statements.Add(new CodeExpressionStatement(
            //        new CodeMethodInvokeExpression(
            //            new CodeVariableReferenceExpression("SAPTestHelper.Current"),
            //            "SetSession", new CodeExpression[0])));


            //    foreach (RecordStep step in mySteps)
            //    {
            //        runMethod.Statements.Add(step.GetCodeDetailStatement());
            //    }

            //    CodeDomProvider provider = CodeDomProvider.CreateProvider("c#");
            //    CodeGeneratorOptions options = new CodeGeneratorOptions();
            //    options.BracingStyle = "C";
            //    StringBuilder sb = new StringBuilder();

            //    using (TextWriter sourceWriter = new StringWriter(sb))
            //    {
            //        provider.GenerateCodeFromMember(runMethod, sourceWriter, options);
            //    }
            //    DragDrop.DoDragDrop(dg_Step, sb.ToString(), DragDropEffects.Copy);
            //}

        }





        public event OnWorkingHandler OnWorking;

        public event OnWorkFinishHandler AfterWorking;

        private void mi_CSharp_Click(object sender, RoutedEventArgs e)
        {
            CodeMemberMethod runMethod = new CodeMemberMethod();
            runMethod.Attributes = MemberAttributes.Static | MemberAttributes.Public;
            runMethod.Name = "recordAction";

            runMethod.Statements.Add(new CodeExpressionStatement(
                new CodeMethodInvokeExpression(
                    new CodeVariableReferenceExpression("SAPTestHelper.Current"),
                    "SetSession", new CodeExpression[0])));


            foreach (RecordStep step in dg_Step.SelectedItems.Cast<RecordStep>().ToList())
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

            Clipboard.SetData(DataFormats.Text, sb.ToString());
        }

        private void mi_HightLight_Click(object sender, RoutedEventArgs e)
        {
            //var data = dg_Step.SelectedItem;
            //var dgr = dg_Step.ItemContainerGenerator.ContainerFromItem(data) as DataGridRow;
            //var backgroud = dgr.Background;
            //if (dg_Step.SelectedItems != null)
            //{
            //    foreach (var row in dg_Step.SelectedItems)
            //    {
            //        var dgr = dg_Step.ItemContainerGenerator.ContainerFromItem(row) as DataGridRow;
            //        dgr.Background = new SolidColorBrush(Colors.GreenYellow);
            //    }
            //}
        }

        private void mi_Test_Click(object sender, RoutedEventArgs e)
        {
            if (dg_Step.SelectedItem != null)
            {
                List<SAPDataParameter> paras = new List<SAPDataParameter>();
                ParameterWindow win = new ParameterWindow(dg_Step.SelectedItems.Cast<RecordStep>(), paras);
                win.ShowDialog();


                DataTable dt = createTable(paras);
                dg_Parameter.DataContext = dt;
            }

            //CodeTypeMember typeMember = SAPAutomationExtension.GetDataClass("Test", paras);
            //CodeHelper.GetCode<CodeTypeMember>(typeMember, p => p.GenerateCodeFromMember);


        }

        private DataTable createTable(List<SAPDataParameter> parameters)
        {
            DataTable dt = new DataTable();
            foreach (var p in parameters)
            {
                DataColumn dc = new DataColumn(p.Name, p.Type);
                dt.Columns.Add(dc);
            }
            DataRow dr = dt.NewRow();
            foreach (var p in parameters)
            {
                dr[p.Name] = p.Value;
            }
            dt.Rows.Add(dr);
            return dt;
        }

        private void mi_Refresh_Click(object sender, RoutedEventArgs e)
        {
            dg_Step.DataContext = steps;
        }
    }
}
