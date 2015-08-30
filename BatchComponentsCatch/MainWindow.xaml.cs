using SAPAutomation;
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
using SAPFEWSELib;

namespace BatchComponentsCatch
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Point start;
        Point end;
        bool isStartSet;
        IEnumerable<GuiVComponent> selectedComponents;
        public MainWindow()
        {
            InitializeComponent();
            SAPTestHelper.Current.SetSession();
            SAPTestHelper.Current.SAPGuiSession.ActiveWindow.Restore();
            this.Width = SAPTestHelper.Current.SAPGuiSession.ActiveWindow.Width;
            
            this.Height = SAPTestHelper.Current.SAPGuiSession.ActiveWindow.Height;

            this.Left = SAPTestHelper.Current.SAPGuiSession.ActiveWindow.Left;
            this.Top = SAPTestHelper.Current.SAPGuiSession.ActiveWindow.Top;
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(selectedComponents!=null)
            {
                foreach(var comp in selectedComponents)
                {
                    comp.Visualize(false);
                }
            }
            start = e.GetPosition(this);
            rect.SetValue(Canvas.LeftProperty, start.X);
            rect.SetValue(Canvas.TopProperty, start.Y);
            isStartSet = true;
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if(isStartSet && e.LeftButton == MouseButtonState.Pressed)
            {
                end = e.GetPosition(this);
                
                if (end.X < start.X)
                {
                    rect.SetValue(Canvas.LeftProperty, end.X);
                }
                if (end.Y < start.Y)
                {
                    rect.SetValue(Canvas.TopProperty, end.Y);
                }
                double width = Math.Abs(end.X - start.X);
                double height = Math.Abs(end.Y - start.Y);
             
                
                rect.Width = width;
                rect.Height = height;
                rectG_Area.Rect = new Rect(start,end);

                X1.Text = start.X.ToString();
                X2.Text = end.X.ToString();
                Y1.Text = start.Y.ToString();
                Y2.Text = end.Y.ToString();

            }
            
            
        }

        private void cv_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            int X1, X2, Y1, Y2;
            X1 = (int)start.X;
            X2 = (int)end.X;
            Y1 = (int)start.Y;
            Y2 = (int)end.Y;
            if(end.X < start.X)
            {
                X1 = (int)end.X;
                X2 = (int)start.X;
            }
            if(end.Y < start.Y)
            {
                Y1 = (int)end.Y;
                Y2 = (int)start.Y;
            }


            selectedComponents = SAPTestHelper.Current.SAPGuiSession.ActiveWindow.FindByRegion(X1,Y1,X2,Y2);
            foreach (var c in selectedComponents)
            {
                c.Visualize(true);
            }
            tb.Text = selectedComponents.Count().ToString();
        }
    }
}
