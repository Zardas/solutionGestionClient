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
    /// Interaction logic for OuvertureTicketSupport.xaml
    /// </summary>
    public partial class OuvertureTicketSupport : Window
    {
        public OuvertureTicketSupport(int IdCompte, string Token, int IdArticle)
        {
            InitializeComponent();
            DataContext = new ViewModels.OuvertureTicketSupportViewModel(this, IdCompte, Token, IdArticle);
        }
    }
}
