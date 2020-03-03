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
        readonly Window _mainWindow;
        static Random rnd = new Random();
        List<Hero> _myTeam;
        List<Hero> _enemyTeam;

        int _round;
        bool _isGaming;

        public GamePage(Window mainWindow, List<Hero> myTeam, List<Hero> enemyTeam, int round)
        {
            InitializeComponent();
            _mainWindow = mainWindow;

            _isGaming = true;

            _myTeam = myTeam;
            _enemyTeam = enemyTeam;
            _round = round;

            InitializeInterface();
        }

        void InitializeInterface()
        {
            InitializeTeam(Hero1, HP1, atkLabel1, dmgLabel1, defLabel1, grthLabel1, 1);
            defButton1.IsEnabled = false;
            InitializeTeam(Hero2, HP2, atkLabel2, dmgLabel2, defLabel2, grthLabel2, 2);
            defButton2.IsEnabled = false;
            InitializeTeam(Hero3, HP3, atkLabel3, dmgLabel3, defLabel3, grthLabel3, 3);
            defButton3.IsEnabled = false;
            InitializeTeam(Hero4, HP4, atkLabel4, dmgLabel4, defLabel4, grthLabel4, 4);
            defButton4.IsEnabled = false;
            InitializeTeam(Hero5, HP5, atkLabel5, dmgLabel5, defLabel5, grthLabel5, 5);
            defButton5.IsEnabled = false;
        }

        void InitializeTeam(Label label, ProgressBar bar, Label atkLab, Label dmgLab, Label defLab, Label grthLab, int number)
        {
            label.Content = GameSettings.myTeam[number - 1].Name;
            bar.Maximum = GameSettings.myTeam[number - 1].Health;
            bar.Value = GameSettings.myTeam[number - 1].Health;
            atkLab.Content = "Atk: " + GameSettings.myTeam[number - 1].Attack;
            dmgLab.Content = "Dmg: " + GameSettings.myTeam[number - 1].minDmg + "-" + GameSettings.myTeam[number - 1].maxDmg;
            defLab.Content = "Def: " + GameSettings.myTeam[number - 1].Defence;
            grthLab.Content = "Gth: " + GameSettings.myTeam[number - 1].Growth;
        }

        void GameAtk(Hero myHero)
        {
            Hero defendingEnemy;
            do
            {
                defendingEnemy = GameSettings.enemyTeam[rnd.Next(GameSettings.enemyTeam.Count)];
            } while (!defendingEnemy.Alive);
            int enemyHP = defendingEnemy.Health;

            myHero.AttackAction(defendingEnemy);

            gameInfoBox.Text = "Attack completed! \r\n" +
                "Your opponent was: " + defendingEnemy.Name + "\r\n" +
                "Killed? : " + (defendingEnemy.Alive?"Nope\r\n":"Hell Yea\r\n") +
                "Damage: " + (enemyHP - defendingEnemy.Health) + "\r\n" +
                "Prepare to defend!";

            IsEveryoneAlive();

            for (int i = 0; i < GameSettings.myTeam.Count; i++)
            {
                if (GameSettings.myTeam[i].Alive)
                {
                    DefencePrep(i);
                }
            }
        }

        void DefencePrep(int number)
        {
            if (number == 0)
            {
                defButton1.IsEnabled = true;
            }
            else if (number == 1)
            {
                defButton2.IsEnabled = true;
            }
            else if (number == 2)
            {
                defButton3.IsEnabled = true;
            }
            else if (number == 3)
            {
                defButton4.IsEnabled = true;
            }
            else if (number == 4)
            {
                defButton5.IsEnabled = true;
            }
        }

        void GameDef(Hero myHero)
        {
            Hero attackingEnemy;
            do
            {
                attackingEnemy = GameSettings.enemyTeam[rnd.Next(GameSettings.enemyTeam.Count)];
            } while (!attackingEnemy.Alive);
            int myHP = myHero.Health;

            attackingEnemy.AttackAction(myHero);

            gameInfoBox.Text = "Defence completed! \r\n" +
                "Your opponent was: " + attackingEnemy.Name + "\r\n" +
                (myHero.Alive?(myHero.Name + " is Alive!"):(myHero.Name + " is Dead...")) +
                "Damage: " + (myHP - myHero.Health) + "\r\n" +
                "We must revenge them!";

            IsEveryoneAlive();

            for (int i = 0; i < GameSettings.myTeam.Count; i++)
            {
                if (GameSettings.myTeam[i].Alive)
                {
                    AttackPrep(i);
                    HPCheck(i);
                }
                else
                {
                    if (GameSettings.myTeam[i].RoundsDead <= 1)
                        HPCheck(i);
                }
            }
        }

        void HPCheck(int number)
        {
            if (number == 0)
            {
                HP1.Value = GameSettings.myTeam[number].Health;
            }
            else if (number == 1)
            {
                HP2.Value = GameSettings.myTeam[number].Health;
            }
            else if (number == 2)
            {
                HP3.Value = GameSettings.myTeam[number].Health;
            }
            else if (number == 3)
            {
                HP4.Value = GameSettings.myTeam[number].Health;
            }
            else if (number == 4)
            {
                HP5.Value = GameSettings.myTeam[number].Health;
            }
        }

        void AttackPrep(int number)
        {
            if (number == 0)
            {
                atkButton1.IsEnabled = true;
            }
            else if (number == 1)
            {
                atkButton2.IsEnabled = true;
            }
            else if (number == 2)
            {
                atkButton3.IsEnabled = true;
            }
            else if (number == 3)
            {
                atkButton4.IsEnabled = true;
            }
            else if (number == 4)
            {
                atkButton5.IsEnabled = true;
            }
        }

        void IsEveryoneAlive()
        {
            bool allyAlive = false;
            bool enemyAlive = false;
            // Ally
            for (int i = 0; i < GameSettings.myTeam.Count; i++)
            {
                if (!GameSettings.myTeam[i].Alive)
                {
                    if (GameSettings.myTeam[i].RoundsDead < 1)
                        FuneralCeremony(i);
                    GameSettings.myTeam[i].RoundsDead++;
                }
                else
                {
                    allyAlive = true;
                }
            }
            // Enemy
            for (int i = 0; i < GameSettings.enemyTeam.Count; i++)
            {
                if (GameSettings.enemyTeam[i].Alive)
                {
                    enemyAlive = true;
                }
            }

            if (!allyAlive)
                ((MainWindow)_mainWindow).Main.Navigate(new DefeatPage(_mainWindow));
            if (!enemyAlive)
                ((MainWindow)_mainWindow).Main.Navigate(new WinPage(_mainWindow));
        }

        void FuneralCeremony(int number)
        {
            if (number == 0)
            {
                atkButton1.IsEnabled = false;
                defButton1.IsEnabled = false;
                Hero1.Content += ": Dead";
            }
            else if (number == 1)
            {
                atkButton2.IsEnabled = false;
                defButton2.IsEnabled = false;
                Hero2.Content += ": Dead";
            }
            else if (number == 2)
            {
                atkButton3.IsEnabled = false;
                defButton3.IsEnabled = false;
                Hero3.Content += ": Dead";
            }
            else if (number == 3)
            {
                atkButton4.IsEnabled = false;
                defButton4.IsEnabled = false;
                Hero4.Content += ": Dead";
            }
            else if (number == 4)
            {
                atkButton5.IsEnabled = false;
                defButton5.IsEnabled = false;
                Hero5.Content += ": Dead";
            }
        }

        private void atkButton1_Click(object sender, RoutedEventArgs e)
        {
            DisableAtk();

            GameAtk(GameSettings.myTeam[0]);
            
        }

        private void atkButton2_Click(object sender, RoutedEventArgs e)
        {
            DisableAtk();

            GameAtk(GameSettings.myTeam[1]);
        }

        private void atkButton3_Click(object sender, RoutedEventArgs e)
        {
            DisableAtk();

            GameAtk(GameSettings.myTeam[2]);
        }

        private void atkButton4_Click(object sender, RoutedEventArgs e)
        {
            DisableAtk();

            GameAtk(GameSettings.myTeam[3]);
        }

        private void atkButton5_Click(object sender, RoutedEventArgs e)
        {
            DisableAtk();

            GameAtk(GameSettings.myTeam[4]);
        }

        void DisableAtk()
        {
            atkButton1.IsEnabled = false;
            atkButton2.IsEnabled = false;
            atkButton3.IsEnabled = false;
            atkButton4.IsEnabled = false;
            atkButton5.IsEnabled = false;
        }

        private void defButton1_Click(object sender, RoutedEventArgs e)
        {
            DisableDef();

            GameDef(GameSettings.myTeam[0]);
        }

        private void defButton2_Click(object sender, RoutedEventArgs e)
        {
            DisableDef();

            GameDef(GameSettings.myTeam[1]);
        }

        private void defButton3_Click(object sender, RoutedEventArgs e)
        {
            DisableDef();

            GameDef(GameSettings.myTeam[2]);
        }

        private void defButton4_Click(object sender, RoutedEventArgs e)
        {
            DisableDef();

            GameDef(GameSettings.myTeam[3]);
        }

        private void defButton5_Click(object sender, RoutedEventArgs e)
        {
            DisableDef();

            GameDef(GameSettings.myTeam[4]);
        }

        void DisableDef()
        {
            defButton1.IsEnabled = false;
            defButton2.IsEnabled = false;
            defButton3.IsEnabled = false;
            defButton4.IsEnabled = false;
            defButton5.IsEnabled = false;
        }
    }
}
