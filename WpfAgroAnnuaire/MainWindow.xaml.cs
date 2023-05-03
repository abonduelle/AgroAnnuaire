using AgroAnnuaire.Data;
using AgroAnnuaire.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfAgroAnnuaire
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Site> MesSites { get; set; }
        public List<Service> MesServices { get; set; }
        public List<Collaborateur> MesCollaborateurs { get; set; }
        public object CollaborateursCompleteList { get; }

        public MainWindow()
        {
            InitializeComponent();

            using (AgroAnnuaireContext agroAnnuaireContext = new())
            {
                MesCollaborateurs = agroAnnuaireContext.Collaborateurs.ToList();
                MesSites = agroAnnuaireContext.Sites.ToList();
                MesServices = agroAnnuaireContext.Services.ToList();
            }
            CollaborateursList.ItemsSource = MesCollaborateurs;
            ComboSiteList.ItemsSource = MesSites;
            WorkingComboSiteList.ItemsSource = MesSites;
            ComboServiceList.ItemsSource = MesServices;
            WorkingComboServiceList.ItemsSource = MesServices;

            CollaborateursCompleteList = MesCollaborateurs;

        }
        public void CRUD_Open(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Bienvenue Administrateur, vous pouvez modifier l'annuaire");
            BtnDeleteCollaborateur.Visibility = Visibility.Visible;
            BtnSaveCollaborateur.Visibility = Visibility.Visible;
            ServiceCollaborateur.Visibility = Visibility.Visible;
            BtnDeleteService.Visibility = Visibility.Visible;
            BtnSaveService.Visibility = Visibility.Visible;
            SiteCollaborateur.Visibility= Visibility.Visible;   
            BtnDeleteSite.Visibility = Visibility.Visible;
            BtnSaveSite.Visibility = Visibility.Visible;
            WorkingComboServiceList.Visibility = Visibility.Visible;
            WorkingComboSiteList.Visibility = Visibility.Visible;
            Site.Visibility = Visibility.Hidden;
            Service.Visibility = Visibility.Hidden; 
        }

        private void ComboSiteList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboSiteList.SelectedIndex >= 0)
            {
            //Récupérer l'id du site
            int IdSite = (int)ComboSiteList.SelectedValue;

                //Filtre une liste de collaborateurs qui ont l'id du site en clé secondaire
                MesCollaborateurs = MesCollaborateurs.Where(x => x.SiteId == IdSite).ToList();
                CollaborateursList.ItemsSource = MesCollaborateurs;
            }
        }

        private void ComboServiceList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboServiceList.SelectedIndex >= 0)
            {
            //Récupérer l'id du service
            int IdService = (int)ComboServiceList.SelectedValue;

                //Filtre une liste de collaborateurs qui ont l'id du service en clé secondaire
                MesCollaborateurs = MesCollaborateurs.Where(x => x.ServiceId == IdService).ToList();
                CollaborateursList.ItemsSource = MesCollaborateurs;
            }
        }

        private void Reinitialize_Click(object sender, RoutedEventArgs e)
        {
            CollaborateursList.ItemsSource = (System.Collections.IEnumerable)CollaborateursCompleteList;
            MesCollaborateurs = (List<Collaborateur>)CollaborateursCompleteList;
            CollaborateursList.ItemsSource = MesCollaborateurs;
            ComboSiteList.SelectedValue = null;
            ComboSiteList.SelectedIndex = -1;
            ComboServiceList.SelectedValue = null;
            ComboServiceList.SelectedIndex = -1;
            Filtre.Text = string.Empty;
            WorkingComboSiteList.SelectedValue = null;
            WorkingComboSiteList.SelectedIndex = -1;
            WorkingComboServiceList.SelectedValue = null;
            WorkingComboServiceList.SelectedIndex = -1;
        }

        private void Filtre_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Recrée une liste de collaborateurs qui contiennent les caractères de la textbox
            string MesCaracteres = Filtre.Text;
            MesCollaborateurs = MesCollaborateurs.Where(x => x.LastName.Contains(MesCaracteres)).ToList();
            CollaborateursList.ItemsSource = MesCollaborateurs;
        }

        private void CollaborateursList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Charge tous les éléments du collaborateur sélectionné dans les champs
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            Nom.Text = string.Empty;
            Prenom.Text = string.Empty;
            TelFixe.Text = string.Empty;
            TelMobile.Text = string.Empty;
            Courriel.Text = string.Empty;
            Service.Text = string.Empty;
            Site.Text = string.Empty;
            ID.Text = string.Empty;
            WorkingComboSiteList.SelectedValue = null;
            WorkingComboSiteList.SelectedIndex = -1;
            WorkingComboServiceList.SelectedValue = null;
            WorkingComboServiceList.SelectedIndex = -1;
        }

        private void Delete_Collaborateur(object sender, MouseButtonEventArgs e)
        {
            using (AgroAnnuaireContext agroAnnuaireContext = new())
            {
                Collaborateur collaborateur = new Collaborateur();
                if (int.Parse(ID.Text) < 1) // alors on efface
                {
                    Clear_Click(sender, e);
                }
                else // sinon on supprime avant d'effacer
                {
                    collaborateur.Id = int.Parse(ID.Text);
                    agroAnnuaireContext.Collaborateurs.Remove(collaborateur);
                    agroAnnuaireContext.SaveChanges();
                    MesCollaborateurs = agroAnnuaireContext.Collaborateurs.ToList();
                    Clear_Click(sender, e);
                }
            }
        }
        private void Save_Collaborateur(object sender, MouseButtonEventArgs e)
        {
            using (AgroAnnuaireContext agroAnnuaireContext = new())
            {
                Collaborateur collaborateur = new Collaborateur();
                if (ID.Text == "") // alors c'est une création et on enregistre
                {
                    collaborateur.LastName = Nom.Text;
                    collaborateur.FirstName = Prenom.Text;
                    collaborateur.PhoneNumber = TelFixe.Text;
                    collaborateur.MobilePhoneNumber = TelMobile.Text;
                    collaborateur.Email = Courriel.Text;
                    collaborateur.ServiceId = int.Parse(WorkingComboServiceList.SelectedValue.ToString());
                    collaborateur.SiteId = int.Parse(WorkingComboServiceList.SelectedValue.ToString());

                    agroAnnuaireContext.Collaborateurs.Add(collaborateur);
                    agroAnnuaireContext.SaveChanges();
                        MesCollaborateurs = agroAnnuaireContext.Collaborateurs.ToList();

                }
                else // sinon c'est une modification, reprendre l'id
                {
                    collaborateur.Id = int.Parse(ID.Text);
                    collaborateur.LastName = Nom.Text;
                    collaborateur.FirstName = Prenom.Text;
                    collaborateur.PhoneNumber = TelFixe.Text;
                    collaborateur.MobilePhoneNumber = TelMobile.Text;
                    collaborateur.Email = Courriel.Text;
                    collaborateur.ServiceId = int.Parse(WorkingComboServiceList.SelectedValue.ToString());
                    collaborateur.SiteId = int.Parse(WorkingComboServiceList.SelectedValue.ToString());

                    agroAnnuaireContext.Collaborateurs.Update(collaborateur);
                    agroAnnuaireContext.SaveChanges();
                    MesCollaborateurs = agroAnnuaireContext.Collaborateurs.ToList();
                }
            };
                CollaborateursList.ItemsSource = MesCollaborateurs;
        }

        private void BtnSaveSite_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (AgroAnnuaireContext agroAnnuaireContext = new())
            {
                Site site = new Site();
                if (ComboSiteList.SelectedValue == "") // alors c'est une création et on enregistre
                {
                    site.Ville = SiteCollaborateur.Text;
                    site.Type = "Site de production";
                    agroAnnuaireContext.Sites.Add(site);
                    agroAnnuaireContext.SaveChanges();
                    MesSites.ToList().Add(site);
                }
                else // sinon c'est une modification, reprendre l'id
                {
                    site.Id = (int)ComboSiteList.SelectedValue;
                    site.Ville = SiteCollaborateur.Text;
                    site.Type = "Site de production";
                    agroAnnuaireContext.Sites.Update(site);
                    agroAnnuaireContext.SaveChanges();
                    MesSites = agroAnnuaireContext.Sites.ToList();
                    //MesSites.ToList().Add(site);
                }
            }
        }

        private void BtnDeleteSite_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (AgroAnnuaireContext agroAnnuaireContext = new())
            {
                Site site = new Site();
                int a = (int)ComboSiteList.SelectedValue;
                if (a < 1) // Il n'y a rien à supprimer, on efface
                {
                    SiteCollaborateur.Text = "";
                }
                else // sinon on supprime avant d'effacer
                {
                    site.Id = a;
                    object t = ComboSiteList.SelectedItem;

                    String str = ((AgroAnnuaire.Models.Site)t).Type;

                    if (str == "Siège social")
                    {
                        MessageBox.Show("Vous ne pouvez pas supprimer le site du siège social");
                    }
                    else
                    {
                    agroAnnuaireContext.Sites.Remove(site);
                    agroAnnuaireContext.SaveChanges();
                    MesSites = agroAnnuaireContext.Sites.ToList();
                    SiteCollaborateur.Text = "";
                    }
                }
            }
        }
    }
}
