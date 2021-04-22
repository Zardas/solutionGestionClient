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

namespace gestionRelationClient.Views
{
    /// <summary>
    /// Logique d'interaction pour ConnexionGestionnaire.xaml
    /// </summary>
    public partial class ConnexionGestionnaire : Window
    {
        public ConnexionGestionnaire()
        {
            InitializeComponent();
            DataContext = new ViewModels.GestionnaireViewModel(this);
        }
    }
}
