using Dal;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication_cxf.Controllers
{
    public class CalendarController : BaseController
    {
        //
        // GET: /Calendar/

        CalendarsDal dal = new CalendarsDal();

        /// <summary>
        /// 所有员工的日程页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AllCalendars()
        {                     
            List<Calendar> CalendarsList = dal.GetCalendar();
            //封装
            ViewBag.Calendars = CalendarsList;
            List<EmployeeInfo> emlist = new List<EmployeeInfo>();
            foreach (var Cal in CalendarsList)
            {
               EmployeeInfo eminfo = dal.GetEmployeerByID(Cal);     
               emlist.Add(eminfo);
            }
            ViewBag.emlist = emlist;
           
            return View();
        }

    }
}
