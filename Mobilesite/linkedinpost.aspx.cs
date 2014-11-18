using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using System.Text;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json.Linq;

public partial class linkedinpost : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        string type = "";
        string Re = "";
        Re += "数据传送方式：";
        if (Request.RequestType.ToUpper() == "POST")
        {
            type = "POST";
            Re += type + "<br/>参数分别是：<br/>";
            SortedList table = Param();
            if (table != null)
            {
                foreach (DictionaryEntry De in table) { Re += "参数名：" + De.Key + " 值：" + De.Value + "<br/>"; }
            }
            else
            { Re = "你没有传递任何参数过来！"; }
        }
        else
        {
            type = "GET";
            Re += type + "<br/>参数分别是：<br/>";
            NameValueCollection nvc = GETInput();
            if (nvc.Count != 0)
            {
                for (int i = 0; i < nvc.Count; i++) { Re += "参数名：" + nvc.GetKey(i) + " 值：" + nvc.GetValues(i)[0] + "<br/>"; }
            }
            else
            { Re = "你没有传递任何参数过来！"; }
        }
        Response.Write(Re);
        WriteTxt(Re);


        //StreamReader sr = new StreamReader(Request.GetBufferlessInputStream());
        //string response = sr.ReadToEnd();

        //if (response != null)
        //{
        //    FileStream fs = new FileStream("c:\\log.txt", FileMode.Create);
        //    StreamWriter sw = new StreamWriter(fs);
        //    //开始写入
        //    sw.Write(response);
        //    //清空缓冲区
        //    sw.Flush();
        //    //关闭流
        //    sw.Close();
        //    fs.Close();
        //}

        //string str="{\"person\":{\"skills\":{\"_total\":2,\"values\":[{\"id\":5,\"skill\":{\"name\":\"Salesforce.com\"}},{\"id\":6,\"skill\":{\"name\":\"SaaS\"}}]},\"id\":\"Kz28zlYULg\",\"headline\":\"Analyst at TE Connectivity\",\"publicProfileUrl\":\"http://www.linkedin.com/pub/wenji-song/33/799/268\",\"positions\":{\"_total\":3,\"values\":[{\"company\":{\"id\":2059,\"industry\":\"Electrical/Electronic Manufacturing\",\"name\":\"TE Connectivity\",\"size\":\"10,001+ employees\",\"ticker\":\"TEL\",\"type\":\"Public Company\"},\"id\":290884442,\"isCurrent\":true,\"startDate\":{\"month\":3,\"year\":2012},\"title\":\"Analyst\"},{\"company\":{\"id\":764805,\"industry\":\"Computer Software\",\"name\":\"Lumesse (formerly known as StepStone Solutions)\",\"size\":\"501-1000 employees\",\"type\":\"Privately Held\"},\"endDate\":{\"month\":3,\"year\":2012},\"id\":184963415,\"isCurrent\":false,\"startDate\":{\"month\":3,\"year\":2011},\"title\":\"FO Consultant\"},{\"company\":{\"id\":1463,\"industry\":\"Human Resources\",\"name\":\"ADP\",\"size\":\"10,001+ employees\",\"ticker\":\"ADP\",\"type\":\"Public Company\"},\"endDate\":{\"month\":3,\"year\":2011},\"id\":302140639,\"isCurrent\":false,\"startDate\":{\"month\":11,\"year\":2007},\"title\":\"Software Engineer - Teamleader\"}]},\"lastName\":\"Song\",\"location\":{\"country\":{\"code\":\"cn\"},\"name\":\"Shanghai City, China\",\"postalCode\":\"200120\"},\"emailAddress\":\"songwenji@hotmail.com\",\"recommendationsReceived\":{\"_total\":0},\"educations\":{\"_total\":1,\"values\":[{\"degree\":\"Bachelor's degree\",\"id\":118701377,\"schoolName\":\"Shanghai Second Polytechnic University\"}]},\"firstName\":\"WenJi\"},\"job\":{\"position\":{\"title\":\"Testing Engineer\"},\"id\":\"1378\",\"company\":{\"name\":\"HRBOSS\"}},\"meta\":\"Apply with LinkedIn\",\"pdfUrl\":\"http://www.linkedin.com/cws/job/pdf?id 值：keIQLikx01y%2B8jUOsgxny%2Fx4UVwlkTc256HXYX3d1TM%3D\"}";

        //JObject jsonObj = JObject.Parse(str);

        //Response.Write(jsonObj["person"]["firstName"]);
        //Response.Write(jsonObj["person"]["lastName"]);
        //Response.Write(jsonObj["person"]["emailAddress"]);
        //Response.Write(jsonObj["job"]["position"]["title"]);
        //Response.Write(jsonObj["person"]["positions"]["values"]);
        //Response.Write(jsonObj["job"]["id"]);
        //Response.End();


    }
    //获取GET返回来的数据
    private NameValueCollection GETInput()
    { return Request.QueryString; }
    // 获取POST返回来的数据
    private string PostInput()
    {
        try
        {
            System.IO.Stream s = Request.InputStream;
            int count = 0;
            byte[] buffer = new byte[1024];
            StringBuilder builder = new StringBuilder();
            while ((count = s.Read(buffer, 0, 1024)) > 0)
            {
                builder.Append(Encoding.UTF8.GetString(buffer, 0, count));
            }
            s.Flush();
            s.Close();
            s.Dispose();
            return builder.ToString();
        }
        catch (Exception ex)
        { throw ex; }
    }
    private SortedList Param()
    {
        string POSTStr = PostInput();
        SortedList SortList = new SortedList();
        int index = POSTStr.IndexOf("&");
        string[] Arr = { };
        if (index != -1) //参数传递不只一项
        {
            Arr = POSTStr.Split('&');
            for (int i = 0; i < Arr.Length; i++)
            {
                int equalindex = Arr[i].IndexOf('=');
                string paramN = Arr[i].Substring(0, equalindex);
                string paramV = Arr[i].Substring(equalindex + 1);
                if (!SortList.ContainsKey(paramN)) //避免用户传递相同参数
                { SortList.Add(paramN, paramV); }
                else //如果有相同的，一直删除取最后一个值为准
                { SortList.Remove(paramN); SortList.Add(paramN, paramV); }
            }
        }
        else //参数少于或等于1项
        {
            int equalindex = POSTStr.IndexOf('=');
            if (equalindex != -1)
            { //参数是1项
                string paramN = POSTStr.Substring(0, equalindex);
                string paramV = POSTStr.Substring(equalindex + 1);
                SortList.Add(paramN, paramV);

            }
            else //没有传递参数过来
            { SortList = null; }
        }
        return SortList;
    }

    public T FromJson<T>(string strJson) where T : class
    {
        DataContractJsonSerializer ds = new DataContractJsonSerializer(typeof(T));
        MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(strJson));

        return ds.ReadObject(ms) as T;
    }

    public void WriteTxt(string text)
    {
        FileStream fs = new FileStream("C:\\log.txt", FileMode.Append);
        StreamWriter sw = new StreamWriter(fs, Encoding.Default);
        sw.Write(text);
        sw.Close();
        fs.Close();
    }
}