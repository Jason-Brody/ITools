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
using System.Windows.Shapes;

namespace ITools.Windows
{
    /// <summary>
    /// Interaction logic for SAPLogView.xaml
    /// </summary>
    public partial class SAPLogView : Window
    {
        ScreenData sc = null;

        string folder = @"C:\Demo\";

        public SAPLogView()
        {
            InitializeComponent();
            
        }

        private void load()
        {
            List<ScreenData> datas = null;
            System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(List<ScreenData>));
            using (System.IO.FileStream fs = new System.IO.FileStream(folder + "1.xml", System.IO.FileMode.Open))
            {
                datas = xs.Deserialize(fs) as List<ScreenData>;
            }
            lv.DataContext = datas;
        }

        private void lv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sc = (sender as ListView).SelectedItem as ScreenData;
            BitmapImage image = new BitmapImage(new Uri(folder + sc.ScreenShot));
            img.Source = image;
            img.Width = image.PixelWidth;
            img.Height = image.PixelHeight;
            
        }

        private void lv_Details_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var se = (sender as ListView).SelectedItem as SAPGuiElement;
            if(se == null)
            {
                rect.Width = 0;
                rect.Height = 0;

            }
            else
            {
                rect.Width = se.Width;
                rect.Height = se.Height;
                Canvas.SetTop(rect, se.AbsoluteTop);
                Canvas.SetLeft(rect, se.AbsoluteLeft);
            }
            
        }

        private void btn_Refresh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                load();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
