using OPDRu.data;
using OPDRu.services;
using System.Windows;
using System.Windows.Controls;

namespace OPDRu
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private readonly IDatabaseService _dbService;

        public LoginPage()
        {
            InitializeComponent();
            _dbService = new DatabaseService(new ApplicationDbContext());
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var user = await _dbService.GetUserByUsernameAsync(UsernameBox.Text);
            if (user != null && user.Password == PasswordBox.Password)
            {
                NavigationService?.Navigate(new HomePage(user, _dbService));
            }
            else
            {
                MessageBox.Show("Неправильный логин или пароль!");
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new RegisterPage());
        }
    }


}
