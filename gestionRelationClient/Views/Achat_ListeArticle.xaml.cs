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
    /// Logique d'interaction pour Achat_ListeArticle.xaml
    /// </summary>
    public partial class Achat_ListeArticle : Window
    {
        public Achat_ListeArticle(int compteId)
        {
            InitializeComponent();
            DataContext = new ViewModels.AchatArticleViewModel(this, compteId);
        }
    }
}
