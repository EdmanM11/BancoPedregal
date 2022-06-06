using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaBancario.Reportes
{
    public partial class ReporteHistorial : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int codcuenta = 0;
            codcuenta = Convert.ToInt32(Request.QueryString["codcuenta"]);
            txtcodigo.Text = codcuenta.ToString();
        }
    }
}