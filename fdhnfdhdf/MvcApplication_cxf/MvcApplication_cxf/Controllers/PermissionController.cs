using Dal;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication_cxf.Controllers
{
    public class PermissionController : BaseController
    {
        //
        // GET: /Permission/

        PermissionDal dal = new PermissionDal();

        /// <summary>
        /// 权限管理页面
        /// </summary>
        /// <returns></returns>
        public ActionResult PermissionManage()
        {
            //查询所有员工
            EmployeeDal emdal = new EmployeeDal();
            List<EmployeeInfo> emlist = emdal.GetAllEmployeer();
            ViewBag.emlist = emlist;

            //查询所有权限（查询菜单栏）
            PermissionDal pd = new PermissionDal();
            List<PermissionInfo> all = pd.GetAll();
            ViewBag.all = all;

            return View();
        }

        /// <summary>
        /// 角色管理页面
        /// </summary>
        /// <returns></returns>
        public ActionResult RoleManage()
        {
            //删除角色

            //接收角色id 
            if (Request["num"] != null)
            {
                int id = Convert.ToInt32(Request["num"]);
                //根据角色id查询“员工角色表”此角色有无员工担当
                Employee_RoleInfo er = dal.GetEmployee_RoleInfoByRoleID(id);
                if (er != null)
                {
                    return Content("0");
                }
                else
                {
                    //根据角色id删除角色
                    bool isDel = dal.DelRoleByRoleID(id);
                    if (isDel)
                    {
                        return Content("1");
                    }
                }
            }


            //查询所有员工
            EmployeeDal emdal = new EmployeeDal();
            List<EmployeeInfo> emlist = emdal.GetAllEmployeer();
            //查询所有权限（查询菜单栏）
            PermissionDal pd = new PermissionDal();
            List<PermissionInfo> perslist = pd.GetAll();
            //查询所有角色
            List<RoleInfo> roles = dal.GetRoles();

            ViewBag.emlist = emlist;
            ViewBag.roles = roles;


            int? RoleID = Convert.ToInt32(Request["RoleID"]);
            string RoleName = Request["RoleName"];

            if (RoleName != null)
            {
                Session["RoleName"] = RoleName;
            }
            if (RoleID != 0)
            {
                Session["RoleID"] = RoleID;
                List<RoleInfo_PermissionInfo> persByRoID = dal.GetPerByRoleID(RoleID);
                List<PermissionInfo> NotPerByRoId = dal.GetNotPerByRoId(RoleID);
                ViewBag.persByRoID = persByRoID;
                ViewBag.NotPerByRoId = NotPerByRoId;
                ViewBag.perslist = perslist;
            }


            return View();
        }
        /// <summary>
        /// 根据员工id查询权限的方法
        /// </summary>
        /// <returns></returns>
        public ActionResult ExeGetPerByEmID()
        {
            //查询所有员工
            EmployeeDal emdal = new EmployeeDal();
            List<EmployeeInfo> emlist = emdal.GetAllEmployeer();
            ViewBag.emlist = emlist;

            int? emid = null;
            if (Request["id"] != null && Request["id"] != "")
            {
                //获得员工id
                emid = Convert.ToInt32(Request["id"]);
            }

            if (emid != null)
            {
                Session["id"] = emid;
                EmployeeDal newdal = new EmployeeDal();
                RoleInfo r = newdal.GetRoleByEmployeeID(emid);
                /*
                 根据员工id和员工角色id 查询该员工的所有权限(角色权限+个人权限)
                */
                List<PermissionInfo> havepers = newdal.GetPersALL(r.ID, emid);
                ViewBag.havepers = havepers;

                /*
                根据员工id和员工角色id 查询该员工没有的权限(角色权限+个人权限)
               */
                List<PermissionInfo> nothavepers = newdal.GetNotPers(r.ID, emid);
                ViewBag.nothavepers = nothavepers;

                RedirectToAction("PermissionManage");
            }
            return View("PermissionManage");
        }

        /// <summary>
        /// 处理添加权限的方法
        /// </summary>
        /// <returns></returns>
        public ActionResult ExeAddPer()
        {
            //接收权限ids
            string ids = Request["nums"];
            //接收被授权员工的id
            int? emerid = Convert.ToInt32(Session["id"]);

            //在添加员工权限时 先查询该员工权限 将该员工权限全部删除 再进行授权

            List<Employee_PermissionInfo> li = dal.GetPerByEmID(emerid);

            if (li.Count > 0)//有权限
            {
                bool isDel = dal.DelPerByEmID(emerid);//删除权限

                if (isDel)
                {
                    //添加权限
                    List<string> list = ids.Split(',').ToList();
                    bool isAdd = false;
                    for (int i = 0; i < list.Count() - 1; i++)
                    {
                        Employee_PermissionInfo ep = new Employee_PermissionInfo();
                        ep.EmployeeId = emerid;
                        ep.PermissionID = Convert.ToInt32(list[i]);

                        isAdd = dal.AddPerByEmID(ep);
                    }

                    if (isAdd)
                    {
                        return RedirectToAction("PermissionManage");
                    }
                }

            }
            else//没有权限
            {
                //添加权限
                List<string> list = ids.Split(',').ToList();
                bool isAdd = false;
                for (int i = 0; i < list.Count() - 1; i++)
                {
                    Employee_PermissionInfo ep = new Employee_PermissionInfo();
                    ep.EmployeeId = emerid;
                    ep.PermissionID = Convert.ToInt32(list[i]);
                    isAdd = dal.AddPerByEmID(ep);
                }

                if (isAdd)
                {
                    return RedirectToAction("PermissionManage");
                }
            }
            return RedirectToAction("PermissionManage");
        }


        /// <summary>
        /// 处理添加角色的方法
        /// </summary>
        /// <returns></returns>
        public ActionResult ExeAddEmployeerRole()
        {
            //获值
            //接收被授权员工的id
            int? emerid = Convert.ToInt32(Request["id"]);

            /*
             只能拥有一个角色
             */
            //接收角色id
            int? id = 0;
            if (Request["num"] != null)
            {
                id = Convert.ToInt32(Request["num"]);
            }

            //根据id查询角色
            Employee_RoleInfo haver = dal.GetRoleByID(id);
            if (haver != null)
            {
                //删除原本角色再添加新角色
                bool isDel = dal.DelEmpRole(haver);
                if (isDel)
                {
                    //添加角色
                    Employee_RoleInfo er = new Employee_RoleInfo();
                    er.EmployeeID = emerid;
                    er.RoleID = id;
                    bool isAdd = dal.AddRoleByEmID(er);
                    if (isAdd)
                    {
                        return RedirectToAction("PermissionManage");
                    }
                }
            }
            else
            {
                //添加角色
                Employee_RoleInfo er = new Employee_RoleInfo();
                er.EmployeeID = emerid;
                er.RoleID = id;
                bool isAdd = dal.AddRoleByEmID(er);
                if (isAdd)
                {
                    return RedirectToAction("PermissionManage");
                }
            }


            return RedirectToAction("PermissionManage");
        }

        /// <summary>
        /// 添加角色页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AddRole()
        {
            //获取所有菜单栏
            List<PermissionInfo> alllist = dal.GetAll();
            ViewBag.alllist = alllist;

            return View();
        }

        /// <summary>
        /// 添加角色的方法
        /// </summary>
        /// <returns></returns>
        public ActionResult ExeAddRole()
        {
            //接收角色名
            string rolename = Request["rolename"];
            //根据角色名称查询角色id
            RoleInfo ri = dal.GetRolebyRoleName(rolename);
            if (ri != null)
            {
                return Content("1");
            }
            else
            {
                bool istrue = false;
                if (rolename != null)
                {
                    RoleInfo r = new RoleInfo();
                    r.RoleName = rolename;
                    istrue = dal.AddRole(r);
                }

                if (istrue)
                {
                    //根据角色名称查询角色id
                    int? roleID = dal.GetIDbyRoleName(rolename);

                    //接收权限ids
                    string ids = Request["nums"];

                    if (roleID != null && ids != null)
                    {
                        //添加角色权限
                        List<string> list = ids.Split(',').ToList();
                        bool isAdd = false;
                        for (int i = 0; i < list.Count() - 1; i++)
                        {
                            RoleInfo_PermissionInfo rp = new RoleInfo_PermissionInfo();
                            rp.RoleId = roleID;
                            rp.PermissionID = Convert.ToInt32(list[i]);

                            isAdd = dal.AddRolePers(rp);
                        }
                        if (isAdd)
                        {
                            return Content("0");
                        }
                    }
                }
            }

            return View("addrole");
        }

        /// <summary>
        /// 添加角色权限的方法
        /// </summary>
        /// <returns></returns>
        public ActionResult ExeAddRolePers()
        {
            //接收权限ids
            string ids = Request["nums"];
            //接收被授权角色的id
            int? roleid = Convert.ToInt32(Request["id"]);

            //在添加角色权限时 先查询该角色权限 将该角色权限全部删除 再进行授权
            List<RoleInfo_PermissionInfo> li = dal.GetPerByRoleID(roleid);
            if (li.Count > 0)//有权限
            {
                bool isDel = dal.DelPerByRoleID(roleid);//删除权限
                if (isDel)
                {
                    //添加权限
                    List<string> list = ids.Split(',').ToList();
                    bool isAdd = false;
                    for (int i = 0; i < list.Count() - 1; i++)
                    {
                        RoleInfo_PermissionInfo rp = new RoleInfo_PermissionInfo();
                        rp.RoleId = roleid;
                        rp.PermissionID = Convert.ToInt32(list[i]);
                        isAdd = dal.AddPerByRoleID(rp);
                    }

                    if (isAdd)
                    {
                        return Content("0");
                    }
                }

            }
            else//没有权限
            {
                //添加权限
                List<string> list = ids.Split(',').ToList();
                bool isAdd = false;
                for (int i = 0; i < list.Count() - 1; i++)
                {
                    RoleInfo_PermissionInfo rp = new RoleInfo_PermissionInfo();
                    rp.RoleId = roleid;
                    rp.PermissionID = Convert.ToInt32(list[i]);
                    isAdd = dal.AddPerByRoleID(rp);
                }

                if (isAdd)
                {
                    return Content("0");
                }
            }
            return RedirectToAction("PermissionManage");
        }

        /// <summary>
        /// 根据员工Id查询角色Id
        /// </summary>
        /// <returns>返回查询到的角色ID</returns>
        /// 
        public void GetPerId()
        {
            //获取员工Id
            //接收被授权员工的id

            int? emerid = Convert.ToInt32(Request["Id"]);

            //实例员工角色对象

            PermissionDal dal = new PermissionDal();

            //获取对象
            Employee_RoleInfo emr = dal.GetRoleByID(emerid);

            //获取角色Id
            int? RoleID = Convert.ToInt32(emr.RoleID);


            //返回查询到的角色id
            Response.Write(RoleID);
            Response.End();
        }
    }
}
