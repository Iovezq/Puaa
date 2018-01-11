using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Dal;
using Model;

namespace MvcApplication_cxf.Controllers
{
    public class PayrollController : BaseController
    {
        //
        // GET: /Payroll/

        PayrollDal dal = new PayrollDal();

        /// <summary>
        /// 工资页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Paylist()
        {
            //查询所有员工
            EmployeeDal edal = new EmployeeDal();
            List<EmployeeInfo> emlist = edal.GetAllEmployeer();
            ViewBag.allem = emlist;

            //查询所有员工工资
            List<Payroll> allPays = edal.GetPayrolls();
            ViewBag.allPays = allPays;

            //接收员工id
            int id = Convert.ToInt32(Request["id"]);

            if (id != 0)
            {
                List<SalaryRecord> srlist = dal.GetByID(id);
                //循环遍历查询‘发放时间’有无与正在发送的时间相同
                string ispay = "";
                //查询该员工在这月有无发送工资
                foreach (var item in srlist)
                {
                    DateTime paytime = (DateTime)item.PayTime;

                    if (paytime.Year == DateTime.Now.Year && paytime.Month == DateTime.Now.Month)
                    {
                        ispay = "已支付";
                        return Content("1");
                    }
                }
                //添加工资发送记录
                if (ispay == "")
                {
                    //获取该员工的总工资
                    int salary = Convert.ToInt32(Request["salary"]);
                    bool isAdd = false;
                    //增加工资记录
                    if (id != 0)
                    {
                        SalaryRecord sr = new SalaryRecord();
                        sr.EmployeeId = id;
                        sr.TotalSalary = salary;
                        sr.PayTime = DateTime.Now;
                        //增加发放工资记录
                        isAdd = dal.AddPayRecord(sr);
                        if (isAdd)
                        {
                            return Content("0");
                        }
                    }

                }
            }


            return View();
        }

        /// <summary>
        /// 工资发放记录页面
        /// </summary>
        /// <returns></returns>
        public ActionResult PayRecordlist()
        {
            //模糊查询
            List<SalaryRecord> srlistby = null;
            if ((Request["year"] == "" && Request["month"] == "" && Request["day"] == "") || (Request["year"] == null && Request["month"] == null && Request["day"] == null))
            {
                srlistby = dal.GetSalaryRecord();
            }
            else
            {
                //根据年
                if (Request["year"] != "" && Request["month"] == "" && Request["day"] == "")
                {
                    srlistby = dal.GetSalaryRecordByYear(Request["year"]);
                }
                //根据月
                if (Request["year"] == "" && Request["month"] != "" && Request["day"] == "")
                {
                    srlistby = dal.GetSalaryRecordByMonth(Request["month"]);
                }
                //根据日
                if (Request["year"] == "" && Request["month"] == "" && Request["day"] != "")
                {
                    srlistby = dal.GetSalaryRecordByDay(Request["day"]);
                }
                //根据年月
                if (Request["year"] != "" && Request["month"] != "" && Request["day"] == "")
                {
                    srlistby = dal.GetSalaryRecordByYearMonth(Request["year"], Request["month"]);
                }
                //根据年日
                if (Request["year"] != "" && Request["month"] == "" && Request["day"] != "")
                {
                    srlistby = dal.GetSalaryRecordByYearDay(Request["year"], Request["day"]);
                }
                //根据月日
                if (Request["year"] == "" && Request["month"] != "" && Request["day"]!= "")
                {
                    srlistby = dal.GetSalaryRecordByMonthDay(Request["day"], Request["month"]);
                }
                //根据年月日
                if (Request["year"] != "" && Request["month"] != "" && Request["day"] != "")
                {
                    srlistby = dal.GetSalaryRecordByYearMonthDay(Request["year"], Request["month"], Request["day"]);
                }
                ViewBag.year = Request["year"];
                ViewBag.month = Request["month"];
                ViewBag.day = Request["day"];
            }
            ViewBag.srlist = srlistby;


            //查询所有员工
            EmployeeDal edal = new EmployeeDal();
            List<EmployeeInfo> emlist = edal.GetAllEmployeer() as List<EmployeeInfo>;
            ViewBag.allem = emlist;

            //删除工资发放记录
            if (Request["id"] != null)
            {
                bool isDel = dal.DelByID(Convert.ToInt32(Request["id"]));
            }

            return View();
        }

        /// <summary>
        /// 新增工资页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AddPay()
        {
            Model.EmployeeInfo em = (Model.EmployeeInfo)Session["Employeer"];

            //查询所有员工
            EmployeeDal edal = new EmployeeDal();
            List<EmployeeInfo> emlist = edal.GetAllEmployeerElse(em.ID);
            ViewBag.allem = emlist;

            EmployeeDal dal = new EmployeeDal();
            if (Request["BasicWage"] != null)
            {
                int? id = Convert.ToInt32(Request["EmployeeId"]);
                //查询工资是否已经添加
                Payroll isexists = dal.GetPayByid(id);
                if (isexists != null)
                {
                    return Content("1");
                }
                else
                {
                    Payroll pay = new Payroll();
                    pay.BasicWage = Convert.ToInt32(Request["BasicWage"]);
                    pay.CommunicationAllowance = Convert.ToInt32(Request["CommunicationAllowance"]);
                    pay.EmployeeId = id;
                    pay.LunchAllowance = Convert.ToInt32(Request["LunchAllowance"]);
                    pay.MeritPay = Convert.ToInt32(Request["MeritPay"]);
                    pay.TrafficAllowance = Convert.ToInt32(Request["TrafficAllowance"]);
                    bool isadd = dal.AddPay(pay);
                    if (isadd)
                    {
                        return Content("0");
                    }
                }


            }

            return View();
        }

        /// <summary>
        /// 修改工资
        /// </summary>
        /// <returns></returns>
        public ActionResult ExeUpdPay()
        {
            int emid = Convert.ToInt32(Request["id"]);
            int BasicWage = Convert.ToInt32(Request["BasicWage"]);
            int LunchAllowance = Convert.ToInt32(Request["LunchAllowance"]);
            int MeritPay = Convert.ToInt32(Request["MeritPay"]);
            int TrafficAllowance = Convert.ToInt32(Request["TrafficAllowance"]);
            int CommunicationAllowance = Convert.ToInt32(Request["CommunicationAllowance"]);
            bool isUpd = false;
            if (emid != 0)
            {
                isUpd = dal.Upd(emid, BasicWage, LunchAllowance, MeritPay, TrafficAllowance, CommunicationAllowance);
            }

            if (isUpd)
            {
                return RedirectToAction("Paylist");
            }

            return RedirectToAction("Paylist");
        }



    }
}
