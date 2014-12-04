using System;
using System.Collections.Specialized;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json.Linq;

/// <summary>
/// Client  class to access Vognitions voice recognition services
/// </summary>
public class Vognition
{
    // Private global variables with default values
    private string Hostname = "sample.whataremindsfor.com";
    private int Portnumber = 46900;
    private string Appkey = "59cddd076cf2a00b9f5555585c177276d8a49873";
    private string Appsecret = "01d3cb9455722c2f6760e11f14a0571179649873";
    private string Conkey = "09326eefff2cad2ae9f4c0fd5d4c7b71db249873";
    private string Consecret = "7092379b31cd621c2c1b574e47ec956733f49873";
    private string regConKey = "";
    private string ttsSpeakerType = "usenglishfemale";
    private string locale = "en-US";
    private string requestType = "ASR";
    private string HPDL_ID = "TEST_HPM_ID";
    private Roles currentRole = Roles.Friend;


    /// <summary>
    /// Vognition supports 4 types of roles: Owner, Family, Relative, Friend, Service Provider
    /// </summary>
    public enum Roles
    {
        Owner,
        Family,
        Relative,
        Friend,
        ServiceProvider
    }

    /// <summary>
    /// Used in assignRole(), gets the current role to be assigned
    /// </summary>
    /// <returns></returns>
    public Roles getRole()
    {
        return currentRole;
    }

    /// <summary>
    /// Allows user to set a different role than 'Friend' in assignRole()
    /// </summary>
    /// <param name="newRole"></param>
    public void setRole(Vognition.Roles newRole)
    {
        this.currentRole = newRole;
    }

    /// <summary>
    /// This function calls the initial sets functions for all the global private variables
    /// </summary>    
    public void DoConfigure(string hostname, int portnumber, string appkey, string appsecret, string conkey, string consecret)
    {
        SetHostname(hostname);
        SetPortnumber(portnumber);
        SetAppKey(appkey);
        SetAppSecret(appsecret);
        SetConsumerKey(conkey);
        SetConsumerSecret(consecret);
    }
					
    /// <summary>
    /// This function is used to assign the Hostname variable
    /// </summary>
    /// <param name="hostname"></param>
    public void SetHostname(string hostname) 
    {
        this.Hostname = hostname;
    }

    /// <summary>
    /// This function is used to assign the Portnumber variable
    /// </summary>
    /// <param name="portnumber"></param>
    public void SetPortnumber(int portnumber)
    {
        this.Portnumber = portnumber;
    }

    /// <summary>
    /// This function is used to assign the Appkey variable
    /// </summary>
    /// <param name="appkey"></param>
    public void SetAppKey(string appkey)
    {
        this.Appkey = appkey;
    }

    /// <summary>
    /// This function is used to assign the Appsecret variable
    /// </summary>
    /// <param name="appsecret"></param>
    public void SetAppSecret(string appsecret)
    {
        this.Appsecret = appsecret;
    }

    /// <summary>
    /// This function is used to assign the Conkey variable
    /// </summary>
    /// <param name="conkey"></param>
    public void SetConsumerKey(string conkey) 
    {
        this.Conkey = conkey;
    }

    /// <summary>
    /// This function is used to assign the Consecret variable
    /// </summary>
    /// <param name="consecret"></param>
    public void SetConsumerSecret(string consecret)
    {
        this.Consecret = consecret;
    }

    /// <summary>
    /// This function is used to assign the regConkey variable obtained 
    /// when registering a new user
    /// </summary>
    /// <param name="regConkey"></param>
    public void regSetConKey(string regConkey)
    {
        this.regConKey = regConkey;
    }

    /// <summary>
    /// This function is used to set the HPDL_ID variable
    /// </summary>
    /// <param name="HPDL_ID"></param>
    public void setHPDL_ID(string HPDL_ID)
    {
        this.HPDL_ID = HPDL_ID;
    }

    /// <summary>
    /// This function is used to get the HPDL_ID variable
    /// </summary>
    /// <returns></returns>
    public string getHPDL_ID()
    {
        return this.HPDL_ID;
    }

    /// <summary>
    /// This function is used to get the Appkey variable
    /// </summary>
    /// <returns></returns>
    public string GetAppKey()
    {
        return this.Appkey;
    }

    /// <summary>
    /// This function is used to get the Appsecret variable
    /// </summary>
    /// <returns></returns>
    public string GetAppSecret()
    {
        return this.Appsecret;
    }

    /// <summary>
    /// This function is used to get the Conkey variable
    /// </summary>
    /// <returns></returns>
    public string GetConsumerKey()
    {
        return this.Conkey;
    }

    /// <summary>
    /// This function is used to get the Consecret variable
    /// </summary>
    /// <returns></returns>
    public string GetConsumerSecret()
    {
        return this.Consecret;
    }

