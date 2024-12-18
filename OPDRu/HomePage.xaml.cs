using OPDRu.data;
using OPDRu.services;
using System.Windows;
using System.Windows.Controls;

namespace OPDRu
{
    /// <summary>
    /// Логика взаимодействия для HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        private readonly User _currentUser;
        private readonly IDatabaseService _databaseService;

        public HomePage(User user, IDatabaseService databaseService)
        {
            InitializeComponent();
            _currentUser = user;
            _databaseService = databaseService;
        }

        private void ChooseTestButton_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService?.Navigate(new TestSelectionPage(_currentUser));
        }

        private void ViewStatsButton_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService?.Navigate(new StatisticsPage(_currentUser));
        }
        private void EditTestButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new TestManagerPage(_databaseService, this));
        }
    }

}
