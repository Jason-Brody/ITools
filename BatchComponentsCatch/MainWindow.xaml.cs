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

namespace BatchComponentsCatch
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Point start;
        Point temp;
        bool isStartSet;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            start = e.GetPosition(cv);
            rect.SetValue(Canvas.LeftProperty, start.X);
            rect.SetValue(Canvas.TopProperty, start.Y);
            isStartSet = true;
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if(isStartSet)
            {
                temp = e.GetPosition(cv);
                if (temp.X < start.X)
                {
                    rect.SetValue(Canvas.LeftProperty, temp.X);
                }
                if (temp.Y < start.Y)
                {
                    rect.SetValue(Canvas.TopProperty, temp.Y);
                }
                rect.Width = Math.Abs(temp.X - start.X);
                rect.Height = Math.Abs(temp.Y - start.Y);
            }
            
            
        }
    }
}
