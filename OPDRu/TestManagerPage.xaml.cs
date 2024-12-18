using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using OPDRu.data;
using OPDRu.services;

namespace OPDRu
{
    public partial class TestManagerPage : Page
    {
        private readonly IDatabaseService _databaseService;
        private readonly Page _parrent;
        public ObservableCollection<Test> Tests { get; set; }

        public TestManagerPage(IDatabaseService databaseService, Page parrent)
        {
            InitializeComponent();
            _databaseService = databaseService;
            _parrent = parrent;
            Tests = new ObservableCollection<Test>();
            TestsDataGrid.ItemsSource = Tests;
            LoadTests();
        }

        private async void LoadTests()
        {
            var tests = await _databaseService.GetAllTestsAsync();
            Tests.Clear();
            foreach (var test in tests)
            {
                Tests.Add(test);
            }
        }

        private async void TestsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (TestsDataGrid.SelectedItem is Test selectedTest)
            {
                var questionPage = new QuestionManagerPage(selectedTest, _databaseService, this);
                NavigationService?.Navigate(questionPage);
            }
        }

        private void EditMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (TestsDataGrid.SelectedItem is Test selectedTest)
            {
                var questionPage = new QuestionManagerPage(selectedTest, _databaseService, this);
                NavigationService?.Navigate(questionPage);
            }
        }

        private async void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (TestsDataGrid.SelectedItem is Test selectedTest)
            {
                
                Tests.Remove(selectedTest);
                await _databaseService.DeleteTestAsync(selectedTest.Id);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(_parrent);
        }



        private async void TestsDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                if (e.Row.Item is Test newTest)
                {
                    
                    var existingTest = await _databaseService.GetAllTestsAsync();
                    var testInDb = existingTest.FirstOrDefault(t => t.Id == newTest.Id);

                    if (testInDb == null)
                    {
                        if (!String.IsNullOrEmpty(newTest.Name) && !String.IsNullOrEmpty(newTest.Description))
                        {
                            await _databaseService.AddTestAsync(newTest);
                        }
                        
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(newTest.Name) && !String.IsNullOrEmpty(newTest.Description))
                        {
                            testInDb.Name = newTest.Name;
                            testInDb.Description = newTest.Description;
                            await _databaseService.Save();
                        }
                    }
                }
            }
        }

    }
}