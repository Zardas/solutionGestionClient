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
    /// Interaction logic for AjoutClient.xaml
    /// </summary>
    public partial class AjoutClient : Window
    {
        public AjoutClient(int IdAdministrateur, string Token)
        {
            InitializeComponent();
            DataContext = new ViewModels.AjoutClientViewModel(this, IdAdministrateur, Token);
        }
    }
}
