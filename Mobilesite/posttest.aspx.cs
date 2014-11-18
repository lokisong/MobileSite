using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections.Specialized;
using System.Collections;
using System.IO;
using Newtonsoft.Json.Linq;
using Mobile.Common;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Net;
using HttpPost;

public partial class posttest : System.Web.UI.Page
{
        protected void Page_Load(object sender, EventArgs e)
        {
            //string type = "";
            //string Re = "";
            //string str="";
            //Re += "数据传送方式：";
            //JObject jsonObj = new JObject();
            //if (Request.RequestType.ToUpper() == "POST")
            //{
                
            //    type = "POST";
            //    Re += type + "<br/>参数分别是：<br/>";
            //    SortedList table = sParam();
            //    //Hashtable table = hParam();
            //    if (table != null)
            //    {
            //        foreach (DictionaryEntry De in table)
            //        {
            //            Re += "参数名：" + De.Key + " 值：" + De.Value + "<br/>";
            //            str += ""+De.Key + De.Value;
                        
            //        }
            //    }
            //    else
            //    { Re = "你没有传递任何参数过来！"; }
                
            //}
            //else
            //{

            //    type = "GET";
            //    Re += type + "<br/>参数分别是：<br/>";
            //    NameValueCollection nvc = GETInput();
            //    if (nvc.Count != 0)
            //    {
            //        for (int i = 0; i < nvc.Count; i++)
            //        {
            //            Re += "参数名：" + nvc.GetKey(i) + " 值：" + nvc.GetValues(i)[0] + "<br/>";
            //        }
            //    }
            //    else
            //    { Re = "你没有传递任何参数过来！"; }
            //}

            ////jsonObj = JObject.Parse(str);
            ////candidateSubmit(jsonObj);
            //FileStream fs = new FileStream("c:\\log.txt", FileMode.Create);
            //StreamWriter sw = new StreamWriter(fs);
            ////开始写入
            //sw.Write(str);
            ////清空缓冲区
            //sw.Flush();
            ////关闭流
            //sw.Close();
            //fs.Close();

            //try { 
            //        jsonObj = JObject.Parse(str);
            //        candidateSubmit(jsonObj);
            //    }
            //catch{}

            WriteTxt(Request.Url.AbsoluteUri.ToString());

            string loginUrl = "https://www.linkedin.com/uas/oauth2/accessToken";
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            //parameters.Add("grant_type", "authorization_code");
            //parameters.Add("code", Request["code"].ToString());
            //parameters.Add("client_id", "759wbpflbgm7wc");
            //parameters.Add("client_secret", "IxMTyhtkPfv0Xq9b");

            string grant_type = "authorization_code";
            //string code = Request["code"].ToString();
            string code = "";
            string client_id = "759wbpflbgm7wc";
            string client_secret = "IxMTyhtkPfv0Xq9b";
            string time = DateTime.Now.ToString();
            string postData = string.Format("grant_type={0}&code={1}&client_id={2}&client_secret={3}", grant_type, code, client_id, client_secret); // 要发放的数据
            postData = @"candidateJson=123";
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(postData);
            HttpWebRequest objWebRequest = (HttpWebRequest)WebRequest.Create("https://fiat-chrysler.hiringboss.com/careersiteSubmitCandidate.do"); //发送地址
            objWebRequest.Method = "POST";//提交方式
            objWebRequest.ContentType = "application/x-www-form-urlencoded";
            objWebRequest.ContentLength = byteArray.Length;
            Stream newStream = objWebRequest.GetRequestStream(); // Send the data.
            newStream.Write(byteArray, 0, byteArray.Length); //写入参数
            newStream.Close();

            //try
            //{
                HttpWebResponse response = (HttpWebResponse)objWebRequest.GetResponse();

                //HttpWebResponse response = HttpWebResponseUtility.CreatePostHttpResponse(loginUrl, parameters, null, null, Encoding.UTF8, null);
                StreamReader sr = new StreamReader(response.GetResponseStream(), System.Text.Encoding.Default);
                string textResponse = sr.ReadToEnd();
                WriteTxt(textResponse);
                Response.Write(textResponse);
            //}
            //catch
            //{
            //    Response.Write("<script type='text/javascript'>alert('Please try again later');</script>");
            //}
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

        private Hashtable hParam()
        {
            string POSTStr = PostInput();
            Hashtable HashList = new Hashtable();
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
                    if (!HashList.ContainsKey(paramN)) //避免用户传递相同参数
                    { HashList.Add(paramN, paramV); }
                    else //如果有相同的，一直删除取最后一个值为准
                    { HashList.Remove(paramN); HashList.Add(paramN, paramV); }
                }
            }
            else //参数少于或等于1项
            {
                int equalindex = POSTStr.IndexOf('=');
                if (equalindex != -1)
                { //参数是1项
                    string paramN = POSTStr.Substring(0, equalindex);
                    string paramV = POSTStr.Substring(equalindex + 1);
                    HashList.Add(paramN, paramV);

                }
                else //没有传递参数过来
                { HashList = null; }
            }
            return HashList;
        }

        private SortedList sParam()
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


        protected void candidateSubmit(JObject jsonObj)
        {

            Candidate tcandidate = new Candidate();

            tcandidate.familyName = jsonObj["person"]["lastName"].ToString();
            tcandidate.firstName = jsonObj["person"]["firstName"].ToString();

            tcandidate.address1 = "";
            tcandidate.candidateSourceId = 1130;
            tcandidate.email = jsonObj["person"]["emailAddress"].ToString();
            tcandidate.workSought = null;

            string[] workType = { "workSought_permanent" };
            tcandidate.workSought = workType;

            Education tempEdu = new Education();


            CandidateDocument[] allDocument = new CandidateDocument[1];
            CandidateDocument document1 = new CandidateDocument();
            document1.setDocumentId(0);
            document1.setisPrimary(true);
            allDocument[0] = document1;

            CNcandidate person = new CNcandidate(Convert.ToInt32(jsonObj["job"]["id"].ToString()), tcandidate, allDocument, tempEdu, 0, "test");


            string loginUrl = "http://masterdemocn.hiringboss.com/careersiteSubmitCandidate.do";
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("candidateJson", GetToJson(person));

            try
            {

                HttpWebResponse response = HttpWebResponseUtility.CreatePostHttpResponse(loginUrl, parameters, null, null, Encoding.UTF8, null);

            }
            catch
            {
            }

        }

        private string GetToJson(object dic)
        {
            //实例化JavaScriptSerializer类的新实例
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                //将对象序列化为json数据
                return jss.Serialize(dic);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public void WriteTxt(string text)
        {
            FileStream fs = new FileStream("C:\\A.txt", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            sw.Write(text);
            sw.Close();
            fs.Close();
        }
    }