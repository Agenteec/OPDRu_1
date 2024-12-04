namespace OPDRu.data
{
    public static class DatabaseInitializer
    {
        public static void InitializeDatabase()
        {
            using var context = new ApplicationDbContext();
            context.Database.EnsureCreated();
        }
    }
}
