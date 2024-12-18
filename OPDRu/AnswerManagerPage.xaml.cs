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
    public partial class AnswerManagerPage : Page
    {
        private readonly Question _question;
        private readonly Page _parrent;
        private readonly IDatabaseService _databaseService;


        public AnswerManagerPage(Question question, IDatabaseService databaseService, Page parrent)
        {
            InitializeComponent();
            _question = question;
            _databaseService = databaseService;
            _parrent = parrent;
            LoadAnswers();
        }

        private async void LoadAnswers()
        {
            AnswersDataGrid.ItemsSource = _question.Answers;
        }

        private async void AddAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            var answerText = AnswerTextBox.Text;

            if (!string.IsNullOrWhiteSpace(answerText))
            {
                var newAnswer = new Answer
                {
                    Text = answerText,
                    QuestionId = _question.Id,
                    IsCorrect = IsAnswerCorrectCheckBox.IsChecked ?? false
                };

                await _databaseService.AddAnswerAsync(newAnswer);
                LoadAnswers();
                AnswerTextBox.Clear();
            }
            else
            {
                MessageBox.Show("Пожалуйста, заполните текст ответа.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private async void DeleteAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            if (AnswersDataGrid.SelectedItem is Answer selectedAnswer)
            {
                await _databaseService.DeleteAnswerAsync(selectedAnswer.Id);
                LoadAnswers();
            }
        }
        private void AnswersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(_parrent);
        }

    }
}
