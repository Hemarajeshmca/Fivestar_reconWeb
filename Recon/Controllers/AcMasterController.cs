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
using System.Net.Http.Headers;
using System.IO;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Net;

namespace Recon.Controllers
{
    public class AcMasterController : Controller
    {
        CommonController objcommon = new CommonController();
        
      //  string url = ConfigurationManager.AppSettings["URL"].ToString();
       string user_codes = ConfigurationManager.AppSettings["usercode"].ToString();
       //string user_codes = "";
        // GET: AcMaster
        //public void sessionout()
        //{
        //    try
        //    {
        //        user_codes = ConfigurationManager.AppSettings["usercode"].ToString();
        //    }
        //    catch
        //    {
        //        RedirectToAction("Login");
        //    }



        //}
        public ActionResult ACList()
        {   
            return View();
        }
        public ActionResult ACView()
        {
            return View();
        }
        public ActionResult AcGrid_Read([DataSourceRequest]DataSourceRequest request)   
        {
            AcMaster_model AcMastergrid = new AcMaster_model();
            AcMastergrid.Account_id = 0;
            AcMastergrid.Status = "";          
            AcMastergrid.user_code = user_codes;
            List<AcMaster_model> objcat_lst = new List<AcMaster_model>();
            DataTable result = new DataTable();
            try
            {
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(AcMastergrid), "AcMasterList"); 
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());

                for (int i = 0; i < result.Rows.Count; i++)
                {
                    AcMaster_model objcat = new AcMaster_model();
                    objcat.Account_id = Convert.ToInt32(result.Rows[i][0]);
                    objcat.AccountNo = result.Rows[i][1].ToString();
                    objcat.AccountName = result.Rows[i][2].ToString();
                    objcat.Category_name = result.Rows[i][3].ToString();
                    objcat.Respon_name = result.Rows[i][4].ToString();
                    objcat.Recontype = result.Rows[i][6].ToString();
                    objcat.withinflag = result.Rows[i][5].ToString();
                    objcat.Accounttype = result.Rows[i][7].ToString();
                    objcat.Deduperule_id = result.Rows[i]["deduprule_gid"].ToString();
                    objcat.Dedup_rule = result.Rows[i]["deduprule_validation"].ToString();
                    objcat.Dedupvalidateion = result.Rows[i][8].ToString();
                    objcat.Status = result.Rows[i][10].ToString();
                    objcat.status_desc = result.Rows[i][11].ToString();
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

        public ActionResult AcMasterGrid_Save(AcMaster_model Deptmodel)
        {            
            try
            {
                Deptmodel.Account_id = 0;
                Deptmodel.dbsource = "MYSQL";
                if (Deptmodel.Recontype == "true")
                {
                    Deptmodel.Recontype = "Y";
                }
                else
                {
                    Deptmodel.Recontype = "N";
                }
                if (Deptmodel.withinflag == "true")
                {
                    Deptmodel.withinflag = "Y";
                }
                else
                {
                    Deptmodel.withinflag = "N";
                }
                if (Deptmodel.Status == null)
                {
                    Deptmodel.Status = "Y";
                }
                if (Deptmodel.Category_name == null)
                {
                    Deptmodel.Category_name = "Y";
                }
                if (Deptmodel.Respon_name == null)
                {
                    Deptmodel.Respon_name = "Y";
                }
                if (Deptmodel.Dedupvalidateion == null)
                {
                    Deptmodel.Dedupvalidateion = "N";
                }
                if (Deptmodel.Accounttype == null)
                {
                    Deptmodel.Accounttype = "I";
                }
                Deptmodel.user_code = user_codes;
                Deptmodel.action = "INSERT";
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(Deptmodel), "AcMasterCreate");
                result = (string[])JsonConvert.DeserializeObject(post_data, result.GetType());
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            return View();
        }
        public ActionResult AcGrid_Update(AcMaster_model Deptmodel)
        {
            
            try
            {                
                Deptmodel.dbsource = "MYSQL";
                if (Deptmodel.Recontype != "Y")
                {
                    if (Deptmodel.Recontype == "true")
                    {
                        Deptmodel.Recontype = "Y";
                    }
                    else
                    {
                        Deptmodel.Recontype = "N";
                    }
                }

                if (Deptmodel.withinflag != "Y")
                {
                    if (Deptmodel.withinflag == "true")
                    {
                        Deptmodel.withinflag = "Y";
                    }
                    else
                    {
                        Deptmodel.withinflag = "N";
                    }
                }
               
                if (Deptmodel.Dedupvalidateion == null)
                {
                    Deptmodel.Dedupvalidateion = "N";
                }
                if (Deptmodel.Accounttype == null)
                {
                    Deptmodel.Accounttype = "I";
                }
                Deptmodel.user_code = user_codes;
                Deptmodel.action = "UPDATE";
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(Deptmodel), "AcMasterUpdate");
                result = (string[])JsonConvert.DeserializeObject(post_data, result.GetType());
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            return View();
        }
        public ActionResult AcGrid_Delete(AcMaster_model Deptmodel)
        {
            try
            {
                Deptmodel.dbsource = "MYSQL";
                if (Deptmodel.Recontype != "Y")
                {
                    if (Deptmodel.Recontype == "true")
                    {
                        Deptmodel.Recontype = "Y";
                    }
                    else
                    {
                        Deptmodel.Recontype = "N";
                    }
                }

                if (Deptmodel.withinflag != "Y")
                {
                    if (Deptmodel.withinflag == "true")
                    {
                        Deptmodel.withinflag = "Y";
                    }
                    else
                    {
                        Deptmodel.withinflag = "N";
                    }
                }

                if (Deptmodel.Dedupvalidateion == null)
                {
                    Deptmodel.Dedupvalidateion = "N";
                }
                if (Deptmodel.Accounttype == null)
                {
                    Deptmodel.Accounttype = "I";
                }
                Deptmodel.user_code = user_codes;
                Deptmodel.action = "DELETE";

                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(Deptmodel), "AcMasterDelete");
                result = (string[])JsonConvert.DeserializeObject(post_data, result.GetType());
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            return View();
        }
        #region drop down
        public JsonResult AccMasCatdrop()//drop down
        {
            try
            {
                List<AcMaster_model> objcat_lst = new List<AcMaster_model>();
                DataTable result = new DataTable();
                AcMaster_model CategoryDROP = new AcMaster_model();
                CategoryDROP.user_code = user_codes;
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(CategoryDROP), "CategoryDROP");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());

                AcMaster_model objcat = new AcMaster_model();
                objcat.Category_id = 0;
                objcat.Category_name = "";
                objcat_lst.Add(objcat);
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    objcat = new AcMaster_model();
                    objcat.Category_id = Convert.ToInt64(result.Rows[i][0]);
                    objcat.Category_name = result.Rows[i][1].ToString();
                    objcat_lst.Add(objcat);
                }
                return Json(objcat_lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception er)
            {
                return Json(er.Message);
            }
        }           
      
