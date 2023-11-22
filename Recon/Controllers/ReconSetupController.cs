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

namespace Recon.Controllers
{
    public class ReconSetupController : Controller
    {
        CommonController objcommon = new CommonController();
       
    //    string url = ConfigurationManager.AppSettings["URL"].ToString();
        string user_codes = ConfigurationManager.AppSettings["usercode"].ToString();
    //    string token = ConfigurationManager.AppSettings["token"].ToString();
        // GET: ReconSetup
        public ActionResult ReconSetupList()
        {
            return View();
        }
        public ActionResult ReconSetupView()
        {
            return View();
        }
        public ActionResult RecontypeGrid_Read([DataSourceRequest]DataSourceRequest request)   // Recon type  Read
        {
            List<Recontype_model> objcat_lst = new List<Recontype_model>();
            DataTable result = new DataTable();
            Recontype_model RecontypeList = new Recontype_model();
            RecontypeList.recon_id=0;
            RecontypeList.user_code=user_codes;
            try
            {

                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(RecontypeList), "RecontypeList");
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
                    objcat.account1_desc = result.Rows[i]["acc_no1"].ToString();
                    objcat.account2_desc = result.Rows[i]["acc_no2"].ToString();
                    objcat.Account1 = result.Rows[i]["acc_no1_desc"].ToString();
                    objcat.Account2 = result.Rows[i]["acc_no2_desc"].ToString();


                    string untillactive = "";
                    untillactive = result.Rows[i][12].ToString();
                    if (untillactive == "Y")
                    {
                        objcat.untillactive = "true";
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
        public ActionResult RecontypeGrid_Save(Recontype_model Deptmodel)
        
        {
            try
            {
                var datestring = Deptmodel.period_from;
                DateTime dt = DateTime.ParseExact(datestring.Substring(0, 24),
                                  "ddd MMM dd yyyy HH:mm:ss",
                                  CultureInfo.InvariantCulture);
                Deptmodel.period_from = dt.ToString("yyyy-MM-dd");
                var datestring1 = Deptmodel.period_to;
                if (datestring1 != null)
                {
                    DateTime dt1 = DateTime.ParseExact(datestring1.Substring(0, 24),
                                  "ddd MMM dd yyyy HH:mm:ss",
                                  CultureInfo.InvariantCulture);
                    Deptmodel.period_to = dt1.ToString("yyyy-MM-dd");
                }
                else
                {
                    Deptmodel.period_to = null;
                }
                
               
                if (Deptmodel.untillactive == null)
                {
                    Deptmodel.untillactive = "N";
                }
                else if (Deptmodel.untillactive == "true" || Deptmodel.untillactive == "Y")
                {
                    Deptmodel.untillactive = "Y";
                } 
                else
                {
                    Deptmodel.untillactive = "N";
                }
                if (Deptmodel.Recon_type == null)
                {
                    Deptmodel.Recon_type = "";
                }
                if (Deptmodel.MappingType_code == null)
                {
                    Deptmodel.MappingType_code = "P";
                }
                if (Deptmodel.Status_code == null)
                {
                    Deptmodel.Status_code = "Y";
                }
                Deptmodel.dbsource = "MYSQL";
                Deptmodel.user_code = user_codes;
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(Deptmodel), "RecontypeCreate");
                string[] result = { };
                result = (string[])JsonConvert.DeserializeObject(post_data, result.GetType());
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

            }
            return View();
        }
        public ActionResult RecontypeGrid_Update(Recontype_model Deptmodel)
        {
            try
            {
                var datestring = Deptmodel.period_from;
                if (datestring.Contains("/"))
                {

                    DateTime dt1 = DateTime.ParseExact(datestring,
                                 "M/d/yyyy",
                                 CultureInfo.InvariantCulture);
                    Deptmodel.period_from = dt1.ToString("yyyy-MM-dd");
                }
                else if (datestring.Contains("-"))
                {
                    DateTime dt = DateTime.ParseExact(datestring,
                             "dd-MM-yyyy",
                             CultureInfo.InvariantCulture);
                    Deptmodel.period_from = dt.ToString("yyyy-MM-dd");
                }
                else
                {
                    Deptmodel.period_from = null;

                }

                var datestring1 = Deptmodel.period_to;
                //if (datestring1 != null)
                //{
                //    DateTime dt1 = DateTime.ParseExact(datestring1,
                //                  "dd-MM-yyyy",
                //                  CultureInfo.InvariantCulture);
                //    Deptmodel.period_to = dt1.ToString("yyyy-MM-dd");
                //}
                //else
                //{
                //    Deptmodel.period_to = null;
                //}
                if (datestring1==null)
                {
                    Deptmodel.period_to = null;
                }
                else if (datestring1.Contains('-'))
                {
                    DateTime dt1 = DateTime.ParseExact(datestring1,
                                  "dd-MM-yyyy",
                                  CultureInfo.InvariantCulture);
                    Deptmodel.period_to = dt1.ToString("yyyy-MM-dd");
                }
                else if (datestring1.Contains('/'))
                {
                    DateTime dt1 = DateTime.ParseExact(datestring1,
                                  "M/d/yyyy",
                                  CultureInfo.InvariantCulture);
                    Deptmodel.period_to = dt1.ToString("yyyy-MM-dd");
                  

                }
                
                //int date = Deptmodel.period_from.Length;
                //if (date == 10)
                //{
                //    DateTime periodparse = DateTime.ParseExact(Deptmodel.period_from, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //    Deptmodel.period_from = periodparse.ToString("yyyy-MM-dd");
                //}
                //else
                //{
                //    var datestring = Deptmodel.period_from;
                //    DateTime dt = DateTime.ParseExact(datestring.Substring(0, 24), "ddd MMM dd yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                //    Deptmodel.period_from = dt.ToString("yyyy-MM-dd");
                //}

                //if (Deptmodel.period_to != null)
                //{
                //    int date1 = Deptmodel.period_to.Length;
                //    if (date1 == 10)
                //    {
                //        DateTime periodparse1 = DateTime.ParseExact(Deptmodel.period_to, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //        Deptmodel.period_to = periodparse1.ToString("yyyy-MM-dd");
                //    }
                //    else
                //    {
                //        var datestring1 = Deptmodel.period_to;
                //        DateTime dt1 = DateTime.ParseExact(datestring1.Substring(0, 24), "ddd MMM dd yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                //        Deptmodel.period_to = dt1.ToString("yyyy-MM-dd");
                //    }
                //}
                if (Deptmodel.Recon_type == null)
                {
                    Deptmodel.Recon_type = "";
                }

                if (Deptmodel.untillactive == null)
                {
                    Deptmodel.untillactive = "N";
                }
                else if (Deptmodel.untillactive == "true" || Deptmodel.untillactive == "Y")
                {
                    Deptmodel.untillactive = "Y";
                }
                else
                {
                    Deptmodel.untillactive = "N";
                }

                Deptmodel.dbsource = "MYSQL";
                Deptmodel.user_code = user_codes;
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(Deptmodel), "RecontypeUpdate");
                result = (string[])JsonConvert.DeserializeObject(post_data, result.GetType());
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

            }
            return View();
        }
        public ActionResult RecontypeGrid_Delete(Recontype_model Deptmodel)
        {
            try
            {
               
                Deptmodel.dbsource = "MYSQL";
                Deptmodel.user_code = user_codes;
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(Deptmodel), "RecontypeDelete");
                result = (string[])JsonConvert.DeserializeObject(post_data, result.GetType());
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

            }
            return View();
        }
        #region dropdown
        public ActionResult AccNo1drop([DataSourceRequest]DataSourceRequest request)
        {
            List<Recontype_model> objcat_lst = new List<Recontype_model>();
            DataTable result = new DataTable();
            AcMaster_model AcMastergrid = new AcMaster_model();
            AcMastergrid.Account_id = 0;
            AcMastergrid.Status = "";
            AcMastergrid.user_code = user_codes;
            try
            {

                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(AcMastergrid), "AcMasterList");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());

