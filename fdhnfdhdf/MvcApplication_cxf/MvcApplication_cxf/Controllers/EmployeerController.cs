using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Dal;
using System.Text.RegularExpressions;
using Model;

namespace MvcApplication_cxf.Controllers
{
    public class EmployeerController : BaseController
    {
        //
        // GET: /Employeer/

        EmployeeDal dal = new EmployeeDal();


        public List<EmployeeInfo> list;//接收分页查询
        public int totalPage;//总页数
        public int currentPage;//当前页
        /// <summary>
        /// 员工列表页面
        /// </summary>
        /// <returns></returns>
        public ActionResult EmployeerList()
        {
            int page = 1;
            int hang = 5;
            currentPage = page;
            if (Request["page"] != null)
            {
                page = Convert.ToInt32(Request["page"]);
                currentPage = page;
            }
            if (Request["hang"] != null)
            {
                hang = Convert.ToInt32(Request["hang"]);
            }
            int totalCount = 0;
            if (Request["searchContent"] != null)
            {
                //获取查询结果的条数
                totalCount = dal.GetCountByName(Request["searchContent"]);
                ViewBag.totalCount = totalCount;
                if (Convert.ToInt32(Request["hang"]) == -1)
                {
                    hang = totalCount;
                }
                ViewData["hang"] = hang;

                //调用分页模糊查询的方法
                list = dal.GetEmployeerInfo(page, hang, Request["searchContent"]);
                ViewBag.content = Request["searchContent"];
            }
            else
            {
                //获取所有员工条数
                totalCount = dal.GetCount();
                ViewBag.totalCount = totalCount;
                if (Convert.ToInt32(Request["hang"]) == -1)
                {
                    hang = totalCount;
                }
                ViewData["hang"] = hang;

                //调用分页模糊查询的方法
                list = dal.GetEmployeerInfo(page, hang, "");
            }


            //获取页数
            if (totalCount % hang != 0)
            {
                totalPage = totalCount / hang + 1;
            }
            else
            {
                totalPage = totalCount / hang;
            }

            //赋值传递
            ViewBag.totalPage = totalPage;
            ViewBag.currentPage = currentPage;
            ViewBag.EmployeerInfo = list;

            //所有部门
            PostDal pdal = new PostDal();
            List<Dept> deptlist = pdal.GetAllDept();
            ViewBag.deptlist = deptlist;

            //所有岗位
            List<PostInfo> postlist = pdal.GetAllPost();
            ViewBag.postlist = postlist;

            return View();
        }
        /// <summary>
        /// 新增员工页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AddEmployeer()
        {
            //验证邮箱
            if (Request["email"] != null)
            {
                string _emial = Request["email"];
                Regex RegCHZN = new Regex(@"^(\w-*\.*)+@(\w-?)+(\.\w{2,})+$");
                Match m = RegCHZN.Match(_emial);
                bool istrue = m.Success;
                if (istrue == false)
                {
                    return Content("1");
                }
            }
            //验证用户昵称(唯一)
            if (Request["xuname"] != null)
            {
                //根据昵称查询员工
                EmployeeInfo em = dal.GetEmployeerByLoginName(Request["xuname"]);
                if (em != null)
                {
                    return Content("2");
                }
            }

            //所有部门
            PostDal pdal = new PostDal();
            List<Dept> deptlist = pdal.GetAllDept();
            ViewBag.deptlist = deptlist;

            //所有岗位
            List<PostInfo> postlist = pdal.GetAllPost();
            ViewBag.postlist = postlist;

            return View();
        }

