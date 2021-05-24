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
    /// Interaction logic for ListeFactures.xaml
    /// </summary>
    public partial class ListeFactures : Window
    {
        public ListeFactures(int IdCompte, string Token)
        {
            InitializeComponent();
            DataContext = new ViewModels.ListeFacturesViewModel(this, IdCompte, Token);
        }
    }
}
