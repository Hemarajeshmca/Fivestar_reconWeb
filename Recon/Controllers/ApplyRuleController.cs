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
using System.Net.Http.Headers;
using System.IO;
using Newtonsoft.Json;
using System.Text;
using System.Net;

namespace Recon.Controllers
{
    public class ApplyRuleController : Controller
    {
        CommonController objcommon = new CommonController();
        // string user_codes = ConfigurationManager.AppSettings["usercode"].ToString();
        string user_codes = System.Web.HttpContext.Current.Session["usercode"].ToString();
        //   string url = ConfigurationManager.AppSettings["URL"].ToString();
        //  string token = ConfigurationManager.AppSettings["token"].ToString();
        // GET: ApplyRule

        public ActionResult ApplyList()
        {
           //return RedirectToAction("ApplyList_New", "ApplyRule_New");
           return View();
        }

        public ActionResult ApplyRule()
        {
            return View();
        }
        public ActionResult ApplyRuleGrid_Read([DataSourceRequest]DataSourceRequest request, string slno)   // read grid dtl
        {
            try
            {
                ApplyRule_model ApplyRuleGrid = new ApplyRule_model();
                int listno = slno.Length;
                if (listno > 5)
                {
                    ApplyRuleGrid.slno = 0;
                }
                else
                {
                    ApplyRuleGrid.slno = Convert.ToInt32(slno);
                }
                ApplyRuleGrid.user_code = user_codes;
                List<ApplyRule_model> objcat_lst = new List<ApplyRule_model>();
                DataTable result = new DataTable();
                try
                {
                    string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(ApplyRuleGrid), "Applyrulegriddtl");
                    result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
                    for (int i = 0; i < result.Rows.Count; i++)
                    {
                        ApplyRule_model objcat = new ApplyRule_model();
                        objcat.slno = Convert.ToInt32(result.Rows[i][0]);
                        objcat.listslno = Convert.ToInt32(result.Rows[i][1]);
                        objcat.Rulegid = Convert.ToInt32(result.Rows[i][2]);
                        objcat.RuleName = result.Rows[i][3].ToString();
                        objcat.reversl_flag = result.Rows[i][4].ToString();
                        objcat.Ruleorder = result.Rows[i][5].ToString();
                        objcat.status = result.Rows[i][6].ToString();
                        objcat.status_desc = result.Rows[i][7].ToString();
                        objcat.sno = Convert.ToInt32(result.Rows[i][8]);
                        objcat.group_flag = result.Rows[i]["group_flag"].ToString();
                        objcat.group_method = result.Rows[i]["group_method"].ToString();
                        objcat.group_many = result.Rows[i]["manytomany_match_flag"].ToString();
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
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        public ActionResult ApplyRuleGrid_Readlist([DataSourceRequest]DataSourceRequest request)   // List  Read
        {
            ApplyRule_model ApplyRuleGrid = new ApplyRule_model();
            ApplyRuleGrid.slno = 0;
            ApplyRuleGrid.status = "";
            ApplyRuleGrid.user_code = user_codes;
            {
                List<ApplyRule_model> objcat_lst = new List<ApplyRule_model>();
                DataTable result = new DataTable();
                try
                {
                    string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(ApplyRuleGrid), "ApplyruleList");
                    result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
                    for (int i = 0; i < result.Rows.Count; i++)
                    {
                        ApplyRule_model objcat = new ApplyRule_model();
                        objcat.listslno = Convert.ToInt32(result.Rows[i]["applyrule_gid"]);
                        objcat.ReconName = result.Rows[i]["recon_name"].ToString();
                        objcat.Recontype = result.Rows[i]["recontype_desc"].ToString();
                        objcat.AccountnoRecName = result.Rows[i]["active_status_desc"].ToString();
                        objcat.supporting_file = result.Rows[i]["tranbrkptype_name"].ToString();
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
        }
        public ActionResult ApplyRule_save(string Recontype, string recon_id, string matchsys, string matchman, string support_id, string period_from, string period_to, string untiflag, string status)   // Company  Read
        {

            {
                try
                {

                    ApplyRule_model ApplyRule = new ApplyRule_model();
                    ApplyRule.slno = 0;
                    ApplyRule.Recontype = Recontype;
                    ApplyRule.ReconName_id = Convert.ToInt32(recon_id);
                    ApplyRule.supporting_file_id = Convert.ToInt32(support_id);
                    ApplyRule.period_from = period_from;
                    if (period_to == "null")
                    {
                        period_to = null;
                    }
                    ApplyRule.period_to = period_to;
                    ApplyRule.matchoffSys = matchsys;
                    ApplyRule.status = status;
                    if (ApplyRule.matchoffSys == "")
                    {
                        ApplyRule.matchoffSys = "N";
                    }
                    ApplyRule.matchoffMan = matchman;
                    if (ApplyRule.matchoffMan == "")
                    {
                        ApplyRule.matchoffMan = "N";
                    }
                    ApplyRule.untillactive = untiflag;
                    if (ApplyRule.untillactive == "")
                    {
                        ApplyRule.untillactive = "N";
                    }
                    ApplyRule.user_code = user_codes;
                    string[] result = { };
                    string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(ApplyRule), "ApplyruleCreate");
                    result = (string[])JsonConvert.DeserializeObject(post_data, result.GetType());
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {

                }
                return View();
            }
        }
        public ActionResult ApplyRule_Update(string Recontype, string recon_id, string matchsys, string matchman, string support_id, string period_from, string period_to, string untiflag, string slno, string status)   // Company  Read
        {
            {
                try
                {
                    ApplyRule_model ApplyRule = new ApplyRule_model();
                    ApplyRule.slno = Convert.ToInt32(slno);
                    ApplyRule.Recontype = Recontype;
                    ApplyRule.ReconName_id = Convert.ToInt32(recon_id);
                    ApplyRule.supporting_file_id = Convert.ToInt32(support_id);
                    ApplyRule.period_from = period_from;

                    ApplyRule.period_to = period_to;
                    ApplyRule.matchoffSys = matchsys;
                    ApplyRule.status = status;
                    if (ApplyRule.matchoffSys == "")
                    {
                        ApplyRule.matchoffSys = "N";
                    }
                    ApplyRule.matchoffMan = matchman;
                    if (ApplyRule.matchoffMan == "")
                    {
                        ApplyRule.matchoffMan = "N";
                    }
                    ApplyRule.untillactive = untiflag;
                    if (ApplyRule.untillactive == "")
                    {
                        ApplyRule.untillactive = "N";
                    }
                    string[] result = { };
                    string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(ApplyRule), "ApplyruleUpdate");
                    result = (string[])JsonConvert.DeserializeObject(post_data, result.GetType());
                    ApplyRule.user_code = user_codes;
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
        public ActionResult ApplyRuleGrid_ReadAdd([DataSourceRequest]DataSourceRequest request, string slno = "0", string rulegid = "0")   // Addtional Condition
        {
            ApplyRule_model ApplyRuleGrid = new ApplyRule_model();
            int listno = slno.Length;
            if (listno > 3)
            {
                ApplyRuleGrid.slno = 0;
            }
            else
            {
                ApplyRuleGrid.slno = Convert.ToInt32(slno.ToString());
            }
            ApplyRuleGrid.Rulegid = Convert.ToInt32(rulegid.ToString());
            ApplyRuleGrid.user_code = user_codes;
            List<ApplyRule_model> objcat_lst = new List<ApplyRule_model>();
            DataTable result = new DataTable();
            try
            {
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(ApplyRuleGrid), "Applyrulegridadd");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());

                for (int i = 0; i < result.Rows.Count; i++)
                {
                    ApplyRule_model objcat = new ApplyRule_model();
                    objcat.slnoadd = Convert.ToInt32(result.Rows[i][0]);
                    objcat.slno = Convert.ToInt32(result.Rows[i][1]);
                    objcat.listslno = Convert.ToInt32(result.Rows[i][2]);
                    objcat.field_name = result.Rows[i][3].ToString();
                    objcat.field_alias_name = result.Rows[i][4].ToString();
                    objcat.ExtractionCriteria = result.Rows[i][5].ToString();
                    objcat.TargetField = result.Rows[i][6].ToString();
                    objcat.TargetField_alias = result.Rows[i][7].ToString();
                    objcat.ComparisonCriteria = result.Rows[i][8].ToString();
                    // objcat.groupflag = result.Rows[i][9].ToString();
                    objcat.status = result.Rows[i][9].ToString();
                    objcat.status_desc = result.Rows[i][10].ToString();
                    objcat.sno = Convert.ToInt32(result.Rows[i][11]);
                    objcat.extraction_filter = Convert.ToInt32(result.Rows[i]["extraction_filter"]);
                    objcat.comparision_filter = Convert.ToInt32(result.Rows[i]["comparison_filter"]);
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
        public ActionResult ApplyRuleGrid_ReadBet([DataSourceRequest]DataSourceRequest request, string slno, string rulegid)   // Base Selection
        {

            ApplyRule_model ApplyRuleGrid = new ApplyRule_model();
            int listno = slno.Length;
            if (listno > 3)
            {
                ApplyRuleGrid.slno = 0;
            }
            else
            {
                ApplyRuleGrid.slno = Convert.ToInt32(slno);
            }
            ApplyRuleGrid.Rulegid = Convert.ToInt32(rulegid);
            ApplyRuleGrid.user_code = user_codes;
            List<ApplyRule_model> objcat_lst = new List<ApplyRule_model>();
            DataTable result = new DataTable();
            try
            {
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(ApplyRuleGrid), "Applyrulegridgroup");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());

                for (int i = 0; i < result.Rows.Count; i++)
                {
                    ApplyRule_model objcat = new ApplyRule_model();
                    objcat.slnobase = Convert.ToInt32(result.Rows[i][0]);
                    objcat.slno = Convert.ToInt32(result.Rows[i][1]);

                    objcat.listslno = Convert.ToInt32(result.Rows[i][2]);
                    objcat.account1 = result.Rows[i]["acc_no"].ToString();
                    objcat.account2 = result.Rows[i]["acc_desc"].ToString();
                    objcat.acc_no = result.Rows[i]["acc_no"].ToString();
                    objcat.acc_desc = result.Rows[i]["acc_desc"].ToString();
                    objcat.Accountmode = result.Rows[i][6].ToString();
                    objcat.Accountmode_alias_name = result.Rows[i][7].ToString();
                    objcat.Order = result.Rows[i][8].ToString();
                    objcat.status = result.Rows[i][9].ToString();
                    objcat.status_desc = result.Rows[i][10].ToString();
                    objcat.sno = Convert.ToInt32(result.Rows[i][11]);
                    objcat.noofbase = result.Rows[i][11].ToString();
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
        public ActionResult ApplyRuleGrid_ReadRule([DataSourceRequest]DataSourceRequest request, string slno, string rulegid)   // Base Selection
        {

            ApplyRule_model ApplyRuleGrid = new ApplyRule_model();

            ApplyRuleGrid.Rulegid = Convert.ToInt32(rulegid);

            ApplyRuleGrid.user_code = user_codes;
            List<ApplyRule_model> objcat_lst = new List<ApplyRule_model>();
            DataTable result = new DataTable();
            try
            {
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(ApplyRuleGrid), "ApplyrulegridRule");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());

                for (int i = 0; i < result.Rows.Count; i++)
                {
                    ApplyRule_model objcat = new ApplyRule_model();
                    objcat.slnorule = Convert.ToInt32(result.Rows[i]["rulefield_gid"]);
                    objcat.slno = Convert.ToInt32(result.Rows[i]["rule_gid"]);
                    objcat.field_name = result.Rows[i]["source_field"].ToString();
                    objcat.field_alias_name = result.Rows[i]["source_field_alias"].ToString();
                    objcat.ExtractionCriteria = result.Rows[i]["extraction_criteria"].ToString();
                    objcat.TargetField = result.Rows[i]["target_field"].ToString();
                    objcat.TargetField_alias = result.Rows[i]["target_field_alias"].ToString();
                    objcat.ComparisonCriteria = result.Rows[i]["comparison_criteria"].ToString();
                    objcat.status = result.Rows[i]["active_status"].ToString();
                    objcat.status_desc = result.Rows[i]["active_status_desc"].ToString();
                    objcat.sno = Convert.ToInt32(result.Rows[i]["sno"]);
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

        public ActionResult ApplyRuleGrid_ReadBaseFilterRule([DataSourceRequest]DataSourceRequest request, string baseFilterRulegid = "0", string inAction = "")   // Base Selection
        {

            ApplyRule_model ApplyRuleGrid = new ApplyRule_model();

            ApplyRuleGrid.Rulegid = Convert.ToInt32(baseFilterRulegid);
            ApplyRuleGrid.in_action = inAction;
            ApplyRuleGrid.user_code = user_codes;
            List<ApplyRule_model> objcat_lst = new List<ApplyRule_model>();
            DataTable result = new DataTable();
            try
            {
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(ApplyRuleGrid), "ApplyruleBaseFilterRule");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());

                for (int i = 0; i < result.Rows.Count; i++)
                {
                    ApplyRule_model objcat = new ApplyRule_model();
                    objcat.slnobaseFilter = Convert.ToInt32(result.Rows[i]["applyrule_basefilter_gid"]);
                    objcat.slnobase = Convert.ToInt32(result.Rows[i]["applyrule_basesele_gid"]);
                    objcat.filter_appliedon = result.Rows[i]["filter_applied_on"].ToString();
                    objcat.filter_appliedon_desc = result.Rows[i]["filter_applied_desc"].ToString();
                    objcat.field_name = result.Rows[i]["filter_field"].ToString();
                    objcat.field_alias_name = result.Rows[i]["base_field_alias"].ToString();
                    objcat.BaseCriteria = result.Rows[i]["filter_criteria"].ToString();
                    objcat.Identitycriteria = result.Rows[i]["ident_criteria"].ToString();
                    objcat.TargetField = result.Rows[i]["ident_value"].ToString();

                    objcat.ComparisonCriteria = result.Rows[i]["add_filter"].ToString();
                    objcat.status = result.Rows[i]["active_status"].ToString();
                    if (objcat.status == "Y")
                    {
                        objcat.status_desc = "Active";
                    }
                    else
                    {
                        objcat.status_desc = "InActive";
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

        public ActionResult ApplyRuleGrid_ListEdit([DataSourceRequest]DataSourceRequest request, int id)//list Grid
        {

            ApplyRule_model dedupgridlist = new ApplyRule_model();
            dedupgridlist.slno = id;
            dedupgridlist.user_code = user_codes;
            List<ApplyRule_model> ListEdit = new List<ApplyRule_model>();
            DataTable result = new DataTable();
            try
            {
                dedupgridlist.dbsource = "MYSQL";
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(dedupgridlist), "Applyrule_listEdit");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    ApplyRule_model objcat = new ApplyRule_model();
                    objcat.slno = Convert.ToInt32(result.Rows[i][0]);
                    objcat.ReconName_id = Convert.ToInt32(result.Rows[i]["recon_gid"]);
                    objcat.Recontype = result.Rows[i][3].ToString();
                    objcat.ReconName = result.Rows[i][2].ToString();
                    objcat.matchoffSys = result.Rows[i][7].ToString();
                    objcat.matchoffMan = result.Rows[i][8].ToString();
                    objcat.period_from = result.Rows[i][9].ToString();
                    objcat.period_to = result.Rows[i][10].ToString();
                    objcat.untillactive = result.Rows[i][11].ToString();
                    objcat.status = result.Rows[i][12].ToString();
                    objcat.supporting_file_id = Convert.ToInt32(result.Rows[i]["tranbrkptype_gid"]);

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
        public ActionResult ApplyRuleGrid_create(ApplyRule_model ApplyRulegrid)//dtl create
        {

            ApplyRulegrid.user_code = user_codes;
            try
            {
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(ApplyRulegrid), "ApplyRuleGridCreate");
                result = (string[])JsonConvert.DeserializeObject(post_data, result.GetType());
                ApplyRulegrid.applyruledtl_gid = Convert.ToInt32(result[1].ToString());
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString();
                LogHelper.WriteLog(ex.ToString(), control);
            }
            return View();
        }
        public ActionResult ApplyRuleGrid_Update(ApplyRule_model ApplyRulegrid)//dtl update
        {
            ApplyRulegrid.user_code = user_codes;
            try
            {
                string result = "";
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(ApplyRulegrid), "ApplyRuleGridUpdate");
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
        public ActionResult ApplyRuleGrid_createAdd(ApplyRule_model ApplyRulegrid)//addtional grid create
        {

            ApplyRulegrid.user_code = user_codes;
            ApplyRulegrid.status = "Y";
            //   ApplyRulegrid.groupflag = "N";
            try
            {
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(ApplyRulegrid), "ApplyRuleGridCreateAdd");
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
        public ActionResult ApplyRuleGrid_UpdateAdd(ApplyRule_model ApplyRulegrid)//addtional grid update
        {

            ApplyRulegrid.user_code = user_codes;
            try
            {
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(ApplyRulegrid), "ApplyRuleGridUpdateAdd");
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

        public ActionResult ApplyRuleGrid_UpdateBaseFilter(ApplyRule_model ApplyRulegrid)//addtional grid update
        {

            ApplyRulegrid.user_code = user_codes;
            try
            {
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(ApplyRulegrid), "ApplyRuleGridUpdateAddBaseFilter");
                result = (string[])JsonConvert.DeserializeObject(post_data, result.GetType());
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        public ActionResult ApplyRuleGrid_createBase(ApplyRule_model ApplyRulegrid)//base selection create
        {

            ApplyRulegrid.user_code = user_codes;
            if (ApplyRulegrid.status == null)
            {
                ApplyRulegrid.status = "Y";
            }
            try
            {
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(ApplyRulegrid), "ApplyRuleGridCreateBase");
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
        public ActionResult ApplyRuleGrid_UpdateBase(ApplyRule_model ApplyRulegrid)//base selecion update
        {

            ApplyRulegrid.user_code = user_codes;
            try
            {
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(ApplyRulegrid), "ApplyRuleGridUpdateBase");
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
        #region drop down
        public ActionResult ApplyRecontype_drop([DataSourceRequest]DataSourceRequest request)   // Recon type  Read
        {
            ApplyRule_model objcat = new ApplyRule_model();
            List<ApplyRule_model> objcat_lst = new List<ApplyRule_model>();
            DataTable result = new DataTable();
            ApplyRule_model RecontypeList = new ApplyRule_model();
            RecontypeList.user_code = user_codes;
            try
            {
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(RecontypeList), "RecontypeList");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());



                objcat.ReconName_id = 0;
                objcat.ReconName = "--SelectAll--";
                objcat_lst.Add(objcat);

                for (int i = 0; i < result.Rows.Count; i++)
                {
                    objcat = new ApplyRule_model();
                    objcat.ReconName_id = Convert.ToInt32(result.Rows[i][0]);
                    objcat.ReconName = result.Rows[i][1].ToString();
                    objcat_lst.Add(objcat);
                }

                return Json(objcat_lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString(); LogHelper.WriteLog(ex.ToString(), control);
            }
            return View();
        }
        public JsonResult Recon_type_desc(string fieldtype, string process)//drop down
        {
            try
            {
                List<ApplyRule_model> objcat_3st = new List<ApplyRule_model>();

                ApplyRule_model dedup = new ApplyRule_model();
                dedup.Select = fieldtype;
                dedup.status = process;
                dedup.user_code = user_codes;

                if (fieldtype == null || fieldtype == "") fieldtype = "W";

                DataTable result2 = new DataTable();
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(dedup), "ApplyruleDrop?fieldtype=" + fieldtype + "&process=" + process + "");
                result2 = (DataTable)JsonConvert.DeserializeObject(post_data, result2.GetType());
                ApplyRule_model objcat2 = new ApplyRule_model();
                objcat2.ReconName_id = 0;
                objcat2.ReconName = "--Select--";
                objcat_3st.Add(objcat2);
                for (int i = 0; i < result2.Rows.Count; i++)
                {
                    objcat2 = new ApplyRule_model();
                    objcat2.ReconName_id = Convert.ToInt32(result2.Rows[i]["recon_gid"]);
                    objcat2.ReconName = result2.Rows[i]["recon_name"].ToString();
                    objcat_3st.Add(objcat2);
                }
                return Json(objcat_3st, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }
        public JsonResult Recon_accno(string code)//drop down
        {
            try
            {
                List<ApplyRule_model> objcat_3st = new List<ApplyRule_model>();

                ApplyRule_model dedup = new ApplyRule_model();
                dedup.ReconName_id = Convert.ToInt32(code.ToString());
                dedup.user_code = user_codes;
                DataTable result2 = new DataTable();
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(dedup), "AccountNodrop?code=" + code + "");
                result2 = (DataTable)JsonConvert.DeserializeObject(post_data, result2.GetType());
                ApplyRule_model objcat2 = new ApplyRule_model();
                objcat2.account2 = "";
                objcat_3st.Add(objcat2);
                for (int i = 0; i < result2.Rows.Count; i++)
                {
                    objcat2 = new ApplyRule_model();
                    objcat2.account1 = result2.Rows[i]["acc_no"].ToString();
                    objcat2.account2 = result2.Rows[i]["acc_desc"].ToString();
                    objcat2.acc_no = result2.Rows[i]["acc_no"].ToString();
                    objcat2.acc_desc = result2.Rows[i]["acc_desc"].ToString();
                    objcat_3st.Add(objcat2);
                }
                return Json(objcat_3st, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }
        public JsonResult Recon_type_Suport(string fieldtype, string process)//drop down
        {
            try
            {
                List<ApplyRule_model> objcat_3st = new List<ApplyRule_model>();

                ApplyRule_model dedup = new ApplyRule_model();
                dedup.slno = 0;
                dedup.status = "Y";
                dedup.user_code = user_codes;
                DataTable result2 = new DataTable();
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(dedup), "ApplyruleSUPDrop?fieldtype=" + fieldtype + "&process=" + process + "");
                result2 = (DataTable)JsonConvert.DeserializeObject(post_data, result2.GetType());
                ApplyRule_model objcat2 = new ApplyRule_model();
                objcat2.supporting_file_id = 0;
                objcat2.supporting_file = "--Select--";
                objcat_3st.Add(objcat2);
                for (int i = 0; i < result2.Rows.Count; i++)
                {
                    objcat2 = new ApplyRule_model();
                    objcat2.supporting_file_id = Convert.ToInt32(result2.Rows[i][0]);
                    objcat2.supporting_file = result2.Rows[i][1].ToString();
                    objcat_3st.Add(objcat2);
                }
                return Json(objcat_3st, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }
        public ActionResult ApplyRuletype_drop([DataSourceRequest]DataSourceRequest request)   // Recon type  Read
        {
            try
            {
                List<ApplyRule_model> objcat_lst = new List<ApplyRule_model>();
                DataTable result = new DataTable();
                ApplyRule_model RuleList = new ApplyRule_model();
                RuleList.user_code = user_codes;
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(RuleList), "RuleList");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());

                ApplyRule_model objcat = new ApplyRule_model();
                objcat.Rulegid = 0;
                objcat.RuleName = "";
                objcat.status = "";
                objcat_lst.Add(objcat);
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    objcat = new ApplyRule_model();
                    objcat.Rulegid = Convert.ToInt32(result.Rows[i][0]);
                    objcat.RuleName = result.Rows[i][1].ToString();
                    objcat.status = result.Rows[i][7].ToString();
                    objcat_lst.Add(objcat);
                }
                return Json(objcat_lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e);
            }
        }

        public ActionResult AccNoonedrop([DataSourceRequest]DataSourceRequest request, int id)
        {
            List<ApplyRule_model> objcat_lst = new List<ApplyRule_model>();
            DataTable result = new DataTable();
            ApplyRule_model AcMasterList = new ApplyRule_model();
            AcMasterList.user_code = user_codes;
            AcMasterList.recon_gid = id;
            try
            {
                try
                {
                    string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(AcMasterList), "GetaccRecon");
                    result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
                    ApplyRule_model objcat = new ApplyRule_model();
                    objcat.account1 = "";
                    objcat.account2 = "";
                    objcat_lst.Add(objcat);
                    for (int i = 0; i < result.Rows.Count; i++)
                    {
                        objcat = new ApplyRule_model();
                        objcat.account1 = result.Rows[i][1].ToString();
                        objcat.account2 = result.Rows[i]["acc_desc"].ToString();
                        objcat_lst.Add(objcat);
                    }
                    return Json(objcat_lst, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    string control = this.ControllerContext.RouteData.Values["controller"].ToString(); LogHelper.WriteLog(ex.ToString(), control);
                }
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString(); LogHelper.WriteLog(ex.ToString(), control);
            }
            return View();
        }

        public ActionResult AccNo1drop([DataSourceRequest]DataSourceRequest request, int value_drop)
        {
            List<ApplyRule_model> objcat_lst = new List<ApplyRule_model>();
            DataTable result = new DataTable();
            ApplyRule_model AcMasterList = new ApplyRule_model();
            AcMasterList.user_code = user_codes;
            AcMasterList.ReconName_id = Convert.ToInt32(value_drop.ToString());
            try
            {
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(AcMasterList), "AccountNodrop");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
                ApplyRule_model objcat = new ApplyRule_model();
                objcat.acc_no = "";
                objcat.acc_desc = "";
                objcat_lst.Add(objcat);
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    objcat = new ApplyRule_model();
                    objcat.acc_no = result.Rows[i]["acc_no"].ToString();
                    objcat.acc_desc = result.Rows[i]["acc_desc"].ToString();
                    objcat_lst.Add(objcat);
                }
                return Json(objcat_lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString(); LogHelper.WriteLog(ex.ToString(), control);
            }
            return View();
        }

        #endregion
        #region delete function
        public ActionResult ApplyRulelist_Delete(ApplyRule_model ApplyRulegrid)//dtl create
        {

            ApplyRulegrid.user_code = user_codes;
            try
            {
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(ApplyRulegrid), "ApplyRuleListDelete");
                result = (string[])JsonConvert.DeserializeObject(post_data, result.GetType());
                ApplyRulegrid.applyruledtl_gid = Convert.ToInt32(result[1].ToString());
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

            }
            return View();
        }
        public ActionResult ApplyRuledtl_Delete(ApplyRule_model ApplyRulegrid)//dtl create
        {

            ApplyRulegrid.user_code = user_codes;
            try
            {
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(ApplyRulegrid), "ApplyRuledtlDelete");
                result = (string[])JsonConvert.DeserializeObject(post_data, result.GetType());
                ApplyRulegrid.applyruledtl_gid = Convert.ToInt32(result[1].ToString());
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString();
                LogHelper.WriteLog(ex.ToString(), control);
            }
            return View();
        }
        public ActionResult ApplyRuleBaseSel_Delete(ApplyRule_model ApplyRulegrid)//dtl create
        {

            ApplyRulegrid.user_code = user_codes;
            try
            {
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(ApplyRulegrid), "ApplyRuleBaseSelDelete");
                result = (string[])JsonConvert.DeserializeObject(post_data, result.GetType());
                ApplyRulegrid.applyruledtl_gid = Convert.ToInt32(result[1].ToString());
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString(); LogHelper.WriteLog(ex.ToString(), control);
            }
            return View();
        }
        public ActionResult ApplyRuleAddCon_Delete(ApplyRule_model ApplyRulegrid)//dtl create
        {

            ApplyRulegrid.user_code = user_codes;
            try
            {
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(ApplyRulegrid), "ApplyRuleAddConDelete");
                result = (string[])JsonConvert.DeserializeObject(post_data, result.GetType());
                ApplyRulegrid.applyruledtl_gid = Convert.ToInt32(result[1].ToString());
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString();
                LogHelper.WriteLog(ex.ToString(), control);
            }
            return View();
        }
        #endregion
    }
}