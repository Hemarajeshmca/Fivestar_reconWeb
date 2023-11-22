using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Recon_Model;
using System.Text;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Data;
using Kendo.Mvc.Extensions;
using System.Configuration;

namespace Recon.Controllers
{
    public class SetUpController : Controller
    {
        // GET: SetUp
     //   string url = ConfigurationManager.AppSettings["URL"].ToString();
        string user_codes = ConfigurationManager.AppSettings["usercode"].ToString();
      //  string token = ConfigurationManager.AppSettings["token"].ToString();
        CommonController objcommon = new CommonController();

        public ActionResult Category()
        {
            return View();
        }
        public ActionResult Responsibility()
        {
            return View();
        }
        public ActionResult Supportfiletype()
        {
            return View();
        }
        public ActionResult Remark()
        {
            return View();
        }
        public ActionResult RemarkSupport()
        {
            return View();
        }

        public ActionResult Remarksupportnew()
        {
            return View();
        }
        #region Category
        public ActionResult CategoryGrid_Save([DataSourceRequest] DataSourceRequest request, CategoryViewModel Deptmodel)
        {
            try
            {
                Deptmodel.category_id = 0;     
               if( Deptmodel.Category_name==null)
               {
                   Deptmodel.Category_name = "";
               }
               if (Deptmodel.Status == null)
               {
                   Deptmodel.Status = "Y";
               }
               Deptmodel.user_code = user_codes;

               string[] result = { };
               string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(Deptmodel), "Category");
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
        public ActionResult CategoryGrid_Read([DataSourceRequest]DataSourceRequest request)
        {
            List<CategoryViewModel> objcat_lst = new List<CategoryViewModel>();
            DataTable result = new DataTable();
            CategoryViewModel CategoryList = new CategoryViewModel();
            CategoryList.category_id = 0;
            CategoryList.user_code = user_codes;
            try
            {
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(CategoryList), "CategoryList");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());


                for (int i = 0; i < result.Rows.Count; i++)
                {
                    CategoryViewModel objcat = new CategoryViewModel();
                    objcat.category_id = Convert.ToInt32(result.Rows[i][0]);
                    objcat.Category_name = result.Rows[i][1].ToString();
                    objcat.Status_desc = result.Rows[i][3].ToString();
                    objcat.Status = result.Rows[i][2].ToString();
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
        public ActionResult CategoryGrid_Update([DataSourceRequest] DataSourceRequest request, CategoryViewModel Deptmodel)
        {
            try
            {             
                Deptmodel.user_code = user_codes;
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(Deptmodel), "CategoryUpdate");
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
        public ActionResult CategoryGrid_Delete([DataSourceRequest] DataSourceRequest request, CategoryViewModel Deptmodel)
        {
            try
            {
                Deptmodel.user_code = user_codes;
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(Deptmodel), "CategoryDelete");
                result = (string[])JsonConvert.DeserializeObject(post_data, result.GetType());
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString(); LogHelper.WriteLog(ex.ToString(), control);
            }
            return View();
        }
        #endregion

        #region Responsibility
        public ActionResult ResponsibilityGrid_Save([DataSourceRequest] DataSourceRequest request, Responsibility_model Deptmodel)
        {
            try
            {
                Deptmodel.Respon_id = 0;
                if (Deptmodel.Respon_name == null)
                {
                    Deptmodel.Respon_name = "";
                }
                if (Deptmodel.Status == null)
                {
                    Deptmodel.Status = "Y";
                }
                Deptmodel.user_code = user_codes;
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(Deptmodel), "Responsibility");
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
        public ActionResult ResponsibilityGrid_Read([DataSourceRequest]DataSourceRequest request)
        {
            List<Responsibility_model> objcat_lst = new List<Responsibility_model>();
            DataTable result = new DataTable();
            Responsibility_model ResponsibilityList = new Responsibility_model();
            ResponsibilityList.Respon_id = 0;
            ResponsibilityList.user_code = user_codes;
            try
            {
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(ResponsibilityList), "ResponsibilityList");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());


                for (int i = 0; i < result.Rows.Count; i++)
                {
                    Responsibility_model objcat = new Responsibility_model();
                    objcat.Respon_id = Convert.ToInt32(result.Rows[i][0]);
                    objcat.Respon_name = result.Rows[i][1].ToString();
                    objcat.Status_desc = result.Rows[i][3].ToString();
                    objcat.Status = result.Rows[i][2].ToString();
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
        public ActionResult ResponsibilityGrid_Delete([DataSourceRequest] DataSourceRequest request, Responsibility_model Deptmodel)
        {
            try
            {
                Deptmodel.user_code = user_codes;
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(Deptmodel), "ResponsibilityDelete");
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
        public ActionResult ResponsibilityGrid_Update([DataSourceRequest] DataSourceRequest request, Responsibility_model Deptmodel)
        {
            try
            {
                Deptmodel.user_code = user_codes;
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(Deptmodel), "ResponsibilityUpdate");
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
        #endregion

        #region Supportfiletype
        public ActionResult Supportfiletype_Save([DataSourceRequest] DataSourceRequest request, CategoryViewModel Deptmodel)
        {
            try
            {
                Deptmodel.category_id = 0;
                if (Deptmodel.Category_name == null)
                {
                    Deptmodel.Category_name = "";
                }
                if (Deptmodel.Status == null)
                {
                    Deptmodel.Status = "Y";
                }
                Deptmodel.user_code = user_codes;
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(Deptmodel), "SupportfiletypeCreate");
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
        public ActionResult Supportfiletype_Read([DataSourceRequest]DataSourceRequest request)
        {
            List<CategoryViewModel> objcat_lst = new List<CategoryViewModel>();
            DataTable result = new DataTable();
            CategoryViewModel SupportfiletypeList = new CategoryViewModel();
            SupportfiletypeList.tranbrkptype_gid = 0;
            SupportfiletypeList.user_code = user_codes;
            try
            {
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(SupportfiletypeList), "SupportfiletypeList");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());

                for (int i = 0; i < result.Rows.Count; i++)
                {
                    CategoryViewModel objcat = new CategoryViewModel();
                    objcat.tranbrkptype_gid = Convert.ToInt32(result.Rows[i]["tranbrkptype_gid"]);
                    objcat.tranbrkptype_name = result.Rows[i]["tranbrkptype_name"].ToString();
                    objcat.Status_desc = result.Rows[i]["active_status_desc"].ToString();
                    objcat.Status = result.Rows[i]["active_status"].ToString();
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
        public ActionResult Supportfiletype_Update([DataSourceRequest] DataSourceRequest request, CategoryViewModel Deptmodel)
        {
            try
            {
                Deptmodel.user_code = user_codes;
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(Deptmodel), "SupportfiletypeUpdate");
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
        public ActionResult Supportfiletype_Delete([DataSourceRequest] DataSourceRequest request, CategoryViewModel Deptmodel)
        {
            try
            {
                Deptmodel.user_code = user_codes;
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(Deptmodel), "SupportfiletypeDelete");
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
        #endregion

        #region Remark
        public ActionResult RemarkGrid_Save([DataSourceRequest] DataSourceRequest request, CategoryViewModel Deptmodel)
        {
            try
            {
                Deptmodel.category_id = 0;
                if (Deptmodel.Category_name == null)
                {
                    Deptmodel.Category_name = "";
                }
                if (Deptmodel.Status == null)
                {
                    Deptmodel.Status = "Y";
                }
                Deptmodel.user_code = user_codes;
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(Deptmodel), "Remark");
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
        public ActionResult RemarkGrid_Read([DataSourceRequest]DataSourceRequest request)
        {
            List<CategoryViewModel> objcat_lst = new List<CategoryViewModel>();
            DataTable result = new DataTable();
            CategoryViewModel RemarkList = new CategoryViewModel();
            RemarkList.remark_gid = 0;
            RemarkList.user_code = user_codes;
            try
            {
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(RemarkList), "RemarkList");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());

                for (int i = 0; i < result.Rows.Count; i++)
                {
                    CategoryViewModel objcat = new CategoryViewModel();
                    objcat.remark_gid = Convert.ToInt32(result.Rows[i]["remark_gid"]);
                    objcat.remark_desc = result.Rows[i]["remark_desc"].ToString();
                    objcat.Status_desc = result.Rows[i]["active_status_desc"].ToString();
                    objcat.Status = result.Rows[i]["active_status"].ToString();
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
        public ActionResult RemarkGrid_Update([DataSourceRequest] DataSourceRequest request, CategoryViewModel Deptmodel)
        {
            try
            {
                Deptmodel.user_code = user_codes;
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(Deptmodel), "RemarkUpdate");
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
        public ActionResult RemarkGrid_Delete([DataSourceRequest] DataSourceRequest request, CategoryViewModel Deptmodel)
        {
            try
            {
                Deptmodel.user_code = user_codes;
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(Deptmodel), "RemarkDelete");
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
        public ActionResult RemarkReason(string remark1, string tran_gid)
        {
            try
            {
                CategoryViewModel Deptmodel = new CategoryViewModel();
                Deptmodel.user_code = user_codes;
                Deptmodel.remark1 = remark1;
                Deptmodel.tran_gid = Convert.ToInt32(tran_gid);
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(Deptmodel), "RemarkReason");
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
        public ActionResult RemarkReason_new(string remark1, string tran_gid, string remark2)
        {
            try
            {
                if (remark1 == "undefined")
                {
                    remark1 = "Empty";
                }

                else
                {
                    remark2 = "Empty";
                }
                
                CategoryViewModel Deptmodel = new CategoryViewModel();
                Deptmodel.user_code = user_codes;
               
                Deptmodel.remark1 = remark1;
                Deptmodel.remark2 = remark2;
                Deptmodel.tran_gid = Convert.ToInt32(tran_gid);
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(Deptmodel), "RemarkReason_new");
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
        public ActionResult Remark_drop()
        {
            List<ManualMatchoff_model> objcat_lst = new List<ManualMatchoff_model>();
            DataTable result = new DataTable();
            CategoryViewModel RemarkList = new CategoryViewModel();
            RemarkList.remark_gid = 0;
            RemarkList.user_code = user_codes;
            try
            {
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(RemarkList), "RemarkList");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());

                for (int i = 0; i < result.Rows.Count; i++)
                {
                    ManualMatchoff_model objcat = new ManualMatchoff_model();
                    objcat.remark_desc = result.Rows[i]["remark_desc"].ToString();
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

        public ActionResult getuserrecon()
        {
            try
            {

                List<ManualMatchoff_model> objcat_3st = new List<ManualMatchoff_model>();
                DataTable result2 = new DataTable();
                ManualMatchoff_model Remarklist = new ManualMatchoff_model();
                Remarklist.user_code = user_codes;
                Remarklist.active_status = "Y";
                Remarklist.recontype = null;
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(Remarklist), "Getuserrecon");
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
        #endregion

    }
}