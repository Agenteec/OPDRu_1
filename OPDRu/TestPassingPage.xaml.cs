using OPDRu.data;
using OPDRu.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace OPDRu
{
    public partial class TestPassingPage : Page
    {
        private readonly Test _test;
        private readonly User _currentUser;
        private readonly IDatabaseService _databaseService;
        private readonly Page _parentPage;
        private int _currentQuestionIndex = 0;
        private DispatcherTimer _timer;
        private TimeSpan _timeRemaining;
        private readonly List<bool> _results = new();

        public TestPassingPage(User currentUser, Test test, IDatabaseService databaseService, Page parentPage)
        {
            InitializeComponent();
            _test = test;
            _databaseService = databaseService;
            _parentPage = parentPage;
            _currentUser = currentUser;

            _timeRemaining = TimeSpan.FromMinutes(15);
            InitializeTimer();
            LoadQuestion();
        }

        private void InitializeTimer()
        {
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            _timeRemaining -= TimeSpan.FromSeconds(1);
            TimerTextBlock.Text = _timeRemaining.ToString("mm\\:ss");

            if (_timeRemaining <= TimeSpan.Zero)
            {
                _timer.Stop();
                EndTest();
            }
        }


        private void LoadQuestion()
        {
            if (_currentQuestionIndex >= _test.Questions.Count)
            {
                EndTest();
                return;
            }

            var question = _test.Questions[_currentQuestionIndex];

            if (question == null || question.Answers == null || !question.Answers.Any())
            {
                MessageBox.Show("Для текущего вопроса отсутствуют варианты ответов.");
                EndTest();
                return;
            }

            QuestionTextBlock.Text = question.Text;


            AnswersListBox.ItemsSource = question.Answers;


            BackButton.IsEnabled = _currentQuestionIndex > 0;
            NextButton.Content = _currentQuestionIndex < _test.Questions.Count - 1 ? "Следующий вопрос" : "Завершить тест";
        }

        private void SaveAnswer()
        {

            var selectedAnswer = AnswersListBox.ItemsSource.Cast<Answer>().FirstOrDefault(a => a.IsSelected);

            if (selectedAnswer != null)
            {
                var correctAnswer = _test.Questions[_currentQuestionIndex].Answers.FirstOrDefault(a => a.IsCorrect);
                _results.Add(selectedAnswer.Text == correctAnswer?.Text);
            }
            else
            {

                _results.Add(false);
            }
        }

        private void NextQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            SaveAnswer();

            if (_currentQuestionIndex < _test.Questions.Count - 1)
            {
                _currentQuestionIndex++;
                LoadQuestion();
            }
            else
            {
                EndTest();
            }
        }

        private void PreviousQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentQuestionIndex > 0)
            {
                _currentQuestionIndex--;
                LoadQuestion();
            }
        }

        private void StopTestButton_Click(object sender, RoutedEventArgs e)
        {
            EndTest();
        }

        private void EndTest()
        {
            _timer.Stop();

            int correctCount = _results.Count(r => r);
            int totalQuestions = _test.Questions.Count;

            var statistic = new Statistic
            {
                UserId = _currentUser.Id,
                TestId = _test.Id,
                DateTaken = DateTime.Now,
                CorrectAnswers = correctCount,
                TotalQuestions = totalQuestions
            };

            _databaseService.SaveStatisticAsync(statistic);

            NavigationService?.Navigate(new TestResultPage(_results, _test.Questions, _parentPage));
        }
    }
}
