using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using EnadlaInventory.UI.ViewModels;

namespace EnadlaInventory.UI.Views.Windows
{
    public partial class SignUpWindow : Window
    {
        public SignUpWindow()
        {
            this.DataContext = new SignUpVM();
            InitializeComponent();
        }

        private void CancelBtn_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
