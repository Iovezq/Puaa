using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class CalendarsDal
    {
        testEntities t = new testEntities();

        /// <summary>
        /// 按照执行的时间顺序查询所有日程
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public List<Calendar> GetCalendar()
        {
            return t.Calendar.OrderBy(a => a.ActionTime).ToList();
        }

        /// <summary>
        /// 根据日程表中的员工id查询员工姓名
        /// </summary>
        /// <param name="_c"></param>
        /// <returns></returns>
        public EmployeeInfo GetEmployeerByID(Calendar _c)
        {
            return t.EmployeeInfo.Where(a=>a.ID==_c.EmployeeID).FirstOrDefault();
        }

        /// <summary>
        /// 根据员工ids查询员工日程表
        /// </summary>
        /// <param name="_c"></param>
        /// <returns></returns>
        public List<Calendar> GetCalendarByID(int _emid)
        {
            return t.Calendar.Where(a => a.EmployeeID==_emid).ToList();
        }

        /// <summary>
        /// 根据日程ids删除日程
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public bool DelCalendarByID(List<Calendar> _clist)
        {
            foreach (var cal in _clist)
            {
                //t.Entry<Calendar>(new Calendar { ID = cal.ID }).State = System.Data.EntityState.Deleted;
                t.Calendar.Remove(cal);
            }
           
            return t.SaveChanges() > 0;
        }

       
    }
}
