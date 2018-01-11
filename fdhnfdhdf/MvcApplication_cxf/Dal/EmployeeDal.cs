using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using Model;

namespace Dal
{
    public class EmployeeDal
    {
        testEntities t = new testEntities();
        /// <summary>
        /// 判断登陆
        /// </summary>
        /// <param name="_u"></param>
        /// <returns></returns>
        public EmployeeInfo Login(EmployeeInfo _em)
        {
            EmployeeInfo em = t.EmployeeInfo.Where(a => a.LoginName == _em.LoginName && a.LoginPassword == _em.LoginPassword).FirstOrDefault();
            return em;
        }
        /// <summary>
        /// 通过用户ID查询用户权限
        /// </summary>
        /// <param name="_uid"></param>
        /// <returns></returns>
        public List<PermissionInfo> GetPermissionByID(int _emid)
        {
            //1.通过用户ID查询权限ID
            List<int?> premissionids = t.Employee_PermissionInfo.Where(a => a.EmployeeId == _emid).Select(a => a.PermissionID).ToList();

            //2.根据权限ID查询权限
            List<PermissionInfo> premissionlist = t.PermissionInfo.Where(a => premissionids.Contains(a.ID)).ToList();

            return premissionlist;
        }
        /// <summary>
        /// 通过用户id查询用户角色
        /// </summary>
        /// <param name="_roleid"></param>
        /// <returns></returns>
        public RoleInfo GetRoleByEmployeeID(int? _EmployeeId)
        {
            Employee_RoleInfo Em_role = t.Employee_RoleInfo.Where(a => a.EmployeeID == _EmployeeId).FirstOrDefault();
            RoleInfo role = new RoleInfo();
            if (Em_role != null)
            {
                role = t.RoleInfo.Where(a => a.ID == Em_role.RoleID).FirstOrDefault();
            }
            return role;
        }
        /// <summary>
        /// 获得所有页面的访问权限
        /// </summary>
        /// <returns></returns>
        public List<PermissionInfo> GetAllPermissions()
        {
            return t.PermissionInfo.ToList();
        }
        /// <summary>
        /// 根据用户名查询用户的日程安排
        /// </summary>
        /// <returns></returns>
        public List<Calendar> GetLoginerCalendar(int _loginID)
        {
            return t.Calendar.Where(a => a.EmployeeID == _loginID).ToList();
        }
        /// <summary>
        /// 根据日程id查询日程
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public Calendar GetCalendarByID(int _id)
        {
            return t.Calendar.Where(a => a.ID == _id).FirstOrDefault();
        }
        /// <summary>
        /// 根据日程id修改日程
        /// </summary>
        /// <param name="_c"></param>
        /// <returns></returns>
        public bool UpdCalendarByID(Calendar _c)
        {
            Calendar c = t.Calendar.Where(a => a.ID == _c.ID).FirstOrDefault();
            c.ID = _c.ID;
            c.ActionTime = _c.ActionTime;
            c.Schedule = _c.Schedule;
            c.ScheduleDate = _c.ScheduleDate;
            return t.SaveChanges() > 0;
        }
        /// <summary>
        /// 根据日程id删除日程
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public bool DelCalendarByID(int _id)
        {

            t.Entry<Calendar>(new Calendar { ID = _id }).State = System.Data.EntityState.Deleted;
            return t.SaveChanges() > 0;
        }

        /// <summary>
        /// 添加日程
        /// </summary>
        /// <param name="_c"></param>
        /// <returns></returns>
        public bool AddCalendar(Calendar _c)
        {
            t.Calendar.Add(_c);
            return t.SaveChanges() > 0;
        }
        /// <summary>
        /// 根据员工id删除员工
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public bool DelEmployeerByID(int _id)
        {
            t.Entry<EmployeeInfo>(new EmployeeInfo { ID = _id }).State = System.Data.EntityState.Deleted;
            return t.SaveChanges() > 0;
        }

        /// <summary>
        /// 查询该员工的薪资
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public Payroll SelEmployeerSalaryByID(int _id)
        {
            return t.Payroll.Where(a => a.EmployeeId == _id).FirstOrDefault();
        }

        /// <summary>
        /// 删除该员工的薪资
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public bool DelEmployeerSalaryByID(Payroll _p)
        {
            t.Payroll.Remove(_p);
            return t.SaveChanges() > 0;
        }

        /// <summary>
        /// 查询所有的员工信息
        /// </summary>
        /// <returns></returns>
        public List<EmployeeInfo> GetAllEmployeer()
        {
            return t.EmployeeInfo.ToList();
        }

