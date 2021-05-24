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
    /// Interaction logic for ModificationService.xaml
    /// </summary>
    public partial class ModificationService : Window
    {
        public ModificationService(int IdGestionnaire, string Token, Models.Service service)
        {
            InitializeComponent();
            DataContext = new ViewModels.ModifierServiceViewModel(this, IdGestionnaire, Token, service);
        }
    }
}
