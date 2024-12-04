using OPDRu.data;
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

        public HomePage(User user)
        {
            InitializeComponent();
            _currentUser = user;
        }

        private void ChooseTestButton_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService?.Navigate(new TestSelectionPage(_currentUser));
        }

        private void ViewStatsButton_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService?.Navigate(new StatisticsPage(_currentUser));
        }
    }

}
