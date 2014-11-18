using System;
using System.Text;
using System.IO;
using System.Web;
using System.Net;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Collections;
using System.Linq;


namespace Mobile.Common
{
    /// <summary>
    /// Submits post data to a url.
    /// </summary>
    public class PostSubmitter
    {
        /// <summary>
        /// determines what type of post to perform.
        /// </summary>
        public enum PostTypeEnum
        {
            /// <summary>
            /// Does a get against the source.
            /// </summary>
            Get,
            /// <summary>
            /// Does a post against the source.
            /// </summary>
            Post
        }

        private string m_url = string.Empty;
        private NameValueCollection m_values = new NameValueCollection();
        private PostTypeEnum m_type = PostTypeEnum.Get;
        /// <summary>
        /// Default constructor.
        /// </summary>
        public PostSubmitter()
        {
        }

        /// <summary>
        /// Constructor that accepts a url as a parameter
        /// </summary>
        /// <param name="url">The url where the post will be submitted to.</param>
        public PostSubmitter(string url)
            : this()
        {
            m_url = url;
        }

        /// <summary>
        /// Constructor allowing the setting of the url and items to post.
        /// </summary>
        /// <param name="url">the url for the post.</param>
        /// <param name="values">The values for the post.</param>
        public PostSubmitter(string url, NameValueCollection values)
            : this(url)
        {
            m_values = values;
        }

