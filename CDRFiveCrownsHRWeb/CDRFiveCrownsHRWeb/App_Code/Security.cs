using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDRFiveCrownsHRWeb
{
    public class Security
    {
        private string connectionString;
        private string storedProcedureName;
        private List<int> returnOutputList;
        private string userName;
        private string password;
        private bool validateUser;
        private dataServer theServer = new dataServer();


        public string UserName
        {
            set
            {
                userName = value;
            }
        }

        public string Password
        {
            set
            {
                password = value;
            }
        }

        public bool LoginUser()
        {
            connectionString = "Data Source=claytonr1.db.5867809.hostedresource.com;Persist Security Info=True;User ID=claytonr1;Password=Interface1";
            storedProcedureName = "fcspLogin";
            // implement Server lib




            validateUser = Convert.ToBoolean(returnOutputList[0]);

            return validateUser;
        }
    }
}