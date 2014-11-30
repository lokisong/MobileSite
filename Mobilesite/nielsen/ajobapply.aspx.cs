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
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Net;
using HttpPost;
using Aqayo;
using System.Dynamic;
using Newtonsoft.Json.Linq;
using System.Configuration;



public partial class ajobapply : System.Web.UI.Page
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
            string jobName = temp2["title"].ToString();
            string newJobID = temp2["id"].ToString();
            jobTitle.InnerHtml = "您在申请:" + jobName;
            ViewState["jobId"] = newJobID;
        }
        catch (Exception ex)
        {
            //Response.Write(ex.Message);

        }

    }


    protected void submit_Click(object sender, EventArgs e)
    {
        string firstNameCol = ConfigurationManager.AppSettings["firstNameCol"];
        string lastNameCol = ConfigurationManager.AppSettings["lastNameCol"];
        string mobileCol = ConfigurationManager.AppSettings["mobileCol"];
        string companyCol = ConfigurationManager.AppSettings["companyCol"];
        string titleCol = ConfigurationManager.AppSettings["titleCol"];
        string yearCol = ConfigurationManager.AppSettings["yearCol"];
        string qualificationCol = ConfigurationManager.AppSettings["qualificationCol"];
        string detialsCol = ConfigurationManager.AppSettings["detialsCol"];

        string firstNameField = String.Empty;
        string lastNameField = String.Empty;
        string mobileField = String.Empty;
        string companyField = String.Empty;
        string titleField = String.Empty;
        string yearField = String.Empty;
        string qualificationField = String.Empty;
        string detialsField = String.Empty;

        AqayoUtility au = new AqayoUtility();
        try
        {
            var temp2 = JObject.Parse("{\"root\": " + au.getCandidateFields() + "}");
           
            //firstName
            var o =
                 from p in temp2["root"]
                 where p["displayedName"].ToString() == firstNameCol
                 select p["fieldName"];
            foreach (var item in o)
            {
                firstNameCol = item.ToString();
            }
            //lastName
            o =
                 from p in temp2["root"]
                 where p["displayedName"].ToString() == lastNameCol
                 select p["fieldName"];
            foreach (var item in o)
            {
                lastNameField = item.ToString();
            }
            //mobile
            o =
                 from p in temp2["root"]
                 where p["displayedName"].ToString() == mobileCol
                 select p["fieldName"];
            foreach (var item in o)
            {
                mobileField = item.ToString();
            }
            //company
            o =
                 from p in temp2["root"]
                 where p["displayedName"].ToString() == companyCol
                 select p["fieldName"];
            foreach (var item in o)
            {
                companyField = item.ToString();
            }
            //title
            o =
                 from p in temp2["root"]
                 where p["displayedName"].ToString() == titleCol
                 select p["fieldName"];
            foreach (var item in o)
            {
                titleField = item.ToString();
            }
            //year
            o =
                 from p in temp2["root"]
                 where p["displayedName"].ToString() == yearCol
                 select p["fieldName"];
            foreach (var item in o)
            {
                yearField = item.ToString();
            }
            //qualification
            o =
                 from p in temp2["root"]
                 where p["displayedName"].ToString() == qualificationCol
                 select p["fieldName"];
            foreach (var item in o)
            {
                qualificationField = item.ToString();
            }
            //detials
            o =
                 from p in temp2["root"]
                 where p["displayedName"].ToString() == detialsCol
                 select p["fieldName"];
            foreach (var item in o)
            {
                detialsField = item.ToString();
            }


            PostSubmitter post = new PostSubmitter();
            post.Url = ConfigurationManager.AppSettings["saveCandidate"];
            post.PostItems.Add("accessKey", au.getAccess());
            post.PostItems.Add("email", email.Text);
            post.PostItems.Add(firstNameField, name.Text);
            post.PostItems.Add(lastNameField, lastName.Text);
            post.PostItems.Add(mobileField, phone.Text);
            post.PostItems.Add(companyField, company.Text);
            post.PostItems.Add(titleField, position.Text);
            post.PostItems.Add(yearField, years.Value);
            post.PostItems.Add(qualificationField, education.Value);
            post.PostItems.Add(detialsField, lastNameField);
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
            catch (Exception ex)
            {
            }

            PostSubmitter post2 = new PostSubmitter();
            post2.Url = ConfigurationManager.AppSettings["associateCandidateToJob"];
            post2.PostItems.Add("accessKey", au.getAccess());
            post2.PostItems.Add("candidateId", result);
            post2.PostItems.Add("jobId", ViewState["jobId"].ToString());
            post2.Type = PostSubmitter.PostTypeEnum.Post;
            try
            {
                result = post2.Post();
                if (result.IndexOf("success")>=0)
                {
                    Response.Redirect("applysuccess.html");
                }
            }
            catch (Exception ex)
            {
            }
                

        }
        catch (Exception ex)
        {
            //Response.Write(ex.Message);

        }

       
    }

  

    public class Customer
    {

        public int? Unid { get; set; }

        public string CustomerName { get; set; }

    }


    public string PostMoths(String para)
    {
        string strURL = "https://fiat-chrysler.hiringboss.com/careersiteSubmitCandidate.do";
        System.Net.HttpWebRequest request;
        request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
        //Post请求方式
        request.Method = "POST";
        //内容类型
        request.ContentType = "application/x-www-form-urlencoded";
        //参数经过URL编码
        string paraUrlCoded = System.Web.HttpUtility.UrlEncode("candidateJson");
        paraUrlCoded += "=" + System.Web.HttpUtility.UrlEncode(para);
        byte[] payload;
        //将URL编码后的字符串转化为字节
        payload = System.Text.Encoding.UTF8.GetBytes(paraUrlCoded);
        //设置请求的ContentLength 
        request.ContentLength = payload.Length;
        //获得请求流
        Stream writer = request.GetRequestStream();
        //将请求参数写入流
        writer.Write(payload, 0, payload.Length);
        //关闭请求流
        writer.Close();
        System.Net.HttpWebResponse response;
        //获得响应流
        response = (System.Net.HttpWebResponse)request.GetResponse();
        System.IO.Stream s;
        s = response.GetResponseStream();
        string StrDate = "";
        string strValue = "";
        StreamReader Reader = new StreamReader(s, Encoding.Default);
        while ((StrDate = Reader.ReadLine()) != null)
        {
            strValue += StrDate + "\r\n";
        }
        // txtInfo.Text = strValue;
        return strValue;
        //XmlTextReader Reader = new XmlTextReader(s);
        //Reader.MoveToContent();
        //string strValue = Reader.ReadInnerXml();
        //strValue = strValue.Replace("&lt;", "<");
        //strValue = strValue.Replace("&gt;", ">");
        //MessageBox.Show(strValue);
        //Reader.Close();
    }
    protected void backBtn_Click(object sender, EventArgs e)
    {

        Response.Redirect("ajoblist.aspx");
    }
}