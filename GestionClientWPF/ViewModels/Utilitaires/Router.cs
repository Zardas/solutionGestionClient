using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace GestionClientWPF.ViewModels
{
    public class Router
    {


        public void GoToConnexion(Window window)
        {
            Views.Connexion newWindow = new Views.Connexion();
            newWindow.Show();
            window.Close();
        }

        public void GoToInscription(Window window)
        {
            Views.Inscription newWindow = new Views.Inscription();
            newWindow.Show();
            window.Close();
        }

        public void GoToInterfaceAdministrateur(Window window, int idAdministrateur, string Token)
        {
            Views.InterfaceAdministrateur newWindow = new Views.InterfaceAdministrateur(idAdministrateur, Token);
            newWindow.Show();
            window.Close();
        }

        public void GoToAjoutClient(Window window, int idAdministrateur, string Token)
        {
            Views.AjoutClient newWindow = new Views.AjoutClient(idAdministrateur, Token);
            newWindow.Show();
            window.Close();
        }

        public void GoToInterfaceCommercial(Window window, int idCommercial, string Token)
        {
            Views.InterfaceCommercial newWindow = new Views.InterfaceCommercial(idCommercial, Token);
            newWindow.Show();
            window.Close();
        }

        public void GoToListeCompteClient(Window window, int idClient, string Token)
        {
            Views.ListeComptesClient newWindow = new Views.ListeComptesClient(idClient, Token);
            newWindow.Show();
            window.Close();
        }
    }
}
