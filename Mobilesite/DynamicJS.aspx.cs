using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Styles_DynamicJS : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string abc=@"$(document).ready(function () {
        $(""div"").after(""<span>hello</span>"")
        });";
        Response.Write(abc);
    }
}