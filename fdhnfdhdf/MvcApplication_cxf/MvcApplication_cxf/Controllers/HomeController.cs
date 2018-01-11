using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

using Dal;
using Model;

namespace MvcApplication_cxf.Controllers
{
    /// <summary>
    /// 个人中心
    /// </summary>
    public class HomeController : BaseController
    {
        //
        // GET: /Home/

        EmployeeDal dal = new EmployeeDal();

        /// <summary>
        /// 锁屏页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Lock()
        {
            return View();
        }

        /// <summary>
        /// 主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Main()
        {
            return View();
        }

        /// <summary>
        /// 我的日程页面
        /// </summary>
        /// <returns></returns>
        public ActionResult MyLists()
        {
            //取得登陆的用户名
            EmployeeInfo em = (EmployeeInfo)Session["Employeer"];

            //根据用户名查询用户日程
            List<Calendar> loginCalendars = dal.GetLoginerCalendar(em.ID);
            //封装
            ViewBag.loginCalendars = loginCalendars;
            return View();
        }

        /// <summary>
        /// 添加日程页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AddLists()
        {
            return View();
        }
        /// <summary>
        /// 显示详细日程安排页面
        /// </summary>
        /// <returns></returns>
        public ActionResult DetailCalendar()
        {
            //获取查询的日程id
            int id = Convert.ToInt32(Request["id"]);
            //根据日程id查询日程
            Calendar c = dal.GetCalendarByID(id);
            ViewBag.OneCalendar = c;

            EmployeeInfo em = dal.GetEmployeerByID(c.EmployeeID);
            ViewBag.OneEmployeer = em;

            return View();
        }
        /// <summary>
        /// 根据详细时间获取星期
        /// </summary>
        /// <param name="newdate"></param>
        public string GetDate(string newdate)
        {
            if (newdate == "Monday")
            {
                newdate = "星期一";
            }
            if (newdate == "Tuesday")
            {
                newdate = "星期二";
            }
            if (newdate == "Wednesday")
            {
                newdate = "星期三";
            }
            if (newdate == "Thursday")
            {
                newdate = "星期四";
            }
            if (newdate == "Friday")
            {
                newdate = "星期五";
            }
            if (newdate == "Saturday")
            {
                newdate = "星期六";
            }
            if (newdate == "Sunday")
            {
                newdate = "星期天";
            }
            return newdate;
        }
        /// <summary>
        /// 修改日程的方法
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdCalendar()
        {

            if (Request["method"] != null)
            {
                if (Request["method"].ToString() == "update")
                {
                    Calendar c = new Calendar();
                    c.ID = Convert.ToInt32(Request["id"]);
                    c.Schedule = Request["schedule"];
                    c.ActionTime = Request["actionTime"];

                    DateTime dt = Convert.ToDateTime(c.ActionTime);

                    //星期一：Mon.=Monday 
                    //星期二：Tues.=Tuesday 
                    //星期三：Wed.=Wednesday 
                    //星期四：Thur.=Thursday 
                    //星期五：Fri.=Friday 
                    //星期六：Sat.=Saturday 
                    //星期天：Sun.=Sunday 
                    string newdate = dt.DayOfWeek.ToString();
                    newdate = GetDate(newdate);
                    c.ScheduleDate = newdate;

                    bool isUpdate = dal.UpdCalendarByID(c);
                    Response.Write(isUpdate);
                    Response.End();
                }
            }
            return View("MyLists");
        }

        /// <summary>
        /// 删除日程的方法
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public ActionResult DelCalendar()
        {
            int delid = Convert.ToInt32(Request["delid"]);
            bool isDel = dal.DelCalendarByID(delid);

            return RedirectToAction("MyLists");
        }

        /// <summary>
        /// 登陆的用户添加自己的日程的方法
        /// </summary>
        /// <param name="_c"></param>
        /// <returns></returns>
        public ActionResult AddCalendar()
        {
            Calendar c = new Calendar();

            string ActionTime = Request["ActionTime"];
            c.ActionTime = ActionTime;

            DateTime dt = Convert.ToDateTime(ActionTime);
            string newdate = dt.DayOfWeek.ToString();
            newdate = GetDate(newdate);
            c.ScheduleDate = newdate;

            string Schedule = Request["Schedule"];
            c.Schedule = Schedule;

            EmployeeInfo em = (EmployeeInfo)Session["Employeer"];
            c.EmployeeID = em.ID;

            bool isAdd = dal.AddCalendar(c);
            //ViewBag.isAdd = isAdd;
            return RedirectToAction("AddLists");
        }

        /// <summary>
        /// 上传图片的方法
        /// </summary>
        public string GetUpdImg()
        {
            string headimgurl = "";
            if (Request.Files[0].FileName != "" && Request.Files.Count > 0)
            {
                Random rnd = new Random();
                int num = rnd.Next(5000, 10000);
                headimgurl = "/media/image/" + num + Request.Files[0].FileName;
                string url = Server.MapPath("~") + headimgurl;
                Request.Files[0].SaveAs(url);

            }
            return headimgurl;
        }


        /// <summary>
        /// /我的个人信息页面
        /// </summary>
        /// <returns></returns>
        public ActionResult MyInfo()
        {
            //获取登陆用户id
            EmployeeInfo em = (EmployeeInfo)Session["Employeer"];
            //根据id查询
            EmployeeInfo emer = dal.GetEmployeerByID(em.ID);
            //将值传到前台
            //
            ViewBag.emer = emer;
            Session["Employeer"] = emer;

            //查询所有岗位
            PostDal pd = new PostDal();
            List<PostInfo> pl = pd.GetAllPost();
            //将值传到前台
            ViewBag.post = pl;


            //查询所有部门
            DeptDal ddd = new DeptDal();
            List<Dept> delist = ddd.GetDeptInfo();
            ViewBag.dept = delist;

            //根据员工id查询员工工资
            EmployeeDal emdal = new EmployeeDal();
            Payroll p = emdal.GetPayByid(em.ID);
            ViewBag.empay = p;

            // 修改员工的方法

            if (Request["name"] != null)
            {

                EmployeeInfo my = new EmployeeInfo();
                my.ID = em.ID;
                string imgurl = GetUpdImg();
                if (imgurl != "")
                {
                    my.EmployeeHead = imgurl;
                }
                my.LoginName = Request["name"];
                my.LoginPassword = Request["pwd"];
                my.Sex = Convert.ToInt32(Request["sex"]);
                my.IDNumber = Convert.ToInt64(Request["num"]);
                my.BirthDay = Request["birth"];
                my.Phone = Convert.ToInt64(Request["phone"]);
                my.Email = Request["email"];
                my.EmployeeAddress = Request["address"];
                my.Degree = Request["degree"];
                my.EmployeeProfile = Request["profile"];

                bool isUpd = dal.UpdMy(my);
                if (isUpd)
                {
                    return Content("1");
                }
            }

            return View();
        }



    }
}
