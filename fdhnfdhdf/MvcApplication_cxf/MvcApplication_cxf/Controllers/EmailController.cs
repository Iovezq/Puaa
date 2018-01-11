using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Dal;
using Model;

namespace MvcApplication_cxf.Controllers
{
    public class EmailController : BaseController
    {
        //
        // GET: /Email/

        public EmailDal dal = new EmailDal();

        /// <summary>
        /// 查询所有邮箱的收件数
        /// </summary>
        public void GetNewEmail()
        {
            //获取登录用户的id
            EmployeeInfo emp = (EmployeeInfo)Session["Employeer"];

            /*收件箱 GetEmailMessages HaverID,IsDel=0,IsRead=0*/
            List<GetEmailMessages> get = dal.GetHaverEmail(emp.ID, 0, 0);
            Session["HaveNum"] = get.Count;

            /*发件箱 SendEmailMessages SenderID,IsSend=1,IsDel=0*/
            List<SendEmailMessages> send = dal.GetSendEmail(emp.ID, 1, 0);
            Session["SendedNum"] = send.Count;

            /*草稿箱 SendEmailMessages SenderID,IsRead=0,IsDel=0*/
            List<SendEmailMessages> write = dal.GetWriteEmail(emp.ID, 0, 0);
            Session["WritedNum"] = write.Count;

            /*已删除 只接受‘在收件箱被删除的’ GetEmailMessages HaverID,IsDel=1*/
            List<GetEmailMessages> dels = dal.GetDelHaverEmail(emp.ID, 1);
            Session["DeletedNum"] = dels.Count;

            /*已读 GetEmailMessages HaverID,IsRead=1,IsDel=0*/
            List<GetEmailMessages> read = dal.GetReadEmail(emp.ID, 1, 0);
            Session["ReadNum"] = read.Count;



        }

        /// <summary>
        /// 邮件管理页面(收件箱)
        /// </summary>
        /// <returns></returns>
        public ActionResult EmailManage()
        {
            EmployeeInfo emp = (EmployeeInfo)Session["Employeer"];
            List<GetEmailMessages> get = dal.GetHaverEmail(emp.ID, 0, 0);
            ViewBag.MES = get;
            ViewBag.HaverName = emp.LoginName;
            GetNewEmail();//刷新邮件数量


            // 处理"收件箱"页面的已读

            //邮件id
            if (!string.IsNullOrEmpty(Request["id"]))
            {
                int id = Convert.ToInt32(Request["id"]);
                //根据id查询
                GetEmailMessages g = dal.GetEmailByID(id);
                g.IsRead = 1;
                //根据id修改
                bool isUpd = dal.UpdEmail(g);
                if (isUpd)
                {
                    return Content("1");
                }
            }
            return View();
        }

        /// <summary>
        /// 发送邮件页面(发件箱)
        /// </summary>
        /// <returns></returns>
        public ActionResult SendEmail()
        {
            Model.EmployeeInfo em = (Model.EmployeeInfo)Session["Employeer"];
            //查询所有员工
            EmployeeDal edal = new EmployeeDal();
            List<EmployeeInfo> emlist = edal.GetAllEmployeerElse(em.ID);
            ViewBag.allem = emlist;
            GetNewEmail();//刷新邮件数量

            return View();
        }
        /// <summary>
        /// (已发送)
        /// </summary>
        /// <returns></returns>
        public ActionResult SendedEmail()
        {
            EmployeeInfo emp = (EmployeeInfo)Session["Employeer"];
            List<SendEmailMessages> send = dal.GetSendEmail(emp.ID, 1, 0);
            ViewBag.sended = send;
            ViewBag.SenderName = emp.LoginName;
            GetNewEmail();//刷新邮件数量

            return View();
        }

