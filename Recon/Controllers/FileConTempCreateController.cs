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
using System.Configuration;

namespace Recon.Controllers
{
    public class FileConTempCreateController : Controller
    {
        CommonController objcommon = new CommonController();
      //  string url = ConfigurationManager.AppSettings["URL"].ToString();
        string user_codes = ConfigurationManager.AppSettings["usercode"].ToString();
       // string token = ConfigurationManager.AppSettings["token"].ToString();
        // GET: FileConTempCreate
        public ActionResult FileConTempCreateView()
        {
            return View();
        }
        public ActionResult FileConTempCreate()
        {
            return View();
        }
        public ActionResult FileConTempGrid_Read([DataSourceRequest]DataSourceRequest request, int slno)   // Company  Read
        {
            List<FileConTemp_model> objcat_lst = new List<FileConTemp_model>();
            DataTable result = new DataTable();
            FileConTemp_model ObjFileTemp_Model = new FileConTemp_model();
            ObjFileTemp_Model.Template_gid = slno;
            ObjFileTemp_Model.user_code = user_codes;
            try
            {
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(ObjFileTemp_Model), "GetFieldGridDt");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());

                for (int i = 0; i < result.Rows.Count; i++)
                {
                    FileConTemp_model objcat = new FileConTemp_model();
                    objcat.FileTemplatefield_gid = Convert.ToInt64(result.Rows[i]["filetemplatefield_gid"]);
                    objcat.Template_gid = Convert.ToInt64(result.Rows[i]["filetemplate_gid"]);
                    objcat.Sourcefieldname = result.Rows[i]["excel_field"].ToString();
                    objcat.Csv_Column_No = Convert.ToInt32(result.Rows[i]["csv_column_no"]);
                    objcat.Txt_Start_Position = Convert.ToInt32(result.Rows[i]["txt_start_position"]);
                    objcat.Txt_End_Position = Convert.ToInt32(result.Rows[i]["txt_end_position"]);
                    objcat.Txt_Length = result.Rows[i]["txt_length"].ToString();
                    objcat.field_name = result.Rows[i]["tran_field"].ToString();
                    objcat.field_alias_name = result.Rows[i]["tran_field_alias_name"].ToString();
                    objcat.Field_Active_Status = result.Rows[i]["active_status"].ToString();
                    objcat.Field_Active_desc = result.Rows[i]["active_status_desc"].ToString();
                    objcat.sno = result.Rows[i]["sno"].ToString();
                    objcat.fieldmappingtype = result.Rows[i]["field_mapping_type"].ToString();
                    objcat.Mandatory_Field = result.Rows[i]["mandatory_field"].ToString();
                    string mand_field = result.Rows[i]["mandatory_field"].ToString();
                    if(mand_field=="Y")
                    {
                        objcat.Mandatory_Fielddummy = "Yes";
                    }
                    else
                    {
                        objcat.Mandatory_Fielddummy = "No";

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

        public ActionResult Filetypedrop([DataSourceRequest]DataSourceRequest request)
        {
            List<FileConTemp_model> objcatFile_list = new List<FileConTemp_model>();
            DataTable result = new DataTable();
            FileConTemp_model TemplateFileType = new FileConTemp_model();
            TemplateFileType.user_code = user_codes;
            try
            {
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(TemplateFileType), "TemplateFileType");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    FileConTemp_model objcat = new FileConTemp_model();
                    objcat.FileType = result.Rows[i][0].ToString();
                    objcat.FileType_desc = result.Rows[i][1].ToString();
                    objcatFile_list.Add(objcat);
                }
                return Json(objcatFile_list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString();LogHelper.WriteLog(ex.ToString(), control);
            }
            return View();
        }

        public ActionResult Trandatedrop([DataSourceRequest]DataSourceRequest request)
        
        {
            List<FileConTemp_model> objcatTemp_lst = new List<FileConTemp_model>();
            DataTable result = new DataTable();
            FileConTemp_model Tranformat = new FileConTemp_model();
            Tranformat.user_code = user_codes;
            try
            {
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(Tranformat), "Trandatedrop");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    FileConTemp_model objcat = new FileConTemp_model();
                    objcat.master_code = result.Rows[i][2].ToString();
                    objcat.master_name = result.Rows[i][4].ToString();
                    objcatTemp_lst.Add(objcat);
                }
                return Json(objcatTemp_lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString(); LogHelper.WriteLog(ex.ToString(), control);
            }
            return View();
        }


