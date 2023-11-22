using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
 
using Recon_Model;
using System.Net.Http;
using System.Configuration;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;
using System.IO;
using System.Data;
using System.Net;

namespace Recon.Controllers
{
    public class ManualMatchOffController : Controller
    {
      //  string url = ConfigurationManager.AppSettings["URL"].ToString();
        //string url = ConfigurationManager.AppSettings["URL"].ToString();
        string token = Convert.ToString(System.Web.HttpContext.Current.Session["token_"]);
        string url = ConfigurationManager.AppSettings["URL"].ToString();
        string user_codes = System.Web.HttpContext.Current.Session["usercode"].ToString();
        CommonController objcommon = new CommonController();
        public static string hostName = Dns.GetHostName(); // Retrive the Name of HOST 
        string ipAddress = Dns.GetHostByName(hostName).AddressList[0].ToString();
        //string token = ConfigurationManager.AppSettings["token"].ToString();
        //string token = ConfigurationManager.AppSettings["token"].ToString();
        // GET: ManualMatchOff
        public ActionResult ManualMatchOff()
        {
            return View();
        }

        public ActionResult Manulmatchoffnew()
        {
            return View();
        }
       
        public ActionResult Dynamicgrid()
        {
            return View();
        }
        public ActionResult ManualMatchOffView()
        {
            return View();
        }
        public ActionResult AmountMatched()
        {
            return View();
        }
        public ActionResult AmountMatchedAll()
        {
            return View();
        }
        public ActionResult AmountMatchedAllnew()
        {
            ViewBag.tokenvalue = token;
            ViewBag.url = url;
            ViewBag.ip = ipAddress;
            return View();
        }
        public ActionResult AdhocEditpartialview()
        {
            return PartialView();
        }

        public ActionResult ManualmatchoffGridRead([DataSourceRequest]DataSourceRequest request ,int outresult,int _count,int _pageno=1,int _pagesize=100,string table_name = "", string condition = "", bool radio_checked = false, int  recon_id = 0)   // ProgressView  Read

        {   //First grid load
            List<ManualMatchoff_model> objcat_lst = new List<ManualMatchoff_model>();
            DataTable result = new DataTable();
            ManualMatchoff_model ReportgridRead = new ManualMatchoff_model();
            ReportgridRead.outresult = outresult;
            ReportgridRead.count = _count;
            ReportgridRead  .pageno=_pageno;
            ReportgridRead.pagesize= _pagesize;
            ReportgridRead.table_name = table_name;
            ReportgridRead.Report_condition = condition;
            ReportgridRead.in_outfile_flag = radio_checked;
            ReportgridRead.report_gid = recon_id;
            ReportgridRead.user_code = user_codes;
            ReportgridRead.ip_address = ipAddress;
            string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(ReportgridRead), "Manualmatchoffgridload");
            result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());



