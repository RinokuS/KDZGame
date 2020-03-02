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

namespace KDZGame
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        readonly Window _mainWindow;
        public MainPage(Window mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }
        private void startBtnClick(object sender, RoutedEventArgs e)
        {
            ((MainWindow)_mainWindow).Main.Navigate(new FilterPage(_mainWindow));
        }
    }
}
