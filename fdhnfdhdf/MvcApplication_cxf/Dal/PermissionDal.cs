using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class PermissionDal
    {
        testEntities t = new testEntities();

        /// <summary>
        /// 查询所有菜单栏
        /// </summary>
        /// <returns></returns>
        public List<PermissionInfo> GetAll()
        {
            return t.PermissionInfo.ToList();
        } 
        /// <summary>
        /// 根据员工id查询他有的权限（个人权限）
        /// </summary>
        /// <returns></returns>
        public List<Employee_PermissionInfo> GetPerByEmID(int? _id)
        {
            return t.Employee_PermissionInfo.Where(a => a.EmployeeId == _id).ToList();
        }

        /// <summary>
        /// 根据员工id在权限表中查询他没有的权限
        /// </summary>
        /// <returns></returns>
        public List<PermissionInfo> GetNotPerById(int? _id)
        {
            return t.Database.SqlQuery<PermissionInfo>(@"select * from PermissionInfo a where not exists 
            (select b.PermissionID from Employee_PermissionInfo b where b.EmployeeId='" +_id+"' and b.PermissionID=a.ID)").ToList();    
        }

        /// <summary>
        /// 根据员工id删除该员工权限
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public bool DelPerByEmID(int? _id)
        {
            List<Employee_PermissionInfo> emlist = t.Employee_PermissionInfo.Where(a => a.EmployeeId == _id).ToList();
            foreach (var item in emlist)
            {
                t.Employee_PermissionInfo.Remove(item);
            }  

            return t.SaveChanges() > 0;
        }

        /// <summary>
        /// 添加员工权限
        /// </summary>
        /// <param name="_ep"></param>
        /// <returns></returns>
        public bool AddPerByEmID(Employee_PermissionInfo _ep)
        {
            t.Employee_PermissionInfo.Add(_ep);
            return t.SaveChanges() > 0;
        }
        /// <summary>
        /// 添加员工角色
        /// </summary>
        /// <param name="_er"></param>
        /// <returns></returns>
        public bool AddRoleByEmID(Employee_RoleInfo _er)
        {
            t.Employee_RoleInfo.Add(_er);
            return t.SaveChanges()>0;
        }

        /// <summary>
        /// 查询所有角色
        /// </summary>
        /// <returns></returns>
        public List<RoleInfo> GetRoles()
        {
            return t.RoleInfo.ToList();
        }

        /// <summary>
        /// 根据角色id查询角色权限
        /// </summary>
        /// <returns></returns>
        public List<RoleInfo_PermissionInfo> GetPerByRoleID(int ?_id)
        {
            return t.RoleInfo_PermissionInfo.Where(a=>a.RoleId==_id).ToList();
        }

        /// <summary>
        /// 根据角色id删除角色权限
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public bool DelPerByRoleID(int? _id)
        {
            List<RoleInfo_PermissionInfo> rolist = t.RoleInfo_PermissionInfo.Where(a => a.RoleId == _id).ToList();
            foreach (var item in rolist)
            {
                t.RoleInfo_PermissionInfo.Remove(item);
            }

            return t.SaveChanges() > 0;
        }

        /// <summary>
        /// 添加角色权限
        /// </summary>
        /// <param name="_rp"></param>
        /// <returns></returns>
        public bool AddPerByRoleID(RoleInfo_PermissionInfo _rp)
        {
            t.RoleInfo_PermissionInfo.Add(_rp);
            return t.SaveChanges() > 0;
        }

        /// <summary>
        /// 根据角色id在权限表中查询角色没有的权限
        /// </summary>
        /// <returns></returns>
        public List<PermissionInfo> GetNotPerByRoId(int? _id)
        {
            return t.Database.SqlQuery<PermissionInfo>(@"select * from PermissionInfo a where not exists 
            (select b.PermissionID from RoleInfo_PermissionInfo b where b.RoleId='" + _id + "' and b.PermissionID=a.ID)").ToList();
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="_r"></param>
        /// <returns></returns>
        public bool AddRole(RoleInfo _r)
        {
            t.RoleInfo.Add(_r);
            return t.SaveChanges() > 0;
        }

        /// <summary>
        /// 根据角色名查找角色
        /// </summary>
        /// <param name="_rolename"></param>
        /// <returns></returns>
        public RoleInfo GetRolebyRoleName(string _rolename)
        {
            RoleInfo r = t.RoleInfo.Where(a => a.RoleName == _rolename).FirstOrDefault();
            return r;
        }


        /// <summary>
        /// 根据角色名查找角色
        /// </summary>
        /// <param name="_rolename"></param>
        /// <returns></returns>
        public int GetIDbyRoleName(string _rolename)
        {
            RoleInfo r = t.RoleInfo.Where(a => a.RoleName == _rolename).FirstOrDefault();
            return r.ID;
        }

        /// <summary>
        /// 添加角色权限
        /// </summary>
        /// <param name="_rp"></param>
        /// <returns></returns>
        public bool AddRolePers(RoleInfo_PermissionInfo _rp)
        {
            t.RoleInfo_PermissionInfo.Add(_rp);
            return t.SaveChanges() > 0;
        }

        /// <summary>
        /// 根据员工id查询员工角色id
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public Employee_RoleInfo GetRoleByID(int? _id)
        {
            return t.Employee_RoleInfo.Where(a => a.EmployeeID == _id).FirstOrDefault();
        }


        /// <summary>
        /// 删除员工角色
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public bool DelEmpRole(Employee_RoleInfo _er)
        {
            t.Employee_RoleInfo.Remove(_er);
            return t.SaveChanges() > 0;
        }
       
        /// <summary>
        /// 查询角色是否有人扮演
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public Employee_RoleInfo GetEmployee_RoleInfoByRoleID(int _id)
        {
           return  t.Employee_RoleInfo.Where(a=>a.RoleID==_id).FirstOrDefault();
        }

        /// <summary>
        /// 根据角色id删除角色
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public bool DelRoleByRoleID(int _id)
        {
            t.RoleInfo.Remove(t.RoleInfo.Where(a => a.ID == _id).FirstOrDefault());
            return t.SaveChanges() > 0;
        }
    }
}
