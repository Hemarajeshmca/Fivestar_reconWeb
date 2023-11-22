
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Recon_Model;
using System.Data;
using System.Net.Http.Headers;
using System.IO;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Configuration;
using System.Web.Configuration;
using System.Net;
using System.Security.Cryptography;
using System.Net.Http.Formatting;
using System.Security.Authentication;


namespace Recon.Controllers
{
    public class CommonController : Controller
    {
        string url = ConfigurationManager.AppSettings["URL"].ToString();
        // string token = ConfigurationManager.AppSettings["token"].ToString();
        string ipAddress = "";
        // GET: Common
        public ActionResult Index()
        {
            return View();
        }

        public string getApiResult(string SerializedString, string ApiMethodName)
        {
        L1: string post_data = "";
            try
            {
                // string token = (string)Session["token_"].ToString();
                //string token = System.Web.HttpContext.Current.Session["token_"].ToString();

                string token = Convert.ToString(System.Web.HttpContext.Current.Session["token_"]);
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(SerializedString, UTF8Encoding.UTF8, "application/json");
                    content.Headers.Add("user_code", "12345");
                    var response = client.PostAsync(ApiMethodName, content).Result;
                    if (response.StatusCode.ToString() == "Unauthorized")
                    {
                        getToken();
                        goto L1;
                    }
                    else
                    {
                        Stream data = response.Content.ReadAsStreamAsync().Result;
                        StreamReader reader = new StreamReader(data);
                        post_data = reader.ReadToEnd();
                    }

                }

            }
            catch (Exception ex)
            {
                if (ex.Message == "Object reference not set to an instance of an object.")
                {
                    getToken();
                    goto L1;
                }
                else
                {
                    string control = this.ControllerContext.RouteData.Values["controller"].ToString(); LogHelper.WriteLog(ex.ToString(), control);
                }
            }
            return post_data;
        }

        [HttpGet]
        public void getToken()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var form = new Dictionary<string, string>
                       {
                           {"grant_type", "password"},
                           {"username", "user"},
                           {"password", "user"},
                       };
                    var tokenResponse = client.PostAsync(url + "/token", new FormUrlEncodedContent(form)).Result;
                    var token = tokenResponse.Content.ReadAsAsync<Recon_Model.Token>(new[] { new JsonMediaTypeFormatter() }).Result;
                    string tokenval = token.access_token;
                    System.Web.HttpContext.Current.Session["token_"] = tokenval.ToString();
                    //ConfigurationManager.AppSettings["token"] = tokenval;
                }
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString();
                LogHelper.WriteLog(ex.ToString(), control);
            }
        }

        public string getApiResult_get(string ApiMethodName)
        {
        L1: string post_data = "";
            try
            {
                // string token = (string)Session["token_"].ToString();
                string token = System.Web.HttpContext.Current.Session["token_"].ToString();
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    // client.DefaultRequestHeaders.Add("user_code","12345");
                    // HttpContent content = new StringContent(SerializedString, UTF8Encoding.UTF8, "application/json");
                    var response = client.GetAsync(ApiMethodName).Result;
                    if (response.StatusCode.ToString() == "Unauthorized")
                    {
                        getToken();
                        goto L1;
                    }
                    else
                    {
                        Stream data = response.Content.ReadAsStreamAsync().Result;
                        StreamReader reader = new StreamReader(data);
                        post_data = reader.ReadToEnd();
                    }

                }

            }
            catch (Exception ex)
            {
                if (ex.Message == "Object reference not set to an instance of an object.")
                {
                    getToken();
                    goto L1;
                }
                else
                {
                    string control = this.ControllerContext.RouteData.Values["controller"].ToString(); LogHelper.WriteLog(ex.ToString(), control);
                }
            }
            return post_data;
        }

    }
}