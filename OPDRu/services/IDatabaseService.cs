﻿using OPDRu.data;

namespace OPDRu.services
{
    public interface IDatabaseService
    {
        Task AddUserAsync(User user);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<List<User>> GetAllUsersAsync();
        Task AddTestAsync(Test test);
        Task<List<Test>> GetAllTestsAsync();
        Task<List<Question>> GetQuestionsByTestIdAsync(int testId);
        Task<List<Answer>> GetAnswersByQuestionIdAsync(int questionId);
        Task AddQuestionAsync(Question question);
        Task DeleteQuestionAsync(int questionId);
        Task AddAnswerAsync(Answer answer);
        Task DeleteAnswerAsync(int answerId);
        Task DeleteTestAsync(int id);
        Task SaveStatisticAsync(Statistic statistic);
        Task<List<Statistic>> GetUserStatisticsAsync(int userId);
        Task<Test?> GetTestWithQuestionsAndAnswersAsync(int testId);
        Task Save();
    }

}