            for (int i = 0; i < result.Rows.Count; i++)
            {
                ManualMatchoff_model objcat = new ManualMatchoff_model();
               objcat.sessiongid = Convert.ToInt32(result.Rows[i]["rptsession_gid"].ToString());
                objcat.tran_gid = Convert.ToInt32(result.Rows[i]["Tran id"].ToString());
                objcat.TranDate =result.Rows[i]["Tran Date"].ToString();
                objcat.ValueDate= result.Rows[i]["Value Date"].ToString();
               objcat.Accountmode = result.Rows[i]["Dr/Cr Code"].ToString();
               objcat.Particulars = result.Rows[i]["Narration"].ToString();
                objcat.acc_no= result.Rows[i]["A/C No"].ToString();
                objcat.amount= result.Rows[i]["Amount"].ToString();
                objcat.excp_amount = result.Rows[i]["Exception Amount"].ToString();
                objcat.remark2 = result.Rows[i]["Remark2"].ToString();
                objcat.remark1 = result.Rows[i]["Remark1"].ToString();
             
                objcat_lst.Add(objcat);

            }
            return Json(objcat_lst.ToDataSourceResult(request));
            //return Json(objcat_lst.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            //return Json(objcat_lst, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ManualMatchoffdependgrid([DataSourceRequest]DataSourceRequest request,string attrbid)   // ProgressView  Read
        {
            //second grid load
            List<ManualMatchoff_model> objcat_lst = new List<ManualMatchoff_model>();
            DataTable result = new DataTable();
            ManualMatchoff_model ReportgridRead = new ManualMatchoff_model();

            string value = attrbid;
            //ReportgridRead.sessiongid = _sessionid;
           // ReportgridRead.tran_gid = _tranid;
            ReportgridRead.attributvalue = attrbid;
            ReportgridRead.user_code = user_codes;
            ReportgridRead.ip_address = ipAddress;
            string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(ReportgridRead), "Manualmatchoffgridsec");
            result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());



            for (int i = 0; i < result.Rows.Count; i++)
            {
                ManualMatchoff_model objcat = new ManualMatchoff_model();

                objcat.tran_gid = Convert.ToInt32(result.Rows[i]["tran_gid"].ToString());
                objcat.TranDate = result.Rows[i]["tran_date"].ToString();
                objcat.ValueDate = result.Rows[i]["value_date"].ToString();
                objcat.Accountmode = result.Rows[i]["acc_mode"].ToString();
                //objcat.Particulars = result.Rows[i]["Narration"].ToString();
                objcat.Particulars = result.Rows[i]["tran_desc"].ToString();
                objcat.acc_no = result.Rows[i]["acc_no"].ToString();
                objcat.amount = result.Rows[i]["amount"].ToString();
                objcat.excp_amount = result.Rows[i]["excp_amount"].ToString();

                objcat_lst.Add(objcat);

            }
            return Json(objcat_lst.ToDataSourceResult(request));
            //return Json(objcat_lst.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            //return Json(objcat_lst, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ManualMatchoffdepend_pop(string attrbid)   // ProgressView  Read
        {
            List<ManualMatchoff_model> objcat_lst = new List<ManualMatchoff_model>();
            DataTable result = new DataTable();
            ManualMatchoff_model ReportgridRead = new ManualMatchoff_model();
            ManualMatchoff_model objcat = new ManualMatchoff_model();
            string value = attrbid;
            //ReportgridRead.sessiongid = _sessionid;
            // ReportgridRead.tran_gid = _tranid;
            ReportgridRead.attributvalue = attrbid;
            ReportgridRead.user_code = user_codes;
            ReportgridRead.ip_address = ipAddress;
            string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(ReportgridRead), "Manualmatchoffgridsec");
            result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());



            for (int i = 0; i < result.Rows.Count; i++)
            {


                objcat.tran_gid = Convert.ToInt32(result.Rows[i]["tran_gid"].ToString());
                objcat.TranDate = result.Rows[i]["tran_date"].ToString();
                objcat.ValueDate = result.Rows[i]["value_date"].ToString();
                objcat.Accountmode = result.Rows[i]["acc_mode"].ToString();
                //objcat.Particulars = result.Rows[i]["Narration"].ToString();
                objcat.Particulars = result.Rows[i]["tran_desc"].ToString();
                objcat.acc_no = result.Rows[i]["acc_no"].ToString();
                objcat.amount = result.Rows[i]["amount"].ToString();
                objcat.excp_amount = result.Rows[i]["excp_amount"].ToString();

                objcat_lst.Add(objcat);

            }
            //return Json(objcat_lst.ToDataSourceResult(request));
            return PartialView(objcat);
            //return Json(objcat_lst.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            //return Json(objcat_lst, JsonRequestBehavior.AllowGet);

        }
        public ActionResult ManualMatchoffdependGridview(string attrbid, int recon_gid, string reportcondition)   // ProgressView  Read
        {
            List<ManualMatchoff_model> objcat_lst = new List<ManualMatchoff_model>();
            DataTable result = new DataTable();
            ManualMatchoff_model ReportgridRead = new ManualMatchoff_model();
            ManualMatchoff_model objcat = new ManualMatchoff_model();
            string value = attrbid;
            //ReportgridRead.sessiongid = _sessionid;
            // ReportgridRead.tran_gid = _tranid;
            ReportgridRead.attributvalue = attrbid;
            ReportgridRead.recon_gid = recon_gid;
            ReportgridRead.report_code = "RPT_TRANEXCP";
            ReportgridRead.Report_condition = reportcondition;
            ReportgridRead.resultset_flag = true;
            ReportgridRead.user_code = user_codes;
            ReportgridRead.ip_address = ipAddress;
           // string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(ReportgridRead), "Manualmatchoffgridsec");
            string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(ReportgridRead), "ReportView");
            result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());

            string Data1 = "";
            Data1 = JsonConvert.SerializeObject(result);
            return Json(Data1, JsonRequestBehavior.AllowGet);

            //for (int i = 0; i < result.Rows.Count; i++)
            //{
                

            //    objcat.tran_gid = Convert.ToInt32(result.Rows[i]["tran_gid"].ToString());
            //    objcat.TranDate = result.Rows[i]["tran_date"].ToString();
            //    objcat.ValueDate = result.Rows[i]["value_date"].ToString();
            //    objcat.Accountmode = result.Rows[i]["acc_mode"].ToString();
            //    //objcat.Particulars = result.Rows[i]["Narration"].ToString();
            //    objcat.Particulars = result.Rows[i]["tran_desc"].ToString();
            //    objcat.acc_no = result.Rows[i]["acc_no"].ToString();
            //    objcat.amount = result.Rows[i]["amount"].ToString();
            //    objcat.excp_amount = result.Rows[i]["excp_amount"].ToString();

            //    objcat_lst.Add(objcat);

            //}
            //return Json(objcat_lst.ToDataSourceResult(request));
            //return PartialView(objcat);
            //return Json(objcat_lst.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            //return Json(objcat_lst, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ManualmatchoffGridReads(int outresult, int _count, string table_name = "", string condition = "", bool radio_checked = false, int recon_id = 0)   // ProgressView  Read
        {
            List<ManualMatchoff_model> objcat_lst = new List<ManualMatchoff_model>();
            DataTable result = new DataTable();
            ManualMatchoff_model ReportgridRead = new ManualMatchoff_model();
            ReportgridRead.outresult = outresult;
            ReportgridRead.count = _count;
            ReportgridRead.table_name = table_name;
            ReportgridRead.Report_condition = condition;
            ReportgridRead.in_outfile_flag = radio_checked;
            ReportgridRead.report_gid = recon_id;
            ReportgridRead.user_code = user_codes;
            ReportgridRead.ip_address = ipAddress;
            string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(ReportgridRead), "Manualmatchoffgridload");
            result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());



            for (int i = 0; i < result.Rows.Count; i++)
            {
                ManualMatchoff_model objcat = new ManualMatchoff_model();
                objcat.TranDate = result.Rows[i]["Tran Date"].ToString();
                objcat.ValueDate = result.Rows[i]["Value Date"].ToString();
                // objcat.Accountmode = result.Rows[i]["A/C Mode"].ToString();
                objcat.acc_no = result.Rows[i]["A/C No"].ToString();
                objcat.amount = result.Rows[i]["Amount"].ToString();
                objcat.excp_amount = result.Rows[i]["Exception Amount"].ToString();

                objcat_lst.Add(objcat);

            }
            //return Json(objcat_lst.ToDataSourceResult(request));
            //return Json(objcat_lst.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            return Json(objcat_lst, JsonRequestBehavior.AllowGet);

        }


        public ActionResult ManualFirstgridload(string table_name = "", string condition = "", bool radio_checked = false, int recon_id = 0, int Recon_gid=0)
        {   //first sp call
            try
            {
                ManualMatchoff_model ManualMatchoffload = new ManualMatchoff_model();

                ManualMatchoffload.table_name = table_name;
                ManualMatchoffload.Report_condition = condition;
                ManualMatchoffload.in_outfile_flag = radio_checked;
                ManualMatchoffload.report_gid = recon_id;
                ManualMatchoffload.recongid = Recon_gid;
                ManualMatchoffload.user_code = user_codes;
                ManualMatchoffload.ip_address = ipAddress;
                ManualMatchoffload.user_code = user_codes;
              
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(ManualMatchoffload), "ManulaMatchfirstload");
                result = (string[])JsonConvert.DeserializeObject(post_data, result.GetType());
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString();
                LogHelper.WriteLog(ex.ToString(), control);
            }
            return View();
        }


        public ActionResult ManualMatchselected(int recon_gid, int ko_amount, string tranid_frame, string knockoffreason = "")
        {//match selected 
            try
            {
                ManualMatchoff_model ManualMatchoffload = new ManualMatchoff_model();

                ManualMatchoffload.recon_gid = recon_gid;
                ManualMatchoffload.debitscore = ko_amount;
                ManualMatchoffload.tranid_frame= tranid_frame;
                ManualMatchoffload.knockoffreason=knockoffreason;
                ManualMatchoffload.user_code = user_codes;
                ManualMatchoffload.ip_address = ipAddress;
                
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(ManualMatchoffload), "ManualMatchselected");
                result = (string[])JsonConvert.DeserializeObject(post_data, result.GetType());
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString();
                LogHelper.WriteLog(ex.ToString(), control);
            }
            return View();
        }


        public ActionResult Manualmatchoff_Delete()//dtl create
        {
            ManualMatchoff_model ManualMatchoff = new ManualMatchoff_model();

            ManualMatchoff.user_code = user_codes;
            try
            {
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(ManualMatchoff), "ManualMatchoffDelete");
                result = (string[])JsonConvert.DeserializeObject(post_data, result.GetType());
                ManualMatchoff.applyruledtl_gid = Convert.ToInt32(result[1].ToString());
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString();
                LogHelper.WriteLog(ex.ToString(), control);
            }
            return View();
        }


        public ActionResult ManualmatchSelect_Read([DataSourceRequest]DataSourceRequest request,string user_code)   // Company  Read
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
                                objcat.ko_amount = result.Rows[i]["excp_amount"].ToString();
                                objcat.recon_gid = Convert.ToInt32(result.Rows[i]["recon_gid"]);
                                objcat.tranrecon_gid = Convert.ToInt32(result.Rows[i]["tranrecon_gid"]);
                                objcat.tran_gid = Convert.ToInt32(result.Rows[i]["tran_gid"]);
                                objcat.tranbrkp_gid = Convert.ToInt32(result.Rows[i]["tranbrkp_gid"]);                               
                                //objcat.Accountmode_desc = result.Rows[i][12].ToString();
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
        public ActionResult GridManualmatchoff([DataSourceRequest]DataSourceRequest request, string periodfrom, string periodto, string recongid)
        {
            List<ManualMatchoff_model> objcat_lst = new List<ManualMatchoff_model>();
            DataTable result = new DataTable();
            {
                try
                {
                    if (periodfrom == "")
                    {
                        periodfrom = null;
                    }
                    if (periodto == "")
                    {
                        periodto = null;
                    }
                    ManualMatchoff_model ManualMatch = new ManualMatchoff_model();
                    ManualMatch.periodfrom = periodfrom;
                    ManualMatch.periodto = periodto;
                    ManualMatch.recongid = Convert.ToInt32(recongid);
                    string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(ManualMatch), "GridManualmatchoff");
                    result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
                    for (int i = 0; i < result.Rows.Count; i++)
                    {
                        ManualMatchoff_model objcat = new ManualMatchoff_model();
                        objcat.SlNo = Convert.ToInt32(result.Rows[i]["sno"]);
                        objcat.tran_gid = Convert.ToInt32(result.Rows[i]["tran_gid"]);
                        objcat.tranrecon_gid = Convert.ToInt32(result.Rows[i]["recon_gid"]);
                      //  objcat.recon_gid = Convert.ToInt32(result.Rows[i][3]);
                        objcat.AccountNo = result.Rows[i]["acc_no"].ToString();
                        objcat.TranDate = result.Rows[i]["tran_date"].ToString();
                        objcat.ValueDate = result.Rows[i]["value_date"].ToString();
                        objcat.Description = result.Rows[i]["tran_desc"].ToString();
                        objcat.Accountmode = result.Rows[i]["acc_mode"].ToString();
                        objcat.Exceptionamountcon = Convert.ToDouble(result.Rows[i]["excp_amount"]);
                        objcat.amountcon = Convert.ToDouble(result.Rows[i]["amount"]);
                        objcat.Accountmode_desc = result.Rows[i]["acc_mode_desc"].ToString();
                        objcat.filetype = result.Rows[i]["file_type"].ToString();
                        objcat.tranbrkp_gid = Convert.ToInt32(result.Rows[i]["tranbrkp_gid"]);
                        objcat_lst.Add(objcat);
                    }
                    var jsonResult = Json(objcat_lst.ToDataSourceResult(request)); ;
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                    //return Json(objcat_lst.ToDataSourceResult(request));
                    //return Json(result1);
                }
                catch (Exception ex)
                {
                    string control = this.ControllerContext.RouteData.Values["controller"].ToString();LogHelper.WriteLog(ex.ToString(), control);
                }
                return View();
            }

        }
		
        public ActionResult GridSupFileExcpSumm([DataSourceRequest]DataSourceRequest request, string periodfrom, string periodto, string recongid)
        {
            List<ManualMatchoff_model> objcat_lst = new List<ManualMatchoff_model>();
            DataTable result = new DataTable();
            {
                try
                {
                    if (periodfrom == "")
                    {
                        periodfrom = null;
                    }
                    if (periodto == "")
                    {
                        periodto = null;
                    }
                    ManualMatchoff_model ManualMatch = new ManualMatchoff_model();
                    ManualMatch.periodfrom = periodfrom;
                    ManualMatch.periodto = periodto;
                    ManualMatch.recongid = Convert.ToInt32(recongid);
                    string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(ManualMatch), "GridSupFileExcpSumm");
                    result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
                    for (int i = 0; i < result.Rows.Count; i++)
                    {
                        ManualMatchoff_model objcat = new ManualMatchoff_model();
                        objcat.SlNo = Convert.ToInt32(result.Rows[i]["sno"]);
                        objcat.tran_gid = Convert.ToInt32(result.Rows[i]["tran_gid"]);
                        objcat.recon_gid = Convert.ToInt32(result.Rows[i]["recon_gid"]);
                        objcat.AccountNo = result.Rows[i]["acc_no"].ToString();
                        objcat.TranDate = result.Rows[i]["tran_date"].ToString();
                        objcat.ValueDate = result.Rows[i]["value_date"].ToString();
                        objcat.Description = result.Rows[i]["tran_desc"].ToString();
                        objcat.Accountmode = result.Rows[i]["acc_mode"].ToString();
                        objcat.Exceptionamountcon = Convert.ToDouble(result.Rows[i]["excp_amount"]);
                        objcat.amountcon = Convert.ToDouble(result.Rows[i]["amount"]);
                        objcat.Accountmode_desc = result.Rows[i]["acc_mode"].ToString();
                        objcat.filetype = result.Rows[i]["file_type"].ToString();
                        objcat_lst.Add(objcat);
                    }
                    var jsonResult = Json(objcat_lst.ToDataSourceResult(request)); ;
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                    //return Json(objcat_lst.ToDataSourceResult(request));
                    //return Json(result1);
                }
                catch (Exception ex)
                {
                    string control = this.ControllerContext.RouteData.Values["controller"].ToString(); LogHelper.WriteLog(ex.ToString(), control);
                }
                return View();
            }

        }
		
        public ActionResult AddMatchOff_Save(ManualMatchoff_model ManualMatchoff)
        {
            try
            {
                ManualMatchoff.user_code = user_codes;
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(ManualMatchoff), "AddMatchOff");
                result = (string[])JsonConvert.DeserializeObject(post_data, result.GetType());
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString(); 
                LogHelper.WriteLog(ex.ToString(), control);
            }
            return View();
        }
        public ActionResult SelectedMatchOff(string recongid, string koamount, string tranrecon, string usercode, string matchoffreason)   
        {
            ManualMatchoff_model selectedmatch = new ManualMatchoff_model();
            tranrecon = tranrecon.Replace(",", "#");
            selectedmatch.recon_gid = Convert.ToInt32(recongid);
            selectedmatch.ko_amountcon = Convert.ToDouble(koamount);
            selectedmatch.Comparition = tranrecon;
            selectedmatch.user_code = user_codes;
            selectedmatch.koreason = matchoffreason;

            List<ManualMatchoff_model> objcat_lst = new List<ManualMatchoff_model>();

            try
            {
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(selectedmatch), "SelectedMatchOffcreate");
                result = (string[])JsonConvert.DeserializeObject(post_data, result.GetType());
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString(); 
                LogHelper.WriteLog(ex.ToString(), control);
            }
            return View();
        }
        public ActionResult SelectedMatchOffdel(string usercode)   // Addtional Condition
        {
            ManualMatchoff_model selectedmatch = new ManualMatchoff_model();          
            selectedmatch.user_code = usercode;

            List<ManualMatchoff_model> objcat_lst = new List<ManualMatchoff_model>();

            try
            {
                string result = "";
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(selectedmatch), "SelectedMatchOffdel");
                result = (string)JsonConvert.DeserializeObject(post_data, result.GetType());
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString();
                LogHelper.WriteLog(ex.ToString(), control);
            }
            return View();
        }

        public JsonResult ReportExpenceGrid_Read([DataSourceRequest]DataSourceRequest request, string period_from, string period_to, string Recomname, string amount, string user_code, string groupfield)
        {
            try
            {
                ManualMatchoff_model amountmatchoff = new ManualMatchoff_model();
                amountmatchoff.periodfrom = period_from;
                amountmatchoff.periodto = period_to;
                amountmatchoff.recon_gid = Convert.ToInt32(Recomname);
                amountmatchoff.ko_amountcon = Convert.ToDouble(amount);
                amountmatchoff.user_code = user_code;
                amountmatchoff.grp_field = groupfield;
                List<ManualMatchoff_model> objcat_lst = new List<ManualMatchoff_model>();
                DataTable result = new DataTable();

                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(amountmatchoff), "AmountMatched");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    ManualMatchoff_model objcat = new ManualMatchoff_model();
                    objcat.SlNo = Convert.ToInt32(result.Rows[i][0]);
                    objcat.group_sno = Convert.ToInt32(result.Rows[i][1]);
                    objcat.filetype = result.Rows[i][2].ToString();
                    objcat.AccountNo = result.Rows[i][3].ToString();
                    objcat.TranDate = result.Rows[i][4].ToString();
                    objcat.ValueDate = result.Rows[i][5].ToString();
                    objcat.Description = result.Rows[i][6].ToString();
                    objcat.Accountmode = result.Rows[i][7].ToString();
                    objcat.amount = result.Rows[i][8].ToString();
                    objcat.Exceptionamount = result.Rows[i][9].ToString();
                    objcat.tranrecon_gid = Convert.ToInt32(result.Rows[i][12]);
                    objcat_lst.Add(objcat);
                }
                var jsonResult = Json(objcat_lst.ToDataSourceResult(request)); ;
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch(Exception e)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString();
                LogHelper.WriteLog(e.ToString(), control);
                return Json(e.Message);
            }
        }
        public JsonResult ReportAmountMatchall_Read([DataSourceRequest]DataSourceRequest request, string _condition, string Recomname, string amount, string user_code, string groupfield)
        {
            List<ManualMatchoff_model> objcat_lst = new List<ManualMatchoff_model>();
            DataTable result = new DataTable();
            try
            {
                ManualMatchoff_model amountmatchoff = new ManualMatchoff_model();
                //amountmatchoff.periodfrom = period_from;
                //amountmatchoff.periodto = period_to;
                amountmatchoff.condition = _condition;
                amountmatchoff.recon_gid = Convert.ToInt32(Recomname);
                amountmatchoff.ko_amountcon = Convert.ToDouble(amount);
                amountmatchoff.user_code = user_code;
                amountmatchoff.grp_field = groupfield;
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(amountmatchoff), "AmountMatchedAll");
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
            }
            catch (Exception e)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString();
                LogHelper.WriteLog(e.ToString(), control);
            }
            var jsonResult = Json(objcat_lst.ToDataSourceResult(request)); ;
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }
        //public JsonResult ReportAmountMatchall_Read([DataSourceRequest]DataSourceRequest request, string period_from, string period_to, string Recomname, string amount, string user_code, string groupfield)
        //{
        //    List<ManualMatchoff_model> objcat_lst = new List<ManualMatchoff_model>();
        //    DataTable result = new DataTable();
        //    {
        //        ManualMatchoff_model amountmatchoff = new ManualMatchoff_model();
        //        amountmatchoff.periodfrom = period_from;
        //        amountmatchoff.periodto = period_to;
        //        amountmatchoff.recon_gid = Convert.ToInt32(Recomname);
        //        amountmatchoff.ko_amountcon = Convert.ToDouble(amount);
        //        amountmatchoff.user_code = user_code;
        //        amountmatchoff.grp_field = groupfield;
        //        using (var client = new HttpClient())
        //        {

        //            client.BaseAddress = new Uri(url);
        //            client.Timeout = TimeSpan.FromMinutes(10);
        //            client.DefaultRequestHeaders.Accept.Clear(); client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        //            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //            HttpContent content = new StringContent(JsonConvert.SerializeObject(amountmatchoff), UTF8Encoding.UTF8, "application/json");
        //            var response = client.PostAsync("AmountMatchedAll", content).Result;
        //            Stream data = response.Content.ReadAsStreamAsync().Result;
        //            StreamReader reader = new StreamReader(data);
        //            string post_data = reader.ReadToEnd();
        //            result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
        //            for (int i = 0; i < result.Rows.Count; i++)
        //            {
        //                ManualMatchoff_model objcat = new ManualMatchoff_model();
        //                objcat.SlNo = Convert.ToInt32(result.Rows[i]["sno"]);
        //                objcat.filetype = result.Rows[i]["file_type"].ToString();
        //                objcat.AccountNo = result.Rows[i]["acc_no"].ToString();
        //                objcat.TranDate = result.Rows[i]["tran_date"].ToString();
        //                objcat.ValueDate = result.Rows[i]["value_date"].ToString();
        //                objcat.Description = result.Rows[i]["tran_desc"].ToString();
        //                objcat.Accountmode = result.Rows[i]["acc_mode"].ToString();
        //                objcat.amount = result.Rows[i]["amount"].ToString();
        //                objcat.Exceptionamount = result.Rows[i]["excp_amount"].ToString();
        //                objcat.ko_amount = result.Rows[i]["ko_amount"].ToString();
        //                objcat.recon_gid = Convert.ToInt32(result.Rows[i]["recon_gid"]);
        //                objcat.tranrecon_gid = Convert.ToInt32(result.Rows[i]["tranrecon_gid"]);
        //                objcat.tran_gid = Convert.ToInt32(result.Rows[i]["tran_gid"]);
        //                objcat.tranbrkp_gid = Convert.ToInt32(result.Rows[i]["tranbrkp_gid"]);
        //                objcat.applyrule_gid = Convert.ToInt32(result.Rows[i]["applyrule_gid"]);
        //                objcat.rule_gid = Convert.ToInt32(result.Rows[i]["rule_gid"]);
        //                objcat.group_sno = Convert.ToInt32(result.Rows[i]["group_sno"]);
        //                objcat.rule_name = result.Rows[i]["rule_name"].ToString();
        //                objcat.grp_field = result.Rows[i]["grp_field"].ToString();
        //                objcat.Comparition = result.Rows[i]["comparision"].ToString();
        //                objcat.Source = result.Rows[i]["source"].ToString();
        //                objcat_lst.Add(objcat);
        //            }
        //        }
        //    }
        //    var jsonResult = Json(objcat_lst.ToDataSourceResult(request)); ;
        //    jsonResult.MaxJsonLength = int.MaxValue;
        //    return jsonResult;

        //}
        public ActionResult AmountMatchedSave(string recongid, string bulk_tranrecon, string user_code)
        {
            ManualMatchoff_model selectedmatch = new ManualMatchoff_model();            
            selectedmatch.recon_gid = Convert.ToInt32(recongid);
            selectedmatch.Description = bulk_tranrecon;
            selectedmatch.user_code = user_codes;

            List<ManualMatchoff_model> objcat_lst = new List<ManualMatchoff_model>();

            try
            {
                string result = "";
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(selectedmatch), "AmountMatchedSave");
                result = (string)JsonConvert.DeserializeObject(post_data, result.GetType());
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString(); LogHelper.WriteLog(ex.ToString(), control);
            }
            return View();
        }

        public ActionResult MatchOffReason(string matchoffreason,string koid)
        {
            ManualMatchoff_model ManualMatchoff = new ManualMatchoff_model();
            ManualMatchoff.koreason = matchoffreason;
            ManualMatchoff.koid = Convert.ToInt32(koid);
            try
            {
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(ManualMatchoff), "MatchOffReason");
                result = (string[])JsonConvert.DeserializeObject(post_data, result.GetType());
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString(); 
                LogHelper.WriteLog(ex.ToString(), control);
            }
            return View();
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
    }
}