                Recontype_model objcat = new Recontype_model();
                objcat.account1_code = "";
                objcat.account1_desc = "";
                objcat_lst.Add(objcat);
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    objcat = new Recontype_model();
                    objcat.account1_code = result.Rows[i][0].ToString();
                    objcat.account1_desc = result.Rows[i][1].ToString();
                    objcat_lst.Add(objcat);
                }
                return Json(objcat_lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString();LogHelper.WriteLog(ex.ToString(), control);
            }
            return View();
        }
        public ActionResult AccN02drop([DataSourceRequest]DataSourceRequest request)
        {
            List<Recontype_model> objcat_lst = new List<Recontype_model>();
            DataTable result = new DataTable();
            AcMaster_model AcMastergrid = new AcMaster_model();
            AcMastergrid.Account_id = 0;
            AcMastergrid.Status = "";
            AcMastergrid.user_code = user_codes;           
            try
            {
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(AcMastergrid), "AcMasterList");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());

                Recontype_model objcat = new Recontype_model();
                objcat.account2_code = "";
                objcat.account2_desc = "";
                objcat_lst.Add(objcat);
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    objcat = new Recontype_model();
                    objcat.account2_code = result.Rows[i][0].ToString();
                    objcat.account2_desc = result.Rows[i][1].ToString();
                    objcat_lst.Add(objcat);
                }
                return Json(objcat_lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString();LogHelper.WriteLog(ex.ToString(), control);
            }
            return View();
        }
        public ActionResult AccName(string code)
        {
            string accname = "";
            try
            {

                Recontype_model Recontype = new Recontype_model();
                Recontype.account1_code = code;
                Recontype.user_code = user_codes;
                DataTable result = new DataTable();
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(Recontype), "AccName");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
                accname = result.Rows[0][0].ToString();
                return Json(accname, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

            }
            return View();
        }
        #endregion
    }
}