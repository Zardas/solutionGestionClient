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
    /// Interaction logic for AjoutService.xaml
    /// </summary>
    public partial class AjoutService : Window
    {
        public AjoutService(int idCommercial, string Token)
        {
            InitializeComponent();
            DataContext = new ViewModels.AjoutServiceViewModel(this, idCommercial, Token);
        }
    }
}
