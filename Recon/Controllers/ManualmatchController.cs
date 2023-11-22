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
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Configuration;
using System.Net;

namespace Recon.Controllers
{
    public class ManualmatchController : Controller
    {
        CommonController objcommon = new CommonController();
     string url = ConfigurationManager.AppSettings["URL"].ToString();
        string user_codes = System.Web.HttpContext.Current.Session["usercode"].ToString();
        string token = Convert.ToString(System.Web.HttpContext.Current.Session["token_"]);
        public static string hostName = Dns.GetHostName(); // Retrive the Name of HOST 
        string ipAddress = Dns.GetHostByName(hostName).AddressList[0].ToString();
       //string token = ConfigurationManager.AppSettings["token"].ToString();
        // GET: Manualmatch
        public ActionResult Manualmatch()
        {
            ViewBag.url= url;
            ViewBag.ip = ipAddress;
            ViewBag.Message = token;
            return View();
        }

        public ActionResult Manualmatchnew()
        {
            ViewBag.url = url;
            ViewBag.ip = ipAddress;
            ViewBag.Message = token;
            return View();
        }

        public ActionResult ManualmatchView()
        {
            return View();
        }

        //public ActionResult ManualmatchGrid_Read([DataSourceRequest]DataSourceRequest request, string slno)   // Company  Read
        //{
        //    ManualMatchoff_model rulegrid = new ManualMatchoff_model();
        //    rulegrid.recongid = Convert.ToInt32(slno);

        //    List<ManualMatchoff_model> objcat_lst = new List<ManualMatchoff_model>();
        //    DataTable result = new DataTable();
        //    try
        //    {
        //        using (var client = new HttpClient())
        //        {
        //            try
        //            {
        //                client.BaseAddress = new Uri(url);
        //                client.DefaultRequestHeaders.Accept.Clear(); client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        //                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        //                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //                HttpContent content = new StringContent(JsonConvert.SerializeObject(rulegrid), UTF8Encoding.UTF8, "application/json");
        //                var response = client.PostAsync("RuleGrid", content).Result;
        //                Stream data = response.Content.ReadAsStreamAsync().Result;
        //                StreamReader reader = new StreamReader(data);
        //                string post_data = reader.ReadToEnd();
        //                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
        //                for (int i = 0; i < result.Rows.Count; i++)
        //                {
        //                    ManualMatchoff_model objcat = new ManualMatchoff_model();
        //                    objcat.sno = Convert.ToInt32(result.Rows[i][0]);
        //                    objcat.applyrule_gid = Convert.ToInt32(result.Rows[i][1]);
        //                    objcat.rule_gid = Convert.ToInt32(result.Rows[i][2]);
        //                    objcat.rule_name = result.Rows[i][3].ToString();
        //                    objcat_lst.Add(objcat);
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                string control = this.ControllerContext.RouteData.Values["controller"].ToString(); LogHelper.WriteLog(ex.ToString(), control);
        //            }
        //            return Json(objcat_lst.ToDataSourceResult(request));
        //            //return Json(result1);     
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string control = this.ControllerContext.RouteData.Values["controller"].ToString(); LogHelper.WriteLog(ex.ToString(), control);
        //    }
        //    return View();
        //}