        /// <summary>
        /// 上传图片的方法
        /// </summary>
        public string ExeAddImg()
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
        /// 添加员工的方法
        /// </summary>
        /// <returns></returns>
        public ActionResult ExeAddEmployeer(EmployeeInfo _em)
        {
            string imgurl=ExeAddImg();
            if (imgurl != null)
            {
                _em.EmployeeHead = imgurl;
                _em.BirthDay = _em.IDNumber.ToString().Substring(6, 4) + "/" + _em.IDNumber.ToString().Substring(10, 2)  + "/" + _em.IDNumber.ToString().Substring(12, 2);
            }
          
            bool isAdd = dal.AddEmployeer(_em);
            if (isAdd)
            {
                RedirectToAction("EmployeerList");
            }
            return RedirectToAction("EmployeerList");
        }
        /// <summary>
        /// 分配权限页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Privileges()
        {
            //查询所有员工
            List<EmployeeInfo> emlist = dal.GetAllEmployeer();
            ViewBag.emlist = emlist;
            //查询岗位
            PostDal pdal = new PostDal();
            List<PostInfo> postlist = pdal.GetAllPost();
            ViewBag.postlist = postlist;
            //查询部门
            DeptDal ddal = new DeptDal();
            List<Dept> deptlist = ddal.GetDeptInfo();
            ViewBag.deptlist = deptlist;


            //根据员工id查询员工
            int? id = Convert.ToInt32(Request["id"]);
            if (id != 0)
            {
                EmployeeInfo em = dal.GetEmployeerByID(id);
                //记录下拉列表选择的员工
                ViewBag.selEmp = em;

            }

            return View();
        }

        /// <summary>
        /// 根据员工id删除员工的方法
        /// </summary>
        /// <returns></returns>
        public ActionResult DelEmByID()
        {
            int id = Convert.ToInt32(Request["delid"]);
            bool isDelemp = dal.DelEmployeerByID(id);//删除该员工
            bool isDelsalary = false;//删除该员工的薪资
            bool isDelsalaryRecord = false;//删除该员工的薪资记录
            bool isDelCalendar = false;//删除该员工的日程
            if (isDelemp)
            {
                //查询该员工有无薪资有则删除该员工的薪资
                Payroll emerpay = dal.SelEmployeerSalaryByID(id);
                if (emerpay != null)
                {
                    isDelsalary = dal.DelEmployeerSalaryByID(emerpay);
                    //查询该员工有无薪资记录有则删除该员工的薪资记录
                    PayrollDal pdal = new PayrollDal();
                    List<SalaryRecord> srlist = pdal.GetByID(id);
                    if (srlist.Count > 0 && isDelsalary == true)
                    {
                        isDelsalaryRecord = pdal.DelByID(srlist);
                    }
                    //查询该员工有无日程有则删除该员工的日程
                    CalendarsDal cdal = new CalendarsDal();
                    List<Calendar> cal = cdal.GetCalendarByID(id);
                    if (cal.Count > 0)
                    {
                        isDelCalendar = cdal.DelCalendarByID(cal);
                    }
                    if (isDelsalary && isDelsalaryRecord && isDelCalendar)
                    {
                        return RedirectToAction("EmployeerList");
                    }

                }
                else
                {
                    //查询该员工有无日程有则删除该员工的日程
                    CalendarsDal cdal = new CalendarsDal();
                    List<Calendar> cal = cdal.GetCalendarByID(id);
                    if (cal.Count > 0)
                    {
                        isDelCalendar = cdal.DelCalendarByID(cal);
                    }
                    if (isDelCalendar)
                    {
                        return RedirectToAction("EmployeerList");
                    }
                }
            }
            return RedirectToAction("EmployeerList");
        }


