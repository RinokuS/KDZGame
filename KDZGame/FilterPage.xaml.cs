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
        public FilterPage()
        {
            InitializeComponent();
        }

        private void generateDataClick(object sender, RoutedEventArgs e)
        {
            int attack;
            int speed;
            int gold;

            if (!int.TryParse(attackFilter.Text,out attack)){
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
                    heroData.ItemsSource = GameSettings.myData.DefaultView;
            }
            else
            {
                BindDataCSV(attack, speed, gold);
            }
        }

        private void BindDataCSV()
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

                for (int i = 1; i < lines.Length; i++)
                {
                    string[] dataWords = lines[i].Split(';');
                    DataRow dr = dt.NewRow();
                    int columnIndex = 0;

                    foreach (string header in headerLabels)
                    {
                        dr[header] = dataWords[columnIndex++];
                    }

                    dt.Rows.Add(dr);
                }
            }

            if (dt.Rows.Count > 0)
            {
                heroData.ItemsSource = dt.DefaultView;
                GameSettings.myData = dt;
                GameSettings.enemyData = dt;
            }
        }

        private void BindDataCSV(int atk, int spd, int gld)
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

                for (int i = 1; i < lines.Length; i++)
                {
                    string[] dataWords = lines[i].Split(';');
                    DataRow dr = dt.NewRow();
                    int columnIndex = 0;

                    foreach (string header in headerLabels)
                    {
                        dr[header] = dataWords[columnIndex++];
                    }

                    if (int.Parse((string)dr["Attack"]) < atk)
                        continue;
                    if (int.Parse((string)dr["Speed"]) < spd)
                        continue;
                    if (int.Parse((string)dr["Gold"]) < gld)
                        continue;

                    dt.Rows.Add(dr);
                }
            }

            if (dt.Rows.Count > 0)
            {
                heroData.ItemsSource = dt.DefaultView;
                GameSettings.myData = dt;
                GameSettings.enemyData = dt;
            }
        }
    }
}
