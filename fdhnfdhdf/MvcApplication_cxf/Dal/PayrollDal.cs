using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class PayrollDal
    {
        /// <summary>
        /// 修改员工工资
        /// </summary>
        /// <param name="_emid"></param>
        /// <param name="_BasicWage"></param>
        /// <param name="_LunchAllowance"></param>
        /// <param name="_MeritPay"></param>
        /// <param name="_TrafficAllowance"></param>
        /// <param name="_CommunicationAllowance"></param>
        /// <returns></returns>
        public bool Upd(int _emid, int _BasicWage, int _LunchAllowance, int _MeritPay, int _TrafficAllowance, int _CommunicationAllowance)
        {
            testEntities t = new testEntities();
            Payroll p = t.Payroll.Where(a => a.EmployeeId == _emid).FirstOrDefault();
            p.BasicWage = _BasicWage;
            p.LunchAllowance = _LunchAllowance;
            p.MeritPay = _MeritPay;
            p.TrafficAllowance = _TrafficAllowance;
            p.CommunicationAllowance = _CommunicationAllowance;
            return t.SaveChanges() > 0;
        }

        /// <summary>
        /// 添加工资发放记录
        /// </summary>
        /// <param name="_sr"></param>
        /// <returns></returns>
        public bool AddPayRecord(SalaryRecord _sr)
        {
            testEntities t = new testEntities();
            t.SalaryRecord.Add(_sr);
            return t.SaveChanges() > 0;
        }

        /// <summary>
        /// 根据员工id查询该员工的工资发放记录
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public List<SalaryRecord> GetByID(int _id)
        {
            testEntities t = new testEntities();
            return t.SalaryRecord.Where(a => a.EmployeeId == _id).ToList();
        }

        /// <summary>
        /// 查询所有员工工资发放记录
        /// </summary>
        /// <returns></returns>
        public List<SalaryRecord> GetSalaryRecord()
        {
            testEntities t = new testEntities();
            return t.SalaryRecord.ToList();
        }
       

        /// <summary>
        /// 根据年查询所有员工工资发放记录
        /// </summary>
        /// <returns></returns>
        public List<SalaryRecord> GetSalaryRecordByYear(string _year)
        {
            testEntities t = new testEntities();
            return t.Database.SqlQuery<SalaryRecord>(@"select*from SalaryRecord where YEAR(PayTime)='" + _year + "'").ToList();

        }
        /// <summary>
        /// 根据月查询所有员工工资发放记录
        /// </summary>
        /// <returns></returns>
        public List<SalaryRecord> GetSalaryRecordByMonth(string _month)
        {
            testEntities t = new testEntities();
            return t.Database.SqlQuery<SalaryRecord>(@"select*from SalaryRecord where MONTH(PayTime)='" + _month + "'").ToList();

        }
        /// <summary>
        /// 根据日查询所有员工工资发放记录
        /// </summary>
        /// <returns></returns>
        public List<SalaryRecord> GetSalaryRecordByDay(string _day)
        {
            testEntities t = new testEntities();
            return t.Database.SqlQuery<SalaryRecord>(@"select*from SalaryRecord where DAY(PayTime)='" + _day + "'").ToList();

        }
        /// <summary>
        /// 根据年月查询所有员工工资发放记录
        /// </summary>
        /// <returns></returns>
        public List<SalaryRecord> GetSalaryRecordByYearMonth(string _year,string _month)
        {
            testEntities t = new testEntities();
            return t.Database.SqlQuery<SalaryRecord>(@"select*from SalaryRecord where YEAR(PayTime)='" + _year + "' and MONTH(PayTime)='" + _month + "'").ToList();

        }
        /// <summary>
        /// 根据月日查询所有员工工资发放记录
        /// </summary>
        /// <returns></returns>
        public List<SalaryRecord> GetSalaryRecordByMonthDay(string _day, string _month)
        {
            testEntities t = new testEntities();
            return t.Database.SqlQuery<SalaryRecord>(@"select*from SalaryRecord where DAY(PayTime)='" + _day + "' and MONTH(PayTime)='" + _month + "'").ToList();

        }
        /// <summary>
        /// 根据年日查询所有员工工资发放记录
        /// </summary>
        /// <returns></returns>
        public List<SalaryRecord> GetSalaryRecordByYearDay(string _year, string _day)
        {
            testEntities t = new testEntities();
            return t.Database.SqlQuery<SalaryRecord>(@"select*from SalaryRecord where DAY(PayTime)='" + _day + "' and YEAR(PayTime)='" + _year + "'").ToList();

        }
        /// <summary>
        /// 根据年月日查询所有员工工资发放记录
        /// </summary>
        /// <returns></returns>
        public List<SalaryRecord> GetSalaryRecordByYearMonthDay(string _year, string _month, string _day)
        {
            testEntities t = new testEntities();
            return t.Database.SqlQuery<SalaryRecord>(@"select*from SalaryRecord where YEAR(PayTime)='" + _year + "' and MONTH(PayTime)='" + _month + "'and DAY(PayTime)='" + _day + "'").ToList();

        }
        /// <summary>
        /// 根据员工id删除该员工的工资发放记录(多条)
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public bool DelByID(List<SalaryRecord> _srl)
        {
            testEntities t = new testEntities();
            foreach (var item in _srl)
            {
                t.Entry<SalaryRecord>(new SalaryRecord { ID = item.ID }).State = System.Data.EntityState.Deleted;
                //t.SalaryRecord.Remove(item);
            }
            return t.SaveChanges() > 0;
        }

        /// <summary>
        /// 根据员工id删除该员工的工资发放记录(单条)
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public bool DelByID(int _id)
        {
            testEntities t = new testEntities();
            t.Entry<SalaryRecord>(new SalaryRecord { ID = _id }).State = System.Data.EntityState.Deleted;
            return t.SaveChanges() > 0;
        }




    }
}
