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
    /// Interakční logika pro WonGame.xaml
    /// </summary>
    public partial class WonGame : Page
    {
        Sound winMillion = new Sound();
        Sound win = new Sound();
        int cash;
        public WonGame(int value)
        {
            InitializeComponent();
            wintext.Content = "Congratulations you have won " + value.ToString() + "$";
            if (value == 1000000)
            {
                winMillion.Play("winMillion.mp3");
                winMillion.SetVolume(50);
            }
            else
            {
                win.Play("win.mp3");
                win.SetVolume(50);
            }
            cash = value;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Menu());
            if(cash == 1000000)
            {
                winMillion.Stop();
            }
            else
            {
                win.Stop();
            }           
        }
    }
}
