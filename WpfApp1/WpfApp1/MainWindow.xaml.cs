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

namespace WpfApp1
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            menuFrame.Navigate(new Menu());
        }

        private double aspectRatio = 1.78;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            aspectRatio = this.ActualWidth / this.ActualHeight;
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            if (sizeInfo.WidthChanged)
            {
                this.Width = sizeInfo.NewSize.Height * aspectRatio;
            }
            else
            {
                this.Height = sizeInfo.NewSize.Width * aspectRatio;
            }
        }
    }
}
