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
    /// Interaction logic for ModificationGestionnaire.xaml
    /// </summary>
    public partial class ModificationGestionnaire : Window
    {
        public ModificationGestionnaire(int IdAdministrateur, string Token, Models.Gestionnaire gestionnaire)
        {
            InitializeComponent();
            DataContext = new ViewModels.ModifierGestionnaireViewModel(this, IdAdministrateur, Token, gestionnaire);
        }
    }
}
