using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
 
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
using System.Net.Mail;
using Recon.Controllers;
using System.Security.Authentication;

namespace Recon.Controllers
{
    public class LoginController : Controller
    {

        CommonController objcommon = new CommonController();
        // GET: Login
        string url = ConfigurationManager.AppSettings["URL"].ToString();
       //string token = ConfigurationManager.AppSettings["token"].ToString();
        string ipAddress = "";
        public ActionResult Login()
        {
           // objcommon.getToken();
            return View();
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        public ActionResult ForgetPassword()
        {
            return View();
        }

        public ActionResult changepassword_Save(string user_gid, string oldpass, string newpass)
        {
            try
            {

                //var pass = EncryptText(newpass);
                var oldpassword = Encrypt(oldpass);
                var pass = Encrypt(newpass);
                user_model usermodel = new user_model();
                usermodel.oldpassworrd = oldpassword;
                usermodel.newpassword = pass;
                usermodel.user_gid = Convert.ToInt32(user_gid); ;
                usermodel.dbsource = "MYSQL";
                using (var client = new HttpClient())
                {
                    string[] result = { };
                    try
                    {
                        string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(usermodel), "changepass_save");
                        result = (string[])JsonConvert.DeserializeObject(post_data, result.GetType());
                    }
                    catch (Exception ex)
                    {
                        string control = this.ControllerContext.RouteData.Values["controller"].ToString();
                        LogHelper.WriteLog(ex.ToString(), control);
                    }

                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString();
                LogHelper.WriteLog(ex.ToString(), control);
            }
            return View();
        }

        public ActionResult Login_validation(string UserName, string Password)
        {

            List<user_model> objcat_lst = new List<user_model>();
            try
            {
                string hostName = Dns.GetHostName(); // Retrive the Name of HOST 
                //LogHelper.WriteLog("ipAddress", "LoginController");
                ipAddress = Dns.GetHostByName(hostName).AddressList[0].ToString();

                //LogHelper.WriteLog(ipAddress,"LoginController");
                DataTable result = new DataTable();
                //var pass = EncryptText(Password);
               // LogHelper.WriteLog("beforePassword Encrypt" + Password, "LoginController");
                
                   
                var pass = Encrypt(Password);

                //LogHelper.WriteLog("Password Encrypt"+pass, "LoginController");
              
                Login_model loginmodel = new Login_model();
                loginmodel.user_id = UserName;
                loginmodel.password = pass;
                loginmodel.ip = ipAddress;

                //LogHelper.WriteLog("UserName" + UserName, "LoginController");
                //LogHelper.WriteLog("pass" + pass, "LoginController");
                //LogHelper.WriteLog("ipAddress" + ipAddress, "LoginController");
             
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(loginmodel), "Loginvalidation");

               // LogHelper.WriteLog("post_data" + post_data, "LoginController");

                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
                LogHelper.WriteLog("result"+result.Rows.Count.ToString(), "LoginController");
                for (int i = 0; i < result.Rows.Count; i++)
                {

                    user_model objcat = new user_model();
                    objcat.user_gid = Convert.ToInt32(result.Rows[i]["user_gid"]);
                    objcat.user_name = result.Rows[i]["user_name"].ToString();
                   // objcat.user_code = result.Rows[i]["user_code"].ToString();
                  //  objcat.user_emailid = result.Rows[i]["user_emailid"].ToString();
                    objcat.passwordexpdate = result.Rows[i]["password_expiry_date"].ToString();
                    objcat.usergroup_gid = Convert.ToInt32(result.Rows[i]["usergroup_gid"]);
                   // objcat.passwordreset = result.Rows[i]["password_reset_flag"].ToString();
                    objcat.result = Convert.ToInt32(result.Rows[i]["out_result"]);
                    objcat.msg = result.Rows[i]["out_msg"].ToString();
                    objcat.oldpassworrd = Decrypt(pass);
                    objcat.user_status = result.Rows[i]["user_status"].ToString();
                    objcat_lst.Add(objcat);
                    /*
                    Configuration webConfigApp = WebConfigurationManager.OpenWebConfiguration("~");
                    //Modifying the AppKey from AppValue to AppValue1
                    webConfigApp.AppSettings.Settings["usercode"].Value = result.Rows[i]["user_name"].ToString();
                    webConfigApp.AppSettings.Settings["user_code"].Value = result.Rows[i]["user_gid"].ToString();
                    webConfigApp.AppSettings.Settings["usergroup_code"].Value = result.Rows[i]["usergroup_gid"].ToString();
                    //Save the Modified settings of AppSettings.
                    webConfigApp.Save();
                    */
                   //System.Web.HttpContext.Current.Session["usercode"] = result.Rows[i]["user_code"].ToString();
                    System.Web.HttpContext.Current.Session["usercode"] = UserName;
                    System.Web.HttpContext.Current.Session["username"] = result.Rows[i]["user_name"].ToString();
                    System.Web.HttpContext.Current.Session["mindate"] = result.Rows[i]["min_tran_date"].ToString();
                    System.Web.HttpContext.Current.Session["fin_date"] = result.Rows[i]["fin_start_date"].ToString();
                   // System.Web.HttpContext.Current.Session["usercodes"] = result.Rows[i]["user_code"].ToString();
                   // System.Web.HttpContext.Current.Session["usermail"] = result.Rows[i]["user_emailid"].ToString();
                    System.Web.HttpContext.Current.Session["user_code"] = result.Rows[i]["user_gid"].ToString();
                    System.Web.HttpContext.Current.Session["usergroup_code"] = result.Rows[i]["usergroup_gid"].ToString();
                    System.Web.HttpContext.Current.Session["userrole"] = "ADMIN";

                }
               // LogHelper.WriteLog("Testsuccess", "LoginController");
            }
             catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString();
                LogHelper.WriteLog(ex.ToString(),control);
            }
            return Json(objcat_lst, JsonRequestBehavior.AllowGet);
        }
        private string Encrypt(string clearText)
        {
            try
            {
                string EncryptionKey = "MAKV2SPBNI99212";
                byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                            cs.Close();
                        }
                        clearText = Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return clearText;
        }
        private string Decrypt(string cipherText)
        {
            try
            {
                string EncryptionKey = "MAKV2SPBNI99212";
                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        cipherText = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return cipherText;
        }
        public ActionResult Forgot_pass(string UserName)
        {
            string message = "";
            try
            {
                string hostName = Dns.GetHostName(); // Retrive the Name of HOST 
                ipAddress = Dns.GetHostByName(hostName).AddressList[0].ToString();
                List<user_model> objcat_lst = new List<user_model>();
                DataTable result = new DataTable();

                Login_model Forgot_pass = new Login_model();
                Forgot_pass.user_id = UserName;
                Forgot_pass.ip = ipAddress;

                using (var client = new HttpClient())
                {
                    string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(Forgot_pass), "forgotpassword");
                    result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
                    for (int i = 0; i < result.Rows.Count; i++)
                    {
                        user_model objcat = new user_model();
                        objcat.user_emailid = result.Rows[i]["user_emailid"].ToString();
                        var password = result.Rows[i]["user_password"].ToString();
                        objcat.oldpassworrd = Decrypt(password);
                        objcat_lst.Add(objcat);
                        objcat.FromEmailId = System.Configuration.ConfigurationManager.AppSettings["FromEmailId"];
                        objcat.Password = System.Configuration.ConfigurationManager.AppSettings["Password"];
                        objcat.SMTPPort = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SMTPPort"]);
                        objcat.Host = System.Configuration.ConfigurationManager.AppSettings["Host"];

                        if (objcat.user_emailid != "")
                        {
                            var sub = "Password";
                            var body = objcat.oldpassworrd;
                            var smtp = new SmtpClient
                            {
                                Host = objcat.Host,
                                Port = objcat.SMTPPort,
                                EnableSsl = true,
                                DeliveryMethod = SmtpDeliveryMethod.Network,
                                UseDefaultCredentials = false,
                                Credentials = new NetworkCredential(objcat.FromEmailId, objcat.Password)
                            };
                            using (var mess = new MailMessage(objcat.FromEmailId, objcat.user_emailid)
                            {
                                Subject = sub,
                                Body = body
                            })
                            {
                                smtp.Send(mess);
                                message = "Mail Sended Sucessfully";
                            }
                        }
                        else
                        {
                            message = "failed";
                        }
                    }
                }
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                message = error;
                string control = this.ControllerContext.RouteData.Values["controller"].ToString();
                LogHelper.WriteLog(ex.ToString(), control);
                return View();
            }
        }

        public ActionResult session_expires()
       {
            

            return View();
        }

     
    }

}