        /// <summary>
        /// (已删除)
        /// </summary>
        /// <returns></returns>
        public ActionResult DeletedEmail()
        {
            EmployeeInfo emp = (EmployeeInfo)Session["Employeer"];
            List<GetEmailMessages> emailList = dal.GetDelHaverEmail(emp.ID, 1);
            ViewBag.deleted = emailList;
            ViewBag.HaverName = emp.LoginName;
            GetNewEmail();//刷新邮件数量

            return View();
        }
        /// <summary>
        /// (已读)
        /// </summary>
        /// <returns></returns>
        public ActionResult ReadEmail()
        {
            EmployeeInfo emp = (EmployeeInfo)Session["Employeer"];
            List<GetEmailMessages> read = dal.GetReadEmail(emp.ID, 1, 0);
            ViewBag.read = read;
            ViewBag.HaverName = emp.LoginName;
            GetNewEmail();//刷新邮件数量

            return View();
        }
        /// <summary>
        /// (草稿箱)
        /// </summary>
        /// <returns></returns>
        public ActionResult WritedEmail()
        {
            EmployeeInfo emp = (EmployeeInfo)Session["Employeer"];
            List<SendEmailMessages> write = dal.GetWriteEmail(emp.ID, 0, 0);
            ViewBag.addsend = write;
            ViewBag.SendName = emp.LoginName;
            GetNewEmail();//刷新邮件数量

            return View();
        }

        /// <summary>
        /// 处理添加邮件
        /// </summary>
        /// <returns></returns>
        public ActionResult DealAdd()
        {
            //获取登录用户的id
            EmployeeInfo emp = (EmployeeInfo)Session["Employeer"];

            //接收前台的数据
            int HaverID = Convert.ToInt32(Request["HaverID"]);
            string Title = Request["Title"];
            string Content = Request["Content"];
            DateTime dt = DateTime.Now;
            string SendTime = dt.ToString();

            //获取保存位置
            string way = Request["way"];
            //群发（发送）
            if (way == "sendmany")
            {
                //接收员工IDS
                string ids = Request["ids"];
                if (ids != null)
                {
                    List<string> list = ids.Split(',').ToList();
                    for (int i = 0; i < list.Count() - 1; i++)
                    {
                        //添加邮件
                        SendEmailMessages s = new SendEmailMessages();
                        s.SenderID = emp.ID;
                        s.HaverID = Convert.ToInt32(list[i]);
                        s.Title = Title;
                        s.Content = Content;
                        s.SendTime = SendTime;
                        s.IsSend = 1;
                        s.IsRead = 0;
                        s.IsDel = 0;
                        bool isadd1 = dal.AddSendEmail(s);

                        GetEmailMessages g = new GetEmailMessages();
                        g.SenderID = emp.ID;
                        g.HaverID = Convert.ToInt32(list[i]);
                        g.Title = Title;
                        g.Content = Content;
                        g.SendTime = SendTime;
                        g.IsRead = 0;
                        g.IsDel = 0;
                        bool isadd2 = dal.AddGetEmail(g);
                    }
                }
            }
            //发送
            if (way == "send")
            {
                SendEmailMessages s = new SendEmailMessages();
                s.SenderID = emp.ID;
                s.HaverID = HaverID;
                s.Title = Title;
                s.Content = Content;
                s.SendTime = SendTime;
                s.IsSend = 1;
                s.IsRead = 0;
                s.IsDel = 0;
                bool isadd1 = dal.AddSendEmail(s);

                GetEmailMessages g = new GetEmailMessages();
                g.SenderID = emp.ID;
                g.HaverID = HaverID;
                g.Title = Title;
                g.Content = Content;
                g.SendTime = SendTime;
                g.IsRead = 0;
                g.IsDel = 0;
                bool isadd2 = dal.AddGetEmail(g);

                if (isadd1 && isadd2)
                {
                    return RedirectToAction("SendEmail");
                }
            }
            //保存
            if (way == "addsend")
            {
                SendEmailMessages s = new SendEmailMessages();
                s.SenderID = emp.ID;
                s.HaverID = HaverID;
                s.Title = Title;
                s.Content = Content;
                s.SendTime = SendTime;
                s.IsSend = 0;
                s.IsRead = 0;
                s.IsDel = 0;
                bool isadd3 = dal.AddSendEmail(s);
                if (isadd3)
                {
                    return RedirectToAction("SendEmail");
                }
            }


            return RedirectToAction("SendEmail");
        }

