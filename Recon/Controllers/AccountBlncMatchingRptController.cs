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
using System.Net;

namespace Recon.Controllers
{
    public class AccountBlncMatchingRptController : Controller
    {
        CommonController objcommon = new CommonController();
        // GET: AccountBlncMatchingRpt

        string user_codes = System.Web.HttpContext.Current.Session["usercode"].ToString();
      //  string token = System.Web.HttpContext.Current.Session["token_"].ToString();
        public ActionResult AccountBlncMatchingRpt()
        {
            return View();
        }
        public ActionResult AccNodrop([DataSourceRequest]DataSourceRequest request)
        {
            List<AccountBlncRpt_model> objcat_lst = new List<AccountBlncRpt_model>();
            DataTable result = new DataTable();
            try
            {
                string post_data = objcommon.getApiResult_get("AcMasterList");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
                AccountBlncRpt_model objcat = new AccountBlncRpt_model();
                objcat.account1_code = "";
                objcat.account1_desc = "";
                objcat_lst.Add(objcat);
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    objcat = new AccountBlncRpt_model();
                    objcat.account1_code = result.Rows[i][0].ToString();
                    objcat.account1_desc = result.Rows[i][1].ToString();
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

        public ActionResult GetStatus_Flag([DataSourceRequest]DataSourceRequest request)
        {
            List<AccountBlncRpt_model> objcat_lst = new List<AccountBlncRpt_model>();
            try
            {
                AccountBlncRpt_model objcat = new AccountBlncRpt_model();
                objcat.status_flag ="A" ;
                objcat.status_desc ="ALL" ;
                objcat_lst.Add(objcat);
                objcat = new AccountBlncRpt_model();
                objcat.status_flag = "Y";
                objcat.status_desc = "Matched";
                objcat_lst.Add(objcat);
                objcat = new AccountBlncRpt_model();
                objcat.status_flag = "N";
                objcat.status_desc = "Not Matched";
                objcat_lst.Add(objcat);
               
            }
            catch(Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString();
                LogHelper.WriteLog(ex.ToString(), control);
            }
            return Json(objcat_lst, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Get_BalanceMatching([DataSourceRequest]DataSourceRequest request, AccountBlncRpt_model AccountMatchingRpt)
        {
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST 
            string ipAddress = Dns.GetHostByName(hostName).AddressList[0].ToString();
            AccountMatchingRpt.ip_address = ipAddress;
            AccountMatchingRpt.user_code = user_codes;
            List<AccountBlncRpt_model> objcat_lst = new List<AccountBlncRpt_model>();
            DataTable result = new DataTable();
            try
            {
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(AccountMatchingRpt), "AccountBlncMatching");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());

                for (int i = 0; i < result.Rows.Count; i++)
                {
                    AccountBlncRpt_model objcat = new AccountBlncRpt_model();
                    objcat.SlNo = Convert.ToInt16(result.Rows[i][0]);
                    objcat.AccNo = result.Rows[i][1].ToString();
                    objcat.AccName = result.Rows[i][2].ToString();
                    DateTime tran_dt = Convert.ToDateTime(result.Rows[i][5]);
                    objcat.Tran_date_rpt = tran_dt.ToString("dd-MM-yyyy");
                    objcat.Tran_Balance_Amount = result.Rows[i][6].ToString();
                    objcat.Arrived_amount = result.Rows[i][8].ToString();
                    objcat.status = result.Rows[i][10].ToString();
                    objcat_lst.Add(objcat);
                }
                return Json(objcat_lst.ToDataSourceResult(request));
                //return Json(result1);
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString();
                LogHelper.WriteLog(ex.ToString(), control);
                
            }
            return View();
        }
    }
}