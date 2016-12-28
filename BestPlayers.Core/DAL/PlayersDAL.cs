using BestPlayers.Core.Models;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BestPlayers.Core.DAL
{
    public class PlayersDAL
    {
        Database objDB;

        public List<T> ConvertTo<T>(DataTable datatable) where T : new()
        {
            List<T> Temp = new List<T>();

            try
            {
                List<string> columnsNames = new List<string>();
                foreach (DataColumn itemDataColumn in datatable.Columns)
                {
                    columnsNames.Add(itemDataColumn.ColumnName);
                }

                Temp = datatable.AsEnumerable().ToList().ConvertAll<T>(row => getObject<T>(row, columnsNames));

                return Temp;

            }
            catch
            {
                return Temp;
            }
        }


        public T getObject<T>(DataRow row, List<string> columnsName) where T : new()
        {
            T obj = new T();

            try
            {
                var columnsname = string.Empty;
                var value = string.Empty;
                PropertyInfo[] Properties;
                Properties = typeof(T).GetProperties();


                foreach (var itemPropertyInfo in Properties)
                {
                    columnsname = columnsName.Find(x => x.ToLower() == itemPropertyInfo.Name.ToLower());

                    if (!string.IsNullOrEmpty(columnsname))
                    {
                        value = row[columnsname].ToString();
                        if (!string.IsNullOrEmpty(value))
                        {
                            if (Nullable.GetUnderlyingType(itemPropertyInfo.PropertyType) != null)
                            {
                                value = row[columnsname].ToString().Replace("$", "").Replace(",", "");
                                itemPropertyInfo.SetValue(obj, Convert.ChangeType(value, Type.GetType(Nullable.GetUnderlyingType(itemPropertyInfo.PropertyType).ToString())), null);
                            }
                            else
                            {
                                value = row[columnsname].ToString();
                                itemPropertyInfo.SetValue(obj, Convert.ChangeType(value, Type.GetType(itemPropertyInfo.PropertyType.ToString())), null);
                            }
                        }
                    }
                }
                return obj;
            }
            catch
            {
                return obj;
            }
        }


        /// <summary>  
        /// This method is created to get all teams.  
        /// </summary>  
        /// <returns></returns>  
        public List<Team> GetAllTeams()
        {
            List<Team> teams = new List<Team>();
            objDB = new SqlDatabase(ConfigurationManager.ConnectionStrings["PlayersConfiguration"].ConnectionString);

            using (DbCommand objCMD = objDB.GetStoredProcCommand("BP_GetAllTeams"))
            {
                try
                {
                    using (DataTable dataTable = objDB.ExecuteDataSet(objCMD).Tables[0])
                    {
                        teams = ConvertTo<Team>(dataTable);
                    }
                }
                catch (Exception ex)
                {
                    bool rethrow = false;
                    if (rethrow)
                    {
                        throw ex;
                    }
                    return null;
                }
            }
            return teams;
        }


        /// <summary>  
        /// This method is created to get players list on the basis of team id.  
        /// </summary>  
        /// <param name="TeamId"></param>  
        /// <param name="page"></param>  
        /// <param name="pageSize"></param>  
        /// <returns></returns>  
        public List<Players> GetPlayersByTeam(int TeamId, string page, string pageSize)
        {
            List<Players> players = null;
            objDB = new SqlDatabase(ConfigurationManager.ConnectionStrings["PlayersConfiguration"].ConnectionString);

            using (DbCommand objCMD = objDB.GetStoredProcCommand("BP_GetPlayersByTeam"))
            {
                try
                {
                    objDB.AddInParameter(objCMD, "@TeamId", DbType.Int32, TeamId);
                    objDB.AddInParameter(objCMD, "@PageNumber", DbType.Int32, Convert.ToInt32(page));
                    objDB.AddInParameter(objCMD, "@PageSize", DbType.Int32, Convert.ToInt32(pageSize));

                    using (DataTable dataTable = objDB.ExecuteDataSet(objCMD).Tables[0])
                    {
                        players = ConvertTo<Players>(dataTable);
                    }
                }
                catch (Exception ex)
                {
                    bool rethrow = false;
                    if (rethrow)
                    {
                        throw ex;
                    }
                    return null;
                }
            }
            return players;
        }
    }
}
