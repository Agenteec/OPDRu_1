using OPDRu.services;
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

namespace OPDRu
{
    /// <summary>
    /// Логика взаимодействия для StatisticsPage.xaml
    /// </summary>
    public partial class StatisticsPage : Page
    {
        private readonly IDatabaseService _databaseService;
        private readonly int _userId;

        public StatisticsPage(int userId, IDatabaseService databaseService)
        {
            InitializeComponent();
            _userId = userId;
            _databaseService = databaseService;

            LoadStatistics();
        }

        private async void LoadStatistics()
        {
            var statistics = await _databaseService.GetUserStatisticsAsync(_userId);
            StatisticsDataGrid.ItemsSource = statistics;
        }
    }
}
