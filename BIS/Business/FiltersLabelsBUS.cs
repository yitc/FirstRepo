using System;
using BIS.DAO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using BIS.Model;

namespace BIS.Business
{
    public class FiltersLabelsBUS
    {
        private FiltersLabelsDAO fl;
        public FiltersLabelsBUS()
        {
            fl = new FiltersLabelsDAO();
        }
        public List<IModel> GetAllFiltersLabels(string language)
        {
            DataTable dataTable = new DataTable();
            dataTable = fl.GetAllFiltersLabels(language);
            List<IModel> filters = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        FiltersLabelsModel model = new FiltersLabelsModel();

                        if (dr["ID"].ToString() != "" || dr["ID"].ToString() != null)
                            model.ID = Int32.Parse(dr["ID"].ToString());

                            model.name = dr["name"].ToString();
                            model.type = dr["type"].ToString();

                            model.nameMenu = dr["nameMenu"].ToString();
                            model.uniques= dr["uniques"].ToString();

                            if (dr["IDLabelUnique"].ToString() != "" || dr["IDLabelUnique"].ToString() != null)
                                model.IDLabelUnique = Int32.Parse(dr["IDLabelUnique"].ToString());

                        filters.Add(model);
                    }
                    return filters;
                }
                else
                    return filters;
            }
            else
                return filters;
        }

        public List<FilterModel> GetAllFilter()
        {
            DataTable dataTable = new DataTable();
            dataTable = fl.GetAllFilters();
            List<FilterModel> filters = new List<FilterModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        FilterModel model = new FilterModel();

                        if (dr["idFilter"].ToString() != "" || dr["idFilter"].ToString() != null)
                            model.idFilter = Int32.Parse(dr["idFilter"].ToString());

                        model.nameFilter = dr["nameFilter"].ToString();
                        model.sortFilter = Int32.Parse(dr["sortFilter"].ToString());

                        filters.Add(model);
                    }
                    return filters;
                }
                else
                    return filters;
            }
            else
                return filters;
        }

        public List<FilterModel> GetLastIdFilter()
        {
            DataTable dataTable = new DataTable();
            dataTable = fl.GetLastIdFilter();
            List<FilterModel> filters = new List<FilterModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        FilterModel model = new FilterModel();

                        if (dr["idFilter"].ToString() != "" || dr["idFilter"].ToString() != null)
                            model.idFilter = Int32.Parse(dr["idFilter"].ToString());

                        model.nameFilter = dr["nameFilter"].ToString();
                        model.sortFilter = Int32.Parse(dr["sortFilter"].ToString());

                        filters.Add(model);
                    }
                    return filters;
                }
                else
                    return filters;
            }
            else
                return filters;
        }

        public List<LabelModel> GetLastIdLabel()
        {
            DataTable dataTable = new DataTable();
            dataTable = fl.GetLastIdLabel();
            List<LabelModel> filters = new List<LabelModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        LabelModel model = new LabelModel();

                        if (dr["idLabel"].ToString() != "" || dr["idLabel"].ToString() != null)
                            model.idLabel = Int32.Parse(dr["idLabel"].ToString());

                        model.nameLabel = dr["nameLabel"].ToString();


                        filters.Add(model);
                    }
                    return filters;
                }
                else
                    return filters;
            }
            else
                return filters;
        }

        public bool UpdateFilterName(int idFilter, string nameFilter, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = fl.UpdateFilterName(idFilter, nameFilter, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
        public bool UpdateLabelName(int idLabel, string nameLabel, int idMenu, int id, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = fl.UpdateLabelName(idLabel, nameLabel, idMenu, id, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
       

        public List<MenuIDModel> GetIdMenu(string nameMenu)
        {
            DataTable dataTable = new DataTable();
            dataTable = fl.GetIDMenu(nameMenu);
            List<MenuIDModel> filters = new List<MenuIDModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        MenuIDModel model = new MenuIDModel();

                        if (dr["idMenu"].ToString() != "" || dr["idMenu"].ToString() != null)
                            model.idMenu = Int32.Parse(dr["idMenu"].ToString());

                      
                        filters.Add(model);
                    }
                    return filters;
                }
                else
                    return filters;
            }
            else
                return filters;
        }
        public bool InsertFilter(int idFilter, string nameFilter, int sortFilter, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = fl.InsertFilter(idFilter, nameFilter, sortFilter, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool InsertLabel(int idLabel, string nameLabel, int idMenu, int id, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {
                retval = fl.InsertLabel(idLabel, nameLabel, idMenu, id, nameForm, idUser);


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool DeleteFilter(int idFilter, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = fl.DeleteFilter(idFilter, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool DeleteLabel(int idLabel, int idUnique, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = fl.DeleteLabel(idLabel, idUnique, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
        public List<LastIdModel> isInFilter()
        {
            DataTable dataTable = new DataTable();
            dataTable = fl.isInFilter();
            List<LastIdModel> f = new List<LastIdModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        LastIdModel model = new LastIdModel();

                        if (dr["ID"].ToString() != "" || dr["ID"].ToString() != null)
                            model.ID = Int32.Parse(dr["ID"].ToString());
                        f.Add(model);
                    }
                    return f;
                }
                else
                    return f;
            }
            else
                return f;
        }
        public List<LastIdModel> isInLabel()
        {
            DataTable dataTable = new DataTable();
            dataTable = fl.isInLabel();
            List<LastIdModel> f = new List<LastIdModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        LastIdModel model = new LastIdModel();

                        if (dr["ID"].ToString() != "" || dr["ID"].ToString() != null)
                            model.ID = Int32.Parse(dr["ID"].ToString());
                        f.Add(model);
                    }
                    return f;
                }
                else
                    return f;
            }
            else
                return f;
        }

        public List<NameMenuModel> GetNameMenu()
        {
            DataTable dataTable = new DataTable();
            dataTable = fl.GetNameMenu();
            List<NameMenuModel> filters = new List<NameMenuModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        NameMenuModel model = new NameMenuModel();

                        model.nameMenu = (dr["nameMenu"].ToString());
                        filters.Add(model);

                        if (dr["idMenu"].ToString() != "" || dr["idMenu"]!=null)
                        model.idMenu = Convert.ToInt32(dr["idMenu"].ToString());
                    }
                    return filters;
                }
                else
                    return filters;
            }
            else
                return filters;
        }
        public List<LabelModel> GetIdLabelForExistName(string nameLabel)
        {
            DataTable dataTable = new DataTable();
            dataTable = fl.GetIdLabelForExistName(nameLabel);
            List<LabelModel> filters = new List<LabelModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        LabelModel model = new LabelModel();
                       // string a = dr["idLabel"].ToString();
                        if (dr["idLabel"].ToString().Equals("") || dr["idLabel"] == null)
                        {
                        }
                        else
                        {
                            model.idLabel = Int32.Parse(dr["idLabel"].ToString());
                        }

                        filters.Add(model);
                    }
                    return filters;
                }
                else
                    return filters;
            }
            else
                return filters;
        }

        public List<LabelModel> NameLabelIsExist(string nameLabel)
        {
            DataTable dataTable = new DataTable();
            dataTable = fl.NameLabelIsExist(nameLabel);
            List<LabelModel> filters = new List<LabelModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        LabelModel model = new LabelModel();

                        if (dr["idLabel"].ToString().Equals("-1") || dr["idLabel"].ToString() == null)
                        {
                            model.idLabel = -1;
                        }
                        else
                        {
                            model.idLabel = Int32.Parse(dr["idLabel"].ToString());
                        }

                        filters.Add(model);
                    }
                    return filters;
                }
                else
                    return filters;
            }
            else
                return filters;
        }

        public List<LabelModel> GetIdLabelForNotExistName()
        {
            DataTable dataTable = new DataTable();
            dataTable = fl.GetIdLabelForNotExistName();
            List<LabelModel> filters = new List<LabelModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        LabelModel model = new LabelModel();

                        if (dr["idLabel"].ToString().Equals(-1) || dr["idLabel"] == null)
                        {
                            model.idLabel = -1;
                        }
                        else
                        {
                            model.idLabel = Int32.Parse(dr["idLabel"].ToString());
                        }

                        filters.Add(model);
                    }
                    return filters;
                }
                else
                    return filters;
            }
            else
                return filters;
        }
        public List<LabelModel> ForExistLabelidIsExist(string nameLabel)
        {
            DataTable dataTable = new DataTable();
            dataTable = fl.ForExistLabelidIsExist(nameLabel);
            List<LabelModel> filters = new List<LabelModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        LabelModel model = new LabelModel();
                        string a = dr["idLabel"].ToString();
                        if (dr["idLabel"].ToString().Equals("-1") || dr["idLabel"] == null)
                        {
                            model.idLabel = -1;
                        }
                        else
                        {
                            model.idLabel = Int32.Parse(dr["idLabel"].ToString());
                         }

                        filters.Add(model);
                    }
                    return filters;
                }
                else
                    return filters;
            }
            else
                return filters;
        }
     // NOVO DELETE:
        public int checkIsInArrangementLabel(int idLabel)
        {
            int num = 0;
            DataTable dataTable = new DataTable();
            dataTable = fl.checkIsInArrangementLabel(idLabel);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    num = 1;
                }

            }
            return num;
        }

        public int checkIsInArrangementLabelFirst(int idLabel)
        {
            int num = 0;
            DataTable dataTable = new DataTable();
            dataTable = fl.checkIsInArrangementLabelFirst(idLabel);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    num = 1;
                }

            }
            return num;
        }

        public int checkIsInClient(int idLabel)
        {
            int num = 0;
            DataTable dataTable = new DataTable();
            dataTable = fl.checkIsInClientLabel(idLabel);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    num = 1;
                }

            }
            return num;
        }

        public int checkIsInContactPerson(int idLabel)
        {
            int num = 0;
            DataTable dataTable = new DataTable();
            dataTable = fl.checkIsInContactPersonLabel(idLabel);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    num = 1;
                }

            }
            return num;
        }
        public int checkIsInEmployee(int idLabel)
        {
            int num = 0;
            DataTable dataTable = new DataTable();
            dataTable = fl.checkIsInEmployeeLabel(idLabel);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    num = 1;
                }

            }
            return num;
        }
        public int checkIsInUser(int idLabel)
        {
            int num = 0;
            DataTable dataTable = new DataTable();
            dataTable = fl.checkIsInUserLabel(idLabel);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    num = 1;
                }

            }
            return num;
        }

        // Filteri:
        public int checkIsInArrangementFilter(int idFilter)
        {
            int num = 0;
            DataTable dataTable = new DataTable();
            dataTable = fl.checkIsInArrangementFilter(idFilter);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    num = 1;
                }

            }
            return num;
        }

      
        public int checkIsInClientFilter(int idFilter)
        {
            int num = 0;
            DataTable dataTable = new DataTable();
            dataTable = fl.checkIsInClientFilter(idFilter);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    num = 1;
                }

            }
            return num;
        }

        public int checkIsInContactPersonFilter(int idFilter)
        {
            int num = 0;
            DataTable dataTable = new DataTable();
            dataTable = fl.checkIsInContactPersonFilter(idFilter);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    num = 1;
                }

            }
            return num;
        }
        public int checkIsInEmployeeFilter(int idFilter)
        {
            int num = 0;
            DataTable dataTable = new DataTable();
            dataTable = fl.checkIsInEmployeeFilter(idFilter);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    num = 1;
                }

            }
            return num;
        }
        public int checkIsInUserFilter(int idFilter)
        {
            int num = 0;
            DataTable dataTable = new DataTable();
            dataTable = fl.checkIsInUserFilter(idFilter);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    num = 1;
                }

            }
            return num;
        }
    }
}
