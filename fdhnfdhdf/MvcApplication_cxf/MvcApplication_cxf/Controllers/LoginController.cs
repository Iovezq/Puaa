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
    /// 登陆
    /// </summary>
    public class LoginController : Controller
    {
     

        EmployeeDal dal = new EmployeeDal();

        /// <summary>
        /// 登陆页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {

            return View();
        }
        //public ContentResult getImg()
        //{

        //    string headimg = "";

        //    if (Request["xuname"] != null)
        //    {
        //        //根据昵称查询员工
        //        EmployeeInfo em = dal.GetEmployeerByLoginName(Request["xuname"]);
        //        if (em != null)
        //        {
        //            headimg = em.EmployeeHead;
        //        }
        //        else
        //        {
        //            headimg = "/media/image/head.png";
        //            return Content(headimg);
        //        }
        //    }

        //    return Content(headimg);
        //}
        /// <summary>
        /// 处理登陆的跳板（传递到baseController）方法
        /// </summary>
        /// <returns></returns>
        public void  ExeLogin()
        {

            //接收前台传值
            EmployeeInfo _em = new EmployeeInfo();

            _em.LoginName =  Request["login"];
            _em.LoginPassword = Request["pwd"];

            //调用dal
            EmployeeInfo em = dal.Login(_em);
            if (em != null)
            {
                Session["Employeer"] = em;

                //若是姓名密码正确就返回
                Response.Write("1");
                Response.End();
               
            }
            else
            {

                Response.Write("0");
                Response.End();
            }

        }
    }
}
