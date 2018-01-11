using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Dal;
using Model;

namespace MvcApplication_cxf.Controllers
{

   
    /// <summary>
    /// 进入每个页面都会自动调用的方法
    /// </summary>
    public class BaseController : Controller
    {
        //
        // GET: /Base/

        //public ActionResult Index()
        //{
        //    return View();
        //}

        //
        // 摘要:
        //     在调用操作方法前调用。
        //
        // 参数:
        //   filterContext:
        //     有关当前请求和操作的信息。
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //接收登陆的用户名
            EmployeeInfo em = filterContext.HttpContext.Session["Employeer"] as EmployeeInfo;


            /*收件箱 GetEmailMessages HaverID,IsDel=0,IsRead=0*/
            EmailDal dalem = new EmailDal();
            if(em!=null)
            {
                List<GetEmailMessages> get = dalem.GetHaverEmail(em.ID, 0, 0);
                Session["list"] = get;
                Session["HaveNum"] = get.Count;
            }
            else
            {
                RedirectToAction("/login/login");
            }

            /*地址栏可输入跳转！！！*/ 
            /*-----------------------------------锁屏开始------------------------------------------*/
            //拿到控制器名称
            string controllerName = filterContext.RouteData.Values["controller"].ToString();
            //拿到视图名称
            string actionName = filterContext.RouteData.Values["action"].ToString();

            //全路径 string url = Request.Url.ToString();    http://localhost:1224/home/main/1
            //虚拟路径 string url = Request.Path;            /home/main/1

            string url = Request.Path;

            //判断是否锁屏
            if (controllerName == "home" && actionName=="lock")
            {
                Session["isLock"] = "true";
            }
            

            if (Session["isLock"] != null && Session["isLock"] == "true") 
            {
                if (url == "/home/main/" + em.LoginPassword)
                {
                    filterContext.RouteData.Values["controller"] = "home";
                    filterContext.RouteData.Values["action"] = "main";
                    Session["isLock"] = "false";
                }
                else
                {
                    filterContext.RouteData.Values["controller"] = "home";
                    filterContext.RouteData.Values["action"] = "lock";
                }
            }
            
       /*-----------------------------------锁屏结束------------------------------------------*/
           
            
            //登陆失败跳转到登陆页面
            if (em == null)
            {
                filterContext.Result = new RedirectResult("/login/login");
            }
            else
            {
                EmployeeDal dal = new EmployeeDal();

                RoleInfo r = dal.GetRoleByEmployeeID(em.ID);
                int roleID = r.ID; //获取角色ID
                List<PermissionInfo> alllist = dal.GetAllPermissions();//查询所有菜单栏
                //判断用户有无角色
                if (r != null)
                {
                    ViewBag.roleName = r.RoleName;
                    if (roleID == 1)
                    {
                        ViewBag.list = alllist;
                    }
                    else
                    {
                        /*
                         根据员工id 查询该员工的所有权限(角色权限+个人权限)
                         */
                        List<PermissionInfo> allpers = dal.GetPers(em.ID);
                        ViewBag.allpers = allpers;
                    }
                }
            }

           
           
           
           
        }

    }
}
