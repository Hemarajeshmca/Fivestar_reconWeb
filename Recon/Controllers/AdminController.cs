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

namespace Recon.Controllers
{
    public class AdminController : Controller
    {
        DataTable dt;
        DataRow dr;
        //string url = ConfigurationManager.AppSettings["URL"].ToString();
        string user_codes = System.Web.HttpContext.Current.Session["usercode"].ToString();
      //  string token = ConfigurationManager.AppSettings["token"].ToString();
        // GET: Admin
        CommonController objcommon = new CommonController();
        public List<user_model.menu> DynamicMenu(string usergroup_gid)
        {
                
                List<user_model.menu> ls_menu = new List<user_model.menu>();
                DataTable result = new DataTable();
                string post_data = "";
               // LogHelper.WriteLog(usergroup_gid, "Admin_menu_Controller");
                
            try
            {
                user_model Deptmodel = new user_model();
                if (usergroup_gid == "")
                {
                    usergroup_gid = "0";
                }
              //  string url = ConfigurationManager.AppSettings["URL"].ToString();
                string user_codes = System.Web.HttpContext.Current.Session["usercode"].ToString();
                string token = System.Web.HttpContext.Current.Session["token_"].ToString();
                Deptmodel.usergroup_gid = Convert.ToInt32(usergroup_gid);
                Deptmodel.dbsource = "MYSQL";
                Deptmodel.user_code = user_codes;
                post_data = objcommon.getApiResult(JsonConvert.SerializeObject(Deptmodel), "GetTreeview");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
                int menucount= result.Rows.Count;
                //LogHelper.WriteLog("Menucount" + menucount, "Admin_menu_Controller");

                 if(menucount==0)
                 {
                     string user_codes1 = System.Web.HttpContext.Current.Session["usercode"].ToString();
                     Deptmodel.usergroup_gid = Convert.ToInt32(usergroup_gid);
                     Deptmodel.dbsource = "MYSQL";
                     Deptmodel.user_code = user_codes1;
                   
                     post_data = objcommon.getApiResult(JsonConvert.SerializeObject(Deptmodel), "GetTreeview");
                     result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
                     int menucount1 = result.Rows.Count;
                     //LogHelper.WriteLog("Menucount" + menucount1, "Admin_menu_Controller");

                 }
                

                 for (int i = 0; i < result.Rows.Count; i++)
                    {
                        int menu_access = 0;
                        user_model.menu menu = new user_model.menu();
                        menu.menu_gid = Convert.ToInt32(result.Rows[i]["menu_gid"].ToString());
                        menu.parent_menu_gid = Convert.ToInt32(result.Rows[i]["parent_menu_gid"].ToString());
                        menu.menu_name = result.Rows[i]["menu_name"].ToString();
                        menu.menu_order = Convert.ToInt32(result.Rows[i]["menu_order"].ToString());
                        if (!String.IsNullOrEmpty(result.Rows[i]["rights_flag"].ToString()))
                        {
                            menu_access = Convert.ToInt32(result.Rows[i]["rights_flag"].ToString());
                        }
                        else
                        {
                            menu_access = 0;
                        }
                        if (menu_access == 0)
                        {
                            menu.rights_flag = false;
                        }
                        else
                        {
                            menu.rights_flag = true;
                        }
                        menu.menu_url = result.Rows[i]["menu_url"].ToString();
                        ls_menu.Add(menu);
                    }
              
                return ls_menu;
            }catch(Exception e)
            {
                return ls_menu;
            }
        }

        public ActionResult Users()
        {
            return View();
        }
        public ActionResult UsersStatus()
        {
            return View();
        }
        public ActionResult Usersgroup()
        {
            return View();
        }
        public ActionResult UsersMenuMapping()
        {
            return RedirectToAction("UsersMenu", "Admin");
           // return View();
        }

