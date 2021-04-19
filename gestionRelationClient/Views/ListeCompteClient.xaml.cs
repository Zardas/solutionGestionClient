﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Logique d'interaction pour ListeCompteClient.xaml
    /// </summary>
    public partial class ListeCompteClient : Window
    {
        public ListeCompteClient(Models.Utilisateur client)
        {
            InitializeComponent();
            DataContext = new ViewModels.ListeComptesViewModel(this, client);
        }
    }
}
