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

public partial class index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //var response = Http.Post("http://audi.hiringboss.com/careersiteJobSearch.do", new NameValueCollection() {
            //{ "keyword", "manager" }
            ////{ "location", "" },
            ////{ "empType", "" },
            ////{ "vertical", "" },
            ////{ "subdomain", "" }
            //});
            //Response.Write(response.ToString());
            jobSearch(null);

           
        }
    }
    protected void jobSearchSubmit_Click(object sender, EventArgs e)
    {
        string serText = Request["jobSearchText"].ToString();
        jobSearch(jobSearchText.Value);
    }

    public void jobSearch(string keyword)
    {
        hbJobContent.InnerHtml = "";
        PostSubmitter post = new PostSubmitter();
        post.Url = "https://masterdemocn.hiringboss.com/careersiteJobSearch.do";
        post.PostItems.Add("keyword", keyword);
        //post.PostItems.Add{ "location", "" };
        //post.PostItems.Add{ "empType", "" };
        //post.PostItems.Add{ "vertical", "" };
        //post.PostItems.Add{ "subdomain", "" };
        post.PostItems.Add("subdomain", "masterdemocn");
        post.Type = PostSubmitter.PostTypeEnum.Post;
        string result = "";
        try
        {
            result = post.Post();
        }
        catch(Exception e)
        {
            hbJobContent.InnerHtml += result;
            hbJobContent.InnerHtml += @"<article class=""underline"">			
			<p>No Data</p>
            </article>";
        }

        try
        {
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Job[]));
            Job[] job = (Job[])serializer.ReadObject(ms);

            for (int i = 0; i < job.Length; i++)
            {
                if (job[i].fullDescription.Length > 200)
                {
                    hbJobContent.InnerHtml += @"<article class=""underline"">
			<h2><a href=""details.aspx?jobid=" + job[i].id + @""">" + job[i].name + @"</a></h2>
			<p>" + job[i].fullDescription.Substring(0, 200).Substring(0, job[i].fullDescription.Substring(0, 200).LastIndexOf('<')) + @"...</p>
			<div class=""date""><span>" + job[i].headCountOpenDate + @"</span></div>
            </article>";
                }
                else
                {
                    hbJobContent.InnerHtml += @"<article class=""underline"">
			<h2><a href=""details.aspx?jobid=" + job[i].id + @""">" + job[i].name + @"</a></h2>
			<p>" + job[i].fullDescription.ToString() + @"...</p>
			<div class=""date""><span>" + job[i].headCountOpenDate + @"</span></div>
            </article>";
                }
            }
        }
        catch (Exception ex)
        { }
    }




}
