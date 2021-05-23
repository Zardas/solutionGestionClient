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
    /// Interaction logic for ModificationClient.xaml
    /// </summary>
    public partial class ModificationClient : Window
    {
        public ModificationClient(int IdAdministrateur, string Token, Models.Client client)
        {
            InitializeComponent();
            DataContext = new ViewModels.ModifierClientViewModel(this, IdAdministrateur, Token, client);
        }
    }
}
