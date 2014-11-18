using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mobile.Common;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;

public partial class jobsearch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            PostSubmitter post = new PostSubmitter();
            post.Url = "https://fiat-chrysler.hiringboss.com/applicationFormPredefinedInfo.do?type=locations%2Cvertical&lang=ZH_CN";
            //post.PostItems.Add("type", "locations,vertical");
            //post.PostItems.Add("lang", "ZH_CN");
            //post.PostItems.Add{ "location", "" };
            //post.PostItems.Add{ "empType", "" };
            //post.PostItems.Add{ "vertical", "" };
            //post.PostItems.Add{ "subdomain", "" };
            //post.PostItems.Add("subdomain", "masterdemocn");
            post.Type = PostSubmitter.PostTypeEnum.Post;
            string result = "";
            try
            {
                result = post.Post();
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Formlist[]));
                Formlist[] formlist = (Formlist[])serializer.ReadObject(ms);
                for (int i = 0; i < formlist[0].result.Length; i++)
                {
                    location.Items.Add(new ListItem(formlist[0].result[i].name, formlist[0].result[i].value));
                }
                for (int i = 0; i < formlist[1].result.Length; i++)
                {
                    department.Items.Add(new ListItem(formlist[1].result[i].name, formlist[1].result[i].value));
                }
                
            }
            catch (Exception ex)
            {
                
            }

            
        }
    }
    protected void submit_Click(object sender, EventArgs e)
    {
        //Server.Transfer("joblist.aspx?location=" + location.SelectedValue + "&vertical=" + department.SelectedValue, true);
       Response.Redirect("joblist.aspx?location=" + location.SelectedValue + "&vertical=" + department.SelectedValue);
        //location.Items.Clear();
    }
    protected void backBtn_Click(object sender, EventArgs e)
    {
        
            Response.Redirect("joblist.aspx");
    }
}