        public ActionResult UsersMenu()
        {
            return View();
        }
        public ActionResult usergridGrid_Read([DataSourceRequest]DataSourceRequest request)
        {
            List<user_model> objcat_lst = new List<user_model>();
            DataTable result = new DataTable();
            user_model UserRead = new user_model();
            UserRead.user_code = user_codes;
            try
            {
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(UserRead), "UserRead");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());


                for (int i = 0; i < result.Rows.Count; i++)
                {
                    user_model objcat = new user_model();
                    objcat.user_gid = Convert.ToInt32(result.Rows[i]["user_gid"]);
                    objcat.user_code = result.Rows[i]["user_code"].ToString();
                    objcat.user_name = result.Rows[i]["user_name"].ToString();
                    objcat.user_address = result.Rows[i]["user_address"].ToString();
                    objcat.user_contact_no = result.Rows[i]["user_contactno"].ToString();
                    objcat.user_emailid = result.Rows[i]["user_emailid"].ToString();
                    objcat.user_type = result.Rows[i]["user_type"].ToString();
                    objcat.city_name = result.Rows[i]["city_name"].ToString();
                    objcat.pin_code = result.Rows[i]["pin_code"].ToString();
                    objcat.state_name = result.Rows[i]["state_name"].ToString();
                    objcat.active_status_desc = result.Rows[i]["active_status_desc"].ToString();
                    objcat.user_status = result.Rows[i]["user_status"].ToString();
                    objcat.sno = Convert.ToInt32(result.Rows[i]["sno"]);
                    objcat.usergroup_gid = Convert.ToInt32(result.Rows[i]["usergroup_gid"]);
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
        public ActionResult usergridGrid_Save(user_model usermodel, string Recongid)
        {
            try
            {
                usermodel.user_gid = 0;
                usermodel.recon_gid = Recongid;
                //usermodel.dbsource = "MYSQL";
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(usermodel), "UserSave");
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
        public JsonResult usertypedrop()//drop down
        {
            try
            {
                List<user_model> objcat_lst = new List<user_model>();
                DataTable result = new DataTable();
                user_model UserTypeDROP = new user_model();
                UserTypeDROP.user_code = user_codes;
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(UserTypeDROP), "UserTypeDROP");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());

                user_model objcat = new user_model();
                objcat.user_type = "";
                objcat.user_type_desc = "";
                objcat_lst.Add(objcat);
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    objcat = new user_model();
                    objcat.user_type = result.Rows[i]["user_type"].ToString();
                    objcat.user_type_desc = result.Rows[i]["usertype_desc"].ToString();
                    objcat_lst.Add(objcat);
                }
                return Json(objcat_lst, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                return Json(e.Message);
            }
        }
        public JsonResult usergroupdrop()//drop down
        {
           try
           {
               List<user_model> objcat_lst = new List<user_model>();
               DataTable result = new DataTable();
               user_model usergroupdrop = new user_model();
               usergroupdrop.user_code = user_codes;
               usergroupdrop.usergroup_gid = 0;
               usergroupdrop.user_status = "Y";
               string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(usergroupdrop), "usergroupdrop");
               result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
               user_model objcat = new user_model();
               objcat.usergroup_gid = 0;
               objcat.usergroup_code = "select";
               objcat_lst.Add(objcat);
               for (int i = 0; i < result.Rows.Count; i++)
               {
                   objcat = new user_model();
                   objcat.usergroup_gid = Convert.ToInt32(result.Rows[i]["usergroup_gid"]); 
                   
                   objcat.usergroup_code = result.Rows[i]["usergroup_name"].ToString();
                   objcat_lst.Add(objcat);
               }
               return Json(objcat_lst, JsonRequestBehavior.AllowGet);
           }
            catch(Exception e)
           {
               return Json(e.Message);
           }
        }
        public JsonResult userStatusdrop()//drop down
        {
            try
            {
                List<user_model> objcat_lst = new List<user_model>();
                DataTable result = new DataTable();
                user_model userStatusdrop = new user_model();
                userStatusdrop.user_code = user_codes;
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(userStatusdrop), "userStatusdrop");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
                user_model objcat = new user_model();
                objcat.user_status = "";
                objcat.active_status_desc = "";
                objcat_lst.Add(objcat);
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    objcat = new user_model();
                    objcat.user_status = result.Rows[i]["user_status"].ToString();
                    objcat.active_status_desc = result.Rows[i]["userstatus_desc"].ToString();
                    objcat_lst.Add(objcat);
                }
                return Json(objcat_lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }
        public ActionResult userStatus_Save(user_model usermodel)
        {
            try
            {
             //   usermodel.dbsource = "MYSQL";
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(usermodel), "UserStatussave");
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
        public ActionResult usergroupGrid_Save([DataSourceRequest] DataSourceRequest request, user_model Deptmodel)
        {
            try
            {
                Deptmodel.usergroup_gid = 0;
              //  Deptmodel.dbsource = "MYSQL";
                Deptmodel.user_code = user_codes;
                if (Deptmodel.user_status == null)
                {
                    Deptmodel.user_status = "Y";
                }
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(Deptmodel), "usergroupsave");
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
        public ActionResult usergroupGrid_Update([DataSourceRequest] DataSourceRequest request, user_model Deptmodel)
        {
            try
            {
                //Deptmodel.dbsource = "MYSQL";
                Deptmodel.user_code = user_codes;
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(Deptmodel), "usergroupUpdate");
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
        public ActionResult usergroupGrid_Delete([DataSourceRequest] DataSourceRequest request, user_model Deptmodel)
        {
            try
            {
               // Deptmodel.dbsource = "MYSQL";
                Deptmodel.user_code = user_codes;
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(Deptmodel), "usergroupDelete");
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
        public ActionResult usergroupGrid_Read([DataSourceRequest]DataSourceRequest request)
        {
            List<user_model> objcat_lst = new List<user_model>();
            DataTable result = new DataTable();
            user_model usergroupList = new user_model();
            usergroupList.usergroup_gid = 0;
            usergroupList.user_status = "Y";
            usergroupList.usergroup_code = "";
            usergroupList.user_code = user_codes;
            try
            {
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(usergroupList), "usergroupList");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());


                for (int i = 0; i < result.Rows.Count; i++)
                {
                    user_model objcat = new user_model();
                    objcat.usergroup_gid = Convert.ToInt32(result.Rows[i]["usergroup_gid"]);
                    objcat.usergroup_code = result.Rows[i]["usergroup_name"].ToString();
                    objcat.active_status_desc = result.Rows[i]["active_status_desc"].ToString();
                    objcat.user_status = result.Rows[i]["active_status"].ToString();
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

        #region tree view
        public ActionResult treeview(string usergroup_gid)
        {
            try
            {
                user_model Deptmodel = new user_model();
                if (usergroup_gid == "")
                {
                    usergroup_gid = "0";
                }
                Deptmodel.usergroup_gid = Convert.ToInt32(usergroup_gid);
                //  Deptmodel.dbsource = "MYSQL";
                Deptmodel.user_code = user_codes;
                List<tree_model> menu_list = new List<tree_model>();
                DataTable result = new DataTable();

                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(Deptmodel), "GetTreeview");
                result = (DataTable)JsonConvert.DeserializeObject(post_data, result.GetType());
                dt = new DataTable();
                dt.Clear();
                dt.Columns.Add("id");
                dt.Columns.Add("text");
                dt.Columns.Add("parent_id");
                dt.Columns.Add("rights_flag");
                dt.Columns.Add("menu_url");
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    add_menu(Convert.ToInt32(result.Rows[i]["menu_gid"]), result.Rows[i]["menu_name"].ToString(), Convert.ToInt32(result.Rows[i]["parent_menu_gid"]), Convert.ToInt32(result.Rows[i]["rights_flag"]), result.Rows[i]["menu_url"].ToString());
                }

                foreach (DataRow datarow in dt.Rows)
                {
                    if (datarow["parent_id"].ToString() == "0")
                    {
                        //tree_model nd = add_node(dt, datarow);
                        tree_model node = new tree_model();
                        tree_model _child_node;

                        node.id = datarow["id"].ToString();
                        node.text = datarow["text"].ToString();
                        node.parent_id = datarow["parent_id"].ToString();
                        node.rights_flag = datarow["rights_flag"].ToString();
                        node.menu_url = datarow["menu_url"].ToString();

                        var rows = dt.AsEnumerable()
                                       .Where(r => r.Field<string>("parent_id") == node.id);

                        foreach (DataRow _row in rows)
                        {
                            _child_node = new tree_model();
                            _child_node = add_node(dt, _row);
                            node.items.Add(_child_node);
                        }
                        menu_list.Add(node);
                        //menu_list.Add(nd);
                        //menu_list.Add(add_node(dt, datarow));
                    }
                }
                return Json(menu_list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }
        private class tree_model
        {
            public string id { get; set; }
            public string text { get; set; }
            public string parent_id { get; set; }
            public string rights_flag { get; set; }
            public string menu_url { get; set; }
            public bool expanded = false;
            public List<tree_model> items = new List<tree_model>();
        }
        private tree_model add_node(DataTable _dt, DataRow _dr)
        {
            tree_model node = new tree_model();
            try
            {
                tree_model _child_node;

                node.id = _dr["id"].ToString();
                node.text = _dr["text"].ToString();
                node.parent_id = _dr["parent_id"].ToString();
                node.rights_flag = _dr["rights_flag"].ToString();
                node.menu_url = _dr["menu_url"].ToString();
                var rows = _dt.AsEnumerable()
                               .Where(r => r.Field<string>("parent_id") == node.id);
                foreach (DataRow _row in rows)
                {
                    _child_node = new tree_model();
                    _child_node = add_node(_dt, _row);
                    node.items.Add(_child_node);
                }
            }
            catch(Exception e)
            {

            }
            return node;
        }
        private void add_menu(int id, string text, int parent_id, int rights_flag, string menu_url)
        {
            try
            {
                dr = dt.NewRow();
                dr["id"] = id.ToString();
                dr["text"] = text;
                dr["parent_id"] = parent_id.ToString();
                dr["rights_flag"] = rights_flag.ToString();
                dr["menu_url"] = menu_url;
                dt.Rows.Add(dr);
            }
            catch (Exception e)
            {

            }
        }
        #endregion
        public ActionResult usergrouptree_Save(string usergroup_gid, string arrayfinal)
        {
            try
            {
                user_model Deptmodel = new user_model();
                Deptmodel.usergroup_gid = Convert.ToInt32(usergroup_gid);
                Deptmodel.treearray = arrayfinal;
              //  Deptmodel.dbsource = "MYSQL";
                Deptmodel.user_code = user_codes;
                if (Deptmodel.user_status == null)
                {
                    Deptmodel.user_status = "Y";
                }
                string[] result = { };
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(Deptmodel), "usertreeviewsave");
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
        public ActionResult getuserrecon_usercode(string usercode)
        {
            try
            {

                List<TransactionRpt_model> objcat_3st = new List<TransactionRpt_model>();
                DataTable result2 = new DataTable();
                TransactionRpt_model DedupList = new TransactionRpt_model();
                DedupList.user_code =usercode;
                DedupList.active_status = "Y";
                DedupList.recontype = null;
                string post_data = objcommon.getApiResult(JsonConvert.SerializeObject(DedupList), "Getuserrecon");
                result2 = (DataTable)JsonConvert.DeserializeObject(post_data, result2.GetType());

                TransactionRpt_model objcat3 = new TransactionRpt_model();
                objcat3.ReconName_id = "0";
                objcat3.ReconName = "Select";
                objcat_3st.Add(objcat3);

                for (int i = 0; i < result2.Rows.Count; i++)
                {
                    objcat3 = new TransactionRpt_model();
                    objcat3.ReconName_id = (result2.Rows[i]["recon_gid"].ToString());
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
      
    }
}