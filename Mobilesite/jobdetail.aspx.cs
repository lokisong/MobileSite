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


public partial class jobdetail : System.Web.UI.Page
{
    public string jobNameShow = "";
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
        GetSubmitter getter=new GetSubmitter();
        PostSubmitter post = new PostSubmitter();
        string getResp = getter.GetModel("https://fiat-chrysler.hiringboss.com/hb/positions/" + keyword + ".do");

        string result = "";
        try
        {
            result = getResp;
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
            jobLocation.InnerHtml += job.locationName;
            jobVertical.InnerHtml += job.verticalName;
            jobDes.InnerHtml += job.publicDescription + "<br/>";
            jobNameShow = job.name;

        }
        catch (Exception ex)
        { }
    }

    protected void applyBtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("jobapply.aspx?jobid=" + Request["jobid"].ToString());
    }
    protected void backBtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("joblist.aspx");
    }
}