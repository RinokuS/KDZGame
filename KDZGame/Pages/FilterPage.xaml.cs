using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Data;

namespace KDZGame
{
    /// <summary>
    /// Логика взаимодействия для FilterPage.xaml
    /// </summary>
    public partial class FilterPage : Page
    {
        static Random rnd = new Random();
        string[] lines = System.IO.File.ReadAllLines(@"..\..\HM3.csv");

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

            addingStack.IsEnabled = true;
            addingStack.Visibility = Visibility.Visible;
            InitializeTeamBuild();
            GameSettings.myTeam = new List<Hero>();

            applyChangesBtn.IsEnabled = true;
            applyChangesBtn.Visibility = Visibility.Visible;
        }

        private void updateDataClick(object sender, RoutedEventArgs e)
        {
            applyChangesBtn_Click(sender, e);
            UpdateData();
        }

        private void applyChangesBtn_Click(object sender, RoutedEventArgs e)
        {
            DataTable correntTable = ((DataView)heroData.ItemsSource).ToTable();
            DataTable correntTeamTable = ((DataView)teamData.ItemsSource).ToTable();
            DataRow cr;
            // Проверяем совпадения имен в основном DataGrid
            for (int i = 0; i < correntTable.Rows.Count; i++)
            {
                for (int j = 0; j < correntTable.Rows.Count; j++)
                {
                    if (i == j)
                        continue;
                    if (correntTable.Rows[i]["Unit_name"].ToString() == correntTable.Rows[j]["Unit_name"].ToString())
                    {
                        MessageBox.Show("You can`t have any heroes with coinciding names.", "Error!", MessageBoxButton.OK);
                        correntTable.Rows[j]["Unit_name"] = GameSettings.oneMoreData.Rows[j]["Unit_name"]; // если есть совпадение - меняем имя
                    }
                }
            }
            // Проверяем совпадения имен в основном DataGrid и имен в DataGrid для выбранных игроков
            for (int i = 0; i < correntTeamTable.Rows.Count; i++)
            {
                for (int j = 0; j < correntTable.Rows.Count; j++)
                {
                    if (correntTeamTable.Rows[i]["Unit_name"].ToString() == correntTable.Rows[j]["Unit_name"].ToString())
                    {
                        MessageBox.Show("You can`t have any heroes with coinciding names.", "Error!", MessageBoxButton.OK);
                        correntTable.Rows[j]["Unit_name"] = GameSettings.oneMoreData.Rows[j]["Unit_name"]; // если есть совпадение - меняем имя
                    }
                }
            }

            for (int i = 0; i < GameSettings.myData.Rows.Count; i++)
            {
                cr = correntTable.Rows[i]; // строку в отдельную переменную, просто чтобы писать 2 символа вместо 20

                // проверяем каждый параметр так, как нам нужно. Не забываем проверить длину строки у int значений, чтобы не вылетел Overflow
                if ((cr["Attack"].ToString().Length > int.MaxValue.ToString().Length) || int.Parse(cr["Attack"].ToString()) < 1)                
                    correntTable.Rows[i]["Attack"] = GameSettings.oneMoreData.Rows[i]["Attack"];             
                if ((cr["Defence"].ToString().Length > int.MaxValue.ToString().Length) || int.Parse(cr["Defence"].ToString()) < 1)
                    correntTable.Rows[i]["Defence"] = GameSettings.oneMoreData.Rows[i]["Defence"];
                if ((cr["Minimum Damage"].ToString().Length > int.MaxValue.ToString().Length) || int.Parse(cr["Minimum Damage"].ToString()) < 1)
                    correntTable.Rows[i]["Minimum Damage"] = GameSettings.oneMoreData.Rows[i]["Minimum Damage"];
                if ((cr["Maximum Damage"].ToString().Length > int.MaxValue.ToString().Length) || int.Parse(cr["Maximum Damage"].ToString()) < 1)
                    correntTable.Rows[i]["Maximum Damage"] = GameSettings.oneMoreData.Rows[i]["Maximum Damage"];
                if ((cr["Health"].ToString().Length > int.MaxValue.ToString().Length) || int.Parse(cr["Health"].ToString()) < 1)
                    correntTable.Rows[i]["Health"] = GameSettings.oneMoreData.Rows[i]["Health"];
                if ((cr["Speed"].ToString().Length > int.MaxValue.ToString().Length) || int.Parse(cr["Speed"].ToString()) < 1)
                    correntTable.Rows[i]["Speed"] = GameSettings.oneMoreData.Rows[i]["Speed"];
                if ((cr["Growth"].ToString().Length > int.MaxValue.ToString().Length) || int.Parse(cr["Growth"].ToString()) < 1)
                    correntTable.Rows[i]["Growth"] = GameSettings.oneMoreData.Rows[i]["Growth"];
                if ((cr["AI_Value"].ToString().Length > int.MaxValue.ToString().Length) || int.Parse(cr["AI_Value"].ToString()) < 1)
                    correntTable.Rows[i]["AI_Value"] = GameSettings.oneMoreData.Rows[i]["AI_Value"];
                if ((cr["Gold"].ToString().Length > int.MaxValue.ToString().Length) || int.Parse(cr["Gold"].ToString()) < 0)
                    correntTable.Rows[i]["Gold"] = GameSettings.oneMoreData.Rows[i]["Gold"];
                if ((string)cr["Unit_name"] == "")
                    correntTable.Rows[i]["Unit_name"] = GameSettings.oneMoreData.Rows[i]["Unit_name"]; ;
                // сохраняем данные в статик таблицу (на самом деле юзлесс так как correntTable с статик таблицей уже как-то связались по ссылке (ненавижу шарпу))
                GameSettings.myData.Rows[i]["Attack"] = correntTable.Rows[i]["Attack"];
                GameSettings.myData.Rows[i]["Defence"] = correntTable.Rows[i]["Defence"];
                GameSettings.myData.Rows[i]["Minimum Damage"] = correntTable.Rows[i]["Minimum Damage"];
                GameSettings.myData.Rows[i]["Maximum Damage"] = correntTable.Rows[i]["Maximum Damage"];
                GameSettings.myData.Rows[i]["Health"] = correntTable.Rows[i]["Health"];
                GameSettings.myData.Rows[i]["Speed"] = correntTable.Rows[i]["Speed"];
                GameSettings.myData.Rows[i]["Growth"] = correntTable.Rows[i]["Growth"];
                GameSettings.myData.Rows[i]["AI_Value"] = correntTable.Rows[i]["AI_Value"];
                GameSettings.myData.Rows[i]["Gold"] = correntTable.Rows[i]["Gold"];
                GameSettings.myData.Rows[i]["Unit_name"] = correntTable.Rows[i]["Unit_name"];
            }

            UpdateData();
            ReferenceDelete();
        }
        private void startGame_Click(object sender, RoutedEventArgs e)
        {
            applyChangesBtn_Click(sender, e);
            for (int i = 0; i < 5; i++)
            {
                string name = GameSettings.names[rnd.Next(GameSettings.names.Count)];
                for (int j = 0; j < GameSettings.myData.Rows.Count; j++)
                {
                    if ((string)GameSettings.myData.Rows[j]["Unit_name"] == name)
                    {
                        object[] copyrow = GameSettings.myData.Rows[j].ItemArray;

                        GameSettings.enemyTeam.Add(new Hero((string)copyrow[0], int.Parse((string)copyrow[1]), int.Parse((string)copyrow[2]), int.Parse((string)copyrow[3]), int.Parse((string)copyrow[4]),
                            int.Parse((string)copyrow[5]), int.Parse((string)copyrow[6]), int.Parse((string)copyrow[7]), int.Parse((string)copyrow[8]), int.Parse((string)copyrow[9])));
                        break;
                    }
                }
            }

            if (GameSettings.myTeam.Count == 5)
                ((MainWindow)_mainWindow).Main.Navigate(new GamePage(_mainWindow, "Attack"));
            else
            {
                MessageBox.Show("You must have 5 heroes to start game!", "Error!", MessageBoxButton.OK);
            }
        }

