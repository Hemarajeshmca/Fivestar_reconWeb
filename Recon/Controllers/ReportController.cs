using System.Configuration;
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
using System.IO;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using MySql.Data.MySqlClient;
using System.Net;
using ClosedXML.Excel;
using System.Dynamic;
using System.Text.RegularExpressions;
using System.Collections;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Recon.Controllers
{
    public  class ReportController : Controller
    {
        CommonController objcommon = new CommonController();
        string token = Convert.ToString(System.Web.HttpContext.Current.Session["token_"]);
      string url = ConfigurationManager.AppSettings["URL"].ToString();
       // string user_codes = ConfigurationManager.AppSettings["usercode"].ToString();
        string user_codes = System.Web.HttpContext.Current.Session["usercode"].ToString();
        //string url = ConfigurationManager.AppSettings["URL"].ToString();
        //string token = ConfigurationManager.AppSettings["token"].ToString();
        // GET: Report
        public static string hostName = Dns.GetHostName(); // Retrive the Name of HOST 
        string ipAddress = Dns.GetHostByName(hostName).AddressList[0].ToString();

        public ActionResult ProcessStatus()
        {
            return View();
        }
        public ActionResult ExceptionReport()
        {
            return View();
        }
        public ActionResult KnockoffReport()
        {
            return View();
        }
        public ActionResult KnockoffReportundo()
        {
            return View();
        }
        public ActionResult FileReport()
        {
            return View();
        }
        public ActionResult FileReportDel()
        {
            return View();
        }
        public ActionResult BalanceReport()
        {
            return View();
        }
        public ActionResult ReportBrs()
        {
            return View();
        }
        public ActionResult withinaccReportBrs()
        {
            return View();
        }
        public ActionResult PaginationReport()
        {
            return View();
        }

        public ActionResult ReportBrs_New()
        {
            return View();
        }
        public ActionResult Recon_between_acc()
        {
            return View();
        }
        public ActionResult TransactionReport()
        {
            ViewBag.tokenvalue = token;
            ViewBag.url = url;
            ViewBag.ip = ipAddress;
            return View();
        }
        public ActionResult SupportingReport()
        {
            return View();
        }
        public ActionResult ReversedTransactions()
        {
            return View();
        }
        public ActionResult ErrorReport()
        {
            return View();
        }
        public ActionResult ErrorlogReport()
        {
            return View();
        }
     
        public ActionResult KnockoffMIS()
        {

            return View();
        }
        public ActionResult KnockoffMIS_New()
        {

            return View();
        }

        public ActionResult Monthendreport_Download()
        {
            return View();
        }

        public ActionResult Monthendreportview()
        {
            return View();
        }



        public ActionResult ReportExpence_Exportcsv()
        {
            string query = "";
            string result = "";
            MySqlConnection conn = new MySqlConnection("Server=169.38.77.180;database=flexi_recon_vinoth;User Id=root;password=Flexi@123;Allow User Variables=true");
            //Random rnd = new Random();
            //int month = rnd.Next(1, 13);  // creates a number between 1 and 12
            //int dice = rnd.Next(1, 7);   // creates a number between 1 and 6
            //int card = rnd.Next(52); 
            try
            {
                query += "select 'excp_amount','tranrecon_gid' " +
                           "union all " +
                           "select b.excp_amount,b.tranrecon_gid from recon_trn_ttran as a " +
                           "inner join recon_trn_ttranrecon as b on a.tran_gid = b.tran_gid " +
                           "where b.excp_amount > 0" +
                           "INTO outfile '/tmp/filename.csv' " +
                           "FIELDS TERMINATED BY ',' " +
                           "LINES TERMINATED BY '\n'; ";
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                return Json("File Exported in Server", JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            
            {

                return Json(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return View();

        }

        //public ActionResult ReportGridRead([DataSourceRequest]DataSourceRequest request)   // ProgressView  Read
        //{
        //    List<Report_model> objcat_lst = new List<Report_model>();
        //    DataTable result = new DataTable();
        //    Report_model ReportgridRead = new Report_model();

        //    string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(ReportgridRead), "");
        //    result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());



        //    for (int i = 0; i < result.Rows.Count; i++)
        //    {
        //        Report_model objcat = new Report_model();
        //        objcat.tran_gid= Convert.ToInt32(result.Rows[i][""]);
        //        objcat.Accountno = result.Rows[i]["parent_master_syscode"].ToString();
        //        objcat.Amount = result.Rows[i]["master_syscode"].ToString();
        //        objcat.transdate = result.Rows[i]["master_code"].ToString();
        //        objcat.TranDescription = result.Rows[i]["master_short_code"].ToString();
        //        objcat. = result.Rows[i]["master_name"].ToString();
        //        objcat_lst.Add(objcat);

        //    }
        //    return Json(objcat_lst.ToDataSourceResult(request));
        //    //return Json(objcat_lst.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        //    //return Json(objcat_lst, JsonRequestBehavior.AllowGet);

        //}

        public ActionResult ReportExpenceGrid_Read([DataSourceRequest]DataSourceRequest request, string table_name, string condition, bool radio_checked, int recon_id, int recon_gid)
        {
            List<Report_model> objcat_lst = new List<Report_model>();
            DataTable result = new DataTable();
            string Data1 = "";
            try
            {
                Report_model report = new Report_model();
                report.table_name = table_name;
                report.Report_condition = condition;
                report.in_outfile_flag = radio_checked;
                report.report_gid = recon_id;
                report.recon_gid = recon_gid;
                report.user_code = user_codes;
                report.ip_address = ipAddress;
                string control = this.ControllerContext.RouteData.Values["controller"].ToString();
                LogHelper.WriteLog("table_name" + table_name, control);
                LogHelper.WriteLog("Report_condition" + condition, control);
                LogHelper.WriteLog("radio_checked" + radio_checked, control);
                LogHelper.WriteLog("recon_id" + recon_id, control);
                LogHelper.WriteLog("recon_gid" + recon_gid, control);
                LogHelper.WriteLog("user_codes" + user_codes, control);
                LogHelper.WriteLog("ipAddress" + ipAddress, control);
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(report), "ExectionReport");

                post_data = post_data.Remove(0,1);
                post_data = post_data.Remove(post_data.Length - 1, 1);

                string[] array_data = new string[] {""} ;

                array_data = post_data.Replace('"',' ').Split(',');

                string control1 = this.ControllerContext.RouteData.Values["controller"].ToString();
                LogHelper.WriteLog("Reportwebsuccess", control1);
               
               /* result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
                Data1 = JsonConvert.SerializeObject(result);
                for (int i = 0; i < result.Rows.Count; i++)
                {

                    report.tran_gid = Convert.ToInt32(result.Rows[i]["tran_gid"]);
                    report.Accountno = result.Rows[i]["acc_no"].ToString();
                    report.Amount = Convert.ToInt32(result.Rows[i]["amount"].ToString());
                    report.transdate = Convert.ToDateTime(result.Rows[i]["tran_date"].ToString());
                    report.TranDescription = result.Rows[i]["tran_desc"].ToString();
                    report.MappedAmount = Convert.ToInt32(result.Rows[i]["mapped_amount"].ToString());
                    objcat_lst.Add(report);
                }*/
               // return Json(objcat_lst.ToDataSourceResult(request));
                return Json(array_data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString();
                LogHelper.WriteLog(e.ToString(), control);
                return Json(e.Message);
            }
        }

        //public JsonResult ReportExpenceGrid_Read(string table_name, string condition)
        //{
        //    DataTable result = new DataTable();
        //     string Data1 = "";
        //    try
        //    {
        //        Report_model report = new Report_model();
        //        report.table_name = table_name;
        //        report.Report_condition = condition;
        //        string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(report), "ExectionReport");
        //        result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
        //        Data1 = JsonConvert.SerializeObject(result);
        //        //return Json("File downloaded in server", JsonRequestBehavior.AllowGet);
        //        var jsonResult = Json(Data1, JsonRequestBehavior.AllowGet);
        //        jsonResult.MaxJsonLength = int.MaxValue;
        //        return jsonResult;
        //    }
        //    catch(Exception e)
        //    {
        //        return Json(e.Message);
        //    }
        //}

        public JsonResult ReportKnockOffGrid_Read([DataSourceRequest]DataSourceRequest request,  string condition, int recon_gid)
        {
            List<Report_model> objcat_lst = new List<Report_model>();
            DataTable result = new DataTable();
            Report_model report = new Report_model();
          //  report.table_name = table_name;
            report.Report_condition = condition;
            report.recon_gid = recon_gid;
           // report.report_code = Reportcode;
            report.resultset_flag = true;
            report.user_code = user_codes;
            report.ip_address = ipAddress;
            try
            {
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(report), "KoHead_Report");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    Report_model objcat = new Report_model();
                    objcat.koid = Convert.ToInt32(result.Rows[i]["Ko Id"]);
                    objcat.kodate = result.Rows[i]["Ko Date"].ToString();
                    objcat.reconname = result.Rows[i]["Recon Name"].ToString();
                    objcat.Knockoffamount = result.Rows[i]["Ko Amount"].ToString();
                    objcat.rulename = result.Rows[i]["Rule Name"].ToString();
                    objcat.mappingdesctype = result.Rows[i]["Mapping Type Code"].ToString();
                    objcat.matchoff_type = result.Rows[i]["Manual Matchoff Flag"].ToString();
                    //objcat.Knockoffby = result.Rows[i][7].ToString();
                    objcat_lst.Add(objcat);
                }
                //Data1 = JsonConvert.SerializeObject(result);
                var jsonResult = Json(objcat_lst.ToDataSourceResult(request));
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }

        }
        public JsonResult ReportBalanceGrid_Read(string table_name, string condition)
        {
            try
            {
                Report_model report = new Report_model();
                report.table_name = table_name;
                report.Report_condition = condition;
                DataTable result = new DataTable();
                string Data1 = "";
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(report), "ExectionReport");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
                Data1 = JsonConvert.SerializeObject(result);
                //return Json("File downloaded in server", JsonRequestBehavior.AllowGet);
                var jsonResult = Json(Data1, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;

            }catch(Exception e)
            {
                return Json(e.Message);
            }
        }
        public JsonResult ReportKnockOFFGrid_ReadVIEW(string table_name, string condition, int recon_gid)
        {
           try
           {
               Report_model report = new Report_model();
               report.table_name = table_name;
               report.Report_condition = condition;
               report.resultset_flag = true;
               report.report_code = "RPT_KO";
               report.recon_gid = recon_gid;
               report.user_code = user_codes;
              
               
               DataTable result = new DataTable();
               string Data1 = "";
              // string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(report), "ExectionReport");
               string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(report), "ReportView");
               result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
               Data1 = JsonConvert.SerializeObject(result);
               return Json(Data1, JsonRequestBehavior.AllowGet);
           }
            catch(Exception e)
           {
               return Json(e.Message);
           }
        }
        public ActionResult ReportKnockOFFGrid_del(string name, string Dkoid, string user_code)
        {
            try
            {
                Report_model report = new Report_model();
                report.review = name;
                report.user_code = user_code;
                report.koid = Convert.ToInt32(Dkoid);
                string result = "";
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(report), "KnockOffDel");
                result = (string)JsonConvert.DeserializeObject(post_data, result.GetType());
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                return Json(e.Message);
            }
        }
        public JsonResult ReportFilereportGrid_Read([DataSourceRequest]DataSourceRequest request, string period_from, string period_to, string Filetype, string Importby)
        {
            try 
            {
                Report_model report = new Report_model();
                report.period_from = period_from;
                report.period_to = period_to;
                report.file_type = Filetype;
                report.user_code = Importby;
                List<Report_model> objcat_lst = new List<Report_model>();
                DataTable result = new DataTable();
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(report), "FilereportRead");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    Report_model objcat = new Report_model();
                    objcat.file_gid = Convert.ToInt32(result.Rows[i][0]);
                    objcat.file_name = result.Rows[i][1].ToString();
                    objcat.file_type = result.Rows[i][2].ToString();
                    objcat.filetype_desc = result.Rows[i][3].ToString();
                    objcat.import_date = result.Rows[i][4].ToString();
                    objcat.review = result.Rows[i][5].ToString();
                    objcat.user_code = result.Rows[i][7].ToString();
                    objcat_lst.Add(objcat);
                }
                //Data1 = JsonConvert.SerializeObject(result);
                return Json(objcat_lst.ToDataSourceResult(request));
            }
            catch(Exception e)
            {
                return Json(e.Message);
            }
        }
        public ActionResult ReportFilereportGrid_del(string name, string Dkoid, string user_code)
        {
           try
           {
               Report_model report = new Report_model();
               report.review = name;
               report.user_code = user_code;
               report.file_gid = Convert.ToInt32(Dkoid);
               string result = "";
               string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(report), "FilereportDel");
               result = (string)JsonConvert.DeserializeObject(post_data, result.GetType());
               return Json(result, JsonRequestBehavior.AllowGet);
           }
            catch(Exception e)
           {
               return Json(e.Message);
           }
        }
        public JsonResult ReportreportBrs_Read([DataSourceRequest]DataSourceRequest request, string trandate, string Recon_gid)
        {
            try
            {
                Report_model report = new Report_model();
                if (trandate == "undefined-undefined-")
                {
                    report.tran_date = DateTime.Now.ToString("yyyy-MM-dd");
                }
                else
                {
                    report.tran_date = trandate;
                }
                report.recon_gid = Convert.ToInt32(Recon_gid);

                List<Report_model> objcat_lst = new List<Report_model>();
                DataTable result = new DataTable();
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(report), "ReportBrsRead");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());

            
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    Report_model objcat = new Report_model();
                    objcat.brs_gid = Convert.ToInt32(result.Rows[i][0]);
                    objcat.particular = result.Rows[i][1].ToString();
                    objcat.amount = result.Rows[i][2].ToString();
                    objcat.accmode = result.Rows[i][3].ToString();
                    string balanceamount=  result.Rows[i][4].ToString();

                    if(balanceamount!="")
                    {
                        Decimal balamount = Convert.ToDecimal(result.Rows[i][4].ToString());
                        objcat.bal_amount = Math.Abs(balamount).ToString();
                    }

                    else
                    {
                        objcat.bal_amount = result.Rows[i][4].ToString();
                    }
                    
                    objcat_lst.Add(objcat);
                }
                //Data1 = JsonConvert.SerializeObject(result);
                return Json(objcat_lst.ToDataSourceResult(request));
            }
            catch(Exception e)
            {
                return Json(e.Message);
            }
        }
        public JsonResult ReportreportBrs_Read_new([DataSourceRequest]DataSourceRequest request)
        {
            try
            {
                Report_model report = new Report_model();

                report.recon_gid = 0 ;
                report.active_status = "Y";
                List<Report_model> objcat_lst = new List<Report_model>();
                DataTable result = new DataTable();
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(report), "ReportBrsRead_new");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    Report_model objcat = new Report_model();
                    objcat.recon_gid= Convert.ToInt32(result.Rows[i]["recon_gid"]);
                    objcat.reconname = result.Rows[i]["recon_name"].ToString();
                    string recontallied_flag = result.Rows[i]["recon_tallied"].ToString();

                    if (recontallied_flag == "Y")
                    {
                        objcat.recon_tallied = "Yes";
                    }
                    else
                    {
                        objcat.recon_tallied = "No";
                    }
                    objcat.glcode = result.Rows[i]["acc_no2"].ToString();
                    objcat_lst.Add(objcat);
                }
                //Data1 = JsonConvert.SerializeObject(result);
                return Json(objcat_lst.ToDataSourceResult(request));
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }
        public JsonResult withinBrs_Read([DataSourceRequest]DataSourceRequest request, string trandate, string Recon_gid)
        {
            try
            {
                Report_model report = new Report_model();
                if (trandate == "undefined-undefined-")
                {
                    report.tran_date = DateTime.Now.ToString("yyyy-MM-dd");
                }
                else
                {
                    report.tran_date = trandate;
                }
                report.recon_gid = Convert.ToInt32(Recon_gid);

                List<Report_model> objcat_lst = new List<Report_model>();
                DataTable result = new DataTable();
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(report), "withinBrsRead");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    Report_model objcat = new Report_model();
                    objcat.brs_gid = Convert.ToInt32(result.Rows[i][0]);
                    objcat.particular = result.Rows[i][1].ToString();
                    objcat.amount = result.Rows[i][2].ToString();
                    objcat.accmode = result.Rows[i][3].ToString();
                    objcat.bal_amount = result.Rows[i][4].ToString();
                    objcat_lst.Add(objcat);
                }
                //Data1 = JsonConvert.SerializeObject(result);
                return Json(objcat_lst.ToDataSourceResult(request));

            }
            catch(Exception e)
            {
                return Json(e.Message);
            }
        }
        public ActionResult GetAccountMode_Flag([DataSourceRequest]DataSourceRequest request)
        {
            List<TransactionRpt_model> objcat_lst = new List<TransactionRpt_model>();
            try
            {

                TransactionRpt_model objcat = new TransactionRpt_model();
                objcat.Account_flag = "A";
                objcat.Account_desc = "All";
                objcat_lst.Add(objcat);
                objcat = new TransactionRpt_model();
                objcat.Account_flag = "C";
                objcat.Account_desc = "Credit";
                objcat_lst.Add(objcat);
                objcat = new TransactionRpt_model();
                objcat.Account_flag = "D";
                objcat.Account_desc = "Debit";
                objcat_lst.Add(objcat);

            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString(); LogHelper.WriteLog(ex.ToString(), control);
            }
            return Json(objcat_lst, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetStatus_Flag([DataSourceRequest]DataSourceRequest request)
        {
            List<TransactionRpt_model> objcat_lst = new List<TransactionRpt_model>();
            try
            {

                TransactionRpt_model objcat = new TransactionRpt_model();
                objcat.status_flag = "A";
                objcat.status_desc = "All";
                objcat_lst.Add(objcat);
                objcat = new TransactionRpt_model();
                objcat.status_flag = "excp_amount=0";
                objcat.status_desc = "Matched";
                objcat_lst.Add(objcat);
                objcat = new TransactionRpt_model();
                objcat.status_flag = "excp_amount>0";
                objcat.status_desc = "Exception";
                objcat_lst.Add(objcat);

            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString(); LogHelper.WriteLog(ex.ToString(), control);
            }
            return Json(objcat_lst, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Get_TransactionRpt([DataSourceRequest]DataSourceRequest request, TransactionRpt_model Obj_TransactionRpt)
        {
            List<TransactionRpt_model> objcat_lst = new List<TransactionRpt_model>();
            DataTable result = new DataTable();
            ////Report_model Transaction_Rpt = new Report_model();
            //Transaction_Rpt.table_name = table_name;
            //Transaction_Rpt.Report_condition = condition;
            try
            {
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(Obj_TransactionRpt), "ExectionReport");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());

                for (int i = 0; i < result.Rows.Count; i++)
                {
                    TransactionRpt_model objcat = new TransactionRpt_model();
                    objcat.Slno = i + 1;
                    objcat.Account_Name = result.Rows[i]["A/C Name"].ToString();
                    objcat.PaymentRef = result.Rows[i]["Payment Reference"].ToString();
                    objcat.Voucher_no = result.Rows[i]["Voucher No"].ToString();
                    objcat.Ref_no = result.Rows[i]["Ref No"].ToString();
                    if (result.Rows[i]["A/C Mode"].ToString().ToUpper() == "C")
                    {
                        objcat.Account_mode = "Credit";
                    }
                    else if (result.Rows[i]["A/C Mode"].ToString().ToUpper() == "D")
                    {
                        objcat.Account_mode = "Debit";
                    }
                    objcat.Amount_rpt = result.Rows[i]["Amount"].ToString();
                    objcat.Tran_desc = result.Rows[i]["Tran Desc"].ToString();
                    if (string.IsNullOrEmpty(result.Rows[i]["Value Date"].ToString()))
                    {
                        objcat.Value_date = "";
                    }
                    else
                    {
                        // DateTime Value_dt = Convert.ToDateTime(result.Rows[i]["Value Date"]);
                        objcat.Value_date = result.Rows[i]["Value Date"].ToString();
                    }
                    if (string.IsNullOrEmpty(result.Rows[i]["Tran Date"].ToString()))
                    {
                        objcat.Tran_date = "";
                    }
                    else
                    {
                        //DateTime tran_dt = Convert.ToDateTime(result.Rows[i]["Tran Date"]);
                        objcat.Tran_date = result.Rows[i]["Tran Date"].ToString();
                    }

                    objcat.Acc_No = result.Rows[i]["A/C No"].ToString();
                    objcat.Tran_id = result.Rows[i]["tran_id"].ToString();
                    objcat.Recon_Name = result.Rows[i]["Recon Name"].ToString();
                    objcat.Cheque_No = result.Rows[i]["Cheque No"].ToString();
                    objcat_lst.Add(objcat);
                }
                ////return Json(objcat_lst, JsonRequestBehavior.AllowGet);

                //return Json(result1);
                return Json(objcat_lst.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString(); LogHelper.WriteLog(ex.ToString(), control);
            }
            return View();
        }
        public ActionResult Get_SupportingFileRpt([DataSourceRequest]DataSourceRequest request, TransactionRpt_model Obj_TransactionRpt)
        {
            List<TransactionRpt_model> objcat_lst = new List<TransactionRpt_model>();
            DataTable result = new DataTable();
            try
            {
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(Obj_TransactionRpt), "ExectionReport");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());

                for (int i = 0; i < result.Rows.Count; i++)
                {
                    TransactionRpt_model objcat = new TransactionRpt_model();
                    objcat.Slno = i + 1;
                    objcat.Account_Name = result.Rows[i]["acc_name"].ToString();
                    objcat.Recon_Name = result.Rows[i]["recon_name"].ToString();
                    objcat.TranBkpType_Name = result.Rows[i]["tranbrkptype_name"].ToString();
                    objcat.TranBkpType_Gid = result.Rows[i]["tranbrkptype_gid"].ToString();
                    objcat.Acc_No = result.Rows[i]["acc_no"].ToString();
                    if (string.IsNullOrEmpty(result.Rows[i]["tran_date"].ToString()))
                    {
                        objcat.Tran_date = "";
                    }
                    else
                    {
                        DateTime tran_dt = Convert.ToDateTime(result.Rows[i]["tran_date"]);
                        objcat.Tran_date = tran_dt.ToString("dd-MM-yyyy");
                    }
                    if (string.IsNullOrEmpty(result.Rows[i]["value_date"].ToString()))
                    {
                        objcat.Value_date = "";
                    }
                    else
                    {
                        DateTime Value_dt = Convert.ToDateTime(result.Rows[i]["value_date"]);
                        objcat.Value_date = Value_dt.ToString("dd-MM-yyyy");
                    }
                    objcat.Tran_desc = result.Rows[i]["tran_desc"].ToString();
                    objcat.Amount_rpt = result.Rows[i]["amount"].ToString();
                    objcat.Excp_Amount = result.Rows[i]["excp_amount"].ToString();
                    if (result.Rows[i]["acc_mode"].ToString().ToUpper() == "C")
                    {
                        objcat.Account_mode = "Credit";
                    }
                    else if (result.Rows[i]["acc_mode"].ToString().ToUpper() == "D")
                    {
                        objcat.Account_mode = "Debit";
                    }
                    objcat.ref_col1 = result.Rows[i]["ref_col1"].ToString();
                    objcat.ref_col2 = result.Rows[i]["ref_col2"].ToString();
                    objcat.ref_col3 = result.Rows[i]["ref_col3"].ToString();
                    objcat.ref_col4 = result.Rows[i]["ref_col4"].ToString();
                    objcat.ref_col5 = result.Rows[i]["ref_col5"].ToString();
                    objcat.ref_col6 = result.Rows[i]["ref_col6"].ToString();
                    objcat.ref_col7 = result.Rows[i]["ref_col7"].ToString();
                    objcat.ref_col8 = result.Rows[i]["ref_col8"].ToString();
                    objcat.ref_col9 = result.Rows[i]["ref_col9"].ToString();
                    objcat.ref_col10 = result.Rows[i]["ref_col10"].ToString();
                    objcat.ref_col11 = result.Rows[i]["ref_col11"].ToString();
                    objcat.ref_col12 = result.Rows[i]["ref_col12"].ToString();
                    objcat.ref_col13 = result.Rows[i]["ref_col13"].ToString();
                    objcat.ref_col14 = result.Rows[i]["ref_col14"].ToString();
                    objcat.ref_col15 = result.Rows[i]["ref_col15"].ToString();
                    objcat.ref_col16 = result.Rows[i]["ref_col16"].ToString();
                    objcat.ref_col17 = result.Rows[i]["ref_col17"].ToString();
                    objcat.ref_col18 = result.Rows[i]["ref_col18"].ToString();
                    objcat.ref_col19 = result.Rows[i]["ref_col19"].ToString();
                    objcat.ref_col20 = result.Rows[i]["ref_col20"].ToString();
                    objcat.ref_col21 = result.Rows[i]["ref_col21"].ToString();
                    objcat.ref_col22 = result.Rows[i]["ref_col22"].ToString();
                    objcat.ref_col23 = result.Rows[i]["ref_col23"].ToString();
                    objcat.ref_col24 = result.Rows[i]["ref_col24"].ToString();
                    objcat.ref_col25 = result.Rows[i]["ref_col25"].ToString();
                    objcat.ref_col26 = result.Rows[i]["ref_col26"].ToString();
                    objcat.ref_col27 = result.Rows[i]["ref_col27"].ToString();
                    objcat.ref_col28 = result.Rows[i]["ref_col28"].ToString();
                    objcat.ref_col29 = result.Rows[i]["ref_col29"].ToString();
                    objcat.ref_col30 = result.Rows[i]["ref_col30"].ToString();
                    objcat.ref_col31 = result.Rows[i]["ref_col31"].ToString();
                    objcat.ref_col32 = result.Rows[i]["ref_col32"].ToString();

                    if (string.IsNullOrEmpty(result.Rows[i]["ko_date"].ToString()))
                    {
                        objcat.ko_date = "";
                    }
                    else
                    {
                        DateTime ko_dt = Convert.ToDateTime(result.Rows[i]["ko_date"]);
                        objcat.ko_date = ko_dt.ToString("dd-MM-yyyy");
                    }


                    objcat.tranrecon_excp_amount = result.Rows[i]["tranrecon_excp_amount"].ToString();
                    objcat.mapped_amount = result.Rows[i]["mapped_amount"].ToString();
                    objcat.tran_remark1 = result.Rows[i]["tran_remark1"].ToString();
                    objcat.tran_remark2 = result.Rows[i]["tran_remark2"].ToString();

                    if (string.IsNullOrEmpty(result.Rows[i]["tranrecon_ko_date"].ToString()))
                    {
                        objcat.tranrecon_ko_date = "";
                    }
                    else
                    {
                        DateTime tranreconko_dt = Convert.ToDateTime(result.Rows[i]["tranrecon_ko_date"]);
                        objcat.tranrecon_ko_date = tranreconko_dt.ToString("dd-MM-yyyy");
                    }
                    if (string.IsNullOrEmpty(result.Rows[i]["import_date"].ToString()))
                    {
                        objcat.import_date = "";
                    }
                    else
                    {
                        DateTime Import_dt = Convert.ToDateTime(result.Rows[i]["import_date"]);
                        objcat.import_date = Import_dt.ToString("dd-MM-yyyy");
                    }
                    objcat.import_by = result.Rows[i]["import_by"].ToString();

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
      
        public ActionResult ReportProcessLog_Read([DataSourceRequest]DataSourceRequest request, string period_from, string period_to, string Initiatedby, string status)   // ProcessLog  Read
        {
            try
            {
                ProgressView_model report = new ProgressView_model();
                report.period_from = period_from;
                report.period_to = period_to;
                report.InitiatedBy = Initiatedby;
                report.process_status = status;
                List<ProgressView_model> objcat_lst = new List<ProgressView_model>();
                DataTable result = new DataTable();
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(report), "ProcessLogReport");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    ProgressView_model objcat = new ProgressView_model();
                    objcat.process_gid = Convert.ToInt32(result.Rows[i]["process_gid"]);
                    objcat.start_date = result.Rows[i]["start_date"].ToString();
                    objcat.end_date = result.Rows[i]["end_date"].ToString();
                    objcat.ProcessName = result.Rows[i]["process_name"].ToString();
                    objcat.process_status = result.Rows[i]["process_status"].ToString();
                    objcat.process_status_desc = result.Rows[i]["process_status_desc"].ToString();
                    objcat.InitiatedBy = result.Rows[i]["process_initiated_by"].ToString();
                    objcat.ip_addr = result.Rows[i]["ip_addr"].ToString();
                    objcat_lst.Add(objcat);
                }
                //Data1 = JsonConvert.SerializeObject(result);
                return Json(objcat_lst.ToDataSourceResult(request));
            }
            catch(Exception e)
            {
                return Json(e.Message);
            }

        }
        public JsonResult ReportReversedTransactions(string table_name, string condition)
        {
            try
            {
                Report_model report = new Report_model();
                report.table_name = table_name;
                report.Report_condition = condition;

                DataTable result = new DataTable();
                string Data1 = "";
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(report), "ExectionReport");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());

                Data1 = JsonConvert.SerializeObject(result);

                //return Json(Data1, JsonRequestBehavior.AllowGet);
                var jsonResult = Json(Data1, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch(Exception e)
            {
                return Json(e.Message);
            }
        }
        public ActionResult reportfield_type(string code)
        {
            try
            {

                Report_model reportfieldtype = new Report_model();
                reportfieldtype.file_type = code;
                List<Report_model> objcat_lst = new List<Report_model>();
                DataTable result = new DataTable();
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(reportfieldtype), "ReportFieldtype");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    Report_model objcat = new Report_model();
                    objcat.field_width = Convert.ToInt32(result.Rows[i]["field_width"]);
                    objcat.field_type = result.Rows[i]["field_type"].ToString();
                    objcat_lst.Add(objcat);
                }
                return Json(objcat_lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            return View();
        }
        public ActionResult getreportfile_name(string date)
        {
            try
            {
                Report_model reportfilename = new Report_model();
                reportfilename.period_from = date;

                List<Report_model> objcat_lst = new List<Report_model>();
                DataTable result = new DataTable();
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(reportfilename), "ReportFilename");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    Report_model objcat = new Report_model();
                    objcat.file_gid = Convert.ToInt32(result.Rows[i]["file_gid"]);
                    objcat.file_name = result.Rows[i]["file_name"].ToString();
                    objcat_lst.Add(objcat);
                }
                return Json(objcat_lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString(); 
                LogHelper.WriteLog(ex.ToString(), control);
            }
            return View();
        }
        public ActionResult ReportFileerror_Read([DataSourceRequest]DataSourceRequest request, string file_gid)
        {
            try
            {
                Report_model reportfileerror = new Report_model();
                reportfileerror.file_gid = Convert.ToInt32(file_gid);
                List<Report_model> objcat_lst = new List<Report_model>();
                DataTable result = new DataTable();
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(reportfileerror), "ReportFileError");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    Report_model objcat = new Report_model();
                    objcat.errline_gid = Convert.ToInt32(result.Rows[i]["errline_gid"]);
                    objcat.errline_no = Convert.ToInt32(result.Rows[i]["errline_no"]);
                    objcat.errline_desc = result.Rows[i]["errline_desc"].ToString();
                    objcat.file_name = result.Rows[i]["file_name"].ToString();
                    objcat_lst.Add(objcat);
                }
                var jsonResult = Json(objcat_lst.ToDataSourceResult(request));
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString(); 
                LogHelper.WriteLog(ex.ToString(), control);
            }
            return View();
        }
        public ActionResult Reporterrorlog_Read(string period_from, string period_to)
        {
            try
            {
                Report_model reporterrorlog = new Report_model();
                reporterrorlog.period_from = period_from;
                reporterrorlog.period_to = period_to;
                List<Report_model> objcat_lst = new List<Report_model>();
                DataTable result = new DataTable();
                string Data1 = "";
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(reporterrorlog), "ReportErrorLog");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
                Data1 = JsonConvert.SerializeObject(result);
                var jsonResult = Json(Data1, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString(); 
                LogHelper.WriteLog(ex.ToString(), control);
            }
            return View();
        }
        public ActionResult getuserrecon_All()
        {
            try
            {

                List<TransactionRpt_model> objcat_3st = new List<TransactionRpt_model>();
                DataTable result2 = new DataTable();
                TransactionRpt_model DedupList = new TransactionRpt_model();
                DedupList.user_code = user_codes;
                DedupList.active_status = "Y";
                DedupList.recontype = null;
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(DedupList), "Getuserrecon");
                result2 = (DataTable)JsonConvert.DeserializeObject(post_data, result2.GetType());

                TransactionRpt_model objcat3 = new TransactionRpt_model();
                objcat3.ReconName_id = "0";
                objcat3.ReconName = "SelectAll";
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

        public ActionResult getuserrecon()
        {
            try
            {

                List<TransactionRpt_model> objcat_3st = new List<TransactionRpt_model>();
                DataTable result2 = new DataTable();
                TransactionRpt_model DedupList = new TransactionRpt_model();
                DedupList.user_code = user_codes;
                DedupList.active_status = "Y";
                DedupList.recontype = null;
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(DedupList), "Getuserrecon");
                result2 = (DataTable)JsonConvert.DeserializeObject(post_data, result2.GetType());

                TransactionRpt_model objcat3 = new TransactionRpt_model();
                objcat3.ReconName_id = "0";
                objcat3.ReconName = "Select";
                objcat_3st.Add(objcat3);

                for (int i = 0; i < result2.Rows.Count; i++)
                {
                    objcat3 = new TransactionRpt_model();
                    objcat3.ReconName_id= (result2.Rows[i]["recon_gid"].ToString());
                    objcat3.ReconName= result2.Rows[i]["recon_name"].ToString();
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

        public ActionResult getuserrecon_type()
        {
            try
            {
                string fieldtype = "B";
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

        public ActionResult getuserrecon_type_within(string fieldtype)
        {
            try
            {
               // string fieldtype = "B";
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

        public JsonResult binddrop()//drop down
        {
            try
            {
                
                List<TransactionRpt_model> objcat_3st = new List<TransactionRpt_model>();
                DataTable result2 = new DataTable();
                TransactionRpt_model DedupList = new TransactionRpt_model();
                //DedupList.valuedrop = value_drop;
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(DedupList), "dropvalue");
                result2 = (DataTable)JsonConvert.DeserializeObject(post_data, result2.GetType());

                TransactionRpt_model objcat3 = new TransactionRpt_model();
                //objcat3.report_gid = 0;
                //objcat3.report_desc = "";
                //objcat_3st.Add(objcat3);

                for (int i = 0; i < result2.Rows.Count; i++)
                {
                    objcat3 = new TransactionRpt_model();
                    objcat3.report_gid = Convert.ToInt32(result2.Rows[i][0].ToString());
                    objcat3.report_desc = result2.Rows[i][1].ToString();
                    objcat_3st.Add(objcat3);
                }
                return Json(objcat_3st, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        public JsonResult dropdownvalue(int value_drop)//drop down
        {
            try
            {
                List<TransactionRpt_model> objcat_3st = new List<TransactionRpt_model>();
                DataTable result2 = new DataTable();
                TransactionRpt_model DedupList = new TransactionRpt_model();
                DedupList.valuedrop = value_drop;
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(DedupList), "bindropvalue");
                result2 = (DataTable)JsonConvert.DeserializeObject(post_data, result2.GetType());

                TransactionRpt_model objcat3 = new TransactionRpt_model();
                //objcat3.report_gid = 0;
                //objcat3.report_desc = "--select--";
                //objcat_3st.Add(objcat3);

                for (int i = 0; i < result2.Rows.Count; i++)
                {
                    objcat3 = new TransactionRpt_model();
                    objcat3.reportid = Convert.ToInt32(result2.Rows[i][0].ToString());
                    objcat3.reportparam_value = result2.Rows[i][2].ToString();
                    objcat3.reportparam_code = result2.Rows[i][1].ToString();
                    objcat_3st.Add(objcat3);
                }
                return Json(objcat_3st, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }
        public ActionResult Report_KnocoffMIS_New(string period_from, string period_to, string Recongid)
        {
            try
            {
                DataRow dr;
                List<TransactionRpt_model> objcat_3st = new List<TransactionRpt_model>();
                List<ReportKnockoffMIS> objcat_rpt = new List<ReportKnockoffMIS>();
                string Data1 = "";
                        
                DataSet result2 = new DataSet();
                DataTable Table = new DataTable();
                DataTable Table1 = new DataTable();
                DataTable dts = new DataTable();
                TransactionRpt_model DedupList = new TransactionRpt_model();

                DedupList.Period_from = period_from;
                DedupList.Period_To = period_to;
                DedupList.Recon_gid = Recongid;
                DedupList.user_code = user_codes;
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(DedupList), "ReportKnocoffMIS");
               result2 = (DataSet)JsonConvert.DeserializeObject(post_data, result2.GetType());
                //List<DataRow> rows = result2.Tables[0].Rows.Cast<DataRow>().ToList();
               // List<dynamic> dynamicDt = result2.Tables[0].ToDynamic();
                List<dynamic> dynamicDt = new List<dynamic>();
                foreach (DataRow row in result2.Tables[0].Rows)
                {
                    dynamic dyn = new ExpandoObject();
                    dynamicDt.Add(dyn);
                    foreach (DataColumn column in result2.Tables[0].Columns)
                    {
                        var dic = (IDictionary<string, object>)dyn;
                        dic[column.ColumnName] = row[column];
                    }
                }
                //for (int i = 0; i < result2.Tables[0].Rows.Count; i++)
                //{

                //    ReportKnockoffMIS objcat3 = new ReportKnockoffMIS();
                //    //objcat3.nullvalues = 1;
                //    objcat3.rowslabel = (result2.Tables[0].Rows[i]["Row Labels"].ToString());
                //    objcat3.drcount = (result2.Tables[0].Rows[i]["Dr Count"].ToString());
                //    objcat3.dramount = (result2.Tables[0].Rows[i]["Dr Amount"].ToString());
                //    objcat3.crcount = (result2.Tables[0].Rows[i]["Cr Count"].ToString());
                //    objcat3.cramount = (result2.Tables[0].Rows[i]["Cr Amount"].ToString());
                //    objcat3.TotalcountofNarration = (result2.Tables[0].Rows[i]["Total Count"].ToString());
                //    objcat3.Totalsumofamount = (result2.Tables[0].Rows[i]["Total Amount"].ToString());
                //    objcat3.backcolor = (result2.Tables[0].Rows[i]["backcolor"].ToString());
                //    objcat3.forecolor = (result2.Tables[0].Rows[i]["forecolor"].ToString());
                //    objcat_rpt.Add(objcat3);
                //}
                Data1 = JsonConvert.SerializeObject(dynamicDt);
                return Json(Data1, JsonRequestBehavior.AllowGet);



            
                //return Json(objcat_rpt, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
            ///return View();
        }


   
        public ActionResult Report_KnocoffMIS([DataSourceRequest]DataSourceRequest request, string period_from, string period_to, string Recongid)
        {
            try
            {
                DataRow dr;
                List<TransactionRpt_model> objcat_3st = new List<TransactionRpt_model>();
                DataSet result2 = new DataSet();
                DataTable Table = new DataTable();
                DataTable Table1 = new DataTable();
                DataTable dts = new DataTable();
                TransactionRpt_model DedupList = new TransactionRpt_model();

                DedupList.Period_from = period_from;
                DedupList.Period_To = period_to;
                DedupList.Recon_gid = Recongid;
                DedupList.user_code = user_codes;
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(DedupList), "ReportKnocoffMIS");
                result2 = (DataSet)JsonConvert.DeserializeObject(post_data, result2.GetType());

                for (int i = 0; i < result2.Tables[0].Rows.Count; i++)
                {

                    TransactionRpt_model objcat3 = new TransactionRpt_model();
                    objcat3.nullvalues = 1;
                    objcat3.rowslabel = (result2.Tables[0].Rows[i]["Row Labels"].ToString());
                    objcat3.drcount = (result2.Tables[0].Rows[i]["Dr Count"].ToString());
                    objcat3.dramount = (result2.Tables[0].Rows[i]["Dr Amount"].ToString());
                    objcat3.crcount = (result2.Tables[0].Rows[i]["Cr Count"].ToString());
                    objcat3.cramount = (result2.Tables[0].Rows[i]["Cr Amount"].ToString());
                    objcat3.TotalcountofNarration = (result2.Tables[0].Rows[i]["Total Count"].ToString());
                    objcat3.Totalsumofamount = (result2.Tables[0].Rows[i]["Total Amount"].ToString());
                    objcat3.backcolor = (result2.Tables[0].Rows[i]["backcolor"].ToString());
                    objcat3.forecolor = (result2.Tables[0].Rows[i]["forecolor"].ToString());
                    objcat_3st.Add(objcat3);
                }
                return Json(objcat_3st.ToDataSourceResult(request));

            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
            return View();
        }

        public ActionResult Report_KnocoffMIS_closedxml([DataSourceRequest]DataSourceRequest request, string period_from, string period_to, string Recongid)
        {
            try
            {
                DataRow dr;
                int subtotal_count = 0;
                string filename = "Knockoff MIS.xlsx";
                List<TransactionRpt_model> objcat_3st = new List<TransactionRpt_model>();
                DataSet result2 = new DataSet();
                DataTable Table = new DataTable();
                DataTable Table1 = new DataTable();
                DataTable dts = new DataTable();
                TransactionRpt_model DedupList = new TransactionRpt_model();

                DedupList.Period_from = period_from;
                DedupList.Period_To = period_to;
                DedupList.Recon_gid = Recongid;
                DedupList.user_code = user_codes;
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(DedupList), "ReportKnocoffMIS");
                result2 = (DataSet)JsonConvert.DeserializeObject(post_data, result2.GetType());



                Table1 = result2.Tables[0].Copy();
                Table1.Columns.RemoveAt(7);
                Table1.Columns.RemoveAt(7);
                Table1.Columns[0].ColumnName = "Rows Labels";
                Table1.Columns[1].ColumnName = "Debit Count";
                Table1.Columns[2].ColumnName = "Debit Amount";
                Table1.Columns[3].ColumnName = "Credit Count";
                Table1.Columns[4].ColumnName = "Credit Amount";
                Table1.Columns[5].ColumnName = "Total Count";
                Table1.Columns[6].ColumnName = "Total Amount";

                for(int i=0 ;i<Table1.Rows.Count;i++)
                {
                    string rowslabel = Table1.Rows[i]["Rows Labels"].ToString();
                    if(rowslabel.Contains("Sub Total"))
                    {
                       subtotal_count = i +4;
                       break;
                    }
                   
                }

                int rowscount = Table1.Rows.Count + 3;
                string celltxt = "A" + rowscount.ToString() + "";
                string celltxt2 = "G" + rowscount.ToString() + "";
                string rangetxt = celltxt + ":"+ celltxt2;
                using (XLWorkbook wb = new XLWorkbook())
                {

                    var ws = wb.AddWorksheet("Recon-Knockoff MIS");

                    //Insrting Table1 Data
                    ws.Cell("A1").SetValue("FIVE-STAR BUSINESS FINANCE LTD"); ws.Cell("A1").Style.Font.Bold = true;
                    ws.Cell("A1").Style.Font.Underline = XLFontUnderlineValues.Single;
                    ws.Cell("A4").Style.Font.Bold = true; ws.Cell("A5").Style.Font.Bold = true;
                    ws.Cell("A3").InsertTable(Table1);
                    wb.Worksheet(1).Table(0).ShowAutoFilter = false;// Disable AutoFilter.
                    wb.Worksheet(1).Table(0).Theme = XLTableTheme.None;
                    wb.Worksheet(1).Table(0).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    wb.Worksheet(1).Table(0).Style.Border.BottomBorderColor = XLColor.Black;
                    wb.Worksheet(1).Table(0).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    wb.Worksheet(1).Table(0).Style.Border.TopBorderColor = XLColor.Black;
                    wb.Worksheet(1).Table(0).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    wb.Worksheet(1).Table(0).Style.Border.LeftBorderColor = XLColor.Black;
                    wb.Worksheet(1).Table(0).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    wb.Worksheet(1).Table(0).Style.Border.RightBorderColor = XLColor.Black;
                    ws.Range("A3:G3").Style.Font.Bold = true;
                    ws.Range("A3:G3").Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);
                    ws.Range(rangetxt).Style.Font.Bold = true;
                    ws.Range(rangetxt).Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);

                     rowscount = rowscount - 2;
                     celltxt = "A" + rowscount.ToString() + "";
                     celltxt2 = "G" + rowscount.ToString() + "";
                     rangetxt = celltxt + ":" + celltxt2;
                     ws.Range(rangetxt).Style.Font.Bold = true;
                     ws.Range(rangetxt).Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);

                     celltxt = "A" + subtotal_count.ToString() + "";
                     celltxt2 = "G" + subtotal_count.ToString() + "";
                     rangetxt = celltxt + ":" + celltxt2;
                     ws.Range(rangetxt).Style.Font.Bold = true;
                     ws.Range(rangetxt).Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);

                     subtotal_count = subtotal_count + 1;
                     celltxt = "A" + subtotal_count.ToString() + "";
                     ws.Cell(celltxt).Style.Font.Bold = true;
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
                    }
                }

            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
            return View();
        }
        //public ActionResult Report_KnocoffMIS([DataSourceRequest]DataSourceRequest request, string period_from, string period_to, string Recongid)
        //{
        //    try
        //    {
        //        DataRow dr;
        //        List<TransactionRpt_model> objcat_3st = new List<TransactionRpt_model>();
        //        DataSet result2 = new DataSet();
        //        DataTable Table = new DataTable();
        //        DataTable Table1 = new DataTable();
        //        DataTable dts = new DataTable();
        //        TransactionRpt_model DedupList = new TransactionRpt_model();

        //        DedupList.Period_from = period_from;
        //        DedupList.Period_To = period_to;
        //        DedupList.Recon_gid = Recongid;
        //        DedupList.user_code = user_codes;
        //        string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(DedupList), "ReportKnocoffMIS");
        //        result2 = (DataSet)JsonConvert.DeserializeObject(post_data, result2.GetType());
        //        Table = result2.Tables[0].Copy();
        //        Table1 = result2.Tables[1].Copy();
        //        dts.Columns.Add("Rows Labels");
        //        dts.Columns.Add("Dr Count", typeof(decimal));
        //        dts.Columns.Add("Cr Count", typeof(decimal));
        //        dts.Columns.Add("Dr Amount", typeof(decimal));
        //        dts.Columns.Add("Cr Amount", typeof(decimal));
        //        dts.Columns.Add("Total Count", typeof(decimal));
        //        dts.Columns.Add("Total Amount", typeof(decimal));
        //        dts.Columns.Add("color", typeof(string));

        //        dr = dts.NewRow();
        //        dr[0] = "Manual KO";
        //        dr[7] = "Blue";
        //        dts.Rows.Add(dr);
        //        for (int i = 0; i < result2.Tables[0].Rows.Count; i++)
        //        {
        //            DedupList = new TransactionRpt_model();


        //            dr = dts.NewRow();
        //            string rulename = Table.Rows[i]["rule_name"].ToString();
        //            dr[0] = "    " + rulename;
        //            dr[1] = Math.Round(Convert.ToDecimal(Table.Rows[i]["dr_count"].ToString()), 2);
        //            dr[2] = Math.Round(Convert.ToDecimal(Table.Rows[i]["cr_count"].ToString()), 2);
        //            //dr[3] = Math.Round(Convert.ToDecimal(Table.Rows[i]["dr_amount"].ToString()),2);
        //            //dr[4] = Math.Round(Convert.ToDecimal(Table.Rows[i]["cr_amount"].ToString()),2);
        //            Decimal drcount = Math.Round(Convert.ToDecimal(Table.Rows[i]["dr_count"].ToString()), 2);
        //            Decimal crcount = Math.Round(Convert.ToDecimal(Table.Rows[i]["cr_count"].ToString()), 2);
        //            Decimal dramount = Math.Round(Convert.ToDecimal(Table.Rows[i]["dr_amount"].ToString()), 2);
        //            Decimal cramount = Math.Round(Convert.ToDecimal(Table.Rows[i]["cr_amount"].ToString()), 2);
        //            dr[3] = dramount.ToString("#.00");
        //            dr[4] = cramount.ToString("#.00");
        //            dr[5] = drcount + crcount;
        //            Decimal total = dramount + cramount;
        //            dr[6] = total.ToString("#.00");
        //            dr[7] = "Black";
        //            dts.Rows.Add(dr);


        //        }
        //        dr = dts.NewRow();
        //        dr[0] = "Rule Based Matched";
        //        dr[7] = "Blue";
        //        dts.Rows.Add(dr);
        //        for (int i = 0; i < result2.Tables[1].Rows.Count; i++)
        //        {
        //            dr = dts.NewRow();
        //            string rulename = Table1.Rows[i]["rule_name"].ToString();
        //            dr[0] = "    " + rulename;
        //            dr[1] = Math.Round(Convert.ToDecimal(Table1.Rows[i]["dr_count"].ToString()), 2);
        //            dr[2] = Math.Round(Convert.ToDecimal(Table1.Rows[i]["cr_count"].ToString()), 2);
        //            //dr[3] = Math.Round(Convert.ToDecimal(Table1.Rows[i]["dr_amount"].ToString()),2);
        //            //dr[4] = Math.Round(Convert.ToDecimal(Table1.Rows[i]["cr_amount"].ToString()),2);
        //            Decimal drcount = Math.Round(Convert.ToDecimal(Table1.Rows[i]["dr_count"].ToString()), 2);
        //            Decimal crcount = Math.Round(Convert.ToDecimal(Table1.Rows[i]["cr_count"].ToString()), 2);
        //            Decimal dramount = Math.Round(Convert.ToDecimal(Table1.Rows[i]["dr_amount"].ToString()), 2);
        //            Decimal cramount = Math.Round(Convert.ToDecimal(Table1.Rows[i]["cr_amount"].ToString()), 2);
        //            dr[3] = dramount.ToString("#.00");
        //            dr[4] = cramount.ToString("#.00");
        //            dr[5] = drcount + crcount;
        //            Decimal total = dramount + cramount;
        //            dr[6] = total.ToString("#.00");
        //            dr[7] = "Black";
        //            dts.Rows.Add(dr);
        //        }
        //        Decimal dr_count = dts.AsEnumerable().Sum(row => row.Field<decimal?>("Dr Count") ?? 0);
        //        Decimal cr_count = dts.AsEnumerable().Sum(row => row.Field<decimal?>("Cr Count") ?? 0);
        //        Decimal dr_amount = dts.AsEnumerable().Sum(row => row.Field<decimal?>("Dr Amount") ?? 0);
        //        Decimal cr_amount = dts.AsEnumerable().Sum(row => row.Field<decimal?>("Cr Amount") ?? 0);
        //        Decimal tot_count = dts.AsEnumerable().Sum(row => row.Field<decimal?>("Total Count") ?? 0);
        //        Decimal tot_amount = dts.AsEnumerable().Sum(row => row.Field<decimal?>("Total Amount") ?? 0);

        //        dr = dts.NewRow();
        //        dr[0] = "Grand Total";
        //        dr[1] = dr_count;
        //        dr[2] = cr_count;
        //        dr[3] = dr_amount;
        //        dr[4] = cr_amount;
        //        dr[5] = tot_count;
        //        dr[6] = tot_amount;
        //        dr[7] = "Red";
        //        dts.Rows.Add(dr);

        //        for (int i = 0; i < dts.Rows.Count; i++)
        //        {
        //            TransactionRpt_model objcat3 = new TransactionRpt_model();
        //            objcat3.rowslabel = (dts.Rows[i]["Rows Labels"].ToString());
        //            objcat3.crcount = (dts.Rows[i]["Cr Count"].ToString());
        //            objcat3.drcount = (dts.Rows[i]["Dr Count"].ToString());
        //            objcat3.cramount = (dts.Rows[i]["Cr Amount"].ToString());
        //            objcat3.dramount = (dts.Rows[i]["Dr Amount"].ToString());
        //            objcat3.TotalcountofNarration = (dts.Rows[i]["Total Count"].ToString());
        //            objcat3.Totalsumofamount = (dts.Rows[i]["Total Amount"].ToString());
        //            objcat3.color = (dts.Rows[i]["color"].ToString());
        //            objcat_3st.Add(objcat3);
        //        }
        //        return Json(objcat_3st.ToDataSourceResult(request));

        //    }
        //    catch (Exception e)
        //    {
        //        return Json(e.Message);
        //    }
        //    return View();
        //}
        public ActionResult KnockoffMIS_Download(string periodfrom, string periodto, string Recongid)
        {
            DataRow dr;
            List<TransactionRpt_model> objcat_3st = new List<TransactionRpt_model>();
            DataSet result2 = new DataSet();
            DataTable Table = new DataTable();
            DataTable Table1 = new DataTable();
            DataTable dts = new DataTable();
            TransactionRpt_model DedupList = new TransactionRpt_model();

            DedupList.Period_from = periodfrom;
            DedupList.Period_To = periodto;
            DedupList.Recon_gid = Recongid;
            DedupList.user_code = user_codes;
            string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(DedupList), "ReportKnocoffMIS");
            result2 = (DataSet)JsonConvert.DeserializeObject(post_data, result2.GetType());
            Table = result2.Tables[0].Copy();
            Table1 = result2.Tables[1].Copy();
            dts.Columns.Add("Rows Labels");
            dts.Columns.Add("Dr Count", typeof(decimal));
            dts.Columns.Add("Cr Count", typeof(decimal));
            dts.Columns.Add("Dr Amount", typeof(decimal));
            dts.Columns.Add("Cr Amount", typeof(decimal));
            dts.Columns.Add("Total Count", typeof(decimal));
            dts.Columns.Add("Total Amount", typeof(decimal));
            dr = dts.NewRow();
            dr[0] = "Manual KO";
            dts.Rows.Add(dr);
            for (int i = 0; i < result2.Tables[0].Rows.Count; i++)
            {
                DedupList = new TransactionRpt_model();
                dr = dts.NewRow();
                string rulename = Table.Rows[i]["rule_name"].ToString();
                dr[0] = "    " + rulename;
                dr[1] = Table.Rows[i]["dr_count"].ToString();
                dr[2] = Table.Rows[i]["cr_count"].ToString();
                Decimal drcount = Convert.ToDecimal(Table.Rows[i]["dr_count"].ToString());
                Decimal crcount = Convert.ToDecimal(Table.Rows[i]["cr_count"].ToString());
                Decimal dramount = Convert.ToDecimal(Table.Rows[i]["dr_amount"].ToString());
                Decimal cramount = Convert.ToDecimal(Table.Rows[i]["cr_amount"].ToString());
                dr[3] = dramount.ToString("#.00");
                dr[4] = cramount.ToString("#.00");
                dr[5] = drcount + crcount;
                Decimal total = dramount + cramount;
                dr[6] = total.ToString("#.00");
                dts.Rows.Add(dr);


            }
            dr = dts.NewRow();
            dr[0] = "Rule Based Matched";
            dts.Rows.Add(dr);
            for (int i = 0; i < result2.Tables[1].Rows.Count; i++)
            {
                dr = dts.NewRow();
                string rulename = Table1.Rows[i]["rule_name"].ToString();
                dr[0] = "    " + rulename;
                dr[1] = Table1.Rows[i]["dr_count"].ToString();
                dr[2] = Table1.Rows[i]["cr_count"].ToString();
                //dr[3] = Table1.Rows[i]["dr_amount"].ToString();
                //dr[4] = Table1.Rows[i]["cr_amount"].ToString();
                Decimal drcount = Convert.ToDecimal(Table1.Rows[i]["dr_count"].ToString());
                Decimal crcount = Convert.ToDecimal(Table1.Rows[i]["cr_count"].ToString());
                Decimal dramount = Convert.ToDecimal(Table1.Rows[i]["dr_amount"].ToString());
                Decimal cramount = Convert.ToDecimal(Table1.Rows[i]["cr_amount"].ToString());
                dr[3] = dramount.ToString("#.00");
                dr[4] = cramount.ToString("#.00");
                dr[5] = drcount + crcount;
                Decimal total = dramount + cramount;
                dr[6] = total.ToString("#.00");
                dts.Rows.Add(dr);
            }
            Decimal creditAmount = dts.AsEnumerable().Sum(row => row.Field<decimal?>("Dr Count") ?? 0);
            Decimal DebitAmount = dts.AsEnumerable().Sum(row => row.Field<decimal?>("Cr Count") ?? 0);
            Decimal credit_Amount = dts.AsEnumerable().Sum(row => row.Field<decimal?>("Dr Amount") ?? 0);
            Decimal Debit_Amount = dts.AsEnumerable().Sum(row => row.Field<decimal?>("Cr Amount") ?? 0);
            Decimal TotalCountofNarration = dts.AsEnumerable().Sum(row => row.Field<decimal?>("Total Count") ?? 0);
            Decimal TotalSumofAmount = dts.AsEnumerable().Sum(row => row.Field<decimal?>("Total Amount") ?? 0);

            dr = dts.NewRow();
            dr[0] = "Grand Total";
            dr[1] = creditAmount;
            dr[2] = DebitAmount;
            dr[3] = credit_Amount;
            dr[4] = Debit_Amount;
            dr[5] = TotalCountofNarration;
            dr[6] = TotalSumofAmount;
            dts.Rows.Add(dr);

            string fileName = "Sample.xlsx";
            using (XLWorkbook wb = new XLWorkbook())// wb.Worksheets.Add(dt);
            {
                var ws = wb.AddWorksheet("Sheet1");

                ws.Cell("A1").Style.Font.Bold = true; ws.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                ws.Cell("B1").Style.Font.Bold = true; ws.Cell("B1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                ws.Cell("C1").Style.Font.Bold = true; ws.Cell("C1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                ws.Cell("D1").Style.Font.Bold = true; ws.Cell("D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                ws.Cell("E1").Style.Font.Bold = true; ws.Cell("E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                ws.Cell("F1").Style.Font.Bold = true; ws.Cell("F1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                ws.Cell("G1").Style.Font.Bold = true; ws.Cell("G1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                ws.Cell("A2").Style.Font.Bold = true;
                wb.Worksheet(1).Cell("A1").InsertTable(dts);
                //ws.Table(0).ShowAutoFilter = false;// Disable AutoFilter.
                //ws.Table(0).Theme = XLTableTheme.TableStyleMedium10;




                int Table1Position = 2 + Table.Rows.Count + 1;
                string count = "A" + Table1Position + "";
                ws.Cell(count).Style.Font.Bold = true;//rule

                int Table2Position = Table1Position + Table1.Rows.Count + 1;
                string count1 = "A" + Table2Position + ""; string total = "B" + Table2Position + ""; string total1 = "C" + Table2Position + "";
                string total2 = "D" + Table2Position + ""; string total3 = "E" + Table2Position + ""; string total4 = "F" + Table2Position + "";
                string total5 = "G" + Table2Position + "";
                ws.Cell(count1).Style.Font.Bold = true; ws.Cell(total5).Style.Font.Bold = true; ws.Cell(total5).Style.Fill.BackgroundColor = XLColor.FromHtml("#E8E8E8");
                ws.Cell(total).Style.Font.Bold = true; ws.Cell(total).Style.Fill.BackgroundColor = XLColor.FromHtml("#E8E8E8");
                ws.Cell(total1).Style.Font.Bold = true; ws.Cell(total1).Style.Fill.BackgroundColor = XLColor.FromHtml("#E8E8E8");
                ws.Cell(total2).Style.Font.Bold = true; ws.Cell(total2).Style.Fill.BackgroundColor = XLColor.FromHtml("#E8E8E8");
                ws.Cell(total3).Style.Font.Bold = true; ws.Cell(total3).Style.Fill.BackgroundColor = XLColor.FromHtml("#E8E8E8");
                ws.Cell(total4).Style.Font.Bold = true; ws.Cell(total4).Style.Fill.BackgroundColor = XLColor.FromHtml("#E8E8E8");
                ws.Cell(count1).Style.Fill.BackgroundColor = XLColor.FromHtml("#E8E8E8");
                var co = 4; var col1 = 5; var col3 = 7;
                //var ro =Table2Position;
                for (int i = 1; i <= Table2Position; i++)
                {
                    ws.Cell(i, co).Style.NumberFormat.Format = "##0.00";
                }

                for (int i = 1; i <= Table2Position; i++)
                {
                    ws.Cell(i, col1).Style.NumberFormat.Format = "##0.00";
                }

                for (int i = 1; i <= Table2Position; i++)
                {
                    ws.Cell(i, col3).Style.NumberFormat.Format = "##0.00";
                }

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }

            }

            return View();
        }
       

        //public ActionResult Report_KnocoffMIS([DataSourceRequest]DataSourceRequest request, string period_from, string period_to, string Recongid)
        // {
        //    try
        //    {
        //        DataRow dr;
        //        List<TransactionRpt_model> objcat_3st = new List<TransactionRpt_model>();
        //        DataSet result2 = new DataSet();
        //        DataTable Table = new DataTable();
        //        DataTable Table1 = new DataTable();
        //        DataTable dts = new DataTable();
        //        TransactionRpt_model DedupList = new TransactionRpt_model();
              
        //        DedupList.Period_from = period_from;
        //        DedupList.Period_To = period_to;
        //        DedupList.Recon_gid = Recongid;
        //        DedupList.user_code = user_codes;
        //        string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(DedupList), "ReportKnocoffMIS");
        //        result2 = (DataSet)JsonConvert.DeserializeObject(post_data, result2.GetType());
        //        Table = result2.Tables[0].Copy();
        //        Table1 = result2.Tables[1].Copy();
        //        dts.Columns.Add("Rows Labels");
        //        dts.Columns.Add("credit Count", typeof(decimal));
        //        dts.Columns.Add("Debit Count", typeof(decimal));
        //        dts.Columns.Add("creditAmount", typeof(decimal));
        //        dts.Columns.Add("DebitAmount", typeof(decimal));
        //        dts.Columns.Add("Total Count of Narration", typeof(decimal));
        //        dts.Columns.Add("Total Sum of Amount", typeof(decimal));
        //        dts.Columns.Add("color", typeof(string));

        //        dr = dts.NewRow();
        //        dr[0] = "Manual KO";
        //        dr[7] = "Blue";
        //        dts.Rows.Add(dr);
        //        for(int i=0; i < result2.Tables[0].Rows.Count; i++)
        //        {
        //            DedupList = new TransactionRpt_model();
                    

        //            dr = dts.NewRow();
        //            string rulename = Table.Rows[i]["rule_name"].ToString();
        //            dr[0] = "    " + rulename;
        //            dr[1] = Math.Round(Convert.ToDecimal(Table.Rows[i]["dr_count"].ToString()),2);
        //            dr[2] = Math.Round(Convert.ToDecimal(Table.Rows[i]["cr_count"].ToString()),2);
        //            //dr[3] = Math.Round(Convert.ToDecimal(Table.Rows[i]["dr_amount"].ToString()),2);
        //            //dr[4] = Math.Round(Convert.ToDecimal(Table.Rows[i]["cr_amount"].ToString()),2);
        //            Decimal drcount = Math.Round(Convert.ToDecimal(Table.Rows[i]["dr_count"].ToString()),2);
        //            Decimal crcount = Math.Round(Convert.ToDecimal(Table.Rows[i]["cr_count"].ToString()),2);
        //            Decimal dramount = Math.Round(Convert.ToDecimal(Table.Rows[i]["dr_amount"].ToString()),2);
        //            Decimal cramount = Math.Round(Convert.ToDecimal(Table.Rows[i]["cr_amount"].ToString()),2);
        //            dr[3] = dramount.ToString("#.00");
        //            dr[4] = cramount.ToString("#.00");
        //            dr[5] = drcount + crcount;
        //            Decimal total = dramount + cramount;
        //            dr[6] = total.ToString("#.00");
        //            dr[7] = "Black";
        //            dts.Rows.Add(dr);


        //        }
        //        dr = dts.NewRow();
        //        dr[0] = "Rule Based Matched";
        //        dr[7] = "Blue";
        //        dts.Rows.Add(dr);
        //        for (int i = 0; i < result2.Tables[1].Rows.Count; i++)
        //        {
        //                dr = dts.NewRow();
        //                string rulename = Table1.Rows[i]["rule_name"].ToString();
        //                dr[0] = "    " + rulename;
        //                dr[1] =Math.Round(Convert.ToDecimal(Table1.Rows[i]["dr_count"].ToString()),2);
        //                dr[2] = Math.Round(Convert.ToDecimal(Table1.Rows[i]["cr_count"].ToString()),2);
        //                //dr[3] = Math.Round(Convert.ToDecimal(Table1.Rows[i]["dr_amount"].ToString()),2);
        //                //dr[4] = Math.Round(Convert.ToDecimal(Table1.Rows[i]["cr_amount"].ToString()),2);
        //                Decimal drcount = Math.Round(Convert.ToDecimal(Table1.Rows[i]["dr_count"].ToString()),2);
        //                Decimal crcount = Math.Round(Convert.ToDecimal(Table1.Rows[i]["cr_count"].ToString()),2);
        //                Decimal dramount = Math.Round(Convert.ToDecimal(Table1.Rows[i]["dr_amount"].ToString()),2);
        //                Decimal cramount = Math.Round(Convert.ToDecimal(Table1.Rows[i]["cr_amount"].ToString()),2);
        //                dr[3] = dramount.ToString("#.00");
        //                dr[4] = cramount.ToString("#.00");
        //                dr[5] = drcount + crcount;
        //                Decimal total = dramount + cramount;
        //                dr[6] = total.ToString("#.00");
        //                dr[7] = "Black";
        //                dts.Rows.Add(dr);
        //        }
        //        Decimal creditAmount = dts.AsEnumerable().Sum(row => row.Field<decimal?>("credit Count")??0);
        //        Decimal DebitAmount = dts.AsEnumerable().Sum(row => row.Field<decimal?>("Debit Count") ?? 0);
        //        Decimal credit_Amount = dts.AsEnumerable().Sum(row => row.Field<decimal?>("creditAmount") ?? 0);
        //        Decimal Debit_Amount = dts.AsEnumerable().Sum(row => row.Field<decimal?>("DebitAmount") ?? 0);
        //        Decimal TotalCountofNarration = dts.AsEnumerable().Sum(row => row.Field<decimal?>("Total Count of Narration") ?? 0);
        //        Decimal TotalSumofAmount = dts.AsEnumerable().Sum(row => row.Field<decimal?>("Total Sum of Amount") ?? 0);

        //        dr = dts.NewRow();
        //        dr[0] = "Grand Total";
        //        dr[1] = creditAmount;
        //        dr[2] = DebitAmount;
        //        dr[3] = credit_Amount;
        //        dr[4] = Debit_Amount;
        //        dr[5] = TotalCountofNarration;
        //        dr[6] = TotalSumofAmount;
        //        dr[7] = "Red";
        //        dts.Rows.Add(dr);

        //        for (int i = 0; i < dts.Rows.Count;i ++ )
        //        {
        //            TransactionRpt_model objcat3 = new TransactionRpt_model();
        //            objcat3.rowslabel = (dts.Rows[i]["Rows Labels"].ToString());
        //            objcat3.crcount = (dts.Rows[i]["credit Count"].ToString());
        //            objcat3.drcount = (dts.Rows[i]["Debit Count"].ToString());
        //            objcat3.cramount =(dts.Rows[i]["creditAmount"].ToString());
        //            objcat3.dramount = (dts.Rows[i]["DebitAmount"].ToString());
        //            objcat3.TotalcountofNarration =(dts.Rows[i]["Total Count of Narration"].ToString());
        //            objcat3.Totalsumofamount = (dts.Rows[i]["Total Sum of Amount"].ToString());
        //            objcat3.color = (dts.Rows[i]["color"].ToString());
        //            objcat_3st.Add(objcat3);
        //        }
        //        return Json(objcat_3st.ToDataSourceResult(request));
              
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(e.Message);
        //    }
        //    return View();
      //  //}
      //  public ActionResult KnockoffMIS_Download(string periodfrom,string periodto,string Recongid)
      //   {
      //      DataRow dr;
      //      List<TransactionRpt_model> objcat_3st = new List<TransactionRpt_model>();
      //      DataSet result2 = new DataSet();
      //      DataTable Table = new DataTable();
      //      DataTable Table1 = new DataTable();
      //      DataTable dts = new DataTable();
      //      TransactionRpt_model DedupList = new TransactionRpt_model();

      //      DedupList.Period_from = periodfrom;
      //      DedupList.Period_To = periodto;
      //      DedupList.Recon_gid = Recongid;
      //      DedupList.user_code = user_codes;
      //      string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(DedupList), "ReportKnocoffMIS");
      //      result2 = (DataSet)JsonConvert.DeserializeObject(post_data, result2.GetType());
      //      Table = result2.Tables[0].Copy();
      //      Table1 = result2.Tables[1].Copy();
      //      dts.Columns.Add("Rows Labels");
      //      dts.Columns.Add("Credit Count", typeof(decimal));
      //      dts.Columns.Add("Debit Count", typeof(decimal));
      //      dts.Columns.Add("CreditAmount", typeof(decimal));
      //      dts.Columns.Add("DebitAmount", typeof(decimal));
      //      dts.Columns.Add("Total Count of Narration", typeof(decimal));
      //      dts.Columns.Add("Total Sum of Amount", typeof(decimal));
      //      dr = dts.NewRow();
      //      dr[0] = "Manual KO";
      //      dts.Rows.Add(dr);
      //      for (int i = 0; i < result2.Tables[0].Rows.Count; i++)
      //      {
      //          DedupList = new TransactionRpt_model();
      //          dr = dts.NewRow();
      //         string rulename= Table.Rows[i]["rule_name"].ToString();
      //          dr[0] = "    "+rulename;
      //          dr[1] = Table.Rows[i]["dr_count"].ToString();
      //          dr[2] = Table.Rows[i]["cr_count"].ToString();
      //          Decimal drcount = Convert.ToDecimal(Table.Rows[i]["dr_count"].ToString());
      //          Decimal crcount = Convert.ToDecimal(Table.Rows[i]["cr_count"].ToString());
      //          Decimal dramount = Convert.ToDecimal(Table.Rows[i]["dr_amount"].ToString());
      //          Decimal cramount = Convert.ToDecimal(Table.Rows[i]["cr_amount"].ToString());
      //          dr[3] = dramount.ToString("#.00");
      //          dr[4] = cramount.ToString("#.00");
      //          dr[5] = drcount + crcount;
      //          Decimal total= dramount+cramount;
      //          dr[6] = total.ToString("#.00");
      //          dts.Rows.Add(dr);


      //      }
      //      dr = dts.NewRow();
      //      dr[0] = "Rule Based Matched";
      //      dts.Rows.Add(dr);
      //      for (int i = 0; i < result2.Tables[1].Rows.Count; i++)
      //      {
      //          dr = dts.NewRow();
      //          string rulename = Table1.Rows[i]["rule_name"].ToString();
      //          dr[0] = "    "+rulename;
      //          dr[1] = Table1.Rows[i]["dr_count"].ToString();
      //          dr[2] = Table1.Rows[i]["cr_count"].ToString();
      //          //dr[3] = Table1.Rows[i]["dr_amount"].ToString();
      //          //dr[4] = Table1.Rows[i]["cr_amount"].ToString();
      //          Decimal drcount = Convert.ToDecimal(Table1.Rows[i]["dr_count"].ToString());
      //          Decimal crcount = Convert.ToDecimal(Table1.Rows[i]["cr_count"].ToString());
      //          Decimal dramount = Convert.ToDecimal(Table1.Rows[i]["dr_amount"].ToString());
      //          Decimal cramount = Convert.ToDecimal(Table1.Rows[i]["cr_amount"].ToString());
      //          dr[3] = dramount.ToString("#.00");
      //          dr[4] = cramount.ToString("#.00");
      //          dr[5] = drcount + crcount;
      //          Decimal total = dramount + cramount;
      //          dr[6] = total.ToString("#.00");
      //          dts.Rows.Add(dr);
      //      }
      //      Decimal creditAmount = dts.AsEnumerable().Sum(row => row.Field<decimal?>("Credit Count") ?? 0);
      //      Decimal DebitAmount = dts.AsEnumerable().Sum(row => row.Field<decimal?>("Debit Count") ?? 0);
      //      Decimal credit_Amount = dts.AsEnumerable().Sum(row => row.Field<decimal?>("CreditAmount") ?? 0);
      //      Decimal Debit_Amount = dts.AsEnumerable().Sum(row => row.Field<decimal?>("DebitAmount") ?? 0);
      //      Decimal TotalCountofNarration = dts.AsEnumerable().Sum(row => row.Field<decimal?>("Total Count of Narration") ?? 0);
      //      Decimal TotalSumofAmount = dts.AsEnumerable().Sum(row => row.Field<decimal?>("Total Sum of Amount") ?? 0);

      //      dr = dts.NewRow();
      //      dr[0] = "Grand Total";
      //      dr[1] = creditAmount;
      //      dr[2] = DebitAmount;
      //      dr[3] = credit_Amount;
      //      dr[4] = Debit_Amount;
      //      dr[5] = TotalCountofNarration;
      //      dr[6] = TotalSumofAmount;
      //      dts.Rows.Add(dr);

      //      string fileName = "Sample.xlsx";
      //      using (XLWorkbook wb = new XLWorkbook())// wb.Worksheets.Add(dt);
      //      {
      //          var ws = wb.AddWorksheet("Sheet1");

      //          ws.Cell("A1").Style.Font.Bold = true; ws.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
      //          ws.Cell("B1").Style.Font.Bold = true; ws.Cell("B1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
      //          ws.Cell("C1").Style.Font.Bold = true; ws.Cell("C1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
      //          ws.Cell("D1").Style.Font.Bold = true; ws.Cell("D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
      //          ws.Cell("E1").Style.Font.Bold = true; ws.Cell("E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
      //          ws.Cell("F1").Style.Font.Bold = true; ws.Cell("F1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
      //          ws.Cell("G1").Style.Font.Bold = true; ws.Cell("G1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
      //          ws.Cell("A2").Style.Font.Bold = true;
      //          wb.Worksheet(1).Cell("A1").InsertTable(dts);
      //          //ws.Table(0).ShowAutoFilter = false;// Disable AutoFilter.
      //          //ws.Table(0).Theme = XLTableTheme.TableStyleMedium10;


            
               
      //          int Table1Position = 2 + Table.Rows.Count+1;
      //          string count = "A" + Table1Position + "";   
      //          ws.Cell(count).Style.Font.Bold = true;//rule

      //          int Table2Position = Table1Position+Table1.Rows.Count+1;
      //          string count1 = "A" + Table2Position + ""; string total = "B" + Table2Position + ""; string total1 = "C" + Table2Position + "";
      //          string total2 = "D" + Table2Position + ""; string total3 = "E" + Table2Position + ""; string total4 = "F" + Table2Position + "";
      //          string total5 = "G" + Table2Position + "";
      //          ws.Cell(count1).Style.Font.Bold = true; ws.Cell(total5).Style.Font.Bold = true; ws.Cell(total5).Style.Fill.BackgroundColor = XLColor.FromHtml("#E8E8E8");
      //          ws.Cell(total).Style.Font.Bold = true; ws.Cell(total).Style.Fill.BackgroundColor = XLColor.FromHtml("#E8E8E8");
      //          ws.Cell(total1).Style.Font.Bold = true; ws.Cell(total1).Style.Fill.BackgroundColor = XLColor.FromHtml("#E8E8E8");
      //          ws.Cell(total2).Style.Font.Bold = true; ws.Cell(total2).Style.Fill.BackgroundColor = XLColor.FromHtml("#E8E8E8");
      //          ws.Cell(total3).Style.Font.Bold = true; ws.Cell(total3).Style.Fill.BackgroundColor = XLColor.FromHtml("#E8E8E8");
      //          ws.Cell(total4).Style.Font.Bold = true; ws.Cell(total4).Style.Fill.BackgroundColor = XLColor.FromHtml("#E8E8E8");
      //          ws.Cell(count1).Style.Fill.BackgroundColor = XLColor.FromHtml("#E8E8E8");
      //          var co = 4; var col1 = 5; var col3 = 7;
      //          //var ro =Table2Position;
      //          for (int i = 1; i <= Table2Position;i++)
      //          {
      //              ws.Cell(i, co).Style.NumberFormat.Format = "##0.00";
      //          }

      //          for (int i = 1; i <= Table2Position; i++)
      //          {
      //              ws.Cell(i, col1).Style.NumberFormat.Format = "##0.00";
      //          }
              
      //          for (int i = 1; i <= Table2Position; i++)
      //          {
      //              ws.Cell(i, col3).Style.NumberFormat.Format = "##0.00";
      //          }

      //          using (MemoryStream stream = new MemoryStream())
      //          {
      //              wb.SaveAs(stream);
      //              return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
      //          }

      //      }

      //      return View();
      //}
    
        public ActionResult MonthBRS_Download(string TranDate, int Recon_gid)
        {

            try
            {
                List<TransactionRpt_model> objcat_3st = new List<TransactionRpt_model>();
                DataSet result2 = new DataSet();
                DataSet dt = new DataSet();
                DataTable Table = new DataTable();
                DataTable Table1 = new DataTable();
                DataTable Table2 = new DataTable();
                DataTable Table3 = new DataTable();
                DataTable Table4 = new DataTable();
                DataTable Table6 = new DataTable();
                DataTable Table7 = new DataTable();

                long row = 0;
                long col = 0;
                string cellTxt = "";
                string cellTxt1 = "";
                string formulaTxt = "";
                string calcCellTxt = "";
                var recon_name1 = "";

                TransactionRpt_model DedupList = new TransactionRpt_model();
                DedupList.Trandate = TranDate;
                DedupList.Recongid = Recon_gid;
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(DedupList), "ReportMonthbrs_Download");
                result2 = (DataSet)JsonConvert.DeserializeObject(post_data, result2.GetType());

                TransactionRpt_model objcat3 = new TransactionRpt_model();
                Table = result2.Tables[0].Copy();
                Table1 = result2.Tables[1].Copy();
                Table2 = result2.Tables[2].Copy();
                Table3 = result2.Tables[3].Copy();
                Table4 = result2.Tables[4].Copy();
                Table6 = result2.Tables[6].Copy();
                Table7 = result2.Tables[7].Copy();

                var entity_name = Table.Rows[0]["entity"].ToString();
                var recon_name = Table.Rows[0]["recon_name"].ToString();
                int i = recon_name.Length;

                if (i >= 30)
                {
                    recon_name1 = recon_name.Substring(0, 30);

                }
                else
                {
                    recon_name1 = recon_name;
                }

                var acc_no1 = Table.Rows[0]["acc_no1"].ToString();
                //  string s2 = Regex.Replace(recon_name, @"[^A-Z]+", String.Empty);
                var acc_no2 = Table.Rows[0]["acc_no2"].ToString();
                string Acc1date = Table.Rows[0]["acc_no1_bal_date"].ToString();
                string Acc2date = Table.Rows[0]["acc_no2_bal_date"].ToString();
                Decimal acc1Bal_amount = Convert.ToDecimal(Table.Rows[0]["acc_no1_bal_amount"].ToString());
                Decimal acc2Bal_amount = Convert.ToDecimal(Table.Rows[0]["acc_no2_bal_amount"].ToString());
                Decimal accno1_drtotal = Convert.ToDecimal(Table.Rows[0]["acc_no1_dr_total"].ToString());
                Decimal accno2_drtotal = Convert.ToDecimal(Table.Rows[0]["acc_no2_dr_total"].ToString());
                Decimal accno1_crtotal = Convert.ToDecimal(Table.Rows[0]["acc_no1_cr_total"].ToString());
                Decimal accno2_crtotal = Convert.ToDecimal(Table.Rows[0]["acc_no2_cr_total"].ToString());

                dt = result2.Copy();
                dt.Tables.Remove("Table");

                string fileName = acc_no1 + ".xlsx";

                using (XLWorkbook wb = new XLWorkbook())// wb.Worksheets.Add(dt);
                {
                    ///Setting Table Data static & Dynamic;

                    var ws = wb.AddWorksheet(recon_name1);

                    ws.FirstCell().SetValue(entity_name); ws.Cell("A1").Style.Font.Bold = true;
                    ws.Cell("A1").Style.Font.Underline.ToString(); ws.Cell("A1").Style.Font.Underline = XLFontUnderlineValues.Single;
                    ws.Range("A1:D1").Row(1).Merge();

                    ws.Cell("E1").SetValue("GL CODE").SetActive(); ws.Cell("E1").Style.Font.Bold = true;
                    ws.Cell("F1").SetValue(acc_no2).SetActive(); ws.Cell("F1").Style.Font.Bold = true;

                    ws.FirstCell().CellBelow().SetValue(recon_name);
                    ws.Cell("A2").Style.Font.Bold = true;
                    ws.Range("A2:F2").Row(1).Merge();

                    ws.Cell("A3").SetValue("CC A/c No.").SetActive(); ws.Cell("A3").Style.Font.Bold = true;
                    ws.Range("A3:B3").Row(1).Merge();

                    ws.Cell("C3").SetValue(acc_no1).SetActive(); ws.Cell("C3").Style.Font.Bold = true;
                    ws.Range("C3:F3").Row(1).Merge();

                    ws.Cell("A4").SetValue("Bank Reconciliation Statement as on date " + Acc1date + "").SetActive();
                    ws.Cell("A4").Style.Font.Bold = true;
                    ws.Range("A4:F4").Row(1).Merge();

                    ws.Cell("A6").SetValue("S U M M A R Y").SetActive(); ws.Cell("A6").Style.Font.Bold = true;
                    ws.Cell("A6").Style.Font.Underline = XLFontUnderlineValues.Single;
                    ws.Cell("A6").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Range("A6:F6").Row(1).Merge();

                    ws.Cell("A7").SetValue("Closing balance  as per our bank book as on " + Acc2date + "").SetActive();
                    ws.Cell("A7").Style.Font.Bold = true;
                    ws.Range("A7:E7").Row(1).Merge();
                    ws.Cell("I7").SetValue(acc2Bal_amount).SetActive(); ws.Cell("I7").Style.NumberFormat.Format = "###0.00";

                    ws.Cell("A9").SetValue("Less: Cheques deposited but not credited in bank passbook").SetActive();
                    ws.Range("A9:E9").Row(1).Merge();
                    ws.Cell("I9").SetValue(accno2_drtotal).SetActive(); ws.Cell("FI9").Style.Font.Bold = true; ws.Cell("I9").Style.NumberFormat.Format = "###0.00";

                    ws.Cell("A11").SetValue("Add: Cheques issued but not presented in the bank").SetActive();
                    ws.Range("A11:E11").Row(1).Merge();
                    ws.Cell("I11").SetValue(accno2_crtotal).SetActive(); ws.Cell("I11").Style.NumberFormat.Format = "###0.00";

                    ws.Cell("A13").SetValue("Less: Amount debited in bank but not accounted").SetActive();
                    ws.Range("A13:E13").Row(1).Merge();
                    ws.Cell("I13").SetValue(accno1_drtotal).SetActive(); ws.Cell("I13").Style.NumberFormat.Format = "###0.00";

                    ws.Cell("A15").SetValue("Add: Amount credited in bank but not accounted").SetActive();
                    ws.Range("A15:E15").Row(1).Merge();
                    ws.Cell("I15").SetValue(accno1_crtotal).SetActive(); ws.Cell("I15").Style.NumberFormat.Format = "###0.00";

                    ws.Cell("A17").SetValue("Closing balance as per bank passbook").SetActive();
                    ws.Cell("A17").Style.Font.Bold = true;
                    ws.Range("A17:E17").Row(1).Merge();

                    ws.Cell("I17").SetValue(acc1Bal_amount).SetActive(); ws.Cell("I17").Style.Font.Bold = true; ws.Cell("I17").Style.NumberFormat.Format = "###0.00";
                    ws.Cell("I17").Style.Font.Bold = true; //ws.Cell("F17").Style.Font.Underline = XLFontUnderlineValues.Double;
                    ws.Cell("I17").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    ws.Cell("I17").Style.Border.TopBorderColor = XLColor.Black;
                    ws.Cell("I17").Style.Border.BottomBorder = XLBorderStyleValues.Double;
                    ws.Cell("I17").Style.Border.BottomBorderColor = XLColor.Black;

                    ws.Cell("A19").SetValue("Less: Cheques deposited but not credited").SetActive(); ws.Cell("A19").Style.Font.Bold = true;
                    ws.Cell("A19").Style.Font.Underline = XLFontUnderlineValues.Single;
                    ws.Range("A19:F19").Row(1).Merge();

                    //Inserting  Table1 Data
                    wb.Worksheet(1).Cell("A20").InsertTable(Table1);
                    ws.Table(0).ShowAutoFilter = false;// Disable AutoFilter.
                    ws.Table(0).Theme = XLTableTheme.None;
                    ws.Range("A20:H20").Style.Font.Bold = true;
                    ws.Range("A20:H20").Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);


                    //Inserting  Table1 Total
                    formulaTxt = "=sum(I21:I" + (20 + Table1.Rows.Count).ToString() + ")";
                    row = 20 + Table1.Rows.Count + 1;
                    cellTxt = "I" + row.ToString() + "";
                    ws.Cell(cellTxt).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    ws.Cell(cellTxt).Style.Border.BottomBorderColor = XLColor.Black;

                    var cellFormulaTableSum = ws.Cell(cellTxt);

                    cellFormulaTableSum.FormulaA1 = formulaTxt; cellFormulaTableSum.Style.NumberFormat.Format = "###0.00";

                    //Inserting calculation
                    calcCellTxt = "I7";
                    formulaTxt = "=+(" + calcCellTxt + "*1)-" + cellTxt;

                    row = row + 1;
                    cellTxt = "I" + row.ToString() + "";
                    calcCellTxt = cellTxt;

                    var cellFormulaCalc = ws.Cell(cellTxt);
                    cellFormulaCalc.FormulaA1 = formulaTxt; cellFormulaCalc.Style.NumberFormat.Format = "###0.00";

                    //Inserting  Table2 Data
                    row = row + 2;
                    cellTxt = "A" + row.ToString() + "";

                    ws.Cell(cellTxt).SetValue("Add: Cheques issued but not presented").SetActive(); ws.Cell(cellTxt).Style.Font.Bold = true;
                    ws.Cell(cellTxt).Style.Font.Underline = XLFontUnderlineValues.Single;
                    cellTxt = "A" + row.ToString() + ":F" + row.ToString();
                    ws.Range(cellTxt).Row(1).Merge();

                    row = row + 1;
                    cellTxt = "A" + row.ToString() + "";

                    wb.Worksheet(1).Cell(cellTxt).InsertTable(Table2);
                    ws.Table(1).ShowAutoFilter = false;// Disable AutoFilter.
                    ws.Table(1).Theme = XLTableTheme.None;
                    cellTxt1 = "A" + row.ToString() + ":H" + row.ToString();
                    ws.Range(cellTxt1).Style.Font.Bold = true;
                    ws.Range(cellTxt1).Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);


                    //Inserting  Table2 Total
                    formulaTxt = "=sum(I" + (row + 1).ToString() + ":I" + (row + Table2.Rows.Count).ToString() + ")";
                    row = row + Table2.Rows.Count + 1;
                    cellTxt = "I" + row.ToString() + "";
                    ws.Cell(cellTxt).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    ws.Cell(cellTxt).Style.Border.BottomBorderColor = XLColor.Black;
                    cellFormulaTableSum = ws.Cell(cellTxt);
                    cellFormulaTableSum.FormulaA1 = formulaTxt; cellFormulaTableSum.Style.NumberFormat.Format = "###0.00";

                    formulaTxt = "=+" + calcCellTxt + "+" + cellTxt;

                    row = row + 1;
                    cellTxt = "I" + row.ToString() + "";
                    calcCellTxt = cellTxt;

                    cellFormulaCalc = ws.Cell(cellTxt);
                    cellFormulaCalc.FormulaA1 = formulaTxt; cellFormulaCalc.Style.NumberFormat.Format = "###0.00";

                    row = row + 2;
                    cellTxt = "A" + row.ToString() + "";

                    ws.Cell(cellTxt).SetValue("Less: Amount  debited  in pass Book").SetActive(); ws.Cell(cellTxt).Style.Font.Bold = true;
                    ws.Cell(cellTxt).Style.Font.Underline = XLFontUnderlineValues.Single;
                    cellTxt = "A" + row.ToString() + ":F" + row.ToString();
                    ws.Range(cellTxt).Row(1).Merge();

                    //Inserting  Table3 Data
                    row = row + 1;
                    cellTxt = "A" + row.ToString() + "";

                    wb.Worksheet(1).Cell(cellTxt).InsertTable(Table3);
                    ws.Table(2).ShowAutoFilter = false;// Disable AutoFilter.
                    ws.Table(2).Theme = XLTableTheme.None;
                    cellTxt1 = "A" + row.ToString() + ":H" + row.ToString();
                    ws.Range(cellTxt1).Style.Font.Bold = true;
                    ws.Range(cellTxt1).Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);

                    //Inserting  Table3 Total
                    formulaTxt = "=sum(I" + (row + 1).ToString() + ":I" + (row + Table3.Rows.Count).ToString() + ")";
                    row = row + Table3.Rows.Count + 1;
                    cellTxt = "I" + row.ToString() + "";
                    ws.Cell(cellTxt).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    ws.Cell(cellTxt).Style.Border.BottomBorderColor = XLColor.Black;
                    cellFormulaTableSum = ws.Cell(cellTxt);
                    cellFormulaTableSum.FormulaA1 = formulaTxt; cellFormulaTableSum.Style.NumberFormat.Format = "###0.00";

                    formulaTxt = "=+" + calcCellTxt + "-" + cellTxt;

                    row = row + 1;
                    cellTxt = "I" + row.ToString() + "";
                    calcCellTxt = cellTxt;

                    cellFormulaCalc = ws.Cell(cellTxt);
                    cellFormulaCalc.FormulaA1 = formulaTxt; cellFormulaCalc.Style.NumberFormat.Format = "###0.00";

                    row = row + 2;
                    cellTxt = "A" + row.ToString() + "";

                    ws.Cell(cellTxt).SetValue("Add: Amount credited in pass Book").SetActive(); ws.Cell(cellTxt).Style.Font.Bold = true;
                    ws.Cell(cellTxt).Style.Font.Underline = XLFontUnderlineValues.Single;

                    cellTxt = "A" + row.ToString() + ":F" + row.ToString();
                    ws.Range(cellTxt).Row(1).Merge();

                    row = row + 1;
                    cellTxt = "A" + row.ToString() + "";

                    wb.Worksheet(1).Cell(cellTxt).InsertTable(Table4);
                    ws.Table(3).ShowAutoFilter = false;// Disable AutoFilter.
                    ws.Table(3).Theme = XLTableTheme.None;
                    cellTxt1 = "A" + row.ToString() + ":H" + row.ToString();
                    ws.Range(cellTxt1).Style.Font.Bold = true;
                    ws.Range(cellTxt1).Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);

                    //Inserting  Table4 Total
                    formulaTxt = "=sum(I" + (row + 1).ToString() + ":I" + (row + Table4.Rows.Count).ToString() + ")";
                    row = row + Table4.Rows.Count + 1;
                    cellTxt = "I" + row.ToString() + "";
                    ws.Cell(cellTxt).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    ws.Cell(cellTxt).Style.Border.BottomBorderColor = XLColor.Black;
                    cellFormulaTableSum = ws.Cell(cellTxt);
                    cellFormulaTableSum.FormulaA1 = formulaTxt; cellFormulaTableSum.Style.NumberFormat.Format = "###0.00";

                    formulaTxt = "=+" + calcCellTxt + "+" + cellTxt;

                    row = row + 1;
                    cellTxt = "I" + row.ToString() + "";
                    calcCellTxt = cellTxt;

                    cellFormulaCalc = ws.Cell(cellTxt);
                    cellFormulaCalc.FormulaA1 = formulaTxt; cellFormulaCalc.Style.NumberFormat.Format = "###0.00";
                    cellFormulaCalc.Style.Font.Bold = true;
                    // cellFormulaCalc.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    // cellFormulaCalc.Style.Border.TopBorderColor = XLColor.Black;
                    //cellFormulaCalc.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    //cellFormulaCalc.Style.Border.BottomBorderColor = XLColor.Black;
                    //cellFormulaCalc.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                    //Total Balance & Difference
                    cellTxt = "B" + row.ToString() + "";
                    ws.Cell(cellTxt).SetValue("Balance as per bank pass book").SetActive(); ws.Cell(cellTxt).Style.Font.Bold = true;

                    row = row + 2;
                    cellTxt = "B" + row.ToString() + "";
                    ws.Cell(cellTxt).SetValue("Difference").SetActive(); ws.Cell(cellTxt).Style.Font.Bold = true;

                    formulaTxt = "=+" + calcCellTxt + "-I17";

                    cellTxt = "I" + row.ToString() + "";

                    cellFormulaCalc = ws.Cell(cellTxt);
                    cellFormulaCalc.FormulaA1 = formulaTxt; cellFormulaCalc.Style.NumberFormat.Format = "###0.00";
                    cellFormulaCalc.Style.Font.Bold = true;
                    cellFormulaCalc.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    cellFormulaCalc.Style.Border.TopBorderColor = XLColor.Black;
                    cellFormulaCalc.Style.Border.BottomBorder = XLBorderStyleValues.Double;
                    cellFormulaCalc.Style.Border.BottomBorderColor = XLColor.Black;
                    cellFormulaCalc.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                    ws.Columns("A").AdjustToContents();
                    ws.Columns("F").AdjustToContents();

                    //Insert Transaction
                    ws = wb.AddWorksheet("Transaction");
                    ws.Cell("A1").InsertTable(Table6).Theme = XLTableTheme.None;
                    ws.Tables.ForEach(x => x.ShowAutoFilter = false);
                    ws.Cell("A1").Style.Font.Bold = true;
                    ws.Range("A1:Z1").Style.Font.Bold = true;
                    ws.Range("A1:Z1").Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);
                    // ws.Table(6).Theme = XLTableTheme.None;

                    if (Table7.Rows.Count > 0)
                    {
                        //Insert Supporting File
                        ws = wb.AddWorksheet("SupportingFile");
                        ws.Cell("A1").InsertTable(Table7).Theme = XLTableTheme.None;
                        ws.Tables.ForEach(x => x.ShowAutoFilter = false);
                        ws.Range("A1:Z1").Style.Font.Bold = true;
                        ws.Range("A1:Z1").Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);
                        // ws.Table(7).Theme = XLTableTheme.None;
                    }

                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                    }

                }

            }
            catch (Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
            return View();

        }
        public ActionResult kendogrid_download(string TranDate, int recon_gid, string recon_name)
        {
            try
            {
                List<TransactionRpt_model> objcat_3st = new List<TransactionRpt_model>();
                string recon_name1 = "";
                DataSet result_data = new DataSet();
                string filename = "Recon-between A/cs.xlsx";
                DataSet dt = new DataSet();
                DataTable Table1 = new DataTable();



                //getting recon summary table
                Report_model report = new Report_model();
                report.tran_date = TranDate;
                report.recon_gid= recon_gid;
                DataTable result = new DataTable();
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(report), "Reportbetween_download_1");
                result_data = (DataSet)JsonConvert.DeserializeObject(post_data, result_data.GetType());
                string Date = result_data.Tables[0].Rows[0]["tran_date"].ToString();
                string entity_name = result_data.Tables[0].Rows[0]["entity_name"].ToString();
                string name = entity_name.Trim('\'');
                string Glcode = result_data.Tables[0].Rows[0]["glcode"].ToString();

                Table1 = result_data.Tables[1].Copy();
                Table1.Columns.RemoveAt(0);
                Table1.Columns[0].ColumnName = "Particulars";
                Table1.Columns[1].ColumnName = "Amount";
                Table1.Columns[2].ColumnName = "Account Mode";
                Table1.Columns[3].ColumnName = "Balance Amount";

                int i = recon_name.Length;

                if (i >= 30)
                {
                    recon_name1 = recon_name.Substring(0, 30);
                    recon_name1 = Regex.Replace(recon_name1, @"[^0-9a-zA-Z\._]", string.Empty);

                }
                else
                {
                    recon_name1 = recon_name;
                    recon_name1 = Regex.Replace(recon_name1, @"[^0-9a-zA-Z\._]", string.Empty);
                }

                using (XLWorkbook wb = new XLWorkbook())
                {

                    var ws = wb.AddWorksheet(recon_name1);

                    //Insrting Table1 Data
                    ws.Cell("A1").SetValue(name); ws.Cell("A1").Style.Font.Bold = true;
                    ws.Cell("A7").Style.Font.Bold = true;
                    ws.Cell("A1").Style.Font.Underline = XLFontUnderlineValues.Single;
                    ws.Cell("A2").SetValue(recon_name); ws.Cell("A2").Style.Font.Bold = true;
                    ws.Cell("B2").SetValue("GL CODE"); ws.Cell("B2").Style.Font.Bold = true;
                    ws.Cell("C2").SetValue(Glcode); ws.Cell("C2").Style.Font.Bold = true;
                    ws.Cell("A4").InsertTable(Table1);
                    wb.Worksheet(1).Table(0).ShowAutoFilter = false;// Disable AutoFilter.
                    wb.Worksheet(1).Table(0).Theme = XLTableTheme.None;
                    wb.Worksheet(1).Table(0).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    wb.Worksheet(1).Table(0).Style.Border.BottomBorderColor = XLColor.Black;
                    wb.Worksheet(1).Table(0).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    wb.Worksheet(1).Table(0).Style.Border.TopBorderColor = XLColor.Black;
                    wb.Worksheet(1).Table(0).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    wb.Worksheet(1).Table(0).Style.Border.LeftBorderColor = XLColor.Black;
                    wb.Worksheet(1).Table(0).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    wb.Worksheet(1).Table(0).Style.Border.RightBorderColor = XLColor.Black;
                    ws.Range("A4:D4").Style.Font.Bold = true;
                    ws.Range("A4:D4").Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);

                    ws.Range("A17:D17").Style.Font.Bold = true;
                    ws.Range("A17:D17").Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);
                    ws.Cell("A11").Style.Font.Bold = true; ws.Cell("A13").Style.Font.Bold = true;
                    ws.Range("A19:D19").Style.Font.Bold = true;
                    ws.Range("A20:D20").Style.Font.Bold = true;
                    ws.Range("A21:D21").Style.Font.Bold = true;

                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
                    }
                }

            }
            catch (Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

            return View();
        }
        public ActionResult Reconpercentage(string TranDate, string recon_gid)
        {
            try
            {
                List<TransactionRpt_model> objcat_3st = new List<TransactionRpt_model>();
                DataSet result2 = new DataSet();
                DataSet result_data = new DataSet();
                string fileName = "";
                DataSet dt = new DataSet();
                DataTable reconsummary = new DataTable();
                DataTable finyear1 = new DataTable();
                DataTable Table = new DataTable();
                DataTable Table1 = new DataTable();
                DataTable Table2 = new DataTable();
                DataTable Table3 = new DataTable();
                DataTable Table4 = new DataTable();
                DataTable Table6 = new DataTable();
                DataTable Table7 = new DataTable();

                long row = 0;
                long col = 0;
                int worksheetcount = 1;
                string cellTxt = "";
                string cellTxt1 = "";
                string filename_tran = "";
                string formulaTxt = "";
                string calcCellTxt = "";
                var recon_name1 = "";

                List<int> recons = recon_gid.Split(',').Select(int.Parse).ToList();
                for (int i = 0; i < recons.Count; i++)
                {
                    if (recons[i] == 0)
                    {
                        recons.RemoveAt(i);
                        i--;
                    }
                }

                int no_of_recons = recons.Count;

                //getting recon summary table
                TransactionRpt_model DedupList = new TransactionRpt_model();
                DedupList.Trandate = TranDate;
                DedupList.Recon_gid = recon_gid;
                DedupList.no_of_recons = no_of_recons;
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(DedupList), "Reportpercentage_overall");
                result_data = (DataSet)JsonConvert.DeserializeObject(post_data, result2.GetType());
                Table1=result_data.Tables[1].Copy();
                Table2 = result_data.Tables[0].Copy();
                string Date = Table2.Rows[0]["Date1"].ToString();
                string entity_name = Table2.Rows[0]["v_entity_name"].ToString();
                string name = entity_name.Trim('\'');

                using (XLWorkbook wb = new XLWorkbook())
                {
                    
                    var ws = wb.AddWorksheet(Date);

                   //Insrting Table1 Data
                    ws.Cell("A1").SetValue(name); ws.Cell("A1").Style.Font.Bold = true;
                    ws.Cell("A3").InsertTable(Table1);
                    wb.Worksheet(1).Table(0).ShowAutoFilter = false;// Disable AutoFilter.
                    wb.Worksheet(1).Table(0).Theme = XLTableTheme.None;
                    wb.Worksheet(1).Table(0).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    wb.Worksheet(1).Table(0).Style.Border.BottomBorderColor = XLColor.Black;
                    wb.Worksheet(1).Table(0).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    wb.Worksheet(1).Table(0).Style.Border.TopBorderColor = XLColor.Black;
                    wb.Worksheet(1).Table(0).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    wb.Worksheet(1).Table(0).Style.Border.LeftBorderColor = XLColor.Black;
                    wb.Worksheet(1).Table(0).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    wb.Worksheet(1).Table(0).Style.Border.RightBorderColor = XLColor.Black;
                    ws.Range("A3:J3").Style.Font.Bold = true;
                    ws.Range("A3:J3").Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);
                   
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                    }
                }

            }
            catch (Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

            return View();
        }

        public ActionResult Monthendreportdownload(string TranDate, string recon_gid, string Filetime)
        {

            try
            {
                List<TransactionRpt_model> objcat_3st = new List<TransactionRpt_model>();
                DataSet result2 = new DataSet();
                DataSet result_data = new DataSet();
                string fileName = "";
                // int recon_gid = 0;
                XLWorkbook wb = new XLWorkbook();
                // var ws = "";
                DataSet dt = new DataSet();
                DataTable reconsummary = new DataTable();
                DataTable finyear1 = new DataTable();
                DataTable Table = new DataTable();
                DataTable Table1 = new DataTable();
                DataTable Table2 = new DataTable();
                DataTable Table3 = new DataTable();
                DataTable Table4 = new DataTable();
                DataTable Table6 = new DataTable();
                DataTable Table7 = new DataTable();
                string fin_year = "";
                string show_fin_year = "";
                long row = 0;
                long col = 0;
                int worksheetcount = 1;
                string cellTxt = "";
                string cellTxt1 = "";
                string filename_tran = "";
                string formulaTxt = "";
                string calcCellTxt = "";
                var recon_name1 = "";

                List<int> recons = recon_gid.Split(',').Select(int.Parse).ToList();
                for (int i = 0; i < recons.Count; i++)
                {
                    if (recons[i] == 0)
                    {
                        recons.RemoveAt(i);
                        i--;
                    }
                }

                int no_of_recons = recons.Count;

                //getting recon summary table
                TransactionRpt_model DedupList = new TransactionRpt_model();
                DedupList.Trandate = TranDate;
                DedupList.Recon_gid = recon_gid;
                DedupList.no_of_recons = no_of_recons;
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(DedupList), "ReportMonthbrs_Download_summary");
                result_data = (DataSet)JsonConvert.DeserializeObject(post_data, result2.GetType());
                reconsummary = result_data.Tables[1].Copy();
                finyear1 = result_data.Tables[0].Copy();

                int recon_summary = 1;
                string Month = DateTime.ParseExact(TranDate, "yyyy-MM-dd", null).ToString("MMMyyyy");
                string filetime = DateTime.ParseExact(Filetime, "yyyy-MM-dd HH:mm:ss", null).ToString("dd_MMM_yyy HH_mm_ss");

                for (int j = 0; j < recons.Count; j++)
                {
                    worksheetcount = worksheetcount + 1;
                    int Reconid = recons[j];
                    TransactionRpt_model DedupList1 = new TransactionRpt_model();
                    DedupList.Trandate = TranDate;
                    DedupList.Recongid = Reconid;
                    string post_data1 = objcommon.getApiResult(JsonConvert.SerializeObject(DedupList), "ReportMonthbrs_Download");
                    result2 = (DataSet)JsonConvert.DeserializeObject(post_data1, result2.GetType());




                    TransactionRpt_model objcat3 = new TransactionRpt_model();
                    Table = result2.Tables[0].Copy();
                    Table1 = result2.Tables[1].Copy();
                    Table1.Columns["Column1"].ColumnName = "-";
                    Table2 = result2.Tables[2].Copy();
                    Table2.Columns["Column1"].ColumnName = "-";
                    Table3 = result2.Tables[3].Copy();
                    Table3.Columns["Column1"].ColumnName = "-";
                    Table4 = result2.Tables[4].Copy();
                    Table4.Columns["Column1"].ColumnName = "-";
                    Table6 = result2.Tables[6].Copy();
                    Table7 = result2.Tables[7].Copy();

                    var entity_name = Table.Rows[0]["entity"].ToString();
                    var recon_name = Table.Rows[0]["recon_name"].ToString();
                    int i = recon_name.Length;

                    if (i >= 30)
                    {
                        recon_name1 = recon_name.Substring(0, 30);
                        recon_name1 = Regex.Replace(recon_name1, @"[^0-9a-zA-Z\._]", string.Empty);
                        

                    }
                    else
                    {
                        recon_name1 = recon_name;
                        recon_name1 = Regex.Replace(recon_name1, @"[^0-9a-zA-Z\._]", string.Empty);
                    }

                    filename_tran = recon_name1.Substring(0, 5);

                    var acc_no1 = Table.Rows[0]["acc_no1"].ToString();
                    string Acc2date = "";
                    string Acc1date = "";
                    //  string s2 = Regex.Replace(recon_name, @"[^A-Z]+", String.Empty);
                    var acc_no2 = Table.Rows[0]["acc_no2"].ToString();
                    //string Acc1date = Table.Rows[0]["acc_no1_bal_date"].ToString();
                    //string Acc2date = Table.Rows[0]["acc_no2_bal_date"].ToString();
                    Decimal acc1Bal_amount = Convert.ToDecimal(Table.Rows[0]["acc_no1_bal_amount"].ToString());
                    Decimal acc2Bal_amount = Convert.ToDecimal(Table.Rows[0]["acc_no2_bal_amount"].ToString());
                    Decimal accno1_drtotal = Convert.ToDecimal(Table.Rows[0]["acc_no1_dr_total"].ToString());
                    Decimal accno2_drtotal = Convert.ToDecimal(Table.Rows[0]["acc_no2_dr_total"].ToString());
                    Decimal accno1_crtotal = Convert.ToDecimal(Table.Rows[0]["acc_no1_cr_total"].ToString());
                    Decimal accno2_crtotal = Convert.ToDecimal(Table.Rows[0]["acc_no2_cr_total"].ToString());
                    if (accno2_crtotal == 0 && accno1_crtotal == 0 && accno2_drtotal == 0 && accno1_drtotal == 0 && acc1Bal_amount == 0 && acc2Bal_amount == 0)
                    {
                        int year = DateTime.Now.Year;
                        int month = DateTime.Now.Month;
                        DateTime lastDayOfMonth = new DateTime(year, month, DateTime.DaysInMonth(year, month));
                        Acc2date = lastDayOfMonth.ToString("dd-MM-yyyy");
                        Acc1date = lastDayOfMonth.ToString("dd-MM-yyyy");

                    }
                    else
                    {
                        Acc2date = Table.Rows[0]["acc_no2_bal_date"].ToString();
                        Acc1date = Table.Rows[0]["acc_no1_bal_date"].ToString();
                    }
                    var val = Convert.ToDateTime(TranDate);

                    int pMonth;
                    int pYear = val.Year;
                    int checkpMonth = val.Month - 1;
                    if (checkpMonth == 0) {
                       // pMonth = val.Month;
                        pMonth = 12;
                        pYear = val.Year - 1;
                    } else {
                        pMonth = checkpMonth;
                        pYear = val.Year;
                    }
                    DateTime lastDayPrevMonth = new DateTime(pYear, pMonth, DateTime.DaysInMonth(pYear, pMonth));

                    var lastday = lastDayPrevMonth.ToString("dd-MM-yyyy");

                    dt = result2.Copy();
                    dt.Tables.Remove("Table");

                    //fileName = acc_no1 + ".xlsx";
                    fileName = "AllReconsMonth_Brs_Summary "+ filetime +".xlsx";

                    using (wb)// wb.Worksheets.Add(dt);
                    {
                        ///Setting Table Data static & Dynamic;
                        var ws = wb.AddWorksheet("todelete");
                        if (ws.Name == "todelete")
                        {
                            ws.Delete();
                        }
                        if (recon_summary == 1)
                        {
                            ///Inserting reconsummary table
                            ws = wb.AddWorksheet("Reconsummary");
                            recon_summary = 0;

                            string name = entity_name.Trim('\'');
                            ws.Cell("F1").SetValue(name); ws.Cell("F1").Style.Font.Bold = true;
                            ws.Cell("F1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell("F1").Style.Font.Underline.ToString(); ws.Cell("F1").Style.Font.Underline = XLFontUnderlineValues.Single;
                            //ws.Range("A1:E1").Row(1).Merge();
                            //ws.Range("G1:L1").Row(1).Merge();

                            fin_year = finyear1.Rows[0]["v_finyear_code"].ToString();
                            show_fin_year = fin_year;
                            fin_year = "Financial Year " + fin_year;
                            ws.Cell("F2").SetValue(fin_year); ws.Cell("F2").Style.Font.Bold = true;
                            ws.Cell("F2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                            var fin_month = finyear1.Rows[0]["v_date_format"].ToString();
                            fin_month = "BRS Summary for the month of " + lastday;
                            ws.Cell("F3").SetValue(fin_month); ws.Cell("F3").Style.Font.Bold = true;
                            ws.Cell("F3").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;



                            reconsummary.Columns[0].ColumnName = "S.No.";
                            reconsummary.Columns[1].ColumnName = "GL Code";
                            reconsummary.Columns[2].ColumnName = "Bank Name";
                            reconsummary.Columns[3].ColumnName = "Account Number";
                            reconsummary.Columns[4].ColumnName = "Account Type";
                            reconsummary.Columns[5].ColumnName = "Closing  balance as per Cash Book";
                            reconsummary.Columns[6].ColumnName = "Less:Cheques deposited but not credited in bank";
                            reconsummary.Columns[7].ColumnName = "Add:Cheques issued but not presented in to bank";
                            reconsummary.Columns[8].ColumnName = "Less:Amount debited in bank but not accounted";
                            reconsummary.Columns[9].ColumnName = "Add:Amount credited in bank but not accounted";
                            reconsummary.Columns[10].ColumnName = "Grand Total";
                            reconsummary.Columns[11].ColumnName = "Closing balance as per bank passbook";
                            reconsummary.AcceptChanges();

                            wb.Worksheet(1).Cell("A4").InsertTable(reconsummary);
                            wb.Worksheet(1).Table(0).ShowAutoFilter = false;// Disable AutoFilter.
                            wb.Worksheet(1).Table(0).Theme = XLTableTheme.None;
                            ws.Range("A4:M4").Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);
                            ws.Range("A4:M4").Style.Font.Bold = true;
                            wb.Worksheet(1).Table(0).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            wb.Worksheet(1).Table(0).Style.Border.BottomBorderColor = XLColor.Black;
                            wb.Worksheet(1).Table(0).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            wb.Worksheet(1).Table(0).Style.Border.TopBorderColor = XLColor.Black;
                            wb.Worksheet(1).Table(0).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            wb.Worksheet(1).Table(0).Style.Border.LeftBorderColor = XLColor.Black;
                            wb.Worksheet(1).Table(0).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            wb.Worksheet(1).Table(0).Style.Border.RightBorderColor = XLColor.Black;

                            int count_rsummary = reconsummary.Rows.Count + 4;


                            int row_cal = count_rsummary + 1;

                            string cell_txt_summary = "F" + count_rsummary.ToString() + "";
                            var sum = ws.Evaluate("SUM(F5:" + cell_txt_summary + ")");
                            string row_total = "F" + row_cal.ToString() + "";
                            ws.Cell(row_total).SetValue(sum); ws.Cell(row_total).Style.Font.Bold = true;
                            ws.Cell(row_total).Style.Font.Underline = XLFontUnderlineValues.Double;


                            cell_txt_summary = "E" + row_cal.ToString() + "";
                            ws.Cell(cell_txt_summary).SetValue("Grand Total"); ws.Cell(cell_txt_summary).Style.Font.Bold = true;
                            ws.Cell(cell_txt_summary).Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);

                            cell_txt_summary = "G" + count_rsummary.ToString() + "";
                            sum = ws.Evaluate("SUM(G5:" + cell_txt_summary + ")");
                            row_total = "G" + row_cal.ToString() + "";
                            ws.Cell(row_total).SetValue(sum); ws.Cell(row_total).Style.Font.Bold = true;
                            ws.Cell(row_total).Style.Font.Underline = XLFontUnderlineValues.Double;

                            cell_txt_summary = "H" + count_rsummary.ToString() + "";
                            sum = ws.Evaluate("SUM(H5:" + cell_txt_summary + ")");
                            row_total = "H" + row_cal.ToString() + "";
                            ws.Cell(row_total).SetValue(sum); ws.Cell(row_total).Style.Font.Bold = true;
                            ws.Cell(row_total).Style.Font.Underline = XLFontUnderlineValues.Double;

                            cell_txt_summary = "I" + count_rsummary.ToString() + "";
                            sum = ws.Evaluate("SUM(I5:" + cell_txt_summary + ")");
                            row_total = "I" + row_cal.ToString() + "";
                            ws.Cell(row_total).SetValue(sum); ws.Cell(row_total).Style.Font.Bold = true;
                            ws.Cell(row_total).Style.Font.Underline = XLFontUnderlineValues.Double;

                            cell_txt_summary = "J" + count_rsummary.ToString() + "";
                            sum = ws.Evaluate("SUM(J5:" + cell_txt_summary + ")");
                            row_total = "J" + row_cal.ToString() + "";
                            ws.Cell(row_total).SetValue(sum); ws.Cell(row_total).Style.Font.Bold = true;
                            ws.Cell(row_total).Style.Font.Underline = XLFontUnderlineValues.Double;

                            cell_txt_summary = "K" + count_rsummary.ToString() + "";
                            sum = ws.Evaluate("SUM(K5:" + cell_txt_summary + ")");
                            row_total = "K" + row_cal.ToString() + "";
                            ws.Cell(row_total).SetValue(sum); ws.Cell(row_total).Style.Font.Bold = true;
                            ws.Cell(row_total).Style.Font.Underline = XLFontUnderlineValues.Double;

                            cell_txt_summary = "L" + count_rsummary.ToString() + "";
                            sum = ws.Evaluate("SUM(L5:" + cell_txt_summary + ")");
                            row_total = "L" + row_cal.ToString() + "";
                            ws.Cell(row_total).SetValue(sum); ws.Cell(row_total).Style.Font.Bold = true;
                            ws.Cell(row_total).Style.Font.Underline = XLFontUnderlineValues.Double;

                            cell_txt_summary = "M" + count_rsummary.ToString() + "";
                            sum = ws.Evaluate("SUM(M5:" + cell_txt_summary + ")");
                            row_total = "M" + row_cal.ToString() + "";
                            ws.Cell(row_total).SetValue(sum); ws.Cell(row_total).Style.Font.Bold = true;
                            ws.Cell(row_total).Style.Font.Underline = XLFontUnderlineValues.Double;
                        }




                        ws = wb.AddWorksheet(recon_name1);
                        string reconbrs = entity_name.Trim('\'');
                        ws.FirstCell().SetValue(reconbrs); ws.Cell("A1").Style.Font.Bold = true;
                        ws.Cell("A1").Style.Font.Underline.ToString(); ws.Cell("A1").Style.Font.Underline = XLFontUnderlineValues.Single;
                        ws.Range("A1:D1").Row(1).Merge();

                        ws.Cell("E1").SetValue("GL CODE").SetActive(); ws.Cell("E1").Style.Font.Bold = true;
                        ws.Cell("F1").SetValue(acc_no2).SetActive(); ws.Cell("F1").Style.Font.Bold = true;

                        ws.FirstCell().CellBelow().SetValue(recon_name);
                        ws.Cell("A2").Style.Font.Bold = true;
                        ws.Range("A2:F2").Row(1).Merge();

                        ws.Cell("A3").SetValue("CC A/c No." + acc_no1 + "").SetActive(); ws.Cell("A3").Style.Font.Bold = true;
                        ws.Range("A3:B3").Row(1).Merge();

                        //ws.Cell("C3").SetValue(acc_no1).SetActive(); ws.Cell("C3").Style.Font.Bold = true;
                        //ws.Range("C3:F3").Row(1).Merge();

                        ws.Cell("A4").SetValue("Bank Reconciliation Statement as on date " + Acc1date + "").SetActive();
                        ws.Cell("A4").Style.Font.Bold = true;
                        ws.Range("A4:F4").Row(1).Merge();

                        ws.Cell("A6").SetValue("S U M M A R Y").SetActive(); ws.Cell("A6").Style.Font.Bold = true;
                        ws.Cell("A6").Style.Font.Underline = XLFontUnderlineValues.Single;
                        ws.Cell("A6").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("A6:F6").Row(1).Merge();

                        ws.Cell("A7").SetValue("Closing balance  as per  Cash book as on " + Acc2date + "").SetActive();
                        ws.Cell("A7").Style.Font.Bold = true;
                        ws.Range("A7:E7").Row(1).Merge();
                        ws.Cell("I7").SetValue(acc2Bal_amount).SetActive(); ws.Cell("I7").Style.NumberFormat.Format = "###0.00";

                        ws.Cell("A9").SetValue("Less: Cheques deposited but not credited in bank").SetActive();
                        ws.Range("A9:E9").Row(1).Merge();
                        ws.Cell("I9").SetValue(accno2_drtotal).SetActive(); ws.Cell("FI9").Style.Font.Bold = true; ws.Cell("I9").Style.NumberFormat.Format = "###0.00";

                        ws.Cell("A11").SetValue("Add: Cheques issued but not presented in to bank").SetActive();
                        ws.Range("A11:E11").Row(1).Merge();
                        ws.Cell("I11").SetValue(accno2_crtotal).SetActive(); ws.Cell("I11").Style.NumberFormat.Format = "###0.00";

                        ws.Cell("A13").SetValue("Less: Amount debited in bank but not accounted").SetActive();
                        ws.Range("A13:E13").Row(1).Merge();
                        ws.Cell("I13").SetValue(accno1_drtotal).SetActive(); ws.Cell("I13").Style.NumberFormat.Format = "###0.00";

                        ws.Cell("A15").SetValue("Add: Amount credited in bank but not accounted").SetActive();
                        ws.Range("A15:E15").Row(1).Merge();
                        ws.Cell("I15").SetValue(accno1_crtotal).SetActive(); ws.Cell("I15").Style.NumberFormat.Format = "###0.00";

                        ws.Cell("A17").SetValue("Closing balance as per bank passbook").SetActive();
                        ws.Cell("A17").Style.Font.Bold = true;
                        ws.Range("A17:E17").Row(1).Merge();

                        ws.Cell("I17").SetValue(acc1Bal_amount).SetActive(); ws.Cell("I17").Style.Font.Bold = true; ws.Cell("I17").Style.NumberFormat.Format = "###0.00";
                        ws.Cell("I17").Style.Font.Bold = true; //ws.Cell("F17").Style.Font.Underline = XLFontUnderlineValues.Double;
                        ws.Cell("I17").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        ws.Cell("I17").Style.Border.TopBorderColor = XLColor.Black;
                        ws.Cell("I17").Style.Border.BottomBorder = XLBorderStyleValues.Double;
                        ws.Cell("I17").Style.Border.BottomBorderColor = XLColor.Black;

                        ws.Cell("A19").SetValue("Less: Cheques deposited but not credited in bank").SetActive(); ws.Cell("A19").Style.Font.Bold = true;
                        ws.Cell("A19").Style.Font.Underline = XLFontUnderlineValues.Single;
                        ws.Range("A19:F19").Row(1).Merge();

                        //Inserting  Table1 Data
                        wb.Worksheet(worksheetcount).Cell("A20").InsertTable(Table1);
                        wb.Worksheet(worksheetcount).Table(0).ShowAutoFilter = false;// Disable AutoFilter.
                        wb.Worksheet(worksheetcount).Table(0).Theme = XLTableTheme.None;
                        ws.Range("A20:Q20").Style.Font.Bold = true;
                        ws.Range("A20:Q20").Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);


                        //Inserting  Table1 Total
                        formulaTxt = "=sum(I21:I" + (20 + Table1.Rows.Count).ToString() + ")";
                        row = 20 + Table1.Rows.Count + 1;
                        cellTxt = "I" + row.ToString() + "";
                        ws.Cell(cellTxt).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        ws.Cell(cellTxt).Style.Border.BottomBorderColor = XLColor.Black;

                        var cellFormulaTableSum = ws.Cell(cellTxt);

                        cellFormulaTableSum.FormulaA1 = formulaTxt; cellFormulaTableSum.Style.NumberFormat.Format = "###0.00";

                        //Inserting calculation
                        calcCellTxt = "I7";
                        formulaTxt = "=+(" + calcCellTxt + "*1)-" + cellTxt;

                        row = row + 1;
                        cellTxt = "I" + row.ToString() + "";
                        calcCellTxt = cellTxt;

                        var cellFormulaCalc = ws.Cell(cellTxt);
                        cellFormulaCalc.FormulaA1 = formulaTxt; cellFormulaCalc.Style.NumberFormat.Format = "###0.00";

                        //Inserting  Table2 Data
                        row = row + 2;
                        cellTxt = "A" + row.ToString() + "";

                        ws.Cell(cellTxt).SetValue("Add: Cheques issued but not presented in to bank").SetActive(); ws.Cell(cellTxt).Style.Font.Bold = true;
                        ws.Cell(cellTxt).Style.Font.Underline = XLFontUnderlineValues.Single;
                        cellTxt = "A" + row.ToString() + ":F" + row.ToString();
                        ws.Range(cellTxt).Row(1).Merge();

                        row = row + 1;
                        cellTxt = "A" + row.ToString() + "";

                        wb.Worksheet(worksheetcount).Cell(cellTxt).InsertTable(Table2);
                        wb.Worksheet(worksheetcount).Table(1).ShowAutoFilter = false;// Disable AutoFilter.
                        wb.Worksheet(worksheetcount).Table(1).Theme = XLTableTheme.None;
                        cellTxt1 = "A" + row.ToString() + ":Q" + row.ToString();
                        ws.Range(cellTxt1).Style.Font.Bold = true;
                        ws.Range(cellTxt1).Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);


                        //Inserting  Table2 Total
                        formulaTxt = "=sum(I" + (row + 1).ToString() + ":I" + (row + Table2.Rows.Count).ToString() + ")";
                        row = row + Table2.Rows.Count + 1;
                        cellTxt = "I" + row.ToString() + "";
                        ws.Cell(cellTxt).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        ws.Cell(cellTxt).Style.Border.BottomBorderColor = XLColor.Black;
                        cellFormulaTableSum = ws.Cell(cellTxt);
                        cellFormulaTableSum.FormulaA1 = formulaTxt; cellFormulaTableSum.Style.NumberFormat.Format = "###0.00";

                        formulaTxt = "=+" + calcCellTxt + "+" + cellTxt;

                        row = row + 1;
                        cellTxt = "I" + row.ToString() + "";
                        calcCellTxt = cellTxt;

                        cellFormulaCalc = ws.Cell(cellTxt);
                        cellFormulaCalc.FormulaA1 = formulaTxt; cellFormulaCalc.Style.NumberFormat.Format = "###0.00";

                        row = row + 2;
                        cellTxt = "A" + row.ToString() + "";

                        ws.Cell(cellTxt).SetValue("Less: Amount  debited  in  bank but not accounted").SetActive(); ws.Cell(cellTxt).Style.Font.Bold = true;
                        ws.Cell(cellTxt).Style.Font.Underline = XLFontUnderlineValues.Single;
                        cellTxt = "A" + row.ToString() + ":F" + row.ToString();
                        ws.Range(cellTxt).Row(1).Merge();

                        //Inserting  Table3 Data
                        row = row + 1;
                        cellTxt = "A" + row.ToString() + "";

                        wb.Worksheet(worksheetcount).Cell(cellTxt).InsertTable(Table3);
                        wb.Worksheet(worksheetcount).Table(2).ShowAutoFilter = false;// Disable AutoFilter.
                        wb.Worksheet(worksheetcount).Table(2).Theme = XLTableTheme.None;
                        cellTxt1 = "A" + row.ToString() + ":Q" + row.ToString();
                        ws.Range(cellTxt1).Style.Font.Bold = true;
                        ws.Range(cellTxt1).Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);

                        //Inserting  Table3 Total
                        formulaTxt = "=sum(I" + (row + 1).ToString() + ":I" + (row + Table3.Rows.Count).ToString() + ")";
                        row = row + Table3.Rows.Count + 1;
                        cellTxt = "I" + row.ToString() + "";
                        ws.Cell(cellTxt).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        ws.Cell(cellTxt).Style.Border.BottomBorderColor = XLColor.Black;
                        cellFormulaTableSum = ws.Cell(cellTxt);
                        cellFormulaTableSum.FormulaA1 = formulaTxt; cellFormulaTableSum.Style.NumberFormat.Format = "###0.00";

                        formulaTxt = "=+" + calcCellTxt + "-" + cellTxt;

                        row = row + 1;
                        cellTxt = "I" + row.ToString() + "";
                        calcCellTxt = cellTxt;

                        cellFormulaCalc = ws.Cell(cellTxt);
                        cellFormulaCalc.FormulaA1 = formulaTxt; cellFormulaCalc.Style.NumberFormat.Format = "###0.00";

                        row = row + 2;
                        cellTxt = "A" + row.ToString() + "";

                        ws.Cell(cellTxt).SetValue("Add: Amount credited in bank but not accounted").SetActive(); ws.Cell(cellTxt).Style.Font.Bold = true;
                        ws.Cell(cellTxt).Style.Font.Underline = XLFontUnderlineValues.Single;

                        cellTxt = "A" + row.ToString() + ":F" + row.ToString();
                        ws.Range(cellTxt).Row(1).Merge();

                        row = row + 1;
                        cellTxt = "A" + row.ToString() + "";

                        wb.Worksheet(worksheetcount).Cell(cellTxt).InsertTable(Table4);
                        wb.Worksheet(worksheetcount).Table(3).ShowAutoFilter = false;// Disable AutoFilter.
                        wb.Worksheet(worksheetcount).Table(3).Theme = XLTableTheme.None;
                        cellTxt1 = "A" + row.ToString() + ":Q" + row.ToString();
                        ws.Range(cellTxt1).Style.Font.Bold = true;
                        ws.Range(cellTxt1).Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);

                        //Inserting  Table4 Total
                        formulaTxt = "=sum(I" + (row + 1).ToString() + ":I" + (row + Table4.Rows.Count).ToString() + ")";
                        row = row + Table4.Rows.Count + 1;
                        cellTxt = "I" + row.ToString() + "";
                        ws.Cell(cellTxt).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        ws.Cell(cellTxt).Style.Border.BottomBorderColor = XLColor.Black;
                        cellFormulaTableSum = ws.Cell(cellTxt);
                        cellFormulaTableSum.FormulaA1 = formulaTxt; cellFormulaTableSum.Style.NumberFormat.Format = "###0.00";

                        formulaTxt = "=+" + calcCellTxt + "+" + cellTxt;

                        row = row + 1;
                        cellTxt = "I" + row.ToString() + "";
                        calcCellTxt = cellTxt;

                        cellFormulaCalc = ws.Cell(cellTxt);
                        cellFormulaCalc.FormulaA1 = formulaTxt; cellFormulaCalc.Style.NumberFormat.Format = "###0.00";
                        cellFormulaCalc.Style.Font.Bold = true;
                        // cellFormulaCalc.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        // cellFormulaCalc.Style.Border.TopBorderColor = XLColor.Black;
                        //cellFormulaCalc.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        //cellFormulaCalc.Style.Border.BottomBorderColor = XLColor.Black;
                        //cellFormulaCalc.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                        //Total Balance & Difference
                        cellTxt = "B" + row.ToString() + "";
                        ws.Cell(cellTxt).SetValue("Balance as per bank pass book").SetActive(); ws.Cell(cellTxt).Style.Font.Bold = true;

                        //Hema edit

                        row = row + 1;
                        cellTxt = "B" + row.ToString() + "";
                        ws.Cell(cellTxt).SetValue("Bank Statement Month Closing Balance").SetActive(); ws.Cell(cellTxt).Style.Font.Bold = true;

                        formulaTxt = "I17";

                        cellTxt = "I" + row.ToString() + "";

                        cellFormulaCalc = ws.Cell(cellTxt);
                        cellFormulaCalc.FormulaA1 = formulaTxt; cellFormulaCalc.Style.NumberFormat.Format = "###0.00";
                        cellFormulaCalc.Style.Font.Bold = true;
                        cellFormulaCalc.Style.Border.BottomBorder = XLBorderStyleValues.Double;
                        cellFormulaCalc.Style.Border.BottomBorderColor = XLColor.Black;
                        cellFormulaCalc.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                        
                        // Edit ends

                        row = row + 2;
                        cellTxt = "B" + row.ToString() + "";
                        ws.Cell(cellTxt).SetValue("Difference").SetActive(); ws.Cell(cellTxt).Style.Font.Bold = true;


                        //Hema Edit start
                        // formulaTxt = "=+" + calcCellTxt + "-I" + "-I17";

                        formulaTxt = "=+" + calcCellTxt + "-I" + Convert.ToString((Convert.ToInt32(calcCellTxt.Split('I')[1])+1));

                        //Hema Edit ends

                        cellTxt = "I" + row.ToString() + "";

                        cellFormulaCalc = ws.Cell(cellTxt);
                        cellFormulaCalc.FormulaA1 = formulaTxt; cellFormulaCalc.Style.NumberFormat.Format = "###0.00";
                        cellFormulaCalc.Style.Font.Bold = true;
                        cellFormulaCalc.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        cellFormulaCalc.Style.Border.TopBorderColor = XLColor.Black;
                        cellFormulaCalc.Style.Border.BottomBorder = XLBorderStyleValues.Double;
                        cellFormulaCalc.Style.Border.BottomBorderColor = XLColor.Black;
                        cellFormulaCalc.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                        ws.Columns("A").AdjustToContents();
                        ws.Columns("F").AdjustToContents();

                        //Insert Transaction
                        //ws = wb.AddWorksheet("Transaction" + filename_tran);
                        //ws.Cell("A1").InsertTable(Table6).Theme = XLTableTheme.None;
                        //worksheetcount = worksheetcount+1;
                        //ws.Tables.ForEach(x => x.ShowAutoFilter = false);
                        //ws.Cell("A1").Style.Font.Bold = true;
                        //ws.Range("A1:Z1").Style.Font.Bold = true;
                        //ws.Range("A1:Z1").Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);
                        //ws.Table(6).Theme = XLTableTheme.None;

                        if (Table7.Rows.Count > 0)
                        {
                            int fileid = Convert.ToInt32(Table7.Rows[0]["File Id"]);
                            //Insert Supporting File
                            if (fileid != 0)
                            {
                                ws = wb.AddWorksheet("SupportingFile" + filename_tran);
                                ws.Cell("A1").InsertTable(Table7).Theme = XLTableTheme.None;
                                worksheetcount = worksheetcount + 1;
                                ws.Tables.ForEach(x => x.ShowAutoFilter = false);
                                ws.Range("A1:Z1").Style.Font.Bold = true;
                                ws.Range("A1:Z1").Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);
                            }

                            // ws.Table(7).Theme = XLTableTheme.None;
                        }


                    }
                }
                using (MemoryStream stream = new MemoryStream())
                {


                    string directoryPath = "D:\\MonthEndReport\\" + show_fin_year + "\\" + Month + "\\";
                    string filePath = Path.Combine(directoryPath, fileName);

                    wb.SaveAs("D:\\MonthEndReport\\" + show_fin_year + "\\" + Month + "\\" + fileName + "");
                     using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                     {
                         fileStream.CopyTo(stream);
                         return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                     }
                    
                }

               


            }
            catch (Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
            return View();

        }


        public ActionResult MonthBRS_Download_1(string TranDate, string recon_gid)
        {
            
            try
            {
                string fin_year = "";
                string show_fin_year = "";
                List<TransactionRpt_model> objcat_3st = new List<TransactionRpt_model>();
                DataSet result2 = new DataSet();
                DataSet result_data = new DataSet();
                string fileName = "";
               // int recon_gid = 0;
                XLWorkbook wb = new XLWorkbook();
               // var ws = "";
                DataSet dt = new DataSet();
                DataTable reconsummary = new DataTable();
                DataTable finyear1 = new DataTable();
                DataTable Table = new DataTable();
                DataTable Table1 = new DataTable();
                DataTable Table2 = new DataTable();
                DataTable Table3 = new DataTable();
                DataTable Table4 = new DataTable();
                DataTable Table6 = new DataTable();
                DataTable Table7 = new DataTable();

                long row = 0;
                long col = 0;
                int worksheetcount = 1;
                string cellTxt = "";
                string cellTxt1 = "";
                string filename_tran = "";
                string formulaTxt = "";
                string calcCellTxt = "";
                var recon_name1 = "";

                List<int> recons = recon_gid.Split(',').Select(int.Parse).ToList();
                for (int i = 0; i < recons.Count; i++)
                {
                    if (recons[i] == 0)
                    {
                        recons.RemoveAt(i);
                        i--;
                    }
                }
               
                int no_of_recons = recons.Count;

                //getting recon summary table
                TransactionRpt_model DedupList = new TransactionRpt_model();
                DedupList.Trandate = TranDate;
                DedupList.Recon_gid = recon_gid;
                DedupList.no_of_recons = no_of_recons;
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(DedupList), "ReportMonthbrs_Download_summary");
                result_data = (DataSet)JsonConvert.DeserializeObject(post_data, result2.GetType());
                reconsummary = result_data.Tables[1].Copy();
                finyear1 = result_data.Tables[0].Copy();

                int recon_summary = 1;

                for (int j = 0; j < recons.Count; j++)
                {
                    worksheetcount = worksheetcount+ 1;
                    int Reconid=recons[j];
                    TransactionRpt_model DedupList1 = new TransactionRpt_model();
                    DedupList.Trandate = TranDate;
                    DedupList.Recongid = Reconid;
                    string post_data1 = objcommon.getApiResult(JsonConvert.SerializeObject(DedupList), "ReportMonthbrs_Download");
                    result2 = (DataSet)JsonConvert.DeserializeObject(post_data1, result2.GetType());




                    TransactionRpt_model objcat3 = new TransactionRpt_model();
                    Table = result2.Tables[0].Copy();
                    Table1 = result2.Tables[1].Copy();
                    Table1.Columns["Column1"].ColumnName = "-";
                    Table2 = result2.Tables[2].Copy();
                    Table2.Columns["Column1"].ColumnName = "-";
                    Table3 = result2.Tables[3].Copy();
                    Table3.Columns["Column1"].ColumnName = "-";
                    Table4 = result2.Tables[4].Copy();
                    Table4.Columns["Column1"].ColumnName = "-";
                    Table6 = result2.Tables[6].Copy();
                    Table7 = result2.Tables[7].Copy();

                    var entity_name = Table.Rows[0]["entity"].ToString();
                    var recon_name = Table.Rows[0]["recon_name"].ToString();
                    int i = recon_name.Length;

                    if (i >= 30)
                    {
                        recon_name1 = recon_name.Substring(0,30);
                        recon_name1 = Regex.Replace(recon_name1, @"[^0-9a-zA-Z\._]", string.Empty);

                    }
                    else
                    {
                        recon_name1 = recon_name;
                        recon_name1 = Regex.Replace(recon_name1, @"[^0-9a-zA-Z\._]", string.Empty);
                    }

                    filename_tran = recon_name1.Substring(0, 5);

                    var acc_no1 = Table.Rows[0]["acc_no1"].ToString();
                    string Acc2date = "";
                    string Acc1date = "";
                    //  string s2 = Regex.Replace(recon_name, @"[^A-Z]+", String.Empty);
                    var acc_no2 = Table.Rows[0]["acc_no2"].ToString();
                    //string Acc1date = Table.Rows[0]["acc_no1_bal_date"].ToString();
                    //string Acc2date = Table.Rows[0]["acc_no2_bal_date"].ToString();
                    Decimal acc1Bal_amount = Convert.ToDecimal(Table.Rows[0]["acc_no1_bal_amount"].ToString());
                    Decimal acc2Bal_amount = Convert.ToDecimal(Table.Rows[0]["acc_no2_bal_amount"].ToString());
                    Decimal accno1_drtotal = Convert.ToDecimal(Table.Rows[0]["acc_no1_dr_total"].ToString());
                    Decimal accno2_drtotal = Convert.ToDecimal(Table.Rows[0]["acc_no2_dr_total"].ToString());
                    Decimal accno1_crtotal = Convert.ToDecimal(Table.Rows[0]["acc_no1_cr_total"].ToString());
                    Decimal accno2_crtotal = Convert.ToDecimal(Table.Rows[0]["acc_no2_cr_total"].ToString());

                    if (accno2_crtotal == 0 && accno1_crtotal == 0 && accno2_drtotal == 0 && accno1_drtotal == 0 && acc1Bal_amount == 0 && acc2Bal_amount == 0)
                    {
                        int year = DateTime.Now.Year;
                        int month = DateTime.Now.Month;
                        DateTime lastDayOfMonth = new DateTime(year, month, DateTime.DaysInMonth(year, month));
                        Acc2date = lastDayOfMonth.ToString("dd-MM-yyyy");
                        Acc1date = lastDayOfMonth.ToString("dd-MM-yyyy");

                    }
                    else
                    {
                        Acc2date = Table.Rows[0]["acc_no2_bal_date"].ToString();
                        Acc1date = Table.Rows[0]["acc_no1_bal_date"].ToString();
                    }
                    int pMonth;
                    var val = Convert.ToDateTime(TranDate);
                    int pYear = val.Year;
                   //int pMonth = val.Month - 1;
                    int checkpMonth = val.Month - 1;
                    if (checkpMonth == 0)
                    {
                        //pMonth = val.Month;
                        pMonth = 12;
                        pYear = val.Year - 1;
                    }
                    else
                    {
                        pMonth = checkpMonth;
                        pYear = val.Year;
                    }
                    DateTime lastDayPrevMonth = new DateTime(pYear, pMonth, DateTime.DaysInMonth(pYear, pMonth));
                    var lastday = lastDayPrevMonth.ToString("dd-MM-yyyy");

                    dt = result2.Copy();
                    dt.Tables.Remove("Table");

                    //fileName = acc_no1 + ".xlsx";
                    fileName = "AllReconsMonth_Brs_Summary.xlsx";   

                    using (wb )// wb.Worksheets.Add(dt);
                    {
                        ///Setting Table Data static & Dynamic;
                        var ws = wb.AddWorksheet("todelete");
                        if (ws.Name == "todelete")
                        {
                            ws.Delete();
                        }
                        if(recon_summary==1)
                        {
                             ///Inserting reconsummary table
                            ws = wb.AddWorksheet("Reconsummary");
                            recon_summary = 0;

                            string name = entity_name.Trim('\'');
                            ws.Cell("F1").SetValue(name); ws.Cell("F1").Style.Font.Bold = true;
                            ws.Cell("F1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell("F1").Style.Font.Underline.ToString(); ws.Cell("F1").Style.Font.Underline = XLFontUnderlineValues.Single;
                            //ws.Range("A1:E1").Row(1).Merge();
                            //ws.Range("G1:L1").Row(1).Merge();

                            fin_year = finyear1.Rows[0]["v_finyear_code"].ToString();
                            show_fin_year = fin_year;
                            fin_year = "Financial Year " + fin_year;
                            ws.Cell("F2").SetValue(fin_year); ws.Cell("F2").Style.Font.Bold = true;
                            ws.Cell("F2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                            var fin_month = finyear1.Rows[0]["v_date_format"].ToString();
                            fin_month = "BRS Summary for the month of " + lastday;
                            ws.Cell("F3").SetValue(fin_month); ws.Cell("F3").Style.Font.Bold = true;
                            ws.Cell("F3").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;



                            reconsummary.Columns[0].ColumnName = "S.No.";
                            reconsummary.Columns[1].ColumnName = "GL Code";
                            reconsummary.Columns[2].ColumnName = "Bank Name";
                            reconsummary.Columns[3].ColumnName = "Account Number";
                            reconsummary.Columns[4].ColumnName = "Account Type";
                            reconsummary.Columns[5].ColumnName = "Closing  balance as per Cash Book";
                            reconsummary.Columns[6].ColumnName = "Less:Cheques deposited but not credited in bank";
                            reconsummary.Columns[7].ColumnName = "Add:Cheques issued but not presented in to bank";
                            reconsummary.Columns[8].ColumnName = "Less:Amount debited in bank but not accounted";
                            reconsummary.Columns[9].ColumnName ="Add:Amount credited in bank but not accounted";
                            reconsummary.Columns[10].ColumnName = "Grand Total";
                            reconsummary.Columns[11].ColumnName = "Closing balance as per bank passbook";
                            reconsummary.AcceptChanges();

                            wb.Worksheet(1).Cell("A4").InsertTable(reconsummary);
                            wb.Worksheet(1).Table(0).ShowAutoFilter = false;// Disable AutoFilter.
                            wb.Worksheet(1).Table(0).Theme=XLTableTheme.None;
                            ws.Range("A4:M4").Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);
                            ws.Range("A4:M4").Style.Font.Bold = true;
                            wb.Worksheet(1).Table(0).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            wb.Worksheet(1).Table(0).Style.Border.BottomBorderColor = XLColor.Black;
                            wb.Worksheet(1).Table(0).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            wb.Worksheet(1).Table(0).Style.Border.TopBorderColor = XLColor.Black;
                            wb.Worksheet(1).Table(0).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            wb.Worksheet(1).Table(0).Style.Border.LeftBorderColor = XLColor.Black;
                            wb.Worksheet(1).Table(0).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            wb.Worksheet(1).Table(0).Style.Border.RightBorderColor= XLColor.Black;

                            int count_rsummary = reconsummary.Rows.Count+4;

                           
                            int  row_cal = count_rsummary + 1;

                            string cell_txt_summary = "F" + count_rsummary.ToString() + "";
                            var sum = ws.Evaluate("SUM(F5:" + cell_txt_summary + ")");
                            string row_total = "F" + row_cal.ToString() + "";
                            ws.Cell(row_total).SetValue(sum); ws.Cell(row_total).Style.Font.Bold = true;
                            ws.Cell(row_total).Style.Font.Underline = XLFontUnderlineValues.Double;


                            cell_txt_summary = "E" + row_cal.ToString() + "";
                            ws.Cell(cell_txt_summary).SetValue("Grand Total"); ws.Cell(cell_txt_summary).Style.Font.Bold = true;
                            ws.Cell(cell_txt_summary).Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);

                            cell_txt_summary = "G" + count_rsummary.ToString() + "";
                            sum = ws.Evaluate("SUM(G5:" + cell_txt_summary + ")");
                            row_total = "G" + row_cal.ToString() + "";
                            ws.Cell(row_total).SetValue(sum); ws.Cell(row_total).Style.Font.Bold = true;
                            ws.Cell(row_total).Style.Font.Underline = XLFontUnderlineValues.Double;

                            cell_txt_summary = "H" + count_rsummary.ToString() + "";
                            sum = ws.Evaluate("SUM(H5:" + cell_txt_summary + ")");
                            row_total = "H" + row_cal.ToString() + "";
                            ws.Cell(row_total).SetValue(sum); ws.Cell(row_total).Style.Font.Bold = true;
                            ws.Cell(row_total).Style.Font.Underline = XLFontUnderlineValues.Double;

                            cell_txt_summary = "I" + count_rsummary.ToString() + "";
                            sum = ws.Evaluate("SUM(I5:" + cell_txt_summary + ")");
                            row_total = "I" + row_cal.ToString() + "";
                            ws.Cell(row_total).SetValue(sum); ws.Cell(row_total).Style.Font.Bold = true;
                            ws.Cell(row_total).Style.Font.Underline = XLFontUnderlineValues.Double;

                            cell_txt_summary = "J" + count_rsummary.ToString() + "";
                            sum = ws.Evaluate("SUM(J5:" + cell_txt_summary + ")");
                            row_total = "J" + row_cal.ToString() + "";
                            ws.Cell(row_total).SetValue(sum); ws.Cell(row_total).Style.Font.Bold = true;
                            ws.Cell(row_total).Style.Font.Underline = XLFontUnderlineValues.Double;

                            cell_txt_summary = "K" + count_rsummary.ToString() + "";
                            sum = ws.Evaluate("SUM(K5:" + cell_txt_summary + ")");
                            row_total = "K" + row_cal.ToString() + "";
                            ws.Cell(row_total).SetValue(sum); ws.Cell(row_total).Style.Font.Bold = true;
                            ws.Cell(row_total).Style.Font.Underline = XLFontUnderlineValues.Double;

                            cell_txt_summary = "L" + count_rsummary.ToString() + "";
                            sum = ws.Evaluate("SUM(L5:" + cell_txt_summary + ")");
                            row_total = "L" + row_cal.ToString() + "";
                            ws.Cell(row_total).SetValue(sum); ws.Cell(row_total).Style.Font.Bold = true;
                            ws.Cell(row_total).Style.Font.Underline = XLFontUnderlineValues.Double;

                            cell_txt_summary = "M" + count_rsummary.ToString() + "";
                            sum = ws.Evaluate("SUM(M5:" + cell_txt_summary + ")");
                            row_total = "M" + row_cal.ToString() + "";
                            ws.Cell(row_total).SetValue(sum); ws.Cell(row_total).Style.Font.Bold = true;
                            ws.Cell(row_total).Style.Font.Underline = XLFontUnderlineValues.Double;
                        }

                       
                        
                       
                        ws = wb.AddWorksheet(recon_name1);
                        string reconbrs = entity_name.Trim('\'');
                        ws.FirstCell().SetValue(reconbrs); ws.Cell("A1").Style.Font.Bold = true;
                        ws.Cell("A1").Style.Font.Underline.ToString(); ws.Cell("A1").Style.Font.Underline = XLFontUnderlineValues.Single;
                        ws.Range("A1:D1").Row(1).Merge();

                        ws.Cell("E1").SetValue("GL CODE").SetActive(); ws.Cell("E1").Style.Font.Bold = true;
                        ws.Cell("F1").SetValue(acc_no2).SetActive(); ws.Cell("F1").Style.Font.Bold = true;

                        ws.FirstCell().CellBelow().SetValue(recon_name);
                        ws.Cell("A2").Style.Font.Bold = true;
                        ws.Range("A2:F2").Row(1).Merge();

                        ws.Cell("A3").SetValue("CC A/c No."+acc_no1+"").SetActive(); ws.Cell("A3").Style.Font.Bold = true;
                        ws.Range("A3:B3").Row(1).Merge();

                        //ws.Cell("C3").SetValue(acc_no1).SetActive(); ws.Cell("C3").Style.Font.Bold = true;
                        //ws.Range("C3:F3").Row(1).Merge();

                        ws.Cell("A4").SetValue("Bank Reconciliation Statement as on date " + Acc1date + "").SetActive();
                        ws.Cell("A4").Style.Font.Bold = true;
                        ws.Range("A4:F4").Row(1).Merge();

                        ws.Cell("A6").SetValue("S U M M A R Y").SetActive(); ws.Cell("A6").Style.Font.Bold = true;
                        ws.Cell("A6").Style.Font.Underline = XLFontUnderlineValues.Single;
                        ws.Cell("A6").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("A6:F6").Row(1).Merge();

                        ws.Cell("A7").SetValue("Closing balance  as per  Cash book as on " + Acc2date + "").SetActive();
                        ws.Cell("A7").Style.Font.Bold = true;
                        ws.Range("A7:E7").Row(1).Merge();
                        ws.Cell("I7").SetValue(acc2Bal_amount).SetActive(); ws.Cell("I7").Style.NumberFormat.Format = "###0.00";

                        ws.Cell("A9").SetValue("Less: Cheques deposited but not credited in bank").SetActive();
                        ws.Range("A9:E9").Row(1).Merge();
                        ws.Cell("I9").SetValue(accno2_drtotal).SetActive(); ws.Cell("FI9").Style.Font.Bold = true; ws.Cell("I9").Style.NumberFormat.Format = "###0.00";

                        ws.Cell("A11").SetValue("Add: Cheques issued but not presented in to bank").SetActive();
                        ws.Range("A11:E11").Row(1).Merge();
                        ws.Cell("I11").SetValue(accno2_crtotal).SetActive(); ws.Cell("I11").Style.NumberFormat.Format = "###0.00";

                        ws.Cell("A13").SetValue("Less: Amount debited in bank but not accounted").SetActive();
                        ws.Range("A13:E13").Row(1).Merge();
                        ws.Cell("I13").SetValue(accno1_drtotal).SetActive(); ws.Cell("I13").Style.NumberFormat.Format = "###0.00";

                        ws.Cell("A15").SetValue("Add: Amount credited in bank but not accounted").SetActive();
                        ws.Range("A15:E15").Row(1).Merge();
                        ws.Cell("I15").SetValue(accno1_crtotal).SetActive(); ws.Cell("I15").Style.NumberFormat.Format = "###0.00";

                        ws.Cell("A17").SetValue("Closing balance as per bank passbook").SetActive();
                        ws.Cell("A17").Style.Font.Bold = true;
                        ws.Range("A17:E17").Row(1).Merge();

                        ws.Cell("I17").SetValue(acc1Bal_amount).SetActive(); ws.Cell("I17").Style.Font.Bold = true; ws.Cell("I17").Style.NumberFormat.Format = "###0.00";
                        ws.Cell("I17").Style.Font.Bold = true; //ws.Cell("F17").Style.Font.Underline = XLFontUnderlineValues.Double;
                        ws.Cell("I17").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        ws.Cell("I17").Style.Border.TopBorderColor = XLColor.Black;
                        ws.Cell("I17").Style.Border.BottomBorder = XLBorderStyleValues.Double;
                        ws.Cell("I17").Style.Border.BottomBorderColor = XLColor.Black;

                        ws.Cell("A19").SetValue("Less: Cheques deposited but not credited in bank").SetActive(); ws.Cell("A19").Style.Font.Bold = true;
                        ws.Cell("A19").Style.Font.Underline = XLFontUnderlineValues.Single;
                        ws.Range("A19:F19").Row(1).Merge();

                        //Inserting  Table1 Data
                        wb.Worksheet(worksheetcount).Cell("A20").InsertTable(Table1);
                        wb.Worksheet(worksheetcount).Table(0).ShowAutoFilter = false;// Disable AutoFilter.
                        wb.Worksheet(worksheetcount).Table(0).Theme = XLTableTheme.None;
                        ws.Range("A20:Q20").Style.Font.Bold = true;
                        ws.Range("A20:Q20").Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);


                        //Inserting  Table1 Total
                        formulaTxt = "=sum(I21:I" + (20 + Table1.Rows.Count).ToString() + ")";
                        row = 20 + Table1.Rows.Count + 1;
                        cellTxt = "I" + row.ToString() + "";
                        ws.Cell(cellTxt).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        ws.Cell(cellTxt).Style.Border.BottomBorderColor = XLColor.Black;

                        var cellFormulaTableSum = ws.Cell(cellTxt);

                        cellFormulaTableSum.FormulaA1 = formulaTxt; cellFormulaTableSum.Style.NumberFormat.Format = "###0.00";

                        //Inserting calculation
                        calcCellTxt = "I7";
                        formulaTxt = "=+(" + calcCellTxt + "*1)-" + cellTxt;

                        row = row + 1;
                        cellTxt = "I" + row.ToString() + "";
                        calcCellTxt = cellTxt;

                        var cellFormulaCalc = ws.Cell(cellTxt);
                        cellFormulaCalc.FormulaA1 = formulaTxt; cellFormulaCalc.Style.NumberFormat.Format = "###0.00";

                        //Inserting  Table2 Data
                        row = row + 2;
                        cellTxt = "A" + row.ToString() + "";

                        ws.Cell(cellTxt).SetValue("Add: Cheques issued but not presented in to bank").SetActive(); ws.Cell(cellTxt).Style.Font.Bold = true;
                        ws.Cell(cellTxt).Style.Font.Underline = XLFontUnderlineValues.Single;
                        cellTxt = "A" + row.ToString() + ":F" + row.ToString();
                        ws.Range(cellTxt).Row(1).Merge();

                        row = row + 1;
                        cellTxt = "A" + row.ToString() + "";

                        wb.Worksheet(worksheetcount).Cell(cellTxt).InsertTable(Table2);
                        wb.Worksheet(worksheetcount).Table(1).ShowAutoFilter = false;// Disable AutoFilter.
                        wb.Worksheet(worksheetcount).Table(1).Theme = XLTableTheme.None;
                        cellTxt1 = "A" + row.ToString() + ":Q" + row.ToString();
                        ws.Range(cellTxt1).Style.Font.Bold = true;
                        ws.Range(cellTxt1).Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);


                        //Inserting  Table2 Total
                        formulaTxt = "=sum(I" + (row + 1).ToString() + ":I" + (row + Table2.Rows.Count).ToString() + ")";
                        row = row + Table2.Rows.Count + 1;
                        cellTxt = "I" + row.ToString() + "";
                        ws.Cell(cellTxt).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        ws.Cell(cellTxt).Style.Border.BottomBorderColor = XLColor.Black;
                        cellFormulaTableSum = ws.Cell(cellTxt);
                        cellFormulaTableSum.FormulaA1 = formulaTxt; cellFormulaTableSum.Style.NumberFormat.Format = "###0.00";

                        formulaTxt = "=+" + calcCellTxt + "+" + cellTxt;

                        row = row + 1;
                        cellTxt = "I" + row.ToString() + "";
                        calcCellTxt = cellTxt;

                        cellFormulaCalc = ws.Cell(cellTxt);
                        cellFormulaCalc.FormulaA1 = formulaTxt; cellFormulaCalc.Style.NumberFormat.Format = "###0.00";

                        row = row + 2;
                        cellTxt = "A" + row.ToString() + "";

                        ws.Cell(cellTxt).SetValue("Less: Amount  debited  in  bank but not accounted").SetActive(); ws.Cell(cellTxt).Style.Font.Bold = true;
                        ws.Cell(cellTxt).Style.Font.Underline = XLFontUnderlineValues.Single;
                        cellTxt = "A" + row.ToString() + ":F" + row.ToString();
                        ws.Range(cellTxt).Row(1).Merge();

                        //Inserting  Table3 Data
                        row = row + 1;
                        cellTxt = "A" + row.ToString() + "";

                        wb.Worksheet(worksheetcount).Cell(cellTxt).InsertTable(Table3);
                        wb.Worksheet(worksheetcount).Table(2).ShowAutoFilter = false;// Disable AutoFilter.
                        wb.Worksheet(worksheetcount).Table(2).Theme = XLTableTheme.None;
                        cellTxt1 = "A" + row.ToString() + ":Q" + row.ToString();
                        ws.Range(cellTxt1).Style.Font.Bold = true;
                        ws.Range(cellTxt1).Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);

                        //Inserting  Table3 Total
                        formulaTxt = "=sum(I" + (row + 1).ToString() + ":I" + (row + Table3.Rows.Count).ToString() + ")";
                        row = row + Table3.Rows.Count + 1;
                        cellTxt = "I" + row.ToString() + "";
                        ws.Cell(cellTxt).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        ws.Cell(cellTxt).Style.Border.BottomBorderColor = XLColor.Black;
                        cellFormulaTableSum = ws.Cell(cellTxt);
                        cellFormulaTableSum.FormulaA1 = formulaTxt; cellFormulaTableSum.Style.NumberFormat.Format = "###0.00";

                        formulaTxt = "=+" + calcCellTxt + "-" + cellTxt;

                        row = row + 1;
                        cellTxt = "I" + row.ToString() + "";
                        calcCellTxt = cellTxt;

                        cellFormulaCalc = ws.Cell(cellTxt);
                        cellFormulaCalc.FormulaA1 = formulaTxt; cellFormulaCalc.Style.NumberFormat.Format = "###0.00";

                        row = row + 2;
                        cellTxt = "A" + row.ToString() + "";

                        ws.Cell(cellTxt).SetValue("Add: Amount credited in bank but not accounted").SetActive(); ws.Cell(cellTxt).Style.Font.Bold = true;
                        ws.Cell(cellTxt).Style.Font.Underline = XLFontUnderlineValues.Single;

                        cellTxt = "A" + row.ToString() + ":F" + row.ToString();
                        ws.Range(cellTxt).Row(1).Merge();

                        row = row + 1;
                        cellTxt = "A" + row.ToString() + "";

                        wb.Worksheet(worksheetcount).Cell(cellTxt).InsertTable(Table4);
                        wb.Worksheet(worksheetcount).Table(3).ShowAutoFilter = false;// Disable AutoFilter.
                        wb.Worksheet(worksheetcount).Table(3).Theme = XLTableTheme.None;
                        cellTxt1 = "A" + row.ToString() + ":Q" + row.ToString();
                        ws.Range(cellTxt1).Style.Font.Bold = true;
                        ws.Range(cellTxt1).Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);

                        //Inserting  Table4 Total
                        formulaTxt = "=sum(I" + (row + 1).ToString() + ":I" + (row + Table4.Rows.Count).ToString() + ")";
                        row = row + Table4.Rows.Count + 1;
                        cellTxt = "I" + row.ToString() + "";
                        ws.Cell(cellTxt).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        ws.Cell(cellTxt).Style.Border.BottomBorderColor = XLColor.Black;
                        cellFormulaTableSum = ws.Cell(cellTxt);
                        cellFormulaTableSum.FormulaA1 = formulaTxt; cellFormulaTableSum.Style.NumberFormat.Format = "###0.00";

                        formulaTxt = "=+" + calcCellTxt + "+" + cellTxt;

                        row = row + 1;
                        cellTxt = "I" + row.ToString() + "";
                        calcCellTxt = cellTxt;

                        cellFormulaCalc = ws.Cell(cellTxt);
                        cellFormulaCalc.FormulaA1 = formulaTxt; cellFormulaCalc.Style.NumberFormat.Format = "###0.00";
                        cellFormulaCalc.Style.Font.Bold = true;
                        // cellFormulaCalc.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        // cellFormulaCalc.Style.Border.TopBorderColor = XLColor.Black;
                        //cellFormulaCalc.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        //cellFormulaCalc.Style.Border.BottomBorderColor = XLColor.Black;
                        //cellFormulaCalc.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                        //Total Balance & Difference
                        cellTxt = "B" + row.ToString() + "";
                        ws.Cell(cellTxt).SetValue("Balance as per bank pass book").SetActive(); ws.Cell(cellTxt).Style.Font.Bold = true;

                        //Hema edit

                        row = row + 1;
                        cellTxt = "B" + row.ToString() + "";
                        ws.Cell(cellTxt).SetValue("Bank Statement Month Closing Balance").SetActive(); ws.Cell(cellTxt).Style.Font.Bold = true;

                        formulaTxt = "I17";

                        cellTxt = "I" + row.ToString() + "";

                        cellFormulaCalc = ws.Cell(cellTxt);
                        cellFormulaCalc.FormulaA1 = formulaTxt; cellFormulaCalc.Style.NumberFormat.Format = "###0.00";
                        cellFormulaCalc.Style.Font.Bold = true;
                        cellFormulaCalc.Style.Border.BottomBorder = XLBorderStyleValues.Double;
                        cellFormulaCalc.Style.Border.BottomBorderColor = XLColor.Black;
                        cellFormulaCalc.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                        // Edit ends



                        row = row + 2;
                        cellTxt = "B" + row.ToString() + "";
                        ws.Cell(cellTxt).SetValue("Difference").SetActive(); ws.Cell(cellTxt).Style.Font.Bold = true;

                        //Hema Edit start
                        //formulaTxt = "=+" + calcCellTxt + "-I17";

                        formulaTxt = "=+" + calcCellTxt + "-I" + Convert.ToString((Convert.ToInt32(calcCellTxt.Split('I')[1]) + 1));

                        //Hema Edit ends

                        

                        cellTxt = "I" + row.ToString() + "";

                        cellFormulaCalc = ws.Cell(cellTxt);
                        cellFormulaCalc.FormulaA1 = formulaTxt; cellFormulaCalc.Style.NumberFormat.Format = "###0.00";
                        cellFormulaCalc.Style.Font.Bold = true;
                        cellFormulaCalc.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        cellFormulaCalc.Style.Border.TopBorderColor = XLColor.Black;
                        cellFormulaCalc.Style.Border.BottomBorder = XLBorderStyleValues.Double;
                        cellFormulaCalc.Style.Border.BottomBorderColor = XLColor.Black;
                        cellFormulaCalc.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                        ws.Columns("A").AdjustToContents();
                        ws.Columns("F").AdjustToContents();
                      
                        //Insert Transaction
                        //ws = wb.AddWorksheet("Transaction" + filename_tran);
                        //ws.Cell("A1").InsertTable(Table6).Theme = XLTableTheme.None;
                        //worksheetcount = worksheetcount+1;
                        //ws.Tables.ForEach(x => x.ShowAutoFilter = false);
                        //ws.Cell("A1").Style.Font.Bold = true;
                        //ws.Range("A1:Z1").Style.Font.Bold = true;
                        //ws.Range("A1:Z1").Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);
                        //ws.Table(6).Theme = XLTableTheme.None;

                           if (Table7.Rows.Count > 0)
                           {
                               int fileid = Convert.ToInt32(Table7.Rows[0]["File Id"]);
                               //Insert Supporting File
                               if(fileid!=0)
                               {
                                   ws = wb.AddWorksheet("SupportingFile" + filename_tran);
                                   ws.Cell("A1").InsertTable(Table7).Theme = XLTableTheme.None;
                                   worksheetcount = worksheetcount + 1;
                                   ws.Tables.ForEach(x => x.ShowAutoFilter = false);
                                   ws.Range("A1:Z1").Style.Font.Bold = true;
                                   ws.Range("A1:Z1").Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);
                               }
                              
                               // ws.Table(7).Theme = XLTableTheme.None;
                           }


                    }
                }
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                 
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }
               
                

            }
            catch (Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
            return View();

        }

        public ActionResult DailyBRS_Download(string TranDate, int Recon_gid)
        {
            try
            {    
                List<TransactionRpt_model> objcat_3st = new List<TransactionRpt_model>();
                DataSet result2 = new DataSet();
              
                DataSet dt = new DataSet();

                DataTable Table = new DataTable();
                DataTable Table1 = new DataTable();
                DataTable Table2 = new DataTable();
                DataTable Table3 = new DataTable();
                DataTable Table4 = new DataTable();
                DataTable Table6 = new DataTable();
                DataTable Table7 = new DataTable();

             
              

               TransactionRpt_model DedupList = new TransactionRpt_model();
               DedupList.Trandate = TranDate;
               DedupList.Recongid = Recon_gid;
               string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(DedupList), "ReportMonthbrs_Download");
               result2 = (DataSet)JsonConvert.DeserializeObject(post_data, result2.GetType());


                 TransactionRpt_model objcat3 = new TransactionRpt_model();

                 Table = result2.Tables[0].Copy();
                 Table1 = result2.Tables[1].Copy();
                 Table2 = result2.Tables[2].Copy();
                 Table3 = result2.Tables[3].Copy();
                 Table4 = result2.Tables[4].Copy();
                 Table6 = result2.Tables[6].Copy();
                 Table7 = result2.Tables[7].Copy();

                
                long row = 0;
                long col = 0;
                string cellTxt = "";
                string formulaTxt = "";
                string calcCellTxt = "";
                var recon_name1 = "";

                var entity_name = Table.Rows[0]["entity"].ToString();
                var recon_name = Table.Rows[0]["recon_name"].ToString();
                int i = recon_name.Length;

                if (i >= 30)
                {
                    recon_name1 = recon_name.Substring(0, 30);

                }
                else
                {
                    recon_name1 = recon_name;
                }
                var acc_no1 = Table.Rows[0]["acc_no1"].ToString();
                var acc_no2 = Table.Rows[0]["acc_no2"].ToString();
                string Acc1date = Table.Rows[0]["acc_no1_bal_date"].ToString();
                string Acc2date = Table.Rows[0]["acc_no2_bal_date"].ToString();
                Decimal acc1Bal_amount = Convert.ToDecimal(Table.Rows[0]["acc_no1_bal_amount"].ToString());
                Decimal acc2Bal_amount = Convert.ToDecimal(Table.Rows[0]["acc_no2_bal_amount"].ToString());
                Decimal accno1_drtotal = Convert.ToDecimal(Table.Rows[0]["acc_no1_dr_total"].ToString());
                Decimal accno2_drtotal = Convert.ToDecimal(Table.Rows[0]["acc_no2_dr_total"].ToString());
                Decimal accno1_crtotal = Convert.ToDecimal(Table.Rows[0]["acc_no1_cr_total"].ToString());
                Decimal accno2_crtotal = Convert.ToDecimal(Table.Rows[0]["acc_no2_cr_total"].ToString());

                

                dt = result2.Copy();
                dt.Tables.Remove("Table");

                string fileName = acc_no1 + ".xlsx";
                         

                using (XLWorkbook wb = new XLWorkbook())// wb.Worksheets.Add(dt);
                {
                    ///Setting Table Data static & Dynamic;
                    var ws = wb.AddWorksheet(recon_name1);

                    ws.FirstCell().SetValue(entity_name); ws.Cell("A1").Style.Font.Bold = true;
                    ws.Cell("A1").Style.Font.Underline.ToString(); ws.Cell("A1").Style.Font.Underline = XLFontUnderlineValues.Single;

                    ws.FirstCell().CellBelow().SetValue(recon_name);
                    ws.Cell("A2").Style.Font.Bold = true;

                    ws.Cell("A3").SetValue("GL CODE").SetActive(); ws.Cell("A3").Style.Font.Bold = true;
                    ws.Cell("B3").SetValue(acc_no2).SetActive(); ws.Cell("B3").Style.Font.Bold = true;

                    ws.Cell("A4").SetValue("CC A/c No.").SetActive(); ws.Cell("A4").Style.Font.Bold = true;

                    ws.Cell("B4").SetValue(acc_no1).SetActive(); ws.Cell("B4").Style.Font.Bold = true;

                    ws.Cell("A5").SetValue("Bank Reconciliation Statement as on date " + Acc1date + "").SetActive();
                    ws.Cell("A5").Style.Font.Bold = true;

            

                    ws.Cell("A7").SetValue("S U M M A R Y").SetActive(); ws.Cell("A7").Style.Font.Bold = true;
                    ws.Cell("A7").Style.Font.Underline = XLFontUnderlineValues.Single;
                    ws.Cell("A7").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    ws.Cell("A8").SetValue("Closing Balance  as per our Bank Book as on " + Acc2date + "").SetActive();
                    ws.Cell("A8").Style.Font.Bold = true;
                    ws.Cell("B8").SetValue(acc2Bal_amount).SetActive(); ws.Cell("B8").Style.Font.Bold = true; ws.Cell("B8").Style.NumberFormat.Format = "###0.00";

                    ws.Cell("A10").SetValue("Less: Cheques Deposited but not credited in bank passbook").SetActive();
                    //ws.Range("A9:E9").Row(1).Merge();
                    ws.Cell("B10").SetValue(accno2_drtotal).SetActive(); ws.Cell("B10").Style.Font.Bold = true; ws.Cell("B10").Style.NumberFormat.Format = "###0.00";

                    ws.Cell("A12").SetValue("Add: Cheques issued but not presented in the bank").SetActive();
                  //  ws.Range("A11:E11").Row(1).Merge();
                    ws.Cell("B12").SetValue(accno2_crtotal).SetActive(); ws.Cell("B12").Style.Font.Bold = true; ws.Cell("B12").Style.NumberFormat.Format = "###0.00";

                    ws.Cell("A14").SetValue("Less: Amount debited in bank but not accounted").SetActive();
                    //ws.Range("A13:E13").Row(1).Merge();
                    ws.Cell("B14").SetValue(accno1_drtotal).SetActive(); ws.Cell("B14").Style.Font.Bold = true; ws.Cell("B14").Style.NumberFormat.Format = "###0.00";

                    ws.Cell("A16").SetValue("Add: Amount credited in bank but not accounted").SetActive();
                  //  ws.Range("A15:E15").Row(1).Merge();
                    ws.Cell("B16").SetValue(accno1_crtotal).SetActive(); ws.Cell("B16").Style.Font.Bold = true; ws.Cell("B16").Style.NumberFormat.Format = "###0.00";

                    ws.Cell("A18").SetValue("Closing Balance as per Bank passbook").SetActive();
                    ws.Cell("A18").Style.Font.Bold = true;
                    //ws.Range("A17:E17").Row(1).Merge();

                    ws.Cell("B18").SetValue(acc1Bal_amount).SetActive(); ws.Cell("B18").Style.Font.Bold = true; ws.Cell("B18").Style.NumberFormat.Format = "###0.00";
                    ws.Cell("B18").Style.Font.Bold = true; //ws.Cell("F17").Style.Font.Underline = XLFontUnderlineValues.Double;
                    ws.Cell("B18").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    ws.Cell("B18").Style.Border.TopBorderColor = XLColor.Black;
                    ws.Cell("B18").Style.Border.BottomBorder = XLBorderStyleValues.Double;
                    ws.Cell("B18").Style.Border.BottomBorderColor = XLColor.Black;

                    //Inserting  Table1 Data
                    ws = wb.AddWorksheet("WD");
                    ws.Cell("A1").InsertTable(Table1);
            

                    //Inserting  Table1 Total
                   // formulaTxt = "=sum(E2:E" +(Table1.Rows.Count+1).ToString() + ")";
                   // row = Table1.Rows.Count + 2;
                   // cellTxt = "F" + row.ToString() + "";

                   // var cellFormulaTableSum = ws.Cell(cellTxt);

                   // cellFormulaTableSum.FormulaA1 = formulaTxt; cellFormulaTableSum.Style.NumberFormat.Format = "###0.00";
                   //// var table1total = cellFormulaTableSum.Value;

                   // //Inserting calculation
                   // calcCellTxt = "F7";
                   // formulaTxt = "=+(" + calcCellTxt + "*-1)-" + cellTxt;

                   // row = row + 1;
                   // cellTxt = "F" + row.ToString() + "";
                   // calcCellTxt = cellTxt;

                   // var cellFormulaCalc = ws.Cell(cellTxt);
                   // cellFormulaCalc.FormulaA1 = formulaTxt; cellFormulaCalc.Style.NumberFormat.Format = "###0.00";
                   // var table1total = cellFormulaCalc.Value;

                    //Inserting  Table2 Data
                    ws = wb.AddWorksheet("WC");
                    ws.Cell("A1").InsertTable(Table2);

                    //Inserting Table2 Total
                    //formulaTxt = "=sum(E2:E" + (Table2.Rows.Count + 1).ToString() + ")";
                    //row = Table2.Rows.Count + 2;
                    //cellTxt = "F" + row.ToString() + "";

                    //var cellFormulaTableSum1 = ws.Cell(cellTxt);

                    //cellFormulaTableSum1.FormulaA1 = formulaTxt; cellFormulaTableSum1.Style.NumberFormat.Format = "###0.00";

                    
                    //var table2total = cellFormulaTableSum1.Value;

                    //Decimal table1_total =Convert.ToDecimal(table1total);
                    //Decimal table2_total = Convert.ToDecimal(table2total);
                    //Decimal diff = table1_total + table2_total;

                    //row = row + 1;
                    //cellTxt = "F" + row.ToString() + "";
                    //calcCellTxt = cellTxt;

                    //ws.Cell(calcCellTxt).SetValue(diff).SetActive(); ws.Cell(calcCellTxt).Style.NumberFormat.Format = "###0.00";

                    //Inserting  Table3 Data
                    ws = wb.AddWorksheet("TD");
                    ws.Cell("A1").InsertTable(Table3);

                    //Inserting Table3 Total
                    //formulaTxt = "=sum(E2:E" + (Table3.Rows.Count + 1).ToString() + ")";
                    //row = Table3.Rows.Count + 2;
                    //cellTxt = "F" + row.ToString() + "";

                    //var cellFormulaTableSum2 = ws.Cell(cellTxt);

                    //cellFormulaTableSum2.FormulaA1 = formulaTxt; cellFormulaTableSum2.Style.NumberFormat.Format = "###0.00";

                   
                    //var table3total = cellFormulaTableSum2.Value;

                    //Decimal table3_total = Convert.ToDecimal(table3total);
                    //Decimal table2tt1 = diff;
                    //Decimal diff1 = table2tt1 - table3_total;

                    //row = row + 1;
                    //cellTxt = "F" + row.ToString() + "";
                    //calcCellTxt = cellTxt;

                    //ws.Cell(calcCellTxt).SetValue(diff1).SetActive(); ws.Cell(calcCellTxt).Style.NumberFormat.Format = "###0.00";

                    //Inserting  Table4 Data
                    ws = wb.AddWorksheet("TC");
                    ws.Cell("A1").InsertTable(Table4);

                    //Inserting Table4 Total
                    //formulaTxt = "=sum(E2:E" + (Table4.Rows.Count + 1).ToString() + ")";
                    //row = Table4.Rows.Count + 2;
                    //cellTxt = "F" + row.ToString() + "";

                    //var cellFormulaTableSum3 = ws.Cell(cellTxt);

                    //cellFormulaTableSum3.FormulaA1 = formulaTxt; cellFormulaTableSum3.Style.NumberFormat.Format = "###0.00";

                  
                    //var table4total = cellFormulaTableSum3.Value;

                    //Decimal table4_total = Convert.ToDecimal(table4total);
                    //Decimal table2tt2 = diff1;
                    //Decimal diff2 = table2tt2 + table4_total;

                    //row = row + 1;
                    //cellTxt = "F" + row.ToString() + "";
                    //calcCellTxt = cellTxt;

                   // ws.Cell(calcCellTxt).SetValue(diff2).SetActive(); ws.Cell(calcCellTxt).Style.NumberFormat.Format = "###0.00";
                   // ws.Cell(calcCellTxt).Style.Font.Bold = true;

                   // cellTxt = "B" + row.ToString() + "";
                   // calcCellTxt = cellTxt;
                   // ws.Cell(calcCellTxt).SetValue("BALANCE AS PER BANK PASS BOOK").SetActive(); ws.Cell(calcCellTxt).Style.Font.Bold = true;

                   // cellTxt = "F" + row.ToString() + "";
                   // calcCellTxt = cellTxt;
                   // cellFormulaCalc = ws.Cell(calcCellTxt);
                   //// cellFormulaCalc.FormulaA1 = formulaTxt; cellFormulaCalc.Style.NumberFormat.Format = "###0.00";
                   // cellFormulaCalc.Style.Font.Bold = true;
                   // cellFormulaCalc.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                   // cellFormulaCalc.Style.Border.TopBorderColor = XLColor.Black;
                   // cellFormulaCalc.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                   // cellFormulaCalc.Style.Border.BottomBorderColor = XLColor.Black;
                   // cellFormulaCalc.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                   // row = row + 2;
                   // cellTxt = "B" + row.ToString() + "";
                   // calcCellTxt = cellTxt;
                   // ws.Cell(cellTxt).SetValue("Difference").SetActive(); ws.Cell(cellTxt).Style.Font.Bold = true;

                   // cellTxt = "F" + row.ToString() + "";
                   // calcCellTxt = cellTxt;
                   // ///calculating Differences;
                   // Decimal difference = diff2 -acc1Bal_amount;
                   // ws.Cell(calcCellTxt).SetValue(difference).SetActive(); ws.Cell(cellTxt).Style.Font.Bold = true;
                   // cellFormulaCalc = ws.Cell(calcCellTxt);
                   // // cellFormulaCalc.FormulaA1 = formulaTxt; cellFormulaCalc.Style.NumberFormat.Format = "###0.00";
                   // cellFormulaCalc.Style.Font.Bold = true;
                   // cellFormulaCalc.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                   // cellFormulaCalc.Style.Border.TopBorderColor = XLColor.Black;
                   // cellFormulaCalc.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                   // cellFormulaCalc.Style.Border.BottomBorderColor = XLColor.Black;
                   // cellFormulaCalc.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                    //Inserting  Table6 Data
                    ws = wb.AddWorksheet("Transaction");
                    ws.Cell("A1").InsertTable(Table6);

                    //Inserting  Table7 Data
                    if (Table7.Rows.Count > 0)
                    {
                        //Insert Supporting File
                        ws = wb.AddWorksheet("SupportingFile");
                        ws.Cell("A1").InsertTable(Table7);
                    }
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                    }
                }

            }
            catch (Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

            return View();
        }
        public ActionResult DailyBRS_Download_1(string TranDate, string Recon_gid)
        {
            try
            {
                List<TransactionRpt_model> objcat_3st = new List<TransactionRpt_model>();
                DataSet result2 = new DataSet();
                DataSet ds = new DataSet();
                DataSet Aging_dataset = new DataSet();

                DataSet dt = new DataSet();

                DataTable Table = new DataTable();
                DataTable Table1 = new DataTable();
                DataTable Table2 = new DataTable();
                DataTable Table3 = new DataTable();
                DataTable Table4 = new DataTable();
                DataTable Table6 = new DataTable();
                DataTable Table7 = new DataTable();

                DataTable Table_aging1 = new DataTable();
                DataTable Table_aging2 = new DataTable();
                DataTable Table_aging3 = new DataTable();
                DataTable Table_aging4 = new DataTable();
                DataTable dummy = new DataTable();

                List<int> recons = Recon_gid.Split(',').Select(int.Parse).ToList();
                for (int i = 0; i < recons.Count; i++)
                {
                    if (recons[i] == 0)
                    {
                        recons.RemoveAt(i);
                        i--;
                    }
                }

                int recon_count = recons.Count();
                Recon_gid = string.Join(", ", recons);
                TransactionRpt_model DedupList = new TransactionRpt_model();
                DedupList.Trandate = TranDate;
                DedupList.recondaily_gid = Recon_gid;
                DedupList.no_of_recons = recon_count;
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(DedupList), "ReportMonthbrs_Download_daily");
                result2 = (DataSet)JsonConvert.DeserializeObject(post_data, result2.GetType());

                //getting Aging dataset 
                int datasetcount = result2.Tables.Count - 1;

                for (int k = 4; k >0;k --)
                {
                    dummy = result2.Tables[datasetcount].Copy();
                    Aging_dataset.Tables.Add(dummy);
                   
                    Table7 = result2.Tables[datasetcount];
                    result2.Tables.Remove(Table7);
                    datasetcount = datasetcount - 1;

                }




                Table = result2.Tables[0].Copy();
                Table1 = result2.Tables[1].Copy();
                Table2 = result2.Tables[2].Copy();
                Table3 = result2.Tables[3].Copy();
                Table4 = result2.Tables[4].Copy();
                Table_aging1 = Aging_dataset.Tables[3].Copy();
                Table_aging2 = Aging_dataset.Tables[2].Copy();
                Table_aging3 = Aging_dataset.Tables[1].Copy();
                Table_aging4 = Aging_dataset.Tables[0].Copy();



                long row = 0;
                long col = 0;
                string cellTxt = "";
                string cellTxt1 = "";
                string formulaTxt = "";
                string calcCellTxt = "";
                var recon_name1 = "";
                string Reconnamevalue = "";

                Reconnamevalue = Table.Rows[0]["recon_name"].ToString();
                string fileName ="AllReconsDaily_Brs_Summary.xlsx";
                string entiyname = Table.Rows[0]["entity_name"].ToString();
                string name = entiyname.Trim('\'');
                string date_=Table.Rows[0]["tran_date"].ToString();
                string fname = "All Bank Daily Brs Consolidated Queries as on " +date_;
               

                using (XLWorkbook wb = new XLWorkbook())// wb.Worksheets.Add(dt);
                {
                    ///Setting Table Data static & Dynamic;
                    var ws = wb.AddWorksheet("Allrecons");

                    //Inserting  Table1 Data

                    ws.Cell("A1").SetValue(name).SetActive();
                    ws.Cell("A1").Style.Font.Underline = XLFontUnderlineValues.Single;
                    ws.Cell("A1").Style.Font.Bold = true;

                    ws.Cell("A2").SetValue(fname).SetActive();
                    ws.Cell("A2").Style.Font.Bold = true;

                    ws.Cell("A4").SetValue("Cheques deposited but not credited in bank").SetActive();
                    ws.Cell("A4").Style.Font.Underline = XLFontUnderlineValues.Double;
                    ws.Cell("A4").Style.Font.Bold = true;
                    ws.Cell("A5").InsertTable(Table1);
                  
                    
                    ws.Table(0).ShowAutoFilter = false;
                    ws.Table(0).Theme = XLTableTheme.None;
                    cellTxt1 = "A5:Q5";
                    ws.Range(cellTxt1).Style.Font.Bold = true;
                    ws.Range(cellTxt1).Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);
                    row = Table1.Rows.Count+ 8;
                    cellTxt = "A" + row.ToString() + "";


                    //Inserting Table2 Data
                    ws.Cell(cellTxt).SetValue("Cheques issued but not presented in to bank").SetActive();
                    ws.Cell(cellTxt).Style.Font.Underline = XLFontUnderlineValues.Double;
                    ws.Cell(cellTxt).Style.Font.Bold = true;
                    row = row+ 1;
                    cellTxt = "A" + row.ToString() + "";

                    ws.Cell(cellTxt).InsertTable(Table2);
                    //wb.Worksheet(1).Table(1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    //wb.Worksheet(1).Table(1).Style.Border.BottomBorderColor = XLColor.Black;
                    //wb.Worksheet(1).Table(1).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    //wb.Worksheet(1).Table(1).Style.Border.TopBorderColor = XLColor.Black;
                    //wb.Worksheet(1).Table(1).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    //wb.Worksheet(1).Table(1).Style.Border.LeftBorderColor = XLColor.Black;
                    //wb.Worksheet(1).Table(1).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    //wb.Worksheet(1).Table(1).Style.Border.RightBorderColor = XLColor.Black;
                    ws.Table(1).ShowAutoFilter = false;
                    ws.Table(1).Theme = XLTableTheme.None;
                    cellTxt1 = "A" + row.ToString() + ":Q" + row.ToString();
                    ws.Range(cellTxt1).Style.Font.Bold = true;
                    ws.Range(cellTxt1).Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);
  
                    row = row+Table2.Rows.Count + 3;
                    cellTxt = "A" + row.ToString() + "";

                    //Inserting Table3 Data
                    ws.Cell(cellTxt).SetValue("Amount debited in bank but not accounted").SetActive();
                    ws.Cell(cellTxt).Style.Font.Underline = XLFontUnderlineValues.Double;
                    ws.Cell(cellTxt).Style.Font.Bold = true;
                    row = row + 1;
                    cellTxt = "A" + row.ToString() + "";

                    ws.Cell(cellTxt).InsertTable(Table3);
                    //wb.Worksheet(1).Table(2).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    //wb.Worksheet(1).Table(2).Style.Border.BottomBorderColor = XLColor.Black;
                    //wb.Worksheet(1).Table(2).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    //wb.Worksheet(1).Table(2).Style.Border.TopBorderColor = XLColor.Black;
                    //wb.Worksheet(1).Table(2).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    //wb.Worksheet(1).Table(2).Style.Border.LeftBorderColor = XLColor.Black;
                    //wb.Worksheet(1).Table(2).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    //wb.Worksheet(1).Table(2).Style.Border.RightBorderColor = XLColor.Black;
                    ws.Table(2).ShowAutoFilter = false;
                    ws.Table(2).Theme = XLTableTheme.None;
                    cellTxt1 = "A" + row.ToString() + ":Q" + row.ToString();
                    ws.Range(cellTxt1).Style.Font.Bold = true;
                    ws.Range(cellTxt1).Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);

                    row = row + Table3.Rows.Count + 3;
                    cellTxt = "A" + row.ToString() + "";

                    //Inserting Table4 Data
                    ws.Cell(cellTxt).SetValue("Amount credited in bank but not accounted").SetActive();
                    ws.Cell(cellTxt).Style.Font.Underline = XLFontUnderlineValues.Double;
                    ws.Cell(cellTxt).Style.Font.Bold = true;
                    row = row + 1;
                    cellTxt = "A" + row.ToString() + "";

                    ws.Cell(cellTxt).InsertTable(Table4);
                    //wb.Worksheet(1).Table(3).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    //wb.Worksheet(1).Table(3).Style.Border.BottomBorderColor = XLColor.Black;
                    //wb.Worksheet(1).Table(3).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    //wb.Worksheet(1).Table(3).Style.Border.TopBorderColor = XLColor.Black;
                    //wb.Worksheet(1).Table(3).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    //wb.Worksheet(1).Table(3).Style.Border.LeftBorderColor = XLColor.Black;
                    //wb.Worksheet(1).Table(3).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    //wb.Worksheet(1).Table(3).Style.Border.RightBorderColor = XLColor.Black;
                    ws.Table(3).ShowAutoFilter = false;
                    ws.Table(3).Theme = XLTableTheme.None;
                    cellTxt1 = "A" + row.ToString() + ":Q" + row.ToString();
                    ws.Range(cellTxt1).Style.Font.Bold = true;
                    ws.Range(cellTxt1).Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);


                    List<string> reconname = new List<string>(Reconnamevalue.Split(','));
                    int namecount=reconname.Count;

                    //Inserting Supporting File
                    int dataset_count = result2.Tables.Count;
                    for (int i = 5; i < dataset_count; i++)
                    {

                        if (result2.Tables[i].Rows.Count > 0)
                        {
                            int fileid = Convert.ToInt32(result2.Tables[i].Rows[0]["File Id"]);

                            if (fileid != 0)
                            {

                                ws = wb.AddWorksheet("SupportingFile" + i);
                                ws.Cell("A1").InsertTable(result2.Tables[i]);
                                ws.Table(0).ShowAutoFilter = false;
                                ws.Table(0).Theme = XLTableTheme.None;
                                cellTxt1 = "A1:Z1";
                                ws.Range(cellTxt1).Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);
                            }
                        }
                    }
                    //Inserting Aging Table1
                    ws = wb.AddWorksheet("Aging");
                    ws.Cell("A1").SetValue("Cheques deposited but not credited in bank").SetActive();
                    ws.Cell("A1").Style.Font.Bold = true;
                    ws.Cell("A2").InsertTable(Table_aging1);
                    wb.Worksheet("Aging").Table(0).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    wb.Worksheet("Aging").Table(0).Style.Border.BottomBorderColor = XLColor.Black;
                    wb.Worksheet("Aging").Table(0).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    wb.Worksheet("Aging").Table(0).Style.Border.TopBorderColor = XLColor.Black;
                    wb.Worksheet("Aging").Table(0).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    wb.Worksheet("Aging").Table(0).Style.Border.LeftBorderColor = XLColor.Black;
                    wb.Worksheet("Aging").Table(0).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    wb.Worksheet("Aging").Table(0).Style.Border.RightBorderColor = XLColor.Black;
                    ws.Table(0).ShowAutoFilter = false;
                    ws.Table(0).Theme = XLTableTheme.None;
                    ws.Range("A2:S2").Style.Font.Bold = true;
                    ws.Range("A2:D2").Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);
                    ws.Range("A1:S1").Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);
                    ws.Range("A10:S10").Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);

                    ws.Range("A10:S10").Style.Border.BottomBorder = XLBorderStyleValues.Thin; ws.Range("A10:S10").Style.Border.BottomBorderColor = XLColor.Black;
                    ws.Range("A10:S10").Style.Border.TopBorder = XLBorderStyleValues.Thin; ws.Range("A10:S10").Style.Border.TopBorderColor = XLColor.Black;
                    ws.Range("A10:S10").Style.Border.LeftBorder = XLBorderStyleValues.Thin; ws.Range("A10:S10").Style.Border.LeftBorderColor = XLColor.Black;
                    ws.Range("A10:S10").Style.Border.RightBorder = XLBorderStyleValues.Thin; ws.Range("A10:S10").Style.Border.RightBorderColor = XLColor.Black;

                    //calculate Loan & Nonloan Amount
                    ws.Cell("A10").SetValue("Total").SetActive(); ws.Cell("A10").Style.Font.Bold = true;
                    var sumloan = ws.Evaluate("SUM(B3:B9)");
                    ws.Cell("B10").SetValue(sumloan).SetActive(); ws.Cell("B10").Style.Font.Bold = true;
                    ws.Cell("B15").SetValue(sumloan).SetActive();
                    var sumnonloan = ws.Evaluate("SUM(C3:C9)");
                    ws.Cell("C10").SetValue(sumnonloan).SetActive(); ws.Cell("C10").Style.Font.Bold = true;
                    ws.Cell("B16").SetValue(sumnonloan).SetActive();
                    var Treasury= ws.Evaluate("SUM(D3:D9)");
                    ws.Cell("B17").SetValue(Treasury).SetActive();
                    ws.Cell("D10").SetValue(Treasury).SetActive(); ws.Cell("D10").Style.Font.Bold = true;
                 

                    //Inserting Aging Table2
                    ws.Cell("F1").SetValue("Cheques issued but not presented in to bank").SetActive();
                    ws.Cell("F1").Style.Font.Bold = true;
                    ws.Cell("F2").InsertTable(Table_aging2);
                    wb.Worksheet("Aging").Table(1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    wb.Worksheet("Aging").Table(1).Style.Border.BottomBorderColor = XLColor.Black;
                    wb.Worksheet("Aging").Table(1).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    wb.Worksheet("Aging").Table(1).Style.Border.TopBorderColor = XLColor.Black;
                    wb.Worksheet("Aging").Table(1).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    wb.Worksheet("Aging").Table(1).Style.Border.LeftBorderColor = XLColor.Black;
                    wb.Worksheet("Aging").Table(1).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    wb.Worksheet("Aging").Table(1).Style.Border.RightBorderColor = XLColor.Black;
                    ws.Table(1).ShowAutoFilter = false;
                    ws.Table(1).Theme = XLTableTheme.None;
                    ws.Range("F2:I2").Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);
              

                    //calculate Loan & Nonloan Amount
                    sumloan = ws.Evaluate("SUM(G3:G9)");
                    ws.Cell("C15").SetValue(sumloan).SetActive();
                    ws.Cell("G10").SetValue(sumloan).SetActive(); ws.Cell("G10").Style.Font.Bold = true;
                    sumnonloan = ws.Evaluate("SUM(H3:H9)");
                    ws.Cell("C16").SetValue(sumnonloan).SetActive();
                    ws.Cell("H10").SetValue(sumnonloan).SetActive(); ws.Cell("H10").Style.Font.Bold = true;
                    Treasury = ws.Evaluate("SUM(I3:I9)");
                    ws.Cell("C17").SetValue(Treasury).SetActive();
                    ws.Cell("I10").SetValue(Treasury).SetActive(); ws.Cell("I10").Style.Font.Bold = true;
                    ws.Cell("F10").SetValue("Total").SetActive(); ws.Cell("F10").Style.Font.Bold = true;

                    //Inserting Aging Table3
                    ws.Cell("K1").SetValue("Amount debited in bank but not accounted").SetActive();
                    ws.Cell("K1").Style.Font.Bold = true;
                    ws.Cell("K2").InsertTable(Table_aging3);
                    wb.Worksheet("Aging").Table(2).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    wb.Worksheet("Aging").Table(2).Style.Border.BottomBorderColor = XLColor.Black;
                    wb.Worksheet("Aging").Table(2).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    wb.Worksheet("Aging").Table(2).Style.Border.TopBorderColor = XLColor.Black;
                    wb.Worksheet("Aging").Table(2).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    wb.Worksheet("Aging").Table(2).Style.Border.LeftBorderColor = XLColor.Black;
                    wb.Worksheet("Aging").Table(2).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    wb.Worksheet("Aging").Table(2).Style.Border.RightBorderColor = XLColor.Black;
                    ws.Table(2).ShowAutoFilter = false;
                    ws.Table(2).Theme = XLTableTheme.None;
                    ws.Range("K2:N2").Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);

                    //calculate Loan & Nonloan Amount
                    sumloan = ws.Evaluate("SUM(L3:L9)");
                    ws.Cell("D15").SetValue(sumloan).SetActive();
                    ws.Cell("L10").SetValue(sumloan).SetActive(); ws.Cell("L10").Style.Font.Bold = true;
                    sumnonloan = ws.Evaluate("SUM(M3:M9)");
                    ws.Cell("M10").SetValue(sumnonloan).SetActive(); ws.Cell("M10").Style.Font.Bold = true;
                    ws.Cell("D16").SetValue(sumnonloan).SetActive();
                    Treasury = ws.Evaluate("SUM(N3:N9)");
                    ws.Cell("D17").SetValue(Treasury).SetActive();
                    ws.Cell("N10").SetValue(Treasury).SetActive(); ws.Cell("N10").Style.Font.Bold = true;
                    ws.Cell("K10").SetValue("Total").SetActive(); ws.Cell("K10").Style.Font.Bold = true;

                    //Inserting Aging Table4
                    ws.Cell("P1").SetValue("Amount credited in bank but not accounted").SetActive();
                    ws.Cell("P1").Style.Font.Bold = true;
                    ws.Cell("P2").InsertTable(Table_aging4);
                    wb.Worksheet("Aging").Table(3).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    wb.Worksheet("Aging").Table(3).Style.Border.BottomBorderColor = XLColor.Black;
                    wb.Worksheet("Aging").Table(3).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    wb.Worksheet("Aging").Table(3).Style.Border.TopBorderColor = XLColor.Black;
                    wb.Worksheet("Aging").Table(3).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    wb.Worksheet("Aging").Table(3).Style.Border.LeftBorderColor = XLColor.Black;
                    wb.Worksheet("Aging").Table(3).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    wb.Worksheet("Aging").Table(3).Style.Border.RightBorderColor = XLColor.Black;
                    ws.Table(3).ShowAutoFilter = false;
                    ws.Table(3).Theme = XLTableTheme.None;
                    ws.Range("P2:S2").Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);

                    //calculate Loan & Nonloan Amount
                    sumloan = ws.Evaluate("SUM(Q3:Q9)");
                    ws.Cell("E15").SetValue(sumloan).SetActive();
                    ws.Cell("Q10").SetValue(sumloan).SetActive(); ws.Cell("Q10").Style.Font.Bold = true;
                    sumnonloan = ws.Evaluate("SUM(R3:R9)");
                    ws.Cell("E16").SetValue(sumnonloan).SetActive();
                    ws.Cell("R10").SetValue(sumnonloan).SetActive(); ws.Cell("R10").Style.Font.Bold = true;
                    Treasury = ws.Evaluate("SUM(S3:S9)");
                    ws.Cell("S10").SetValue(Treasury).SetActive(); ws.Cell("S10").Style.Font.Bold = true;
                    ws.Cell("E17").SetValue(Treasury).SetActive();
                    ws.Cell("P10").SetValue("Total").SetActive(); ws.Cell("P10").Style.Font.Bold = true;


                    //Grand Total
                    ws.Cell("A14").SetValue("Type").SetActive(); ws.Cell("A14").Style.Font.Bold = true;
                    ws.Cell("B14").SetValue("Cheques deposited but not credited in bank").SetActive(); ws.Cell("B14").Style.Font.Bold = true;
                    ws.Cell("C14").SetValue("Cheques issued but not presented in to bank").SetActive(); ws.Cell("C14").Style.Font.Bold = true;
                    ws.Cell("D14").SetValue("Amount debited in bank but not accounted").SetActive(); ws.Cell("D14").Style.Font.Bold = true;
                    ws.Cell("E14").SetValue("Amount credited in bank but not accounted").SetActive(); ws.Cell("E14").Style.Font.Bold = true;

                    ws.Cell("A15").SetValue("Loan").SetActive(); ws.Cell("A15").Style.Font.Bold = true;
                    ws.Range("A15:E15").Style.Border.BottomBorder = XLBorderStyleValues.Thin; ws.Range("A15:E15").Style.Border.BottomBorderColor = XLColor.Black;
                    ws.Range("A15:E15").Style.Border.TopBorder = XLBorderStyleValues.Thin; ws.Range("A15:E15").Style.Border.TopBorderColor= XLColor.Black;
                    ws.Range("A15:E15").Style.Border.LeftBorder = XLBorderStyleValues.Thin; ws.Range("A15:E15").Style.Border.LeftBorderColor = XLColor.Black;
                    ws.Range("A15:E15").Style.Border.RightBorder = XLBorderStyleValues.Thin; ws.Range("A15:E15").Style.Border.RightBorderColor = XLColor.Black;

                    ws.Range("A14:E14").Style.Border.BottomBorder = XLBorderStyleValues.Thin; ws.Range("A14:E14").Style.Border.BottomBorderColor = XLColor.Black;
                    ws.Range("A14:E14").Style.Border.TopBorder = XLBorderStyleValues.Thin; ws.Range("A14:E14").Style.Border.TopBorderColor = XLColor.Black;
                    ws.Range("A14:E14").Style.Border.LeftBorder = XLBorderStyleValues.Thin; ws.Range("A14:E14").Style.Border.LeftBorderColor = XLColor.Black;
                    ws.Range("A14:E14").Style.Border.RightBorder = XLBorderStyleValues.Thin; ws.Range("A14:E14").Style.Border.RightBorderColor = XLColor.Black;

                    ws.Range("A16:E16").Style.Border.BottomBorder = XLBorderStyleValues.Thin; ws.Range("A16:E16").Style.Border.BottomBorderColor = XLColor.Black;
                    ws.Range("A16:E16").Style.Border.TopBorder = XLBorderStyleValues.Thin; ws.Range("A16:E16").Style.Border.TopBorderColor = XLColor.Black;
                    ws.Range("A16:E16").Style.Border.LeftBorder = XLBorderStyleValues.Thin; ws.Range("A16:E16").Style.Border.LeftBorderColor = XLColor.Black;
                    ws.Range("A16:E16").Style.Border.RightBorder = XLBorderStyleValues.Thin; ws.Range("A16:E16").Style.Border.RightBorderColor = XLColor.Black;

                    ws.Range("A17:E17").Style.Border.BottomBorder = XLBorderStyleValues.Thin; ws.Range("A17:E17").Style.Border.BottomBorderColor = XLColor.Black;
                    ws.Range("A17:E17").Style.Border.TopBorder = XLBorderStyleValues.Thin; ws.Range("A17:E17").Style.Border.TopBorderColor = XLColor.Black;
                    ws.Range("A17:E17").Style.Border.LeftBorder = XLBorderStyleValues.Thin; ws.Range("A17:E17").Style.Border.LeftBorderColor = XLColor.Black;
                    ws.Range("A17:E17").Style.Border.RightBorder = XLBorderStyleValues.Thin; ws.Range("A17:E17").Style.Border.RightBorderColor = XLColor.Black;

                    ws.Range("A18:E18").Style.Border.BottomBorder = XLBorderStyleValues.Thin; ws.Range("A18:E18").Style.Border.BottomBorderColor = XLColor.Black;
                    ws.Range("A18:E18").Style.Border.TopBorder = XLBorderStyleValues.Thin; ws.Range("A18:E18").Style.Border.TopBorderColor = XLColor.Black;
                    ws.Range("A18:E18").Style.Border.LeftBorder = XLBorderStyleValues.Thin; ws.Range("A18:E18").Style.Border.LeftBorderColor = XLColor.Black;
                    ws.Range("A18:E18").Style.Border.RightBorder = XLBorderStyleValues.Thin; ws.Range("A18:E18").Style.Border.RightBorderColor = XLColor.Black;

                    ws.Cell("A16").SetValue("Non Loan").SetActive(); ws.Cell("A16").Style.Font.Bold = true;
                    ws.Cell("A17").SetValue("Treasury").SetActive(); ws.Cell("A17").Style.Font.Bold = true;
                    ws.Cell("A18").SetValue("Total").SetActive(); ws.Cell("A18").Style.Font.Bold = true;
                    ws.Range("A14:E14").Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);
                    ws.Range("A18:E18").Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);

                    var sum_total = ws.Evaluate("SUM(B15:B17)");
                    ws.Cell("B18").SetValue(sum_total).SetActive(); ws.Cell("B18").Style.Font.Bold = true;
                    sum_total = ws.Evaluate("SUM(C15:C17)");
                    ws.Cell("C18").SetValue(sum_total).SetActive(); ws.Cell("C18").Style.Font.Bold = true;
                     sum_total = ws.Evaluate("SUM(D15:D17)");
                     ws.Cell("D18").SetValue(sum_total).SetActive(); ws.Cell("D18").Style.Font.Bold = true;
                     sum_total = ws.Evaluate("SUM(E15:E17)");
                     ws.Cell("E18").SetValue(sum_total).SetActive(); ws.Cell("E18").Style.Font.Bold = true;


                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                    }
                }

            }
            catch (Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

            return View();
        }


        public ActionResult ReportmatchoffGridRead([DataSourceRequest]DataSourceRequest request, int outresult, int _count, int _pageno = 1, int _pagesize = 100, string table_name = "", string condition = "", bool radio_checked = false, int recon_id = 0)   // ProgressView  Read
        {   //First grid load
            List<ManualMatchoff_model> objcat_lst = new List<ManualMatchoff_model>();
            DataTable result = new DataTable();
            string Data1 = "";
            ManualMatchoff_model ReportgridRead = new ManualMatchoff_model();
            ReportgridRead.outresult = outresult;
            ReportgridRead.count = _count;
            ReportgridRead.pageno = _pageno;
            ReportgridRead.pagesize = _pagesize;
            ReportgridRead.table_name = table_name;
            ReportgridRead.Report_condition = condition;
            ReportgridRead.in_outfile_flag = radio_checked;
            ReportgridRead.report_gid = recon_id;
            ReportgridRead.user_code = user_codes;
            ReportgridRead.ip_address = ipAddress;
            string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(ReportgridRead), "Manualmatchoffgridload");
            result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
            List<dynamic> dynamicDt = new List<dynamic>();
            int i = result.Rows.Count;
            foreach (DataRow row in result.Rows)
            {
                dynamic dyn = new ExpandoObject();
                dynamicDt.Add(dyn);
                foreach (DataColumn column in result.Columns)
                {
                    var dic = (IDictionary<string, object>)dyn;
                    dic[column.ColumnName] = row[column];
                }
            }

            Data1 = JsonConvert.SerializeObject(dynamicDt);
            return Json(Data1, JsonRequestBehavior.AllowGet);
            //return Json(objcat_lst.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            //return Json(objcat_lst, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Report_Runpagereport(string table_name = "", string condition = "", bool radio_checked = false, int recon_id = 0, int Recon_gid = 0)
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

       
        public JsonResult get_paginationreport()//drop down
        {
            try
            {
                
                List<TransactionRpt_model> objcat_3st = new List<TransactionRpt_model>();
                DataTable result2 = new DataTable();
                TransactionRpt_model DedupList = new TransactionRpt_model();
                //DedupList.valuedrop = value_drop;
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(DedupList), "get_paginationreport");
                result2 = (DataTable)JsonConvert.DeserializeObject(post_data, result2.GetType());

                TransactionRpt_model objcat3 = new TransactionRpt_model();
                //objcat3.report_gid = 0;
                //objcat3.report_desc = "";
                //objcat_3st.Add(objcat3);

                for (int i = 0; i < result2.Rows.Count; i++)
                {
                    objcat3 = new TransactionRpt_model();
                    objcat3.report_gid = Convert.ToInt32(result2.Rows[i][0].ToString());
                    objcat3.report_desc = result2.Rows[i][1].ToString();
                    objcat_3st.Add(objcat3);
                }
                return Json(objcat_3st, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }
        public ActionResult Report_recontallied(int recon_gid,string recon_tallied,string usercode)
        {
            try
            {

                TransactionRpt_model recontallied = new TransactionRpt_model();
                recontallied.Recongid = Convert.ToInt32(recon_gid);
                recontallied.recontallied = recon_tallied;
                recontallied.user_code = usercode;
                recontallied.ip_address = ipAddress;
              
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(recontallied), "Report_recontallied");
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

        public ActionResult monthendreportFolder()
        {
            Report_model model = new Report_model();
            List<Report_model> objcat_3st = new List<Report_model>();
            string rootPath = @"D:\MonthEndReport";
            DataRow dr;
            DirectoryInfo Dictiontory = new DirectoryInfo(rootPath);
            int count = Dictiontory.GetDirectories().Length;
            DirectoryInfo[] Dir = Dictiontory.GetDirectories();
            DataTable dt = new DataTable();
            dt.Columns.Add("Sno", typeof(int));
            dt.Columns.Add("Foldersname", typeof(string));


            List<string> subfolders = new List<string>();
            Dictionary<string, object> str = new Dictionary<string, object>();

            string filename = "";
            for (int i = 0; i < count; i++)
            {
                var excelfiles = new List<string>();
                string ds = Dir[i].ToString();
                subfolders.Add(ds);

            }

            int subfolders_count = 1;

            for (int j = 0; j < subfolders.Count; j++)
            {
                dr = dt.NewRow();
                dr["Sno"] = subfolders_count;
                dr["Foldersname"] = subfolders[j];
                dt.Rows.Add(dr);
                subfolders_count += 1;
            }

            model.Sno = 0;
            model.Foldersname = "Select";
            objcat_3st.Add(model);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                model = new Report_model();
                model.Sno = Convert.ToInt32(dt.Rows[i]["Sno"].ToString());
                model.Foldersname = dt.Rows[i]["Foldersname"].ToString();
                objcat_3st.Add(model);
            }
            return Json(objcat_3st, JsonRequestBehavior.AllowGet);
            return View(str);
        }


       // public ActionResult monthendreport()
       //{
       //     Report_model model = new Report_model();
       //     List<Report_model> objcat_3st = new List<Report_model>();
       //     string rootPath = @"D:\MonthEndReport";
       //     DataRow dr;
       //     DirectoryInfo Dictiontory = new DirectoryInfo(rootPath);
       //     int count = Dictiontory.GetDirectories().Length;
       //     DirectoryInfo[] Dir = Dictiontory.GetDirectories();
       //     DataTable dt = new DataTable();
       //     dt.Columns.Add("Sno", typeof(int));
       //     dt.Columns.Add("Foldersname", typeof(string));


       //     List<string> subfolders = new List<string>();
       //     Dictionary<string, object> str = new Dictionary<string, object>();

       //     //loadsubdir(rootPath, subfolders);

           
            
       //     string filename = "";
       //     for (int i = 0; i < count; i++)
       //     {
       //         var excelfiles = new List<string>();
       //         string ds = Dir[i].ToString();
       //         subfolders.Add(ds);

       //     }
            
       //     int subfolders_count = 1;

       //     for (int j = 0; j < subfolders.Count; j++)
       //     {
       //         dr = dt.NewRow();
       //         dr["Sno"] = subfolders_count;
       //         dr["Foldersname"] = subfolders[j];
       //         dt.Rows.Add(dr);
       //         subfolders_count += 1;
       //     }

       //     model.Sno=0;
       //     model.Foldersname = "Select";
       //     objcat_3st.Add(model);

       //     for (int i = 0; i < dt.Rows.Count; i++)
       //     {
       //         model = new Report_model();
       //         model.Sno = Convert.ToInt32(dt.Rows[i]["Sno"].ToString());
       //         model.Foldersname = dt.Rows[i]["Foldersname"].ToString();
       //         objcat_3st.Add(model);
       //     }
       //     return Json(objcat_3st, JsonRequestBehavior.AllowGet);


       //     return View(str);
          
       // }

        public ActionResult getsubfolderName(string Subfolder)
       {
            Report_model model = new Report_model();
            List<Report_model> objcat_3st = new List<Report_model>();
            string rootPath = @"D:\MonthEndReport\" + Subfolder;
            DataRow dr;
            DirectoryInfo Dictiontory = new DirectoryInfo(rootPath);
            int count = Dictiontory.GetDirectories().Length;
            DirectoryInfo[] Dir = Dictiontory.GetDirectories();
            DataTable dt = new DataTable();
            dt.Columns.Add("Sno", typeof(int));
            dt.Columns.Add("subFoldersname", typeof(string));


            List<string> subfolders = new List<string>();
            Dictionary<string, object> str = new Dictionary<string, object>();         
            
            string filename = "";
            for (int i = 0; i < count; i++)
            {
                var excelfiles = new List<string>();
                string ds = Dir[i].ToString();
                subfolders.Add(ds);

            }
            
            int subfolders_count = 1;

            for (int j = 0; j < subfolders.Count; j++)
            {
                dr = dt.NewRow();
                dr["Sno"] = subfolders_count;
                dr["subFoldersname"] = subfolders[j];
                dt.Rows.Add(dr);
                subfolders_count += 1;
            }

            model.Sno=0;
            model.subFoldersname = "Select";
            objcat_3st.Add(model);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                model = new Report_model();
                model.Sno = Convert.ToInt32(dt.Rows[i]["Sno"].ToString());
                model.subFoldersname = dt.Rows[i]["subFoldersname"].ToString();
                objcat_3st.Add(model);
            }
            return Json(objcat_3st, JsonRequestBehavior.AllowGet);


            return View(str);
          
        }


        public ActionResult excelfiles([DataSourceRequest] DataSourceRequest request, string foldername, string subfolder)
        {
            if (foldername != "Select")
            {
                string filename = "";
                DataTable dt = new DataTable();
                Report_model model = new Report_model();
                List<Report_model> objcat_3st = new List<Report_model>();
                DataRow dr;
                string rootPath = @"D:\MonthEndReport\" + foldername;
                List<string> list = new List<string>();
                Dictionary<string, object> str = new Dictionary<string, object>();
                DirectoryInfo Dictiontory = new DirectoryInfo(rootPath);
                int count = Dictiontory.GetDirectories().Length;
                DirectoryInfo[] Dir = Dictiontory.GetDirectories();

                String[] all = Directory.GetDirectories(rootPath);
                int sublen = all.Length;
                for (int j = 0; j < count; j++)
                {
                    var excelfiles = new List<string>();
                    string ds = Dir[j].ToString();
                    var sub1 = all[j];
                    DirectoryInfo objDirectoryInfo = new DirectoryInfo(sub1);
                    FileInfo[] allFiles = objDirectoryInfo.GetFiles("*.*", SearchOption.TopDirectoryOnly);
                    for (int k = 0; k < allFiles.Length; k++)
                    {
                        filename = allFiles[k].ToString();
                        excelfiles.Add(filename);
                    }
                    str.Add(ds, excelfiles);
                }

                dt.Columns.Add("Sno", typeof(int));
                dt.Columns.Add("ExcelFiles", typeof(string));
                int Filescnt = 1;

                foreach (var item in str)
                {
                    if (item.Key == subfolder)
                    {
                        var values = item.Value;
                        var result = ((IEnumerable)values).Cast<object>().ToList();
                        for (int j = 0; j < result.Count; j++)
                        {
                            string Files = result[j].ToString();
                            dr = dt.NewRow();
                            dr["Sno"] = Filescnt;
                            dr["ExcelFiles"] = Files;
                            dt.Rows.Add(dr);
                            Filescnt += 1;
                        }
                    }
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    model = new Report_model();
                    model.Sno = Convert.ToInt32(dt.Rows[i]["Sno"].ToString());
                    model.ExcelFiles = dt.Rows[i]["ExcelFiles"].ToString();
                    model.Foldersname = subfolder;
                    objcat_3st.Add(model);
                }
                return Json(objcat_3st.ToDataSourceResult(request));
                return View();
            }
            else
            {
                return Json("");
            }
            
        }

        public ActionResult DownloadFile(string Subfolder, string Excelfiles)
        {
            string Result = "";
            byte[] filebyte;
            string filepath1 = @"D:\MonthEndReport\"+ Subfolder.Replace('/','\\') +"\\" + Excelfiles + "";
            string subDirectory = filepath1;
            try
            {
                filebyte = GetFile(filepath1);
                return File(filebyte, System.Net.Mime.MediaTypeNames.Application.Octet, "Recons.xlsx");
            }
            catch (Exception ex)
            {
               
            }
            return View();
        }

        byte[] GetFile(string s)
        {
            System.IO.FileStream fs = System.IO.File.OpenRead(s);
            byte[] data = new byte[fs.Length];
            int br = fs.Read(data, 0, data.Length);
            if (br != fs.Length)
                throw new System.IO.IOException(s);
            return data;
        }

        //public ActionResult MonthBRS_Download(string TranDate, int Recon_gid)
        //{

        //    try
        //    {    D:\DMSdinesh\Recon_OracleUAT\Recon_Web\Recon\Views\FileImport\FileImportTest.cshtml
        //        List<TransactionRpt_model> objcat_3st = new List<TransactionRpt_model>();
        //        DataSet result2 = new DataSet();
        //        DataSet dt = new DataSet();
        //        DataTable Table = new DataTable();
        //        DataTable Table1 = new DataTable();
        //        DataTable Table2 = new DataTable();
        //        DataTable Table3 = new DataTable();
        //        DataTable Table4 = new DataTable();
        //        TransactionRpt_model DedupList = new TransactionRpt_model();
        //        DedupList.Trandate = TranDate;
        //        DedupList.Recongid = Recon_gid;
        //        string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(DedupList), "ReportMonthbrs_Download");
        //        result2 = (DataSet)JsonConvert.DeserializeObject(post_data, result2.GetType());

        //        TransactionRpt_model objcat3 = new TransactionRpt_model();
        //        Table=result2.Tables[0].Copy();
        //        Table1 = result2.Tables[1].Copy();
        //        Table2= result2.Tables[2].Copy();
        //        Table3 = result2.Tables[3].Copy();
        //        Table4 = result2.Tables[4].Copy();

        //        var  acc_no1=Table.Rows[0]["acc_no1"].ToString();
        //        var acc_no2 = Table.Rows[0]["acc_no2"].ToString();
        //        string Acc1date =Table.Rows[0]["acc_no1_bal_date"].ToString();
        //        string Acc2date = Table.Rows[0]["acc_no2_bal_date"].ToString();
        //        Decimal acc1Bal_amount =Convert.ToDecimal(Table.Rows[0]["acc_no1_bal_amount"].ToString());
        //        Decimal acc2Bal_amount = Convert.ToDecimal(Table.Rows[0]["acc_no2_bal_amount"].ToString());
        //        Decimal accno1_drtotal = Convert.ToDecimal(Table.Rows[0]["acc_no1_dr_total"].ToString());
        //        Decimal accno2_drtotal = Convert.ToDecimal(Table.Rows[0]["acc_no2_dr_total"].ToString());
        //        Decimal accno1_crtotal = Convert.ToDecimal(Table.Rows[0]["acc_no1_cr_total"].ToString());
        //        Decimal accno2_crtotal = Convert.ToDecimal(Table.Rows[0]["acc_no2_cr_total"].ToString());

        //        dt = result2.Copy();
        //        dt.Tables.Remove("Table");
        //        string fileName = "Sample.xlsx";
        //        using (XLWorkbook wb = new XLWorkbook())// wb.Worksheets.Add(dt);
        //        {
        //            ///Setting Table Data static & Dynamic;
        //            var ws = wb.AddWorksheet("Sheet1");
        //            ws.FirstCell().SetValue("FIVE-STAR BUSINESS FINANCE LTD"); ws.Cell("A1").Style.Font.Bold = true;
        //            ws.Cell("A1").Style.Font.Underline.ToString(); ws.Cell("A1").Style.Font.Underline = XLFontUnderlineValues.Single;
        //            ws.FirstCell().CellBelow().SetValue("BANDHAN BANK LTD"); ws.Cell("A2").Style.Font.Bold = true;
        //            ws.Cell("A3").SetValue("CC A/c No.").SetActive(); ws.Cell("A3").Style.Font.Bold = true;
        //            ws.Cell("A4").SetValue("Bank Reconciliation Statement as on date " + Acc1date+"").SetActive();
        //            ws.Cell("A4").Style.Font.Bold = true;
        //            ws.Cell("A7").SetValue("Closing Balance  as per our Bank Book as on "+Acc2date+"").SetActive();
        //            ws.Cell("A7").Style.Font.Bold = true;
        //            ws.Cell("A9").SetValue("Less: Cheques Deposited but not credited in bank passbook").SetActive();
        //            ws.Cell("A11").SetValue("Add: Cheques issued but not presented in the bank").SetActive();
        //            ws.Cell("A13").SetValue("Less: Amount debited in bank but not accounted").SetActive();
        //            ws.Cell("A15").SetValue("Add: Amount credited in bank but not accounted").SetActive();
        //            ws.Cell("A17").SetValue("Closing Balance as per Bank passbook").SetActive();
        //            ws.Cell("A17").Style.Font.Bold = true;
        //            ws.Cell("C6").SetValue("S U M M A R Y").SetActive(); ws.Cell("C6").Style.Font.Bold = true; ws.Cell("C6").Style.Font.Underline = XLFontUnderlineValues.Single;
        //            ws.Cell("C6").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        //            ws.Cell("E1").SetValue("GL CODE").SetActive(); ws.Cell("E1").Style.Font.Bold = true;
        //            ws.Cell("F1").SetValue(acc_no2).SetActive(); ws.Cell("F1").Style.Font.Bold = true;
        //            ws.Cell("C3").SetValue(acc_no1).SetActive(); ws.Cell("C3").Style.Font.Bold = true;
        //            ws.Cell("F7").SetValue(acc2Bal_amount).SetActive(); ws.Cell("F7").Style.Font.Bold = true; ws.Cell("F7").Style.NumberFormat.Format = "###0.00";
        //            ws.Cell("F9").SetValue(accno2_drtotal).SetActive(); ws.Cell("F9").Style.Font.Bold = true; ws.Cell("F9").Style.NumberFormat.Format = "###0.00";
        //            ws.Cell("F11").SetValue(accno2_crtotal).SetActive(); ws.Cell("F11").Style.Font.Bold = true; ws.Cell("F11").Style.NumberFormat.Format = "###0.00";
        //            ws.Cell("F13").SetValue(accno1_drtotal).SetActive(); ws.Cell("F13").Style.Font.Bold = true; ws.Cell("F13").Style.NumberFormat.Format = "###0.00";
        //            ws.Cell("F15").SetValue(accno1_crtotal).SetActive(); ws.Cell("F15").Style.Font.Bold = true; ws.Cell("F15").Style.NumberFormat.Format = "###0.00";
        //            ws.Cell("F17").SetValue(acc1Bal_amount).SetActive(); ws.Cell("F17").Style.Font.Bold = true; ws.Cell("F17").Style.NumberFormat.Format = "###0.00";
        //            ws.Cell("F17").Style.Font.Bold = true; //ws.Cell("F17").Style.Font.Underline = XLFontUnderlineValues.Double;
        //            ws.Cell("F17").Style.Border.TopBorder = XLBorderStyleValues.Thin;
        //            ws.Cell("F17").Style.Border.TopBorderColor = XLColor.Black;
        //            ws.Cell("F17").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
        //            ws.Cell("F17").Style.Border.BottomBorderColor = XLColor.Black;
        //            ws.Cell("F17").Style.Border.InsideBorder = XLBorderStyleValues.Thin;
        //            ws.Cell("A19").SetValue("Less: Cheques Deposited But Not Credited").SetActive(); ws.Cell("A19").Style.Font.Bold = true;
        //            ws.Cell("A19").Style.Font.Underline = XLFontUnderlineValues.Single;
                  

        //            //Inserting  Table1 Data
        //            wb.Worksheet(1).Cell("A20").InsertTable(Table1);

        //            //Inserting  Table2 Data
        //            int Table2Position = 20+Table1.Rows.Count + 3;
        //            string count = "A" + Table2Position + "";
        //            wb.Worksheet(1).Cell(count).InsertTable(Table2);
        //            int Table2text = Table2Position - 1;
        //            string text1 = "A" + Table2text + "";
        //            ws.Cell(text1).SetValue("Add: Cheques Issued But Not Presented").SetActive(); ws.Cell(text1).Style.Font.Bold = true;
        //            ws.Cell(text1).Style.Font.Underline = XLFontUnderlineValues.Single;

        //            //Inserting  Table3 Data
        //            int Table3Position = Table2Position +Table2.Rows.Count+3;
        //            string count1 = "A" + Table3Position + "";
        //            wb.Worksheet(1).Cell(count1).InsertTable(Table3);
        //            int Table3text = Table3Position - 1;
        //            string text2 = "A" + Table3text + "";
        //            ws.Cell(text2).SetValue("Less: Amount  Debited  In Pass Book").SetActive(); ws.Cell(text2).Style.Font.Bold = true;
        //            ws.Cell(text2).Style.Font.Underline = XLFontUnderlineValues.Single;

        //            //Inserting  Table4 Data
        //            int Table4Position = Table3Position + Table3.Rows.Count + 3;
        //            string count2 = "A" + Table4Position + "";
        //            wb.Worksheet(1).Cell(count2).InsertTable(Table4);
        //            int Table4text = Table4Position - 1;
        //            string text3 = "A" + Table4text + "";
        //            ws.Cell(text3).SetValue("Add: Amount Credited In Pass Book").SetActive(); ws.Cell(text3).Style.Font.Bold = true;
        //            ws.Cell(text3).Style.Font.Underline = XLFontUnderlineValues.Single;
                    

        //            //Total Balance & Difference
        //            int balance = Table4Position + Table4.Rows.Count + 1;
        //            string bal = "B" + balance + "";
        //            ws.Cell(bal).SetValue("BALANCE AS PER BANK PASS BOOK").SetActive(); ws.Cell(bal).Style.Font.Bold = true;
        //            int Difference = balance + 2;
        //            string diff = "D" + Difference + "";
        //            ws.Cell(diff).SetValue("Difference").SetActive(); ws.Cell(diff).Style.Font.Bold = true;

        //            using (MemoryStream stream = new MemoryStream())
        //            {
        //                wb.SaveAs(stream);
        //                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        //            }

        //        }
             
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(e.Message);
        //    }
        //    return View();

        //}


        // Hema changes



        public ActionResult getReportType()
        {
            try
            {

                List<Report_model.report_type> objcat_3st = new List<Report_model.report_type>();

                //Report_model.report_type objcat = new Report_model.report_type();
                //objcat.type_code = "";
                //objcat.type_name = "-- Select --";
                //objcat_3st.Add(objcat);

                Report_model.report_type objcat1 = new Report_model.report_type();
                objcat1.type_code = "month_brs";
                objcat1.type_name = "Month Brs";
                objcat_3st.Add(objcat1);

                Report_model.report_type objcat2 = new Report_model.report_type();
                objcat2.type_code = "daily_brs";
                objcat2.type_name = "Daily Brs";
                objcat_3st.Add(objcat2);
                
                Report_model.report_type objcat3 = new Report_model.report_type();
                objcat3.type_code = "recon_percentage";
                objcat3.type_name = "Recon Percentage";
                objcat_3st.Add(objcat3);                
                return Json(objcat_3st, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
           
        }

        public ActionResult Reconpercentage_new(string fromdate, string todate, string recon_gid)
        {
            try
            {
                List<TransactionRpt_model> objcat_3st = new List<TransactionRpt_model>();
                DataSet result2 = new DataSet();
                DataSet result_data = new DataSet();
                string fileName = "";
                DataSet dt = new DataSet();
                DataTable reconsummary = new DataTable();
                DataTable finyear1 = new DataTable();
                DataTable Table = new DataTable();
                DataTable Table1 = new DataTable();
                DataTable Table2 = new DataTable();
                DataTable Table3 = new DataTable();
                DataTable Table4 = new DataTable();
                DataTable Table6 = new DataTable();
                DataTable Table7 = new DataTable();

                long row = 0;
                long col = 0;
                int worksheetcount = 1;
                string cellTxt = "";
                string cellTxt1 = "";
                string filename_tran = "";
                string formulaTxt = "";
                string calcCellTxt = "";
                var recon_name1 = "";

                List<int> recons = recon_gid.Split(',').Select(int.Parse).ToList();
                for (int i = 0; i < recons.Count; i++)
                {
                    if (recons[i] == 0)
                    {
                        recons.RemoveAt(i);
                        i--;
                    }
                }

                int no_of_recons = recons.Count;

                //getting recon summary table
                TransactionRpt_model DedupList = new TransactionRpt_model();
                DedupList.fromdate = fromdate;
                DedupList.todate = todate;
                DedupList.Recon_gid = recon_gid;
                DedupList.no_of_recons = no_of_recons;
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(DedupList), "Reportpercentage_new");
                result_data = (DataSet)JsonConvert.DeserializeObject(post_data, result2.GetType());
                Table1 = result_data.Tables[1].Copy();
                Table2 = result_data.Tables[0].Copy();
                string Date = Table2.Rows[0]["Date1"].ToString();
                string entity_name = Table2.Rows[0]["v_entity_name"].ToString();
                string name = entity_name.Trim('\'');

                using (XLWorkbook wb = new XLWorkbook())
                {

                    var ws = wb.AddWorksheet(Date);

                    //Insrting Table1 Data
                    ws.Cell("A1").SetValue(name); ws.Cell("A1").Style.Font.Bold = true;
                    ws.Cell("A3").InsertTable(Table1);
                    wb.Worksheet(1).Table(0).ShowAutoFilter = false;// Disable AutoFilter.
                    wb.Worksheet(1).Table(0).Theme = XLTableTheme.None;
                    wb.Worksheet(1).Table(0).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    wb.Worksheet(1).Table(0).Style.Border.BottomBorderColor = XLColor.Black;
                    wb.Worksheet(1).Table(0).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    wb.Worksheet(1).Table(0).Style.Border.TopBorderColor = XLColor.Black;
                    wb.Worksheet(1).Table(0).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    wb.Worksheet(1).Table(0).Style.Border.LeftBorderColor = XLColor.Black;
                    wb.Worksheet(1).Table(0).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    wb.Worksheet(1).Table(0).Style.Border.RightBorderColor = XLColor.Black;
                    ws.Range("A3:L3").Style.Font.Bold = true;
                    ws.Range("A3:L3").Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);

                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                    }
                }

            }
            catch (Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

            return View();
        }
    
    }
}

