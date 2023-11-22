using Kendo.Mvc.UI;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Recon_Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Recon.Controllers
{
    public class FileImportController : Controller
    {
        string url = ConfigurationManager.AppSettings["URL"].ToString();
        string user_codes = ConfigurationManager.AppSettings["usercode"].ToString();
        string token = ConfigurationManager.AppSettings["token_"].ToString();
        string ServPath = "";
        string ServerSavePath = "";

        public ActionResult FileImport()
        {
            return View();
        }
        public ActionResult FileImportTest()
        {
            return View();
        }
        public ActionResult FileimportRaw()
        {
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

        public ActionResult Upload_FileImport()
        {
            string result = string.Empty;
            string[] response_api = { };
            FileImport_model Fileobjmodel = new FileImport_model();
            string AccType = Request.Form["AccType"].ToString();
            int ReconId = Convert.ToInt16(Request.Form["ReconId"].ToString());
            int SupportId = Convert.ToInt16(Request.Form["SupportId"].ToString());
            string InputFileName = string.Empty;
            string line = string.Empty;
            string[] strArray = { };
            DataTable result_csv_field = new DataTable();
            //string response_api = string .Empty ;

            HttpFileCollectionBase File = Request.Files;
            //string FileSeprator = "/";
            string ActionBy = user_codes;



            HttpPostedFileBase excelfile = File[0];
            DataSet ds = new DataSet();
            string fileExtension = System.IO.Path.GetExtension(excelfile.FileName);//get file selected file extension.

            if (fileExtension == ".xls" || fileExtension == ".xlsx")
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
                        excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    }
                    else if (fileExtension == ".xlsx")
                    {
                        excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
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

                    int excel = excelSheets.Length;
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
                            ServPath = Path.Combine(Path.Combine(filePath, sptVar[0] + ".csv"));
                            SaveToCSV(dtexceldata, ',');
                        }
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
                    string[] sptVar = excelfile.FileName.Split(new[] { fileExtension }, StringSplitOptions.None);
                    string filePath = ConfigurationManager.AppSettings["FileImporte"].ToString();
                    ServPath = Path.Combine(Path.Combine(filePath, sptVar[0] + ".csv"));
                    MySqlConnection con = new MySqlConnection("Server=169.38.77.180;database=fivestar_test;User Id=root;password=Flexi@123;Allow User Variables=true");
                    con.Open();
                    if (Fileobjmodel.FileType != "S")
                    {
                        MySqlBulkLoader bl = new MySqlBulkLoader(con)
                        {
                            Columns = { "rec_slno", "acc_no", "@tran_date", "@value_date", "tran_desc", "dr_amount", "cr_amount", "bal_amount", "@chq_date", "chq_no", "loan_no", "ref_no", "tran_ref_no", "doc_ref_no", "batch_ref_no", "remark", "@file_gid", },
                            Expressions =  {
                                    "tran_date = ifnull(STR_TO_DATE(@tran_date,'%d-%m-%Y'),null)",
                                    "value_date = ifnull(STR_TO_DATE(@value_date,'%d-%m-%Y'),null)",
                                    "chq_date = ifnull(STR_TO_DATE(@chq_date,'%d-%m-%Y'),null)",
                                    "file_gid = " + file_gid
                               }
                        };
                        bl.Local = true;
                        bl.ConflictOption = MySqlBulkLoaderConflictOption.Ignore;
                        bl.TableName = "recon_tmp_ttran";
                        //ServerSavePath = ServerSavePath.Replace("\\", "/");
                        ServPath = ServPath.Replace("\\", "/");
                        bl.FieldTerminator = ",";
                        bl.LineTerminator = "\n";
                        bl.FileName = ServPath;// "d:/temp/recon/fileupload/Fivestar.csv";
                        bl.NumberOfLinesToSkip = 1;
                        int i = bl.Load();

                        //Move Temp table to Tran table
                        Fileobjmodel.file_gid = file_gid;

                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(url);
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            HttpContent content = new StringContent(JsonConvert.SerializeObject(Fileobjmodel), UTF8Encoding.UTF8, "application/json");
                            var response = client.PostAsync("Movetemptotran", content).Result;
                            Stream data = response.Content.ReadAsStreamAsync().Result;
                            StreamReader reader = new StreamReader(data);
                            string post_data = reader.ReadToEnd();
                            response_file = (string[])JsonConvert.DeserializeObject(post_data, response_file.GetType());

                            result = response_file[0];
                            fileresult = Convert.ToInt16(response_file[1]);
                            if (fileresult == 1)
                            {
                                return Json(result, JsonRequestBehavior.AllowGet);
                            }

                        }
                    }
                    else
                    {

                        Fileobjmodel.recon_gid = ReconId;
                        Fileobjmodel.tranbrkptype_gid = SupportId;
                        MySqlBulkLoader bl1 = new MySqlBulkLoader(con)
                   {
                       Columns = { "acc_no", "@tran_date", "@value_date", "tran_desc", "dr_amount", "cr_amount", "bal_amount", "@chq_date", "chq_no", "loan_no", "ref_no", "tran_ref_no", "doc_ref_no", "batch_ref_no", "remark", "@file_gid" },
                       Expressions =  {
                                    "tran_date = ifnull(STR_TO_DATE(@tran_date,'%d-%m-%Y'),null)",
                                    "value_date = ifnull(STR_TO_DATE(@value_date,'%d-%m-%Y'),null)",
                                    "chq_date = ifnull(STR_TO_DATE(@chq_date,'%d-%m-%Y'),null)",
                                    "file_gid = " + file_gid
                               }
                   };
                        bl1.Local = true;
                        bl1.ConflictOption = MySqlBulkLoaderConflictOption.Ignore;
                        bl1.TableName = "recon_tmp_ttranbrkp";
                        //ServerSavePath = ServerSavePath.Replace("\\", "/");
                        ServPath = ServPath.Replace("\\", "/");
                        bl1.FieldTerminator = ",";
                        bl1.LineTerminator = "\n";
                        bl1.FileName = ServPath;// "d:/temp/recon/fileupload/Fivestar.csv";
                        bl1.NumberOfLinesToSkip = 1;
                        int j = bl1.Load();

                        //Move Temp table to Tran table
                        Fileobjmodel.file_gid = file_gid;

                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(url);
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            HttpContent content = new StringContent(JsonConvert.SerializeObject(Fileobjmodel), UTF8Encoding.UTF8, "application/json");
                            var response = client.PostAsync("Movetemptotranbrkp", content).Result;
                            Stream data = response.Content.ReadAsStreamAsync().Result;
                            StreamReader reader = new StreamReader(data);
                            string post_data = reader.ReadToEnd();
                            response_file = (string[])JsonConvert.DeserializeObject(post_data, response_file.GetType());

                            result = response_file[0];
                            fileresult = Convert.ToInt16(response_file[1]);
                            if (fileresult == 1)
                            {
                                return Json(result, JsonRequestBehavior.AllowGet);
                            }

                        }
                    }
                }

            }
            return View();

        }

        public void SaveToCSV(DataTable DT, char csvDelimiter)
        {
            // code block for writing headers of data table
            int columnCount = DT.Columns.Count;
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

        public ActionResult Upload_FileImportTest()
        {
            string result = string.Empty;
            string[] response_api = { };
            FileImport_model Fileobjmodel = new FileImport_model();
            string AccType = Request.Form["AccType"].ToString();
            int ReconId = Convert.ToInt16(Request.Form["ReconId"].ToString());
            int SupportId = Convert.ToInt16(Request.Form["SupportId"].ToString());
            int noofcolumns = Convert.ToInt16(Request.Form["Noofcolumn"].ToString());
            string InputFileName = string.Empty;
            string line = string.Empty;
            string[] strArray = { };
            DataTable result_csv_field = new DataTable();
            //string response_api = string .Empty ;

            HttpFileCollectionBase File = Request.Files;
            //string FileSeprator = "/";
            string ActionBy = user_codes;



            HttpPostedFileBase excelfile = File[0];
            DataSet ds = new DataSet();
            string fileExtension = System.IO.Path.GetExtension(excelfile.FileName);//get file selected file extension.

            if (fileExtension == ".xls" || fileExtension == ".xlsx")
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
                        excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    }
                    else if (fileExtension == ".xlsx")
                    {
                        excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
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

                    int excel = excelSheets.Length;
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
                            ServPath = Path.Combine(Path.Combine(filePath, sptVar[0] + ".csv"));
                            SaveToCSVNew(dtexceldata, ',',noofcolumns);
                        }
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
                    string[] sptVar = excelfile.FileName.Split(new[] { fileExtension }, StringSplitOptions.None);
                    string filePath = ConfigurationManager.AppSettings["FileImporte"].ToString();
                    ServPath = Path.Combine(Path.Combine(filePath, sptVar[0] + ".csv"));
                    MySqlConnection con = new MySqlConnection("Server=169.38.77.180;database=fivestar_recon;User Id=root;password=Flexi@123;Allow User Variables=true");
                    con.Open();
                    if (Fileobjmodel.FileType != "S")
                    {
                        //MySqlBulkLoader bl = new MySqlBulkLoader(con)
                        //{
                        //    Columns = { "rec_slno", "acc_no", "@tran_date", "@value_date", "tran_desc", "dr_amount", "cr_amount", "bal_amount", "@chq_date", "chq_no", "loan_no", "ref_no", "tran_ref_no", "doc_ref_no", "batch_ref_no", "remark", "@file_gid", },
                        //    Expressions =  {
                        //            "tran_date = ifnull(STR_TO_DATE(@tran_date,'%d-%m-%Y'),null)",
                        //            "value_date = ifnull(STR_TO_DATE(@value_date,'%d-%m-%Y'),null)",
                        //            "chq_date = ifnull(STR_TO_DATE(@chq_date,'%d-%m-%Y'),null)",
                        //            "file_gid = " + file_gid
                        //       }
                        //};
                        //bl.Local = true;
                        //bl.ConflictOption = MySqlBulkLoaderConflictOption.Ignore;
                        //bl.TableName = "recon_tmp_ttran";
                        ////ServerSavePath = ServerSavePath.Replace("\\", "/");
                        //ServPath = ServPath.Replace("\\", "/");
                        //bl.FieldTerminator = ",";
                        //bl.LineTerminator = "\n";
                        //bl.FileName = ServPath;// "d:/temp/recon/fileupload/Fivestar.csv";
                        //bl.NumberOfLinesToSkip = 1;
                        //int i = bl.Load();


                        MySqlBulkLoader bbl = new MySqlBulkLoader(con)
                        {
                            Columns = {"col1","col2","col3","col4","col5"},
                            Expressions =  {
                                    //"tran_date = ifnull(STR_TO_DATE(@tran_date,'%d-%m-%Y'),null)",
                                    //"value_date = ifnull(STR_TO_DATE(@value_date,'%d-%m-%Y'),null)",
                                    //"chq_date = ifnull(STR_TO_DATE(@chq_date,'%d-%m-%Y'),null)",
                                    "file_gid = " + file_gid
                               }
                        };
                        bbl.Local = true;
                        bbl.ConflictOption = MySqlBulkLoaderConflictOption.Ignore;
                        bbl.TableName = "recon_trn_tbcp";
                        //ServerSavePath = ServerSavePath.Replace("\\", "/");
                        ServPath = ServPath.Replace("\\", "/");
                        bbl.FieldTerminator = ",";
                        bbl.LineTerminator = "\r\n";
                        bbl.FileName = ServPath;// "d:/temp/recon/fileupload/Fivestar.csv";
                        bbl.NumberOfLinesToSkip = 0;
                       // bbl.Columns.
                        int j = bbl.Load();

                        //Move Temp table to Tran table
                        Fileobjmodel.file_gid = file_gid;

                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(url);
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            HttpContent content = new StringContent(JsonConvert.SerializeObject(Fileobjmodel), UTF8Encoding.UTF8, "application/json");
                            var response = client.PostAsync("Movetemptotran", content).Result;
                            Stream data = response.Content.ReadAsStreamAsync().Result;
                            StreamReader reader = new StreamReader(data);
                            string post_data = reader.ReadToEnd();
                            response_file = (string[])JsonConvert.DeserializeObject(post_data, response_file.GetType());

                            result = response_file[0];
                            fileresult = Convert.ToInt16(response_file[1]);
                            if (fileresult == 1)
                            {
                                return Json(result, JsonRequestBehavior.AllowGet);
                            }

                        }
                    }
                    else
                    {

                        Fileobjmodel.recon_gid = ReconId;
                        Fileobjmodel.tranbrkptype_gid = SupportId;
                        MySqlBulkLoader bl1 = new MySqlBulkLoader(con)
                        {
                            Columns = { "acc_no", "@tran_date", "@value_date", "tran_desc", "dr_amount", "cr_amount", "bal_amount", "@chq_date", "chq_no", "loan_no", "ref_no", "tran_ref_no", "doc_ref_no", "batch_ref_no", "remark", "@file_gid" },
                            Expressions =  {
                                    "tran_date = ifnull(STR_TO_DATE(@tran_date,'%d-%m-%Y'),null)",
                                    "value_date = ifnull(STR_TO_DATE(@value_date,'%d-%m-%Y'),null)",
                                    "chq_date = ifnull(STR_TO_DATE(@chq_date,'%d-%m-%Y'),null)",
                                    "file_gid = " + file_gid
                               }
                        };
                        bl1.Local = true;
                        bl1.ConflictOption = MySqlBulkLoaderConflictOption.Ignore;
                        bl1.TableName = "recon_tmp_ttranbrkp";
                        //ServerSavePath = ServerSavePath.Replace("\\", "/");
                        ServPath = ServPath.Replace("\\", "/");
                        bl1.FieldTerminator = ",";
                        bl1.LineTerminator = "\n";
                        bl1.FileName = ServPath;// "d:/temp/recon/fileupload/Fivestar.csv";
                        bl1.NumberOfLinesToSkip = 1;
                        int j = bl1.Load();

                        //Move Temp table to Tran table
                        Fileobjmodel.file_gid = file_gid;

                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(url);
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            HttpContent content = new StringContent(JsonConvert.SerializeObject(Fileobjmodel), UTF8Encoding.UTF8, "application/json");
                            var response = client.PostAsync("Movetemptotranbrkp", content).Result;
                            Stream data = response.Content.ReadAsStreamAsync().Result;
                            StreamReader reader = new StreamReader(data);
                            string post_data = reader.ReadToEnd();
                            response_file = (string[])JsonConvert.DeserializeObject(post_data, response_file.GetType());

                            result = response_file[0];
                            fileresult = Convert.ToInt16(response_file[1]);
                            if (fileresult == 1)
                            {
                                return Json(result, JsonRequestBehavior.AllowGet);
                            }

                        }
                    }
                }

            }
            return View();

        }
        public void SaveToCSVNew(DataTable DT, char csvDelimiter,int noofcolumns)
        {
            // code block for writing headers of data table
           // int columnCount = DT.Columns.Count;
            int columnCount = noofcolumns;
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
    }
}
