using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Dal;
using Model;

namespace MvcApplication_cxf.Controllers
{
    public class TimeSheetController : BaseController
    {
        //
        // GET: /TimeSheet/

        TimeSheetDal dal = new TimeSheetDal();
        public bool isOut = false;
        public bool isIn = false;
        /// <summary>
        /// 考勤管理页面
        /// </summary>
        /// <returns></returns>
        public ActionResult TimeManage()
        {

            //接收所有员工信息
            EmployeeDal emdal = new EmployeeDal();
            List<EmployeeInfo> elist = emdal.GetAllEmployeer();
            ViewBag.alluser = elist;

            //查询所有考勤记录
            List<TimeSheet> rslist = dal.GetTimeSheets();
            ViewBag.allts = rslist;
            return View();
        }

        /// <summary>
        /// 签到，签退页面
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckOrSignBack()
        {
            Model.EmployeeInfo em = (Model.EmployeeInfo)Session["Employeer"];

            //根据id查询考勤表（一条）
            TimeSheet tse = dal.GetTimeSheetByUserID(em.ID);
            if (tse == null)
            {
                ViewBag.timeinfo = null;
            }
            else
            {
                ViewBag.timeinfo = tse;
            }
            return View();
        }

        /// <summary>
        /// 签到
        /// </summary>
        /// <returns></returns>
        public ActionResult AddCheckin()
        {
            Model.EmployeeInfo em = (Model.EmployeeInfo)Session["Employeer"];

            //根据id查询考勤表（一条）
            TimeSheet tse = dal.GetTimeSheetByUserID(em.ID);
            if (tse != null)//已经签到
            {
                return Content("1");
            }
            else
            {
                DateTime dt = DateTime.Now;
                double totaltime = dt.TimeOfDay.TotalHours;//将时间转换成小时
                Session["detailcheckin"] = totaltime;
                Session["checkin"] = dt.TimeOfDay.ToString().Substring(0, 5);

                TimeSheet ts = new TimeSheet();
                ts.EmployeeID = em.ID;
                ts.OfficeHour = dt.ToShortDateString() + " " + " 09:30";
                ts.QuittingTime = "17:30";
                ts.Check_inTime = Session["checkin"].ToString();
                ts.SignBackTime = null;
                if (Convert.ToDouble(Session["detailcheckin"]) < 9.5)
                {
                    ts.Check_inType = 1;
                    ts.Remark = "早到";
                }
                else if (Convert.ToDouble(Session["detailcheckin"]) > 9.5)
                {
                    ts.Check_inType = 0;
                    ts.Remark = "迟到";
                }
                isOut = dal.AddCheckin(ts);

                return RedirectToAction("CheckOrSignBack");
            }
        }

        /// <summary>
        /// 签退
        /// </summary>
        /// <returns></returns>
        public ActionResult AddCheckout()
        {

            Model.EmployeeInfo em = (Model.EmployeeInfo)Session["Employeer"];

            //根据id查询考勤表
            TimeSheet tse = dal.GetTimeSheetByUserID(em.ID);
            if (tse != null && tse.SignBackTime != null)//已经签退
            {
                return Content("1");
            }
            else
            {
                //根据id查询考勤表
                TimeSheet checkedin = dal.GetTimeSheetByUserID(em.ID);
                //是否已经签到
                if (checkedin == null)
                {
                    return Content("0");
                }
                else
                {
                    DateTime dt = DateTime.Now;
                    double totaltime = dt.TimeOfDay.TotalHours;//将时间转换成小时
                    Session["detailcheckout"] = totaltime;
                    Session["checkout"] = dt.TimeOfDay.ToString().Substring(0, 5);

                    //根据id查询考勤表
                    TimeSheet ts = dal.GetTimeSheetByUserID(em.ID);
                    ts.SignBackTime = Session["checkout"].ToString();

                    if (Convert.ToDouble(Session["detailcheckout"]) < 17.5)
                    {
                        ts.SignBackType = 1;
                        ts.Remark += "早退";
                    }
                    else if (Convert.ToDouble(Session["detailcheckout"]) > 17.5)
                    {
                        ts.SignBackType = 0;
                        ts.Remark += "迟退";
                    }
                    isIn = dal.UpdCheck(ts);
                    return RedirectToAction("CheckOrSignBack");
                }
            }
        }


        //<summary>
        //我的签到记录页面
        //</summary>
        //<returns></returns>
        public ActionResult CheckRecord()
        {
            Model.EmployeeInfo em = (Model.EmployeeInfo)Session["Employeer"];

            //根据id查询考勤表(多条)
            List<TimeSheet> tses = dal.GetTimeSheetsByUserID(em.ID);
            ViewBag.manyts = tses;


            return View();
        }

        /// <summary>
        /// 删除考勤记录
        /// </summary>
        /// <returns></returns>
        public ActionResult DelTimeSheetByID()
        {
            //获取id
            int id = Convert.ToInt32(Request["id"]);

            bool isDel = dal.DelTimeSheetByID(id);
            if (isDel)
            {
                RedirectToAction("TimeManage");
            }
            return RedirectToAction("TimeManage");
        }
    }
}