    /// <summary>
    /// This function is used to get the regConKey variable, set in registerUser()
    /// </summary>
    /// <returns></returns>
    public string regGetConKey()
    {
        return this.regConKey;
    }

    /// <summary>
    /// This function is used to get the Hostname variable
    /// </summary>
    /// <returns></returns>
    public string GetHostname()
    {
        return this.Hostname;
    }

    /// <summary>
    /// This function is used to get the Portnumber variable
    /// </summary>
    /// <returns></returns>
    public int GetPortnumber()
    {
        return this.Portnumber;
    }

    /// <summary>
    /// This API submits a textual sentence to be translated to a machine actionable command 
    /// assembled in Vognition. It is submitted to the API via an HTTP POST
    /// </summary>
    /// <param name="sentence"></param>
    /// <returns></returns>
    public string transText(string sentence)
    {
        // Strip any non-alphanumeric characters from the translated message
        sentence = Regex.Replace(sentence, @"[^0-9a-zA-Z ]+", "");

        NameValueCollection data = new NameValueCollection();
        // populate the NameValueCollection with Vognition parameters
        data["appkey"] = Appkey;
        data["appsecret"] = Appsecret;
        data["conkey"] = Conkey;
        data["consecret"] = Consecret;
        data["sentence"] = sentence;
        data["ttsSpeakerType"] = ttsSpeakerType;
        data["locale"] = locale;
        data["requestType"] = requestType;
        data["HPDL_ID"] = HPDL_ID;

        string transTextURL = "http://" + Hostname + ":" + Portnumber + "/apiv1/transtext";
        string retVal = "_NULL";

        // Post the data to the transText URL
        using (var wb = new WebClient())
        {
            try
            {
                var response = wb.UploadValues(transTextURL, "POST", data);
                retVal = Encoding.UTF8.GetString(response, 0, response.Length);
            }
            catch (WebException e)
            {
                retVal =  e.Message;
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }
        return retVal;
    }
   
    /// <summary>
    /// This API submits a voice file to be analyzed by the Vognition service using the iSpeech ASR. It
    /// is submitted to the API via an HTTP POST
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public string dictation(string filePath)
    {
        string appId = "NMDPTRIAL_le_richard206520141105025111";
        string appKey = "db3253ad1bdd222d25517ef496be850cf7c8d74044b6d750c09b7f4963f7593e2aaceb458a0e72af5ff0a3f5ae2802a6f0d9e6348fbd854f580499bcfcf2fa26";
        string id = "0000";
        string nuanceURL = "https://dictation.nuancemobility.net:443/NMDPAsrCmdServlet/dictation?appId=" + appId + "&appKey=" + appKey + "&id=" + id;
        string translatedMessage = "null";
        try
        {
            // handles certification
            SSLValidator.OverrideValidation();
            HttpWebRequest nuanceRequest = (HttpWebRequest)WebRequest.Create(nuanceURL);
            nuanceRequest.ProtocolVersion = HttpVersion.Version11;
            nuanceRequest.ContentType = "audio/x-wav;codec=pcm;bit=16;rate=16000";
            nuanceRequest.Accept = "text/plain";
            nuanceRequest.Method = WebRequestMethods.Http.Post;
            nuanceRequest.Headers["Accept-Language"] = "enus";
            nuanceRequest.Headers["Accept-Topic"] = "Dictation";
            nuanceRequest.Headers["X-Dictation-NBestListSize"] = "1";

            // Store the post data into the MemoryStream
            MemoryStream postDataStream = new MemoryStream();
            StreamWriter postDataWriter = new StreamWriter(postDataStream);

            // Read the file and write the bytes to the post stream
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[1024];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                postDataStream.Write(buffer, 0, bytesRead);
            }
            fileStream.Close();
            postDataWriter.Flush();

            // Set the http request body content length
            nuanceRequest.ContentLength = postDataStream.Length;

            // Dump the post data from the memory stream to the request stream
            using (Stream s = nuanceRequest.GetRequestStream())
            {
                postDataStream.WriteTo(s);
            }
            postDataStream.Close();

            // Get the response from the server
            StreamReader responseReader = new StreamReader(nuanceRequest.GetResponse().GetResponseStream());
            translatedMessage = responseReader.ReadToEnd();
        }
        catch (WebException e)
        {
            translatedMessage = e.Message;
        }
        catch (FileNotFoundException file)
        {
            Console.WriteLine(file.Message);
            throw new FileNotFoundException();
        }
        return translatedMessage;
    }

