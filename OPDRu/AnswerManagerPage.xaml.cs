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

        private async void DeleteAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            if (AnswersDataGrid.SelectedItem is Answer selectedAnswer)
            {
                await _databaseService.DeleteAnswerAsync(selectedAnswer.Id);
                LoadAnswers();
            }
        }
        private async void AnswersDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                if (e.Row.Item is Answer editedAnswer)
                {
                    var existingAnswers = await _databaseService.GetAnswersByQuestionIdAsync(_question.Id);
                    var answerInDb = existingAnswers.FirstOrDefault(q => q.Id == editedAnswer.Id);
                    editedAnswer.QuestionId = _question.Id;
                    if (answerInDb == null)
                    {
                        if (!String.IsNullOrEmpty(editedAnswer?.Text))
                        {
                            await _databaseService.AddAnswerAsync(editedAnswer);
                        }
                    }
                    else
                    {
                        answerInDb.Text = editedAnswer.Text;
                        await _databaseService.Save();
                    }
                }
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(_parrent);
        }


    }
}
