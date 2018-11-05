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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interakční logika pro Menu.xaml
    /// </summary>
    public partial class Menu : Page
    {
        Sound menu = new Sound();
        public Menu()
        {
            InitializeComponent();        
            menu.Play("menu.mp3");
            menu.SetVolume(10);
        }
        private void Play_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Game());
            menu.Stop();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(1);
        }
    }
}
