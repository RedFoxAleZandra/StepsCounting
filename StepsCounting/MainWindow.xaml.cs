
using System.Windows;
using System.Windows.Controls;

namespace StepsCounting
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new AppViewModel();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            User item = (User)(sender as Button).DataContext;
            AppViewModel.SaveUserData(item);
            MessageBox.Show("Данные сохранены на диск!\n Путь: C:\\jsonFiles");
        }

    }

}

