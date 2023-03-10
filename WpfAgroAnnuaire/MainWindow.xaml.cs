using AgroAnnuaire.Data;
using AgroAnnuaire.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Site = AgroAnnuaire.Models.Site;

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
            ComboServiceList.ItemsSource = MesServices;

        }
        private void ComboSiteList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Récupérer l'id du site
            int IdSite;
            IdSite = (int)ComboSiteList.SelectedValue;

            //Recréer une liste de collaborateurs qui ont l'id du site en clé secondaire
            using (AgroAnnuaireContext agroAnnuaireContext = new())
            {
                MesCollaborateurs = agroAnnuaireContext.Collaborateurs.Where(x => x.SiteId == IdSite).ToList();
            }
            CollaborateursList.ItemsSource = MesCollaborateurs;  
        }

        private void ComboServiceList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Récupérer l'id du service
            int IdService;
            IdService = (int)ComboServiceList.SelectedValue;

            //Recréer une liste de collaborateurs qui ont l'id du service en clé secondaire
            using (AgroAnnuaireContext agroAnnuaireContext = new())
            {
                MesCollaborateurs = agroAnnuaireContext.Collaborateurs.Where(x => x.ServiceId == IdService).ToList();
            }
            CollaborateursList.ItemsSource = MesCollaborateurs;
        }

        private void Reinitialize_Click(object sender, RoutedEventArgs e)
        {
            
                using (AgroAnnuaireContext agroAnnuaireContext = new())
                {
                    MesCollaborateurs = agroAnnuaireContext.Collaborateurs.ToList();
                }
                CollaborateursList.ItemsSource = MesCollaborateurs;
               
        }

        private void Filtre_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Recrée une liste de collaborateurs qui contiennent les caractères de la textbox
            using (AgroAnnuaireContext agroAnnuaireContext = new())
            {
                string MesCaracteres = Filtre.Text;
                MesCollaborateurs = agroAnnuaireContext.Collaborateurs.Where(x => x.LastName.Contains(MesCaracteres)).ToList();
            }
            CollaborateursList.ItemsSource = MesCollaborateurs;

        }

        private void CollaborateursList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Charge tous les éléments du collaborateur sélectionné dans les champs
           
        }
    }
}