        /// <summary>
        /// Event for adding selected hero to your team
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addHero(object sender, RoutedEventArgs e)
        {
            try
            {
                applyChangesBtn_Click(sender, e);
                DataRowView row = (heroData.SelectedItem as DataRowView) ?? (teamData.SelectedItem as DataRowView);

                string selectedHero = row[0].ToString();

                DataTable dt = ((DataView)teamData.ItemsSource).ToTable();
                CheckNullRows(ref dt);

                if (dt.Rows.Count >= 0 && dt.Rows.Count < 5)
                {
                    for (int i = 0; i < GameSettings.myData.Rows.Count; i++)
                    {
                        try
                        {
                            if ((string)GameSettings.myData.Rows[i]["Unit_name"] == selectedHero)
                            {
                                object[] copyrow = GameSettings.myData.Rows[i].ItemArray;
                                dt.Rows.Add(copyrow);
                                GameSettings.myData.Rows.Remove(GameSettings.myData.Rows[i]);
                                GameSettings.names.Remove(selectedHero);

                                GameSettings.myTeam.Add(new Hero((string)copyrow[0], int.Parse((string)copyrow[1]), int.Parse((string)copyrow[2]), int.Parse((string)copyrow[3]), int.Parse((string)copyrow[4]),
                                    int.Parse((string)copyrow[5]), int.Parse((string)copyrow[6]), int.Parse((string)copyrow[7]), int.Parse((string)copyrow[8]), int.Parse((string)copyrow[9])));
                                break;
                            }
                        }
                        catch (InvalidCastException)
                        {
                            GameSettings.myData.Rows.Remove(GameSettings.myData.Rows[i]);
                            i--;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("You must have no more than 5 heroes", "Error!", MessageBoxButton.OK);
                }

                teamData.ItemsSource = dt.DefaultView;
                heroData.ItemsSource = GameSettings.myData.DefaultView;
                for (int i = 0; i < teamData.Columns.Count; i++)
                {
                    teamData.Columns[i].IsReadOnly = true;
                }
                UpdateData();
                ReferenceDelete();
            }
            catch (NullReferenceException)
            {

            }
        }
        /// <summary>
        /// Event for removing selected hero from your team
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeHero(object sender, RoutedEventArgs e)
        {
            applyChangesBtn_Click(sender, e);
            try
            {
                DataRowView row = (teamData.SelectedItem as DataRowView) ?? (heroData.SelectedItem as DataRowView);              
                string selectedHero = row[0].ToString();

                DataTable dt = ((DataView)teamData.ItemsSource).ToTable();
                CheckNullRows(ref dt);


                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        try
                        {
                            if ((string)dt.Rows[i]["Unit_name"] == selectedHero)
                            {
                                object[] copyrow = dt.Rows[i].ItemArray;
                                GameSettings.myData.Rows.Add(copyrow);
                                dt.Rows.Remove(dt.Rows[i]);
                                GameSettings.names.Add(selectedHero);
                                // Deleting Hero from our Team List
                                for (int j = 0; j < GameSettings.myTeam.Count; j++)
                                {
                                    if (GameSettings.myTeam[j].Name == selectedHero)
                                    {
                                        GameSettings.myTeam.Remove(GameSettings.myTeam[j]);
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                        catch (InvalidCastException)
                        {
                            dt.Rows.Remove(dt.Rows[i]);
                            i--;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("You must have at least one hero to remove!", "Error!", MessageBoxButton.OK);
                }

                teamData.ItemsSource = dt.DefaultView;
                heroData.ItemsSource = GameSettings.myData.DefaultView;
                for (int i = 0; i < teamData.Columns.Count; i++)
                {
                    teamData.Columns[i].IsReadOnly = true;
                }
                UpdateData();
                ReferenceDelete();
            }
            catch (NullReferenceException)
            {

            }
        }
        /// <summary>
        /// Trying to fix bug with Null Rows
        /// </summary>
        /// <param name="dt"></param>
        void CheckNullRows(ref DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i].IsNull(0))
                {
                    dt.Rows.Remove(dt.Rows[i]);
                    i--;
                }
            }
        }
        /// <summary>
        /// Method for initialize data grid
        /// </summary>
        private void BindDataCSV()
        {
            DataTable dt = new DataTable();
            try
            {

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
                }
                ReferenceDelete();
            }
            catch (System.IO.IOException)
            {
                Console.WriteLine("Закрой файл, или я тебя укушу");
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
            try
            {

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
                }
            }
            catch (System.IO.IOException)
            {
                Console.WriteLine("Закрой файл, или я тебя укушу");
            }
        }
        /// <summary>
        /// Method for initialize team building components
        /// </summary>
        void InitializeTeamBuild()
        {
            DataTable dt = new DataTable();

            try
            {                

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
                for (int i = 0; i < teamData.Columns.Count; i++)
                {
                    teamData.Columns[i].IsReadOnly = true;
                }
            }
            catch (System.IO.IOException)
            {
                Console.WriteLine("Закрой файл, или я тебя укушу");
            } 
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

        void ReferenceDelete()
        {
            DataTable dt = new DataTable();

            string firstLine = lines[0];
            string[] headerLabels = firstLine.Split(';');

            foreach (string header in headerLabels)
            {
                dt.Columns.Add(new DataColumn(header));
            }

            for (int i = 0; i < GameSettings.myData.Rows.Count; i++)
            {
                try
                {
                    object[] copyrow = GameSettings.myData.Rows[i].ItemArray;
                    dt.Rows.Add(copyrow);
                }
                catch (InvalidCastException)
                {
                    GameSettings.myData.Rows.Remove(GameSettings.myData.Rows[i]);
                    i--;
                }
            }

            GameSettings.oneMoreData = dt;
        }
    }
}
