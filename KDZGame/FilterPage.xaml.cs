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
            BindDataCSV();
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
                heroData.ItemsSource = dt.DefaultView;
        }
    }
}
