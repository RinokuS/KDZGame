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
using System.Xml;
using System.IO;

namespace KDZGame
{
    /// <summary>
    /// Логика взаимодействия для GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        readonly Window _mainWindow;
        static Random rnd = new Random();

        static int _round;
        static bool _isGaming;
        static string gameStage;

        public GamePage(Window mainWindow, string stage)
        {
            InitializeComponent();
            _mainWindow = mainWindow;

            _isGaming = true;
            _round = 1;
            gameStage = stage;

            WriteSaveGameXmlStream();
            InitializeInterface(stage);
        }

        public GamePage(Window mainWindow, List<Hero> myTeam, List<Hero> enemyTeam, int round, string stage)
        {
            InitializeComponent();
            _mainWindow = mainWindow;

            GameSettings.myTeam = myTeam;
            GameSettings.enemyTeam = enemyTeam;

            _isGaming = true;
            _round = round;
            gameStage = stage;

            IsEveryoneAlive();
            WriteSaveGameXmlStream();
            InitializeInterface(stage);
        }

        static void WriteSaveGameXmlStream()
        {
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                NewLineOnAttributes = false,
                Encoding = Encoding.UTF8
            };
            using (XmlWriter writer = XmlWriter.Create(@"..\..\Save.xml", settings))
            {
                writer.WriteStartDocument();

                // <Students>
                writer.WriteStartElement("Game");   // <Game>

                writer.WriteElementString("Round", _round.ToString());
                writer.WriteElementString("Stage", gameStage);
                writer.WriteStartElement("AllyTeam");   // <AllyTeam>

                writer.WriteStartElement("Hero1");   // </Hero1>

                writer.WriteElementString("Name", GameSettings.myTeam[0].Name);
                writer.WriteElementString("Attack", GameSettings.myTeam[0].Attack.ToString());
                writer.WriteElementString("Defence", GameSettings.myTeam[0].Defence.ToString());
                writer.WriteElementString("minDmg", GameSettings.myTeam[0].minDmg.ToString());
                writer.WriteElementString("maxDmg", GameSettings.myTeam[0].maxDmg.ToString());
                writer.WriteElementString("maxHealth", GameSettings.myTeam[0].MaxHealth.ToString());
                writer.WriteElementString("Health", GameSettings.myTeam[0].Health.ToString());
                writer.WriteElementString("Speed", GameSettings.myTeam[0].Speed.ToString());
                writer.WriteElementString("Growth", GameSettings.myTeam[0].Growth.ToString());
                writer.WriteElementString("AI_Value", GameSettings.myTeam[0].AI_Value.ToString());
                writer.WriteElementString("Gold", GameSettings.myTeam[0].Gold.ToString());
                writer.WriteElementString("Alive", GameSettings.myTeam[0].Alive.ToString());
                writer.WriteElementString("RoundsDead", GameSettings.myTeam[0].RoundsDead.ToString());
                // </Hero1>
                writer.WriteEndElement();

                writer.WriteStartElement("Hero2");   // </Hero2>

                writer.WriteElementString("Name", GameSettings.myTeam[1].Name);
                writer.WriteElementString("Attack", GameSettings.myTeam[1].Attack.ToString());
                writer.WriteElementString("Defence", GameSettings.myTeam[1].Defence.ToString());
                writer.WriteElementString("minDmg", GameSettings.myTeam[1].minDmg.ToString());
                writer.WriteElementString("maxDmg", GameSettings.myTeam[1].maxDmg.ToString());
                writer.WriteElementString("maxHealth", GameSettings.myTeam[1].MaxHealth.ToString());
                writer.WriteElementString("Health", GameSettings.myTeam[1].Health.ToString());
                writer.WriteElementString("Speed", GameSettings.myTeam[1].Speed.ToString());
                writer.WriteElementString("Growth", GameSettings.myTeam[1].Growth.ToString());
                writer.WriteElementString("AI_Value", GameSettings.myTeam[1].AI_Value.ToString());
                writer.WriteElementString("Gold", GameSettings.myTeam[1].Gold.ToString());
                writer.WriteElementString("Alive", GameSettings.myTeam[1].Alive.ToString());
                writer.WriteElementString("RoundsDead", GameSettings.myTeam[1].RoundsDead.ToString());
                // </Hero2>
                writer.WriteEndElement();

                writer.WriteStartElement("Hero3");   // </Hero3>

                writer.WriteElementString("Name", GameSettings.myTeam[2].Name);
                writer.WriteElementString("Attack", GameSettings.myTeam[2].Attack.ToString());
                writer.WriteElementString("Defence", GameSettings.myTeam[2].Defence.ToString());
                writer.WriteElementString("minDmg", GameSettings.myTeam[2].minDmg.ToString());
                writer.WriteElementString("maxDmg", GameSettings.myTeam[2].maxDmg.ToString());
                writer.WriteElementString("maxHealth", GameSettings.myTeam[2].MaxHealth.ToString());
                writer.WriteElementString("Health", GameSettings.myTeam[2].Health.ToString());
                writer.WriteElementString("Speed", GameSettings.myTeam[2].Speed.ToString());
                writer.WriteElementString("Growth", GameSettings.myTeam[2].Growth.ToString());
                writer.WriteElementString("AI_Value", GameSettings.myTeam[2].AI_Value.ToString());
                writer.WriteElementString("Gold", GameSettings.myTeam[2].Gold.ToString());
                writer.WriteElementString("Alive", GameSettings.myTeam[2].Alive.ToString());
                writer.WriteElementString("RoundsDead", GameSettings.myTeam[2].RoundsDead.ToString());
                // </Hero3>
                writer.WriteEndElement();

                writer.WriteStartElement("Hero4");   // </Hero4>

                writer.WriteElementString("Name", GameSettings.myTeam[3].Name);
                writer.WriteElementString("Attack", GameSettings.myTeam[3].Attack.ToString());
                writer.WriteElementString("Defence", GameSettings.myTeam[3].Defence.ToString());
                writer.WriteElementString("minDmg", GameSettings.myTeam[3].minDmg.ToString());
                writer.WriteElementString("maxDmg", GameSettings.myTeam[3].maxDmg.ToString());
                writer.WriteElementString("maxHealth", GameSettings.myTeam[3].MaxHealth.ToString());
                writer.WriteElementString("Health", GameSettings.myTeam[3].Health.ToString());
                writer.WriteElementString("Speed", GameSettings.myTeam[3].Speed.ToString());
                writer.WriteElementString("Growth", GameSettings.myTeam[3].Growth.ToString());
                writer.WriteElementString("AI_Value", GameSettings.myTeam[3].AI_Value.ToString());
                writer.WriteElementString("Gold", GameSettings.myTeam[3].Gold.ToString());
                writer.WriteElementString("Alive", GameSettings.myTeam[3].Alive.ToString());
                writer.WriteElementString("RoundsDead", GameSettings.myTeam[3].RoundsDead.ToString());
                // </Hero4>
                writer.WriteEndElement();

                writer.WriteStartElement("Hero5");   // </Hero5>

                writer.WriteElementString("Name", GameSettings.myTeam[4].Name);
                writer.WriteElementString("Attack", GameSettings.myTeam[4].Attack.ToString());
                writer.WriteElementString("Defence", GameSettings.myTeam[4].Defence.ToString());
                writer.WriteElementString("minDmg", GameSettings.myTeam[4].minDmg.ToString());
                writer.WriteElementString("maxDmg", GameSettings.myTeam[4].maxDmg.ToString());
                writer.WriteElementString("maxHealth", GameSettings.myTeam[4].MaxHealth.ToString());
                writer.WriteElementString("Health", GameSettings.myTeam[4].Health.ToString());
                writer.WriteElementString("Speed", GameSettings.myTeam[4].Speed.ToString());
                writer.WriteElementString("Growth", GameSettings.myTeam[4].Growth.ToString());
                writer.WriteElementString("AI_Value", GameSettings.myTeam[4].AI_Value.ToString());
                writer.WriteElementString("Gold", GameSettings.myTeam[4].Gold.ToString());
                writer.WriteElementString("Alive", GameSettings.myTeam[4].Alive.ToString());
                writer.WriteElementString("RoundsDead", GameSettings.myTeam[4].RoundsDead.ToString());
                // </Hero5>
                writer.WriteEndElement();
                // </AllyTeam>
                writer.WriteEndElement();   

                writer.WriteStartElement("EnemyTeam");   // <EnemyTeam>

                writer.WriteStartElement("Hero1");   // </Hero1>

                writer.WriteElementString("Name", GameSettings.enemyTeam[0].Name);
                writer.WriteElementString("Attack", GameSettings.enemyTeam[0].Attack.ToString());
                writer.WriteElementString("Defence", GameSettings.enemyTeam[0].Defence.ToString());
                writer.WriteElementString("minDmg", GameSettings.enemyTeam[0].minDmg.ToString());
                writer.WriteElementString("maxDmg", GameSettings.enemyTeam[0].maxDmg.ToString());
                writer.WriteElementString("maxHealth", GameSettings.enemyTeam[0].MaxHealth.ToString());
                writer.WriteElementString("Health", GameSettings.enemyTeam[0].Health.ToString());
                writer.WriteElementString("Speed", GameSettings.enemyTeam[0].Speed.ToString());
                writer.WriteElementString("Growth", GameSettings.enemyTeam[0].Growth.ToString());
                writer.WriteElementString("AI_Value", GameSettings.enemyTeam[0].AI_Value.ToString());
                writer.WriteElementString("Gold", GameSettings.enemyTeam[0].Gold.ToString());
                writer.WriteElementString("Alive", GameSettings.enemyTeam[0].Alive.ToString());
                writer.WriteElementString("RoundsDead", GameSettings.enemyTeam[0].RoundsDead.ToString());
                // </Hero1>
                writer.WriteEndElement();

                writer.WriteStartElement("Hero2");   // </Hero2>

                writer.WriteElementString("Name", GameSettings.enemyTeam[1].Name);
                writer.WriteElementString("Attack", GameSettings.enemyTeam[1].Attack.ToString());
                writer.WriteElementString("Defence", GameSettings.enemyTeam[1].Defence.ToString());
                writer.WriteElementString("minDmg", GameSettings.enemyTeam[1].minDmg.ToString());
                writer.WriteElementString("maxDmg", GameSettings.enemyTeam[1].maxDmg.ToString());
                writer.WriteElementString("maxHealth", GameSettings.enemyTeam[1].MaxHealth.ToString());
                writer.WriteElementString("Health", GameSettings.enemyTeam[1].Health.ToString());
                writer.WriteElementString("Speed", GameSettings.enemyTeam[1].Speed.ToString());
                writer.WriteElementString("Growth", GameSettings.enemyTeam[1].Growth.ToString());
                writer.WriteElementString("AI_Value", GameSettings.enemyTeam[1].AI_Value.ToString());
                writer.WriteElementString("Gold", GameSettings.enemyTeam[1].Gold.ToString());
                writer.WriteElementString("Alive", GameSettings.enemyTeam[1].Alive.ToString());
                writer.WriteElementString("RoundsDead", GameSettings.enemyTeam[1].RoundsDead.ToString());
                // </Hero2>
                writer.WriteEndElement();

                writer.WriteStartElement("Hero3");   // </Hero3>

                writer.WriteElementString("Name", GameSettings.enemyTeam[2].Name);
                writer.WriteElementString("Attack", GameSettings.enemyTeam[2].Attack.ToString());
                writer.WriteElementString("Defence", GameSettings.enemyTeam[2].Defence.ToString());
                writer.WriteElementString("minDmg", GameSettings.enemyTeam[2].minDmg.ToString());
                writer.WriteElementString("maxDmg", GameSettings.enemyTeam[2].maxDmg.ToString());
                writer.WriteElementString("maxHealth", GameSettings.enemyTeam[2].MaxHealth.ToString());
                writer.WriteElementString("Health", GameSettings.enemyTeam[2].Health.ToString());
                writer.WriteElementString("Speed", GameSettings.enemyTeam[2].Speed.ToString());
                writer.WriteElementString("Growth", GameSettings.enemyTeam[2].Growth.ToString());
                writer.WriteElementString("AI_Value", GameSettings.enemyTeam[2].AI_Value.ToString());
                writer.WriteElementString("Gold", GameSettings.enemyTeam[2].Gold.ToString());
                writer.WriteElementString("Alive", GameSettings.enemyTeam[2].Alive.ToString());
                writer.WriteElementString("RoundsDead", GameSettings.enemyTeam[2].RoundsDead.ToString());
                // </Hero3>
                writer.WriteEndElement();

                writer.WriteStartElement("Hero4");   // </Hero4>

                writer.WriteElementString("Name", GameSettings.enemyTeam[3].Name);
                writer.WriteElementString("Attack", GameSettings.enemyTeam[3].Attack.ToString());
                writer.WriteElementString("Defence", GameSettings.enemyTeam[3].Defence.ToString());
                writer.WriteElementString("minDmg", GameSettings.enemyTeam[3].minDmg.ToString());
                writer.WriteElementString("maxDmg", GameSettings.enemyTeam[3].maxDmg.ToString());
                writer.WriteElementString("maxHealth", GameSettings.enemyTeam[3].MaxHealth.ToString());
                writer.WriteElementString("Health", GameSettings.enemyTeam[3].Health.ToString());
                writer.WriteElementString("Speed", GameSettings.enemyTeam[3].Speed.ToString());
                writer.WriteElementString("Growth", GameSettings.enemyTeam[3].Growth.ToString());
                writer.WriteElementString("AI_Value", GameSettings.enemyTeam[3].AI_Value.ToString());
                writer.WriteElementString("Gold", GameSettings.enemyTeam[3].Gold.ToString());
                writer.WriteElementString("Alive", GameSettings.enemyTeam[3].Alive.ToString());
                writer.WriteElementString("RoundsDead", GameSettings.enemyTeam[3].RoundsDead.ToString());
                // </Hero4>
                writer.WriteEndElement();

                writer.WriteStartElement("Hero5");   // </Hero5>

                writer.WriteElementString("Name", GameSettings.enemyTeam[4].Name);
                writer.WriteElementString("Attack", GameSettings.enemyTeam[4].Attack.ToString());
                writer.WriteElementString("Defence", GameSettings.enemyTeam[4].Defence.ToString());
                writer.WriteElementString("minDmg", GameSettings.enemyTeam[4].minDmg.ToString());
                writer.WriteElementString("maxDmg", GameSettings.enemyTeam[4].maxDmg.ToString());
                writer.WriteElementString("maxHealth", GameSettings.enemyTeam[4].MaxHealth.ToString());
                writer.WriteElementString("Health", GameSettings.enemyTeam[4].Health.ToString());
                writer.WriteElementString("Speed", GameSettings.enemyTeam[4].Speed.ToString());
                writer.WriteElementString("Growth", GameSettings.enemyTeam[4].Growth.ToString());
                writer.WriteElementString("AI_Value", GameSettings.enemyTeam[4].AI_Value.ToString());
                writer.WriteElementString("Gold", GameSettings.enemyTeam[4].Gold.ToString());
                writer.WriteElementString("Alive", GameSettings.enemyTeam[4].Alive.ToString());
                writer.WriteElementString("RoundsDead", GameSettings.enemyTeam[4].RoundsDead.ToString());
                // </Hero5>
                writer.WriteEndElement();
                // </EnemyTeam>
                writer.WriteEndElement();   

                // </Game>
                writer.WriteEndElement();
            }
        }

        void InitializeInterface(string stage)
        {
            if (stage == "Attack")
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
            else
            {
                InitializeTeam(Hero1, HP1, atkLabel1, dmgLabel1, defLabel1, grthLabel1, 1);
                atkButton1.IsEnabled = false;
                InitializeTeam(Hero2, HP2, atkLabel2, dmgLabel2, defLabel2, grthLabel2, 2);
                atkButton2.IsEnabled = false;
                InitializeTeam(Hero3, HP3, atkLabel3, dmgLabel3, defLabel3, grthLabel3, 3);
                atkButton3.IsEnabled = false;
                InitializeTeam(Hero4, HP4, atkLabel4, dmgLabel4, defLabel4, grthLabel4, 4);
                atkButton4.IsEnabled = false;
                InitializeTeam(Hero5, HP5, atkLabel5, dmgLabel5, defLabel5, grthLabel5, 5);
                atkButton5.IsEnabled = false;
            }
        }

        void InitializeTeam(Label label, ProgressBar bar, Label atkLab, Label dmgLab, Label defLab, Label grthLab, int number)
        {
            label.Content = GameSettings.myTeam[number - 1].Name;
            bar.Maximum = GameSettings.myTeam[number - 1].MaxHealth;
            bar.Value = GameSettings.myTeam[number - 1].Health;
            atkLab.Content = "Atk: " + GameSettings.myTeam[number - 1].Attack;
            dmgLab.Content = "Dmg: " + GameSettings.myTeam[number - 1].minDmg + "-" + GameSettings.myTeam[number - 1].maxDmg;
            defLab.Content = "Def: " + GameSettings.myTeam[number - 1].Defence;
            grthLab.Content = "Gth: " + GameSettings.myTeam[number - 1].Growth;
        }
        /// <summary>
        /// Player`s attack move
        /// </summary>
        /// <param name="myHero">Picked hero</param>
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

            gameStage = "Defence";
            WriteSaveGameXmlStream();
        }
        /// <summary>
        /// Preparing to defence
        /// </summary>
        /// <param name="number">index of Alive hero</param>
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
        /// <summary>
        /// Player`s def move
        /// </summary>
        /// <param name="myHero"></param>
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

            gameStage = "Attack";
            WriteSaveGameXmlStream();
        }
        /// <summary>
        /// Changing of HP Bar
        /// </summary>
        /// <param name="number">index of hero</param>
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
        /// <summary>
        /// Preparing to attack
        /// </summary>
        /// <param name="number">index of hero</param>
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
        /// <summary>
        /// Method for checking alive heroes and enemies
        /// </summary>
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
            // Changing our Page to win/defeat Pages
            if (!allyAlive)
            {
                File.Delete(@"..\..\Save.xml");
                ((MainWindow)_mainWindow).Main.Navigate(new DefeatPage(_mainWindow));
            }
            if (!enemyAlive)
            {
                File.Delete(@"..\..\Save.xml");
                ((MainWindow)_mainWindow).Main.Navigate(new WinPage(_mainWindow));
            }               
        }
        /// <summary>
        /// Method for buring our dead hero
        /// </summary>
        /// <param name="number"></param>
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
