using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class TimeSheetDal
    {
        testEntities t = new testEntities();

        /// <summary>
        /// 签到（添加）
        /// </summary>
        /// <param name="_ts"></param>
        /// <returns></returns>
        public bool AddCheckin(TimeSheet _ts)
        {
            t.TimeSheet.Add(_ts);
            return t.SaveChanges()>0;
        }
        /// <summary>
        /// 签退（修改）
        /// </summary>
        /// <param name="_ts"></param>
        /// <returns></returns>
        public bool UpdCheck(TimeSheet _ts)
        {
            TimeSheet ts = t.TimeSheet.Where(a => a.ID == _ts.ID).FirstOrDefault();
            ts.SignBackTime = _ts.SignBackTime;
            return t.SaveChanges() > 0;
        }

        /// <summary>
        /// 根据员工id查询考勤表(一条)
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public TimeSheet GetTimeSheetByUserID(int _id)
        {
            return t.TimeSheet.Where(a => a.EmployeeID == _id).FirstOrDefault();
        }

        /// <summary>
        /// 根据员工id查询考勤表(多条)
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public List<TimeSheet> GetTimeSheetsByUserID(int _id)
        {
            return t.TimeSheet.Where(a => a.EmployeeID == _id).ToList();
        }
        /// <summary>
        /// 查询所有的考勤记录
        /// </summary>
        /// <returns></returns>
        public List<TimeSheet> GetTimeSheets()
        {
            return t.TimeSheet.ToList();
        }

        /// <summary>
        /// 根据id删除考勤记录
        /// </summary>
        /// <returns></returns>
        public bool DelTimeSheetByID(int _id)
        {
            t.Entry<TimeSheet>(new TimeSheet { ID = _id }).State = System.Data.EntityState.Deleted;
            return t.SaveChanges() > 0;
        }
    }
}