        public ActionResult TemplateTypedrop ([DataSourceRequest]DataSourceRequest request)
        {
            List<FileConTemp_model> objcatTemp_lst = new List<FileConTemp_model>();
            DataTable result = new DataTable();
            FileConTemp_model TemplateType = new FileConTemp_model();
            TemplateType.user_code = user_codes;
            try
            {
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(TemplateType), "TemplateType");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    FileConTemp_model objcat = new FileConTemp_model();
                    objcat.Template_type = result.Rows[i][0].ToString();
                    objcat.Template_type_desc = result.Rows[i][1].ToString();
                    objcatTemp_lst.Add(objcat);
                }
                return Json(objcatTemp_lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString();LogHelper.WriteLog(ex.ToString(), control);
            }
            return View();
        }


        public JsonResult DropDownBinding(string dropdown, string param_1)
        {
            List<FileConTemp_model> objcatTemp_lst = new List<FileConTemp_model>();
            DataTable result = new DataTable();

            try
            {
                if (dropdown == "TransactionAmount" || dropdown == "BalanceAmount")
                {
                    FileConTemp_model TemplateType = new FileConTemp_model();
                    TemplateType.user_code = user_codes;
                    TemplateType.dropdowntype = dropdown;
                    TemplateType.param_1 = param_1;
                    string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(TemplateType), "DropDownBinding");
                    result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
                    if (param_1 == "T")
                    {
                        for (int i = 0; i < result.Rows.Count; i++)
                        {
                            FileConTemp_model objcat = new FileConTemp_model();
                            objcat.Transaction_Amount_type = result.Rows[i][0].ToString();
                            objcat.Transaction_Amount_type_desc = result.Rows[i][1].ToString();
                            objcatTemp_lst.Add(objcat);
                        }
                    }
                    else if (param_1 == "B")
                    {
                        for (int i = 0; i < result.Rows.Count; i++)
                        {
                            FileConTemp_model objcat = new FileConTemp_model();
                            objcat.Balance_Amount_type = result.Rows[i][0].ToString();
                            objcat.Balance_Amount_type_desc = result.Rows[i][1].ToString();
                            objcatTemp_lst.Add(objcat);
                        }
                    }
                    return Json(objcatTemp_lst, JsonRequestBehavior.AllowGet);
                }
                else if (dropdown == "applyfilteron")
                {
                    FileConTemp_model TemplateType = new FileConTemp_model();
                    TemplateType.user_code = user_codes;
                    TemplateType.dropdowntype = dropdown;
                    TemplateType.param_1 = param_1;
                    string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(TemplateType), "DropDownBinding");
                    result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());

                    for (int i = 0; i < result.Rows.Count; i++)
                    {
                        FileConTemp_model objcat = new FileConTemp_model();
                        objcat.applyfiltercode = Convert.ToString(result.Rows[i]["appyfilter_code"]);
                        objcat.applyfilterdesc = Convert.ToString(result.Rows[i]["appyfilter_desc"]);
                        objcat.applyfilter_code = Convert.ToString(result.Rows[i]["appyfilter_code"]);
                        objcat.applyfilter_desc = Convert.ToString(result.Rows[i]["appyfilter_desc"]);
                        objcatTemp_lst.Add(objcat);
                    }

