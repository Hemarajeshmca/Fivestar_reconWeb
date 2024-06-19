using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Recon_Model;
using System.Data;
using Newtonsoft.Json;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Net;
using Kendo.Mvc.Extensions;
using System.Net.Http.Headers;




namespace Recon.Controllers
{
    public class DashboardController : Controller
    {
        CommonController objcommon = new CommonController();
        int countread = 0;
        //  string url = ConfigurationManager.AppSettings["URL"].ToString();
        //string user_codes = ConfigurationManager.AppSettings["usercode"].ToString();
        string user_codes = System.Web.HttpContext.Current.Session["usercode"].ToString();
        // GET: Dashboard
        public ActionResult Dashboard()
        {
            return View();
        }


        public ActionResult Dash()
        {
            return View();
        }


        public ActionResult Index()
        {
            return View();
        }


        public ActionResult getuserrecon()
        {
            try
            {

                List<DashboardModel> objcat_3st = new List<DashboardModel>();
                DataTable result2 = new DataTable();
                DashboardModel DedupList = new DashboardModel();
                DedupList.user_code = user_codes;
                DedupList.recontype = null;
                DedupList.active_status = "Y";
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(DedupList), "Getuserrecon");
                result2 = (DataTable)JsonConvert.DeserializeObject(post_data, result2.GetType());

                DashboardModel objcat3 = new DashboardModel();
                //objcat3.ReconName_id = 0;
                //objcat3.ReconName = "SelectAll";
                // objcat_3st.Add(objcat3);

                for (int i = 0; i < result2.Rows.Count; i++)
                {
                    objcat3 = new DashboardModel();
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

        public ActionResult Dashboard_read(string period_from,string period_to,string Recongid)
        {
            DashboardModel dashboard_grid = new DashboardModel();
            dashboard_grid.periodfrom = period_from;
            dashboard_grid.periodto =period_to;
            if (Recongid == "" ||Recongid=="undefined")
            {
                dashboard_grid.recon_gid = "0";
            }
            else
            {
                dashboard_grid.recon_gid = Recongid;
            }

            dashboard_grid.user_code = user_codes;
            List<DashboardModel> objcat_lst = new List<DashboardModel>();
            List<DashboardModel> objcat_lst1 = new List<DashboardModel>();
            List<DashboardModel> objlist = new List<DashboardModel>();
            DataSet result = new DataSet();
            DataSet table1 = new DataSet();
            DataSet table2 = new DataSet();
       
         
            try
            {
                   string post_data1= objcommon.getApiResult(JsonConvert.SerializeObject(dashboard_grid), "DashboardList");
                   result = (DataSet)JsonConvert.DeserializeObject(post_data1, result.GetType());

                   DashboardModel objcats = new DashboardModel();
                   DashboardModel obj = new DashboardModel();

                   objcats.recon_count = Convert.ToInt32(result.Tables[0].Rows[0]["recon_count"]);
                   objcats.acc_count = Convert.ToInt32(result.Tables[0].Rows[0]["acc_count"]);
                   objcats.tran_count = Convert.ToInt32(result.Tables[0].Rows[0]["tran_count"]);
                   objcats.ko_count = Convert.ToInt32(result.Tables[0].Rows[0]["ko_count"]);
                   objcats.excp_count = Convert.ToInt32(result.Tables[0].Rows[0]["excp_count"]);
                   objcats.Ko_partialcount = Convert.ToInt32(result.Tables[0].Rows[0]["ko_partialexcp_count"]);
                   objcats.openingexcp_count = Convert.ToInt32(result.Tables[0].Rows[0]["opening_excp_count"]);
                   obj.ko_system_count = Convert.ToDecimal(result.Tables[0].Rows[0]["ko_system_count"]);
                   obj.ko_manual_count = Convert.ToDecimal(result.Tables[0].Rows[0]["ko_manual_count"]);
                   objlist.Add(obj);

                  

                for (int i = 0; i < result.Tables[1].Rows.Count; i++)
                {
                    DashboardModel objcat = new DashboardModel();
                    objcat.ko_month = result.Tables[1].Rows[i]["ko_month"].ToString();
                    string scount = result.Tables[1].Rows[i]["manual_ko_count"].ToString();
                    string systemcount = result.Tables[1].Rows[i]["system_ko_count"].ToString();
                    if(systemcount=="")
                    {
                        objcat.system_ko_count = 0;
                    }
                    else
                    {
                        objcat.system_ko_count = Convert.ToInt32(result.Tables[1].Rows[i]["system_ko_count"]);
                    }
                    if(scount=="")
                    {
                        objcat.manual_ko_count = 0;
                    }
                    else
                    {
                        objcat.manual_ko_count = Convert.ToInt32(result.Tables[1].Rows[i]["manual_ko_count"]);

                    }
                    
                    objcat_lst.Add(objcat);
                }

                for (int i = 0; i < result.Tables[2].Rows.Count; i++)
                {
                    DashboardModel objcat1 = new DashboardModel();
                    objcat1.aging_desc = result.Tables[2].Rows[i]["aging_desc"].ToString();
                    string sCount = Convert.ToString(result.Tables[2].Rows[i]["excp_count"]);
                    if (sCount == "")
                    { objcat1.excp_count = 0; 
                    }
                    else
                    {
                        objcat1.excp_count = Convert.ToInt32(result.Tables[2].Rows[i]["excp_count"]);
                    }
                  
                    objcat_lst1.Add(objcat1);
                }

                return Json(new { objcat_lst, objcat_lst1, objcats, objlist }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString(); LogHelper.WriteLog(ex.ToString(), control);
            }
            return View();

            
        }
    }
}