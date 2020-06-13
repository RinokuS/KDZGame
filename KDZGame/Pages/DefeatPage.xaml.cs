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
    /// Логика взаимодействия для DefeatPage.xaml
    /// </summary>
    public partial class DefeatPage : Page
    {
        readonly Window _mainWindow;
        public DefeatPage(Window mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        private void endBtn_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)_mainWindow).Main.Navigate(new MainPage(_mainWindow));
        }
    }
}
