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
using System.Configuration;

namespace Recon.Controllers
{
    public class RuleDefinitionController : Controller
    {

      //  string url = ConfigurationManager.AppSettings["URL"].ToString();
        string user_codes = ConfigurationManager.AppSettings["usercode"].ToString();
       // string token = ConfigurationManager.AppSettings["token"].ToString();
        CommonController objcommon = new CommonController();

        // GET: RuleDefinition
        public ActionResult RuleDefinition()
        {
            return View();
        }
        public ActionResult RuleDefinitionList()
        {
            return View();
        }

        public ActionResult RuleList([DataSourceRequest]DataSourceRequest request)   // Rule list
        {
            List<DedupeRule_model> objcat_lst = new List<DedupeRule_model>();
            DataTable result = new DataTable();
            DedupeRule_model RuleList = new DedupeRule_model();
            RuleList.user_code = user_codes;
            try
            {
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(RuleList), "RuleList");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    DedupeRule_model objcat = new DedupeRule_model();
                    objcat.slno = Convert.ToInt32(result.Rows[i]["rule_gid"]);
                    objcat.Rule_name = result.Rows[i]["rule_name"].ToString();
                    objcat.status = result.Rows[i]["active_status_desc"].ToString();
                    objcat.insertdate=result.Rows[i]["insert_date"].ToString();
                    objcat.updatedate=result.Rows[i]["update_date"].ToString();
                    objcat_lst.Add(objcat);
                }
                return Json(objcat_lst.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString(); 
                LogHelper.WriteLog(ex.ToString(), control);
            }
            return View();
        }

