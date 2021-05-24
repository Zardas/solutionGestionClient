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
    /// Interaction logic for ListeTicketsClient.xaml
    /// </summary>
    public partial class ListeTicketsClient : Window
    {
        public ListeTicketsClient(int IdCompte, string Token)
        {
            InitializeComponent();
            DataContext = new ViewModels.ListeTicketsClientViewModel(this, IdCompte, Token);
        }
    }
}