    /// <summary>
    /// Registers a user associated to the application ID
    /// </summary>
    /// <returns></returns>
    public string createUser()
    {
        NameValueCollection data = new NameValueCollection();
        data["appkey"] = Appkey;
        data["appsecret"] = Appsecret;

        string userURL = "http://" + Hostname + ":" + Portnumber + "/apiv1/user";
        string retVal = "_NULL";

        // Post the data to the transText URL
        using (var wb = new WebClient())
        {
            try
            {
                var response = wb.UploadValues(userURL, "POST", data);
                retVal = Encoding.UTF8.GetString(response, 0, response.Length);
            }
            catch (WebException e)
            {
                retVal = e.Message;
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }
        regSetConKey((string)JObject.Parse(retVal).SelectToken("conkey"));
        return retVal;
    }

    /// <summary>
    /// Creates a home profile ID
    /// </summary>
    /// <returns></returns>
    public string createHomeProfileID()
    {
        NameValueCollection data = new NameValueCollection();
        data["appkey"] = Appkey;
        data["appsecret"] = Appsecret;
        data["conkey"] = Conkey;
        data["consecret"] = Consecret;
        //data["HPDL_ID"] = HPDL_ID;
        //data["roleeKey"] = regGetConKey();

        string createHomeProfileIDURL = "http://" + Hostname + ":" + Portnumber + "/apiv1/homeprofile";
        string retVal = "_NULL";

        // Post the data to the transText URL
        using (var wb = new WebClient())
        {
            try
            {
                var response = wb.UploadValues(createHomeProfileIDURL, "POST", data);
                retVal = Encoding.UTF8.GetString(response, 0, response.Length);
            }
            catch (WebException e)
            {
                retVal = e.Message;
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }
        setHPDL_ID( (string)JObject.Parse(retVal).SelectToken("HPDL_ID") );
        return retVal;
    }

    /// <summary>
    /// Creates a home profile XML that defines the existence 
    /// of devices and service to utilize
    /// </summary>
    /// <returns></returns>
    public string uploadHomeProfile()
    {
        NameValueCollection data = new NameValueCollection();
        data["appkey"] = "59cddd076cf2a00b9f5555585c177276d8a49873";
        data["appsecret"] = "01d3cb9455722c2f6760e11f14a0571179649873";
        data["conkey"] = "09326eefff2cad2ae9f4c0fd5d4c7b71db249873";
        data["consecret"] = "7092379b31cd621c2c1b574e47ec956733f49873";
        data["HPDL_ID"] = HPDL_ID;

        string HomeProfileID_URL = "http://" + Hostname + ":" + Portnumber + "/apiv1/homeprofile";
        string filePath = @"C:\Users\Richard\Desktop\TEST_HPM_ID.hpdgl.xml";
        string fileName = "TEST_HPM_ID.hpdgl.xml";
        byte[] file = File.ReadAllBytes(filePath);
        string boundary = System.Guid.NewGuid().ToString();

        HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(HomeProfileID_URL);
        request.KeepAlive = true;
        request.ContentType = "text/plain";
        request.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);
        request.Method = "PUT";

        // Write parameters and values for request
        StringBuilder contents = new StringBuilder();
        foreach(string param in data)
        {
            contents.AppendLine(boundary);
            contents.AppendLine(string.Format("Content-Disposition: form-data; name=\"{0}\"", param));
            contents.AppendLine();
            contents.AppendLine(data[param]);
        }

        // file
        contents.AppendLine(boundary);
        contents.AppendLine(string.Format("Content-Disposition: form-data; name=\"content\"; filename=\"{0}\"", fileName));
        contents.AppendLine("Content-Type: text/xml");
        contents.AppendLine();
        contents.AppendLine(Encoding.UTF8.GetString(file));
        contents.AppendLine(boundary);

        // Execute the post request
        byte[] bytes = Encoding.UTF8.GetBytes(contents.ToString());
        request.ContentLength = bytes.Length;
        string retVal = "null";
        using (Stream requestStream = request.GetRequestStream())
        {
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Flush();
            requestStream.Close();
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        retVal = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException e)
            {
                retVal = e.Message;
            }
        }
        return retVal;
    }