        public ActionResult Rule_create(string Rule_name, string period_from, string period_to, string status, string untill, string ingroup)
        {
            try
            {

                DedupeRule_model Rule = new DedupeRule_model();
                Rule.Rule_name = Rule_name;
                Rule.period_from = period_from;
                Rule.period_To = period_to;
                Rule.status = status;
                Rule.group_flag = ingroup;
                Rule.untillactive = untill;
                if (Rule.untillactive == "")
                {
                    Rule.untillactive = "N";
                }
                Rule.user_code = user_codes;
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(Rule), "RuleCreate");
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
        public ActionResult Rule_Delete(DedupeRule_model dedupgrid)
        {
            dedupgrid.user_code = user_codes;
            try
            {
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(dedupgrid), "RuleDelete");
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
        public ActionResult Rulegrid_create(DedupeRule_model dedupgrid)
        {
            if (dedupgrid.status == null)
            {
                dedupgrid.status = "Y";
            }
            if (dedupgrid.group_flag == null)
            {
                dedupgrid.group_flag = "N";
            }
            dedupgrid.user_code = user_codes;
            try
            {
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(dedupgrid), "RulegridCreate");
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
        public ActionResult Rulegrid_ListEdit([DataSourceRequest]DataSourceRequest request, int id)
        {
            DedupeRule_model dedupgridlist = new DedupeRule_model();
            dedupgridlist.slno = id;
            dedupgridlist.user_code = user_codes;
            Session["listslno"] = id;
            List<DedupeRule_model> ListEdit = new List<DedupeRule_model>();
            DataTable result = new DataTable();
            try
            {
                dedupgridlist.dbsource = "MYSQL";
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(dedupgridlist), "RuleGrid_listEdit");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    DedupeRule_model objcat = new DedupeRule_model();
                    objcat.slno = Convert.ToInt32(result.Rows[i][0]);
                    objcat.Rule_name = result.Rows[i][1].ToString();
                    objcat.period_from = result.Rows[i][2].ToString();
                    objcat.period_To = result.Rows[i][3].ToString();
                    objcat.period_To = result.Rows[i][3].ToString();
                    objcat.status = result.Rows[i][6].ToString();
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
        public ActionResult Rulegrid_Read([DataSourceRequest]DataSourceRequest request, string slno)   // Company  Read
        {

            DedupeRule_model dedupgridlist = new DedupeRule_model();
            int listno = slno.Length;
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
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(dedupgridlist), "RuleGridList");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    DedupeRule_model objcat = new DedupeRule_model();
                    objcat.slno = Convert.ToInt32(result.Rows[i][0]);
                    objcat.slnolist = Convert.ToInt32(result.Rows[i][1]);
                    objcat.field_name = result.Rows[i][2].ToString();
                    objcat.field_alias_name = result.Rows[i][3].ToString();
                    objcat.ExtractionCriteria = result.Rows[i][4].ToString();
                    objcat.TargetField = result.Rows[i][5].ToString();
                    objcat.TargetField_alias = result.Rows[i]["target_field_alias"].ToString();
                    objcat.ComparisonCriteria = result.Rows[i][7].ToString();
                    objcat.status = result.Rows[i][8].ToString();
                    objcat.status_desc = result.Rows[i][9].ToString();
                    objcat.sno = Convert.ToInt32(result.Rows[i][10]);
                    objcat.extraction_filter = Convert.ToInt32(result.Rows[i]["extraction_filter"]);
                    objcat.comparision_filter = Convert.ToInt32(result.Rows[i]["comparison_filter"]);
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
        public ActionResult Rule_update(string Rule_name, string period_from, string period_to, string status, string ingroup, string untill, string slno)
        {
            try
            {

                DedupeRule_model Rule = new DedupeRule_model();
                Rule.slno = Convert.ToInt32(slno);
                Rule.Rule_name = Rule_name;
                Rule.period_from = period_from;
                Rule.period_To = period_to;
                Rule.status = status;
                Rule.group_flag = ingroup;
                Rule.untillactive = untill;
                if (Rule.untillactive == "")
                {
                    Rule.untillactive = "N";
                }
                Rule.user_code = user_codes;
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(Rule), "RuleUpdate");
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
        public ActionResult Rulegrid_update(DedupeRule_model dedupgrid)
        {
            dedupgrid.user_code = user_codes;
            try
            {
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(dedupgrid), "RulegridUpdate");
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
        public ActionResult Rulegrid_Delete(DedupeRule_model dedupgrid)
        {
            dedupgrid.user_code = user_codes;
            try
            {
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(dedupgrid), "RulegridDelete");
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

        public JsonResult ruleExtracioncreteriadrop(string fieldtype, string process)//drop down
        {
            try
            {
                List<drop> objcat_3st = new List<drop>();

                DedupeRule_model dedup = new DedupeRule_model();
                dedup.field_type = fieldtype;
                dedup.Name = process;
                dedup.user_code = user_codes;
                DataTable result2 = new DataTable();
                string Data1 = "";
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(dedup), "ExtractioncertiaDrop?fieldtype=" + fieldtype + "&process=" + process + "");
                result2 = (DataTable)JsonConvert.DeserializeObject(post_data, result2.GetType());

                for (int i = 0; i < result2.Rows.Count; i++)
                {
                    drop objcat2 = new drop();
                    objcat2.ExtractionCriteria = result2.Rows[i]["condition_desc"].ToString();
                    objcat_3st.Add(objcat2);
                }
                //using (var client = new HttpClient())
                //{

                //    client.BaseAddress = new Uri(url);
                //    client.DefaultRequestHeaders.Accept.Clear(); client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //    HttpContent content = new StringContent(JsonConvert.SerializeObject(dedup), UTF8Encoding.UTF8, "application/json");
                //    var response = client.PostAsync(, content).Result;
                //    Stream data = response.Content.ReadAsStreamAsync().Result;
                //    StreamReader reader = new StreamReader(data);

                //    //Data1 = JsonConvert.SerializeObject(result2);
                //}

                return Json(objcat_3st, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }
        public JsonResult ruleconditioncreteriadrop(string fieldtype, string process)//drop down
        {
            try
            {
                List<DedupeRule_model> objcat_2st = new List<DedupeRule_model>();

                DedupeRule_model dedup = new DedupeRule_model();
                dedup.field_type = fieldtype;
                dedup.Name = process;
                dedup.user_code = user_codes;
                DataTable result1 = new DataTable();
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(dedup), "ExtractioncertiaDrop?fieldtype=" + fieldtype + "&process=" + process + "");
                result1 = (DataTable)JsonConvert.DeserializeObject(post_data, result1.GetType());

                for (int i = 0; i < result1.Rows.Count; i++)
                {
                    DedupeRule_model objcat1 = new DedupeRule_model();
                    objcat1.ComparisonCriteria = result1.Rows[i]["condition_desc"].ToString();
                    objcat_2st.Add(objcat1);
                }

                //using (var client = new HttpClient())
                //{
                //    try
                //    {
                //        client.BaseAddress = new Uri(url);
                //        client.DefaultRequestHeaders.Accept.Clear(); client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //        HttpContent content = new StringContent(JsonConvert.SerializeObject(dedup), UTF8Encoding.UTF8, "application/json");
                //        var response = client.PostAsync("ExtractioncertiaDrop?fieldtype=" + fieldtype + "&process=" + process + "", content).Result;
                //        Stream data = response.Content.ReadAsStreamAsync().Result;
                //        StreamReader reader = new StreamReader(data);

                //    }
                //    catch (Exception ex)
                //    {
                //        string control = this.ControllerContext.RouteData.Values["controller"].ToString(); LogHelper.WriteLog(ex.ToString(), control);
                //    }

                //    //return Json(result1);
                //}
                return Json(objcat_2st, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

    }
}