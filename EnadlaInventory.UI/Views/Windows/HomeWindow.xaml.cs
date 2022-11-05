using System.Windows;
using EnadlaInventory.UI.Views.Windows;

namespace EnadlaInventory.UI.Views.Windows
{
    public partial class HomeWindow : Window
    {
        public HomeWindow()
        {
            InitializeComponent();
        }

        private void onClickCreateNewAccount(object sender, RoutedEventArgs e)
        {
            SignUpWindow signUpWindow = new SignUpWindow();
            signUpWindow.Owner = this;
            signUpWindow.ShowDialog();
        }
    }
}
