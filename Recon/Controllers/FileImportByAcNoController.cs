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
using System.Data.OleDb;
using System.Reflection;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic.FileIO;
using MySql.Data.MySqlClient;
using System.Net;


namespace Recon.Controllers
{
    public class FileImportByAcNoController : Controller
    {
        CommonController objcommon = new CommonController();
        // GET: FileImportByAcNo
        string url = ConfigurationManager.AppSettings["URL"].ToString();
        string user_codes = System.Web.HttpContext.Current.Session["usercode"].ToString();
        string token = System.Web.HttpContext.Current.Session["token_"].ToString();
        string ServPath = "";
        string ServerSavePath = "";
        public static string hostName = Dns.GetHostName(); // Retrive the Name of HOST 
        string ipAddress = Dns.GetHostByName(hostName).AddressList[0].ToString();
        public ActionResult FileImportByAcNo()
        {
            return View();
        }
        public ActionResult FileImport()
        {
            return View();
        }

        public ActionResult FileImportByAcNo_update()
        {
            return View();
        }
        #region DropDown


        public ActionResult GetACCNo(string AccType)
        {
            List<Recontype_model> objcat_lst = new List<Recontype_model>();
            DataTable result = new DataTable();
            Recontype_model AcMasterList = new Recontype_model();
            AcMasterList.user_code = user_codes;
            try
            {
                using (var client = new HttpClient())
                {
                    try
                    {
                        client.BaseAddress = new Uri(url);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        HttpContent content = new StringContent(JsonConvert.SerializeObject(AcMasterList), UTF8Encoding.UTF8, "application/json");
                        var response = client.PostAsync("AcMasterList", content).Result;
                        Stream data = response.Content.ReadAsStreamAsync().Result;
                        StreamReader reader = new StreamReader(data);
                        string post_data = reader.ReadToEnd();
                        result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());

                        Recontype_model objcat = new Recontype_model();
                        objcat.account1_code = "";
                        objcat.account1_desc = "";
                        objcat_lst.Add(objcat);
                        for (int i = 0; i < result.Rows.Count; i++)
                        {
                            objcat = new Recontype_model();
                            objcat.account1_code = result.Rows[i]["acc_no"].ToString();
                            objcat.account1_desc = result.Rows[i]["acc_desc"].ToString();
                            objcat_lst.Add(objcat);
                        }
                    }
                    catch (Exception ex)
                    {
                        string control = this.ControllerContext.RouteData.Values["controller"].ToString(); LogHelper.WriteLog(ex.ToString(), control);
                    }
                    return Json(objcat_lst, JsonRequestBehavior.AllowGet);
                    //return Json(result1);
                }
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString(); LogHelper.WriteLog(ex.ToString(), control);
            }
            return View();

        }


