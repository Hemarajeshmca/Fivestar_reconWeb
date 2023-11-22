using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
using Kendo.Mvc.Extensions;

using Recon_Model;
using System.Data;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using Newtonsoft.Json;
using System.Text;
using System.Globalization;
using System.Net;
using System.Configuration;
using System.Data.OleDb;
using MySql.Data.MySqlClient;
using System.Data.Odbc;
using System.Transactions;

namespace Recon.Controllers
{
    public class QcdmasterController : Controller
    {
        string ServerSavePath = "";
        string ServPath = "";
        CommonController objcommon = new CommonController();
       // string url = ConfigurationManager.AppSettings["URL"].ToString();
        public static string hostName = Dns.GetHostName();
        string ipAddress = Dns.GetHostByName(hostName).AddressList[0].ToString();
        string user_codes = System.Web.HttpContext.Current.Session["usercode"].ToString();
        //string urldownload = "http://169.38.77.180:9091";//"https://localhost:44350";
        //string token = System.Web.HttpContext.Current.Session["token_"].ToString();
        // GET: QcdMaster
        public ActionResult QcdMaster()
        {
            return View();
        }
        public ActionResult ACView()
        {
            return View();
        }
        public ActionResult QcdMasterRead([DataSourceRequest]DataSourceRequest request, QcdMaster Qcdmastergrid)   // ProgressView  Read
        {
            List<QcdMaster> objcat_lst = new List<QcdMaster>();
            DataTable result = new DataTable();
            QcdMaster QcdMasterRead = new QcdMaster();
          

            string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(QcdMasterRead), "QcdMasterRead");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());



                for (int i = 0; i < result.Rows.Count; i++)
                {
                    QcdMaster objcat = new QcdMaster();
                    objcat.masterGid = Convert.ToInt32(result.Rows[i]["master_gid"]);
                    objcat.masterSyscode = result.Rows[i]["master_syscode"].ToString();
                    objcat.masterCode = result.Rows[i]["master_code"].ToString();
                    objcat.masterName = result.Rows[i]["master_name"].ToString();
                    objcat_lst.Add(objcat);

                }
             