        public ActionResult ManualmatchGrid_Read([DataSourceRequest]DataSourceRequest request, string slno)   // Company  Read
        {
            ManualMatchoff_model rulegrid = new ManualMatchoff_model();
            rulegrid.recongid = Convert.ToInt32(slno);

            List<ManualMatchoff_model> objcat_lst = new List<ManualMatchoff_model>();
            DataTable result = new DataTable();
            try
            {
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(rulegrid), "RuleGrid");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    ManualMatchoff_model objcat = new ManualMatchoff_model();
                    objcat.sno = Convert.ToInt32(result.Rows[i][0]);
                    objcat.applyrule_gid = Convert.ToInt32(result.Rows[i][1]);
                    objcat.rule_gid = Convert.ToInt32(result.Rows[i][2]);
                    objcat.rule_name = result.Rows[i][3].ToString();
                    objcat.update_date = result.Rows[i]["update_date"].ToString();
                    objcat_lst.Add(objcat);
                }
                return Json(objcat_lst.ToDataSourceResult(request));
                //return Json(result1);     
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString(); LogHelper.WriteLog(ex.ToString(), control);
            }
            return View();
        }

        public ActionResult ManualmatchGrid_Readload([DataSourceRequest]DataSourceRequest request)   // Company  Read
        {
            ManualMatchoff_model rulegrid = new ManualMatchoff_model();
            rulegrid.user_code = user_codes;

            List<ManualMatchoff_model> objcat_lst = new List<ManualMatchoff_model>();
            DataTable result = new DataTable();
            try
            {
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(rulegrid), "GetManualmatch");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    ManualMatchoff_model objcat = new ManualMatchoff_model();
                    objcat.file_name = result.Rows[i]["file_name"].ToString();
                    objcat.recon_name = result.Rows[i]["recon_name"].ToString();
                    objcat.total_records = Convert.ToInt64(result.Rows[i]["tot_count"]);
                    objcat.Debit_total = Convert.ToDecimal(result.Rows[i]["ko_dr_amount"]);
                    objcat.credit_total = Convert.ToDecimal(result.Rows[i]["ko_cr_amount"]);
                    objcat.Difference = Convert.ToDecimal(result.Rows[i]["ko_diff_amount"]);
                    objcat.file_gid = Convert.ToInt32(result.Rows[i]["file_gid"]);
                    objcat.recon_gid = Convert.ToInt32(result.Rows[i]["recon_gid"]);
                    string recon_tallied = result.Rows[i]["recon_tallied"].ToString();
                    if (recon_tallied == "Y")
                    {
                        objcat.recon_tallied = "Yes";
                    }
                    else
                    {
                        objcat.recon_tallied = "No";
                    }
                    objcat_lst.Add(objcat);
                }
                return Json(objcat_lst.ToDataSourceResult(request));
                //return Json(result1);     
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString(); LogHelper.WriteLog(ex.ToString(), control);
            }
            return View();
        }

        public ActionResult Runmatchfile(int File_gid)
        {
            ManualMatchoff_model ManualMatchoff = new ManualMatchoff_model();
            //Attribid = Attribid.Replace(",", "");
            ManualMatchoff.file_gid = File_gid;
            ManualMatchoff.user_code = user_codes;
            try
            {
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(ManualMatchoff), "RunMatch_file");
                result = (string[])JsonConvert.DeserializeObject(post_data, result.GetType());
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

            }
            return View();
        }


        public ActionResult MatchOff_Save(string Attribid, string formatted_period_from, string formatted_period_to, string recontype, string automatch, string user_code)
        {
            ManualMatchoff_model ManualMatchoff = new ManualMatchoff_model();
            //Attribid = Attribid.Replace(",", "");
            ManualMatchoff.Rule = Attribid;
            ManualMatchoff.periodfrom = formatted_period_from;
            ManualMatchoff.periodto = formatted_period_to;
            ManualMatchoff.recontype = recontype;
            ManualMatchoff.automatchoff = automatch;
            ManualMatchoff.user_code = user_codes;
            try
            {
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(ManualMatchoff), "RuleMatchOff");
                result = (string[])JsonConvert.DeserializeObject(post_data, result.GetType());
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

            }
            return View();
        }


        public ActionResult ManualmatchSelect_Read([DataSourceRequest]DataSourceRequest request, string user_code)   // Company  Read
        {
            List<ManualMatchoff_model> objcat_lst = new List<ManualMatchoff_model>();
            DataTable result = new DataTable();
            {
                try
                {
                    ManualMatchoff_model ManualMatch = new ManualMatchoff_model();
                    ManualMatch.user_code = user_codes;
                    string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(ManualMatch), "SelectedMatchOff");
                    result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
                    for (int i = 0; i < result.Rows.Count; i++)
                    {
                        ManualMatchoff_model objcat = new ManualMatchoff_model();
                        objcat.SlNo = Convert.ToInt32(result.Rows[i]["sno"]);
                        objcat.filetype = result.Rows[i]["file_type"].ToString();
                        objcat.AccountNo = result.Rows[i]["acc_no"].ToString();
                        objcat.TranDate = result.Rows[i]["tran_date"].ToString();
                        objcat.ValueDate = result.Rows[i]["value_date"].ToString();
                        objcat.Description = result.Rows[i]["tran_desc"].ToString();
                        objcat.Accountmode = result.Rows[i]["acc_mode"].ToString();
                        objcat.amount = result.Rows[i]["amount"].ToString();
                        objcat.Exceptionamount = result.Rows[i]["excp_amount"].ToString();
                        objcat.ko_amount = result.Rows[i]["ko_amount"].ToString();
                        objcat.recon_gid = Convert.ToInt32(result.Rows[i]["recon_gid"]);
                        objcat.tranrecon_gid = Convert.ToInt32(result.Rows[i]["tranrecon_gid"]);
                        objcat.tran_gid = Convert.ToInt32(result.Rows[i]["tran_gid"]);
                        objcat.tranbrkp_gid = Convert.ToInt32(result.Rows[i]["tranbrkp_gid"]);
                        objcat.applyrule_gid = Convert.ToInt32(result.Rows[i]["applyrule_gid"]);
                        objcat.rule_gid = Convert.ToInt32(result.Rows[i]["rule_gid"]);
                        objcat.group_sno = Convert.ToInt32(result.Rows[i]["group_sno"]);
                        objcat.rule_name = result.Rows[i]["rule_name"].ToString();
                        objcat.grp_field = result.Rows[i]["grp_field"].ToString();
                        objcat.Comparition = result.Rows[i]["comparision"].ToString();
                        objcat.Source = result.Rows[i]["source"].ToString();
                        objcat_lst.Add(objcat);
                    }
                    var jsonResult = Json(objcat_lst.ToDataSourceResult(request)); ;
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
                catch (Exception ex)
                {
                    string control = this.ControllerContext.RouteData.Values["controller"].ToString();LogHelper.WriteLog(ex.ToString(), control);
                }
                return View();
            }
        }

        public ActionResult ManualMatchedSave(string recongid, string bulk_tranrecon, string user_code)
        {
            ManualMatchoff_model selectedmatch = new ManualMatchoff_model();
            selectedmatch.recon_gid = Convert.ToInt32(recongid);
            selectedmatch.Description = bulk_tranrecon;
            selectedmatch.user_code = user_codes;

            List<ManualMatchoff_model> objcat_lst = new List<ManualMatchoff_model>();

            try
            {
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(selectedmatch), "RuleMatchOffSelction");
                result = (string[])JsonConvert.DeserializeObject(post_data, result.GetType());
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        public ActionResult ManualMatchedSingleView(string filetype, string tranrecon_gid, string tranbrkp_gid)
        {
            ManualMatchoff_model singleselected = new ManualMatchoff_model();
            singleselected.tranbrkp_gid = Convert.ToInt32(tranbrkp_gid);
            singleselected.tranrecon_gid = Convert.ToInt32(tranrecon_gid);
            singleselected.filetype = filetype;

            DataTable result = new DataTable();
            string Data1 = "";
            string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(singleselected), "ManualSingleView");
            result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());

            Data1 = JsonConvert.SerializeObject(result);
            return Json(Data1, JsonRequestBehavior.AllowGet);

        }

        public ActionResult getuserrecon()
        {
            try
            {

                List<ManualMatchoff_model> objcat_3st = new List<ManualMatchoff_model>();
                DataTable result2 = new DataTable();
                ManualMatchoff_model manualmatch = new ManualMatchoff_model();
                manualmatch.user_code = user_codes;
                manualmatch.active_status = "Y";
                manualmatch.recontype = null;
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(manualmatch), "Getuserrecon");
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

        public ActionResult getuserrecon_tallied()
        {
            try
            {

                List<ManualMatchoff_model> objcat_3st = new List<ManualMatchoff_model>();
                DataTable result2 = new DataTable();
                ManualMatchoff_model manualmatch = new ManualMatchoff_model();
                manualmatch.user_code = user_codes;
                manualmatch.active_status = "Y";
                manualmatch.recontype = null;
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(manualmatch), "getuserrecon_tallied");
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
    }
}