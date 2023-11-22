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
using System.Threading.Tasks;
using System.Threading;
using System.Net;
namespace Recon.Controllers
{
    public class SystemMatchOffController : Controller
    {
        //string url = ConfigurationManager.AppSettings["URL"].ToString();
        //string token = Convert.ToString(System.Web.HttpContext.Current.Session["token_"]);
        string url = ConfigurationManager.AppSettings["URL"].ToString();
           string token= Convert.ToString(System.Web.HttpContext.Current.Session["token_"]);
        //string token = ConfigurationManager.AppSettings["token"].ToString();
        string user_codes = System.Web.HttpContext.Current.Session["usercode"].ToString();
        public static string hostName = Dns.GetHostName(); // Retrive the Name of HOST 
        string ipAddress = Dns.GetHostByName(hostName).AddressList[0].ToString();

        CommonController objcommon = new CommonController();
        // GET: SystemMatchOff
        public ActionResult SystemBulk()
        {
            
            return View();
        }
        public ActionResult Individual()
        {
            ViewBag.url = url;
            ViewBag.ip = ipAddress;
            ViewBag.Message = token;
            return View();
        }
        public ActionResult SystemMatchGrid_Read([DataSourceRequest]DataSourceRequest request, string fieldtype, string process)   // Company  Read
        {
            SystemMatchOff_model sysindivW = new SystemMatchOff_model();
            sysindivW.Select = fieldtype;
            sysindivW.status = process;  
           List<SystemMatchOff_model> objcat_lst = new List<SystemMatchOff_model>();
                DataTable result = new DataTable();
                try
                {
                    string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(sysindivW), "WithinAccList");
                    result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
                    for (int i = 0; i < result.Rows.Count; i++)
                    {
                        SystemMatchOff_model objcat = new SystemMatchOff_model();
                        objcat.WSlNo = Convert.ToInt32(result.Rows[i][0]);
                        objcat.ReconName = result.Rows[i]["recon_name"].ToString();
                        objcat.AccountNo = result.Rows[i]["acc_no1"].ToString();
                        string recon_tallied= result.Rows[i]["recon_tallied"].ToString();
                        if(recon_tallied=="Y")
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
                }
                catch (Exception ex)
                {
                    string control = this.ControllerContext.RouteData.Values["controller"].ToString();LogHelper.WriteLog(ex.ToString(), control);
                }
                return View();
            }

        public ActionResult SystemMatchGridBET_Read([DataSourceRequest]DataSourceRequest request, string fieldtype, string process)   // Company  Read
        {
            SystemMatchOff_model sysindivW = new SystemMatchOff_model();
            sysindivW.Select = fieldtype;
            sysindivW.status = process;
            List<SystemMatchOff_model> objcat_lst = new List<SystemMatchOff_model>();
            DataTable result = new DataTable();
            try
            {
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(sysindivW), "BetweenAccList");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    SystemMatchOff_model objcat = new SystemMatchOff_model();
                    objcat.BSlNo = Convert.ToInt32(result.Rows[i][0]);
                    objcat.ReconName = result.Rows[i]["recon_name"].ToString();
                    objcat.AccountNo1 = result.Rows[i]["acc_no1"].ToString();
                    objcat.AccountNo2 = result.Rows[i]["acc_no2_name"].ToString();
                    objcat.AccountNo1_desc = result.Rows[i]["acc_no1_desc"].ToString();
                    objcat.AccountNo2_desc = result.Rows[i]["acc_no2_desc"].ToString();
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
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString(); LogHelper.WriteLog(ex.ToString(), control);
            }
            return View();
        }


        public ActionResult SystemMatchGrid(string slno, string period_from, string period_to, string recontype)   // Addtional Condition
        {
            SystemMatchOff_model sysindivW = new SystemMatchOff_model();

            sysindivW.Select = slno;
            sysindivW.period_from = period_from;
            sysindivW.period_to = period_to;
            sysindivW.status = "Y";
            sysindivW.Recontype = recontype;
            sysindivW.createdby = user_codes;
            List<SystemMatchOff_model> objcat_lst = new List<SystemMatchOff_model>();

            try
            {
                string result = "";
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(sysindivW), "SystemMatchoffWith");
                result = (string)JsonConvert.DeserializeObject(post_data, result.GetType());
                result = "knock off process in progress please check in utility - > process in progress menu";
                return Json(result, JsonRequestBehavior.AllowGet);
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