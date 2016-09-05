using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BIS.Core;
using BIS.Model;
using System.Data.SqlTypes;

namespace BIS.DAO
{
    public class EmployeeDAO
    {
        private dbConnection conn;
        public EmployeeDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetEmployee(Int32 iEmpID, string idLang)
        {
            string query = string.Format(@"
            SELECT idEmployee,firstNameEmployee,initialsEmployee, cp.titleEmployee ,CASE WHEN st.StringValue IS NOT NULL THEN st.StringValue ELSE t.nameTitle END AS nameTitle,cp.genderEmployee,CASE WHEN sg.StringValue IS NOT NULL THEN sg.StringValue ELSE g.nameGender END AS nameGender,midNameEmployee
                ,lastNameEmployee,maidenEmployee,addressEmployee,houseNumberEmployee,extensionEmployee,zipCodeEmployee,cityEmployee,cp.idCountry,c.internationalCode,dtBirthDateEmployee,
                isMariedEmployee,isentBsnEmploee,bicEmployee,ibanEmployee,emergencyPersonEmployee,emergencyTelEmployee,dtHireDateEmployee,contractNumberEmployee,Department,
                CASE WHEN std.StringValue IS NOT NULL THEN std.StringValue ELSE d.nameDepartment END AS nameDepartment,[Function],CASE WHEN stf.StringValue IS NOT NULL THEN stf.StringValue ELSE f.nameFunction END AS nameFunction,WishFunction,
                CASE WHEN stwf.StringValue IS NOT NULL THEN stwf.StringValue ELSE wf.nameFunction END as nameWishFunction,statusEmployee,CASE WHEN stes.StringValue IS NOT NULL THEN stes.StringValue ELSE es.descriptionEmployee END AS descriptionEmployee,imageEmployee,isAplicationUser
                FROM Employees cp
                LEFT JOIN Gender g ON g.idGender = cp.genderEmployee
                LEFT JOIN String" + idLang + @" sg ON sg.stringKey = g.nameGender 
                LEFT JOIN Title t ON t.idTitle = cp.titleEmployee
                LEFT JOIN String" + idLang + @" st ON st.stringKey = t.nameTitle 
                LEFT JOIN Departments d ON d.idDepartment = cp.Department
                LEFT JOIN String" + idLang + @" std ON std.stringKey = d.nameDepartment
                LEFT JOIN EmployeeFunction f ON f.idFunction = cp.[Function]
                LEFT JOIN String" + idLang + @" stf ON stf.stringKey = f.nameFunction
                LEFT JOIN EmployeeFunction wf ON wf.idFunction = cp.WishFunction
                LEFT JOIN String" + idLang + @" stwf ON stwf.stringKey = wf.nameFunction
                LEFT JOIN EmployeeStatus es ON es.idStatusEmployee = cp.statusEmployee
                LEFT JOIN String" + idLang + @" stes ON stes.stringKey = es.descriptionEmployee
                LEFT JOIN Country c ON c.idCountry = cp.idCountry
            where idEmployee = @iEmpID");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@iEmpID", SqlDbType.Int);
            sqlParameters[0].Value = iEmpID;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetAllEmploees(int idFilter, List<int> labels,string idLang)
        {

            string query = string.Format(@" SELECT idEmployee,firstNameEmployee,initialsEmployee, cp.titleEmployee ,CASE WHEN st.StringValue IS NOT NULL THEN st.StringValue ELSE t.nameTitle END AS nameTitle,cp.genderEmployee,CASE WHEN sg.StringValue IS NOT NULL THEN sg.StringValue ELSE g.nameGender END AS nameGender,midNameEmployee
                ,lastNameEmployee,maidenEmployee,addressEmployee,houseNumberEmployee,extensionEmployee,zipCodeEmployee,cityEmployee,cp.idCountry,c.internationalCode,dtBirthDateEmployee,
                isMariedEmployee,isentBsnEmploee,bicEmployee,ibanEmployee,emergencyPersonEmployee,emergencyTelEmployee,dtHireDateEmployee,contractNumberEmployee,Department,
                CASE WHEN std.StringValue IS NOT NULL THEN std.StringValue ELSE d.nameDepartment END AS nameDepartment,[Function],CASE WHEN stf.StringValue IS NOT NULL THEN stf.StringValue ELSE f.nameFunction END AS nameFunction,WishFunction,
                CASE WHEN stwf.StringValue IS NOT NULL THEN stwf.StringValue ELSE wf.nameFunction END as nameWishFunction,statusEmployee,CASE WHEN stes.StringValue IS NOT NULL THEN stes.StringValue ELSE es.descriptionEmployee END AS descriptionEmployee,imageEmployee,isAplicationUser
                FROM Employees cp
                LEFT JOIN Gender g ON g.idGender = cp.genderEmployee
                LEFT JOIN String"+idLang+@" sg ON sg.stringKey = g.nameGender 
                LEFT JOIN Title t ON t.idTitle = cp.titleEmployee
                LEFT JOIN String" + idLang + @" st ON st.stringKey = t.nameTitle 
                LEFT JOIN Departments d ON d.idDepartment = cp.Department
                LEFT JOIN String" + idLang + @" std ON std.stringKey = d.nameDepartment
                LEFT JOIN EmployeeFunction f ON f.idFunction = cp.[Function]
                LEFT JOIN String" + idLang + @" stf ON stf.stringKey = f.nameFunction
                LEFT JOIN EmployeeFunction wf ON wf.idFunction = cp.WishFunction
                LEFT JOIN String" + idLang + @" stwf ON stwf.stringKey = wf.nameFunction
                LEFT JOIN EmployeeStatus es ON es.idStatusEmployee = cp.statusEmployee
                LEFT JOIN String" + idLang + @" stes ON stes.stringKey = es.descriptionEmployee
                LEFT JOIN Country c ON c.idCountry = cp.idCountry");

            
            return conn.executeSelectQuery(query, null);

        }
        public DataTable GetLastEmployeeID()
        {
            string query = string.Format(@"SELECT TOP 1 idEmployee FROM Employees ORDER BY idEmployee DESC");
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return conn.executeSelectQuery(query, sqlParameters);
        }
             public DataTable GetAllEmpl(string idLang)
        {

            string query = string.Format(@"SELECT idEmployee,firstNameEmployee,initialsEmployee, cp.titleEmployee ,CASE WHEN st.StringValue IS NOT NULL THEN st.StringValue ELSE t.nameTitle END AS nameTitle,cp.genderEmployee,CASE WHEN sg.StringValue IS NOT NULL THEN sg.StringValue ELSE g.nameGender END AS nameGender,midNameEmployee
                ,lastNameEmployee,maidenEmployee,addressEmployee,houseNumberEmployee,extensionEmployee,zipCodeEmployee,cityEmployee,cp.idCountry,c.internationalCode,dtBirthDateEmployee,
                isMariedEmployee,isentBsnEmploee,bicEmployee,ibanEmployee,emergencyPersonEmployee,emergencyTelEmployee,dtHireDateEmployee,contractNumberEmployee,Department,
                CASE WHEN std.StringValue IS NOT NULL THEN std.StringValue ELSE d.nameDepartment END AS nameDepartment,[Function],CASE WHEN stf.StringValue IS NOT NULL THEN stf.StringValue ELSE f.nameFunction END AS nameFunction,WishFunction,
                CASE WHEN stwf.StringValue IS NOT NULL THEN stwf.StringValue ELSE wf.nameFunction END as nameWishFunction,statusEmployee,CASE WHEN stes.StringValue IS NOT NULL THEN stes.StringValue ELSE es.descriptionEmployee END AS descriptionEmployee,imageEmployee,isAplicationUser
                FROM Employees cp
                LEFT JOIN Gender g ON g.idGender = cp.genderEmployee
                LEFT JOIN String" + idLang + @" sg ON sg.stringKey = g.nameGender 
                LEFT JOIN Title t ON t.idTitle = cp.titleEmployee
                LEFT JOIN String" + idLang + @" st ON st.stringKey = t.nameTitle 
                LEFT JOIN Departments d ON d.idDepartment = cp.Department
                LEFT JOIN String" + idLang + @" std ON std.stringKey = d.nameDepartment
                LEFT JOIN EmployeeFunction f ON f.idFunction = cp.[Function]
                LEFT JOIN String" + idLang + @" stf ON stf.stringKey = f.nameFunction
                LEFT JOIN EmployeeFunction wf ON wf.idFunction = cp.WishFunction
                LEFT JOIN String" + idLang + @" stwf ON stwf.stringKey = wf.nameFunction
                LEFT JOIN EmployeeStatus es ON es.idStatusEmployee = cp.statusEmployee
                LEFT JOIN String" + idLang + @" stes ON stes.stringKey = es.descriptionEmployee
                LEFT JOIN Country c ON c.idCountry = cp.idCountry");

            return conn.executeSelectQuery(query, null);
                       
        }
             public bool Save(EmployeeModel employee, string nameForm, int idUser)
             {
                 List<string> _query = new List<string>();
                 List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

                 string query = string.Format(@"INSERT INTO 
                    Employees(firstNameEmployee, initialsEmployee, titleEmployee, genderEmployee, midNameEmployee, 
                    lastNameEmployee, maidenEmployee, addressEmployee, houseNumberEmployee, extensionEmployee, zipCodeEmployee, cityEmployee, dtBirthDateEmployee,
                    isMariedEmployee, isentBsnEmploee,bicEmployee,ibanEmployee,emergencyPersonEmployee,emergencyTelEmployee,   
                    dtHireDateEmployee,contractNumberEmployee,Department,WishFunction,statusEmployee,imageEmployee,isAplicationUser,[Function]) 
                    VALUES (@firstNameEmployee, @initialsEmployee, @titleEmployee, @genderEmployee, @midNameEmployee, 
                    @lastNameEmployee, @maidenEmployee, @addressEmployee, @houseNumberEmployee, @extensionEmployee, @zipCodeEmployee, @cityEmployee, @dtBirthDateEmployee,
                    @isMariedEmployee, @isentBsnEmploee,@bicEmployee,@ibanEmployee,@emergencyPersonEmployee,@emergencyTelEmployee,
                    @dtHireDateEmployee,@contractNumberEmployee,@Department,@WishFunction,@statusEmployee,@imageEmployee,@isAplicationUser,@Function)");
                 // ,dtHireDateEmployee,contractNumberEmployee,Department, WishFunction,statusEmployee,imageEmployee,isAplicationUser,[Function]
                 // ,@dtHireDateEmployee,@contractNumberEmployee,@Department,@WishFunction,@statusEmployee,@imageEmployee,@isAplicationUser,@[Function]

                 SqlParameter[] sqlParameter = new SqlParameter[27];

                 sqlParameter[0] = new SqlParameter("@firstNameEmployee", SqlDbType.NVarChar);
                 sqlParameter[0].Value = employee.firstNameEmployee;

                 sqlParameter[1] = new SqlParameter("@initialsEmployee", SqlDbType.NVarChar);
                 sqlParameter[1].Value = employee.initialsEmployee;

                 sqlParameter[2] = new SqlParameter("@titleEmployee", SqlDbType.Int);
                 sqlParameter[2].Value = employee.titleEmployee;

                 sqlParameter[3] = new SqlParameter("@genderEmployee", SqlDbType.Int);
                 sqlParameter[3].Value = employee.genderEmployee;

                 sqlParameter[4] = new SqlParameter("@midNameEmployee", SqlDbType.NVarChar);
                 sqlParameter[4].Value = employee.midNameEmployee;

                 sqlParameter[5] = new SqlParameter("@lastNameEmployee", SqlDbType.NVarChar);
                 sqlParameter[5].Value = employee.lastNameEmployee;

                 sqlParameter[6] = new SqlParameter("@maidenEmployee", SqlDbType.NVarChar);
                 sqlParameter[6].Value = employee.maidenEmployee;

                 sqlParameter[7] = new SqlParameter("@addressEmployee", SqlDbType.NVarChar);
                 sqlParameter[7].Value = employee.addressEmployee;

                 sqlParameter[8] = new SqlParameter("@houseNumberEmployee", SqlDbType.NVarChar);
                 sqlParameter[8].Value = employee.houseNumberEmployee;

                 sqlParameter[9] = new SqlParameter("@extensionEmployee", SqlDbType.NVarChar);
                 sqlParameter[9].Value = employee.extensionEmployee;

                 sqlParameter[10] = new SqlParameter("@zipCodeEmployee", SqlDbType.NVarChar);
                 sqlParameter[10].Value = employee.zipCodeEmployee;

                 sqlParameter[11] = new SqlParameter("@cityEmployee", SqlDbType.NVarChar);
                 sqlParameter[11].Value = employee.cityEmployee;

                 sqlParameter[12] = new SqlParameter("@dtBirthDateEmployee", SqlDbType.DateTime);
                 sqlParameter[12].Value = (employee.dtBirthDateEmployee == null || employee.dtBirthDateEmployee == DateTime.MinValue) ? SqlDateTime.Null : employee.dtBirthDateEmployee;

                 sqlParameter[13] = new SqlParameter("@isMariedEmployee", SqlDbType.Bit);
                 sqlParameter[13].Value = employee.isMariedEmployee;

                 sqlParameter[14] = new SqlParameter("@isentBsnEmploee", SqlDbType.NVarChar);
                 sqlParameter[14].Value = employee.isentBsnEmploee;

                 sqlParameter[15] = new SqlParameter("@bicEmployee", SqlDbType.NVarChar);
                 sqlParameter[15].Value = employee.bicEmployee;

                 sqlParameter[16] = new SqlParameter("@ibanEmployee", SqlDbType.NVarChar);
                 sqlParameter[16].Value = employee.ibanEmployee;

                 sqlParameter[17] = new SqlParameter("@emergencyPersonEmployee", SqlDbType.NVarChar);
                 sqlParameter[17].Value = employee.emergencyPersonEmployee;

                 sqlParameter[18] = new SqlParameter("@emergencyTelEmployee", SqlDbType.NVarChar);
                 sqlParameter[18].Value = employee.emergencyTelEmployee;

                 sqlParameter[19] = new SqlParameter("@dtHireDateEmployee", SqlDbType.Date);
                 sqlParameter[19].Value = employee.dtHireDateEmployee;

                 sqlParameter[20] = new SqlParameter("@contractNumberEmployee", SqlDbType.NVarChar);
                 sqlParameter[20].Value = employee.contractNumberEmployee;

                 sqlParameter[21] = new SqlParameter("@Department", SqlDbType.Int);
                 sqlParameter[21].Value = employee.Department;

                 sqlParameter[22] = new SqlParameter("@WishFunction", SqlDbType.Int);
                 sqlParameter[22].Value = employee.WishFunction;

                 sqlParameter[23] = new SqlParameter("@statusEmployee", SqlDbType.Int);
                 sqlParameter[23].Value = employee.statusEmployee;

                 sqlParameter[24] = new SqlParameter("@imageEmployee", SqlDbType.NVarChar);
                 sqlParameter[24].Value = employee.imageEmployee;

                 sqlParameter[25] = new SqlParameter("@isAplicationUser", SqlDbType.Bit);
                 sqlParameter[25].Value = employee.isAplicationUser;

                 sqlParameter[26] = new SqlParameter("@Function", SqlDbType.Int);
                 sqlParameter[26].Value = employee.Function;


                 _query.Add(query);
                 sqlParameters.Add(sqlParameter);


                 query = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId,tableName,description)
                                    VALUES(@textForm,@idUser,@dtLog,@typeChange,@id,@nameId,@tableName,@description)");

                 sqlParameter = new SqlParameter[8];


                 sqlParameter[0] = new SqlParameter("@textForm", SqlDbType.NVarChar);
                 sqlParameter[0].Value = nameForm;

                 sqlParameter[1] = new SqlParameter("@idUser", SqlDbType.Int);
                 sqlParameter[1].Value = idUser;

                 sqlParameter[2] = new SqlParameter("@dtLog", SqlDbType.DateTime);
                 sqlParameter[2].Value = DateTime.Now;

                 sqlParameter[3] = new SqlParameter("@typeChange", SqlDbType.NVarChar);
                 sqlParameter[3].Value = "I";

                 sqlParameter[4] = new SqlParameter("@id", SqlDbType.NVarChar);
                 sqlParameter[4].Value = conn.GetLastTableID("Employees")+1;

                 sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
                 sqlParameter[5].Value = "idEmployee";

                 sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
                 sqlParameter[6].Value = "Employees";

                 sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                 sqlParameter[7].Value = "Save employee";

                 _query.Add(query);
                 sqlParameters.Add(sqlParameter);


                 return conn.executQueryTransaction(_query, sqlParameters);
             }
             public bool Update(EmployeeModel employee, string nameForm, int idUser)
             {
                 List<string> _query = new List<string>();
                 List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

                 string query = string.Format(@"UPDATE  
                    Employees SET firstNameEmployee=@firstNameEmployee, initialsEmployee=@initialsEmployee,titleEmployee=@titleEmployee,
                    genderEmployee=@genderEmployee, midNameEmployee=@midNameEmployee, lastNameEmployee=@lastNameEmployee, maidenEmployee=@maidenEmployee,
                    addressEmployee=@addressEmployee, houseNumberEmployee=@houseNumberEmployee, extensionEmployee=@extensionEmployee,
                    zipCodeEmployee=@zipCodeEmployee, cityEmployee=@cityEmployee, dtBirthDateEmployee=@dtBirthDateEmployee,
                    isMariedEmployee=@isMariedEmployee, isentBsnEmploee=@isentBsnEmploee,bicEmployee=@bicEmployee,ibanEmployee=@ibanEmployee,
                    emergencyPersonEmployee=@emergencyPersonEmployee,emergencyTelEmployee=@emergencyTelEmployee, imageEmployee=@imageEmployee,
                    dtHireDateEmployee=@dtHireDateEmployee,contractNumberEmployee=@contractNumberEmployee,Department=@Department,[Function]=@Function,
                    WishFunction=@WishFunction,statusEmployee=@statusEmployee, isAplicationUser = @isAplicationUser
                     WHERE idEmployee = @idEmployee "); 
                
                 //,
                 //   
                 //   ,
                 //  ,dtHireDateEmployee=@dtHireDateEmployee,
                 //   contractNumberEmployee=@contractNumberEmployee,Department=@Department,WishFunction=@WishFunction,statusEmployee=@statusEmployee,
                 //  ,isAplicationUser=@isAplicationUser



                 SqlParameter[] sqlParameter = new SqlParameter[28];

                 sqlParameter[0] = new SqlParameter("@firstNameEmployee", SqlDbType.NVarChar);
                 sqlParameter[0].Value = employee.firstNameEmployee;

                 sqlParameter[1] = new SqlParameter("@initialsEmployee", SqlDbType.NVarChar);
                 sqlParameter[1].Value = employee.initialsEmployee;

                 sqlParameter[2] = new SqlParameter("@titleEmployee", SqlDbType.Int);
                 sqlParameter[2].Value = employee.titleEmployee;

                 sqlParameter[3] = new SqlParameter("@genderEmployee", SqlDbType.Int);
                 sqlParameter[3].Value = employee.genderEmployee;

                 sqlParameter[4] = new SqlParameter("@midNameEmployee", SqlDbType.NVarChar);
                 sqlParameter[4].Value = employee.midNameEmployee;

                 sqlParameter[5] = new SqlParameter("@lastNameEmployee", SqlDbType.NVarChar);
                 sqlParameter[5].Value = employee.lastNameEmployee;

                 sqlParameter[6] = new SqlParameter("@maidenEmployee", SqlDbType.NVarChar);
                 sqlParameter[6].Value = employee.maidenEmployee;

                 sqlParameter[7] = new SqlParameter("@addressEmployee", SqlDbType.NVarChar);
                 sqlParameter[7].Value = employee.addressEmployee;

                 sqlParameter[8] = new SqlParameter("@houseNumberEmployee", SqlDbType.NVarChar);
                 sqlParameter[8].Value = employee.houseNumberEmployee;

                 sqlParameter[9] = new SqlParameter("@extensionEmployee", SqlDbType.NVarChar);
                 sqlParameter[9].Value = employee.extensionEmployee;

                 sqlParameter[10] = new SqlParameter("@zipCodeEmployee", SqlDbType.NVarChar);
                 sqlParameter[10].Value = employee.zipCodeEmployee;

                 sqlParameter[11] = new SqlParameter("@cityEmployee", SqlDbType.NVarChar);
                 sqlParameter[11].Value = employee.cityEmployee;

                 sqlParameter[12] = new SqlParameter("@dtBirthDateEmployee", SqlDbType.DateTime);
                 sqlParameter[12].Value = (employee.dtBirthDateEmployee == null || employee.dtBirthDateEmployee == DateTime.MinValue) ? SqlDateTime.Null : employee.dtBirthDateEmployee;

                 sqlParameter[13] = new SqlParameter("@isMariedEmployee", SqlDbType.Bit);
                 sqlParameter[13].Value = employee.isMariedEmployee;

                 sqlParameter[14] = new SqlParameter("@isentBsnEmploee", SqlDbType.NVarChar);
                 sqlParameter[14].Value = employee.isentBsnEmploee;

                 sqlParameter[15] = new SqlParameter("@bicEmployee", SqlDbType.NVarChar);
                 sqlParameter[15].Value = employee.bicEmployee;

                 sqlParameter[16] = new SqlParameter("@ibanEmployee", SqlDbType.NVarChar);
                 sqlParameter[16].Value = employee.ibanEmployee;

                 sqlParameter[17] = new SqlParameter("@emergencyPersonEmployee", SqlDbType.NVarChar);
                 sqlParameter[17].Value = employee.emergencyPersonEmployee;

                 sqlParameter[18] = new SqlParameter("@emergencyTelEmployee", SqlDbType.NVarChar);
                 sqlParameter[18].Value = employee.emergencyTelEmployee;

                 sqlParameter[19] = new SqlParameter("@imageEmployee", SqlDbType.NVarChar);
                 sqlParameter[19].Value = (employee.imageEmployee == null) ? SqlString.Null : employee.imageEmployee;


                 sqlParameter[20] = new SqlParameter("@dtHireDateEmployee", SqlDbType.DateTime);
                 sqlParameter[20].Value = (employee.dtHireDateEmployee == null || employee.dtHireDateEmployee == DateTime.MinValue) ? SqlDateTime.Null : employee.dtHireDateEmployee;

                 sqlParameter[21] = new SqlParameter("@contractNumberEmployee", SqlDbType.NVarChar);
                 sqlParameter[21].Value = employee.contractNumberEmployee;

                 sqlParameter[22] = new SqlParameter("@Department", SqlDbType.Int);
                 sqlParameter[22].Value = employee.Department;

                 sqlParameter[23] = new SqlParameter("@Function", SqlDbType.Int);
                 sqlParameter[23].Value = employee.Function;

                 sqlParameter[24] = new SqlParameter("@WishFunction", SqlDbType.Int);
                 sqlParameter[24].Value = employee.WishFunction;

                 sqlParameter[25] = new SqlParameter("@statusEmployee", SqlDbType.Int);
                 sqlParameter[25].Value = employee.statusEmployee;

                 sqlParameter[26] = new SqlParameter("@isAplicationUser", SqlDbType.Bit);
                 sqlParameter[26].Value = employee.isAplicationUser;

                 sqlParameter[27] = new SqlParameter("@idEmployee", SqlDbType.Int);
                 sqlParameter[27].Value = employee.idEmployee;

                 _query.Add(query);
                 sqlParameters.Add(sqlParameter);


                 query = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId,tableName,description)
                                    VALUES(@textForm,@idUser,@dtLog,@typeChange,@id,@nameId,@tableName,@description)");

                 sqlParameter = new SqlParameter[8];


                 sqlParameter[0] = new SqlParameter("@textForm", SqlDbType.NVarChar);
                 sqlParameter[0].Value = nameForm;

                 sqlParameter[1] = new SqlParameter("@idUser", SqlDbType.Int);
                 sqlParameter[1].Value = idUser;

                 sqlParameter[2] = new SqlParameter("@dtLog", SqlDbType.DateTime);
                 sqlParameter[2].Value = DateTime.Now;

                 sqlParameter[3] = new SqlParameter("@typeChange", SqlDbType.NVarChar);
                 sqlParameter[3].Value = "U";

                 sqlParameter[4] = new SqlParameter("@id", SqlDbType.NVarChar);
                 sqlParameter[4].Value = employee.idEmployee;

                 sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
                 sqlParameter[5].Value = "idEmployee";

                 sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
                 sqlParameter[6].Value = "Employees";

                 sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                 sqlParameter[7].Value = "Update employee";

                 _query.Add(query);
                 sqlParameters.Add(sqlParameter);


                 return conn.executQueryTransaction(_query, sqlParameters);
             }

        /// BRISANJE 
             public Boolean DeleteEmployee(int idEmployee, string nameForm, int idUser)
             {
                 List<string> _query = new List<string>();
                 List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

                 string query = string.Format(@"DELETE FROM Employees WHERE idEmployee = @idEmployee ");

                 SqlParameter[] sqlParameter = new SqlParameter[1];
                 sqlParameter[0] = new SqlParameter("@idEmployee", SqlDbType.Int);
                 sqlParameter[0].Value = idEmployee;

                 _query.Add(query);
                 sqlParameters.Add(sqlParameter);



                 query = string.Format(@"DELETE FROM EmployeeEmail WHERE idEmployee = @idEmployee ");

                 _query.Add(query);
                 sqlParameters.Add(sqlParameter);

                 query = string.Format(@"DELETE FROM EmployeeFilter WHERE idContPers = @idEmployee ");


                 _query.Add(query);
                 sqlParameters.Add(sqlParameter);

                 query = string.Format(@"DELETE FROM EmployeeLabel WHERE idContPers = @idEmployee ");


                 _query.Add(query);
                 sqlParameters.Add(sqlParameter);

                 query = string.Format(@"DELETE FROM EmployeeNote WHERE idEmployee = @idEmployee ");


                 _query.Add(query);
                 sqlParameters.Add(sqlParameter);

                 query = string.Format(@"DELETE FROM EmployeePassport WHERE idEmployee = @idEmployee ");


                 _query.Add(query);
                 sqlParameters.Add(sqlParameter);

                 query = string.Format(@"DELETE FROM Users WHERE idEmployee = @idEmployee ");

                 _query.Add(query);
                 sqlParameters.Add(sqlParameter);


                 query = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId,tableName,description)
                                    VALUES(@textForm,@idUser,@dtLog,@typeChange,@id,@nameId,@tableName,@description)");

                 sqlParameter = new SqlParameter[8];


                 sqlParameter[0] = new SqlParameter("@textForm", SqlDbType.NVarChar);
                 sqlParameter[0].Value = nameForm;

                 sqlParameter[1] = new SqlParameter("@idUser", SqlDbType.Int);
                 sqlParameter[1].Value = idUser;

                 sqlParameter[2] = new SqlParameter("@dtLog", SqlDbType.DateTime);
                 sqlParameter[2].Value = DateTime.Now;

                 sqlParameter[3] = new SqlParameter("@typeChange", SqlDbType.NVarChar);
                 sqlParameter[3].Value = "D";

                 sqlParameter[4] = new SqlParameter("@id", SqlDbType.NVarChar);
                 sqlParameter[4].Value = idEmployee;

                 sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
                 sqlParameter[5].Value = "idEmployee";

                 sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
                 sqlParameter[6].Value = "Employees";

                 sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                 sqlParameter[7].Value = "Delete employee";

                 _query.Add(query);
                 sqlParameters.Add(sqlParameter);


                 return conn.executQueryTransaction(_query, sqlParameters);

             }
    }


}

