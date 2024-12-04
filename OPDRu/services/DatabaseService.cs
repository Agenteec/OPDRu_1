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
    }

}
