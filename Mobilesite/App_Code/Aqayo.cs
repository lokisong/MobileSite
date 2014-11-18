using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using HttpPost;
using Mobile.Common;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Configuration;
using System.Dynamic;
using System.Net;
using System.Reflection.Emit;

/// <summary>
/// Summary description for Aqayo
/// </summary>
namespace Aqayo
{


    public class accessKey
    {

        public int id { get; set; }
        public string accesskey { get; set; }
        public string username { get; set; }
        public string accesstime { get; set; }

    }

    public class searchResult
    {

        //[JsonProperty("columns")]
        //public Columns[] Columns { get; set; }

        [JsonProperty("columns")]
        public string[] Columns { get; set; }

        [JsonProperty("currentpage")]
        public int Currentpage { get; set; }

        [JsonProperty("currentpagerecords")]
        public int Currentpagerecords { get; set; }

        //[JsonProperty("data")]
        //public Datum[] Data { get; set; }

        [JsonProperty("data")]
        public string[] Data { get; set; }

        [JsonProperty("filterData")]
        public FilterData FilterData { get; set; }

        [JsonProperty("recordsperpage")]
        public int Recordsperpage { get; set; }

        [JsonProperty("sourcekeyscols")]
        public object[] Sourcekeyscols { get; set; }

        [JsonProperty("start")]
        public int Start { get; set; }

        [JsonProperty("totalrecords")]
        public int Totalrecords { get; set; }
    }

    public class Columns
    {

        [JsonProperty("labelToUse")]
        public string LabelToUse { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("fieldname")]
        public string Fieldname { get; set; }

        [JsonProperty("fieldtype")]
        public string Fieldtype { get; set; }

        [JsonProperty("length")]
        public int Length { get; set; }

        [JsonProperty("column")]
        public string Column { get; set; }

        [JsonProperty("filterSettings")]
        public string FilterSettings { get; set; }

        [JsonProperty("selectionDefaultedValues")]
        public object[] SelectionDefaultedValues { get; set; }
    }

    public class Datum
    {

        [JsonProperty("country_28701")]
        public string Country28701 { get; set; }

        [JsonProperty("employmenttype_95916")]
        public string Employmenttype95916 { get; set; }

        [JsonProperty("externaluid")]
        public string Externaluid { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("jobfunction_30896")]
        public string Jobfunction30896 { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("yearsofexperien_20245")]
        public string Yearsofexperien20245 { get; set; }
    }

    public class FilterData
    {
    }


    public partial class joblist
    {
        public class Column
        {
            public string labelToUse;
            public int id;
            public string fieldname;
            public string fieldtype;
            public int length;
            public string column;
            public string filterSettings;
            public object[] selectionDefaultedValues;
        }
    }

    public partial class joblist
    {
        public class Datum
        {
            public string country;
            public string employmenttype_95916;
            public string externaluid;
            public int id;
            public string jobfunction_30896;
            public string title;
            public string yearsofexperien_20245;
        }
    }

    public partial class joblist
    {
        public class FilterData2
        {
        }
    }

    public partial class joblist
    {
        public Column[] columns;
        public int currentpage;
        public int currentpagerecords;
        public Datum[] data;
        public FilterData2 filterData;
        public int recordsperpage;
        public object[] sourcekeyscols;
        public int start;
        public int totalrecords;
    }

    public class AqayoUtility
    {

        public string getAccess()
        {
            GetSubmitter newget = new GetSubmitter();
            string url = ConfigurationManager.AppSettings["getAccess"] + "?username=" + ConfigurationManager.AppSettings["username"] + "&password=" + ConfigurationManager.AppSettings["password"];
            HttpWebResponse response = HttpWebResponseUtility.CreateGetHttpResponse(url, null, null, null);
            //string result = newget.GetModel(url);
            //accessKey ak = JsonHelper.JsonDeserialize<accessKey>();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(accessKey));
            accessKey ak = (accessKey)serializer.ReadObject(response.GetResponseStream());
            return ak.accesskey;
        }

        public string getJobs(string start)
        {
            GetSubmitter newget = new GetSubmitter();
            string url = ConfigurationManager.AppSettings["getjobs"] + "?accessKey=" + getAccess() + "&externalsiteid=" + ConfigurationManager.AppSettings["externalsiteid"] + "&start=" + start;
            string result = newget.GetModel(url);
            //MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            //DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(searchResult));
            //searchResult sr = (searchResult)serializer.ReadObject(ms);
            return result;
        }

        public joblist getJobsObj(string start)
        {
            GetSubmitter newget = new GetSubmitter();
            string url = ConfigurationManager.AppSettings["getjobs"] + "?accessKey=" + getAccess() + "&externalsiteid=" + ConfigurationManager.AppSettings["externalsiteid"] + "&start=" + start;
            //string result = newget.GetModel(url);
            //MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            //DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(joblist));
            //joblist sr = (joblist)serializer.ReadObject(ms);
            HttpWebResponse response = HttpWebResponseUtility.CreateGetHttpResponse(url, null, null, null);
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(joblist));
            joblist sr = (joblist)serializer.ReadObject(response.GetResponseStream());
            return sr;

        }
    }

}