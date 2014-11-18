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


public partial class jobapply : System.Web.UI.Page
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
        PostSubmitter post = new PostSubmitter();
        post.Url = "https://fiat-chrysler.hiringboss.com/careersiteJobDetail.do";
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

            jobTitle.InnerHtml ="您在申请:"+ job.name;
            //jobDes.InnerHtml += "Vertical: " + job.verticalName + "<br/>";
            jobNameShow = job.name;

        }
        catch (Exception ex)
        { }
    }


    protected void submit_Click(object sender, EventArgs e)
    {

        Candidate tcandidate = new Candidate();



        tcandidate.address1 = "";
        //tcandidate.availabilityStart="";
        tcandidate.candidateSourceId = 32704;
        //tcandidate.city="";
        //tcandidate.companyJson="";
        //tcandidate.contractinterval="";
        //tcandidate.contractRate=0;
        //tcandidate.country="";
        //tcandidate.countrySpecific="";
        //tcandidate.currentEmployer="";
        //tcandidate.currentSalary=0;
        //tcandidate.dateOfBirth=null;
        //tcandidate.desireSalary=0;
        //tcandidate.driversLicenseNumber="";
        //tcandidate.driversLicenseType="";
        //tcandidate.educationSummary="";
        tcandidate.email = email.Text;
        //tcandidate.emergencyEmail="";
        //tcandidate.emergencyName="";
        //tcandidate.emergencyPhone="";
        //tcandidate.emergencyRelationship="";
        //tcandidate.experienceSummary="";
        tcandidate.familyName = lastName.Text;
        tcandidate.firstName = name.Text;
        //tcandidate.firstNameKana="";
        //tcandidate.genderTitle="";
        //tcandidate.home_phone="";
        //tcandidate.hukou="";
        //tcandidate.idNumber="";
        //tcandidate.idType="";
        //tcandidate.industry=0;
        //tcandidate.keyword="";
        //tcandidate.lastNameKana="";
        //tcandidate.linked_in_profile="";
        //tcandidate.male=0;
        //tcandidate.middleName="";
        //tcandidate.middleNameKana="";
        //tcandidate.nationality="";
        //tcandidate.otherBenefits="";
        //tcandidate.passportNo="";
        //tcandidate.personalAddress1="";
        //tcandidate.personalAddress2="";
        //tcandidate.personalCity="";
        //tcandidate.personalCountry="";
        //tcandidate.personalState="";
        tcandidate.personalStatements = Background.Value;
        //tcandidate.personalZipcode="";
        tcandidate.phone = phone.Text;
        //tcandidate.phone2="";
        //tcandidate.politicalStatus="";
        tcandidate.presentJob = position.Text;
        tcandidate.currentEmployer = company.Text;
        //tcandidate.relocate="";
        //tcandidate.skillsSummary="";
        //tcandidate.skype="";
        //tcandidate.sourceContactId="";
        //tcandidate.state="";
        //tcandidate.toeicScore=0;
        //tcandidate.totalPA="";
        //tcandidate.work_email="";
        //tcandidate.work_phone="";
        tcandidate.workSought = null;
        try
        {
            tcandidate.yearOfExperience = Convert.ToInt32(years.Value);
        }
        catch
        {
            tcandidate.yearOfExperience = 0;
        }
        //tcandidate.zipcode="";

        //tcandidate.setFamilyName("");
        //tcandidate.setFirstName(name.Text);

        //tcandidate.setAddress1("");
        //tcandidate.setAvailabilityStart("");
        //tcandidate.setCandidateSourceId(32665);
        //tcandidate.setCity("");
        //tcandidate.setCompanyJson("");
        //tcandidate.setContractinterval("");
        //tcandidate.setContractRate(0);
        //tcandidate.setCountry("");
        //tcandidate.setCountrySpecific("");
        //tcandidate.setCurrentEmployer("");
        //tcandidate.setCurrentSalary(0);
        //tcandidate.setDateOfBirth("");
        //tcandidate.setDesireSalary(0);
        //tcandidate.setDriversLicenseNumber("");
        //tcandidate.setDriversLicenseType("");
        //tcandidate.setEducationSummary("");
        //tcandidate.setEmail("");
        //tcandidate.setEmergencyEmail("");
        //tcandidate.setEmergencyName("");
        //tcandidate.setEmergencyPhone("");
        //tcandidate.setEmergencyRelationship("");
        //tcandidate.setExperienceSummary("");
        //tcandidate.setFamilyName("");
        //tcandidate.setFirstName("");
        //tcandidate.setFirstNameKana("");
        //tcandidate.setGenderTitle("");
        //tcandidate.setHome_phone("");
        //tcandidate.setHukou("");
        //tcandidate.setIdNumber("");
        //tcandidate.setIdType("");
        //tcandidate.setIndustry(0);
        //tcandidate.setKeyword("");
        //tcandidate.setLastNameKana("");
        //tcandidate.setLinked_in_profile("");
        //tcandidate.setMale(0);
        //tcandidate.setMiddleName("");
        //tcandidate.setMiddleNameKana("");
        //tcandidate.setNationality("");
        //tcandidate.setOtherBenefits("");
        //tcandidate.setPassportNo("");
        //tcandidate.setPersonalAddress1("");
        //tcandidate.setPersonalAddress2("");
        //tcandidate.setPersonalCity("");
        //tcandidate.setPersonalCountry("");
        //tcandidate.setPersonalState("");
        //tcandidate.setPersonalStatements("");
        //tcandidate.setPersonalZipcode("");
        //tcandidate.setPhone("");
        //tcandidate.setPhone2("");
        //tcandidate.setPoliticalStatus("");
        //tcandidate.setPresentJob("");
        //tcandidate.setRelocate("");
        //tcandidate.setSkillsSummary("");
        //tcandidate.setSkype("");
        //tcandidate.setSourceContactId("");
        //tcandidate.setState("");
        //tcandidate.setToeicScore(0);
        //tcandidate.setTotalPA("");
        //tcandidate.setWork_email("");
        //tcandidate.setWork_phone("");
        //tcandidate.setWorkSought(null);
        //tcandidate.setYearOfExperience(0);
        //tcandidate.setZipcode("");

        string[] workType = { "workSought_permanent" };
        tcandidate.workSought = workType;
        //tcandidate.setPersonalStatements(Background.Value);
        //tcandidate.setEmail(email.Text);

        Education tempEdu = new Education();
        //tempEdu.setEducation(Convert.ToInt16(education.Value));
        tempEdu.education = Convert.ToInt16(education.Value);
        //tempEdu.degree_name = "";
        //tempEdu.gpa = "";
        //tempEdu.graduation_date = "";
        //tempEdu.major = "";
        //tempEdu.minor = "";
        //tempEdu.qualification = "";
        //tempEdu.school_name = "";
        //tempEdu.start_date = "";


        CandidateDocument[] allDocument = new CandidateDocument[1];
        CandidateDocument document1 = new CandidateDocument();
        document1.setDocumentId(0);
        document1.setisPrimary(true);
        allDocument[0] = document1;

        CNcandidate person = new CNcandidate(Convert.ToInt32(Request["jobid"]), tcandidate, allDocument, tempEdu, 0, "test");


        string loginUrl = "http://fiat-chrysler.hiringboss.com/careersiteSubmitCandidate.do";
        IDictionary<string, string> parameters = new Dictionary<string, string>();
        parameters.Add("candidateJson", GetToJson(person));
        PostSubmitter post = new PostSubmitter();
        post.Url = loginUrl;
        post.PostItems.Add("candidateJson", GetToJson(person));
            

        try
        {

            //HttpWebResponse response = HttpWebResponseUtility.CreatePostHttpResponse(loginUrl, parameters, null, null, Encoding.UTF8, null);
            //string cookieString = response.Headers["Set-Cookie"]; 
            string ret = post.Post();
            Response.Redirect("applysuccess.html");
            //Response.Write("<script type='text/javascript'>alert('Thank you for your application');</script>");
        }
        catch
        {
            Response.Write("<script type='text/javascript'>alert('Please try again later');</script>");
        }

    }

    public static string ObjectToJsons(object obj)
    {
        DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
        MemoryStream stream = new MemoryStream();
        serializer.WriteObject(stream, obj);
        byte[] dataBytes = new byte[stream.Length];
        stream.Position = 0;
        stream.Read(dataBytes, 0, (int)stream.Length);
        return Encoding.UTF8.GetString(dataBytes);
    }

    public static string ObjectToJson(object obj)
    {
        return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
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

        Response.Redirect("joblist.aspx");
    }
}