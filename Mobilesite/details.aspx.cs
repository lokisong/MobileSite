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

public partial class details : System.Web.UI.Page
{
    public string jobNameShow="";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            jobSearch(Request["jobid"] == null ? "0" : Request["jobid"].ToString());
        }
    }

    public void jobSearch(string keyword)
    {
        jobTitle.InnerHtml = "";
        PostSubmitter post = new PostSubmitter();
        post.Url = "https://masterdemocn.hiringboss.com/careersiteJobDetail.do";
        post.PostItems.Add("position", keyword);
        //post.PostItems.Add{ "location", "" };
        //post.PostItems.Add{ "empType", "" };
        //post.PostItems.Add{ "vertical", "" };
        //post.PostItems.Add{ "subdomain", "" };
        //post.PostItems.Add("candidateSourceId", "32665");
        post.Type = PostSubmitter.PostTypeEnum.Post;
        string result = "";
        try
        {
            result = post.Post();
        }
        catch (Exception e)
        {
        }

        try
        {
            
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Job));
            Job job = (Job)serializer.ReadObject(ms);


            jobTitle.InnerHtml = job.name;
            jobDes.InnerHtml = "";
            jobDes.InnerHtml += "Location: " + job.locationName + "<br/>";
            jobDes.InnerHtml += "Vertical: " + job.verticalName + "<br/>";
            jobDes.InnerHtml += job.fullDescription + "<br/>";
            jobNameShow = job.name;

        }
        catch (Exception ex)
        { }
    }
    protected void apply_Click(object sender, EventArgs e)
    {
        Response.Redirect("apply.aspx?jobid=" + Request["jobid"].ToString());
    }
}