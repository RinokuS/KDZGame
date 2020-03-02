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
using System.Data;

namespace KDZGame
{
    /// <summary>
    /// Логика взаимодействия для FilterPage.xaml
    /// </summary>
    public partial class FilterPage : Page
    {
        static Random rnd = new Random();

        readonly Window _mainWindow;
        public FilterPage(Window mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;

            addingStack.IsEnabled = false;
            addingStack.Visibility = Visibility.Hidden;

            applyChangesBtn.IsEnabled = false;
            applyChangesBtn.Visibility = Visibility.Hidden;
        }

        private void generateDataClick(object sender, RoutedEventArgs e)
        {
            BindDataCSV();

            if (!addingStack.IsEnabled)
            {
                addingStack.IsEnabled = true;
                addingStack.Visibility = Visibility.Visible;
                InitializeTeamBuild();
            }
            if (!applyChangesBtn.IsEnabled)
            {
                applyChangesBtn.IsEnabled = true;
                applyChangesBtn.Visibility = Visibility.Visible;
            }
        }

        private void updateDataClick(object sender, RoutedEventArgs e)
        {
            UpdateData();
        }

        private void applyChangesBtn_Click(object sender, RoutedEventArgs e)
        {
            DataTable correntTable = ((DataView)heroData.ItemsSource).ToTable();

            foreach (DataRow row in correntTable.Rows)
            {
                for (int i = 0; i < GameSettings.myData.Rows.Count; i++)
                {
                    if (row["Unit_name"] == GameSettings.myData.Rows[i]["Unit_name"])
                    {
                        GameSettings.myData.Rows[i]["Attack"] = row["Attack"];
                        GameSettings.myData.Rows[i]["Defence"] = row["Defence"];
                        GameSettings.myData.Rows[i]["Minimum Damage"] = row["Minimum Damage"];
                        GameSettings.myData.Rows[i]["Maximum Damage"] = row["Maximum Damage"];
                        GameSettings.myData.Rows[i]["Health"] = row["Health"];
                        GameSettings.myData.Rows[i]["Speed"] = row["Speed"];
                        GameSettings.myData.Rows[i]["Growth"] = row["Growth"];
                        GameSettings.myData.Rows[i]["AI_Value"] = row["AI_Value"];
                        GameSettings.myData.Rows[i]["Gold"] = row["Gold"];
                    }
                }
            }
            UpdateData();
        }
        private void startGame_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < GameSettings.myData.Rows.Count; j++)
                {
                    if ((string)GameSettings.myData.Rows[j]["Unit_name"] == name)
                    {
                        object[] copyrow = GameSettings.myData.Rows[j].ItemArray;
                        dt.Rows.Add(copyrow);
                        GameSettings.myData.Rows.Remove(GameSettings.myData.Rows[j]);
                        GameSettings.names.Remove(name);
                        GameSettings.myTeam.Add(new Hero((string)copyrow[0], (int)copyrow[1], (int)copyrow[2], (int)copyrow[3], (int)copyrow[4],
                            (int)copyrow[5], (int)copyrow[6], (int)copyrow[7], (int)copyrow[8], (int)copyrow[9]));
                        break;
                    }
                }
            }

            if (GameSettings.myTeam.Count == 5)
                ((MainWindow)_mainWindow).Main.Navigate(new GamePage());
        }

        /// <summary>
        /// Event for adding selected hero to your team
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addHero(object sender, RoutedEventArgs e)
        {
            string name = addHeroName.Text;

            DataTable dt = ((DataView)teamData.ItemsSource).ToTable();

            if (dt.Rows.Count >= 0 && dt.Rows.Count < 5)
            {
                for (int i = 0; i < GameSettings.myData.Rows.Count; i++)
                {
                    if ((string)GameSettings.myData.Rows[i]["Unit_name"] == name)
                    {
                        object[] copyrow = GameSettings.myData.Rows[i].ItemArray;
                        dt.Rows.Add(copyrow);
                        GameSettings.myData.Rows.Remove(GameSettings.myData.Rows[i]);
                        GameSettings.names.Remove(name);
                        GameSettings.myTeam.Add(new Hero((string)copyrow[0], (int)copyrow[1], (int)copyrow[2], (int)copyrow[3], (int)copyrow[4], 
                            (int)copyrow[5], (int)copyrow[6], (int)copyrow[7], (int)copyrow[8], (int)copyrow[9]));
                        break;
                    }
                }
            }

            teamData.ItemsSource = dt.DefaultView;
            heroData.ItemsSource = GameSettings.myData.DefaultView;
            UpdateData();
        }
        /// <summary>
        /// Event for removing selected hero from your team
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeHero(object sender, RoutedEventArgs e)
        {
            string name = addHeroName.Text;

            DataTable dt = ((DataView)teamData.ItemsSource).ToTable();
            
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if ((string)dt.Rows[i]["Unit_name"] == name)
                    {
                        object[] copyrow = dt.Rows[i].ItemArray;
                        GameSettings.myData.Rows.Add(copyrow);
                        dt.Rows.Remove(dt.Rows[i]);
                        GameSettings.names.Add(name);
                        // Deleting Hero from our Team List
                        for (int j = 0; j < GameSettings.myTeam.Count; j++)
                        {
                            if (GameSettings.myTeam[j].Name == name)
                            {
                                GameSettings.myTeam.Remove(GameSettings.myTeam[j]);
                                break;
                            }
                        }
                        break;
                    }
                }
            }

            teamData.ItemsSource = dt.DefaultView;
            heroData.ItemsSource = GameSettings.myData.DefaultView;
            UpdateData();
        }
        /// <summary>
        /// Method for initialize data grid
        /// </summary>
        private void BindDataCSV()
        {
            DataTable dt = new DataTable();
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\User\Documents\Projects\KDZGame\HM3.csv");
            List<string> names = new List<string>();

            if (lines.Length > 0)
            {
                // first line to create header

                string firstLine = lines[0];
                string[] headerLabels = firstLine.Split(';');

                foreach (string header in headerLabels)
                {
                    dt.Columns.Add(new DataColumn(header));
                }

                // for data

                for (int i = 1; i < lines.Length; i++)
                {
                    string[] dataWords = lines[i].Split(';');
                    DataRow dr = dt.NewRow();
                    int columnIndex = 0;

                    foreach (string header in headerLabels)
                    {
                        dr[header] = dataWords[columnIndex++];
                    }

                    names.Add((string)dr["Unit_name"]);
                    dt.Rows.Add(dr);
                }
            }

            if (dt.Rows.Count > 0)
            {
                heroData.ItemsSource = dt.DefaultView;
                GameSettings.myData = dt;
                GameSettings.names = names;
                heroData.Columns[0].IsReadOnly = true;
            }
        }
        /// <summary>
        /// Method for filtering data in data grid
        /// </summary>
        /// <param name="atk">attack</param>
        /// <param name="spd">speed</param>
        /// <param name="gld">gold</param>
        private void BindDataCSVFilt(int atk, int spd, int gld)
        {
            DataTable dt = new DataTable();
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\User\Documents\Projects\KDZGame\HM3.csv");

            if (lines.Length > 0)
            {
                // first line to create header

                string firstLine = lines[0];
                string[] headerLabels = firstLine.Split(';');

                foreach (string header in headerLabels)
                {
                    dt.Columns.Add(new DataColumn(header));
                }

                // for data

                for (int i = 0; i < GameSettings.myData.Rows.Count; i++)
                {
                    object[] copyrow = GameSettings.myData.Rows[i].ItemArray;
                    dt.Rows.Add(copyrow);
                }
            }       

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (int.Parse((string)dt.Rows[i]["Attack"]) < atk)
                {
                    dt.Rows.Remove(dt.Rows[i]);
                    i--;
                    continue;
                }
                if (int.Parse((string)dt.Rows[i]["Speed"]) < spd)
                {
                    dt.Rows.Remove(dt.Rows[i]);
                    i--;
                    continue;
                }
                if (int.Parse((string)dt.Rows[i]["Gold"]) < gld)
                {
                    dt.Rows.Remove(dt.Rows[i]);
                    i--;
                    continue;
                }
            }

            if (dt.Rows.Count > 0)
            {
                heroData.ItemsSource = dt.DefaultView;
                heroData.Columns[0].IsReadOnly = true;
            }
        }
        /// <summary>
        /// Method for initialize team building components
        /// </summary>
        void InitializeTeamBuild()
        {
            DataTable dt = new DataTable();
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\User\Documents\Projects\KDZGame\HM3.csv");

            if (lines.Length > 0)
            {
                // first line to create header

                string firstLine = lines[0];
                string[] headerLabels = firstLine.Split(';');

                foreach (string header in headerLabels)
                {
                    dt.Columns.Add(new DataColumn(header));
                }
            }

            teamData.ItemsSource = dt.DefaultView;
        }
        /// <summary>
        /// Method for Updating our DataGrid
        /// </summary>
        void UpdateData()
        {
            int attack;
            int speed;
            int gold;

            if (!int.TryParse(attackFilter.Text, out attack))
            {
                attack = 0;
                attackFilter.Text = "0";
            }
            if (!int.TryParse(speedFilter.Text, out speed))
            {
                speed = 0;
                speedFilter.Text = "0";
            }
            if (!int.TryParse(goldFilter.Text, out gold))
            {
                gold = 0;
                goldFilter.Text = "0";
            }


            if (attack == 0 && speed == 0 && gold == 0)
            {
                if (GameSettings.myData == null)
                    BindDataCSV();
                else
                {
                    heroData.ItemsSource = GameSettings.myData.DefaultView;
                    heroData.Columns[0].IsReadOnly = true;
                }
            }
            else
            {
                // Костыль, чтобы отфильтрованные данные не заменяли полные в Settings
                if (GameSettings.myData == null)
                {
                    BindDataCSV();
                    BindDataCSVFilt(attack, speed, gold);
                }
                else
                    BindDataCSVFilt(attack, speed, gold);
            }

            if (!addingStack.IsEnabled)
            {
                addingStack.IsEnabled = true;
                addingStack.Visibility = Visibility.Visible;
                InitializeTeamBuild();
            }
            if (!applyChangesBtn.IsEnabled)
            {
                applyChangesBtn.IsEnabled = true;
                applyChangesBtn.Visibility = Visibility.Visible;
            }
        }
    }
}