        /// <summary>
        /// 分页查询员工信息
        /// </summary>
        /// <param name="_page"></param>
        /// <param name="_rows"></param>
        /// <returns></returns>
        //        public List<EmployeeInfo> oldGetEmployeerInfo(int _page, int _rows)
        //        {
        //            return t.Database.SqlQuery<EmployeeInfo>(@"select *from (
        //select ROW_NUMBER()over (order by id)as 'rownumber',*from EmployeeInfo
        //)temp where temp.rownumber between {0} and {1}", (_page - 1) * _rows + 1, _rows * _page).ToList();
        //        }

        /// <summary>
        /// 分页模糊查询查询员工信息
        /// </summary>
        /// <param name="_page"></param>
        /// <param name="_rows"></param>
        /// <returns></returns>
        public List<EmployeeInfo> GetEmployeerInfo(int _page, int _rows, string _content)
        {
            var sList = t.EmployeeInfo.Where(a => a.LoginName.Contains(_content)).OrderBy(a => a.ID).Skip((_page - 1) * _rows).Take(_rows).ToList();
            return sList;
        }


        /// <summary>
        /// 查询员工人数
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        {
            return t.EmployeeInfo.Count();
        }
        /// <summary>
        /// 查询员工人数+条件
        /// </summary>
        /// <returns></returns>
        public int GetCountByName(string _name)
        {
            return t.EmployeeInfo.Where(a => a.LoginName.Contains(_name)).Count();
        }
        /// <summary>
        /// 查询所有的员工信息除了登陆的员工
        /// </summary>
        /// <returns></returns>
        public List<EmployeeInfo> GetAllEmployeerElse(int _loginID)
        {
            return t.EmployeeInfo.Where(a => a.ID != _loginID).ToList();
        }

        /// <summary>
        /// 根据员工id查询员工
        /// </summary>
        /// <returns></returns>
        public EmployeeInfo GetEmployeerByID(int? _id)
        {
            return t.EmployeeInfo.Where(a => a.ID == _id).FirstOrDefault();
        }
        /// <summary>
        /// 根据员工昵称查询员工
        /// </summary>
        /// <returns></returns>
        public EmployeeInfo GetEmployeerByLoginName(string _xuname)
        {
            return t.EmployeeInfo.Where(a => a.LoginName == _xuname).FirstOrDefault();
        }
        /// <summary>
        /// 修改我的个人信息
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="_em"></param>
        /// <returns></returns>
        public bool UpdMy(EmployeeInfo _em)
        {
            EmployeeInfo my = t.EmployeeInfo.Where(a => a.ID == _em.ID).FirstOrDefault();
            my.IDNumber = _em.IDNumber;
            if (_em.EmployeeHead != "" && _em.EmployeeHead != null)
            {
                my.EmployeeHead = _em.EmployeeHead;
            }
            my.LoginName = _em.LoginName;
            my.LoginPassword = _em.LoginPassword;
            my.Sex = _em.Sex;
            my.IDNumber = _em.IDNumber;
            my.BirthDay = _em.BirthDay;
            my.Phone = _em.Phone;
            my.Email = _em.Email;
            my.EmployeeAddress = _em.EmployeeAddress;
            my.Degree = _em.Degree;
            my.EmployeeProfile = _em.EmployeeProfile;
            return t.SaveChanges() > 0;
        }

        /// <summary>
        /// 根据员工id修改员工
        /// </summary>
        /// <param name="_em"></param>
        /// <returns></returns>
        public bool UpdEmployeerInfo(EmployeeInfo _em)
        {
            return Update(_em);
        }

        /// <summary>
        /// 修改员工的方法
        /// </summary>
        /// <param name="_em"></param>
        /// <returns></returns>
        public bool Update(EmployeeInfo _em)
        {
            //拿到当前对象
            var me = t.Entry<EmployeeInfo>(_em);
            //设置状态
            me.State = System.Data.EntityState.Unchanged;
            //得到当前泛型类的所有属性
            PropertyInfo[] pt = _em.GetType().GetProperties();
            for (int i = 0; i < pt.Length; i++)
            {
                //拿到要更新的字段
                object obj = pt[i].GetValue(_em);
                //更新
                if (obj != null) me.Property(pt[i].Name).IsModified = true;
            }
            return t.SaveChanges() > 0;
        }

        /// <summary>
        /// 根据员工id查询员工日程
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public List<Calendar> GetCalendarByEmID(int _id)
        {
            return t.Calendar.Where(a => a.EmployeeID == _id).ToList();
        }
        /// <summary>
        /// 添加员工
        /// </summary>
        /// <param name="_c"></param>
        /// <returns></returns>
        public bool AddEmployeer(EmployeeInfo _em)
        {
            t.EmployeeInfo.Add(_em);
            return t.SaveChanges() > 0;
        }