            //return Json(objcat_lst.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            return Json(objcat_lst, JsonRequestBehavior.AllowGet);

        }

        public ActionResult QcdMasterGridRead([DataSourceRequest]DataSourceRequest request, string mastercode)   // ProgressView  Read
        {
            List<QcdMaster> objcat_lst = new List<QcdMaster>();
            DataTable result = new DataTable();
            QcdMaster QcdMasterRead = new QcdMaster();
            QcdMasterRead.masterCode = mastercode;

            string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(QcdMasterRead), "QcdMasterGridRead");
            result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());


            
            
            for (int i = 0; i < result.Rows.Count; i++)
            {
                QcdMaster objcat = new QcdMaster();
                objcat.masterGid = Convert.ToInt32(result.Rows[i]["master_gid"]);
                objcat.ParentMasterSyscode = result.Rows[i]["parent_master_syscode"].ToString();
                objcat.masterSyscode = result.Rows[i]["master_syscode"].ToString();
                objcat.masterCode = result.Rows[i]["master_code"].ToString();
                objcat.masterShortCode = result.Rows[i]["master_short_code"].ToString();
                objcat.masterName = result.Rows[i]["master_name"].ToString();
                string status = result.Rows[i]["active_status"].ToString();
                objcat.mastermutiplename = result.Rows[i]["master_multiple_name"].ToString();
                if(status=="Y")
                {
                    objcat.active_status = "Active";
                }
                else
                {
                    objcat.active_status = "InActive";
                }
               
                objcat_lst.Add(objcat);

            }
            return Json(objcat_lst.ToDataSourceResult(request));
            //return Json(objcat_lst.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            //return Json(objcat_lst, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Qcdmaster_Save(QcdMaster Qcdmastergrid, string _action,string parent_code)//dtl create
        {

            Qcdmastergrid.user_code = user_codes;
            Qcdmastergrid.ip_address = ipAddress;
            Qcdmastergrid.action = _action;
            Qcdmastergrid.ParentMasterSyscode = parent_code;
           
            if(Qcdmastergrid.Status==null)
            {
                Qcdmastergrid.Status = "Y";
            }

            //Qcdmastergrid.masterGid = _mastergid;
            try
            {
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(Qcdmastergrid), "Qcdmaster_Create");
                result = (string[])JsonConvert.DeserializeObject(post_data, result.GetType());
                //var RES = result[0].ToString();
                //ViewBag.Message = RES;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString();
                LogHelper.WriteLog(ex.ToString(), control);
            }
            return View();
        }

        public ActionResult SetQcdmaster_delete(QcdMaster Qcdmastergrid)//dtl create
        {

            Qcdmastergrid.user_code = user_codes;
            Qcdmastergrid.ip_address = ipAddress;
            if (Qcdmastergrid.Status == null)
            {
                Qcdmastergrid.Status = "Y";
            }
            //Qcdmastergrid.masterGid = _mastergid;
            try
            {
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(Qcdmastergrid), "Qcdmaster_Delete");
                result = (string[])JsonConvert.DeserializeObject(post_data, result.GetType());
                //var RES = result[0].ToString();
                //ViewBag.Message = RES;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString();
                LogHelper.WriteLog(ex.ToString(), control);
            }
            return View();
        }

        public ActionResult Fileupload()
        {
            string[] result1 = { };
            string InputFileName = string.Empty;
            string result = string.Empty;
            string sqlinsert = "";
            HttpFileCollectionBase Files = Request.Files;
            DataTable dtexceldata = new DataTable();
            DataTable table = new DataTable();

            foreach (string requestFileName in Request.Files)
            {
                HttpPostedFileBase excelfile = Request.Files[requestFileName];
                DataSet ds = new DataSet();
                string fileExtension = System.IO.Path.GetExtension(excelfile.FileName);

                if (fileExtension == ".xls" || fileExtension == ".xlsx" || fileExtension == ".csv")
                {
                    if (excelfile != null && excelfile.ContentLength > 0)
                    {
                        string[] sptVar = excelfile.FileName.Split(new[] { fileExtension }, StringSplitOptions.None);
                        string filePath = ConfigurationManager.AppSettings["FileImporte"].ToString();//"d:/temp/recon/fileuploadQcd";
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
                            excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
                        }
                        else if (fileExtension == ".xlsx")
                        {
                            excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0 Xml;HDR=Yes;IMEX=1;\"";
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
                                            LogHelper.WriteLog("Excel Sheet Select" + ex.ToString(), control);
                                        }
                                        table = ds.Tables[0].Copy();
                                        MySqlConnection con = new MySqlConnection(ConfigurationManager.AppSettings["ConnectionString"].ToString());
                                        con.Open();
                                        for (int i = 0; i < table.Rows.Count; i++)
                                        {
                                            DataTable dt = new DataTable();
                                            //  string query1 = "select * from recon_mst_tmaster_tb where master_syscode='" + item[0] + "' and master_code='" + item[1] + "'and master_short_code='" + item[2] + "' and master_name='" + item[3] + "' and master_multiple_name='" + item[4] + "' and parent_master_syscode='" + item[5] + "' and active_status='" + item[6] + "'";
                                            using (MySqlCommand cmd = new MySqlCommand("pr_recon_mst_qcdmaster", con))
                                            {
                                                QcdMaster Qcdmastergrid = new QcdMaster();
                                                Qcdmastergrid.masterSyscode = table.Rows[i]["master_syscode"].ToString();
                                                Qcdmastergrid.masterCode = table.Rows[i]["master_code"].ToString();
                                                Qcdmastergrid.masterShortCode = table.Rows[i]["master_Short_code"].ToString();
                                                Qcdmastergrid.masterName = table.Rows[i]["master_name"].ToString();
                                                Qcdmastergrid.mastermutiplename = table.Rows[i]["master_multiple_name"].ToString();
                                                Qcdmastergrid.ParentMasterSyscode = table.Rows[i]["parent_master_syscode"].ToString();
                                                Qcdmastergrid.Status = table.Rows[i]["active_status"].ToString();
                                                Qcdmastergrid.action = "INSERT";

                                               
                                                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(Qcdmastergrid), "QcdExcelFile");
                                                result1 = (string[])JsonConvert.DeserializeObject(post_data, result1.GetType());
                                                //result = result1[0];


                                            }

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

                                    return Json(result1, JsonRequestBehavior.AllowGet);
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
                }
                
            }
            return View();
        }

      
       


      public void ToCSV_(DataTable dtDataTable, string csvDelimiter, int noofcolumns, string ServPath)
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

                                value.Replace("\n","");

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

    }
}