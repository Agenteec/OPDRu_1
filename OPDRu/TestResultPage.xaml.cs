using OPDRu.data;
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
    /// Логика взаимодействия для TestResultPage.xaml
    /// </summary>
    public partial class TestResultPage : Page
    {
        private readonly Page _parentPage;

        public TestResultPage(List<bool> results, List<Question> questions, Page parentPage)
        {
            InitializeComponent();
            _parentPage = parentPage;

            var resultData = questions.Select((q, i) => new
            {
                Question = q.Text,
                YourAnswer = q.Answers.FirstOrDefault(a => a.IsCorrect)?.Text ?? "Нет ответа",
                CorrectAnswer = q.Answers.FirstOrDefault(a => a.IsCorrect)?.Text
            }).ToList();

            ResultsDataGrid.ItemsSource = resultData;

            var correctCount = results.Count(r => r);
            ScoreTextBlock.Text = $"Ваш результат: {correctCount} из {questions.Count}";
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(_parentPage);
        }
    }
}
