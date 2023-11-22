using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
 
using Recon_Model;
using System.Data;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using Newtonsoft.Json;
using System.Text;
using System.Globalization;
using System.Configuration;
using System.Net;
using System.Text.RegularExpressions;

namespace Recon.Controllers
{
    public class ProgressController : Controller
    {
        string url = Convert.ToString(ConfigurationManager.AppSettings["URL"]);
        string urldownload = Convert.ToString(ConfigurationManager.AppSettings["fileDownloadURL"]);
        string user_codes = Convert.ToString(System.Web.HttpContext.Current.Session["usercode"]);
        string token = Convert.ToString(System.Web.HttpContext.Current.Session["token_"]);
        public static string hostName = Dns.GetHostName(); // Retrive the Name of HOST 
        string ipAddress = Dns.GetHostByName(hostName).AddressList[0].ToString();


        CommonController objcommon = new CommonController();
        // GET: Progress
        public ActionResult ProgressView()
        {
            return View();
        }
        public ActionResult PostSupFile()
        {
            ViewBag.ip = ipAddress;
            ViewBag.Message = token;
            ViewBag.url = url;
            return View();
        }
        public ActionResult ProcessCompleted()
        {
            return View();
        }
        public ActionResult UndoJob()
        {
            ViewBag.ip = ipAddress;
            ViewBag.Message = token;
            ViewBag.url = url;
            return View();
        }
        public ActionResult PostSupFilemapping()
        {
            return View();
        }
        public ActionResult PostSupFilemappingView()
        {
            return View();
        }
        public ActionResult RecontypeGrid_Read([DataSourceRequest]DataSourceRequest request)   // Recon type  Read
        {
            List<Recontype_model> objcat_lst = new List<Recontype_model>();
            DataTable result = new DataTable();
           Recontype_model RecontypeList= new Recontype_model();
           RecontypeList.user_code = user_codes;
            try
            {
                using (var client = new HttpClient())
                {
                    try
                    {
                        client.BaseAddress = new Uri(url);
                        client.DefaultRequestHeaders.Accept.Clear(); client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        HttpContent content = new StringContent(JsonConvert.SerializeObject(RecontypeList), UTF8Encoding.UTF8, "application/json");
                        var response = client.PostAsync("RecontypeList", content).Result;
                        Stream data = response.Content.ReadAsStreamAsync().Result;
                        StreamReader reader = new StreamReader(data);
                        string post_data = reader.ReadToEnd();
                        result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
                        

                        for (int i = 0; i < result.Rows.Count; i++)
                        {
                            Recontype_model objcat = new Recontype_model();
                            objcat.recon_id = Convert.ToInt32(result.Rows[i]["recon_gid"]);
                            objcat.ReconName = result.Rows[i]["recon_name"].ToString();                          
                            objcat.MappingType_code = result.Rows[i]["mapping_type"].ToString();
                            objcat.MappingType_name = result.Rows[i]["mappingtype_desc"].ToString();
                            objcat.period_from = result.Rows[i]["period_from"].ToString();
                            objcat.period_to = result.Rows[i]["period_to"].ToString();
                            objcat.Status_code = result.Rows[i]["active_status"].ToString();
                            objcat.status_desc = result.Rows[i]["active_status_desc"].ToString();
                            objcat.Recon_type = result.Rows[i]["recon_type"].ToString();
                            objcat.account1_desc = result.Rows[i]["acc_no1_desc"].ToString();
                            objcat.account2_desc = result.Rows[i]["acc_no2_desc"].ToString();
                            objcat.Account1 = result.Rows[i]["acc_no1"].ToString();
                            objcat.Account2 = result.Rows[i]["acc_no2"].ToString();
                            string recontallied_flag = result.Rows[i]["recon_tallied"].ToString();

                            if (recontallied_flag == "Y")
                            {
                                objcat.recon_tallied = "Yes";
                            }
                            else
                            {
                                objcat.recon_tallied = "No";
                            }

                            string untillactive = "";
                            untillactive = result.Rows[i][12].ToString();
                            if (untillactive == "Y")
                            {
                                objcat.untillactive = "true";
                            }

                            objcat_lst.Add(objcat);
                        }
                    }
                    catch (Exception ex)
                    {
                        string control = this.ControllerContext.RouteData.Values["controller"].ToString();LogHelper.WriteLog(ex.ToString(), control);
                    }
                    return Json(objcat_lst.ToDataSourceResult(request));
                    //return Json(result1);
                }
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString();LogHelper.WriteLog(ex.ToString(), control);
            }
            return View();
        }
        public ActionResult postSupportfilecreate(string period_from,string period_to,string recon_gid,string user_code)
        {
            try
            {
                ProgressView_model postsupfile = new ProgressView_model();
                postsupfile.period_from = period_from;
                postsupfile.period_to = period_to;
                postsupfile.user_code = user_code;
                postsupfile.recon_gid = Convert.ToInt32(recon_gid);
                using (var client = new HttpClient())
                {
                    string result = "";
                    try
                    {
                        client.BaseAddress = new Uri(url);
                        client.DefaultRequestHeaders.Accept.Clear(); client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        HttpContent content = new StringContent(JsonConvert.SerializeObject(postsupfile), UTF8Encoding.UTF8, "application/json");
                        var response = client.PostAsync("postSupportfilecreate", content).Result;
                        Stream data = response.Content.ReadAsStreamAsync().Result;
                        StreamReader reader = new StreamReader(data);
                        string post_data = reader.ReadToEnd();
                        result = (string)JsonConvert.DeserializeObject(post_data, result.GetType());
                    }
                    catch (Exception ex)
                    {
                        string control = this.ControllerContext.RouteData.Values["controller"].ToString();LogHelper.WriteLog(ex.ToString(), control);
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
        public ActionResult ReportProcessLog_Read([DataSourceRequest]DataSourceRequest request, string Jobtype, string Fromdate,string Todate, string Job_status)   // ProgressView  Read
        {
            List<Job_model> objcat_lst = new List<Job_model>();
            DataTable result = new DataTable();
            Job_model ProgressViewList = new Job_model();
            ProgressViewList.Job_initiated_by = user_codes;
            ProgressViewList.Job_status = Job_status;
            ProgressViewList.Job_type = Jobtype;
            if (Fromdate != null && Todate != null && Todate != "" && Fromdate != ""){
                ProgressViewList.Job_start_date = Fromdate.Split('-')[2] + '-' + Fromdate.Split('-')[1] + '-' + Fromdate.Split('-')[0];
                ProgressViewList.Job_end_date = Todate.Split('-')[2] + '-' + Todate.Split('-')[1] + '-' + Todate.Split('-')[0];
            }
            

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear(); client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpContent content = new StringContent(JsonConvert.SerializeObject(ProgressViewList), UTF8Encoding.UTF8, "application/json");
                var response = client.PostAsync("ProgressViewList", content).Result;
                Stream data = response.Content.ReadAsStreamAsync().Result;
                StreamReader reader = new StreamReader(data);
                string post_data = reader.ReadToEnd();
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
                
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    Job_model objcat = new Job_model();
                    objcat.Job_gid = Convert.ToInt32(result.Rows[i]["job_gid"]);
                    objcat.Job_type = result.Rows[i]["job_type"].ToString();
                    objcat.Job_type_desc = result.Rows[i]["jobtype_desc"].ToString();
                    objcat.Job_ref_gid = Convert.ToInt32(result.Rows[i]["job_ref_gid"].ToString());
                    objcat.Job_name = result.Rows[i]["job_name"].ToString();
                    objcat.Job_status = result.Rows[i]["job_status"].ToString();
                    objcat.Job_status_desc = result.Rows[i]["jobstatus_desc"].ToString();
                    objcat.Job_ip_addr = result.Rows[i]["ip_addr"].ToString();
                    objcat.Job_initiated_by = result.Rows[i]["job_initiated_by"].ToString();
                    objcat.Job_start_date = result.Rows[i]["start_date"].ToString();
                    objcat.Job_end_date = result.Rows[i]["end_date"].ToString();
                    objcat.Job_remark = result.Rows[i]["job_remark"].ToString();
                    objcat.File_path = result.Rows[i]["file_path"].ToString();
                    objcat_lst.Add(objcat);
                }
                //Data1 = JsonConvert.SerializeObject(result);
            }

            return Json(objcat_lst.ToDataSourceResult(request));

        }
        public ActionResult SupFilemapping_Read([DataSourceRequest]DataSourceRequest request, string tran_gid)   // SupFilemapping  Read
        {
            List<ManualMatchoff_model> objcat_lst = new List<ManualMatchoff_model>();
            DataTable result = new DataTable();
            {
                try
                {
                    ManualMatchoff_model ManualMatch = new ManualMatchoff_model();
                    ManualMatch.tran_gid = Convert.ToInt32(tran_gid);
                    ManualMatch.user_code = user_codes;
                    using (var client = new HttpClient())
                    {

                        try
                        {
                            client.BaseAddress = new Uri(url);
                            client.DefaultRequestHeaders.Accept.Clear(); client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            HttpContent content = new StringContent(JsonConvert.SerializeObject(ManualMatch), UTF8Encoding.UTF8, "application/json");
                            var response = client.PostAsync("SupFilemappingRead", content).Result;
                            Stream data = response.Content.ReadAsStreamAsync().Result;
                            StreamReader reader = new StreamReader(data);
                            string post_data = reader.ReadToEnd();
                            result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
                            for (int i = 0; i < result.Rows.Count; i++)
                            {
                                ManualMatchoff_model objcat = new ManualMatchoff_model();
                                objcat.file_gid = Convert.ToInt32(result.Rows[i]["file_gid"]);
                                objcat.file_name = result.Rows[i]["file_name"].ToString();
                                objcat.acc_no = result.Rows[i]["acc_no"].ToString();
                                objcat.tranbrkptype_name = result.Rows[i]["tranbrkptype_name"].ToString();
                                objcat.excp_amount = result.Rows[i]["excp_amount"].ToString();
                                objcat.tran_gid = Convert.ToInt32(result.Rows[i]["tran_gid"]);
                                objcat.cnt = result.Rows[i]["cnt"].ToString();                               
                                objcat_lst.Add(objcat);
                            }
                        }

                        catch (Exception ex)
                        {
                            string control = this.ControllerContext.RouteData.Values["controller"].ToString();LogHelper.WriteLog(ex.ToString(), control);
                        }
                        var jsonResult = Json(objcat_lst.ToDataSourceResult(request)); ;
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
                }
                catch (Exception ex)
                {
                    string control = this.ControllerContext.RouteData.Values["controller"].ToString();LogHelper.WriteLog(ex.ToString(), control);
                }
                return View();
            }
        }
        public ActionResult SupFilemapping_check(string tran_gid)
        {
            try
            {
                ManualMatchoff_model ManualMatch = new ManualMatchoff_model();
                ManualMatch.tran_gid = Convert.ToInt32(tran_gid);
                ManualMatch.user_code = user_codes;
                using (var client = new HttpClient())
                {
                    string[] result = { };
                    try
                    {
                        client.BaseAddress = new Uri(url);
                        client.DefaultRequestHeaders.Accept.Clear(); client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        HttpContent content = new StringContent(JsonConvert.SerializeObject(ManualMatch), UTF8Encoding.UTF8, "application/json");
                        var response = client.PostAsync("SupFilemappingcheck", content).Result;
                        Stream data = response.Content.ReadAsStreamAsync().Result;
                        StreamReader reader = new StreamReader(data);
                        string post_data = reader.ReadToEnd();
                        result = (string[])JsonConvert.DeserializeObject(post_data, result.GetType());
                    }
                    catch (Exception ex)
                    {
                        string control = this.ControllerContext.RouteData.Values["controller"].ToString();LogHelper.WriteLog(ex.ToString(), control);
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

        [HttpPost]
        public ActionResult SupFilemapping_create(progressmodel1 ManualMatchoff)
        {
            ManualMatchoff.user_code = user_codes;
            try
            {
                using (var client = new HttpClient())
                {
                    string[] result = { };
                    try
                    {
                        client.BaseAddress = new Uri(url);
                        client.DefaultRequestHeaders.Accept.Clear(); client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        HttpContent content = new StringContent(JsonConvert.SerializeObject(ManualMatchoff), UTF8Encoding.UTF8, "application/json");
                        var response = client.PostAsync("SupFilemappingcreate", content).Result;
                        Stream data = response.Content.ReadAsStreamAsync().Result;
                        StreamReader reader = new StreamReader(data);
                        string post_data = reader.ReadToEnd();
                        result = (string[])JsonConvert.DeserializeObject(post_data, result.GetType());
                    }
                    catch (Exception ex)
                    {
                        string control = this.ControllerContext.RouteData.Values["controller"].ToString();LogHelper.WriteLog(ex.ToString(), control);
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

        public ActionResult undomatch(int Job_gid = 0,string _knockoffreason="")
        {
            try
            {
                ManualMatchoff_model ManualMatch = new ManualMatchoff_model();
                ManualMatch.jobgid = Convert.ToInt32(Job_gid);
                ManualMatch.knockoffreason = _knockoffreason;
                ManualMatch.user_code = user_codes;
                using (var client = new HttpClient())
                {
                    string[] result = { };
                    try
                    {
                        client.BaseAddress = new Uri(url);
                        client.DefaultRequestHeaders.Accept.Clear(); client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        HttpContent content = new StringContent(JsonConvert.SerializeObject(ManualMatch), UTF8Encoding.UTF8, "application/json");
                        var response = client.PostAsync("Undomatchoff", content).Result;
                        Stream data = response.Content.ReadAsStreamAsync().Result;
                        StreamReader reader = new StreamReader(data);
                        string post_data = reader.ReadToEnd();
                        result = (string[])JsonConvert.DeserializeObject(post_data, result.GetType());
                    }
                    catch (Exception ex)
                    {
                        string control = this.ControllerContext.RouteData.Values["controller"].ToString(); LogHelper.WriteLog(ex.ToString(), control);
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

       
        public void DownloadFile(int Job_gid = 0, String Job_name = "", String filePath = "")
        {
            string Result = "";
            
            FileDownload_modal FileDownloadgrid = new FileDownload_modal();
            FileDownloadgrid.jobGid =Job_gid;
            FileDownloadgrid.jobName =  Job_name;
            FileDownloadgrid.filePath = filePath.Replace("'", ""); //"D:/DMSdinesh/ReconfiledownloadTest/file";// //// "D:/DMSdinesh/ReconfiledownloadTest/file"; //
            try
            {
               // string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(FileDownloadgrid), "FileDownload");

                string token = System.Web.HttpContext.Current.Session["token_"].ToString();
                string control = this.ControllerContext.RouteData.Values["controller"].ToString();
                LogHelper.WriteLog("Token"+token, control);
                using (var client = new HttpClient())
                {
                    string[] result = { };
                  
                    LogHelper.WriteLog("Beforeurl", control);
                    client.BaseAddress = new Uri(urldownload);

                    LogHelper.WriteLog("urldownload" + urldownload, control);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //var json = JsonConvert.SerializeObject(FileDownloadgrid);
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(FileDownloadgrid), UTF8Encoding.UTF8, "application/json");
                    content.Headers.Add("user_code", "12345");

                    var response = client.PostAsync("files", content).Result;
                    //var res = response.Content.Headers.ContentLength;
                     
                    LogHelper.WriteLog("response"+response, control);
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    string base64data = string.Empty;

                    var bytes = new byte[data.Length];
                    data.Read(bytes, 0, bytes.Length);
                 // byte[] bytes = Convert.FromBase64String(reader.ReadLine().Replace('"', ' '));
                    LogHelper.WriteLog("bytes" + bytes, control);
                    var responses = new FileContentResult(bytes, "application/octet-stream");
                    LogHelper.WriteLog("responsesuccess", control);
                    responses.FileDownloadName = Job_name;

                    Response.Clear();
                    //Response.AddHeader("content-disposition", "inline; filename=" + Job_gid.ToString() + ConfigurationManager.AppSettings["downloadFiletype"].ToString());
                    Response.AddHeader("content-disposition", "inline; filename=" + Job_gid.ToString() + ConfigurationManager.AppSettings["downloadFiletype"].ToString());
                    Response.ContentType = "application/octet-stream";
                    Response.OutputStream.Write(bytes, 0, bytes.Length);
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                Result = this.ControllerContext.RouteData.Values["controller"].ToString(); LogHelper.WriteLog(ex.ToString(), Result);
            }
           // return View();
        }

        public ActionResult getuserrecon()
        {
            try
            {

                List<ManualMatchoff_model> objcat_3st = new List<ManualMatchoff_model>();
                DataTable result2 = new DataTable();
                ManualMatchoff_model manualmatchoff = new ManualMatchoff_model();
                manualmatchoff.user_code = user_codes;
                manualmatchoff.active_status = "Y";
                manualmatchoff.recontype = null;
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(manualmatchoff), "Getuserrecon");
                result2 = (DataTable)JsonConvert.DeserializeObject(post_data, result2.GetType());

                ManualMatchoff_model objcat3 = new ManualMatchoff_model();
                objcat3.ReconName_id = 0;
                objcat3.ReconName = "Select";
                objcat_3st.Add(objcat3);

                for (int i = 0; i < result2.Rows.Count; i++)
                {
                    objcat3 = new ManualMatchoff_model();
                    objcat3.ReconName_id = Convert.ToInt32(result2.Rows[i]["recon_gid"].ToString());
                    objcat3.ReconName = result2.Rows[i]["recon_name"].ToString();
                    objcat_3st.Add(objcat3);
                }
                return Json(objcat_3st, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
            return View();
        }
        public ActionResult getuserrecon_tallied(string fieldtype)
        {
            try
            {

                List<ManualMatchoff_model> objcat_3st = new List<ManualMatchoff_model>();
                DataTable result2 = new DataTable();
                ManualMatchoff_model manualmatchoff = new ManualMatchoff_model();
                manualmatchoff.user_code = user_codes;
                manualmatchoff.active_status = "Y";
                manualmatchoff.recontype = fieldtype;
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(manualmatchoff), "Getuserrecon_tallied");
                result2 = (DataTable)JsonConvert.DeserializeObject(post_data, result2.GetType());

                ManualMatchoff_model objcat3 = new ManualMatchoff_model();
                objcat3.ReconName_id = 0;
                objcat3.ReconName = "Select";
                objcat_3st.Add(objcat3);

                for (int i = 0; i < result2.Rows.Count; i++)
                {
                    objcat3 = new ManualMatchoff_model();
                    objcat3.ReconName_id = Convert.ToInt32(result2.Rows[i]["recon_gid"].ToString());
                    objcat3.ReconName = result2.Rows[i]["recon_name"].ToString();
                    string recon_tallied = result2.Rows[i]["recon_tallied"].ToString();
                    if (recon_tallied == "Y")
                    {
                        objcat3.recon_tallied = "Yes";
                        objcat3.isDeleted = false;
                    }
                    else
                    {
                        objcat3.recon_tallied = "No";
                        objcat3.isDeleted = true;
                    }
                    objcat_3st.Add(objcat3);
                }
                return Json(objcat_3st, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
            return View();
        }
        public ActionResult getuserrecon_type(string fieldtype)
        {
            try
            {

                List<TransactionRpt_model> objcat_3st = new List<TransactionRpt_model>();
                DataTable result2 = new DataTable();
                TransactionRpt_model DedupList = new TransactionRpt_model();
                DedupList.user_code = user_codes;
                DedupList.active_status = "Y";
                DedupList.recontype = fieldtype;
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(DedupList), "Getuserrecon");
                result2 = (DataTable)JsonConvert.DeserializeObject(post_data, result2.GetType());

                TransactionRpt_model objcat3 = new TransactionRpt_model();
                objcat3.ReconName_id = "0";
                objcat3.ReconName = "Select";
                objcat_3st.Add(objcat3);

                for (int i = 0; i < result2.Rows.Count; i++)
                {
                    objcat3 = new TransactionRpt_model();
                    objcat3.ReconName_id = (result2.Rows[i]["recon_gid"].ToString());
                    objcat3.ReconName = result2.Rows[i]["recon_name"].ToString();
                    objcat_3st.Add(objcat3);
                }
                return Json(objcat_3st, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
            return View();
        }

        public ActionResult Dropdownko()
        {
            List<TransactionRpt_model> objcat_3st = new List<TransactionRpt_model>();
            TransactionRpt_model objcat3 = new TransactionRpt_model();
            objcat3.Dropdownkoid = 0;
            objcat3.Dropdownkovalue = "Select";
            objcat_3st.Add(objcat3);

            objcat3 = new TransactionRpt_model();
            objcat3.Dropdownkoid = 1;
            objcat3.Dropdownkovalue = "Auto Matching";
            objcat_3st.Add(objcat3);

            objcat3 = new TransactionRpt_model();
            objcat3.Dropdownkoid = 2;
            objcat3.Dropdownkovalue = "Manual Matching";
            objcat_3st.Add(objcat3);

            return Json(objcat_3st, JsonRequestBehavior.AllowGet);

            return View();
        }

    }
}