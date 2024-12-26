using OPDRu.data;
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
    /// Логика взаимодействия для TestSelectionPage.xaml
    /// </summary>
    public partial class TestSelectionPage : Page
    {
        private readonly User _currentUser;
        private readonly IDatabaseService _databaseService;
        private readonly Page _parentPage;

        public TestSelectionPage(User currentUser, IDatabaseService databaseService, Page parentPage = null)
        {
            InitializeComponent();
            _currentUser = currentUser;
            _databaseService = databaseService;
            _parentPage = parentPage;

            LoadTests();
        }

        private async void LoadTests()
        {
            var tests = await _databaseService.GetAllTestsAsync();
            TestListBox.ItemsSource = tests;
        }

        private void StartTestButton_Click(object sender, RoutedEventArgs e)
        {
            if (TestListBox.SelectedItem is Test selectedTest)
            {
               
                NavigationService?.Navigate(new TestPassingPage(_currentUser, selectedTest, _databaseService, this));
            }
            else
            {
                MessageBox.Show("Выберите тест для начала прохождения.");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(_parentPage);
        }
    }
}
