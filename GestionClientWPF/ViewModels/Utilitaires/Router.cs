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

        public void GoToInterfaceAdministrateur(Window window, int idAdministrateur)
        {
            Views.InterfaceAdministrateur newWindow = new Views.InterfaceAdministrateur(idAdministrateur);
            newWindow.Show();
            window.Close();
        }

        public void GoToInterfaceCommercial(Window window, int idCommercial)
        {
            Views.InterfaceCommercial newWindow = new Views.InterfaceCommercial(idCommercial);
            newWindow.Show();
            window.Close();
        }

        public void GoToListeCompteClient(Window window, int idClient)
        {
            Views.ListeComptesClient newWindow = new Views.ListeComptesClient(idClient);
            newWindow.Show();
            window.Close();
        }
    }
}
