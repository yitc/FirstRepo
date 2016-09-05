using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIS.Model;
using BIS.Business;
using Telerik.WinControls.UI;

namespace GUI
{
    /// <summary>
    /// Interfejs za data gridove. 
    /// Svaki grid koji nasledi ovaj interfejs mora implementirati defnisane metode.
    /// </summary>
    public interface IBISGrid
    {
        // Vraca koleciju kolona iz grida
        GridViewColumnCollection Columns
        {
            get;                    
        }

        // Vraca odgovarajuci filter folder
        string FilterFolder
        {
            get;
        }

        // true - Ucitava Menu, false - Ne uzitava menu
        bool bLoadTreeMenu
        {
            get;
            set;
        }

        // Vraca odgovarajuci label folder
        string LabelFolder
        {
            get;
        }
        // Binduje data source na grid.
        void SetDataPersonBinding(List<IModel> model);
        
        //Snima layout grida
        void SaveLayout(string file);
        
        //Ucitava layout grida
        void LoadLayout(string file);
        
        // Vraca podatke iz baze podataka. 
        List<IModel> GetData(int selectedFilter, List<int> idLabelList,string intLang);
        
        // Vraca listu filtera za taj grid
        List<FilterModel> ReturnFilters();
        
        // Vraca listu labela za taj grid
        List<LabelModel> ReturnLabels();
        
        //Vraca listu custom filtera za taj grid
        void LoadCustomFilters(IList<RadTreeNode> nodes, Boolean isSaveLayoutDialogClicked);
        
        // Ucitava favorites menu za taj grid
        void LoadTreeFavorites(IList<RadTreeNode> nodes, List<FilterModel> lista);
        
        //Ucitava u menu listu labela a taj grid
        void LoadTreeViewRootLabels(IList<RadTreeNode> nodes, List<LabelModel> lista);
        
        // Resetuje filtere
        void ClearDescriptors();
        
        //public void SetDataPersonBinding(List<PersonModel> binding)
    }
}
