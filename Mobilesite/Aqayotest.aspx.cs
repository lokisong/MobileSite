using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aqayo;
using Mobile.Common;
using System.Dynamic;
using Newtonsoft.Json.Linq;
using System.Configuration;

public partial class Aqayotest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        AqayoUtility au = new AqayoUtility();
        Response.Write("access:"+au.getAccess());

        joblist sr = au.getJobsObj("0");
        Response.Write("start:" + sr.columns[0]);

        //JObject temp = JObject.Parse(@"{""id"":52,""accesskey"":""DA04DFA92C954D535EB0DB3E89F5B19D.server1-1262033722"",""username"":""jobboaruser"",""accesstime"":""2014-11-17""}");
        //Response.Write(temp["accesskey"]);

        var temp2 = JObject.Parse(au.getJobs("0",""));
        Response.Write(temp2);

        string positionNanme = ConfigurationManager.AppSettings["position"];
        string functionNanme = ConfigurationManager.AppSettings["function"];
        string experienceNanme = ConfigurationManager.AppSettings["experience"];
        string employmentNanme = ConfigurationManager.AppSettings["employment"];
        string countryNanme = ConfigurationManager.AppSettings["country"];

        string positionCode = string.Empty;
        string functionCode = string.Empty;
        string experienceCode = string.Empty;
        string employmentCode = string.Empty;
        string countryCode = string.Empty;
        int totalRecodes = 0;
        int perPage = 0;

        //get Postion column code
        var s =
              from p in temp2["columns"]
              where p["fieldname"].ToString() == positionNanme
              select p["column"];        
        foreach (var item in s)
        {
            Response.Write(item+"<br/>");
            positionCode = item.ToString();
        }

        //get Function column code
        s =
              from p in temp2["columns"]
              where p["fieldname"].ToString() == functionNanme
              select p["column"];
        foreach (var item in s)
        {
            Response.Write(item + "<br/>");
            functionCode = item.ToString();
        }

        //get Years of experience column code
        s =
              from p in temp2["columns"]
              where p["fieldname"].ToString() == experienceNanme
              select p["column"];
        foreach (var item in s)
        {
            Response.Write(item + "<br/>");
            experienceCode = item.ToString();
        }

        //get Employment Type column code
        s =
              from p in temp2["columns"]
              where p["fieldname"].ToString() == employmentNanme
              select p["column"];
        foreach (var item in s)
        {
            Response.Write(item + "<br/>");
            employmentCode = item.ToString();
        }

        //get Country column code
        s =
              from p in temp2["columns"]
              where p["fieldname"].ToString() == countryNanme
              select p["column"];
        foreach (var item in s)
        {
            Response.Write(item + "<br/>");
            countryCode = item.ToString();
        }

        //get Total Records
        
        if(ViewState["totalrecords"]==null)
        {
            Int32.TryParse(temp2["totalrecords"].ToString(), out totalRecodes);
            ViewState["totalrecords"]=totalRecodes;
        }
        Response.Write(ViewState["totalrecords"] + "<br/>");

        //get Records in Perpage
        
        if(ViewState["recordsperpage"]==null)
        {
            Int32.TryParse(temp2["recordsperpage"].ToString(), out perPage);
            ViewState["recordsperpage"]=perPage;
        }
        Response.Write(ViewState["recordsperpage"] + "<br/>");

       


        var v =
      from p in temp2["data"]
      select p;
        foreach (var item in v)
        {
            Response.Write("<p>" + item[positionCode] + "<br/>" + item[functionCode] + "<br/>" + item[countryCode] + "<br/>" + item[experienceCode] + "<br/></p>");
        }

        temp2 = JObject.Parse("{\"root\": " + au.getCandidateFields() + "}");
        var o =
             from p in temp2["root"]
             where p["displayedName"].ToString() == "Email"
             select p["fieldName"];
        foreach (var item in o)
        {
            Response.Write(item);
        }


        Response.End();

    }
}