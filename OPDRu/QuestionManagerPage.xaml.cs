using OPDRu.data;
using OPDRu.services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace OPDRu
{
    public partial class QuestionManagerPage : Page
    {
        private readonly Test _test;
        private readonly IDatabaseService _databaseService;
        private readonly Page _parrent;
        public ObservableCollection<Question> Questions { get; set; }

        public QuestionManagerPage(Test test, IDatabaseService databaseService, Page parrent)
        {
            InitializeComponent();
            _test = test;
            _databaseService = databaseService;
            _parrent = parrent;
            Questions = new ObservableCollection<Question>();
            QuestionsDataGrid.ItemsSource = Questions;
            LoadQuestions();
        }

        private async void LoadQuestions()
        {
            var questions = await _databaseService.GetQuestionsByTestIdAsync(_test.Id);
            Questions.Clear();
            foreach (var question in questions)
            {
                Questions.Add(question);
            }
        }

        private async void DeleteQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            if (QuestionsDataGrid.SelectedItem is Question selectedQuestion)
            {
                await _databaseService.DeleteQuestionAsync(selectedQuestion.Id);
                Questions.Remove(selectedQuestion);
            }
        }

        private void QuestionsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (QuestionsDataGrid.SelectedItem is Question selectedQuestion)
            {
                var answerPage = new AnswerManagerPage(selectedQuestion, _databaseService, this);
                NavigationService.Navigate(answerPage);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(_parrent);
        }
        private void AnswersDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (QuestionsDataGrid.SelectedItem is Question selectedQuestion)
            {
                var answerPage = new AnswerManagerPage(selectedQuestion, _databaseService, this);
                NavigationService.Navigate(answerPage);
            }
        }

        private async void QuestionsDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                if (e.Row.Item is Question editedQuestion)
                {
                    var existingQuestions = await _databaseService.GetQuestionsByTestIdAsync(_test.Id);
                    var questionInDb = existingQuestions.FirstOrDefault(q => q.Id == editedQuestion.Id);
                    editedQuestion.TestId  = _test.Id;
                    if (questionInDb == null)
                    {
                        if (!String.IsNullOrEmpty(editedQuestion?.Text))
                        {
                            await _databaseService.AddQuestionAsync(editedQuestion);
                        }
                        //Questions.Add(editedQuestion);
                    }
                    else
                    {
                        questionInDb.Text = editedQuestion.Text;
                        await _databaseService.Save();
                    }
                }
            }
        }
    }
}