        /// <summary>
        ///个人信息页面（根据员工id查询员工）
        /// </summary>
        /// <returns></returns>
        public ActionResult SelProfile()
        {
            int id = Convert.ToInt32(Request["id"]);
            Session["emid"] = id;
            EmployeeInfo em = dal.GetEmployeerByID(id);
            ViewBag.info = em;

            //所有部门
            PostDal pdal = new PostDal();
            List<Dept> deptlist = pdal.GetAllDept();
            ViewBag.deptlist = deptlist;

            //所有岗位
            List<PostInfo> postlist = pdal.GetAllPost();
            ViewBag.postlist = postlist;

            return View();
        }
        /// <summary>
        /// 更新信息页面
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdProfile()
        {
            //获取查询员工的id
            int id = Convert.ToInt32(Session["emid"]);
            EmployeeInfo em = dal.GetEmployeerByID(id);
            ViewBag.info = em;
            
            PostDal pdal = new PostDal();
            //所有岗位
            List<PostInfo> postlist = pdal.GetAllPost();
            ViewBag.postlist = postlist;

            return View();
        }
        /// <summary>
        /// 更新信息的方法
        /// </summary>
        /// <returns></returns>
        public ActionResult ExeUpdProfile(EmployeeInfo _em)
        {
            bool isUpd = dal.UpdEmployeerInfo(_em);
            return RedirectToAction("UpdProfile/id=" + _em.ID, "Employeer");
        }
        /// <summary>
        /// 个人日程页面（根据员工id查询员工日程）
        /// </summary>
        /// <returns></returns>
        public ActionResult SelOneCalendar()
        {
            //查询员工日程
            int id = Convert.ToInt32(Session["emid"]);
            List<Calendar> clist = dal.GetCalendarByEmID(id);
            ViewBag.presonalCalendar = clist;

            //查询员工姓名
            List<EmployeeInfo> emlist = new List<EmployeeInfo>();
            foreach (var Cal in clist)
            {
                CalendarsDal cdal = new CalendarsDal();
                EmployeeInfo eminfo = cdal.GetEmployeerByID(Cal);
                emlist.Add(eminfo);
            }
            ViewBag.emlist = emlist;
            return View();
        }

        /// <summary>
        /// 删除多个员工根据员工id
        /// </summary>
        /// <returns></returns>
        public ActionResult ExeDelByIDs()
        {
            //接收员工IDS
            string ids = Request["ids"];
            if (ids != null)
            {
                List<string> list = ids.Split(',').ToList();
                bool isDelemper = false;
                bool isDelSalary = false;
                bool isDelSalaryRecord = false;
                bool isDelCalendar = false;
                for (int i = 0; i < list.Count() - 1; i++)
                {
                    isDelemper = dal.DelEmployeerByID(Convert.ToInt32(list[i]));//删除多个员工
                    //查询该员工有无薪资有则删除该员工的薪资
                    Payroll emerpay = dal.SelEmployeerSalaryByID(Convert.ToInt32(list[i]));
                    if (emerpay != null)
                    {
                        isDelSalary = dal.DelEmployeerSalaryByID(emerpay);
                        //查询该员工有无薪资记录有则删除该员工的薪资记录
                        PayrollDal pdal = new PayrollDal();
                        List<SalaryRecord> srlist = pdal.GetByID(Convert.ToInt32(list[i]));
                        if (srlist.Count > 0 && isDelSalary == true)
                        {
                            isDelSalaryRecord = pdal.DelByID(srlist);
                        }
                        //查询该员工有无日程有则删除该员工的日程
                        CalendarsDal cdal = new CalendarsDal();
                        List<Calendar> cal = cdal.GetCalendarByID(Convert.ToInt32(list[i]));
                        if (cal.Count > 0)
                        {
                            isDelCalendar = cdal.DelCalendarByID(cal);
                        }
                    }
                    else
                    {
                        //查询该员工有无日程有则删除该员工的日程
                        CalendarsDal cdal = new CalendarsDal();
                        List<Calendar> cal = cdal.GetCalendarByID(Convert.ToInt32(list[i]));
                        if (cal.Count > 0)
                        {
                            isDelCalendar = cdal.DelCalendarByID(cal);
                        }
                    }
                }
                return RedirectToAction("EmployeerList");
            }
            return RedirectToAction("EmployeerList");
        }

        /// <summary>
        /// 调动岗位的方法
        /// </summary>
        /// <returns></returns>
        public ActionResult ExeUpdEmployeerPost()
        {
            //接收员工id
            int id = Convert.ToInt32(Request["id"]);
            //接收调动岗位id
            int? num = Convert.ToInt32(Request["num"]);
            bool isUpd = dal.UpdEmp(id, num);
            if (isUpd)
            {
                return RedirectToAction("Privileges");
            }

            return View("Privileges");
        }

    }
}
