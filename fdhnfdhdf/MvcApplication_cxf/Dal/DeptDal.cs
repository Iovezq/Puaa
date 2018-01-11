using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class DeptDal
    {
        testEntities t = new testEntities();
        /// <summary>
        /// 查询部门信息
        /// </summary>
        /// <returns></returns>
        public List<Dept> GetDeptInfo()
        {
            return t.Dept.ToList();
        }
//        /// <summary>
//        /// 分页查询部门信息
//        /// </summary>
//        /// <param name="_page"></param>
//        /// <param name="_rows"></param>
//        /// <returns></returns>
//        public List<Dept> GetPageDeptInfo(int _page, int _rows)
//        {
//            return t.Database.SqlQuery<Dept>(@"select *from (
//select ROW_NUMBER()over (order by id)as 'rownumber',*from Dept
//)temp where temp.rownumber between {0} and {1}", (_page - 1) * _rows + 1, _rows * _page).ToList();
//        }

        /// <summary>
        /// 分页查询部门信息
        /// </summary>
        /// <param name="_page"></param>
        /// <param name="_rows"></param>
        /// <returns></returns>
        public List<Dept> GetPageDeptInfo(int _page, int _rows, string _name)
        {
           return t.Dept.Where(a => a.DeptName.Contains(_name)).OrderBy(a => a.ID).Skip((_page - 1) * _rows).Take(_rows).ToList();
        }


        /// <summary>
        /// 获取部门信息总条数
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        {
            return t.Dept.Count();
        }

        /// <summary>
        /// 获取模糊部门信息总条数
        /// </summary>
        /// <returns></returns>
        public int GetCountBy(string _name)
        {
            return t.Dept.Where(a => a.DeptName.Contains(_name)).Count();
        }

        /// <summary>
        /// 根据岗位id获取岗位下的员工
        /// </summary>
        /// <param name="_DeptCode"></param>
        /// <returns></returns>
        public List<EmployeeInfo> GetEmployeerByPostID(int? _PostID)
        {
            return t.EmployeeInfo.Where(a => a.PostID == _PostID).ToList();
        }


        /// <summary>
        /// 根据部门id删除部门
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public bool DelDept(int _id)
        {
            t.Entry<Dept>(new Dept { ID = _id }).State = System.Data.EntityState.Deleted;
            return t.SaveChanges() > 0;
        }

         /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="_c"></param>
        /// <returns></returns>
        public bool AddDepts(Dept _d)
        {
            t.Dept.Add(_d);
            return t.SaveChanges() > 0;
        }


        /// <summary>
        /// 根据部门id查询部门
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public Dept GetDeptOne(int? _id)
        {
            return t.Dept.Where(a=>a.ID==_id).FirstOrDefault();
        }

        /// <summary>
        /// 修改部门信息
        /// </summary>
        /// <param name="_d"></param>
        /// <returns></returns>
        public bool UpdDept(Dept _d)
        {
            Dept d = t.Dept.Where(a=>a.ID==_d.ID).FirstOrDefault();
            d.DeptCode = _d.DeptCode;
            d.DeptDuties = _d.DeptDuties;
            d.DeptName = _d.DeptName;
            return t.SaveChanges() > 0;
        }

        /// <summary>
        /// 根据员工岗位id查询部门id
        /// </summary>
        /// <param name="_postID"></param>
        /// <returns></returns>
        public PostInfo GetDeptIDByPostID(int? _postID)
        {
            return t.PostInfo.Where(a => a.ID == _postID).FirstOrDefault();
        }

        /// <summary>
        /// 根据部门id获得该部门下的岗位
        /// </summary>
        /// <param name="_DeptID"></param>
        /// <returns></returns>
        public List<PostInfo> GetPostByDeptID(int? _DeptID)
        {
            return t.PostInfo.Where(a=>a.DeptID==_DeptID).ToList();
        }

        
    }
}
