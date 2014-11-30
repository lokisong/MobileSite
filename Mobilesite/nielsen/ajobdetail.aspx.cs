using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mobile.Common;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using Aqayo;
using System.Dynamic;
using Newtonsoft.Json.Linq;
using System.Configuration;


public partial class ajobdetail : System.Web.UI.Page
{
    public string jobNameShow = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            jobSearch(Request["jobid"] == null ? "0" : Request["jobid"].ToString());
        }
    }

    public void jobSearch(string jobId)
    {
        

        AqayoUtility au = new AqayoUtility();
        

        try
        {

            var temp2 = JObject.Parse(au.getJobDetail(jobId));


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

            string jobName = temp2["title"].ToString();
            string jobDescritpion = temp2["description"].ToString();
            string jobCountry = string.Empty;
            string jobYear = string.Empty;
            string jobFunction = string.Empty;
            //get column name test
            foreach (var item in temp2)
            {
                if (item.Key.ToString().StartsWith("country_") && item.Value.ToString().Length>1)
                {
                    jobCountry = item.Value.ToString();
                }
                if(item.Key.ToString().StartsWith("yearsofexperien_") && item.Value.ToString()!="null")
                {
                    jobYear = item.Value.ToString();
                }
                if (item.Key.ToString().StartsWith("function_") && item.Value.ToString() != "null")
                {
                    jobFunction = item.Value.ToString();
                }

            }
            jobTitle.InnerHtml = jobName;
            jobLocation.InnerHtml += jobCountry;
            jobVertical.InnerHtml += jobFunction;
            jobDes.InnerHtml = jobDescritpion;
           
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
           
        }

    }

    protected void applyBtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("ajobapply.aspx?jobid=" + Request["jobid"].ToString()+"&jobname=" + jobTitle.InnerHtml.ToString());
    }
    protected void backBtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("ajoblist.aspx");
    }
}