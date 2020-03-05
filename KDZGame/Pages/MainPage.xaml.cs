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
using System.IO;
using System.Xml;

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

        private void resumeBtn_Click(object sender, RoutedEventArgs e)
        {
            List<Hero> myTeam;
            List<Hero> enemyTeam;
            int round;
            string stage;

            if (File.Exists(@"..\..\Save.xml"))
            {
                try
                {
                    ReadSaveGameXmlStream(out myTeam, out enemyTeam, out round, out stage);
                    IsEveryoneAlive(myTeam, enemyTeam, round, stage);
                }
                catch (Exception) // Не декомпозирую кэтч, ибо независимо от ошибки буду сообщать, что файл поврежден.
                {
                    errorBox.Text = "Файл повреждён. Загрузить игру не удалось.";
                }
            }
            else
            {
                errorBox.Text = "Не удалось загрузить сохранение. \r\n" +
                    "Оно может быть удалено.";
            }
        }

        static void ReadSaveGameXmlStream(out List<Hero> myTeam, out List<Hero> enemyTeam, out int round, out string stage)
        {
            XmlReaderSettings settings = new XmlReaderSettings
            {
                ConformanceLevel = ConformanceLevel.Fragment,
                IgnoreWhitespace = true,
                ValidationType = ValidationType.None,
                IgnoreComments = true
            };
            string name = null;
            int attack = -1;
            int defence = -1;
            int minDmg = -1;
            int maxDmg = -1;
            int maxHealth = -1;
            int health = -1;
            int speed = -1;
            int growth = -1;
            int ai_value = -1;
            int gold = -1;
            bool alive = false;
            int roundsDead = -1;

            myTeam = new List<Hero>();
            enemyTeam = new List<Hero>();
            round = 1;
            stage = "Attack";

            int i = 0;
            int j = 0;
            using (XmlReader reader = XmlReader.Create(@"..\..\Save.xml", settings))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element
                        && reader.Name == "Game"
                        && reader.IsStartElement())
                    {
                        reader.Read();
                        while (reader.Name != "AllyTeam")
                        {
                            if (reader.Name == "Round")
                                round = reader.ReadElementContentAsInt();
                            else if (reader.Name == "Stage")
                                stage = reader.ReadElementContentAsString();
                        }

                        if (reader.NodeType == XmlNodeType.Element // going through AllyTeam
                        && reader.Name == "AllyTeam"
                        && reader.IsStartElement())
                        {
                            reader.Read();
                            while (reader.Name != "AllyTeam")
                            {
                                if (reader.NodeType == XmlNodeType.Element // going through AllyTeam
                                && reader.Name == "Hero" + (i + 1)
                                && reader.IsStartElement())
                                {
                                    reader.Read();
                                    while (reader.Name != "Hero" + (i + 1) && reader.Name != "EnemyTeam")
                                    {
                                        if (reader.Name == "Name")
                                            name = reader.ReadElementContentAsString();
                                        else if (reader.Name == "Attack")
                                            attack = reader.ReadElementContentAsInt();
                                        else if (reader.Name == "Defence")
                                            defence = reader.ReadElementContentAsInt();
                                        else if (reader.Name == "minDmg")
                                            minDmg = reader.ReadElementContentAsInt();
                                        else if (reader.Name == "maxDmg")
                                            maxDmg = reader.ReadElementContentAsInt();
                                        else if (reader.Name == "maxHealth")
                                            maxHealth = reader.ReadElementContentAsInt();
                                        else if (reader.Name == "Health")
                                            health = reader.ReadElementContentAsInt();
                                        else if (reader.Name == "Speed")
                                            speed = reader.ReadElementContentAsInt();
                                        else if (reader.Name == "Growth")
                                            growth = reader.ReadElementContentAsInt();
                                        else if (reader.Name == "AI_Value")
                                            ai_value = reader.ReadElementContentAsInt();
                                        else if (reader.Name == "Gold")
                                            gold = reader.ReadElementContentAsInt();
                                        else if (reader.Name == "Alive")
                                            alive = reader.ReadElementContentAsString() == "True"?true:false;
                                        else if (reader.Name == "RoundsDead")
                                            roundsDead = reader.ReadElementContentAsInt();
                                        if (++j > 50)
                                            break;
                                    }
                                    if ((name == null) || (attack<1) || (defence<1) || (minDmg<1) || (maxDmg<1) || (maxHealth<1)
                                        || (maxHealth < health) || (speed<1) || (growth<1) || (ai_value<1)
                                        || (gold<1) || (roundsDead < 0))
                                    {
                                        throw new ArgumentException("Файл повреждён!");
                                    }
                                    else
                                    {
                                        myTeam.Add(new Hero(name, attack, defence, minDmg, maxDmg, maxHealth, health,
                                            speed, growth, ai_value, gold, alive, roundsDead));
                                    }
                                    i++;
                                    reader.Read();
                                }
                                else
                                {
                                    reader.Read();
                                }
                            }
                        }
                        i = 0;
                        j = 0;
                        if (reader.NodeType == XmlNodeType.Element // going through EnemyTeam
                        && reader.Name == "EnemyTeam"
                        && reader.IsStartElement())
                        {
                            reader.Read();
                            while (reader.Name != "EnemyTeam")
                            {
                                if (reader.NodeType == XmlNodeType.Element // going through EnemyTeam
                                && reader.Name == "Hero" + (i + 1)
                                && reader.IsStartElement())
                                {
                                    reader.Read();
                                    while (reader.Name != "Hero" + (i + 1) && reader.Name != "EnemyTeam")
                                    {
                                        if (reader.Name == "Name")
                                            name = reader.ReadElementContentAsString();
                                        else if (reader.Name == "Attack")
                                            attack = reader.ReadElementContentAsInt();
                                        else if (reader.Name == "Defence")
                                            defence = reader.ReadElementContentAsInt();
                                        else if (reader.Name == "minDmg")
                                            minDmg = reader.ReadElementContentAsInt();
                                        else if (reader.Name == "maxDmg")
                                            maxDmg = reader.ReadElementContentAsInt();
                                        else if (reader.Name == "maxHealth")
                                            maxHealth = reader.ReadElementContentAsInt();
                                        else if (reader.Name == "Health")
                                            health = reader.ReadElementContentAsInt();
                                        else if (reader.Name == "Speed")
                                            speed = reader.ReadElementContentAsInt();
                                        else if (reader.Name == "Growth")
                                            growth = reader.ReadElementContentAsInt();
                                        else if (reader.Name == "AI_Value")
                                            ai_value = reader.ReadElementContentAsInt();
                                        else if (reader.Name == "Gold")
                                            gold = reader.ReadElementContentAsInt();
                                        else if (reader.Name == "Alive")
                                            alive = reader.ReadElementContentAsString() == "True" ? true : false;
                                        else if (reader.Name == "RoundsDead")
                                            roundsDead = reader.ReadElementContentAsInt();
                                        if (++j > 50)
                                            break;
                                    }
                                    if ((name == null) || (attack < 1) || (defence < 1) || (minDmg < 1) || (maxDmg < 1) || (maxHealth < 1)
                                        || (maxHealth < health) || (speed < 1) || (growth < 1) || (ai_value < 1)
                                        || (gold < 1) || (roundsDead < 0))
                                    {
                                        throw new ArgumentException("Файл повреждён!");
                                    }
                                    else
                                    {
                                        enemyTeam.Add(new Hero(name, attack, defence, minDmg, maxDmg, maxHealth, health,
                                            speed, growth, ai_value, gold, alive, roundsDead));
                                    }
                                    i++;
                                    reader.Read();
                                }
                                else
                                {
                                    reader.Read();
                                }
                            }



                        }
                    }

                }
            }
        }

        void IsEveryoneAlive(List<Hero> myTeam, List<Hero> enemyTeam, int round, string stage)
        {
            bool allyAlive = false;
            bool enemyAlive = false;
            // Ally
            for (int i = 0; i < myTeam.Count; i++)
            {
                if (myTeam[i].Alive)
                {
                    allyAlive = true;
                    break;
                }
            }
            // Enemy
            for (int i = 0; i < enemyTeam.Count; i++)
            {
                if (enemyTeam[i].Alive)
                {
                    enemyAlive = true;
                    break;
                }
            }
            // Changing our Page to win/defeat Pages
            if (!allyAlive)
            {
                File.Delete(@"..\..\Save.xml");
                ((MainWindow)_mainWindow).Main.Navigate(new DefeatPage(_mainWindow));
            }
            else if (!enemyAlive)
            {
                File.Delete(@"..\..\Save.xml");
                ((MainWindow)_mainWindow).Main.Navigate(new WinPage(_mainWindow));
            }
            else
            {
                ((MainWindow)_mainWindow).Main.Navigate(new GamePage(_mainWindow, myTeam, enemyTeam, round, stage));
            }
        }
    }
}