        /// <summary>
        /// 处理在已发送页面的删除
        /// </summary>
        /// <returns></returns>
        public ActionResult ExeDelEmailOnSended()
        {
            //删除一个
            if (Request["id"] != null)
            {
                int id = Convert.ToInt32(Request["id"]);
                //根据id删除
                bool isDel = dal.DelSendEmailByID(id);
                if (isDel)
                {
                    RedirectToAction("SendedEmail");
                }
            }

            //删除多个
            if (Request["ids"] != null)
            {
                string ids = Request["ids"];
                if (ids != null)
                {
                    bool isDel = false;
                    List<string> list = ids.Split(',').ToList();
                    for (int i = 0; i < list.Count() - 1; i++)
                    {
                        //根据id删除
                        isDel = dal.DelSendEmailByID(Convert.ToInt32(list[i]));
                    }
                    if (isDel)
                    {
                        RedirectToAction("SendedEmail");
                    }
                }
            }



            return RedirectToAction("SendedEmail");
        }

        /// <summary>
        /// 处理在收件箱页面的删除
        /// </summary>
        /// <returns></returns>
        public ActionResult ExeDelEmailOnDeleted()
        {
            int id = Convert.ToInt32(Request["id"]);
            //根据id删除
            bool isDel = dal.DelDeletedEmailByID(id);
            if (isDel)
            {
                RedirectToAction("DeletedEmail");
            }

            return RedirectToAction("DeletedEmail");
        }

        /// <summary>
        /// 处理在草稿箱页面的删除
        /// </summary>
        /// <returns></returns>
        public ActionResult ExeDelEmailOnWrited()
        {
            int id = Convert.ToInt32(Request["id"]);
            //根据id删除
            bool isDel = dal.DelSendEmailByID(id);
            if (isDel)
            {
                RedirectToAction("WritedEmail");
            }

            return RedirectToAction("WritedEmail");
        }

        /// <summary>
        /// 处理"收件箱"页面的删除
        /// </summary>
        /// <returns></returns>
        public ActionResult ExeDelGetEmail()
        {
            int id = Convert.ToInt32(Request["id"]);
            //根据id查询
            GetEmailMessages g = dal.GetEmailByID(id);
            g.IsDel = 1;
            //根据id修改
            bool isUpd = dal.UpdEmail(g);
            if (isUpd)
            {
                RedirectToAction("EmailManage");
            }

            return RedirectToAction("EmailManage");
        }

        /// <summary>
        /// 处理"收件箱"页面的已读
        /// </summary>
        /// <returns></returns>
        public ActionResult ExeDelReadedEmail()
        {
            int id = Convert.ToInt32(Request["id"]);
            //根据id查询
            GetEmailMessages g = dal.GetEmailByID(id);
            g.IsDel = 1;
            //根据id修改
            bool isUpd = dal.UpdEmail(g);
            if (isUpd)
            {
                return RedirectToAction("ReadEmail");
            }

            return RedirectToAction("ReadEmail");
        }


        /// <summary>
        /// 处理"草稿箱"页面的发送
        /// </summary>
        /// <returns></returns>
        public ActionResult DealWritedAdd()
        {
            string id = Request["id"];
            string SenderID = Request["SenderID"];
            string HaverID = Request["HaverID"];
            string Title = Request["Title"];
            string Content = Request["Content"];//.Trim();
            string SendTime = Request["SendTime"];

            if (SenderID != null && SenderID != "")
            {
                SendEmailMessages s = new SendEmailMessages();
                s.SenderID = Convert.ToInt32(SenderID);
                s.HaverID = Convert.ToInt32(HaverID);
                s.Title = Title;
                s.Content = Content;
                s.SendTime = SendTime;
                s.IsSend = 1;
                s.IsRead = 0;
                s.IsDel = 0;
                bool isadd1 = dal.AddSendEmail(s);

                GetEmailMessages g = new GetEmailMessages();
                g.SenderID = Convert.ToInt32(SenderID);
                g.HaverID = Convert.ToInt32(HaverID);
                g.Title = Title;
                g.Content = Content;
                g.SendTime = SendTime;
                g.IsRead = 0;
                g.IsDel = 0;
                bool isadd2 = dal.AddGetEmail(g);

                if (isadd1 && isadd2)
                {
                    //发送成功后就删除在草稿箱的记录
                    bool isDel = dal.DelSendEmailByID(Convert.ToInt32(id));
                    if (isDel)
                    {
                        return RedirectToAction("WritedEmail");
                    }
                }
            }
            return RedirectToAction("WritedEmail");
        }


    }
}