    /// <summary>
    /// Removes a home profile from all users
    /// </summary>
    /// <returns></returns>
    public string removeHomeProfile()
    {
        NameValueCollection data = new NameValueCollection();
        data["appkey"] = Appkey;
        data["appsecret"] = Appsecret;
        data["conkey"] = Conkey;
        data["consecret"] = Consecret;
        data["HPDL_ID"] = HPDL_ID;

        string HomeProfileID_URL = "http://" + Hostname + ":" + Portnumber + "/apiv1/homeprofile";
        string retVal = "_NULL";

        // Post the data to the transText URL
        using (var wb = new WebClient())
        {
            try
            {
                var response = wb.UploadValues(HomeProfileID_URL, "DELETE", data);
                retVal = Encoding.UTF8.GetString(response, 0, response.Length);
            }
            catch (WebException e)
            {
                retVal = e.Message;
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }
        return retVal;
    }

    /// <summary>
    /// Creates a role to associate a different user to the homeprofile of the owner
    /// </summary>
    /// <returns></returns>
    public string assignRole()
    {
        NameValueCollection data = new NameValueCollection();
        data["appkey"] = Appkey;
        data["appsecret"] = Appsecret;
        data["conkey"] = Conkey;
        data["consecret"] = Consecret;
        data["HPDL_ID"] = HPDL_ID;
        data["roleeKey"] = regGetConKey();
        data["role"] = currentRole.ToString();

        string roleURL = "http://" + Hostname + ":" + Portnumber + "/apiv1/role";
        string retVal = "_NULL";

        // Post the data to the role URL
        using (var wb = new WebClient())
        {
            try
            {
                var response = wb.UploadValues(roleURL, "POST", data);
                retVal = Encoding.UTF8.GetString(response, 0, response.Length);
            }
            catch (WebException e)
            {
                retVal = e.Message;
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }
        return retVal;
    }

    /// <summary>
    /// Owner of the home profile can change the privileges of the user
    /// </summary>
    /// <returns></returns>
    public string changeRole()
    {
        NameValueCollection data = new NameValueCollection();
        data["appkey"] = Appkey;
        data["appsecret"] = Appsecret;
        data["conkey"] = Conkey;
        data["consecret"] = Consecret;
        data["HPDL_ID"] = HPDL_ID;
        data["accept"] = "1";

        string changeRoleURL = "http://" + Hostname + ":" + Portnumber + "/apiv1/role";
        string retVal = "_NULL";

        // Put the data to the role URL
        using (var wb = new WebClient())
        {
            try
            {
                var response = wb.UploadValues(changeRoleURL, "PUT", data);
                retVal = Encoding.UTF8.GetString(response, 0, response.Length);
            }
            catch (WebException e)
            {
                retVal = e.Message;
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }
        return retVal;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="regConKey"></param>
    /// <returns></returns>
    public string deleteRole()
    {
        NameValueCollection data = new NameValueCollection();
        data["appkey"] = Appkey;
        data["appsecret"] = Appsecret;
        data["conkey"] = Conkey;
        data["consecret"] = Consecret;
        data["HPDL_ID"] = HPDL_ID;
        data["roleeKey"] = regConKey;

        string transTextURL = "http://" + Hostname + ":" + Portnumber + "/apiv1/role";
        string retVal = "_NULL";

        // Delete the data from the role URL
        using (var wb = new WebClient())
        {
            try
            {
                var response = wb.UploadValues(transTextURL, "DELETE", data);
                retVal = Encoding.UTF8.GetString(response, 0, response.Length);
            }
            catch (WebException e)
            {
                retVal = e.Message;
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }
        return retVal;
    }

    /// <summary>
    /// User accepts a role access to another home profile
    /// </summary>
    /// <returns></returns>
    public string acceptRole()
    {
        NameValueCollection data = new NameValueCollection();
        data["appkey"] = Appkey;
        data["appsecret"] = Appsecret;
        data["conkey"] = Conkey;
        data["consecret"] = Consecret;
        data["HPDL_ID"] = HPDL_ID;
        data["accept"] = "1";

        string acceptRoleURL = "http://" + Hostname + ":" + Portnumber + "/apiv1/assignedrolee";
        string retVal = "_NULL";

        // Post the data to the assignedRolee URL
        using (var wb = new WebClient())
        {
            try
            {
                var response = wb.UploadValues(acceptRoleURL, "POST", data);
                retVal = Encoding.UTF8.GetString(response, 0, response.Length);
            }
            catch (WebException e)
            {
                retVal = e.Message;
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }
        return retVal;
    }

    /// <summary>
    /// User removes themselves from a role access to another home profile
    /// </summary>
    /// <returns></returns>
    public string unassignRole()
    {
        NameValueCollection data = new NameValueCollection();
        data["appkey"] = Appkey;
        data["appsecret"] = Appsecret;
        data["conkey"] = Conkey;
        data["consecret"] = Consecret;
        data["HPDL_ID"] = HPDL_ID;

        string unassignRoleURL = "http://" + Hostname + ":" + Portnumber + "/apiv1/assignedrolee";
        string retVal = "_NULL";

        // Delete the data from the assignedRolee URL
        using (var wb = new WebClient())
        {
            try
            {
                var response = wb.UploadValues(unassignRoleURL, "DELETE", data);
                retVal = Encoding.UTF8.GetString(response, 0, response.Length);
            }
            catch (WebException e)
            {
                retVal = e.Message;
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }
        return retVal;
    }

    /// <summary>
    /// Inner class called to handle HttpWebRequest certification
    /// </summary>
    public static class SSLValidator
    {
        private static bool OnValidateCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
        public static void OverrideValidation()
        {
            ServicePointManager.ServerCertificateValidationCallback = OnValidateCertificate;
            ServicePointManager.Expect100Continue = true;
        }
    }


}