using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace CDRFiveCrownsHRWeb
{
    public partial class _default : System.Web.UI.Page
    {
        private string connectionString;
        private dataServer theServer;
        private DataSet dsEmployee;

        protected void Page_Load(object sender, EventArgs e)
        {
            connectionString = "Data Source=claytonr1.db.5867809.hostedresource.com;Persist Security Info=True;User ID=claytonr1;Password=Interface1";
            
            theServer = new dataServer(connectionString);

            dsEmployee=theServer.runSQLDataSet("select * from Employees", "Employees");
            this.GridView1.DataSource = dsEmployee;
            GridView1.DataBind();
            this.Label1.Text = "Success";
        }
    }
}