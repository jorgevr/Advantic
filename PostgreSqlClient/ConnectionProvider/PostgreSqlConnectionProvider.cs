using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Npgsql;
using System.Data;

namespace PostgreSqlClient.ConnectionProvider
{
    public class PostgreSqlConnectionProvider
    {
        private const String SELECT_TYPE = "SELECT";
        private const String INSERT_TYPE = "INSERT";
        private const String UPDATE_TYPE = "UPDATE";

        #region Public Virtual Properties

        public virtual String Host { get; set; }
        public virtual int Port { get; set; }
        public virtual String User { get; set; }
        public virtual String Password { get; set; }
        public virtual String Database { get; set; }

        public PostgreSqlConnectionProvider() 
        {
            Host = getpostgreSqlConnectionHost();
            Port = getpostgreSqlConnectionPort();
            User = getpostgreSqlConnectionUser();
            Password = getpostgreSqlConnectionPassword();
            Database = getpostgreSqlConnectionDatabase();
        }

        public PostgreSqlConnectionProvider(String host, int port, string user, string password, string database)
        {
            Host = host;
            Port = port;
            User = user;
            Password = password;
            Database = database;
        }

        #endregion

        #region private properties

        private static NpgsqlConnection _connection;

        #endregion


        #region Public Override Methods

        public override bool Equals(object obj)
        {
            PostgreSqlConnectionProvider c = obj as PostgreSqlConnectionProvider;
            return c != null
                && c.Host == Host;
        }

        public override int GetHashCode()
        {
            return Host.GetHashCode();
        }

        #endregion


        #region public methods

        public void InitializeConnection()
        {
            InitializeConnection(Host, Port, User, Password, Database);
        }

        public void InitializeConnection(string host, int port, string user, string password, string database)
        {
            if (IsInitialized())
                throw new InvalidOperationException("You cannot start more than one PostgreSql connection at same time");

            String connectionParameters = getConnectionParameters(host, port, user, password, database);
            _connection = new NpgsqlConnection(connectionParameters);
            _connection.Open();
        }

        public DataTable queryExecute(string query, String type)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter();
            if (type == SELECT_TYPE)
            {
                adapter.SelectCommand =new NpgsqlCommand(query, _connection);
                adapter.Fill(ds);
                return ds.Tables[0];
            }
            if (type == INSERT_TYPE)
            {
                adapter.InsertCommand=new NpgsqlCommand(query,_connection);
                adapter.InsertCommand.ExecuteNonQuery();
            }
            if (type == UPDATE_TYPE)
            {
                adapter.UpdateCommand = new NpgsqlCommand(query, _connection);
                adapter.UpdateCommand.ExecuteNonQuery();
            }
            return new DataTable();
 
        }

        public void EndConnection()
        {
            if (!IsInitialized())
                throw new InvalidOperationException("The connection is closed");
            _connection.Close();
        }


        
        #endregion

        #region private methods

        private static string getpostgreSqlConnectionHost()
        {
            return ConfigurationManager.AppSettings["PostgreSqlHost"];
        }

        private string getpostgreSqlConnectionUser()
        {
            return ConfigurationManager.AppSettings["PostgreSqlUser"];
        }

        private int getpostgreSqlConnectionPort()
        {
            return int.Parse(ConfigurationManager.AppSettings["PostgreSqlPort"]);
        }

        private string getpostgreSqlConnectionPassword()
        {
            return ConfigurationManager.AppSettings["PostgreSqlPassword"];
        }

        private string getpostgreSqlConnectionDatabase()
        {
            return ConfigurationManager.AppSettings["PostgreSqlDatabase"];
        }

        private string getConnectionParameters(string host, int port, string user, string password, string database)
        {
           return String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};",host, port, user,password, database);
        }

        private bool IsInitialized()
        {
            return _connection !=null;
        }


        #endregion




    }
}
