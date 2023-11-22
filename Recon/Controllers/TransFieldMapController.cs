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

namespace Recon.Controllers
{
    public class TransFieldMapController : Controller
    {
        CommonController objcommon = new CommonController();
       // string url = ConfigurationManager.AppSettings["URL"].ToString();
        string user_codes = ConfigurationManager.AppSettings["usercode"].ToString();
       // string token = ConfigurationManager.AppSettings["token"].ToString();
        // GET: TransFieldMap
        public ActionResult TransFieldMapView()
        {
            return View();
        }
        public ActionResult TransFieldGrid_Read([DataSourceRequest]DataSourceRequest request)   // Company  Read
        {
            {
                List<TransFieldmap_model> objcat_lst = new List<TransFieldmap_model>();
                DataTable result = new DataTable();
                TransFieldmap_model TransFieldGridRead = new TransFieldmap_model();
                TransFieldGridRead.user_code = user_codes;
                try
                {

                    string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(TransFieldGridRead), "TransFieldGridRead");
                    result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());


                    for (int i = 0; i < result.Rows.Count; i++)
                    {
                        TransFieldmap_model objcat = new TransFieldmap_model();
                        objcat.slno = Convert.ToInt32(result.Rows[i]["fieldstru_gid"]);
                        objcat.DBfieldName = result.Rows[i]["field_name"].ToString();
                        objcat.Alias = result.Rows[i]["field_alias_name"].ToString();
                        objcat.Type = result.Rows[i]["field_type"].ToString();
                        objcat.Length = result.Rows[i]["field_length"].ToString();
                        objcat.Format = result.Rows[i]["field_format"].ToString();
                        objcat.Mandatory = result.Rows[i]["field_mapping_flag"].ToString();
                        objcat.temflag = result.Rows[i]["template_flag"].ToString();
                        string tempflag = result.Rows[i]["template_flag"].ToString();
                        if(tempflag=="Y")
                        {
                            objcat.temflag_dummy = "Yes";
                        }
                        else
                        {
                            objcat.temflag_dummy = "No";

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
        }
        public JsonResult Transfielddrop()//drop down
        {
            try
            {
                List<TransFieldmap_model> objcat_lst = new List<TransFieldmap_model>();
                DataTable result = new DataTable();
                TransFieldmap_model Transfielddrop = new TransFieldmap_model();
                Transfielddrop.user_code = user_codes;
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(Transfielddrop), "Transfielddrop");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());

                TransFieldmap_model objcat = new TransFieldmap_model();
                objcat.Type = "";
                objcat_lst.Add(objcat);
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    objcat = new TransFieldmap_model();
                    objcat.Type = result.Rows[i][0].ToString();
                    objcat_lst.Add(objcat);
                }
                return Json(objcat_lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
            }
        public ActionResult TransFieldGrid_Update(TransFieldmap_model TransField_model)
        {
            try
            {
                TransField_model.dbsource = "MYSQL";
                TransField_model.Length = "";
                TransField_model.user_code = user_codes;
                string result = "";
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(TransField_model), "TransFieldGridUpdate");
                result = (string)JsonConvert.DeserializeObject(post_data, result.GetType());
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

            }
            return View();
        }
    }
}