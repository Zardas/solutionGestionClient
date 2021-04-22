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
    /// Logique d'interaction pour Consultation_DetailsArticle.xaml
    /// </summary>
    public partial class Consultation_DetailsArticle : Window
    {
        public Consultation_DetailsArticle(int compteId, int articleId)
        {
            InitializeComponent();
            // On utilise le même ViewModel que pour l'achat puisque c'est basiquement la même page sans le bouton d'achat
            DataContext = new ViewModels.AchatArticleDetailViewModel(this, compteId, articleId);
        }
    }
}
