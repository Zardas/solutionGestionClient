using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GestionClientWPF.Views
{
    /// <summary>
    /// Interaction logic for InterfaceClient.xaml
    /// </summary>
    public partial class InterfaceClient : Window
    {
        public InterfaceClient(int IdCompte, string Token)
        {
            InitializeComponent();
            DataContext = new ViewModels.InterfaceClientViewModel(this, IdCompte, Token);
        }
    }
}
