using GestionClientWPF.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public void GoToModificationClient(Window window, int idAdministrateur, string Token, Client client)
        {
            Views.ModificationClient newWindow = new Views.ModificationClient(idAdministrateur, Token, client);
            newWindow.Show();
            window.Close();
        }

        public void GoToAjoutGestionnaire(Window window, int idAdministrateur, string Token)
        {
            Views.AjoutGestionnaire newWindow = new Views.AjoutGestionnaire(idAdministrateur, Token);
            newWindow.Show();
            window.Close();
        }

        public void GoToModificationGestionnaire(Window window, int idAdministrateur, string Token, Gestionnaire gestionnaire)
        {
            Views.ModificationGestionnaire newWindow = new Views.ModificationGestionnaire(idAdministrateur, Token, gestionnaire);
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
