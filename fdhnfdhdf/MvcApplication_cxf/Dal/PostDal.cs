using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class PostDal
    {
        testEntities t = new testEntities();

        /// <summary>
        /// 查询所有岗位
        /// </summary>
        /// <returns></returns>
        public List<PostInfo> GetAllPost()
        {
            return t.PostInfo.ToList();
        }

        /// <summary>
        /// 分页查询岗位信息
        /// </summary>
        /// <param name="_page"></param>
        /// <param name="_rows"></param>
        /// <returns></returns>
        public List<PostInfo> GetPagePostInfo(int _page, int _rows, string _name)
        {
            return t.PostInfo.Where(a => a.JobTitle.Contains(_name)).OrderBy(a => a.ID).Skip((_page - 1) * _rows).Take(_rows).ToList();
        }

        /// <summary>
        /// 获取模糊岗位信息总条数
        /// </summary>
        /// <returns></returns>
        public int GetCountBy(string _name)
        {
            return t.PostInfo.Where(a => a.JobTitle.Contains(_name)).Count();
        }


        /// <summary>
        /// 新增岗位
        /// </summary>
        /// <param name="_pi"></param>
        /// <returns></returns>
        public bool AddPost(PostInfo _pi)
        {
            t.PostInfo.Add(_pi);
            return t.SaveChanges() > 0;
        }

        /// <summary>
        /// 查询总条数
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        {
            return t.PostInfo.Count();
        }

        /// <summary>
        /// 在员工表中根据岗位id查询员工
        /// </summary>
        /// <returns></returns>
        public EmployeeInfo GetEmployeerByPostID(int? _id)
        {
            return t.EmployeeInfo.Where(a => a.PostID == _id).FirstOrDefault();
        }

        /// <summary>
        /// 根据岗位id删除岗位
        /// </summary>
        /// <returns></returns>
        public bool DelPostByID(int _id)
        {
            t.Entry<PostInfo>(new PostInfo { ID = _id }).State = System.Data.EntityState.Deleted;
            return t.SaveChanges() > 0;
        }
        /// <summary>
        /// 查询所有的部门信息
        /// </summary>
        /// <returns></returns>
        public List<Dept> GetAllDept()
        {
            return t.Dept.ToList();
        }

        /// <summary>
        /// 根据岗位id修改岗位
        /// </summary>
        /// <returns></returns>
        public bool UpdPostByID(PostInfo _pi)
        {
            PostInfo pi = t.PostInfo.Where(a => a.ID == _pi.ID).FirstOrDefault();
            pi.JobDescription = _pi.JobDescription;
            pi.JobTitle = _pi.JobTitle;
            pi.DeptID = _pi.DeptID;

            return t.SaveChanges() > 0;
        }

        /// <summary>
        /// 根据岗位id查询岗位
        /// </summary>
        /// <returns></returns>
        public PostInfo GetPostOne(int? _id)
        {
            return t.PostInfo.Where(a => a.ID == _id).FirstOrDefault();
        }


    }
}
