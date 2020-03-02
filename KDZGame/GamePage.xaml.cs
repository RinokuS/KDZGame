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
    /// Логика взаимодействия для GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        List<Hero> _myTeam;
        List<Hero> _enemyTeam;
        int _round;
        bool _isGaming;

        public GamePage(List<Hero> myTeam, List<Hero> enemyTeam, int round)
        {
            InitializeComponent();

            _myTeam = myTeam;
            _enemyTeam = enemyTeam;
            _round = round;

            InitializeInterface();
        }

        void InitializeInterface()
        {
            InitializeTeam(Hero1, HP1, 1);
            InitializeTeam(Hero2, HP2, 2);
            InitializeTeam(Hero3, HP3, 3);
            InitializeTeam(Hero4, HP4, 4);
            InitializeTeam(Hero5, HP5, 5);
        }

        void InitializeTeam(Label label, ProgressBar bar, int number)
        {
            label.Content = GameSettings.myTeam[number - 1].Name;
            bar.Maximum = GameSettings.myTeam[number - 1].Health;
            bar.Value = GameSettings.myTeam[number - 1].Health;
        }
    }
}
