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

namespace Recon.Controllers
{
    public class DedupeRuleController : Controller
    {
        CommonController objcommon = new CommonController();
       // string url = ConfigurationManager.AppSettings["URL"].ToString();
        string user_codes = ConfigurationManager.AppSettings["usercode"].ToString();
       // string token = ConfigurationManager.AppSettings["token"].ToString();
        // GET: DedupeRule
        public ActionResult DedupeRuleView()
        {
            return View();
        }
        public ActionResult DedupeRule()
        {
            return View();
        }

        public ActionResult dummy()
        {
            return View();
        }

        public ActionResult DedupeRule_create(string Rule_name, string period_from, string period_to, string status, string untill)
        {
            try
            {

                DedupeRule_model dedup = new DedupeRule_model();
                dedup.Rule_name = Rule_name;
                dedup.period_from = period_from;
                dedup.period_To = period_to;
                dedup.status = status;
                dedup.untillactive = untill;
                if (dedup.untillactive == "")
                {
                    dedup.untillactive = "N";
                }
                dedup.user_code = user_codes;
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(dedup), "DedupCreate");
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
        public ActionResult DedupeRule_update(string Rule_name, string period_from, string period_to, string status, string untill,string slno)
        {
            try
            {

                DedupeRule_model dedup = new DedupeRule_model();
                dedup.slno = Convert.ToInt32(slno); 
                dedup.Rule_name = Rule_name;
                dedup.period_from = period_from;
                dedup.period_To = period_to;
                dedup.status = status;
                dedup.untillactive = untill;
                if (dedup.untillactive == "")
                {
                    dedup.untillactive = "N";
                }
                dedup.user_code = user_codes;
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(dedup), "DedupUpdate");
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
        public ActionResult DedupeRulegrid_create(DedupeRule_model dedupgrid)
        {           
            dedupgrid.group_flag = "Y";
            if (dedupgrid.status == null)
            {
                dedupgrid.status = "Y";
            }
            dedupgrid.user_code = user_codes;
            try
            {
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(dedupgrid), "DedupGridCreate");
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
        public ActionResult DedupeRulegrid_update(DedupeRule_model dedupgrid)
        {
            dedupgrid.user_code = user_codes;
            try
            {
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(dedupgrid), "DedupGridupdate");
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
        public ActionResult DedupeGrid_Read([DataSourceRequest]DataSourceRequest request, string slno)   // Company  Read
        {
            
            DedupeRule_model dedupgridlist = new DedupeRule_model();
            int listno =slno.Length;
            if (listno > 3)
            {
                dedupgridlist.slno = 0;
            }
            else
            {
                dedupgridlist.slno = Convert.ToInt32(slno); 
            }
            dedupgridlist.user_code = user_codes;
            List<DedupeRule_model> GridEdit = new List<DedupeRule_model>();
            DataTable result = new DataTable();
            try
            {
                dedupgridlist.dbsource = "MYSQL";
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(dedupgridlist), "DedupgridList");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    DedupeRule_model objcat = new DedupeRule_model();
                    objcat.slno = Convert.ToInt32(result.Rows[i][0]);
                    objcat.slnolist = Convert.ToInt32(result.Rows[i][1]);
                    objcat.field_name = result.Rows[i][2].ToString();
                    objcat.field_alias_name = result.Rows[i][3].ToString();
                    objcat.ComparisonCriteria = result.Rows[i][4].ToString();
                    objcat.ExtractionCriteria = result.Rows[i][5].ToString();
                    objcat.status = result.Rows[i][6].ToString();
                    objcat.status_desc = result.Rows[i][7].ToString();
                    objcat.sno = Convert.ToInt32(result.Rows[i][8]);
                    GridEdit.Add(objcat);
                }
                return Json(GridEdit.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString(); 
                LogHelper.WriteLog(ex.ToString(), control);
            }
            return View();
        }
        public ActionResult DedupeGrid_ReadList([DataSourceRequest]DataSourceRequest request)   // Dedup  List
        {
            List<DedupeRule_model> objcat_lst = new List<DedupeRule_model>();
            DataTable result = new DataTable();
            DedupeRule_model DedupList = new DedupeRule_model();
            DedupList.user_code = user_codes;
            try
            {
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(DedupList), "DedupList");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());


                for (int i = 0; i < result.Rows.Count; i++)
                {
                    DedupeRule_model objcat = new DedupeRule_model();
                    objcat.slno = Convert.ToInt32(result.Rows[i][0]);
                    objcat.Deduperule = result.Rows[i][1].ToString();
                    objcat.status = result.Rows[i][6].ToString();
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
        public ActionResult DedupeGrid_ListEdit([DataSourceRequest]DataSourceRequest request, int id)
        {
            DedupeRule_model dedupgridlist = new DedupeRule_model();
            dedupgridlist.slno = id;
            dedupgridlist.user_code = user_codes;
            List<DedupeRule_model> ListEdit = new List<DedupeRule_model>();
            DataTable result = new DataTable();
            try
            {
                dedupgridlist.dbsource = "MYSQL";
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(dedupgridlist), "Dedupgrid_listEdit");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    DedupeRule_model objcat = new DedupeRule_model();
                    objcat.slno = Convert.ToInt32(result.Rows[i][0]);
                    objcat.Deduperule = result.Rows[i][1].ToString();
                    objcat.period_from = result.Rows[i][2].ToString();
                    objcat.period_To = result.Rows[i][3].ToString();
                    objcat.status = result.Rows[i][5].ToString();
                    objcat.untillactive = result.Rows[i][4].ToString();
                    ListEdit.Add(objcat);
                }
                return Json(ListEdit, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString(); 
                LogHelper.WriteLog(ex.ToString(), control);
            }
            return View();
        }
        #region drop down
        public JsonResult dedupDbaliasdrop()//drop down
        {
            try
            {
                List<DedupeRule_model> objcat_lst = new List<DedupeRule_model>();
                DataTable result = new DataTable();
                DedupeRule_model DbAliasDrop = new DedupeRule_model();
                DbAliasDrop.user_code = user_codes;
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(DbAliasDrop), "DbAliasDrop");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());

                DedupeRule_model objcat = new DedupeRule_model();
                objcat.field_name = "";
                objcat.field_alias_name = "";
                objcat.TargetField = "";
                objcat.TargetField_alias = "";
                objcat_lst.Add(objcat);
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    objcat = new DedupeRule_model();
                    objcat.field_name = result.Rows[i][0].ToString();
                    objcat.field_alias_name = result.Rows[i][1].ToString();
                    objcat.TargetField = result.Rows[i][0].ToString();
                    objcat.TargetField_alias = result.Rows[i][1].ToString();
                    objcat_lst.Add(objcat);
                }
                return Json(objcat_lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        public JsonResult dedupconditiondrop(string fieldtype, int filter_flag)//drop down
        {
            try
            {
                List<DedupeRule_model> objcat_2st = new List<DedupeRule_model>();

                DedupeRule_model dedup = new DedupeRule_model();
                dedup.field_type = fieldtype;
                dedup.filter_flag = filter_flag;
                dedup.user_code = user_codes;
                DataTable result1 = new DataTable();
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(dedup), "certiaDrop?fieldtype=" + fieldtype + "&filter_flag=" + filter_flag + "");
                result1 = (DataTable)JsonConvert.DeserializeObject(post_data, result1.GetType());

                for (int i = 0; i < result1.Rows.Count; i++)
                {
                    DedupeRule_model objcat1 = new DedupeRule_model();
                    objcat1.filter_flag = Convert.ToInt32(result1.Rows[i]["filter_flag"]);
                    objcat1.filter_desc = result1.Rows[i]["filter_desc"].ToString();
                    objcat1.field_type = result1.Rows[i]["field_type"].ToString();
                    objcat1.function_name = result1.Rows[i]["function_name"].ToString();
                    objcat1.selected_status = Convert.ToInt32(result1.Rows[i]["selected_status"]);
                    objcat_2st.Add(objcat1);
                }
                return Json(objcat_2st, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                return Json(e.Message);
            }
        }
        public JsonResult dedupExtraciondrop(string fieldtype, int filter_flag)//drop down
        {
            try
            {
                List<DedupeRule_model> objcat_3st = new List<DedupeRule_model>();

                DedupeRule_model dedup = new DedupeRule_model();
                dedup.field_type = fieldtype;
                dedup.filter_flag = filter_flag;
                dedup.user_code = user_codes;
                DataTable result2 = new DataTable();
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(dedup), "certiaDrop?fieldtype=" + fieldtype + "&filter_flag=" + filter_flag + "");
                result2 = (DataTable)JsonConvert.DeserializeObject(post_data, result2.GetType());
                for (int i = 0; i < result2.Rows.Count; i++)
                {
                    DedupeRule_model objcat1 = new DedupeRule_model();
                    objcat1.filter_flag = Convert.ToInt32(result2.Rows[i]["filter_flag"]);
                    objcat1.filter_desc = result2.Rows[i]["filter_desc"].ToString();
                    objcat1.field_type = result2.Rows[i]["field_type"].ToString();
                    objcat1.function_name = result2.Rows[i]["function_name"].ToString();
                    objcat1.selected_status = Convert.ToInt32(result2.Rows[i]["selected_status"]); ;
                    objcat_3st.Add(objcat1);
                }
                return Json(objcat_3st, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                return Json(e.Message);
            }
        }

        #endregion

        ///field type
        public ActionResult field_type(string code)
        {
            try
            {
                Session["Name"] = code;
                DedupeRule_model dedup = new DedupeRule_model();
                dedup.field_type = code;
                dedup.user_code = user_codes;
                string result = "";
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(dedup), "Fieldtype");
                result = (string)JsonConvert.DeserializeObject(post_data, result.GetType());
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString(); LogHelper.WriteLog(ex.ToString(), control);
            }
            return View();
        }

    }
}