        public ActionResult FileTemplate([DataSourceRequest]DataSourceRequest request)
        {
            List<FileImport_model> objcat_lst = new List<FileImport_model>();
            DataTable result = new DataTable();
            FileImport_model FileTemplateList = new FileImport_model();
            FileTemplateList.user_code = user_codes;
            try
            {
                using (var client = new HttpClient())
                {
                    try
                    {
                        client.BaseAddress = new Uri(url);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        HttpContent content = new StringContent(JsonConvert.SerializeObject(FileTemplateList), UTF8Encoding.UTF8, "application/json");
                        var response = client.PostAsync("FileTemplateList", content).Result;
                        Stream data = response.Content.ReadAsStreamAsync().Result;
                        StreamReader reader = new StreamReader(data);
                        string post_data = reader.ReadToEnd();
                        result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());


                        FileImport_model objcat = new FileImport_model();
                        objcat.Template_gid = 0;
                        objcat.Template_name = "";
                        objcat.TemplateType_desc = "";
                        objcat_lst.Add(objcat);
                        for (int i = 0; i < result.Rows.Count; i++)
                        {
                            objcat = new FileImport_model();
                            objcat.Template_gid = Convert.ToInt64(result.Rows[i][0]);
                            objcat.Template_name = result.Rows[i][1].ToString();
                            objcat.TemplateType_desc = result.Rows[i][3].ToString();
                            objcat.FileSeperator = result.Rows[i]["csv_seperator"].ToString();
                            objcat.FileType = result.Rows[i][4].ToString();
                           // objcat.FileType_desc = result.Rows[i][5].ToString();
                            objcat.Hflag = result.Rows[i]["header_flag"].ToString();
                            objcat.Acflag = result.Rows[i]["acc_no_flag"].ToString();
                            objcat.Blflag = result.Rows[i]["bal_amount_flag"].ToString();
                            objcat.Transaction_Amount_type = result.Rows[i]["tran_amount_type"].ToString();
                            objcat.Balance_Amount_type = result.Rows[i]["bal_amount_type"].ToString();
                            objcat.Transaction_Amount_type_desc = result.Rows[i]["tran_amount_type_desc"].ToString();
                            objcat.Balance_Amount_type_desc = result.Rows[i]["bal_amount_type_desc"].ToString();
                            objcat.Csv_Columns = Convert.ToInt32(result.Rows[i]["csv_columns"].ToString());
                            objcat_lst.Add(objcat);
                        }
                    }
                    catch (Exception ex)
                    {
                        string control = this.ControllerContext.RouteData.Values["controller"].ToString(); LogHelper.WriteLog(ex.ToString(), control);
                    }
                    return Json(objcat_lst, JsonRequestBehavior.AllowGet);
                    //return Json(result1);
                }
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString(); LogHelper.WriteLog(ex.ToString(), control);
            }
            return View();
        }


        public ActionResult ReconnTypeList([DataSourceRequest]DataSourceRequest request)
        {
            List<FileImport_model> objcat_lst = new List<FileImport_model>();
            DataTable result = new DataTable();
            FileImport_model ReconNameList = new FileImport_model();
            ReconNameList.user_code = user_codes;
            try
            {
                using (var client = new HttpClient())
                {
                    try
                    {
                        client.BaseAddress = new Uri(url);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        HttpContent content = new StringContent(JsonConvert.SerializeObject(ReconNameList), UTF8Encoding.UTF8, "application/json");
                        var response = client.PostAsync("ReconNameList", content).Result;
                        Stream data = response.Content.ReadAsStreamAsync().Result;
                        StreamReader reader = new StreamReader(data);
                        string post_data = reader.ReadToEnd();
                        result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());

                        FileImport_model objcat = new FileImport_model();
                        objcat.recon_gid = 0;
                        objcat.recon_name = "";
                        objcat_lst.Add(objcat);
                        for (int i = 0; i < result.Rows.Count; i++)
                        {
                            objcat = new FileImport_model();
                            objcat.recon_gid = Convert.ToInt64(result.Rows[i][0]);
                            objcat.recon_name = result.Rows[i][1].ToString();
                            objcat_lst.Add(objcat);
                        }
                    }
                    catch (Exception ex)
                    {
                        string control = this.ControllerContext.RouteData.Values["controller"].ToString(); LogHelper.WriteLog(ex.ToString(), control);
                    }
                    return Json(objcat_lst, JsonRequestBehavior.AllowGet);
                    //return Json(result1);
                }
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString(); LogHelper.WriteLog(ex.ToString(), control);
            }
            return View();
        }
        public ActionResult GetSupportingFile([DataSourceRequest]DataSourceRequest request)
        {
            List<FileImport_model> objcat_lst = new List<FileImport_model>();
            DataTable result = new DataTable();
            FileImport_model SupportingFileList = new FileImport_model();
            SupportingFileList.user_code = user_codes;
            try
            {
                using (var client = new HttpClient())
                {
                    try
                    {
                        client.BaseAddress = new Uri(url);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        HttpContent content = new StringContent(JsonConvert.SerializeObject(SupportingFileList), UTF8Encoding.UTF8, "application/json");
                        var response = client.PostAsync("SupportingFileList", content).Result;
                        Stream data = response.Content.ReadAsStreamAsync().Result;
                        StreamReader reader = new StreamReader(data);
                        string post_data = reader.ReadToEnd();
                        result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());

                        FileImport_model objcat = new FileImport_model();
                        objcat.tranbrkptype_gid = 0;
                        objcat.tranbrkptype_name = "";
                        objcat_lst.Add(objcat);
                        for (int i = 0; i < result.Rows.Count; i++)
                        {
                            objcat = new FileImport_model();
                            objcat.tranbrkptype_gid = Convert.ToInt64(result.Rows[i][0]);
                            objcat.tranbrkptype_name = result.Rows[i][1].ToString();
                            objcat_lst.Add(objcat);
                        }
                    }
                    catch (Exception ex)
                    {
                        string control = this.ControllerContext.RouteData.Values["controller"].ToString(); LogHelper.WriteLog(ex.ToString(), control);
                    }
                    return Json(objcat_lst, JsonRequestBehavior.AllowGet);
                    //return Json(result1);
                }
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString(); LogHelper.WriteLog(ex.ToString(), control);
            }
            return View();
        }
        public ActionResult GetReconAccNo(string recon_gid)
        {
            string accname = "";
            try
            {
                List<Recontype_model> objcat_lst = new List<Recontype_model>();
                FileImport_model ObjImpModel = new FileImport_model();
                ObjImpModel.recon_gid = Convert.ToInt16(recon_gid);
                ObjImpModel.user_code = user_codes;
                using (var client = new HttpClient())
                {
                    DataTable result = new DataTable();
                    try
                    {
                        client.BaseAddress = new Uri(url);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        HttpContent content = new StringContent(JsonConvert.SerializeObject(ObjImpModel), UTF8Encoding.UTF8, "application/json");
                        var response = client.PostAsync("ReconAccNoList", content).Result;
                        Stream data = response.Content.ReadAsStreamAsync().Result;
                        StreamReader reader = new StreamReader(data);
                        string post_data = reader.ReadToEnd();
                        result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
                        Recontype_model objcat = new Recontype_model();
                        objcat.account1_code = "";
                        objcat.account1_desc = "";
                        objcat_lst.Add(objcat);
                        for (int i = 0; i < result.Rows.Count; i++)
                        {
                            objcat = new Recontype_model();
                            objcat.account1_code = result.Rows[i]["acc_no"].ToString();
                            objcat.account1_desc = result.Rows[i]["acc_desc"].ToString();
                            objcat_lst.Add(objcat);
                        }
                    }
                    catch (Exception ex)
                    {
                        string control = this.ControllerContext.RouteData.Values["controller"].ToString(); LogHelper.WriteLog(ex.ToString(), control);
                    }
                    return Json(objcat_lst, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }
        #endregion

        public ActionResult Upload_FileImport()
        {
            string result = string.Empty;
            string[] response_api = { };
            FileImport_model Fileobjmodel = new FileImport_model();
            string InputFileName = string.Empty;
            string line = string.Empty;
            string[] strArray = { };
            DataTable result_csv_field = new DataTable();
            int rdf = 0;
            //string response_api = string .Empty ;
            int lineno = 0;
            string file_gid = "";
          
            try
            {
                HttpFileCollectionBase File = Request.Files;
                string Templatedesc = Request.Form["TemplateType_desc"].ToString();
                int TemplateId = Convert.ToInt16(Request.Form["templateid"].ToString());
                string FileSeprator = Request.Form["csvseprator"].ToString();
                string AccType = Request.Form["AccType"].ToString();
                string ActionBy = user_codes;
                string ACno_By = Request.Form["ACno_By"].ToString();
                string AccNo = Request.Form["ACCNo"].ToString();
                int ReconId = Convert.ToInt16(Request.Form["ReconId"].ToString());
                int SupportId = Convert.ToInt16(Request.Form["SupportId"].ToString());
                int csvcoloumns = Convert.ToInt32(Request.Form["csv_columns"].ToString());
                string acflag = Request.Form["acflag"].ToString();
                string header_flag = Request.Form["header_flag"].ToString();

                if (Templatedesc.ToUpper() == "CSV")
                {
                    DataTable dt = new DataTable();

                    HttpPostedFileBase CSVfile = File[0];
                    string filePath = ConfigurationManager.AppSettings["FileImporte"].ToString();
                    InputFileName = Path.GetFileName(CSVfile.FileName);
                    var ServerSavePath = Path.Combine(Path.Combine(filePath, Path.GetFileName(CSVfile.FileName)));
                    string fileLocation = ServerSavePath;
                    //Delete the file if already exist in same name.
                    if (System.IO.File.Exists(fileLocation))
                    {
                        System.IO.File.Delete(fileLocation);
                    }
                    CSVfile.SaveAs(ServerSavePath);

                    FileImport_model Fileobjmodel_csv = new FileImport_model();
                    Fileobjmodel_csv.Template_id = TemplateId;
                    Fileobjmodel_csv.user_code = user_codes;
                    List<FileImport_model> Fileimport_list_csv = new List<FileImport_model>();
                    List<FileImport_model> DBFieldList = new List<FileImport_model>();
                    List<Formula_model> DBFormulaList;
                    Formula_model formula_model;
                    DataTable DtFormula;

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(url);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        HttpContent content = new StringContent(JsonConvert.SerializeObject(Fileobjmodel_csv), UTF8Encoding.UTF8, "application/json");
                        var response = client.PostAsync("GetDBFieldList", content).Result;
                        Stream data = response.Content.ReadAsStreamAsync().Result;
                        StreamReader reader = new StreamReader(data);
                        string post_data = reader.ReadToEnd();
                        result_csv_field = (DataTable)JsonConvert.DeserializeObject(post_data, result_csv_field.GetType());

                        for (int i = 0; i < result_csv_field.Rows.Count; i++)
                        {
                            FileImport_model objmodel_exfield = new FileImport_model();
                            objmodel_exfield.csv_column_no = Convert.ToInt16(result_csv_field.Rows[i]["csv_column_no"].ToString());
                            objmodel_exfield.TranField = result_csv_field.Rows[i]["tran_field"].ToString();
                            objmodel_exfield.filetemplatefield_gid = Convert.ToInt64(result_csv_field.Rows[i]["filetemplatefield_gid"].ToString());
                            objmodel_exfield.field_mapping_type = result_csv_field.Rows[i]["field_mapping_type"].ToString();
                            objmodel_exfield.field_mapping_type_desc = result_csv_field.Rows[i]["field_mapping_type_desc"].ToString();
                            //Fileimport_list.Add(objmodel_exfield);
                            if (result_csv_field.Rows[i]["field_mapping_type"].ToString() == "" || result_csv_field.Rows[i]["field_mapping_type"].ToString() == "E")
                            {
                                objmodel_exfield.ExcelFileName = result_csv_field.Rows[i]["csv_column_no"].ToString();
                            }
                            else
                            {
                                objmodel_exfield.ExcelFileName = "";

                                using (var client1 = new HttpClient())
                                {
                                    DtFormula = new DataTable();

                                    DBFormulaList = new List<Formula_model>();
                                    formula_model = new Formula_model();

                                    formula_model.filetemplatefield_gid = Convert.ToInt32(result_csv_field.Rows[i]["filetemplatefield_gid"].ToString());
                                    formula_model.filetemplatefieldformula_gid = 0;
                                    formula_model.active_status = "Y";

                                    // add formula
                                    client1.BaseAddress = new Uri(url);
                                    client1.DefaultRequestHeaders.Accept.Clear();
                                    client1.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                                    client1.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                    content = new StringContent(JsonConvert.SerializeObject(formula_model), UTF8Encoding.UTF8, "application/json");
                                    data = client1.PostAsync("FileTemplateFieldFormulaRead", content).Result.Content.ReadAsStreamAsync().Result;

                                    reader = new StreamReader(data);
                                    post_data = reader.ReadToEnd();

                                    DtFormula = (DataTable)JsonConvert.DeserializeObject(post_data, DtFormula.GetType());

                                    for (int j = 0; j < DtFormula.Rows.Count; j++)
                                    {
                                        formula_model = new Formula_model();
                                        formula_model.filetemplatefield_gid = Convert.ToInt32(DtFormula.Rows[j]["filetemplatefield_gid"].ToString());
                                        formula_model.filetemplatefieldformula_gid = Convert.ToInt32(DtFormula.Rows[j]["filetemplatefieldformula_gid"].ToString());
                                        formula_model.formula_order = Convert.ToInt32(DtFormula.Rows[j]["formula_order"]);
                                        formula_model.Sourcefieldname = DtFormula.Rows[j]["source_field"].ToString();
                                        formula_model.source_csv_column = DtFormula.Rows[j]["source_csv_column"].ToString();
                                        formula_model.source_txt_start = Convert.ToInt32(DtFormula.Rows[j]["source_txt_start"]);
                                        formula_model.source_txt_end = Convert.ToInt32(DtFormula.Rows[j]["source_txt_end"]);
                                        formula_model.ExtractionCriteria = DtFormula.Rows[j]["extraction_criteria"].ToString();
                                        formula_model.extraction_filter = Convert.ToInt32(DtFormula.Rows[j]["extraction_filter"]);
                                        formula_model.lookup_flag = DtFormula.Rows[j]["lookup_flag"].ToString();
                                        formula_model.lookup_table_code = DtFormula.Rows[j]["lookup_table_code"].ToString();
                                        formula_model.lookup_field = DtFormula.Rows[j]["lookup_field"].ToString();
                                        formula_model.lookup_extraction_field = DtFormula.Rows[j]["lookup_extraction_field"].ToString();
                                        formula_model.lookup_extraction_criteria = DtFormula.Rows[j]["lookup_extraction_criteria"].ToString();
                                        formula_model.lookup_extraction_filter = Convert.ToInt32(DtFormula.Rows[j]["lookup_extraction_filter"]);
                                        formula_model.prefix_value = DtFormula.Rows[j]["prefix_value"].ToString();
                                        formula_model.suffix_value = DtFormula.Rows[j]["suffix_value"].ToString();
                                        formula_model.active_status = DtFormula.Rows[j]["active_status"].ToString();
                                        formula_model.active_status_desc = DtFormula.Rows[j]["active_status_desc"].ToString();
                                        formula_model.sno = Convert.ToInt32(DtFormula.Rows[j]["sno"]);
                                        DBFormulaList.Add(formula_model);
                                    }
                                }
                                objmodel_exfield.formula = DBFormulaList;

                            }
                            DBFieldList.Add(objmodel_exfield);
                        }
                    }


                    int FileImportField_Count = Fileimport_list_csv.Count();
                    int CsvFieldList_Count = 0;
                    foreach (var fieldvalues in DBFieldList)
                    {
                        if (fieldvalues.field_mapping_type == "F")
                        {
                            FileImport_model objmodel_exfield = new FileImport_model();
                            objmodel_exfield.TranField = fieldvalues.TranField;
                            objmodel_exfield.ExFieldName = "FORMULA";
                            objmodel_exfield.formula = fieldvalues.formula;
                            Fileimport_list_csv.Add(objmodel_exfield);
                            CsvFieldList_Count++;
                        }
                        else if (fieldvalues.field_mapping_type == "B")
                        {
                            FileImport_model objmodel_exfield = new FileImport_model();
                            objmodel_exfield.TranField = fieldvalues.TranField;
                            objmodel_exfield.ExFieldName = "Blank";
                            objmodel_exfield.formula = fieldvalues.formula;
                            Fileimport_list_csv.Add(objmodel_exfield);
                            CsvFieldList_Count++;
                        }
                        else
                        {
                            Fileimport_list_csv.Add(fieldvalues);
                        }
                    }

                    Fileobjmodel.FileName = InputFileName;
                    Fileobjmodel.FileType = AccType;
                    Fileobjmodel.Actionby = ActionBy;
                    Fileobjmodel.user_code = user_codes;

                    //string FileInsMsg = "";
                    string[] response_file = { };
                    int fileresult = 0;
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(url);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        HttpContent content = new StringContent(JsonConvert.SerializeObject(Fileobjmodel), UTF8Encoding.UTF8, "application/json");
                        var response = client.PostAsync("InsertFileDetails", content).Result;
                        Stream data = response.Content.ReadAsStreamAsync().Result;
                        StreamReader reader = new StreamReader(data);
                        string post_data = reader.ReadToEnd();
                        response_file = (string[])JsonConvert.DeserializeObject(post_data, response_file.GetType());

                        Fileobjmodel.file_gid = response_file[0];
                        file_gid = response_file[0];
                        //FileInsMsg = response_file[1];
                        result = response_file[1];
                        fileresult = Convert.ToInt16(response_file[2]);
                    }
                    if (fileresult == 0)
                    {
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }


                    string acc_mode_flag = "";

                    foreach (var fieldvalues in Fileimport_list_csv)
                    {
                        string tranval = fieldvalues.TranField;
                        if (tranval == "amount")
                        {
                            acc_mode_flag = "NO";
                        }
                        if (acc_mode_flag == "NO")
                        {
                            if (tranval == "acc_mode")
                            {
                                acc_mode_flag = "YES";
                                break;
                            }
                        }
                    }

                    if (acc_mode_flag == "NO")
                    {

                        return Json("ACCOUNT MODE IS MUST.", JsonRequestBehavior.AllowGet);
                    }

                    int k = 0;
                    Double tranvalue_db = 0.00;

                    //Read each line in the CVS file until it's empty

                    using (TextFieldParser parser = new TextFieldParser(fileLocation))
                    {
                        parser.Delimiters = new string[] { FileSeprator };
                        while (!parser.EndOfData)
                        {
                            string[] parts = parser.ReadFields();
                            int csvfileheader_count = parts.Count();
                            if (csvfileheader_count >= FileImportField_Count)
                            {
                                foreach (var fieldvalues in Fileimport_list_csv)
                                {
                                    //string tranvalues = dtexceldata.Rows[j][i].ToString();
                                    int csvrow = fieldvalues.csv_column_no - 1;
                                    //string tranvalues = parts[csvrow].ToString().Trim();
                                    string tranval = fieldvalues.TranField;
                                    DateTime dte;
                                    string tranvalues = "";
                                    string formula_value = "";
                                    string formula_concat_value = "";
                                    string formula_blank_value = "";
                                    if (fieldvalues.ExFieldName == "FORMULA")
                                    {
                                        for (int m = 0; m < fieldvalues.formula.Count; m++)
                                        {
                                            int csvrowformula = Convert.ToInt32(fieldvalues.formula[m].source_csv_column) - 1;
                                            tranvalues = parts[csvrowformula].ToString().Trim();
                                            tranvalues = CriteriaExtract(tranvalues, fieldvalues.formula[m].ExtractionCriteria.ToString().Trim());

                                            if (fieldvalues.formula[m].extraction_filter > 0)
                                            {
                                                Common_model Commonmodel = new Common_model();
                                                Commonmodel.field_value = tranvalues;
                                                Commonmodel.filter_flag = fieldvalues.formula[m].extraction_filter;
                                                Commonmodel.user_code = user_codes;

                                                string[] result_filter = { };
                                                using (var client = new HttpClient())
                                                {
                                                    client.BaseAddress = new Uri(url);
                                                    client.DefaultRequestHeaders.Accept.Clear();
                                                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                                                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                                    HttpContent content = new StringContent(JsonConvert.SerializeObject(Commonmodel), UTF8Encoding.UTF8, "application/json");
                                                    var response = client.PostAsync("GetFilterValue", content).Result;
                                                    Stream data = response.Content.ReadAsStreamAsync().Result;
                                                    StreamReader reader = new StreamReader(data);
                                                    string post_data = reader.ReadToEnd();
                                                    result_filter = (string[])JsonConvert.DeserializeObject(post_data, result_filter.GetType());
                                                    formula_value = result_filter[0].ToString();
                                                }
                                            }

                                            if (fieldvalues.formula[m].lookup_flag.ToString() == "Y")
                                            {
                                                Master_model mastermodel = new Master_model();
                                                mastermodel.table_code = fieldvalues.formula[m].lookup_table_code.ToString();
                                                mastermodel.lookup_field = fieldvalues.formula[m].lookup_field.ToString();
                                                mastermodel.return_field = fieldvalues.formula[m].lookup_extraction_field.ToString();
                                                mastermodel.lookup_value = tranvalues;
                                                string[] result_formula = { };
                                                using (var client = new HttpClient())
                                                {
                                                    client.BaseAddress = new Uri(url);
                                                    client.DefaultRequestHeaders.Accept.Clear();
                                                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                                                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                                    HttpContent content = new StringContent(JsonConvert.SerializeObject(mastermodel), UTF8Encoding.UTF8, "application/json");
                                                    var response = client.PostAsync("GetMasterData", content).Result;
                                                    Stream data = response.Content.ReadAsStreamAsync().Result;
                                                    StreamReader reader = new StreamReader(data);
                                                    string post_data = reader.ReadToEnd();
                                                    result_formula = (string[])JsonConvert.DeserializeObject(post_data, result_formula.GetType());
                                                    formula_value = CriteriaExtract(result_formula[0].ToString(), fieldvalues.formula[m].lookup_extraction_criteria.ToString());
                                                }

                                                if (fieldvalues.formula[m].lookup_extraction_filter > 0)
                                                {
                                                    Common_model Commonmodel = new Common_model();
                                                    Commonmodel.field_value = formula_value;
                                                    Commonmodel.filter_flag = fieldvalues.formula[m].lookup_extraction_filter;
                                                    Commonmodel.user_code = user_codes;

                                                    string[] result_filter = { };
                                                    using (var client = new HttpClient())
                                                    {
                                                        client.BaseAddress = new Uri(url);
                                                        client.DefaultRequestHeaders.Accept.Clear();
                                                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                                                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                                        HttpContent content = new StringContent(JsonConvert.SerializeObject(Commonmodel), UTF8Encoding.UTF8, "application/json");
                                                        var response = client.PostAsync("GetFilterValue", content).Result;
                                                        Stream data = response.Content.ReadAsStreamAsync().Result;
                                                        StreamReader reader = new StreamReader(data);
                                                        string post_data = reader.ReadToEnd();
                                                        result_filter = (string[])JsonConvert.DeserializeObject(post_data, result_filter.GetType());
                                                        formula_value = result_filter[0].ToString();
                                                    }
                                                }

                                            }
                                            if (formula_value == "")
                                            {
                                                formula_value = tranvalues;
                                                formula_value = (fieldvalues.formula[m].prefix_value.ToString().Trim() + tranvalues + fieldvalues.formula[m].suffix_value.ToString().Trim());
                                            }
                                            else
                                            {
                                                formula_value = (fieldvalues.formula[m].prefix_value.ToString().Trim() + formula_value + fieldvalues.formula[m].suffix_value.ToString().Trim());
                                            }
                                            formula_concat_value += formula_value;
                                        }

                                        tranvalues = formula_concat_value;
                                    }
                                    else if (fieldvalues.ExFieldName == "Blank")
                                    {
                                        for (int m = 0; m < fieldvalues.formula.Count; m++)
                                        {
                                            int csvrowformula = Convert.ToInt32(fieldvalues.formula[m].source_csv_column) - 1;
                                            tranvalues = parts[csvrowformula].ToString().Trim();
                                            if (tranvalues == "")
                                            {
                                                tranvalues = CriteriaExtract(tranvalues, fieldvalues.formula[m].ExtractionCriteria.ToString().Trim());

                                                if (fieldvalues.formula[m].extraction_filter > 0)
                                                {
                                                    Common_model Commonmodel = new Common_model();
                                                    Commonmodel.field_value = tranvalues;
                                                    Commonmodel.filter_flag = fieldvalues.formula[m].extraction_filter;
                                                    Commonmodel.user_code = user_codes;

                                                    string[] result_filter = { };
                                                    using (var client = new HttpClient())
                                                    {
                                                        client.BaseAddress = new Uri(url);
                                                        client.DefaultRequestHeaders.Accept.Clear();
                                                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                                                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                                        HttpContent content = new StringContent(JsonConvert.SerializeObject(Commonmodel), UTF8Encoding.UTF8, "application/json");
                                                        var response = client.PostAsync("GetFilterValue", content).Result;
                                                        Stream data = response.Content.ReadAsStreamAsync().Result;
                                                        StreamReader reader = new StreamReader(data);
                                                        string post_data = reader.ReadToEnd();
                                                        result_filter = (string[])JsonConvert.DeserializeObject(post_data, result_filter.GetType());
                                                        formula_value = result_filter[0].ToString();
                                                    }
                                                }

                                                if (fieldvalues.formula[m].lookup_flag.ToString() == "Y")
                                                {
                                                    Master_model mastermodel = new Master_model();
                                                    mastermodel.table_code = fieldvalues.formula[m].lookup_table_code.ToString();
                                                    mastermodel.lookup_field = fieldvalues.formula[m].lookup_field.ToString();
                                                    mastermodel.return_field = fieldvalues.formula[m].lookup_extraction_field.ToString();
                                                    mastermodel.lookup_value = tranvalues;
                                                    string[] result_formula = { };
                                                    using (var client = new HttpClient())
                                                    {
                                                        client.BaseAddress = new Uri(url);
                                                        client.DefaultRequestHeaders.Accept.Clear();
                                                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                                                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                                        HttpContent content = new StringContent(JsonConvert.SerializeObject(mastermodel), UTF8Encoding.UTF8, "application/json");
                                                        var response = client.PostAsync("GetMasterData", content).Result;
                                                        Stream data = response.Content.ReadAsStreamAsync().Result;
                                                        StreamReader reader = new StreamReader(data);
                                                        string post_data = reader.ReadToEnd();
                                                        result_formula = (string[])JsonConvert.DeserializeObject(post_data, result_formula.GetType());
                                                        formula_value = CriteriaExtract(result_formula[0].ToString(), fieldvalues.formula[m].lookup_extraction_criteria.ToString());
                                                    }

                                                    if (fieldvalues.formula[m].lookup_extraction_filter > 0)
                                                    {
                                                        Common_model Commonmodel = new Common_model();
                                                        Commonmodel.field_value = formula_value;
                                                        Commonmodel.filter_flag = fieldvalues.formula[m].lookup_extraction_filter;
                                                        Commonmodel.user_code = user_codes;

                                                        string[] result_filter = { };
                                                        using (var client = new HttpClient())
                                                        {
                                                            client.BaseAddress = new Uri(url);
                                                            client.DefaultRequestHeaders.Accept.Clear();
                                                            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                                                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                                            HttpContent content = new StringContent(JsonConvert.SerializeObject(Commonmodel), UTF8Encoding.UTF8, "application/json");
                                                            var response = client.PostAsync("GetFilterValue", content).Result;
                                                            Stream data = response.Content.ReadAsStreamAsync().Result;
                                                            StreamReader reader = new StreamReader(data);
                                                            string post_data = reader.ReadToEnd();
                                                            result_filter = (string[])JsonConvert.DeserializeObject(post_data, result_filter.GetType());
                                                            formula_value = result_filter[0].ToString();
                                                        }
                                                    }

                                                }
                                                if (formula_value == "")
                                                {
                                                    formula_value = tranvalues;
                                                    formula_value = (fieldvalues.formula[m].prefix_value.ToString().Trim() + tranvalues + fieldvalues.formula[m].suffix_value.ToString().Trim());
                                                }
                                                else
                                                {
                                                    formula_value = (fieldvalues.formula[m].prefix_value.ToString().Trim() + formula_value + fieldvalues.formula[m].suffix_value.ToString().Trim());
                                                }
                                                formula_blank_value += formula_value;
                                            }
                                            else
                                            {
                                                formula_blank_value = tranvalues;
                                            }
                                            tranvalues = formula_blank_value;
                                        }
                                    }
                                    else
                                    {
                                        tranvalues = parts[csvrow].ToString().Trim();
                                    }
                                    if (tranval == "tran_date" || tranval == "value_date")
                                    {
                                        if (DateTime.TryParseExact(tranvalues, "dd-MMM-yy", null, DateTimeStyles.None, out dte) == true)
                                        {
                                            //your date is valid and is in dte
                                            tranvalues = dte.ToString("yyyy-MM-dd");
                                        }
                                        else
                                        {
                                            tranvalues = null;
                                        }
                                    }
                                    else if (tranval == "amount" || tranval == "balance_amount" || tranval == "amount_signed" || tranval == "amount_debit" || tranval == "amount_credit"
                                        || tranval == "bal_amount_signed" || tranval == "bal_amount_reverse_signed" || tranval == "amount_reverse_signed")
                                    {
                                        if (tranvalues == "") tranvalues = "0";
                                        //tranvalue_db = Convert.ToDouble(tranvalues);
                                        if (Double.TryParse(tranvalues, out tranvalue_db) == true)
                                        {
                                            tranvalues = tranvalue_db.ToString("0.00");
                                        }
                                        else
                                        {
                                            tranvalues = "0";
                                        }
                                    }

                                    Type FileImport_Type = typeof(FileImport_model);
                                    PropertyInfo ObjModel = FileImport_Type.GetProperty(tranval);
                                    ObjModel.SetValue(Fileobjmodel, tranvalues);

                                    Fileobjmodel.debug_flag = "0";
                                }
                                if (ACno_By == "Selection")
                                {
                                    Fileobjmodel.acc_no = AccNo;

                                }
                                result = "";
                                lineno++;
                                Fileobjmodel.LineNo = lineno;
                                Fileobjmodel.user_code = user_codes;
                                if (Fileobjmodel.FileType == "T")
                                {
                                    using (var client = new HttpClient())
                                    {
                                        client.BaseAddress = new Uri(url);
                                        client.DefaultRequestHeaders.Accept.Clear();
                                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                        HttpContent content = new StringContent(JsonConvert.SerializeObject(Fileobjmodel), UTF8Encoding.UTF8, "application/json");
                                        var response = client.PostAsync("InsertTranFile", content).Result;
                                        Stream data = response.Content.ReadAsStreamAsync().Result;
                                        StreamReader reader = new StreamReader(data);
                                        string post_data = reader.ReadToEnd();
                                        result = (string)JsonConvert.DeserializeObject(post_data, result.GetType());

                                    }
                                }
                                else if (Fileobjmodel.FileType == "B")
                                {
                                    using (var client = new HttpClient())
                                    {
                                        client.BaseAddress = new Uri(url);
                                        client.DefaultRequestHeaders.Accept.Clear();
                                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                        HttpContent content = new StringContent(JsonConvert.SerializeObject(Fileobjmodel), UTF8Encoding.UTF8, "application/json");
                                        var response = client.PostAsync("InsertBalFile", content).Result;
                                        Stream data = response.Content.ReadAsStreamAsync().Result;
                                        StreamReader reader = new StreamReader(data);
                                        string post_data = reader.ReadToEnd();
                                        result = (string)JsonConvert.DeserializeObject(post_data, result.GetType());

                                    }
                                }
                                else if (Fileobjmodel.FileType == "S")
                                {
                                    Fileobjmodel.recon_gid = ReconId;
                                    Fileobjmodel.tranbrkptype_gid = SupportId;

                                    using (var client = new HttpClient())
                                    {
                                        client.BaseAddress = new Uri(url);
                                        client.DefaultRequestHeaders.Accept.Clear();
                                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                        HttpContent content = new StringContent(JsonConvert.SerializeObject(Fileobjmodel), UTF8Encoding.UTF8, "application/json");
                                        var response = client.PostAsync("InsertSupportFile", content).Result;
                                        Stream data = response.Content.ReadAsStreamAsync().Result;
                                        StreamReader reader = new StreamReader(data);
                                        string post_data = reader.ReadToEnd();
                                        result = (string)JsonConvert.DeserializeObject(post_data, result.GetType());

                                    }
                                }

                                if (result == "Record saved successfully !")
                                {
                                    k++;
                                }
                            }
                            rdf++;
                        }
                    }

                    System.IO.File.Delete(fileLocation);
                    result = String.Concat("Out of ", rdf, " record(s) ", k, " record(s) imported successfully ! ");
                    return Json(result, JsonRequestBehavior.AllowGet);

                }

                else if (Templatedesc.ToUpper() == "EXCEL")
                {

                    HttpPostedFileBase excelfile = File[0];
                    DataSet ds = new DataSet();

                    if (excelfile != null && excelfile.ContentLength > 0)
                    {
                        string fileExtension = System.IO.Path.GetExtension(excelfile.FileName);//get file selected file extension.
                        string filePath = ConfigurationManager.AppSettings["FileImporte"].ToString();
                        InputFileName = Path.GetFileName(excelfile.FileName);
                        var ServerSavePath = Path.Combine(Path.Combine(filePath, Path.GetFileName(excelfile.FileName)));
                        string fileLocation = ServerSavePath;
                        //Delete the file if already exist in same name.
                        if (System.IO.File.Exists(fileLocation))
                        {
                            System.IO.File.Delete(fileLocation);
                        }
                        //Save file to server folder.  
                        excelfile.SaveAs(ServerSavePath);
                        string excelConnectionString = string.Empty;
                        //excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=PathToExcelFileWithExtension;Extended Properties='Excel 8.0;HDR=YES'";
                        if (fileExtension == ".xls")
                        {
                            excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
                        }
                        else if (fileExtension == ".xlsx")
                        {
                            excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1;MAXSCANROWS=0\"";
                            //"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";" + "Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1;MAXSCANROWS=0'";
                        }

                        OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);//connection for excel.
                        //excelConnection.Close();//excel connection close.
                        excelConnection.Open();//excel connection open.
                        DataTable dtexcel = new DataTable();//datatable object.
                        dtexcel = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        if (dtexcel == null)
                        {
                            return null;
                        }
                        String[] excelSheets = new String[dtexcel.Rows.Count];
                        int t = 0;
                        //excel data saves in temp file here.
                        foreach (DataRow row in dtexcel.Rows)
                        {
                            excelSheets[t] = row["TABLE_NAME"].ToString();
                            t++;
                        }

                        int excel = excelSheets.Count();
                        if (excel == 0)
                        {
                            result = "invalid file";
                            excelConnection.Close();
                        }
                        else
                        {
                            string query = string.Format("Select * from [{0}]", excelSheets[0]);
                            using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection))
                            {
                                dataAdapter.Fill(ds);
                            }
                            DataTable dtexceldata = new DataTable();
                            if (ds.Tables.Count > 0)
                            {
                                dtexceldata = ds.Tables[0]; //Excel Data

                                ////code for removing invalid empty rows from datatable.
                                //dtexceldata = dtexceldata.Rows.Cast<DataRow>().Where(row => !row.ItemArray.All(field =>
                                //field is System.DBNull || string.Compare((field as string).Trim(), string.Empty) == 0)).CopyToDataTable();

                                //dtexceldata = dtexceldata.Rows.Cast<DataRow>().Where(row => !row.ItemArray.All(field =>
                                //field is DBNull || string.IsNullOrWhiteSpace(field as string))).CopyToDataTable();
                            }
                            if (dtexceldata.Rows.Count == 0)
                            {
                                result = "mandatory fields empty";
                                excelConnection.Close();
                                return Json(result, JsonRequestBehavior.AllowGet);
                            }
                            FileImport_model Fileobjmodel_excl = new FileImport_model();
                            List<FileImport_model> Fileimport_list = new List<FileImport_model>();
                            List<FileImport_model> DBFieldList = new List<FileImport_model>();
                            List<Formula_model> DBFormulaList;
                            Formula_model formula_model;

                            DataTable DtFormula;

                            Fileobjmodel_excl.Template_id = TemplateId;
                            Fileobjmodel_excl.user_code = user_codes;
                            using (var client = new HttpClient())
                            {
                                DataTable FieldList = new DataTable();

                                client.BaseAddress = new Uri(url);
                                client.DefaultRequestHeaders.Accept.Clear();
                                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                HttpContent content = new StringContent(JsonConvert.SerializeObject(Fileobjmodel_excl), UTF8Encoding.UTF8, "application/json");
                                Stream data = client.PostAsync("GetDBFieldList", content).Result.Content.ReadAsStreamAsync().Result;

                                StreamReader reader = new StreamReader(data);
                                string post_data = reader.ReadToEnd();

                                FieldList = (DataTable)JsonConvert.DeserializeObject(post_data, FieldList.GetType());
                                for (int i = 0; i < FieldList.Rows.Count; i++)
                                {
                                    FileImport_model ObjModel_dbFiled = new FileImport_model();
                                    ObjModel_dbFiled.filetemplatefield_gid = Convert.ToInt64(FieldList.Rows[i]["filetemplatefield_gid"].ToString());
                                    ObjModel_dbFiled.field_mapping_type = FieldList.Rows[i]["field_mapping_type"].ToString();
                                    ObjModel_dbFiled.field_mapping_type_desc = FieldList.Rows[i]["field_mapping_type_desc"].ToString();
                                    ObjModel_dbFiled.TranField = FieldList.Rows[i]["tran_field"].ToString();

                                    if (FieldList.Rows[i]["field_mapping_type"].ToString() == "" || FieldList.Rows[i]["field_mapping_type"].ToString() == "E")
                                    {
                                        ObjModel_dbFiled.ExcelFileName = FieldList.Rows[i]["excel_field"].ToString();
                                    }
                                    else
                                    {
                                        ObjModel_dbFiled.ExcelFileName = "";

                                        using (var client1 = new HttpClient())
                                        {
                                            DtFormula = new DataTable();

                                            DBFormulaList = new List<Formula_model>();
                                            formula_model = new Formula_model();

                                            formula_model.filetemplatefield_gid = Convert.ToInt32(FieldList.Rows[i]["filetemplatefield_gid"].ToString());
                                            formula_model.filetemplatefieldformula_gid = 0;
                                            formula_model.active_status = "Y";

                                            // add formula
                                            client1.BaseAddress = new Uri(url);
                                            client1.DefaultRequestHeaders.Accept.Clear();
                                            client1.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                                            client1.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                            content = new StringContent(JsonConvert.SerializeObject(formula_model), UTF8Encoding.UTF8, "application/json");
                                            data = client1.PostAsync("FileTemplateFieldFormulaRead", content).Result.Content.ReadAsStreamAsync().Result;

                                            reader = new StreamReader(data);
                                            post_data = reader.ReadToEnd();

                                            DtFormula = (DataTable)JsonConvert.DeserializeObject(post_data, DtFormula.GetType());

                                            for (int j = 0; j < DtFormula.Rows.Count; j++)
                                            {
                                                formula_model = new Formula_model();
                                                formula_model.filetemplatefield_gid = Convert.ToInt32(DtFormula.Rows[j]["filetemplatefield_gid"].ToString());
                                                formula_model.filetemplatefieldformula_gid = Convert.ToInt32(DtFormula.Rows[j]["filetemplatefieldformula_gid"].ToString());
                                                formula_model.formula_order = Convert.ToInt32(DtFormula.Rows[j]["formula_order"]);
                                                formula_model.Sourcefieldname = DtFormula.Rows[j]["source_field"].ToString();
                                                formula_model.source_csv_column = DtFormula.Rows[j]["source_csv_column"].ToString();
                                                formula_model.source_txt_start = Convert.ToInt32(DtFormula.Rows[j]["source_txt_start"]);
                                                formula_model.source_txt_end = Convert.ToInt32(DtFormula.Rows[j]["source_txt_end"]);
                                                formula_model.ExtractionCriteria = DtFormula.Rows[j]["extraction_criteria"].ToString();
                                                formula_model.extraction_filter = Convert.ToInt32(DtFormula.Rows[j]["extraction_filter"]);
                                                formula_model.lookup_flag = DtFormula.Rows[j]["lookup_flag"].ToString();
                                                formula_model.lookup_table_code = DtFormula.Rows[j]["lookup_table_code"].ToString();
                                                formula_model.lookup_field = DtFormula.Rows[j]["lookup_field"].ToString();
                                                formula_model.lookup_extraction_field = DtFormula.Rows[j]["lookup_extraction_field"].ToString();
                                                formula_model.lookup_extraction_criteria = DtFormula.Rows[j]["lookup_extraction_criteria"].ToString();
                                                formula_model.lookup_extraction_filter = Convert.ToInt32(DtFormula.Rows[j]["lookup_extraction_filter"]);
                                                formula_model.prefix_value = DtFormula.Rows[j]["prefix_value"].ToString();
                                                formula_model.suffix_value = DtFormula.Rows[j]["suffix_value"].ToString();
                                                formula_model.active_status = DtFormula.Rows[j]["active_status"].ToString();
                                                formula_model.active_status_desc = DtFormula.Rows[j]["active_status_desc"].ToString();
                                                formula_model.sno = Convert.ToInt32(DtFormula.Rows[j]["sno"]);

                                                DBFormulaList.Add(formula_model);
                                            }
                                        }
                                        ObjModel_dbFiled.formula = DBFormulaList;

                                    }
                                    DBFieldList.Add(ObjModel_dbFiled);
                                }
                            }

                            int DBFieldList_count = DBFieldList.Count();
                            int ExcelFieldList_Count = 0;

                            for (int c = 0; c < dtexceldata.Columns.Count; c++)
                            {

                                Fileobjmodel_excl.ExFieldName = dtexceldata.Columns[c].ToString().Trim().ToUpper();
                                foreach (var fieldvalues in DBFieldList)
                                {
                                    if (fieldvalues.ExcelFileName.ToUpper() == Fileobjmodel_excl.ExFieldName && fieldvalues.ExcelFileName.ToUpper() != "")
                                    {

                                        using (var client = new HttpClient())
                                        {
                                            client.BaseAddress = new Uri(url);
                                            client.DefaultRequestHeaders.Accept.Clear();
                                            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                            HttpContent content = new StringContent(JsonConvert.SerializeObject(Fileobjmodel_excl), UTF8Encoding.UTF8, "application/json");
                                            var response = client.PostAsync("FileHeaderList", content).Result;
                                            Stream data = response.Content.ReadAsStreamAsync().Result;
                                            StreamReader reader = new StreamReader(data);
                                            string post_data = reader.ReadToEnd();
                                            response_api = (string[])JsonConvert.DeserializeObject(post_data, response_api.GetType());

                                            result = response_api[1];
                                            if (result == "Success")
                                            {
                                                FileImport_model objmodel_exfield = new FileImport_model();
                                                objmodel_exfield.TranField = response_api[0];
                                                objmodel_exfield.ExFieldName = Fileobjmodel_excl.ExFieldName;
                                                Fileimport_list.Add(objmodel_exfield);
                                                ExcelFieldList_Count++;
                                            }
                                        }

                                        if (result != "Success")
                                        {
                                            excelConnection.Close();//excel connection close.
                                            return Json(result, JsonRequestBehavior.AllowGet);
                                        }
                                        break;
                                    }
                                }
                            }

                            foreach (var fieldvalues in DBFieldList)
                            {
                                if (fieldvalues.field_mapping_type == "F")
                                {
                                    FileImport_model objmodel_exfield = new FileImport_model();
                                    objmodel_exfield.TranField = fieldvalues.TranField;
                                    objmodel_exfield.ExFieldName = "FORMULA";
                                    objmodel_exfield.formula = fieldvalues.formula;
                                    Fileimport_list.Add(objmodel_exfield);
                                    ExcelFieldList_Count++;
                                }
                                else if (fieldvalues.field_mapping_type == "B")
                                {
                                    FileImport_model objmodel_exfield = new FileImport_model();
                                    objmodel_exfield.TranField = fieldvalues.TranField;
                                    objmodel_exfield.ExFieldName = "Blank";
                                    objmodel_exfield.formula = fieldvalues.formula;
                                    Fileimport_list.Add(objmodel_exfield);
                                    ExcelFieldList_Count++;
                                }
                            }
                            if (ExcelFieldList_Count != DBFieldList_count)
                            {
                                excelConnection.Close();//excel connection close.
                                return Json("Invalid Template..!", JsonRequestBehavior.AllowGet);
                            }

                            Fileobjmodel.FileName = InputFileName;
                            Fileobjmodel.FileType = AccType;
                            Fileobjmodel.Actionby = ActionBy;
                            Fileobjmodel.user_code = user_codes;

                            //string FileInsMsg = "";
                            string[] response_file = { };
                            int fileresult = 0;

                            using (var client = new HttpClient())
                            {
                                client.BaseAddress = new Uri(url);
                                client.DefaultRequestHeaders.Accept.Clear();
                                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                HttpContent content = new StringContent(JsonConvert.SerializeObject(Fileobjmodel), UTF8Encoding.UTF8, "application/json");
                                var response = client.PostAsync("InsertFileDetails", content).Result;
                                Stream data = response.Content.ReadAsStreamAsync().Result;
                                StreamReader reader = new StreamReader(data);
                                string post_data = reader.ReadToEnd();
                                response_file = (string[])JsonConvert.DeserializeObject(post_data, response_file.GetType());

                                Fileobjmodel.file_gid = response_file[0];
                                //FileInsMsg = response_file[1];
                                file_gid = response_file[0];
                                result = response_file[1];
                                fileresult = Convert.ToInt16(response_file[2]);
                            }
                            if (fileresult == 0)
                            {
                                excelConnection.Close();//excel connection close.
                                return Json(result, JsonRequestBehavior.AllowGet);
                            }


                            string acc_mode_flag = "";

                            foreach (var fieldvalues in Fileimport_list)
                            {
                                string tranval = fieldvalues.TranField;
                                if (tranval == "amount")
                                {
                                    acc_mode_flag = "NO";
                                }

                                if (acc_mode_flag == "NO")
                                {
                                    if (tranval == "acc_mode")
                                    {
                                        acc_mode_flag = "YES";
                                        break;
                                    }
                                }
                            }

                            if (acc_mode_flag == "NO")
                            {
                                result = "ACCOUNT MODE IS MUST";
                                excelConnection.Close();//excel connection close.
                                return Json(result, JsonRequestBehavior.AllowGet);
                            }

                            String Error_msg = "";

                            int k = 0;
                            Double tranvalue_db = 0.00;
                            DateTime dte;

                            for (int j = 0; j < dtexceldata.Rows.Count; j++)
                            {
                                int i = 0;
                                string tranvalues = "";
                                string tranval = "";
                                string formula_value = "";
                                string formula_concat_value = "";
                                string formula_blank_value = "";

                                foreach (var fieldvalues in Fileimport_list)
                                {
                                    //tranvalues = dtexceldata.Rows[j][fieldvalues.ExFieldName].ToString().Trim();
                                    tranval = fieldvalues.TranField;

                                    if (fieldvalues.ExFieldName == "FORMULA")
                                    {
                                        for (int m = 0; m < fieldvalues.formula.Count; m++)
                                        {
                                            tranvalues = dtexceldata.Rows[j][fieldvalues.formula[m].Sourcefieldname].ToString().Trim();
                                            tranvalues = CriteriaExtract(tranvalues, fieldvalues.formula[m].ExtractionCriteria.ToString().Trim());

                                            if (fieldvalues.formula[m].extraction_filter > 0)
                                            {
                                                Common_model Commonmodel = new Common_model();
                                                Commonmodel.field_value = tranvalues;
                                                Commonmodel.filter_flag = fieldvalues.formula[m].extraction_filter;
                                                Commonmodel.user_code = user_codes;

                                                string[] result_filter = { };
                                                using (var client = new HttpClient())
                                                {
                                                    client.BaseAddress = new Uri(url);
                                                    client.DefaultRequestHeaders.Accept.Clear();
                                                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                                                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                                    HttpContent content = new StringContent(JsonConvert.SerializeObject(Commonmodel), UTF8Encoding.UTF8, "application/json");
                                                    var response = client.PostAsync("GetFilterValue", content).Result;
                                                    Stream data = response.Content.ReadAsStreamAsync().Result;
                                                    StreamReader reader = new StreamReader(data);
                                                    string post_data = reader.ReadToEnd();
                                                    result_filter = (string[])JsonConvert.DeserializeObject(post_data, result_filter.GetType());
                                                    formula_value = result_filter[0].ToString().Trim();
                                                }
                                            }

                                            if (fieldvalues.formula[m].lookup_flag.ToString() == "Y")
                                            {
                                                Master_model mastermodel = new Master_model();
                                                mastermodel.table_code = fieldvalues.formula[m].lookup_table_code.ToString();
                                                mastermodel.lookup_field = fieldvalues.formula[m].lookup_field.ToString();
                                                mastermodel.return_field = fieldvalues.formula[m].lookup_extraction_field.ToString();
                                                mastermodel.lookup_value = tranvalues;
                                                string[] result_formula = { };
                                                using (var client = new HttpClient())
                                                {
                                                    client.BaseAddress = new Uri(url);
                                                    client.DefaultRequestHeaders.Accept.Clear();
                                                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                                                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                                    HttpContent content = new StringContent(JsonConvert.SerializeObject(mastermodel), UTF8Encoding.UTF8, "application/json");
                                                    var response = client.PostAsync("GetMasterData", content).Result;
                                                    Stream data = response.Content.ReadAsStreamAsync().Result;
                                                    StreamReader reader = new StreamReader(data);
                                                    string post_data = reader.ReadToEnd();
                                                    result_formula = (string[])JsonConvert.DeserializeObject(post_data, result_formula.GetType());
                                                    formula_value = CriteriaExtract(result_formula[0].ToString(), fieldvalues.formula[m].lookup_extraction_criteria.ToString());
                                                }

                                                if (fieldvalues.formula[m].lookup_extraction_filter > 0)
                                                {
                                                    Common_model Commonmodel = new Common_model();
                                                    Commonmodel.field_value = formula_value;
                                                    Commonmodel.filter_flag = fieldvalues.formula[m].lookup_extraction_filter;
                                                    Commonmodel.user_code = user_codes;

                                                    string[] result_filter = { };
                                                    using (var client = new HttpClient())
                                                    {
                                                        client.BaseAddress = new Uri(url);
                                                        client.DefaultRequestHeaders.Accept.Clear();
                                                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                                                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                                        HttpContent content = new StringContent(JsonConvert.SerializeObject(Commonmodel), UTF8Encoding.UTF8, "application/json");
                                                        var response = client.PostAsync("GetFilterValue", content).Result;
                                                        Stream data = response.Content.ReadAsStreamAsync().Result;
                                                        StreamReader reader = new StreamReader(data);
                                                        string post_data = reader.ReadToEnd();
                                                        result_filter = (string[])JsonConvert.DeserializeObject(post_data, result_filter.GetType());
                                                        formula_value = result_filter[0].ToString().Trim();
                                                    }
                                                }

                                            }

                                            formula_value = (fieldvalues.formula[m].prefix_value.ToString().Trim() + formula_value + fieldvalues.formula[m].suffix_value.ToString().Trim());
                                            formula_concat_value += formula_value;
                                        }

                                        tranvalues = formula_concat_value;
                                    }
                                    else if (fieldvalues.ExFieldName == "Blank")
                                    {
                                        for (int m = 0; m < fieldvalues.formula.Count; m++)
                                        {
                                            tranvalues = dtexceldata.Rows[j][fieldvalues.formula[m].Sourcefieldname].ToString().Trim();
                                            if (tranvalues == "")
                                            {
                                                tranvalues = CriteriaExtract(tranvalues, fieldvalues.formula[m].ExtractionCriteria.ToString().Trim());

                                                if (fieldvalues.formula[m].extraction_filter > 0)
                                                {
                                                    Common_model Commonmodel = new Common_model();
                                                    Commonmodel.field_value = tranvalues;
                                                    Commonmodel.filter_flag = fieldvalues.formula[m].extraction_filter;
                                                    Commonmodel.user_code = user_codes;

                                                    string[] result_filter = { };
                                                    using (var client = new HttpClient())
                                                    {
                                                        client.BaseAddress = new Uri(url);
                                                        client.DefaultRequestHeaders.Accept.Clear();
                                                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                                                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                                        HttpContent content = new StringContent(JsonConvert.SerializeObject(Commonmodel), UTF8Encoding.UTF8, "application/json");
                                                        var response = client.PostAsync("GetFilterValue", content).Result;
                                                        Stream data = response.Content.ReadAsStreamAsync().Result;
                                                        StreamReader reader = new StreamReader(data);
                                                        string post_data = reader.ReadToEnd();
                                                        result_filter = (string[])JsonConvert.DeserializeObject(post_data, result_filter.GetType());
                                                        formula_value = result_filter[0].ToString().Trim();
                                                    }
                                                }

                                                if (fieldvalues.formula[m].lookup_flag.ToString() == "Y")
                                                {
                                                    Master_model mastermodel = new Master_model();
                                                    mastermodel.table_code = fieldvalues.formula[m].lookup_table_code.ToString();
                                                    mastermodel.lookup_field = fieldvalues.formula[m].lookup_field.ToString();
                                                    mastermodel.return_field = fieldvalues.formula[m].lookup_extraction_field.ToString();
                                                    mastermodel.lookup_value = tranvalues;
                                                    string[] result_formula = { };
                                                    using (var client = new HttpClient())
                                                    {
                                                        client.BaseAddress = new Uri(url);
                                                        client.DefaultRequestHeaders.Accept.Clear();
                                                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                                                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                                        HttpContent content = new StringContent(JsonConvert.SerializeObject(mastermodel), UTF8Encoding.UTF8, "application/json");
                                                        var response = client.PostAsync("GetMasterData", content).Result;
                                                        Stream data = response.Content.ReadAsStreamAsync().Result;
                                                        StreamReader reader = new StreamReader(data);
                                                        string post_data = reader.ReadToEnd();
                                                        result_formula = (string[])JsonConvert.DeserializeObject(post_data, result_formula.GetType());
                                                        formula_value = CriteriaExtract(result_formula[0].ToString(), fieldvalues.formula[m].lookup_extraction_criteria.ToString());
                                                    }

                                                    if (fieldvalues.formula[m].lookup_extraction_filter > 0)
                                                    {
                                                        Common_model Commonmodel = new Common_model();
                                                        Commonmodel.field_value = formula_value;
                                                        Commonmodel.filter_flag = fieldvalues.formula[m].lookup_extraction_filter;
                                                        Commonmodel.user_code = user_codes;

                                                        string[] result_filter = { };
                                                        using (var client = new HttpClient())
                                                        {
                                                            client.BaseAddress = new Uri(url);
                                                            client.DefaultRequestHeaders.Accept.Clear();
                                                            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                                                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                                            HttpContent content = new StringContent(JsonConvert.SerializeObject(Commonmodel), UTF8Encoding.UTF8, "application/json");
                                                            var response = client.PostAsync("GetFilterValue", content).Result;
                                                            Stream data = response.Content.ReadAsStreamAsync().Result;
                                                            StreamReader reader = new StreamReader(data);
                                                            string post_data = reader.ReadToEnd();
                                                            result_filter = (string[])JsonConvert.DeserializeObject(post_data, result_filter.GetType());
                                                            formula_value = result_filter[0].ToString().Trim();
                                                        }
                                                    }

                                                }

                                                formula_value = (fieldvalues.formula[m].prefix_value.ToString().Trim() + formula_value + fieldvalues.formula[m].suffix_value.ToString().Trim());
                                                formula_blank_value += formula_value;
                                            }  
                                            else{

                                                formula_blank_value = tranvalues;
                                            }
                                            tranvalues = formula_blank_value;
                                        }
                                    }
                                    else
                                    {
                                        tranvalues = dtexceldata.Rows[j][fieldvalues.ExFieldName].ToString().Trim();
                                    }

                                    if (tranval == "tran_date" || tranval == "value_date")
                                    {
                                        if (tranvalues == "" || tranvalues == null)
                                        {
                                            tranvalues = null;
                                        }
                                        else
                                        {
                                            DateTime dt = Convert.ToDateTime(tranvalues);
                                            tranvalues = dt.ToString("yyyy-MM-dd");
                                        }
                                    }
                                    else if (tranval == "amount" || tranval == "balance_amount" || tranval == "amount_signed" || tranval == "amount_debit" || tranval == "amount_credit"
                                        || tranval == "bal_amount_signed" || tranval == "bal_amount_reverse_signed" || tranval == "amount_reverse_signed")
                                    {
                                        if (tranvalues == "") tranvalues = "0";

                                        tranvalue_db = Convert.ToDouble(tranvalues);
                                        tranvalues = tranvalue_db.ToString("0.00");
                                    }

                                    Type FileImport_Type = typeof(FileImport_model);
                                    PropertyInfo ObjModel = FileImport_Type.GetProperty(tranval);
                                    ObjModel.SetValue(Fileobjmodel, tranvalues);

                                    Fileobjmodel.debug_flag = "0";
                                    i++;

                                }
                                if (ACno_By == "Selection")
                                {
                                    Fileobjmodel.acc_no = AccNo;

                                }
                                Fileobjmodel.user_code = user_codes;
                                //string responsevalues="";
                                result = "";
                                lineno++;
                                Fileobjmodel.LineNo = lineno;
                                if (Fileobjmodel.FileType == "T")
                                {
                                    using (var client = new HttpClient())
                                    {
                                        client.BaseAddress = new Uri(url);
                                        client.DefaultRequestHeaders.Accept.Clear();
                                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                        HttpContent content = new StringContent(JsonConvert.SerializeObject(Fileobjmodel), UTF8Encoding.UTF8, "application/json");
                                        var response = client.PostAsync("InsertTranFile", content).Result;
                                        Stream data = response.Content.ReadAsStreamAsync().Result;
                                        StreamReader reader = new StreamReader(data);
                                        string post_data = reader.ReadToEnd();
                                        result = (string)JsonConvert.DeserializeObject(post_data, result.GetType());
                                    }
                                }
                                else if (Fileobjmodel.FileType == "B")
                                {
                                    using (var client = new HttpClient())
                                    {
                                        client.BaseAddress = new Uri(url);
                                        client.DefaultRequestHeaders.Accept.Clear();
                                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                        HttpContent content = new StringContent(JsonConvert.SerializeObject(Fileobjmodel), UTF8Encoding.UTF8, "application/json");
                                        var response = client.PostAsync("InsertBalFile", content).Result;
                                        Stream data = response.Content.ReadAsStreamAsync().Result;
                                        StreamReader reader = new StreamReader(data);
                                        string post_data = reader.ReadToEnd();
                                        result = (string)JsonConvert.DeserializeObject(post_data, result.GetType());

                                    }
                                }
                                else if (Fileobjmodel.FileType == "S")
                                {
                                    Fileobjmodel.recon_gid = ReconId;
                                    Fileobjmodel.tranbrkptype_gid = SupportId;

                                    using (var client = new HttpClient())
                                    {
                                        client.BaseAddress = new Uri(url);
                                        client.DefaultRequestHeaders.Accept.Clear();
                                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                        HttpContent content = new StringContent(JsonConvert.SerializeObject(Fileobjmodel), UTF8Encoding.UTF8, "application/json");
                                        var response = client.PostAsync("InsertSupportFile", content).Result;
                                        Stream data = response.Content.ReadAsStreamAsync().Result;
                                        StreamReader reader = new StreamReader(data);
                                        string post_data = reader.ReadToEnd();
                                        result = (string)JsonConvert.DeserializeObject(post_data, result.GetType());

                                    }
                                }

                                if (result == "Record saved successfully !")
                                {
                                    k++;
                                }

                            }
                            excelConnection.Close();//excel connection close.
                            System.IO.File.Delete(fileLocation);
                            result = String.Concat("Out of ", dtexceldata.Rows.Count, " record(s) ", k, " record(s) imported successfully ! ");
                            return Json(result, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        result = "invalid file";

                    }
                }
                else if (Templatedesc.ToUpper() == "NOTEPAD")
                {
                    DataTable dt = new DataTable();

                    HttpPostedFileBase TextFile = File[0];
                    string filePath = ConfigurationManager.AppSettings["FileImporte"].ToString();
                    InputFileName = Path.GetFileName(TextFile.FileName);
                    var ServerSavePath = Path.Combine(Path.Combine(filePath, Path.GetFileName(TextFile.FileName)));
                    string fileLocation = ServerSavePath;
                    //Delete the file if already exist in same name.
                    if (System.IO.File.Exists(fileLocation))
                    {
                        System.IO.File.Delete(fileLocation);
                    }
                    TextFile.SaveAs(ServerSavePath);

                    StreamReader str = new StreamReader(fileLocation);
                    line = str.ReadLine();
                    int Textlength = line.Length;

                    FileImport_model Fileobjmodel_csv = new FileImport_model();
                    Fileobjmodel_csv.Template_id = TemplateId;
                    Fileobjmodel_csv.user_code = user_codes;
                    List<FileImport_model> Fileimport_list_text = new List<FileImport_model>();
                    List<FileImport_model> DBFieldList = new List<FileImport_model>();
                    List<Formula_model> DBFormulaList;
                    Formula_model formula_model;

                    DataTable DtFormula;

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(url);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        HttpContent content = new StringContent(JsonConvert.SerializeObject(Fileobjmodel_csv), UTF8Encoding.UTF8, "application/json");
                        var response = client.PostAsync("GetDBFieldList", content).Result;
                        Stream data = response.Content.ReadAsStreamAsync().Result;
                        StreamReader reader = new StreamReader(data);
                        string post_data = reader.ReadToEnd();
                        result_csv_field = (DataTable)JsonConvert.DeserializeObject(post_data, result_csv_field.GetType());

                        for (int i = 0; i < result_csv_field.Rows.Count; i++)
                        {
                            FileImport_model objmodel_exfield = new FileImport_model();
                            objmodel_exfield.Txt_start_position = Convert.ToInt16(result_csv_field.Rows[i]["txt_start_position"].ToString());
                            objmodel_exfield.Txt_end_position = Convert.ToInt16(result_csv_field.Rows[i]["txt_end_position"].ToString());
                            objmodel_exfield.TxtLenght = Convert.ToInt16(result_csv_field.Rows[i]["txt_length"].ToString());
                            objmodel_exfield.TranField = result_csv_field.Rows[i]["tran_field"].ToString();
                            //Fileimport_list.Add(objmodel_exfield);
                            objmodel_exfield.filetemplatefield_gid = Convert.ToInt64(result_csv_field.Rows[i]["filetemplatefield_gid"].ToString());
                            objmodel_exfield.field_mapping_type = result_csv_field.Rows[i]["field_mapping_type"].ToString();
                            objmodel_exfield.field_mapping_type_desc = result_csv_field.Rows[i]["field_mapping_type_desc"].ToString();

                            if (result_csv_field.Rows[i]["field_mapping_type"].ToString() == "" || result_csv_field.Rows[i]["field_mapping_type"].ToString() == "E")
                            {
                                objmodel_exfield.ExcelFileName = result_csv_field.Rows[i]["txt_start_position"].ToString();
                            }
                            else
                            {
                                objmodel_exfield.ExcelFileName = "";

                                using (var client1 = new HttpClient())
                                {
                                    DtFormula = new DataTable();

                                    DBFormulaList = new List<Formula_model>();
                                    formula_model = new Formula_model();

                                    formula_model.filetemplatefield_gid = Convert.ToInt32(result_csv_field.Rows[i]["filetemplatefield_gid"].ToString());
                                    formula_model.filetemplatefieldformula_gid = 0;
                                    formula_model.active_status = "Y";

                                    // add formula
                                    client1.BaseAddress = new Uri(url);
                                    client1.DefaultRequestHeaders.Accept.Clear();
                                    client1.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                                    client1.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                    content = new StringContent(JsonConvert.SerializeObject(formula_model), UTF8Encoding.UTF8, "application/json");
                                    data = client1.PostAsync("FileTemplateFieldFormulaRead", content).Result.Content.ReadAsStreamAsync().Result;

                                    reader = new StreamReader(data);
                                    post_data = reader.ReadToEnd();

                                    DtFormula = (DataTable)JsonConvert.DeserializeObject(post_data, DtFormula.GetType());

                                    for (int j = 0; j < DtFormula.Rows.Count; j++)
                                    {
                                        formula_model = new Formula_model();
                                        formula_model.filetemplatefield_gid = Convert.ToInt32(DtFormula.Rows[j]["filetemplatefield_gid"].ToString());
                                        formula_model.filetemplatefieldformula_gid = Convert.ToInt32(DtFormula.Rows[j]["filetemplatefieldformula_gid"].ToString());
                                        formula_model.formula_order = Convert.ToInt32(DtFormula.Rows[j]["formula_order"]);
                                        formula_model.Sourcefieldname = DtFormula.Rows[j]["source_field"].ToString();
                                        formula_model.source_csv_column = DtFormula.Rows[j]["source_csv_column"].ToString();
                                        formula_model.source_txt_start = Convert.ToInt32(DtFormula.Rows[j]["source_txt_start"]);
                                        formula_model.source_txt_end = Convert.ToInt32(DtFormula.Rows[j]["source_txt_end"]);
                                        formula_model.ExtractionCriteria = DtFormula.Rows[j]["extraction_criteria"].ToString();
                                        formula_model.extraction_filter = Convert.ToInt32(DtFormula.Rows[j]["extraction_filter"]);
                                        formula_model.lookup_flag = DtFormula.Rows[j]["lookup_flag"].ToString();
                                        formula_model.lookup_table_code = DtFormula.Rows[j]["lookup_table_code"].ToString();
                                        formula_model.lookup_field = DtFormula.Rows[j]["lookup_field"].ToString();
                                        formula_model.lookup_extraction_field = DtFormula.Rows[j]["lookup_extraction_field"].ToString();
                                        formula_model.lookup_extraction_criteria = DtFormula.Rows[j]["lookup_extraction_criteria"].ToString();
                                        formula_model.lookup_extraction_filter = Convert.ToInt32(DtFormula.Rows[j]["lookup_extraction_filter"]);
                                        formula_model.prefix_value = DtFormula.Rows[j]["prefix_value"].ToString();
                                        formula_model.suffix_value = DtFormula.Rows[j]["suffix_value"].ToString();
                                        formula_model.active_status = DtFormula.Rows[j]["active_status"].ToString();
                                        formula_model.active_status_desc = DtFormula.Rows[j]["active_status_desc"].ToString();
                                        formula_model.sno = Convert.ToInt32(DtFormula.Rows[j]["sno"]);
                                        formula_model.TxtLenght = Convert.ToInt16(DtFormula.Rows[j]["txt_length"].ToString());

                                        DBFormulaList.Add(formula_model);
                                    }
                                }
                                objmodel_exfield.formula = DBFormulaList;

                            }
                            DBFieldList.Add(objmodel_exfield);
                        }
                    }
                    int MaxEndPosition = 0;
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(url);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        HttpContent content = new StringContent(JsonConvert.SerializeObject(Fileobjmodel_csv), UTF8Encoding.UTF8, "application/json");
                        var response = client.PostAsync("GetMaxEndPosition", content).Result;
                        Stream data = response.Content.ReadAsStreamAsync().Result;
                        StreamReader reader = new StreamReader(data);
                        string post_data = reader.ReadToEnd();
                        result = (string)JsonConvert.DeserializeObject(post_data, result.GetType());
                        MaxEndPosition = Convert.ToInt16(result.ToString());
                    }
                    int FileImportField_Count = Fileimport_list_text.Count();
                    int CsvFieldList_Count = 0;
                    foreach (var fieldvalues in DBFieldList)
                    {
                        if (fieldvalues.field_mapping_type == "F")
                        {
                            FileImport_model objmodel_exfield = new FileImport_model();
                            objmodel_exfield.TranField = fieldvalues.TranField;
                            objmodel_exfield.ExFieldName = "FORMULA";
                            objmodel_exfield.formula = fieldvalues.formula;
                            Fileimport_list_text.Add(objmodel_exfield);
                            CsvFieldList_Count++;
                        }
                        else if (fieldvalues.field_mapping_type == "B")
                        {
                            FileImport_model objmodel_exfield = new FileImport_model();
                            objmodel_exfield.TranField = fieldvalues.TranField;
                            objmodel_exfield.ExFieldName = "Blank";
                            objmodel_exfield.formula = fieldvalues.formula;
                            Fileimport_list_text.Add(objmodel_exfield);
                            CsvFieldList_Count++;
                        }
                        else
                        {
                            Fileimport_list_text.Add(fieldvalues);
                        }
                    }
                    Fileobjmodel.FileName = InputFileName;
                    Fileobjmodel.FileType = AccType;
                    Fileobjmodel.Actionby = ActionBy;
                    Fileobjmodel.user_code = user_codes;
                    //string FileInsMsg = "";
                    string[] response_file = { };
                    int fileresult = 0;
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(url);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        HttpContent content = new StringContent(JsonConvert.SerializeObject(Fileobjmodel), UTF8Encoding.UTF8, "application/json");
                        var response = client.PostAsync("InsertFileDetails", content).Result;
                        Stream data = response.Content.ReadAsStreamAsync().Result;
                        StreamReader reader = new StreamReader(data);
                        string post_data = reader.ReadToEnd();
                        response_file = (string[])JsonConvert.DeserializeObject(post_data, response_file.GetType());

                        Fileobjmodel.file_gid = response_file[0];
                        //FileInsMsg = response_file[1];
                        file_gid = response_file[0];
                        result = response_file[1];
                        fileresult = Convert.ToInt16(response_file[2]);
                    }
                    if (fileresult == 0)
                    {
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    string acc_mode_flag = "";

                    foreach (var fieldvalues in Fileimport_list_text)
                    {
                        string tranval = fieldvalues.TranField;
                        if (tranval == "amount")
                        {
                            acc_mode_flag = "NO";
                        }
                        if (acc_mode_flag == "NO")
                        {
                            if (tranval == "acc_mode")
                            {
                                acc_mode_flag = "YES";
                                break;
                            }
                        }
                    }

                    if (acc_mode_flag == "NO")
                    {

                        return Json("ACCOUNT MODE IS MUST.", JsonRequestBehavior.AllowGet);
                    }

                    int k = 0;
                    Double tranvalue_db = 0.00;
                    using (StreamReader file = new StreamReader(fileLocation))
                    {
                        string rowvalues;

                        while ((rowvalues = file.ReadLine()) != null)
                        {
                            int rowvalue_length = rowvalues.Length;
                            if (rowvalue_length >= MaxEndPosition)
                            {
                                foreach (var fieldvalues in Fileimport_list_text)
                                {
                                    //string tranvalues = rowvalues.Substring(fieldvalues.Txt_start_position - 1, fieldvalues.TxtLenght).ToString().Trim();
                                    string tranval = fieldvalues.TranField;
                                    DateTime dte;
                                    string tranvalues = "";
                                    string formula_value = "";
                                    string formula_concat_value = "";
                                    string formula_blank_value = "";
                                    if (fieldvalues.ExFieldName == "FORMULA")
                                    {
                                        for (int m = 0; m < fieldvalues.formula.Count; m++)
                                        {
                                            tranvalues = rowvalues.Substring(fieldvalues.formula[m].source_txt_start - 1, fieldvalues.formula[m].TxtLenght).ToString().Trim();
                                            tranvalues = CriteriaExtract(tranvalues, fieldvalues.formula[m].ExtractionCriteria.ToString().Trim());

                                            if (fieldvalues.formula[m].extraction_filter > 0)
                                            {
                                                Common_model Commonmodel = new Common_model();
                                                Commonmodel.field_value = tranvalues;
                                                Commonmodel.filter_flag = fieldvalues.formula[m].extraction_filter;
                                                Commonmodel.user_code = user_codes;

                                                string[] result_filter = { };
                                                using (var client = new HttpClient())
                                                {
                                                    client.BaseAddress = new Uri(url);
                                                    client.DefaultRequestHeaders.Accept.Clear();
                                                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                                                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                                    HttpContent content = new StringContent(JsonConvert.SerializeObject(Commonmodel), UTF8Encoding.UTF8, "application/json");
                                                    var response = client.PostAsync("GetFilterValue", content).Result;
                                                    Stream data = response.Content.ReadAsStreamAsync().Result;
                                                    StreamReader reader = new StreamReader(data);
                                                    string post_data = reader.ReadToEnd();
                                                    result_filter = (string[])JsonConvert.DeserializeObject(post_data, result_filter.GetType());
                                                    formula_value = result_filter[0].ToString();
                                                }
                                            }

                                            if (fieldvalues.formula[m].lookup_flag.ToString() == "Y")
                                            {
                                                Master_model mastermodel = new Master_model();
                                                mastermodel.table_code = fieldvalues.formula[m].lookup_table_code.ToString();
                                                mastermodel.lookup_field = fieldvalues.formula[m].lookup_field.ToString();
                                                mastermodel.return_field = fieldvalues.formula[m].lookup_extraction_field.ToString();
                                                mastermodel.lookup_value = tranvalues;
                                                string[] result_formula = { };
                                                using (var client = new HttpClient())
                                                {
                                                    client.BaseAddress = new Uri(url);
                                                    client.DefaultRequestHeaders.Accept.Clear();
                                                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                                                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                                    HttpContent content = new StringContent(JsonConvert.SerializeObject(mastermodel), UTF8Encoding.UTF8, "application/json");
                                                    var response = client.PostAsync("GetMasterData", content).Result;
                                                    Stream data = response.Content.ReadAsStreamAsync().Result;
                                                    StreamReader reader = new StreamReader(data);
                                                    string post_data = reader.ReadToEnd();
                                                    result_formula = (string[])JsonConvert.DeserializeObject(post_data, result_formula.GetType());
                                                    formula_value = CriteriaExtract(result_formula[0].ToString(), fieldvalues.formula[m].lookup_extraction_criteria.ToString());
                                                }

                                                if (fieldvalues.formula[m].lookup_extraction_filter > 0)
                                                {
                                                    Common_model Commonmodel = new Common_model();
                                                    Commonmodel.field_value = formula_value;
                                                    Commonmodel.filter_flag = fieldvalues.formula[m].lookup_extraction_filter;
                                                    Commonmodel.user_code = user_codes;

                                                    string[] result_filter = { };
                                                    using (var client = new HttpClient())
                                                    {
                                                        client.BaseAddress = new Uri(url);
                                                        client.DefaultRequestHeaders.Accept.Clear();
                                                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                                                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                                        HttpContent content = new StringContent(JsonConvert.SerializeObject(Commonmodel), UTF8Encoding.UTF8, "application/json");
                                                        var response = client.PostAsync("GetFilterValue", content).Result;
                                                        Stream data = response.Content.ReadAsStreamAsync().Result;
                                                        StreamReader reader = new StreamReader(data);
                                                        string post_data = reader.ReadToEnd();
                                                        result_filter = (string[])JsonConvert.DeserializeObject(post_data, result_filter.GetType());
                                                        formula_value = result_filter[0].ToString();
                                                    }
                                                }

                                            }
                                            if (formula_value == "")
                                            {
                                                formula_value = tranvalues;
                                                formula_value = (fieldvalues.formula[m].prefix_value.ToString().Trim() + tranvalues + fieldvalues.formula[m].suffix_value.ToString().Trim());
                                            }
                                            else
                                            {
                                                formula_value = (fieldvalues.formula[m].prefix_value.ToString().Trim() + formula_value + fieldvalues.formula[m].suffix_value.ToString().Trim());
                                            }
                                            formula_concat_value += formula_value;
                                        }

                                        tranvalues = formula_concat_value;
                                    }
                                 else if (fieldvalues.ExFieldName == "Blank")
                                    {
                                      for (int m = 0; m < fieldvalues.formula.Count; m++)
                                        {
                                            tranvalues = rowvalues.Substring(fieldvalues.formula[m].source_txt_start - 1, fieldvalues.formula[m].TxtLenght).ToString().Trim();
                                           
                                            if (tranvalues == "")
                                            {
                                                tranvalues = CriteriaExtract(tranvalues, fieldvalues.formula[m].ExtractionCriteria.ToString().Trim());
                                            if (fieldvalues.formula[m].extraction_filter > 0)
                                            {
                                                Common_model Commonmodel = new Common_model();
                                                Commonmodel.field_value = tranvalues;
                                                Commonmodel.filter_flag = fieldvalues.formula[m].extraction_filter;
                                                Commonmodel.user_code = user_codes;

                                                string[] result_filter = { };
                                                using (var client = new HttpClient())
                                                {
                                                    client.BaseAddress = new Uri(url);
                                                    client.DefaultRequestHeaders.Accept.Clear();
                                                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                                                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                                    HttpContent content = new StringContent(JsonConvert.SerializeObject(Commonmodel), UTF8Encoding.UTF8, "application/json");
                                                    var response = client.PostAsync("GetFilterValue", content).Result;
                                                    Stream data = response.Content.ReadAsStreamAsync().Result;
                                                    StreamReader reader = new StreamReader(data);
                                                    string post_data = reader.ReadToEnd();
                                                    result_filter = (string[])JsonConvert.DeserializeObject(post_data, result_filter.GetType());
                                                    formula_value = result_filter[0].ToString();
                                                }
                                            }

                                            if (fieldvalues.formula[m].lookup_flag.ToString() == "Y")
                                            {
                                                Master_model mastermodel = new Master_model();
                                                mastermodel.table_code = fieldvalues.formula[m].lookup_table_code.ToString();
                                                mastermodel.lookup_field = fieldvalues.formula[m].lookup_field.ToString();
                                                mastermodel.return_field = fieldvalues.formula[m].lookup_extraction_field.ToString();
                                                mastermodel.lookup_value = tranvalues;
                                                string[] result_formula = { };
                                                using (var client = new HttpClient())
                                                {
                                                    client.BaseAddress = new Uri(url);
                                                    client.DefaultRequestHeaders.Accept.Clear();
                                                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                                                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                                    HttpContent content = new StringContent(JsonConvert.SerializeObject(mastermodel), UTF8Encoding.UTF8, "application/json");
                                                    var response = client.PostAsync("GetMasterData", content).Result;
                                                    Stream data = response.Content.ReadAsStreamAsync().Result;
                                                    StreamReader reader = new StreamReader(data);
                                                    string post_data = reader.ReadToEnd();
                                                    result_formula = (string[])JsonConvert.DeserializeObject(post_data, result_formula.GetType());
                                                    formula_value = CriteriaExtract(result_formula[0].ToString(), fieldvalues.formula[m].lookup_extraction_criteria.ToString());
                                                }

                                                if (fieldvalues.formula[m].lookup_extraction_filter > 0)
                                                {
                                                    Common_model Commonmodel = new Common_model();
                                                    Commonmodel.field_value = formula_value;
                                                    Commonmodel.filter_flag = fieldvalues.formula[m].lookup_extraction_filter;
                                                    Commonmodel.user_code = user_codes;

                                                    string[] result_filter = { };
                                                    using (var client = new HttpClient())
                                                    {
                                                        client.BaseAddress = new Uri(url);
                                                        client.DefaultRequestHeaders.Accept.Clear();
                                                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                                                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                                        HttpContent content = new StringContent(JsonConvert.SerializeObject(Commonmodel), UTF8Encoding.UTF8, "application/json");
                                                        var response = client.PostAsync("GetFilterValue", content).Result;
                                                        Stream data = response.Content.ReadAsStreamAsync().Result;
                                                        StreamReader reader = new StreamReader(data);
                                                        string post_data = reader.ReadToEnd();
                                                        result_filter = (string[])JsonConvert.DeserializeObject(post_data, result_filter.GetType());
                                                        formula_value = result_filter[0].ToString();
                                                    }
                                                }
                                            }
                                            if (formula_value == "")
                                            {
                                                formula_value = tranvalues;
                                                formula_value = (fieldvalues.formula[m].prefix_value.ToString().Trim() + tranvalues + fieldvalues.formula[m].suffix_value.ToString().Trim());
                                            }
                                            else
                                            {
                                                formula_value = (fieldvalues.formula[m].prefix_value.ToString().Trim() + formula_value + fieldvalues.formula[m].suffix_value.ToString().Trim());
                                            }
                                            formula_blank_value  += formula_value;
                                        }
                                            else
                                            {
                                                formula_blank_value = tranvalues;
                                            }
                                            tranvalues = formula_blank_value;
                                        }
                                    }
                                    else
                                    {
                                        tranvalues = rowvalues.Substring(fieldvalues.Txt_start_position - 1, fieldvalues.TxtLenght).ToString().Trim();
                                    }
                                    if (tranval == "tran_date" || tranval == "value_date")
                                    {
                                        if (DateTime.TryParseExact(tranvalues, "dd-MMM-yy", null, DateTimeStyles.None, out dte) == true)
                                        {
                                            //your date is valid and is in dte
                                            tranvalues = dte.ToString("yyyy-MM-dd");
                                        }
                                        else
                                        {
                                            tranvalues = null;
                                        }
                                    }
                                    else if (tranval == "amount" || tranval == "balance_amount" || tranval == "amount_signed" || tranval == "amount_debit" || tranval == "amount_credit"
                                        || tranval == "bal_amount_signed" || tranval == "bal_amount_reverse_signed" || tranval == "amount_reverse_signed")
                                    {
                                        if (tranvalues == "") tranvalues = "0";
                                        //tranvalue_db = Convert.ToDouble(tranvalues);
                                        if (Double.TryParse(tranvalues, out tranvalue_db) == true)
                                        {
                                            tranvalues = tranvalue_db.ToString("0.00");
                                        }
                                        else
                                        {
                                            tranvalues = "0";
                                        }
                                    }

                                    Type FileImport_Type = typeof(FileImport_model);
                                    PropertyInfo ObjModel = FileImport_Type.GetProperty(tranval);
                                    ObjModel.SetValue(Fileobjmodel, tranvalues);

                                    Fileobjmodel.debug_flag = "0";
                                }

                                if (ACno_By == "Selection")
                                {
                                    Fileobjmodel.acc_no = AccNo;

                                }
                                Fileobjmodel.user_code = user_codes;
                                result = "";
                                lineno++;
                                Fileobjmodel.LineNo = lineno;
                                if (Fileobjmodel.FileType == "T")
                                {
                                    using (var client = new HttpClient())
                                    {
                                        client.BaseAddress = new Uri(url);
                                        client.DefaultRequestHeaders.Accept.Clear();
                                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                        HttpContent content = new StringContent(JsonConvert.SerializeObject(Fileobjmodel), UTF8Encoding.UTF8, "application/json");
                                        var response = client.PostAsync("InsertTranFile", content).Result;
                                        Stream data = response.Content.ReadAsStreamAsync().Result;
                                        StreamReader reader = new StreamReader(data);
                                        string post_data = reader.ReadToEnd();
                                        result = (string)JsonConvert.DeserializeObject(post_data, result.GetType());

                                    }
                                }
                                else if (Fileobjmodel.FileType == "B")
                                {
                                    using (var client = new HttpClient())
                                    {
                                        client.BaseAddress = new Uri(url);
                                        client.DefaultRequestHeaders.Accept.Clear();
                                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                        HttpContent content = new StringContent(JsonConvert.SerializeObject(Fileobjmodel), UTF8Encoding.UTF8, "application/json");
                                        var response = client.PostAsync("InsertBalFile", content).Result;
                                        Stream data = response.Content.ReadAsStreamAsync().Result;
                                        StreamReader reader = new StreamReader(data);
                                        string post_data = reader.ReadToEnd();
                                        result = (string)JsonConvert.DeserializeObject(post_data, result.GetType());

                                    }
                                }
                                else if (Fileobjmodel.FileType == "S")
                                {
                                    Fileobjmodel.recon_gid = ReconId;
                                    Fileobjmodel.tranbrkptype_gid = SupportId;

                                    using (var client = new HttpClient())
                                    {
                                        client.BaseAddress = new Uri(url);
                                        client.DefaultRequestHeaders.Accept.Clear();
                                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                        HttpContent content = new StringContent(JsonConvert.SerializeObject(Fileobjmodel), UTF8Encoding.UTF8, "application/json");
                                        var response = client.PostAsync("InsertSupportFile", content).Result;
                                        Stream data = response.Content.ReadAsStreamAsync().Result;
                                        StreamReader reader = new StreamReader(data);
                                        string post_data = reader.ReadToEnd();
                                        result = (string)JsonConvert.DeserializeObject(post_data, result.GetType());

                                    }
                                }

                                if (result == "Record saved successfully !")
                                {
                                    k++;
                                }
                            }
                            rdf++;
                        }

                        file.Close();
                        //System.IO.File.Delete(fileLocation);
                    }
                    
                    result = String.Concat("Out of ", rdf, " record(s) ", k, " record(s) imported successfully ! ");
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString(); LogHelper.WriteLog(ex.ToString(), control);
                string error = ex.ToString();
                FileImport_model errorline = new FileImport_model();
                errorline.LineNo = lineno;
                errorline.file_gid = file_gid;
                errorline.error_line = error;
                string[] result1 = { };
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(errorline), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("Importerrorline", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    string post_data = reader.ReadToEnd();
                    result1 = (string[])JsonConvert.DeserializeObject(post_data, result.GetType());
                    return Json(result1, JsonRequestBehavior.AllowGet);
                }
            }
           
        }

        private string CriteriaExtract(string txt, string criteria)
        {
            string extract = criteria;
            int n = 0;
            int c = 0, m = 0, start_position = 0, end_position = 0, length = 0;

            if (criteria.Contains("(") && criteria.Contains(")"))
            {
                n = criteria.IndexOf("(");
                m = criteria.IndexOf(")");
                c = criteria.IndexOf(",");

                if (n > 0)
                {
                    extract = criteria.Substring(0, n);

                    if (c < 0)
                    {
                        length = Convert.ToInt16(criteria.Substring(n + 1, m - n - 1));
                    }
                    else
                    {
                        start_position = Convert.ToInt16(criteria.Substring(n + 1, c - n - 1));
                        end_position = Convert.ToInt16(criteria.Substring(c + 1, m - c - 1));
                    }
                }
            }

            switch (extract.ToUpper())
            {
                case "EXACT":
                    return txt;
                case "LEFT":
                    return txt.Substring(0, length);
                case "RIGHT":
                    return txt.Substring(txt.Length - length);
                case "SUBSTR":
                    return txt.Substring(start_position - 1, end_position);
                default:
                    return txt;
            }
        }


        public ActionResult Upload_FileImportTest()
        {
            try
            {


                string result = string.Empty;
                string res = string.Empty;
                string[] response_api = { };
                FileImport_model Fileobjmodel = new FileImport_model();
                string AccType = Request.Form["AccType"].ToString();
                int ReconId = Convert.ToInt16(Request.Form["ReconId"].ToString());
                int SupportId = Convert.ToInt16(Request.Form["SupportId"].ToString());
                // int noofcolumns = Convert.ToInt16(Request.Form["Noofcolumn"].ToString());

                string Templatedesc = Request.Form["TemplateType_desc"].ToString();
                int TemplateId = Convert.ToInt16(Request.Form["templateid"].ToString());
                string FileSeprator = Request.Form["csvseprator"].ToString();
                //string AccType = Request.Form["AccType"].ToString();
                //string ActionBy = user_codes;
                string ACno_By = Request.Form["ACno_By"].ToString();
                string AccNo = Request.Form["ACCNo"].ToString();
                // int ReconId = Convert.ToInt16(Request.Form["ReconId"].ToString());
                //int SupportId = Convert.ToInt16(Request.Form["SupportId"].ToString());
                int noofcolumns = Convert.ToInt32(Request.Form["csv_columns"].ToString());
                string acflag = Request.Form["acflag"].ToString();
                string header_flag = Request.Form["header_flag"].ToString();

                string InputFileName = string.Empty;
                string line = string.Empty;
                string[] strArray = { };
                DataTable result_csv_field = new DataTable();
                //string response_api = string .Empty ;

                HttpFileCollectionBase File = Request.Files;
                //string FileSeprator = "/";
                string ActionBy = user_codes;
                DataTable dtexceldata = new DataTable();


                HttpPostedFileBase excelfile = File[0];
                DataSet ds = new DataSet();
                string fileExtension = System.IO.Path.GetExtension(excelfile.FileName);//get file selected file extension.

                if (fileExtension == ".xls" || fileExtension == ".xlsx" || fileExtension == ".csv")
                {
                    if (excelfile != null && excelfile.ContentLength > 0)
                    {
                        string[] sptVar = excelfile.FileName.Split(new[] { fileExtension }, StringSplitOptions.None);
                        string filePath = ConfigurationManager.AppSettings["FileImporte"].ToString();
                        InputFileName = Path.GetFileName(excelfile.FileName);
                        ServerSavePath = Path.Combine(Path.Combine(filePath, Path.GetFileName(excelfile.FileName)));

                        string fileLocation = ServerSavePath;
                        //Delete the file if already exist in same name.
                        if (System.IO.File.Exists(fileLocation))
                        {
                            System.IO.File.Delete(fileLocation);
                        }

                        excelfile.SaveAs(ServerSavePath);
                        string excelConnectionString = string.Empty;

                        excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=PathToExcelFileWithExtension;Extended Properties='Excel 8.0;HDR=YES'";
                        if (fileExtension == ".xls")
                        {
                            excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1;\"";
                        }
                        else if (fileExtension == ".xlsx")
                        {
                            excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0 Xml;HDR=Yes;IMEX=1\"";
                            //"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";" + "Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1;MAXSCANROWS=0'";
                        }
                        if (fileExtension == ".xls" || fileExtension == ".xlsx")
                        {
                            using (OleDbConnection excelConnection = new OleDbConnection(excelConnectionString))//connection for excel.
                            {
                            try
                                {
                                    //excelConnection.Close();//excel connection close.
                                    excelConnection.Open();//excel connection open.
                                    DataTable dtexcel = new DataTable();//datatable object.
                                    dtexcel = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                                    if (dtexcel == null)
                                    {
                                        return null;
                                    }
                                    String[] excelSheets = new String[dtexcel.Rows.Count];
                                    int t = 0;
                                    //excel data saves in temp file here.
                                    foreach (DataRow row in dtexcel.Rows)
                                    {
                                        if (row["TABLE_NAME"].ToString().Contains('$'))
                                        {
                                            excelSheets[t] = row["TABLE_NAME"].ToString();
                                            t++;
                                        }
                                    }

                                    int excel = excelSheets.Length;
                                    if (excel == 0)
                                    {
                                        result = "invalid file";
                                        excelConnection.Close();
                                    }
                                    else
                                    {
                                        string query = string.Format("Select * from [{0}]", excelSheets[0]);
                                        try
                                        {
                                            OleDbCommand theCmd = new OleDbCommand(query, excelConnection);
                                            ds = new DataSet();
                                            using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(theCmd))
                                            {
                                                dataAdapter.Fill(ds);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            string control = this.ControllerContext.RouteData.Values["controller"].ToString();
                                            LogHelper.WriteLog("Excel Sheet Select"+ex.ToString(), control);
                                        }
                                        

                                        if (ds.Tables.Count > 0)
                                        {
                                            dtexceldata = ds.Tables[0]; //Excel Data
                                            ServPath = Path.Combine(Path.Combine(filePath, sptVar[0] + ".csv"));
                                            if (fileExtension == ".xls" || fileExtension == ".xlsx")
                                            {

                                                //   SaveToCSVNew(dtexceldata, ",", 0);
                                                ToCSV_(dtexceldata, "\t", 0, ServPath);
                                            }
                                            else
                                            {
                                                //  SaveToCSVNew(dtexceldata, FileSeprator, noofcolumns);
                                            }
                                        }
                                        excelConnection.Close();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    string control = this.ControllerContext.RouteData.Values["controller"].ToString();
                                    LogHelper.WriteLog(ex.ToString(), control);
                                    if (excelConnection.State == ConnectionState.Open) excelConnection.Close();
                                    LogHelper.WriteLog("Afterclose", control);

                                }
                            }
                        }
                        else
                        {

                        }
                    }

                    //Implemented By Mohan on 23-08-2022
                    // File table save part Started
                    string[] response_file = { };
                    int fileresult = 0;
                    string file_gid = "";

                    Fileobjmodel.FileName = InputFileName;
                    Fileobjmodel.FileType = AccType;
                    Fileobjmodel.Actionby = ActionBy;
                    Fileobjmodel.user_code = user_codes;
                    Fileobjmodel.acc_no = AccNo;
                    Fileobjmodel.recon_gid = ReconId;
                    Fileobjmodel.tranbrkptype_gid = SupportId;
                    Fileobjmodel.filetemplate_gid = TemplateId;
                    if (fileExtension == ".csv")
                    {
                        Fileobjmodel.Csv_Columns = noofcolumns;
                    }
                    else
                    {
                        Fileobjmodel.Csv_Columns = dtexceldata.Columns.Count;
                    }


                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(url);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        HttpContent content = new StringContent(JsonConvert.SerializeObject(Fileobjmodel), UTF8Encoding.UTF8, "application/json");
                        var response = client.PostAsync("InsertFileDetails", content).Result;
                        Stream data = response.Content.ReadAsStreamAsync().Result;
                        StreamReader reader = new StreamReader(data);
                        string post_data = reader.ReadToEnd();
                        response_file = (string[])JsonConvert.DeserializeObject(post_data, response_file.GetType());

                        Fileobjmodel.file_gid = response_file[0];
                        //FileInsMsg = response_file[1];
                        file_gid = response_file[0];
                        result = response_file[1];
                        fileresult = Convert.ToInt16(response_file[2]);
                        if (fileresult == 0)
                        {
                            return Json(result, JsonRequestBehavior.AllowGet);
                        }
                    }



                    // File table save part end
                    if (file_gid != "0")
                    {

                        string[] response_file1 = { };
                        Job_model Jobmodel = new Job_model();
                        Jobmodel.Job_ref_gid = Convert.ToInt64(file_gid);
                        Jobmodel.Job_type = "F";
                        Jobmodel.Job_name = InputFileName + " Import";
                        Jobmodel.Job_initiated_by = user_codes;
                        Jobmodel.Job_status = "I";
                        Jobmodel.Job_remark = InputFileName + " File Importing...";
                        Jobmodel.Job_ip_addr = ipAddress;
                        string post_data1 = objcommon.getApiResult(JsonConvert.SerializeObject(Jobmodel), "InsertJob");
                        response_file1 = (string[])JsonConvert.DeserializeObject(post_data1, response_file1.GetType());
                        string job_gid = response_file1[0];
                        string result1 = response_file1[1];
                        int fileresult1 = Convert.ToInt16(response_file1[2]);


                        int headerskip = 0;
                        string _delimitor = "\t";
                        if (header_flag == "Y" || Templatedesc=="Csv")
                        {
                            headerskip = 1;
                        }
                        if (fileExtension == ".csv")
                        {
                            _delimitor = FileSeprator;
                        }
                        if (_delimitor == "" || _delimitor == null)
                        {
                            _delimitor = ",";
                        }
                        string[] sptVar = excelfile.FileName.Split(new[] { fileExtension }, StringSplitOptions.None);
                        string filePath = ConfigurationManager.AppSettings["FileImporte"].ToString();
                        ServPath = Path.Combine(Path.Combine(filePath, sptVar[0] + ".csv"));
                        //MySqlConnection con = new MySqlConnection("Server=146.56.55.230;database=fivestar_recon;User Id=root;password=Flexi@123;Allow User Variables=true");
                        MySqlConnection con = new MySqlConnection(ConfigurationManager.AppSettings["ConnectionString"].ToString());
                        con.Open();

                        MySqlBulkLoader bbl = new MySqlBulkLoader(con)
                        {
                            Columns = { "col1","col2","col3","col4","col5","col6","col7","col8","col9","col10",
                                "col11","col12","col13","col14","col15","col16","col17","col18","col19","col20",
                                "col21","col22","col23","col24","col25","col26","col27","col28","col29","col30",
                                "col31","col32","col33","col34","col35","col36","col37","col38","col39","col40",
                                "col41","col42","col43","col44","col45","col46","col47","col48","col49","col50",
                                "col51","col52","col53","col54","col55","col56","col57","col58","col59","col60",
                                "col61","col62","col63","col64","col65","col66","col67","col68","col69","col70",
                                "col71","col72","col73","col74","col75","col76","col77","col78","col79","col80",
                                "col81","col82","col83","col84","col85","col86","col87","col88","col89","col90",
                                "col91","col92","col93","col94","col95","col96","col97","col98","col99","col100",
                                "col101","col102","col103","col104","col105","col106","col107","col108","col109","col110",
                                "col111","col112","col113","col114","col115","col116","col117","col118","col119","col120",
                                "col121","col122","col123","col124","col125","col126","col127","col128" },
                            Expressions =  {
                                    "file_gid =" + file_gid,
                               }
                        };
                        bbl.Local = true;
                        bbl.ConflictOption = MySqlBulkLoaderConflictOption.Ignore;
                        bbl.TableName = "recon_trn_tbcp";
                        //ServerSavePath = ServerSavePath.Replace("\\", "/");
                        ServPath = ServPath.Replace("\\", "/");
                        bbl.FieldTerminator = _delimitor;
                        bbl.LineTerminator = "\r\n";
                        bbl.FileName = ServPath;// "d:/temp/recon/fileupload/Fivestar.csv";
                        bbl.NumberOfLinesToSkip = headerskip;
                        // bbl.Columns.
                        int j = bbl.Load();

                        //Move Temp table to Tran table
                        Fileobjmodel.file_gid = file_gid;

                        if (AccType=="U" || AccType =="Q")
                        {
                            using (var client = new HttpClient())
                            {
                                client.BaseAddress = new Uri(url);
                                client.DefaultRequestHeaders.Accept.Clear();
                                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                                HttpContent content1 = new StringContent(JsonConvert.SerializeObject(Fileobjmodel), UTF8Encoding.UTF8, "application/json");
                                var response1 = client.PostAsync("InsertFileTransfer_update", content1).Result;
                                Stream data1 = response1.Content.ReadAsStreamAsync().Result;
                                StreamReader reader1 = new StreamReader(data1);
                                string post_data5 = reader1.ReadToEnd();
                                response_file = (string[])JsonConvert.DeserializeObject(post_data5, response_file.GetType());

                                result = response_file[1];
                                fileresult = Convert.ToInt16(response_file[2]);
                                if (fileresult == 1)
                                {
                                    return Json(result, JsonRequestBehavior.AllowGet);
                                }


                            }
                        }

                        else
                        {
                            using (var client = new HttpClient())
                            {
                                client.BaseAddress = new Uri(url);
                                client.DefaultRequestHeaders.Accept.Clear();
                                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                                //HttpContent content = new StringContent(JsonConvert.SerializeObject(Fileobjmodel), UTF8Encoding.UTF8, "application/json");
                                //var response = client.PostAsync("Movetemptotran", content).Result;
                                //Stream data = response.Content.ReadAsStreamAsync().Result;
                                //StreamReader reader = new StreamReader(data);
                                //string post_data = reader.ReadToEnd();
                                //res = post_data;
                                //response_file = (string[])JsonConvert.DeserializeObject(post_data, response_file.GetType());

                                //result = response_file[0];
                                //result = "File Imported Successfully";
                                //fileresult = Convert.ToInt16(response_file[1]);


                                HttpContent content1 = new StringContent(JsonConvert.SerializeObject(Fileobjmodel), UTF8Encoding.UTF8, "application/json");
                                var response1 = client.PostAsync("InsertFileTransfer", content1).Result;
                                Stream data1 = response1.Content.ReadAsStreamAsync().Result;
                                StreamReader reader1 = new StreamReader(data1);
                                string post_data5 = reader1.ReadToEnd();
                                response_file = (string[])JsonConvert.DeserializeObject(post_data5, response_file.GetType());

                                result = response_file[1];
                                fileresult = Convert.ToInt16(response_file[2]);
                                if (fileresult == 1)
                                {
                                    return Json(result, JsonRequestBehavior.AllowGet);
                                }


                            }

                        }

                

                        con.Close();

                    }

                   

                }
            }
            catch(Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString();
                LogHelper.WriteLog(ex.ToString(), control);
            }


                    return View();


                
            
        }

        public void SaveToCSVNew(DataTable DT, string csvDelimiter, int noofcolumns)
        {
            // code block for writing headers of data table
            // int columnCount = DT.Columns.Count;
            int columnCount = noofcolumns;
            if (noofcolumns <= 0) {
                columnCount = DT.Columns.Count;
            }
            string columnNames = "";
            string[] output = new string[DT.Rows.Count + 1];
            for (int i = 0; i < columnCount; i++)
            {
                columnNames += DT.Columns[i].ToString() + csvDelimiter;
            }
            output[0] += columnNames;

            // code block for writing rows of data table
            for (int i = 1; (i - 1) < DT.Rows.Count; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    output[i] += DT.Rows[i - 1][j].ToString() + csvDelimiter;
                }
            }

            System.IO.File.WriteAllLines(ServPath, output, System.Text.Encoding.UTF8);
        }

        public void ToCSV_( DataTable dtDataTable, string csvDelimiter, int noofcolumns, string ServPath)
        {
            try
            {
                int columnCount = noofcolumns;
                if (noofcolumns <= 0)
                {
                    columnCount = dtDataTable.Columns.Count;
                }

                using (StreamWriter sw = new StreamWriter(ServPath, false))
                {
                    //headers    
                    for (int i = 0; i < columnCount; i++)
                    {
                        sw.Write(dtDataTable.Columns[i]);
                        if (i < columnCount - 1)
                        {
                            sw.Write(csvDelimiter);
                        }
                    }
                    sw.Write(sw.NewLine);
                    foreach (DataRow dr in dtDataTable.Rows)
                    {
                        for (int i = 0; i < columnCount; i++)
                        {
                            if (!Convert.IsDBNull(dr[i]))
                            {
                                string value = dr[i].ToString();

                                value = value.Replace("\n", "");
                                value = value.Replace("\t", "");

                                if (value.Contains(csvDelimiter))
                                {
                                    value = String.Format("\"{0}\"", value);
                                    sw.Write(value);
                                }
                                else
                                {
                                    sw.Write(value);
                                }
                            }
                            if (i < columnCount - 1)
                            {
                                sw.Write(csvDelimiter);
                            }
                        }
                        sw.Write(sw.NewLine);
                    }
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString();
                LogHelper.WriteLog("CSV Catch" + ex.ToString(), control);

            }
        }

        public ActionResult getfiletype(string Fileupdate_flag)
        {
            try
            {
                string usergroup_code= System.Web.HttpContext.Current.Session["usergroup_code"].ToString();
                List<FileImport_model> objcat_3st = new List<FileImport_model>();
                DataTable result2 = new DataTable();
                FileImport_model filemodel = new FileImport_model();
                filemodel.user_code = user_codes;
                filemodel.active_status = "Y";
                filemodel.recontype = null;
                filemodel.update_flag = Fileupdate_flag;
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(filemodel), "getfieldtype");
                result2 = (DataTable)JsonConvert.DeserializeObject(post_data, result2.GetType());

                FileImport_model objcat3 = new FileImport_model();
                objcat3.AccType = "Select";
                objcat3.filetype_val = "-";
                objcat_3st.Add(objcat3);

                for (int i = 0; i < result2.Rows.Count; i++)
                {
                    objcat3 = new FileImport_model();
                    objcat3.AccType = result2.Rows[i]["filetype_desc"].ToString();
                    objcat3.filetype_val = result2.Rows[i]["file_type"].ToString();
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

        public ActionResult getuserrecon()
        {
            try
            {
               

                List<FileImport_model> objcat_3st = new List<FileImport_model>();
                DataTable result2 = new DataTable();
                FileImport_model filemodel = new FileImport_model();
                filemodel.user_code = user_codes;
                filemodel.active_status = "Y";
                filemodel.recontype = null;
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(filemodel), "Getuserrecon");
                result2 = (DataTable)JsonConvert.DeserializeObject(post_data, result2.GetType());

                FileImport_model objcat3 = new FileImport_model();
                objcat3.recon_gid = 0;
                objcat3.recon_name = "Select";
                objcat_3st.Add(objcat3);

                for (int i = 0; i < result2.Rows.Count; i++)
                {
                    objcat3 = new FileImport_model();
                    objcat3.recon_gid = Convert.ToInt32(result2.Rows[i]["recon_gid"].ToString());
                    objcat3.recon_name = result2.Rows[i]["recon_name"].ToString();
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


        public ActionResult gettemplate_frotype(string file_type)
        {
            List<FileImport_model> objcat_lst = new List<FileImport_model>();
            DataTable result = new DataTable();
            FileImport_model fileImport = new FileImport_model();
            fileImport.FileType = file_type;
            fileImport.active_status = "Y";
            try
            {
                using (var client = new HttpClient())
                {
                    try
                    {
                        client.BaseAddress = new Uri(url);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        HttpContent content = new StringContent(JsonConvert.SerializeObject(fileImport), UTF8Encoding.UTF8, "application/json");
                        var response = client.PostAsync("FileTemplateList_fromtype", content).Result;
                        Stream data = response.Content.ReadAsStreamAsync().Result;
                        StreamReader reader = new StreamReader(data);
                        string post_data = reader.ReadToEnd();
                        result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());

                        FileImport_model objcat = new FileImport_model();
                         objcat.Template_gid = 0;
                         objcat.Template_name = "Select";
                        objcat.TemplateType_desc = "";
                        objcat_lst.Add(objcat);
                        for (int i = 0; i < result.Rows.Count; i++)
                        {
                            objcat = new FileImport_model();
                            objcat.Template_gid = Convert.ToInt64(result.Rows[i][0]);
                            objcat.Template_name = result.Rows[i][1].ToString();
                            objcat.TemplateType_desc = result.Rows[i][3].ToString();
                            objcat.FileSeperator = result.Rows[i]["csv_seperator"].ToString();
                            objcat.FileType = result.Rows[i][4].ToString();
                            // objcat.FileType_desc = result.Rows[i][5].ToString();
                            objcat.Hflag = result.Rows[i]["header_flag"].ToString();
                            objcat.Acflag = result.Rows[i]["acc_no_flag"].ToString();
                            objcat.Blflag = result.Rows[i]["bal_amount_flag"].ToString();
                            objcat.Transaction_Amount_type = result.Rows[i]["tran_amount_type"].ToString();
                            objcat.Balance_Amount_type = result.Rows[i]["bal_amount_type"].ToString();
                            objcat.Transaction_Amount_type_desc = result.Rows[i]["tran_amount_type_desc"].ToString();
                            objcat.Balance_Amount_type_desc = result.Rows[i]["bal_amount_type_desc"].ToString();
                            objcat.Csv_Columns = Convert.ToInt32(result.Rows[i]["csv_columns"].ToString());
                            objcat_lst.Add(objcat);
                        }
                    }
                    catch (Exception ex)
                    {
                        string control = this.ControllerContext.RouteData.Values["controller"].ToString(); LogHelper.WriteLog(ex.ToString(), control);
                    }
                    return Json(objcat_lst, JsonRequestBehavior.AllowGet);
                    //return Json(result1);
                }
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString(); LogHelper.WriteLog(ex.ToString(), control);
            }
            return View();

        }
    
    }

}