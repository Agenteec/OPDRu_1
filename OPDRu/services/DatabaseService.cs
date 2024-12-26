namespace OPDRu.services
{
    using Microsoft.EntityFrameworkCore;
    using OPDRu.data;

    public class DatabaseService : IDatabaseService
    {
        private readonly ApplicationDbContext _context;

        public DatabaseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }


        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task AddTestAsync(Test test)
        {
            _context.Tests.Add(test);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Test>> GetAllTestsAsync()
        {
            return await _context.Tests.ToListAsync();
        }

        public async Task<List<Question>> GetQuestionsByTestIdAsync(int testId)
        {
            return await _context.Questions
                .Include(q => q.Answers)
                .Where(q => q.TestId == testId)
                .ToListAsync();
        }
        public async Task<List<Answer>> GetAnswersByQuestionIdAsync(int questionId)
        {
            return await _context.Answers
               .Where(a => a.QuestionId == questionId)
               .ToListAsync();
        }

        public async Task AddQuestionAsync(Question question)
        {
            _context.Questions.Add(question);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteQuestionAsync(int questionId)
        {
            var question = await _context.Questions.FindAsync(questionId);
            if (question != null)
            {
                _context.Questions.Remove(question);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddAnswerAsync(Answer answer)
        {
            _context.Answers.Add(answer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAnswerAsync(int answerId)
        {
            var answer = await _context.Answers.FindAsync(answerId);
            if (answer != null)
            {
                _context.Answers.Remove(answer);
                await _context.SaveChangesAsync();
            }
        }
        public async Task SaveStatisticAsync(Statistic statistic)
        {
            _context.Statistics.Add(statistic);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Statistic>> GetUserStatisticsAsync(int userId)
        {
            return await _context.Statistics
                .Include(s => s.Test)
                .Where(s => s.UserId == userId)
                .ToListAsync();
        }

        public async Task DeleteTestAsync(int testId)
        {
            var test = await _context.Tests.FindAsync(testId);
            if (test != null)
            {
                _context.Tests.Remove(test);
                await _context.SaveChangesAsync();
            }
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<Test?> GetTestWithQuestionsAndAnswersAsync(int testId)
        {
            return await _context.Tests
                .Include(t => t.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(t => t.Id == testId);
        }
    }
}
