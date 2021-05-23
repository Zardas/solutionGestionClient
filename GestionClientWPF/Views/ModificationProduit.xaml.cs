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
    /// Interaction logic for ModificationProduit.xaml
    /// </summary>
    public partial class ModificationProduit : Window
    {
        public ModificationProduit(int IdGestionnaire, string Token, Models.Produit produit)
        {
            InitializeComponent();
            DataContext = new ViewModels.ModifierProduitViewModel(this, IdGestionnaire, Token, produit);
        }
    }
}
