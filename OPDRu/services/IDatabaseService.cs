using OPDRu.data;

namespace OPDRu.services
{
    public interface IDatabaseService
    {
        Task AddUserAsync(User user);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<List<User>> GetAllUsersAsync();
    }

}
