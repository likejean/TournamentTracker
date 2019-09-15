using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.DataAccess;
using System.Configuration;

namespace TrackerLibrary
{
    public static class GlobalConfig
    {
        /// <summary>
        /// Create an instance (obj): list of items of specific type: IDataConnection Contracts
        /// </summary>
        ///The Read-Write Property of IDataConnection type
        public static IDataConnection Connection { get; private set; }
        ///
        public static void InitializeConnections(DatabaseType db)
        {            
            if (db == DatabaseType.Sql)
            {
                //TODO - Set up the SQL Connection properly
                //sql & Connection are the properties of the same type: 'IDataConnection'
                SqlConnector sql = new SqlConnector();
                Connection = sql;

            }
            else if (db == DatabaseType.TextFile)
            {
                //TODO - Create the Text Connection
                //text & Connection are the properties of the same type: 'IDataConnection'
                TextConnector text = new TextConnector();
                Connection = text;
            }
        }
        //Get the connection string in order to link my local SQL database
        public static string CnnString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
