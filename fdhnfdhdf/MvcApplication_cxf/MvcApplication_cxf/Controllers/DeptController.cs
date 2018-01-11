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
    /// 部门
    /// </summary>
    public class DeptController : BaseController
    {
        //
        // GET: /Dept/

        DeptDal dal = new DeptDal();


        public List<Dept> list;//接收分页查询
        public int totalPage;//总页数
        public int currentPage;//当前页
        /// <summary>
        /// 部门列表的页面
        /// </summary>
        /// <returns></returns>
        public ActionResult DeptList()
        {
            //ViewBag.DeptInfo = dal.GetDeptInfo();           

            int page = 1;
            currentPage = page;
            if (Request["page"] != null)
            {
                page = Convert.ToInt32(Request["page"]);
                currentPage = page;
            }
            //获取总条数
            int totalCount = 0;
            if (Request["name"] != null && Request["name"] != "")
            {
                list = dal.GetPageDeptInfo(page, 5, Request["name"]);
                totalCount = dal.GetCountBy(Request["name"]);
                ViewBag.dname = Request["name"];
            }
            else
            {
                list = dal.GetPageDeptInfo(page, 5, "");
                totalCount = dal.GetCount();
            }


            //获取页数
            if (totalCount % 5 != 0)
            {
                totalPage = totalCount / 5 + 1;
            }
            else
            {
                totalPage = totalCount / 5;
            }
            //赋值传递
            ViewBag.totalPage = totalPage;
            ViewBag.currentPage = currentPage;
            ViewBag.DeptInfo = list;
            return View();
        }
        /// <summary>
        /// 添加部门的页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AddDept()
        {
            return View();
        }


        /// <summary>
        /// 部门下的岗位的页面
        /// </summary>
        /// <returns></returns>
        public ActionResult DeptPost()
        {
            //获取查询的条件部门ID
            int? DeptID = Convert.ToInt32(Request["DeptID"]);
            //获得部门下的岗位
            List<PostInfo> PostInfoList = dal.GetPostByDeptID(DeptID);
            //传递
            ViewBag.PostInfoList = PostInfoList;

            return View();
        }


        /// <summary>
        /// 部门下的员工的页面
        /// </summary>
        /// <returns></returns>
        public ActionResult DeptEmployeer()
        {
            //获取查询的条件岗位ID
            int? PostID = Convert.ToInt32(Request["PostID"]);
            //获取查询的条件部门ID
            int? DeptID = Convert.ToInt32(Request["DeptID"]);

            //获得岗位下的员工
            List<EmployeeInfo> DeptEmployeerList = dal.GetEmployeerByPostID(PostID);
            //传递
            ViewBag.DeptEmployeer = DeptEmployeerList;
            ViewBag.deptid = DeptID;

            return View();
        }

        /// <summary>
        /// 根据部门id删除部门的方法
        /// </summary>
        /// <returns></returns>
        public ActionResult DelDept()
        {
            /*如果该部门下有岗位就不能解散该部门*/

            //接收部门id
            int id = Convert.ToInt32(Request["id"]);

            if (id != 0)
            {

                //获得部门下的岗位
                List<PostInfo> PostInfoList = dal.GetPostByDeptID(id);

                if (PostInfoList.Count == 0)
                {
                    //解散该部门
                    bool isDel = dal.DelDept(id);
                    if (isDel)
                    {
                        Response.Write("0");
                        Response.End();
                    }
                }
            }
            return RedirectToAction("DeptList");
        }



        /// <summary>
        /// 添加部门的方法
        /// </summary>
        /// <param name="_c"></param>
        /// <returns></returns>
        public ActionResult ExeAddDepts()
        {
            Dept d = new Dept();
            string DeptName = Request["DeptName"];
            string DeptDuties = Request["DeptDuties"];

            d.DeptCode = new Random().Next(100,999).ToString();
            d.DeptName = DeptName;
            d.DeptDuties = DeptDuties;

            bool isAdd = dal.AddDepts(d);
            ViewBag.isAdd = isAdd;
            return RedirectToAction("AddDept");
        }

        /// <summary>
        /// 修改部门的方法
        /// </summary>
        /// <param name="_c"></param>
        /// <returns></returns>
        public ActionResult ExeUpdDepts()
        {
            int? id = Convert.ToInt32(Request["id"]);

            if (id != 0)
            {
                string DeptCode = Request["deptcode"];
                string DeptName = Request["deptname"];
                string DeptDuties = Request["deptremark"];
                Dept d = dal.GetDeptOne(id);
                d.DeptCode = DeptCode;
                d.DeptName = DeptName;
                d.DeptDuties = DeptDuties;
                bool isUpd = dal.UpdDept(d);
                if (isUpd)
                {
                    return RedirectToAction("DeptList");
                }
            } return RedirectToAction("DeptList");
        }

    }
}