        public JsonResult AccMasRESdrop()//drop down
        {
           try
           {
               List<AcMaster_model> objcat_2st = new List<AcMaster_model>();
               DataTable result1 = new DataTable();
               AcMaster_model ResponsibilityList = new AcMaster_model();
               ResponsibilityList.user_code = user_codes;
               string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(ResponsibilityList), "ResponsibilityList");
               result1 = (DataTable)JsonConvert.DeserializeObject(post_data, result1.GetType());

               AcMaster_model objcat1 = new AcMaster_model();
               objcat1.Respon_id = 0;
               objcat1.Respon_name = "";
               objcat_2st.Add(objcat1);
               for (int i = 0; i < result1.Rows.Count; i++)
               {
                   objcat1 = new AcMaster_model();
                   objcat1.Respon_id = Convert.ToInt64(result1.Rows[i][0]);
                   objcat1.Respon_name = result1.Rows[i][1].ToString();
                   objcat_2st.Add(objcat1);
               }
               return Json(objcat_2st, JsonRequestBehavior.AllowGet);
           }
            catch(Exception e)
           {
               return Json(e.Message);
           }
            }          
     
        public JsonResult AccMasDupdrop()//drop down
        {
            try
            {
                List<AcMaster_model> objcat_3st = new List<AcMaster_model>();
                DataTable result2 = new DataTable();
                AcMaster_model DedupList = new AcMaster_model();
                DedupList.user_code = user_codes;
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(DedupList), "DedupList");
                result2 = (DataTable)JsonConvert.DeserializeObject(post_data, result2.GetType());

                AcMaster_model objcat3 = new AcMaster_model();
                objcat3.Deduperule_id = "0";
                objcat3.Dedup_rule = "";
                objcat_3st.Add(objcat3);

                for (int i = 0; i < result2.Rows.Count; i++)
                {
                    objcat3 = new AcMaster_model();
                    objcat3.Deduperule_id = result2.Rows[i][0].ToString();
                    objcat3.Dedup_rule = result2.Rows[i][1].ToString();
                    objcat_3st.Add(objcat3);
                }
                return Json(objcat_3st, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                return Json(e.Message);
            }
            }
        #endregion
    }
}