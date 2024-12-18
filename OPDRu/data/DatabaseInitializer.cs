namespace OPDRu.data
{
    public static class DatabaseInitializer
    {
        public static void InitializeDatabase(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}