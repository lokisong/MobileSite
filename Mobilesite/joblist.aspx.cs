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

public partial class joblistaspx : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string keyword = null;
            string location = null;
            string vertical = null;
            int pageindex = 1;
            if (Request["keyword"] != null) 
            {
                keyword = Request["keyword"].ToString();
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
            int pagesize = 10;
            jobSearch(keyword,pageindex,pagesize,location,vertical);

        }
    }

    public void jobSearch(string keyword, int page, int pagesize,string location, string vertical)
    {
        hbJobContent.InnerHtml = "";
        PostSubmitter post = new PostSubmitter();
        post.Url = "https://fiat-chrysler.hiringboss.com/careersiteJobSearch.do";
        post.PostItems.Add("keyword", keyword);
        post.PostItems.Add("location",location);
        post.PostItems.Add("vertical", vertical);
        //post.PostItems.Add{ "empType", "" };
        //post.PostItems.Add{ "subdomain", "" };
        post.PostItems.Add("subdomain", "fcamobile");
        post.Type = PostSubmitter.PostTypeEnum.Post;
        string result = "";
        try
        {
            result = post.Post();
        }
        catch (Exception e)
        {
            hbJobContent.InnerHtml += result;
            hbJobContent.InnerHtml += @"No Data";
        }

        try
        {
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Job[]));
            Job[] job = (Job[])serializer.ReadObject(ms);
            int maxpage = Convert.ToInt32(Math.Ceiling((double)job.Length / (double)pagesize));
            pageIndex.InnerHtml = page.ToString() + "/" + (Math.Ceiling((double)job.Length / (double)pagesize)).ToString();
            if (page-1 <= 1)
            {
                prePage.HRef = keyword == null ? "?page=1" : "?page=1&keyword="+keyword;
            }
            else 
            {
                prePage.HRef = keyword == null ? "?page=" + (page - 1).ToString() : "?page=" + (page - 1).ToString()+"&keyword=" + keyword;
            }
            if (page + 1 >= maxpage)
            {
                nextPage.HRef = keyword == null ? "?page=" + maxpage.ToString() : "?page=" + maxpage.ToString()+"&keyword=" + keyword;
            }
            else 
            {
                nextPage.HRef = keyword == null ? "?page=" + (page + 1).ToString() : "?page=" + (page + 1).ToString() + "&keyword=" + keyword;
            }
            hbJobContent.InnerHtml += JobCommon.JobUtility.GetJob(job, page, pagesize);
            
        }
        catch (Exception ex)
        { }
    }
    protected void searchBtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("joblist.aspx?keyword=" + SearchBox.Value);
    }
    protected void adSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect("jobsearch.aspx");
    }
}