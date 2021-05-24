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
    /// Interaction logic for AssociationClient.xaml
    /// </summary>
    public partial class AssociationClient : Window
    {
        public AssociationClient(int idCommercial, string Token)
        {
            InitializeComponent();
            DataContext = new ViewModels.AjoutClientAssocieViewModel(this, idCommercial, Token);
        }
    }
}
