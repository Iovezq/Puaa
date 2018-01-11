using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Dal;
using Model;

namespace MvcApplication_cxf.Controllers
{
    public class PostController : BaseController
    {
        //
        // GET: /Post/

        PostDal dal = new PostDal();
        public List<PostInfo> list;//接收分页查询
        public int totalPage;//总页数
        public int currentPage;//当前页
        /// <summary>
        /// 岗位列表页面
        /// </summary>
        /// <returns></returns>
        public ActionResult PostList()
        {
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
                list = dal.GetPagePostInfo(page, 5, Request["name"]);
                totalCount = dal.GetCountBy(Request["name"]);
                ViewBag.pname = Request["name"];
            }
            else
            {
                list = dal.GetPagePostInfo(page, 5, "");
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

            //所有部门
            List<Dept> deptlist = dal.GetAllDept();
            ViewBag.deptlist = deptlist;


            //赋值传递
            ViewBag.totalPage = totalPage;
            ViewBag.currentPage = currentPage;
            ViewBag.plist = list;
            return View();
        }
        /// <summary>
        /// 新增岗位页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AddPost()
        {
            //所有部门
            List<Dept> deptlist = dal.GetAllDept();
            ViewBag.deptlist = deptlist;
            return View();
        }
        /// <summary>
        /// 处理新增岗位的方法
        /// </summary>
        /// <returns></returns>
        public ActionResult ExeAddPost()
        {
            PostInfo p = new PostInfo();
            int DeptID = Convert.ToInt16(Request["DeptID"]);
            string PostName = Request["PostName"];
            string PostDuties = Request["PostDuties"];

            p.DeptID = DeptID;
            p.JobDescription = PostDuties;
            p.JobTitle = PostName;

            bool isAdd = dal.AddPost(p);
            ViewBag.isAdd = isAdd;
            return RedirectToAction("AddPost");
        }

        /// <summary>
        /// 处理删除岗位的方法
        /// </summary>
        /// <returns></returns>
        public ActionResult ExeDelPost()
        {
            //获取被删除的岗位id
            int delid = Convert.ToInt32(Request["delid"]);
            /*判断如果该岗位下还有员工则不能进行删除*/
            //在员工表中根据岗位id查询员工
            EmployeeInfo em = dal.GetEmployeerByPostID(delid);
            //有员工
            if (em != null)
            {
                Response.Write("0");
                Response.End();
            }
            else//无员工
            {
                //根据岗位id删除岗位
                bool isDel = dal.DelPostByID(delid);
                if (isDel)
                {
                    Response.Write("1");
                    Response.End();
                }

            }

            return RedirectToAction("PostList");
        }

        /// <summary>
        /// 将岗位调动给部门页面
        /// </summary>
        /// <returns></returns>
        public ActionResult PostByDept()
        {

            //所有部门
            List<Dept> deptlist = dal.GetAllDept();
            ViewBag.deptlist = deptlist;

            //所有岗位
            List<PostInfo> polist = dal.GetAllPost();
            ViewBag.polist = polist;

            return View();
        }

        /// <summary>
        /// 处理修改岗位的方法
        /// </summary>
        /// <returns></returns>
        public ActionResult ExeUpdPost()
        {
            //获得传值
            int id = Convert.ToInt32(Request["id"]);
            string PostName = Request["PostName"];
            int DeptID = Convert.ToInt32(Request["DeptID"]);
            string PostDuties = Request["PostDuties"];

            //修改
            PostInfo pi = new PostInfo();
            pi.ID = id;
            pi.JobTitle = PostName;
            pi.DeptID = DeptID;
            pi.JobDescription = PostDuties;
            bool isUpd = dal.UpdPostByID(pi);

            if (isUpd)
            {
                return RedirectToAction("PostList");
            }
            return RedirectToAction("PostList");
        }
    }
}