        /// <summary>
        /// 根据角色id查询角色权限
        /// </summary>
        /// <param name="_roleID"></param>
        /// <returns></returns>
        public List<RoleInfo_PermissionInfo> GetPermissionsByRoleID(int? _roleID)
        {
            return t.RoleInfo_PermissionInfo.Where(a => a.RoleId == _roleID).ToList();
        }

        /// <summary>
        /// 根据员工id 查询该员工的所有权限
        /// </summary>
        /// <param name="_roleID"></param>
        /// <param name="_loginID"></param>
        /// <returns></returns>
        public List<PermissionInfo> GetPers( int? _loginID)
        {
            SqlParameter[] pars ={                         
                new SqlParameter( "@EmployeeID",_loginID),
            };

            List<PermissionInfo> havepers = t.Database.SqlQuery<PermissionInfo>(@"select * from PermissionInfo a where ID in 
                                ( select PermissionID from Employee_PermissionInfo  where EmployeeID=@EmployeeID)", pars).ToList();
            return havepers;
        }


        /// <summary>
        /// 根据员工id和员工角色id 查询该员工没有的权限
        /// </summary>
        /// <param name="_roleID"></param>
        /// <param name="_loginID"></param>
        /// <returns></returns>
        public List<PermissionInfo> GetNotPers(int? _roleID, int? _loginID)
        {
            SqlParameter[] pars ={                         
                new SqlParameter( "@EmployeeID",_loginID),
                new SqlParameter( "@RoleID",_roleID),
            };

            List<PermissionInfo> nothavepers = t.Database.SqlQuery<PermissionInfo>(@"select * from PermissionInfo a where not exists (
select * from(
select a.PermissionID from Employee_PermissionInfo a where a.EmployeeID=@EmployeeID UNION
(select a.PermissionID from RoleInfo_PermissionInfo a join Employee_RoleInfo b on a.RoleId=b.RoleID
where b.EmployeeID=@EmployeeID and b.RoleID=@RoleID)
)tb where a.ID=tb.PermissionID
)", pars).ToList();

            return nothavepers;

        }



        //public List<PermissionInfo> GetAllPermiddionsID(List<int> _ids)
        //{
        //    List<PermissionInfo> perslist=null;
        //    return perslist;
        //}


        /// <summary>
        /// 查询所有员工工资
        /// </summary>
        /// <returns></returns>
        public List<Payroll> GetPayrolls()
        {
            return t.Payroll.ToList();
        }

        /// <summary>
        /// 添加员工工资
        /// </summary>
        /// <param name="_p"></param>
        /// <returns></returns>
        public bool AddPay(Payroll _p)
        {
            t.Payroll.Add(_p);
            return t.SaveChanges() > 0;
        }

        /// <summary>
        /// 根据员工id查询员工工资
        /// </summary>
        /// <returns></returns>
        public Payroll GetPayByid(int? _id)
        {
            return t.Payroll.Where(a => a.EmployeeId == _id).FirstOrDefault();

        }

        /// <summary>
        /// 修改员工岗位
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="_num"></param>
        /// <returns></returns>
        public bool UpdEmp(int? _id, int? _num)
        {
            EmployeeInfo em = t.EmployeeInfo.Where(a => a.ID == _id).FirstOrDefault();
            em.PostID = _num;
            return t.SaveChanges() > 0;
        }


        /// <summary>
        /// 修改员工权限（根据员工ID和角色Id查找）
        /// </summary>
        /// <param name="_roleID">角色ID</param>
        /// <param name="emid">员工ID</param>
        /// <returns>查询到的权限</returns>
        public List<PermissionInfo> GetPersALL(int _roleID, int? _loginID)
        {
            SqlParameter[] pars ={                         
                new SqlParameter( "@EmployeeID",_loginID),
                new SqlParameter( "@RoleID",_roleID),
            };

            List<PermissionInfo> nothavepers = t.Database.SqlQuery<PermissionInfo>(@"select * from PermissionInfo a where exists (
                                    select * from(
                                    select a.PermissionID from Employee_PermissionInfo a where a.EmployeeID=@EmployeeID UNION
                                    (select a.PermissionID from RoleInfo_PermissionInfo a join Employee_RoleInfo b on a.RoleId=b.RoleID
                                    where b.EmployeeID=@EmployeeID and b.RoleID=@RoleID)
                                    )tb where a.ID=tb.PermissionID
                                    )", pars).ToList();

            return nothavepers;
        }
    }
}
