using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class EmailDal
    {
        testEntities t = new testEntities();

        /// <summary>
        ///草稿箱
        /// </summary>
        /// <returns></returns>
        public List<SendEmailMessages> GetWriteEmail(int _SenderID,int _IsSend, int _IsDel)
        {
            return t.SendEmailMessages.Where(a => a.SenderID == _SenderID && a.IsDel == _IsDel && a.IsSend==_IsSend).ToList();
        }
        /// <summary>
        /// 查询收件箱
        /// </summary>
        /// <param name="_loginID"></param>
        /// <returns></returns>
        public List<GetEmailMessages> GetHaverEmail(int _HaverID, int _IsDel, int _IsRead)
        {
            return t.GetEmailMessages.Where(a => a.HaverID == _HaverID && a.IsDel == _IsDel && a.IsRead == _IsRead).ToList();
        }
        /// <summary>
        /// 查询发件箱
        /// </summary>
        /// <returns></returns>
        public List<SendEmailMessages> GetSendEmail(int _SenderID, int _IsSend, int _IsDel)
        {
            return t.SendEmailMessages.Where(a => a.SenderID == _SenderID && a.IsSend == _IsSend && a.IsDel == _IsDel).ToList();
        }
        /// <summary>
        /// 查询在收件箱被删除的邮件
        /// </summary>
        /// <returns></returns>
        public List<GetEmailMessages> GetDelHaverEmail(int _HaverID, int _IsDel)
        {
            return t.GetEmailMessages.Where(a => a.HaverID == _HaverID && a.IsDel == _IsDel).ToList();
        }
        /// <summary>
        /// 查询在收件箱已读的邮件
        /// </summary>
        /// <returns></returns>
        public List<GetEmailMessages> GetReadEmail(int _HaverID, int _IsRead, int _IsDel)
        {
            return t.GetEmailMessages.Where(a => a.HaverID == _HaverID && a.IsRead == _IsRead && a.IsDel == _IsDel).ToList();
        }
        /// <summary>
        /// 添加邮件(Send)
        /// </summary>
        /// <returns></returns>
        public bool AddSendEmail(SendEmailMessages _email)
        {
            t.SendEmailMessages.Add(_email);
            return t.SaveChanges() > 0;
        }

        /// <summary>
        /// 添加邮件(Get)
        /// </summary>
        /// <returns></returns>
        public bool AddGetEmail(GetEmailMessages _email)
        {
            t.GetEmailMessages.Add(_email);
            return t.SaveChanges() > 0;
        }

        /// <summary>
        /// 根据邮件id删除‘已发送’邮件
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public bool DelSendEmailByID(int _id)
        {
            t.Entry<SendEmailMessages>(new SendEmailMessages { ID = _id }).State = System.Data.EntityState.Deleted;
            return t.SaveChanges() > 0;
        }

         /// <summary>
        /// 根据邮件id删除‘已删除’邮件
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public bool DelDeletedEmailByID(int _id)
        {
            t.Entry<GetEmailMessages>(new GetEmailMessages { ID = _id }).State = System.Data.EntityState.Deleted;
            return t.SaveChanges() > 0;
        }
       

        /// <summary>
        /// 根据邮件id查询"收件箱"邮件
        /// </summary>
        /// <returns></returns>
        public GetEmailMessages GetEmailByID(int _id)
        {
            return t.GetEmailMessages.Where(a => a.ID == _id).FirstOrDefault();
        }


        /// <summary>
        /// 根据id将"收件箱"邮件是否被删除的状态改变
        /// </summary>
        /// <param name="_rs"></param>
        /// <returns></returns>
        public bool UpdEmail(GetEmailMessages _em)
        {
            GetEmailMessages em = t.GetEmailMessages.Where(a => a.ID == _em.ID).FirstOrDefault();
            em.IsDel = _em.IsDel;
            return t.SaveChanges() > 0;
        }

       






    }
}