        /// <summary>
        /// Gets or sets the url to submit the post to.
        /// </summary>
        public string Url
        {
            get
            {
                return m_url;
            }
            set
            {
                m_url = value;
            }
        }
        /// <summary>
        /// Gets or sets the name value collection of items to post.
        /// </summary>
        public NameValueCollection PostItems
        {
            get
            {
                return m_values;
            }
            set
            {
                m_values = value;
            }
        }
        /// <summary>
        /// Gets or sets the type of action to perform against the url.
        /// </summary>
        public PostTypeEnum Type
        {
            get
            {
                return m_type;
            }
            set
            {
                m_type = value;
            }
        }
        /// <summary>
        /// Posts the supplied data to specified url.
        /// </summary>
        /// <returns>a string containing the result of the post.</returns>
        public string Post()
        {
            StringBuilder parameters = new StringBuilder();
            for (int i = 0; i < m_values.Count; i++)
            {
                EncodeAndAddItem(ref parameters, m_values.GetKey(i), m_values[i]);
            }
            string result = PostData(m_url, parameters.ToString());
            return result;
        }
        /// <summary>
        /// Posts the supplied data to specified url.
        /// </summary>
        /// <param name="url">The url to post to.</param>
        /// <returns>a string containing the result of the post.</returns>
        public string Post(string url)
        {
            m_url = url;
            return this.Post();
        }
        /// <summary>
        /// Posts the supplied data to specified url.
        /// </summary>
        /// <param name="url">The url to post to.</param>
        /// <param name="values">The values to post.</param>
        /// <returns>a string containing the result of the post.</returns>
        public string Post(string url, NameValueCollection values)
        {
            m_values = values;
            return this.Post(url);
        }
        /// <summary>
        /// Posts data to a specified url. Note that this assumes that you have already url encoded the post data.
        /// </summary>
        /// <param name="postData">The data to post.</param>
        /// <param name="url">the url to post to.</param>
        /// <returns>Returns the result of the post.</returns>
        private string PostData(string url, string postData)
        {
            HttpWebRequest request = null;
            if (m_type == PostTypeEnum.Post)
            {
                Uri uri = new Uri(url);
                request = (HttpWebRequest)WebRequest.Create(uri);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = postData.Length;
                request.Proxy = null;
                request.KeepAlive = false;
                using (Stream writeStream = request.GetRequestStream())
                {
                    UTF8Encoding encoding = new UTF8Encoding();
                    byte[] bytes = encoding.GetBytes(postData);
                    writeStream.Write(bytes, 0, bytes.Length);
                }
            }
            else
            {
                Uri uri = new Uri(url + "?" + postData);
                request = (HttpWebRequest)WebRequest.Create(uri);
                request.Method = "GET";
            }
            string result = string.Empty;
            System.GC.Collect();
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader readStream = new StreamReader(responseStream, Encoding.UTF8))
                    {
                        result = readStream.ReadToEnd();
                    }
                }
            }
            request = null;
            return result;
        }
        /// <summary>
        /// Encodes an item and ads it to the string.
        /// </summary>
        /// <param name="baseRequest">The previously encoded data.</param>
        /// <param name="dataItem">The data to encode.</param>
        /// <returns>A string containing the old data and the previously encoded data.</returns>
        private void EncodeAndAddItem(ref StringBuilder baseRequest, string key, string dataItem)
        {
            if (baseRequest == null)
            {
                baseRequest = new StringBuilder();
            }
            if (baseRequest.Length != 0)
            {
                baseRequest.Append("&");
            }
            baseRequest.Append(key);
            baseRequest.Append("=");
            baseRequest.Append(System.Web.HttpUtility.UrlEncode(dataItem));
        }
    }

    public class GetSubmitter
    {
        public string GetModel(string strUrl)
        {
            string strRet = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strUrl);
                request.Timeout = 2000;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                System.IO.Stream resStream = response.GetResponseStream();
                Encoding encode = System.Text.Encoding.UTF8;
                StreamReader readStream = new StreamReader(resStream, encode);
                Char[] read = new Char[256];
                int count = readStream.Read(read, 0, 256);
                while (count > 0)
                {
                    String str = new String(read, 0, count);
                    strRet = strRet + str;
                    count = readStream.Read(read, 0, 256);
                }
                resStream.Close();
            }
            catch (Exception e)
            {
                strRet = "";
            }
            return strRet;
        }
    }

    public class Job
    {

        public int id { get; set; }
        public string name { get; set; }
        public string businessUnit { get; set; }
        public int locationId { get; set; }
        public string locationName { get; set; }
        public int verticalId { get; set; }
        public string verticalName { get; set; }
        public string fullDescription { get; set; }
        public string publicDescription { get; set; }
        public int latestStatus { get; set; }
        public string contractType { get; set; }
        public int positionType { get; set; }
        public string headCountOpenDate { get; set; }
        public string employmentType { get; set; }
        public string headCountCloseDate { get; set; }
        public int headCount { get; set; }

    }

    //public class Candidate
    //{
    //    protected int desireSalary;
    //    protected int candidateSourceId;
    //    protected string firstName;
    //    protected string middleName;
    //    protected string familyName;
    //    protected string lastNameKana;
    //    protected string firstNameKana;
    //    protected string middleNameKana;
    //    protected string emergencyName;
    //    protected string dateOfBirth;
    //    protected string emergencyRelationship;
    //    protected string nationality;
    //    protected string emergencyPhone;
    //    protected string emergencyEmail;
    //    protected string email;
    //    protected string work_email;
    //    protected int currentSalary;
    //    protected string availabilityStart;
    //    protected string phone;
    //    protected string relocate;
    //    protected string phone2;
    //    protected string passportNo;
    //    protected string home_phone;
    //    protected string driversLicenseNumber;
    //    protected string work_phone;
    //    protected string otherBenefits;
    //    protected string linked_in_profile;
    //    protected string driversLicenseType;
    //    protected string skype;
    //    protected string sourceContactId;
    //    protected string contractinterval;
    //    protected string address1;
    //    protected string city;
    //    protected string country;
    //    protected string presentJob;
    //    protected string zipcode;
    //    protected string currentEmployer;
    //    protected string state;
    //    protected int yearOfExperience;
    //    protected string personalAddress1;
    //    protected int industry;
    //    protected string personalAddress2;
    //    protected string totalPA;
    //    protected string personalCity;
    //    protected string personalCountry;
    //    protected int contractRate;
    //    protected string personalZipcode;
    //    protected string personalState;
    //    protected string keyword;
    //    protected int toeicScore;
    //    protected int male;
    //    protected string genderTitle;
    //    protected string skillsSummary;
    //    protected string educationSummary;
    //    protected string experienceSummary;
    //    protected string[] workSought;
    //    protected string countrySpecific;
    //    protected string idType;
    //    protected string idNumber;
    //    protected string politicalStatus;
    //    protected string hukou;
    //    protected string companyJson;
    //    protected string personalStatements;

    //    public string getPersonalStatements()
    //    {
    //        return personalStatements;
    //    }
    //    public void setPersonalStatements(string personalStatements)
    //    {
    //        this.personalStatements = personalStatements;
    //    }

    //    public string getCompanyJson()
    //    {
    //        return companyJson;
    //    }
    //    public void setCompanyJson(string companyJson)
    //    {
    //        this.companyJson = companyJson;
    //    }

    //    public string getCountrySpecific()
    //    {
    //        return countrySpecific;
    //    }
    //    public void setCountrySpecific(string countrySpecific)
    //    {
    //        this.countrySpecific = countrySpecific;
    //    }

    //    public string getIdType()
    //    {
    //        return idType;
    //    }
    //    public void setIdType(string idType)
    //    {
    //        this.idType = idType;
    //    }

    //    public string getIdNumber()
    //    {
    //        return idNumber;
    //    }
    //    public void setIdNumber(string idNumber)
    //    {
    //        this.idNumber = idNumber;
    //    }

    //    public string getPoliticalStatus()
    //    {
    //        return politicalStatus;
    //    }
    //    public void setPoliticalStatus(string politicalStatus)
    //    {
    //        this.politicalStatus = politicalStatus;
    //    }

    //    public string getHukou()
    //    {
    //        return hukou;
    //    }
    //    public void setHukou(string hukou)
    //    {
    //        this.hukou = hukou;
    //    }

    //    public string[] getWorkSought()
    //    {
    //        return workSought;
    //    }
    //    public void setWorkSought(string[] workSought)
    //    {
    //        this.workSought = workSought;
    //    }

    //    public string getExperienceSummary()
    //    {
    //        return experienceSummary;
    //    }
    //    public void setExperienceSummary(string experienceSummary)
    //    {
    //        this.experienceSummary = experienceSummary;
    //    }

    //    public string getEducationSummary()
    //    {
    //        return educationSummary;
    //    }
    //    public void setEducationSummary(string educationSummary)
    //    {
    //        this.educationSummary = educationSummary;
    //    }

    //    public string getSkillsSummary()
    //    {
    //        return skillsSummary;
    //    }
    //    public void setSkillsSummary(string skillsSummary)
    //    {
    //        this.skillsSummary = skillsSummary;
    //    }
    //    public string getGenderTitle()
    //    {
    //        return genderTitle;
    //    }
    //    public void setGenderTitle(string genderTitle)
    //    {
    //        this.genderTitle = genderTitle;
    //    }

    //    public int getMale()
    //    {
    //        return male;
    //    }
    //    public void setMale(int male)
    //    {
    //        this.male = male;
    //    }

    //    public int getDesireSalary()
    //    {
    //        return desireSalary;
    //    }

    //    public void setDesireSalary(int desireSalary)
    //    {
    //        this.desireSalary = desireSalary;
    //    }

    //    public int getCandidateSourceId()
    //    {
    //        return candidateSourceId;
    //    }

    //    public void setCandidateSourceId(int candidateSourceId)
    //    {
    //        this.candidateSourceId = candidateSourceId;
    //    }

    //    public string getFirstName()
    //    {
    //        return firstName;
    //    }

    //    public void setFirstName(string firstName)
    //    {
    //        this.firstName = firstName;
    //    }

    //    public string getMiddleName()
    //    {
    //        return middleName;
    //    }

    //    public void setMiddleName(string middleName)
    //    {
    //        this.middleName = middleName;
    //    }

    //    public string getFamilyName()
    //    {
    //        return familyName;
    //    }

    //    public void setFamilyName(string familyName)
    //    {
    //        this.familyName = familyName;
    //    }

    //    public string getLastNameKana()
    //    {
    //        return lastNameKana;
    //    }

    //    public void setLastNameKana(string lastNameKana)
    //    {
    //        this.lastNameKana = lastNameKana;
    //    }

    //    public string getFirstNameKana()
    //    {
    //        return firstNameKana;
    //    }

    //    public void setFirstNameKana(string firstNameKana)
    //    {
    //        this.firstNameKana = firstNameKana;
    //    }

    //    public string getMiddleNameKana()
    //    {
    //        return middleNameKana;
    //    }

    //    public void setMiddleNameKana(string middleNameKana)
    //    {
    //        this.middleNameKana = middleNameKana;
    //    }

    //    public string getEmergencyName()
    //    {
    //        return emergencyName;
    //    }

    //    public void setEmergencyName(string emergencyName)
    //    {
    //        this.emergencyName = emergencyName;
    //    }

    //    public string getDateOfBirth()
    //    {
    //        return dateOfBirth;
    //    }

    //    public void setDateOfBirth(string dateOfBirth)
    //    {
    //        this.dateOfBirth = dateOfBirth;
    //    }

    //    public string getEmergencyRelationship()
    //    {
    //        return emergencyRelationship;
    //    }

    //    public void setEmergencyRelationship(string emergencyRelationship)
    //    {
    //        this.emergencyRelationship = emergencyRelationship;
    //    }

    //    public string getNationality()
    //    {
    //        return nationality;
    //    }

    //    public void setNationality(string nationality)
    //    {
    //        this.nationality = nationality;
    //    }

    //    public string getEmergencyPhone()
    //    {
    //        return emergencyPhone;
    //    }

    //    public void setEmergencyPhone(string emergencyPhone)
    //    {
    //        this.emergencyPhone = emergencyPhone;
    //    }


    //    public string getEmergencyEmail()
    //    {
    //        return emergencyPhone;
    //    }

    //    public void setEmergencyEmail(string emergencyEmail)
    //    {
    //        this.emergencyEmail = emergencyEmail;
    //    }

    //    public string getEmail()
    //    {
    //        return email;
    //    }

    //    public void setEmail(string email)
    //    {
    //        this.email = email;
    //    }

    //    public string getWork_email()
    //    {
    //        return work_email;
    //    }

    //    public void setWork_email(string work_email)
    //    {
    //        this.work_email = work_email;
    //    }

    //    public int getCurrentSalary()
    //    {
    //        return currentSalary;
    //    }

    //    public void setCurrentSalary(int currentSalary)
    //    {
    //        this.currentSalary = currentSalary;
    //    }

    //    public string getAvailabilityStart()
    //    {
    //        return availabilityStart;
    //    }

    //    public void setAvailabilityStart(string availabilityStart)
    //    {
    //        this.availabilityStart = availabilityStart;
    //    }

    //    public string getPhone()
    //    {
    //        return phone;
    //    }

    //    public void setPhone(string phone)
    //    {
    //        this.phone = phone;
    //    }

    //    public string getRelocate()
    //    {
    //        return relocate;
    //    }

    //    public void setRelocate(string relocate)
    //    {
    //        this.relocate = relocate;
    //    }

    //    public string getPhone2()
    //    {
    //        return phone2;
    //    }

    //    public void setPhone2(string phone2)
    //    {
    //        this.phone2 = phone2;
    //    }

    //    public string getPassportNo()
    //    {
    //        return passportNo;
    //    }

    //    public void setPassportNo(string passportNo)
    //    {
    //        this.passportNo = passportNo;
    //    }

    //    public string getHome_phone()
    //    {
    //        return home_phone;
    //    }

    //    public void setHome_phone(string home_phone)
    //    {
    //        this.home_phone = home_phone;
    //    }

    //    public string getDriversLicenseNumber()
    //    {
    //        return driversLicenseNumber;
    //    }

    //    public void setDriversLicenseNumber(string driversLicenseNumber)
    //    {
    //        this.driversLicenseNumber = driversLicenseNumber;
    //    }

    //    public string getWork_phone()
    //    {
    //        return work_phone;
    //    }

    //    public void setWork_phone(string work_phone)
    //    {
    //        this.work_phone = work_phone;
    //    }

    //    public string getOtherBenefits()
    //    {
    //        return otherBenefits;
    //    }

    //    public void setOtherBenefits(string otherBenefits)
    //    {
    //        this.otherBenefits = otherBenefits;
    //    }

    //    public string getLinked_in_profile()
    //    {
    //        return linked_in_profile;
    //    }

    //    public void setLinked_in_profile(string linked_in_profile)
    //    {
    //        this.linked_in_profile = linked_in_profile;
    //    }

    //    public string getDriversLicenseType()
    //    {
    //        return driversLicenseType;
    //    }

    //    public void setDriversLicenseType(string driversLicenseType)
    //    {
    //        this.driversLicenseType = driversLicenseType;
    //    }

    //    public string getSkype()
    //    {
    //        return skype;
    //    }

    //    public void setSkype(string skype)
    //    {
    //        this.skype = skype;
    //    }

    //    public string getSourceContactId()
    //    {
    //        return sourceContactId;
    //    }

    //    public void setSourceContactId(string sourceContactId)
    //    {
    //        this.sourceContactId = sourceContactId;
    //    }

    //    public string getContractinterval()
    //    {
    //        return contractinterval;
    //    }

    //    public void setContractinterval(string contractinterval)
    //    {
    //        this.contractinterval = contractinterval;
    //    }

    //    public string getAddress1()
    //    {
    //        return address1;
    //    }

    //    public void setAddress1(string address1)
    //    {
    //        this.address1 = address1;
    //    }

    //    public string getCity()
    //    {
    //        return city;
    //    }

    //    public void setCity(string city)
    //    {
    //        this.city = city;
    //    }

    //    public string getCountry()
    //    {
    //        return country;
    //    }

    //    public void setCountry(string country)
    //    {
    //        this.country = country;
    //    }

    //    public string getPresentJob()
    //    {
    //        return presentJob;
    //    }

    //    public void setPresentJob(string presentJob)
    //    {
    //        this.presentJob = presentJob;
    //    }

    //    public string getZipcode()
    //    {
    //        return zipcode;
    //    }

    //    public void setZipcode(string zipcode)
    //    {
    //        this.zipcode = zipcode;
    //    }

    //    public string getCurrentEmployer()
    //    {
    //        return currentEmployer;
    //    }

    //    public void setCurrentEmployer(string currentEmployer)
    //    {
    //        this.currentEmployer = currentEmployer;
    //    }

    //    public string getState()
    //    {
    //        return state;
    //    }

    //    public void setState(string state)
    //    {
    //        this.state = state;
    //    }

    //    public int getYearOfExperience()
    //    {
    //        return yearOfExperience;
    //    }

    //    public void setYearOfExperience(int yearOfExperience)
    //    {
    //        this.yearOfExperience = yearOfExperience;
    //    }

    //    public string getPersonalAddress1()
    //    {
    //        return personalAddress1;
    //    }

    //    public void setPersonalAddress1(string personalAddress1)
    //    {
    //        this.personalAddress1 = personalAddress1;
    //    }

    //    public int getIndustry()
    //    {
    //        return industry;
    //    }

    //    public void setIndustry(int industry)
    //    {
    //        this.industry = industry;
    //    }

    //    public string getPersonalAddress2()
    //    {
    //        return personalAddress2;
    //    }

    //    public void setPersonalAddress2(string personalAddress2)
    //    {
    //        this.personalAddress2 = personalAddress2;
    //    }

    //    public string getTotalPA()
    //    {
    //        return totalPA;
    //    }

    //    public void setTotalPA(string totalPA)
    //    {
    //        this.totalPA = totalPA;
    //    }

    //    public string getPersonalCity()
    //    {
    //        return personalCity;
    //    }

    //    public void setPersonalCity(string personalCity)
    //    {
    //        this.personalCity = personalCity;
    //    }

    //    public string getPersonalCountry()
    //    {
    //        return personalCountry;
    //    }

    //    public void setPersonalCountry(string personalCountry)
    //    {
    //        this.personalCountry = personalCountry;
    //    }

    //    public int getContractRate()
    //    {
    //        return contractRate;
    //    }

    //    public void setContractRate(int contractRate)
    //    {
    //        this.contractRate = contractRate;
    //    }

    //    public string getPersonalZipcode()
    //    {
    //        return personalZipcode;
    //    }

    //    public void setPersonalZipcode(string personalZipcode)
    //    {
    //        this.personalZipcode = personalZipcode;
    //    }

    //    public string getPersonalState()
    //    {
    //        return personalState;
    //    }

    //    public void setPersonalState(string personalState)
    //    {
    //        this.personalState = personalState;
    //    }


    //    public string getKeyword()
    //    {
    //        return keyword;
    //    }

    //    public void setKeyword(string keyword)
    //    {
    //        this.keyword = keyword;
    //    }

    //    public int getToeicScore()
    //    {
    //        return toeicScore;
    //    }

    //    public void setToeicScore(int toeicScore)
    //    {
    //        this.toeicScore = toeicScore;
    //    }

    //}

    public class Candidate
    {
        public int desireSalary { get; set; }
        public int candidateSourceId { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string familyName { get; set; }
        public string lastNameKana { get; set; }
        public string firstNameKana { get; set; }
        public string middleNameKana { get; set; }
        public string emergencyName { get; set; }
        public string dateOfBirth { get; set; }
        public string emergencyRelationship { get; set; }
        public string nationality { get; set; }
        public string emergencyPhone { get; set; }
        public string emergencyEmail { get; set; }
        public string email { get; set; }
        public string work_email { get; set; }
        public int currentSalary { get; set; }
        public string availabilityStart { get; set; }
        public string phone { get; set; }
        public string relocate { get; set; }
        public string phone2 { get; set; }
        public string passportNo { get; set; }
        public string home_phone { get; set; }
        public string driversLicenseNumber { get; set; }
        public string work_phone { get; set; }
        public string otherBenefits { get; set; }
        public string linked_in_profile { get; set; }
        public string driversLicenseType { get; set; }
        public string skype { get; set; }
        public string sourceContactId { get; set; }
        public string contractinterval { get; set; }
        public string address1 { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string presentJob { get; set; }
        public string zipcode { get; set; }
        public string currentEmployer { get; set; }
        public string state { get; set; }
        public int yearOfExperience { get; set; }
        public string personalAddress1 { get; set; }
        public int industry { get; set; }
        public string personalAddress2 { get; set; }
        public string totalPA { get; set; }
        public string personalCity { get; set; }
        public string personalCountry { get; set; }
        public int contractRate { get; set; }
        public string personalZipcode { get; set; }
        public string personalState { get; set; }
        public string keyword { get; set; }
        public int toeicScore { get; set; }
        public int male { get; set; }
        public string genderTitle { get; set; }
        public string skillsSummary { get; set; }
        public string educationSummary { get; set; }
        public string experienceSummary { get; set; }
        public string[] workSought { get; set; }
        public string countrySpecific { get; set; }
        public string idType { get; set; }
        public string idNumber { get; set; }
        public string politicalStatus { get; set; }
        public string hukou { get; set; }
        public string companyJson { get; set; }
        public string personalStatements { get; set; }

    }

    public class CandidateDocument
    {
        public int documentId { get; set; }
        public Boolean isPrimary { get; set; }

        public int getDocumentId()
        {
            return this.documentId;
        }

        public void setDocumentId(int documentId)
        {
            this.documentId = documentId;
        }

        public Boolean getisPrimary()
        {
            return this.isPrimary;
        }

        public void setisPrimary(Boolean isPrimary)
        {
            this.isPrimary = isPrimary;
        }
    }

    public class Education
    {
        public int education{ get; set; }
        public string school_name{ get; set; }
        public string start_date{ get; set; }
        public string graduation_date{ get; set; }
        public string degree_name{ get; set; }
        public string qualification{ get; set; }
        public string gpa{ get; set; }
        public string major{ get; set; }
        public string minor{ get; set; }

        //public int getEducation()
        //{
        //    return education;
        //}
        //public void setEducation(int education)
        //{
        //    this.education = education;
        //}
        //public string getSchool_name()
        //{
        //    return school_name;
        //}
        //public void setSchool_name(string school_name)
        //{
        //    this.school_name = school_name;
        //}
        //public string getStart_date()
        //{
        //    return start_date;
        //}
        //public void setStart_date(string start_date)
        //{
        //    this.start_date = start_date;
        //}
        //public string getGraduation_date()
        //{
        //    return graduation_date;
        //}
        //public void setGraduation_date(string graduation_date)
        //{
        //    this.graduation_date = graduation_date;
        //}
        //public string getDegree_name()
        //{
        //    return degree_name;
        //}
        //public void setDegree_name(string degree_name)
        //{
        //    this.degree_name = degree_name;
        //}
        //public string getQualification()
        //{
        //    return qualification;
        //}
        //public void setQualification(string qualification)
        //{
        //    this.qualification = qualification;
        //}
        //public string getGpa()
        //{
        //    return gpa;
        //}
        //public void setGpa(string gpa)
        //{
        //    this.gpa = gpa;
        //}
        //public string getMajor()
        //{
        //    return major;
        //}
        //public void setMajor(string major)
        //{
        //    this.major = major;
        //}
        //public string getMinor()
        //{
        //    return minor;
        //}
        //public void setMinor(string minor)
        //{
        //    this.minor = minor;
        //}
    }

    public class CNcandidate
    {
        public long jobId { get; set; }
        public Candidate candidate { get; set; }
        public CandidateDocument[] candidateDocument { get; set; }
        public Education candidateEducation { get; set; }
        public int primaryResumeId { get; set; }
        public string clientName { get; set; }

        public CNcandidate(long jobid, Candidate candidate, CandidateDocument[] document, Education candidateEducation, int primaryResumeId, string clientName)
        {
            this.jobId = jobid;
            this.candidate = candidate;
            this.candidateDocument = document;
            this.candidateEducation = candidateEducation;
            this.primaryResumeId = primaryResumeId;
            this.clientName = clientName;
        }

    }

    public class Formlist
    {
        public string type { get; set; }
        public string lang { get; set; }
        public string label { get; set; }
        public listresult[] result { get; set; }
    }

    public class listresult 
    {
        public string name { get; set; }
        public string value { get; set; }
        public string description { get; set; }

    }

    /// <summary>
    /// JSON序列化和反序列化辅助类
    /// </summary>
    public class JsonHelper
    {
        /// <summary>
        /// JSON序列化
        /// </summary>
        public static string JsonSerializer<T>(T t)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, t);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return jsonString;
        }
    
        /// <summary>
        /// JSON反序列化
        /// </summary>
        public static T JsonDeserialize<T>(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ser.ReadObject(ms);
            return obj;
        }
    }


    /// <summary>
    /// json转换Dynamic
    /// </summary>
    public class JsonParser {

        /// <summary>
        /// 从json字符串到对象。
        /// </summary>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public dynamic FromJson(string jsonStr) {

            JavaScriptSerializer jss = new JavaScriptSerializer();

            jss.RegisterConverters(new JavaScriptConverter[] { new DynamicJsonConverter() });

            dynamic glossaryEntry = jss.Deserialize(jsonStr,typeof(object)) as dynamic;

            return glossaryEntry;

        }

    }

    public class DynamicJsonConverter : JavaScriptConverter

    {

        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)

        {

            if (dictionary == null)

                throw new ArgumentNullException("dictionary");



            if (type == typeof(object))

            {

                return new DynamicJsonObject(dictionary);

            }



            return null;

        }



        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)

        {

            throw new NotImplementedException();

        }



        public override IEnumerable<Type> SupportedTypes

        {

            get { return new ReadOnlyCollection<Type>(new List<Type>(new Type[] { typeof(object) })); }

        }

    }

    public class DynamicJsonObject : DynamicObject

    {

        private IDictionary<string, object> Dictionary { get; set; }



        public DynamicJsonObject(IDictionary<string, object> dictionary)

        {

            this.Dictionary = dictionary;

        }



        public override bool TryGetMember(GetMemberBinder binder, out object result)

        {

            result = this.Dictionary[binder.Name];



            if (result is IDictionary<string, object>)

            {

                result = new DynamicJsonObject(result as IDictionary<string, object>);

            }

            else if (result is ArrayList && (result as ArrayList) is IDictionary<string, object>)

            {

                result = new List<DynamicJsonObject>((result as ArrayList).ToArray().Select(x => new DynamicJsonObject(x as IDictionary<string, object>)));

            }

            else if (result is ArrayList)

            {

                result = new List<object>((result as ArrayList).ToArray());

            }



            return this.Dictionary.ContainsKey(binder.Name);

        }

    }
}