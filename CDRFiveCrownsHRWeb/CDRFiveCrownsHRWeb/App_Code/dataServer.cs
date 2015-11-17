using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Xml;
using System.Data;
using System.Collections;


namespace CDRFiveCrownsHRWeb
{
    public class dataServer
    {
        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataReader dataReader;
        private XmlReader xReader;
        private SqlDataAdapter dataAdapter;
        private DataSet dataSet;
        private ArrayList parameterList = new ArrayList();

        private string connectionString;
        private string server;
        private string database;
        private string user;
        private string password;

        private string moduleName;
        private bool disposed;
        private const string exceptionMessage = "Data Application Error!\n  Detailed error information can be found in the Application Log.";

        public enum SQLDataType
        {
            SQLString,
            SQLChar,
            SQLInteger,
            SQLBit,
            SQLDateTime,
            SQLDecimal,
            SQLMoney,
            SQLImage
        }

        public dataServer()
        {
            moduleName = this.GetType().ToString();
        }

        public dataServer(string inConnectionString)
            : base()
        {
            connectionString = inConnectionString;
        }

        public dataServer(string inServer, string inDatabase, string inUserName, string inPassword)
            : base()
        {
            connectionString = string.Format("Server={0};Database={1};UserID={2};Password={3};", inServer, inDatabase, inUserName, inPassword);
        }

        public string ConnectionString
        {
            get
            {
                try
                {
                    return connection.ConnectionString;
                }
                catch
                {
                    return "";
                }
            }
            set
            {
                connectionString = value;
            }
        }

        public DataSet runSQLDataSet(string inSQL, string inTableName)
        {
            try
            {
                connection = new SqlConnection(connectionString);
                command = new SqlCommand(inSQL, connection);
                dataAdapter = new SqlDataAdapter(command);
                dataSet = new DataSet();

                if (inTableName == null)
                {
                    dataAdapter.Fill(dataSet);
                }
                else
                {
                    dataAdapter.Fill(dataSet, inTableName);
                }

                connection.Close();
                return dataSet;
            }
            catch (Exception ExceptionObject)
            {
                throw new Exception(exceptionMessage, ExceptionObject);
            }
        }

        public void AddParameter(string inParameterName, string inValue, SQLDataType inSQLType, int inSize, ParameterDirection inDirection = ParameterDirection.Input)
        {
            SqlDbType buildDataType;
            Parameter buildParameter;

            switch (inSQLType)
            {
                case SQLDataType.SQLString:
                    buildDataType = SqlDbType.VarChar;
                    break;
                case SQLDataType.SQLChar:
                    buildDataType = SqlDbType.Char;
                    break;
                case SQLDataType.SQLInteger:
                    buildDataType = SqlDbType.Int;
                    break;
                case SQLDataType.SQLBit:
                    buildDataType = SqlDbType.Bit;
                    break;
                case SQLDataType.SQLDateTime:
                    buildDataType = SqlDbType.DateTime;
                    break;
                case SQLDataType.SQLDecimal:
                    buildDataType = SqlDbType.Decimal;
                    break;
                case SQLDataType.SQLMoney:
                    buildDataType = SqlDbType.Money;
                    break;
                case SQLDataType.SQLImage:
                    buildDataType = SqlDbType.Image;
                    break;
            }
            buildParameter = new Parameter(inParameterName, inValue, inSQLType, inSize, inDirection);
        }

        public class Parameter
        {
            public string ParameterName;
            public string ParameterValue;
            public dataServer.SQLDataType ParameterDataType;
            public int ParameterSize;
            public ParameterDirection ParameterDirectionUsed;

            public Parameter(string inParameterName, string inValue, dataServer.SQLDataType inDataType, int inSize, ParameterDirection inDirection = ParameterDirection.Input)
            {
                ParameterName = inParameterName;
                ParameterValue = inValue;
                ParameterDataType = inDataType;
                ParameterSize = inSize;
                ParameterDirectionUsed = inDirection;
            }
        }
    }
}