                    return Json(objcatTemp_lst, JsonRequestBehavior.AllowGet);
                }
                else if (dropdown == "jobtype")
                {
                    FileConTemp_model TemplateType = new FileConTemp_model();
                    TemplateType.user_code = user_codes;
                    TemplateType.dropdowntype = dropdown;
                    TemplateType.param_1 = param_1;
                    string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(TemplateType), "DropDownBinding");
                    result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());

                    for (int i = 0; i < result.Rows.Count; i++)
                    {
                        FileConTemp_model objcat = new FileConTemp_model();
                        objcat.jobtypecode = result.Rows[i][0].ToString();
                        objcat.jobtypedesc = result.Rows[i][1].ToString();
                        objcatTemp_lst.Add(objcat);
                    }

                    return Json(objcatTemp_lst, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString(); LogHelper.WriteLog(ex.ToString(), control);
            }
            return Json(objcatTemp_lst, JsonRequestBehavior.AllowGet);
        }


        public ActionResult DbAliasDropTemp([DataSourceRequest]DataSourceRequest request)
        {
            List<FileConTemp_model> objcatFile_list = new List<FileConTemp_model>();
            DataTable result = new DataTable();
            FileConTemp_model DbAliasDropTemp = new FileConTemp_model();
            DbAliasDropTemp.user_code = user_codes;
            try
            {
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(DbAliasDropTemp), "DbAliasDropTemp");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
                FileConTemp_model objcat = new FileConTemp_model();
                objcat.field_name = "";
                objcat.field_alias_name = "";
                objcatFile_list.Add(objcat);
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    objcat = new FileConTemp_model();
                    objcat.field_name = result.Rows[i]["field_name"].ToString();
                    objcat.field_alias_name = result.Rows[i]["field_alias_name"].ToString();
                    objcatFile_list.Add(objcat);
                }
                return Json(objcatFile_list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString();LogHelper.WriteLog(ex.ToString(), control);
            }
            return View();
        }
        public ActionResult FileConTempCreateGrid_Read([DataSourceRequest]DataSourceRequest request)
        {
            List<FileConTemp_model> objcat_lst = new List<FileConTemp_model>();
            DataTable result = new DataTable();
            FileConTemp_model FileTemplateList = new FileConTemp_model();
            FileTemplateList.user_code = user_codes;
            try
            {
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(FileTemplateList), "FileTemplateList");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    FileConTemp_model objcat = new FileConTemp_model();
                    objcat.Template_gid = Convert.ToInt64(result.Rows[i][0]);
                    objcat.Template_name = result.Rows[i][1].ToString();
                    objcat.Template_type_desc = result.Rows[i][3].ToString();
                    objcat.FileType_desc = result.Rows[i][5].ToString();
                    objcat.Csv_seperator = result.Rows[i][6].ToString();
                    objcat.Active_status = result.Rows[i]["active_status"].ToString();
                     objcat_lst.Add(objcat);

                    if (objcat.Active_status == "Y")
                    {
                        objcat.Active_status = "Active";
                    }
                    else
                    {
                        objcat.Active_status = "InActive";
                    }
                }
                return Json(objcat_lst.ToDataSourceResult(request));
            }
            catch(Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString();LogHelper.WriteLog(ex.ToString(), control);
            }
            return View();
        }
        public ActionResult FileConTempGrid_ListEdit(int id)
        {
            FileConTemp_model dedupgridlist = new FileConTemp_model();
            dedupgridlist.Template_gid = id;
            dedupgridlist.user_code = user_codes;
            List<FileConTemp_model> ListEdit = new List<FileConTemp_model>();
            DataTable result = new DataTable();
            try
            {
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(dedupgridlist), "GetTemplateFieldList");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    FileConTemp_model objcat = new FileConTemp_model();
                    objcat.Template_gid = Convert.ToInt32(result.Rows[i][0]);
                    objcat.Template_name = result.Rows[i][1].ToString();
                    objcat.Template_type = result.Rows[i][2].ToString();
                    objcat.Template_type_desc = result.Rows[i][3].ToString();
                    objcat.FileType = result.Rows[i][4].ToString();
                    objcat.FileType_desc = result.Rows[i][5].ToString();
                    objcat.Hflag = result.Rows[i]["header_flag"].ToString();
                    objcat.Acflag = result.Rows[i]["acc_no_flag"].ToString();
                    objcat.Blflag = result.Rows[i]["bal_amount_flag"].ToString();
                    objcat.Transaction_Amount_type = result.Rows[i]["tran_amount_type"].ToString();
                    objcat.Balance_Amount_type = result.Rows[i]["bal_amount_type"].ToString();
                    objcat.Transaction_Amount_type_desc = result.Rows[i]["tran_amount_type_desc"].ToString();
                    objcat.Balance_Amount_type_desc = result.Rows[i]["bal_amount_type_desc"].ToString();
                    objcat.Csv_Columns = Convert.ToInt32(result.Rows[i]["csv_columns"].ToString());
                    objcat.Csv_seperator = result.Rows[i]["csv_seperator"].ToString();
                    objcat.Active_status = result.Rows[i]["active_status"].ToString();
                    objcat.Tran_date_format = result.Rows[i]["tran_date_format"].ToString();
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

        public ActionResult FileConTemp_Create(FileConTemp_model  FileConvmodel)
        {
            FileConvmodel.user_code = user_codes;
            try
            {
                string[] result = { };
                string msg = "";
                string id = "";
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(FileConvmodel), "FileConvTempCreate");
                result = (string[])JsonConvert.DeserializeObject(post_data, result.GetType());
                msg = result[0];
                id = result[1];
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString();
                LogHelper.WriteLog(ex.ToString(), control);
            }
            return View();
        }
        public ActionResult FileConTemp_Delete(FileConTemp_model FileConvmodel)
        {
            FileConvmodel.user_code = user_codes;
            try
            {
                string[] result = { };
                string msg = "";
                string id = "";
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(FileConvmodel), "FileConvTempDelete");
                result = (string[])JsonConvert.DeserializeObject(post_data, result.GetType());
                msg = result[0];
                id = result[1];
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString(); 
                LogHelper.WriteLog(ex.ToString(), control);
            }
            return View();
        }


        public ActionResult FileConTempCreateGrid_Update(FileConTemp_model ObjFileConvTemp)
        {
            string mandatoryfile = ObjFileConvTemp.Mandatory_Field;
            if (mandatoryfile=="N")
            {
                ObjFileConvTemp.Mandatory_Field = "N";
            }
            else
            {
                ObjFileConvTemp.Mandatory_Field = "Y";
            }
          // mandatory_field;
            //ObjFileConvTemp.Field_Active_Status = "Y";
            ObjFileConvTemp.user_code = user_codes;
            ObjFileConvTemp.Template_gid = ObjFileConvTemp.Header_id;
            try
            {
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(ObjFileConvTemp), "FileConvTempFiledUpdate");
                result = (string[])JsonConvert.DeserializeObject(post_data, result.GetType());
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString();LogHelper.WriteLog(ex.ToString(), control);
            }
            return View();
        }
        public ActionResult FileConTempCreateGrid_Save(FileConTemp_model ObjFileConvTemp)
        {
            string mandatoryfile = ObjFileConvTemp.Mandatory_Field;
            if (mandatoryfile == "N")
            {
                ObjFileConvTemp.Mandatory_Field = "N";
            }
            else
            {
                ObjFileConvTemp.Mandatory_Field = "Y";
            }
            if (ObjFileConvTemp.Field_Active_Status == null)
            {
                ObjFileConvTemp.Field_Active_Status = "Y";
            }
            ObjFileConvTemp.user_code = user_codes;
            ObjFileConvTemp.Template_gid = ObjFileConvTemp.Header_id;
            try
            {
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(ObjFileConvTemp), "FileConvTempFiledCreate");
                result = (string[])JsonConvert.DeserializeObject(post_data, result.GetType());
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString();LogHelper.WriteLog(ex.ToString(), control);
            }
            return View();
        }
        public ActionResult FileConTempCreateGrid_Delete(FileConTemp_model ObjFileConvTemp)
        {
            ObjFileConvTemp.Mandatory_Field = "Y";
            //ObjFileConvTemp.Field_Active_Status  = "Y";
            ObjFileConvTemp.Template_gid = ObjFileConvTemp.Header_id;
            ObjFileConvTemp.user_code = user_codes;
            try
            {
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(ObjFileConvTemp), "FileConvTempFiledDelete");
                result = (string[])JsonConvert.DeserializeObject(post_data, result.GetType());
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString();LogHelper.WriteLog(ex.ToString(), control);
            }
            return View();
        }
   

        public ActionResult  DBFiletypedrop([DataSourceRequest]DataSourceRequest request)//drop down
        {
            List<FileConTemp_model> objcatFile_list = new List<FileConTemp_model>();
            DataTable result = new DataTable();
            FileConTemp_model GetFieldStruct = new FileConTemp_model();
            GetFieldStruct.user_code = user_codes;
            try
            {
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(GetFieldStruct), "GetFieldStruct");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    FileConTemp_model objcat = new FileConTemp_model();
                    objcat.field_name = result.Rows[i][1].ToString();
                    objcat.field_alias_name = result.Rows[i][2].ToString();
                    objcatFile_list.Add(objcat);
                }
                return Json(objcatFile_list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString();LogHelper.WriteLog(ex.ToString(), control);
            }
            return View();
        }

        public JsonResult ruleExtracioncreteriadrop(string fieldtype, string process)//drop down
        {
            try
            {
                List<Formula_model> objcat_3st = new List<Formula_model>();

                DedupeRule_model dedup = new DedupeRule_model();
                dedup.field_type = fieldtype;
                dedup.Name = process;
                dedup.user_code = user_codes;
                DataTable result2 = new DataTable();
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(dedup), "ExtractioncertiaDrop?fieldtype=" + fieldtype + "&process=" + process + "");
                result2 = (DataTable)JsonConvert.DeserializeObject(post_data, result2.GetType());
                Formula_model objcat2 = new Formula_model();
                objcat2.ExtractionCriteria = "";
                objcat_3st.Add(objcat2);

                for (int i = 0; i < result2.Rows.Count; i++)
                {
                    objcat2 = new Formula_model();
                    objcat2.ExtractionCriteria = result2.Rows[i]["condition_desc"].ToString();
                    objcat_3st.Add(objcat2);
                }
                return Json(objcat_3st, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString(); LogHelper.WriteLog(e.ToString(), control);
                return Json(e.Message);
            }
        }
   
    public JsonResult LookupMaster(string Parentsys, string mastersys,string status)//drop down
        {
            try
            {
                List<Formula_model> objcat_3st = new List<Formula_model>();

                Formula_model master_code = new Formula_model();
                master_code.parent_master_syscode = Parentsys;
                master_code.master_syscode = mastersys;
                master_code.active_status = status;
                master_code.user_code = user_codes;
                DataTable result2 = new DataTable();
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(master_code), "LookupMasterdrop");
                result2 = (DataTable)JsonConvert.DeserializeObject(post_data, result2.GetType());

                Formula_model objcat2 = new Formula_model();
                objcat2.master_gid = 0;
                objcat2.master_syscode = "";
                objcat2.master_code = "";
                objcat2.master_short_code = "";
                objcat2.master_name = "";
                objcat2.parent_master_syscode = "";
                objcat2.active_status = "";
                objcat2.active_status_desc = "";
                objcat_3st.Add(objcat2);

                for (int i = 0; i < result2.Rows.Count; i++)
                {
                    objcat2 = new Formula_model();
                    objcat2.master_gid = Convert.ToInt32(result2.Rows[i]["master_gid"]);
                    objcat2.master_syscode = result2.Rows[i]["master_syscode"].ToString();
                    objcat2.master_code = result2.Rows[i]["master_code"].ToString();
                    objcat2.master_short_code = result2.Rows[i]["master_short_code"].ToString();
                    objcat2.master_name = result2.Rows[i]["master_name"].ToString();
                    objcat2.parent_master_syscode = result2.Rows[i]["parent_master_syscode"].ToString();
                    objcat2.active_status = result2.Rows[i]["active_status"].ToString();
                    objcat2.active_status_desc = result2.Rows[i]["active_status_desc"].ToString();
                    objcat_3st.Add(objcat2);
                }
                return Json(objcat_3st, JsonRequestBehavior.AllowGet);
            }
           
         catch (Exception ex)
        {
          string control = this.ControllerContext.RouteData.Values["controller"].ToString(); LogHelper.WriteLog(ex.ToString(), control);
        return Json(ex.Message, JsonRequestBehavior.AllowGet);
        }

       
            
        }
    public ActionResult LookupMasterSave(Formula_model ObjFileConvTempFor)
    {
        if (ObjFileConvTempFor.active_status == null)
        {
            ObjFileConvTempFor.active_status = "Y";
        }
        if (ObjFileConvTempFor.extraction_filter == null)
        {
            ObjFileConvTempFor.extraction_filter = 0;
        }
        if (ObjFileConvTempFor.lookup_flag == null)
        {
            ObjFileConvTempFor.lookup_flag = "N";
        }
        if (ObjFileConvTempFor.lookup_table_code == null)
        {
            ObjFileConvTempFor.lookup_table_code = " ";
        }
        if (ObjFileConvTempFor.lookup_field == null)
        {
            ObjFileConvTempFor.lookup_field = "";
        }
        if (ObjFileConvTempFor.lookup_extraction_field == null)
        {
            ObjFileConvTempFor.lookup_extraction_field = "";
        }
        if (ObjFileConvTempFor.lookup_extraction_criteria == null)
        {
            ObjFileConvTempFor.lookup_extraction_criteria = "";
        }
        if (ObjFileConvTempFor.lookup_extraction_filter == null)
        {
            ObjFileConvTempFor.lookup_extraction_filter = 0;
        }
        if (ObjFileConvTempFor.prefix_value == null)
        {
            ObjFileConvTempFor.prefix_value = "";
        }
        if (ObjFileConvTempFor.suffix_value == null)
        {
            ObjFileConvTempFor.suffix_value = " ";
        }
        ObjFileConvTempFor.user_code = user_codes;       
        try
        {
            string[] result = { };
            string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(ObjFileConvTempFor), "LookupMasterSave");
            result = (string[])JsonConvert.DeserializeObject(post_data, result.GetType());
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            string control = this.ControllerContext.RouteData.Values["controller"].ToString(); LogHelper.WriteLog(ex.ToString(), control);
        }
        return View();
    }

    public ActionResult LookupMasterRead([DataSourceRequest]DataSourceRequest request, int filetemplatefield_gid, int filetemplatefieldformula_gid,string status)  
    {
        List<Formula_model> objcat_lst = new List<Formula_model>();
        DataTable result = new DataTable();
        Formula_model ObjFileTemp_Model = new Formula_model();
        ObjFileTemp_Model.filetemplatefield_gid = filetemplatefield_gid;
        ObjFileTemp_Model.filetemplatefieldformula_gid = filetemplatefieldformula_gid;
        ObjFileTemp_Model.active_status = status;
        ObjFileTemp_Model.user_code = user_codes;
        try
        {
            string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(ObjFileTemp_Model), "LookupMasterRead");
            result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());

            for (int i = 0; i < result.Rows.Count; i++)
            {
                Formula_model objcat = new Formula_model();
                objcat.filetemplatefieldformula_gid = Convert.ToInt32(result.Rows[i]["filetemplatefieldformula_gid"]);
                objcat.filetemplatefield_gid = Convert.ToInt32(result.Rows[i]["filetemplatefield_gid"]);
                objcat.formula_order = Convert.ToInt32(result.Rows[i]["formula_order"]);
                objcat.Sourcefieldname = result.Rows[i]["source_field"].ToString();
                objcat.source_csv_column = result.Rows[i]["source_csv_column"].ToString();
                objcat.source_txt_start = Convert.ToInt32(result.Rows[i]["source_txt_start"]);
                objcat.source_txt_end = Convert.ToInt32(result.Rows[i]["source_txt_end"]);
                objcat.ExtractionCriteria = result.Rows[i]["extraction_criteria"].ToString();
                objcat.extraction_filter = Convert.ToInt32(result.Rows[i]["extraction_filter"]);
                objcat.lookup_flag = result.Rows[i]["lookup_flag"].ToString();
                objcat.lookup_table_code = result.Rows[i]["lookup_table_code"].ToString();
                objcat.lookup_field = result.Rows[i]["lookup_field"].ToString();
                objcat.lookup_extraction_field = result.Rows[i]["lookup_extraction_field"].ToString();
                objcat.lookup_extraction_criteria = result.Rows[i]["lookup_extraction_criteria"].ToString();
                objcat.lookup_extraction_filter = Convert.ToInt32(result.Rows[i]["lookup_extraction_filter"]);
                objcat.prefix_value = result.Rows[i]["prefix_value"].ToString();
                objcat.suffix_value = result.Rows[i]["suffix_value"].ToString();
                objcat.active_status = result.Rows[i]["active_status"].ToString();
                objcat.active_status_desc = result.Rows[i]["active_status_desc"].ToString();
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
    public ActionResult LookupMasterUpdate(Formula_model ObjFileConvTempFor)
    {
        if (ObjFileConvTempFor.active_status == null)
        {
            ObjFileConvTempFor.active_status = "Y";
        }
        if (ObjFileConvTempFor.lookup_flag == null)
        {
            ObjFileConvTempFor.lookup_flag = "N";
        }
        if (ObjFileConvTempFor.lookup_table_code == null)
        {
            ObjFileConvTempFor.lookup_table_code = " ";
        }
        if (ObjFileConvTempFor.lookup_field == null)
        {
            ObjFileConvTempFor.lookup_field = "";
        }
        if (ObjFileConvTempFor.lookup_extraction_field == null)
        {
            ObjFileConvTempFor.lookup_extraction_field = "";
        }
        if (ObjFileConvTempFor.lookup_extraction_criteria == null)
        {
            ObjFileConvTempFor.lookup_extraction_criteria = "";
        }
        if (ObjFileConvTempFor.lookup_extraction_filter == null)
        {
            ObjFileConvTempFor.lookup_extraction_filter = 0;
        }
        if (ObjFileConvTempFor.prefix_value == null)
        {
            ObjFileConvTempFor.prefix_value = "";
        }
        if (ObjFileConvTempFor.suffix_value == null)
        {
            ObjFileConvTempFor.suffix_value = " ";
        }
        ObjFileConvTempFor.user_code = user_codes;
        try
        {
            string[] result = { };
            string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(ObjFileConvTempFor), "LookupMasterUpdate");
            result = (string[])JsonConvert.DeserializeObject(post_data, result.GetType());
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            string control = this.ControllerContext.RouteData.Values["controller"].ToString(); LogHelper.WriteLog(ex.ToString(), control);
        }
        return View();
    }
    public ActionResult LookupMasterDelete(Formula_model ObjFileConvTempFor)
    {       
        ObjFileConvTempFor.user_code = user_codes;
        try
        {
            string[] result = { };
            string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(ObjFileConvTempFor), "LookupMasterDelete");
            result = (string[])JsonConvert.DeserializeObject(post_data, result.GetType());
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
