using OPDRu.data;
using System.Windows;

namespace OPDRu
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            using (var context = new ApplicationDbContext())
            {
                DatabaseInitializer.InitializeDatabase(context);
            }
        }
    }

}
