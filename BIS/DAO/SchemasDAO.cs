using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BIS.Core;
using System.Data.SqlTypes;
using BIS.Model;


namespace BIS.DAO
{
    public class SchemasDAO
    {

        private dbConnection conn;

        public SchemasDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetClientSchema()
        {
            string query = string.Format(@"
                SELECT * 
                FROM Client
                WHERE 1 = 2");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetPersonSchema()
        {
            string query = string.Format(@"
                SELECT * 
                FROM ContactPerson
                WHERE 1 = 2");

            return conn.executeSelectQuery(query, null);
        }

//        public DataTable GetPersonSchemaPart1()
//        {
//            string query = string.Format(@"
//                SELECT cp.idContPers,initialsContPers,firstname,midname,lastname,maidenname,'' as nameTitle,'' as nameGender,birthdate,dtCreated,
//                '' as nameUserCreated,dtModified,idUserModified,idUserResponsible,isMaried,isActive,identBSN, '' as Address, '' as Tel, '' as Email
//                FROM ContactPerson cp
//                WHERE 1=2");

//            return conn.executeSelectQuery(query, null);
//        }

//        public DataTable GetPersonSchemaPart2()
//        {
//            //za prevode
//            string query = string.Format(@"
//               SELECT '' as [ID person],'' as [Initials] ,'' as [First name], '' as [Middle name],'' as [Last name],'' as [Maiden name],'' as Title,'' as Gender,'' as [Birth date],'' as [Creation date],
//                '' as [Creator user],'' as [Modification date],'' as [Modified user],'' as [Responsibile person],'' as [Married],'' as [Active],IdentBSN, '' as Address, '' as Telephone, '' as Email
//                FROM ContactPerson cp      
//                 WHERE 1=2");

//            return conn.executeSelectQuery(query, null);
//        }
        public DataTable GetPersonSchemaPart1()
        {
            string query = string.Format(@"
                SELECT initialsContPers,firstname,midname,lastname,maidenname,'' as nameTitle,'' as nameGender,birthdate,
                identBSN, '' as Address, '' as Tel, '' as Email
                FROM ContactPerson cp
                WHERE 1=2");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetPersonSchemaPart2()
        {
            //za prevode
            string query = string.Format(@"
               SELECT '' as [Initials] ,'' as [First name], '' as [Middle name],'' as [Last name],'' as [Maiden name],'' as Title,'' as Gender,'' as [Birth date],
                IdentBSN, '' as Address, '' as Telephone, '' as Email
                FROM ContactPerson cp      
                 WHERE 1=2");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetClientSchemaPart1()
        {
            string query = string.Format(@"
                SELECT accountCodeClient, nameClient,  zipCodeClient, cityClient, '' as nameCountry,
                visitAddressClient, visitZipCodeClient, visitCityClient,  webClient, '' as nameTypeClient,
                '' as Address, '' as Tel, '' as Email                
                FROM Client c
                WHERE 1=2");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetClientSchemaPart2()
        {
            string query = string.Format(@"
                SELECT '' as [Code Client], '' as [Name],  '' as [Zip code], '' as [City], '' as [Country],
                '' as [Visiting address], '' as [Visiting post code], '' as [Visiting city],  '' as [Web], '' as [Type],
                '' as Address, '' as Telephone, '' as Email                 
                FROM Client c
                WHERE 1=2");

            return conn.executeSelectQuery(query, null);
        }
       
                


        public DataTable GetPersonSchemaJoined()
        {
            string query = string.Format(@"
               SELECT cp.idContPers,initialsContPers,firstname,midname,lastname,maidenname,cp.idTitle,t.nameTitle,cp.idGender,birthdate,dtCreated,
                idUserCreated,dtModified,idUserModified,idUserResponsible,isMaried,isActive,isDied,dtOfDeath,isNeedProspect,
                isNeedMail,identBSN,isPayInvoice,isSharePicture
                FROM ContactPerson cp
                LEFT JOIN Gender g ON g.idGender = cp.idGender
                LEFT JOIN Title t ON t.idTitle = cp.idTitle                
                INNER JOIN ContactpersonFilter cpf ON cp.idContPers = cpf.idContPers
                WHERE 1=2");

            return conn.executeSelectQuery(query, null);
        }
    }

     
}
