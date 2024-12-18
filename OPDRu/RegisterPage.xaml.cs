using OPDRu.data;
using OPDRu.services;
using System.Windows;
using System.Windows.Controls;

namespace OPDRu
{
    /// <summary>
    /// Логика взаимодействия для RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        private readonly IDatabaseService _dbService;

        public RegisterPage()
        {
            InitializeComponent();
            _dbService = new DatabaseService(new ApplicationDbContext());
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UsernameBox.Text) || string.IsNullOrWhiteSpace(PasswordBox1.Password) || string.IsNullOrWhiteSpace(PasswordBox2.Password))
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }
            if (!PasswordBox1.Password.Equals(PasswordBox2.Password))
            {
                MessageBox.Show("Пароли не совпадают!");
                return;
            }
            var existingUser = await _dbService.GetUserByUsernameAsync(UsernameBox.Text);
            if (existingUser != null)
            {
                MessageBox.Show("Пользователь с таким логином уже существует!");
                return;
            }

            var user = new User
            {
                Username = UsernameBox.Text,
                Password = PasswordBox1.Password
            };

            await _dbService.AddUserAsync(user);
            MessageBox.Show("Регистрация успешна!");
            NavigationService?.Navigate(new LoginPage());
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new LoginPage());
        }
    }

}
