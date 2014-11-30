using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Collections.Specialized;
using Mobile.Common;
using System.Runtime.Serialization.Json;
using System.Collections;
using System.IO;
using System.Text;
using Aqayo;
using System.Dynamic;
using Newtonsoft.Json.Linq;
using System.Configuration;

public partial class ajoblistaspx : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

            string keyword = null;
            string location = null;
            string vertical = null;
            int pageindex = 1;
            int size = 0;
            if (Request["keyword"] != null) 
            {
                keyword = Request["keyword"].ToString();
            }
            if (ViewState["keywords"] != null)
            {
                keyword = ViewState["keywords"].ToString();
            }
            if (Request["location"] != null)
            {
                location = Request["location"].ToString();
            }
            if (Request["vertical"] != null)
            {
                vertical = Request["vertical"].ToString();
            }
            if (Request["page"] != null)
            {
                pageindex = Convert.ToInt32(Request["page"]);
            }
            if (pageSize != null)
            {
                size = pageSize;
            }
            jobSearch(keyword,pageindex,size);
    }

   

    public void jobSearch(string keyword, int page, int size)
    {
        
        ViewState["keywords"] = keyword;
        AqayoUtility au = new AqayoUtility();
        int pageStart = 0;
        if (page > 1 && pageSize>0)
        {
            pageStart = (page-1) * pageSize;
        }


        try
        {

        var temp2 = JObject.Parse(au.getJobs(pageStart.ToString(),keyword));
        

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
                positionCode = item.ToString();
            }

            //get Function column code
            s =
                  from p in temp2["columns"]
                  where p["fieldname"].ToString() == functionNanme
                  select p["column"];
            foreach (var item in s)
            {
                functionCode = item.ToString();
            }

            //get Years of experience column code
            s =
                  from p in temp2["columns"]
                  where p["fieldname"].ToString() == experienceNanme
                  select p["column"];
            foreach (var item in s)
            {
                experienceCode = item.ToString();
            }

            //get Employment Type column code
            s =
                  from p in temp2["columns"]
                  where p["fieldname"].ToString() == employmentNanme
                  select p["column"];
            foreach (var item in s)
            {
                employmentCode = item.ToString();
            }

            //get Country column code
            s =
                  from p in temp2["columns"]
                  where p["fieldname"].ToString() == countryNanme
                  select p["column"];
            foreach (var item in s)
            {
                countryCode = item.ToString();
            }

            //get Total Records
            Int32.TryParse(temp2["totalrecords"].ToString(), out totalRecodes);

            //get Records in per page
            Int32.TryParse(temp2["recordsperpage"].ToString(), out perPage);
            pageSize = perPage;

            //compose Job View
            StringBuilder jobString = new StringBuilder();
            var v =
              from p in temp2["data"]
              select p;
            foreach (var item in v)
            {
                jobString.Append(@"<ul data-role=""listview"" data-inset=""true"" data-divider-theme=""b"">
					<li data-role=""list-divider"">" + item[positionCode] + @"</li>
					<li data-icon=""grid""><a href=""ajobdetail.aspx?jobid=" + item["externaluid"] + @"""> Location:" + item[countryCode] + ",Function:" + item[functionCode] + ",Working Exp:" + item[experienceCode] + @"...</a></li>
				</ul><article class=""""underline"""">");
            }
            hbJobContent.InnerHtml = jobString.ToString();

            //compse paging 
            int maxpage = Convert.ToInt32(Math.Ceiling((double)totalRecodes / (double)perPage));
            pageIndex.InnerHtml = page.ToString() + "/" + maxpage.ToString();
            if (page - 1 <= 1)
            {
                prePage.HRef = keyword == null ? "?page=1&size=" + pageSize : "?page=1&size=" + pageSize + "&keyword=" + keyword;
            }
            else
            {
                prePage.HRef = keyword == null ? "?page=" + (page - 1).ToString() + "&size=" + pageSize : "?page=" + (page - 1).ToString() + "&size=" + pageSize + "&keyword=" + keyword;
            }
            if (page + 1 >= maxpage)
            {
                nextPage.HRef = keyword == null ? "?page=" + maxpage.ToString() + "&size=" + pageSize : "?page=" + maxpage.ToString() + "&size=" + pageSize + "&keyword=" + keyword;
            }
            else
            {
                nextPage.HRef = keyword == null ? "?page=" + (page + 1).ToString() + "&size=" + pageSize : "?page=" + (page + 1).ToString() + "&size=" + pageSize + "&keyword=" + keyword;
            }
        }
        catch (Exception ex)
        {

             hbJobContent.InnerHtml = "No data";
        }
  
    }
    protected void searchBtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("ajoblist.aspx?keyword=" + SearchBox.Value);
    }
    protected void adSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect("ajobsearch.aspx");
    }

    public static int pageSize;
}