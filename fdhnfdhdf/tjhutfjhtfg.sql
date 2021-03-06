/*
建库
*/
use master
go
if exists( select *from sys.sysdatabases where name='test' )
	drop database test
go
create database test
go
use test
go
/*
建表
*/
----------------------------------------------部门表
create table Dept
(
ID int primary key identity,
DeptCode varchar(32),--部门编码
DeptName varchar(32),--部门名称
DeptDuties text,--部门职责
)
go
insert Dept values('001','技术部',' 1.负责对新产品的设计和开发的控制及编制各类技术文件。2.参与产品实现的策划。3.参与合同评审...')
insert Dept values('002','技术研发部','1.在执行总经理的领导下开展工作。2.负责公司新产品、...')
insert Dept values('003','技术支持部','1. 收集竞争者产品的功能、性能信息，比较我方产品的优缺点，提出同类产品比较报告。 2. 对公司产品熟练的运用，并能从机理上...')
insert Dept values('004','协劣部门','1.实施和改进货量管理体系2.代表评审、形成经营理念和戓略,并 在此基础上策划和发布公司...')
insert Dept values('005','销售部门','1.需求分析、销售预测。 2.确定销售部门目标体系和销售配额。 3.对所属下级的工作有监督检查权。 4.对所属下级的工作争议有裁决权。 5.对直接下级... ')

go

----------------------------------------------岗位表
create table PostInfo
(
ID int primary key identity,
JobTitle varchar(32),--岗位名称
JobDescription text,--岗位描述
DeptID int,--所在部门ID
)
go
 
insert PostInfo values('部门经理','属基本工作岗位，对IT软件部门整体负责。全面负责IT软件部门业务规划、队伍建设、应用解决方案、软件产品研发、项目实施等各项综合管理工作。规划人数一般建议为1人',null)



--'001','技术部'
insert PostInfo values('项目经理','属非岗,在每项具体的应用解决方案策划、咨询、实施服务工作开始前，根据项目实际需要做出任命，负责项目的全过程管理，对项目成败全权负责。',1)
insert PostInfo values('项目总监','1.负责项目的总体运行与推进,指导各项目开发单位工作,各单位工作结果进行完成 情况判定 2.负责项目一级编制,二级计划审批及执行监督、考核和管理; ...',1)
insert PostInfo values('项目实施工程师','进行软件前期的项目需求分析，对项目进行风险评估并解决这些风险，然后进行软件开发，后期对软件的进度做相关的评估。软件实施工程师的工作...',1)


--'002','技术研发部'
insert PostInfo values('技术总监','将科学研究成果转为营利项目。负责一个企业的技术管理体系的建设和维护，制定技术标准和相关流程，能够带领和激励自己的团队完成公司赋予的任务，实现公司的技术管理和支撑目标，为公司创造价值。 ',2)
insert PostInfo values('系统分析师 ','属基本工作岗位，为业务领域专家，同时应为主流技术架构设计专家（建议同时具备架构师的能力），负责解决方案策划、软件产品规划、软件项目需求设计、架构设计等技术把关工作。规划人数一般建议每个团队为2-3人。',2)
insert PostInfo values('系统构架师','是一个最终确认和评估系统需求、给出开发规范、搭建系统实现的核心构架，并澄清技术细节、扫清主要难点的技术人员。主要着眼于系统的“技术实现”。因此他/她应该是特定的开发平台、语言、工具的大师，对常见应用场景...',2)
insert PostInfo values('开发工程师 ','属基本工作岗位，为专业的软件设计开发人员，具有精湛的开发技术、优良的开发习惯和良好的合作精神，专门负责软件产品的研发工作。规划人数视具体情况而定。',2)
insert PostInfo values('开发经理','属非岗，在每项具体的软件产品研发工作开始前，根据产品研发工作实际需要做出任命，负责相应产品开发的全过程管理，对产品质量全权负责 ',2)
insert PostInfo values('产品经理','指在公司中，针对某一项或是某一类的产品进行规划和管理的人员，主要负...',2)

--'003','技术支持部'
insert PostInfo values('咨询工程师 ','属基本工作岗位，为业务领域专家，专门负责应用解决方案策划工作，为软件项目的售前支持、产品的推广营销提供技术支撑。规划人数视具体情况而定。',3)
insert PostInfo values('测试经理','有效的领导一个测试团队。为了更好的履行这个职责，测试经理必须理解测试的基本原则，在履行一个传统的领导角色的同时还应懂得该如何有效地实...',3)
insert PostInfo values('测试工程师','1.要有较好的编写代码的水平，最好是自己亲自独立完成过某软件的开发工作 2.需要对数据... ',3)

--'004','协劣部门'
insert PostInfo values('运维工程师','是集合网络、系统、开发工作于一身的“复合性人才”。',4)
insert PostInfo values('程序员','从事程序开发、维护的专业人员。',4)

--'005','销售部门'
insert PostInfo values('商务经理','1、全面负责公司商务管理相关事宜，规范商务流程，确保公司利益； 2、负责重大项目的商务谈判，审核商务合同条款，组织起草合作协议； 3、监控项目合作的开展...',5)
insert PostInfo values('销售经理','1.需求分析、销售预测。 2.确定销售部门目标体系和销售配额。 3.对所属下级的工作有监督检查权。 4.对所属下级的工作争议有裁决权。 5.对直接下级... ',5)
insert PostInfo values('售后工程师','更懂得产品的技术性能和原理，能够解答客户的专业性问题，排除客户...',5)
insert PostInfo values('销售员','推销研发产品',5)


go
----------------------------------------------员工表


create table EmployeeInfo
(
ID int primary key identity,
LoginName varchar(32),--用户名
LoginPassword varchar(32),--登录密码
EmployeeHead varchar(128),--员工头像
Sex int,--性别（0代表‘男’，1代表‘女’）
IDNumber bigint,--身份证号
BirthDay varchar(64),--出生日期
Phone bigint,--联系电话
Email varchar(64),--电子邮箱
EmployeeAddress text, --家庭住址
Degree varchar(64),--学历
PostID int,--岗位ID
DeptID int,--所在部门ID
EmployeeProfile text,--员工概况
Hiredate datetime,--入职日期(默认为系统当前日期)
Wage int,--工资
)
go

insert EmployeeInfo values('路明非','111111','t.jpg',0,513001199812231246,'1998/12/23',15323287503,'llz@qq.com','重庆江北区','学士',0,null,'三年it行业工作经验，熟悉主机板的生产流程、结构功能，对工程表单、bom等工程文件的制作发行都有全面细致的了解，熟悉各种不同的芯片组与motherboard各部件的功能，熟悉各种测试软件，有较强的bug分析与追踪能力，同时对cpu、bios、memory、显卡、网卡、storage等都有比较深刻的认识。','2012-12-01 08:10:36',12000)
insert EmployeeInfo values('凯撒','111111','ted.jpeg',0,220503197906194497,'1979/06/19',15123287503,'cyy@qq.com','吉林省通化市二道江区 ','硕士',1,null,'本年度我中心组织学员参加‘网上祭扫先烈’活动；开展的‘爱家乡，爱高陵’系列活动（泾渭分明健步行和庆‘六一’家乡文化之旅活动）；参加陕西省举办的青少年足球夏令营活动；制作新年祝福视频参与校外同仁联欢会。这些活动的精彩瞬间都被及时的发到‘两网’上，同全国校外同仁分享精彩活动。参加2016全国‘优秀网络社区’评选活动，我中心获‘优秀网络社区’的光荣称号。','2013-01-01 09:46:36',null)
insert EmployeeInfo values('楚子航','111111','e.jpg',1,130301197311250343,'1973/11/25',15823287503,'qjr@qq.com','河北省秦皇岛市市辖区  ','硕士',2,null,'我热爱自己的工作，积极运用有效的工作时间做好自己分内的工作。在做好各项校外教育工作的同时，严格遵守中心的各项规章制度。不论是分到哪一项工作，我都配合同事尽自己的努力把工作做好。','2013-02-01 10:34:36',null)
insert EmployeeInfo values('绘梨衣','111111','uuuyt.jpeg',1,433130197202067923,'1972/02/06',13923287503,'xtf@qq.com','湖南省湘西土家族苗族自治州龙山县','学士',3,null,'学习上，自从参加工作以来，我从没有放弃学习理论知识和业务知识。不但掌握和提高了计算机以及互联网方面的知识，也有了一定的理论水平，完全达到了研究生所具有的水准。学习理论的同时，更加钻研业务，把学到的计算机以及网络的知识融会到工作中去，使业务水平不断提高。','2013-01-02 11:32:36',null)
insert EmployeeInfo values('诺诺','111111','yyrejpg.jpg',1,220102199007249432,'1979/07/24',13923287503,'dws@qq.com','吉林省长春市南关区','学士',4,null,'学习上，自从参加工作以来，我从没有放弃学习理论知识和业务知识。不但掌握和提高了计算机以及互联网方面的知识，也有了一定的理论水平，完全达到了研究生所具有的水准。学习理论的同时，更加钻研业务，把学到的计算机以及网络的知识融会到工作中去，使业务水平不断提高。','2013-01-03 08:42:36',null)
insert EmployeeInfo values('芬格尔','111111','',1,642221197303144630,'1973/03/14',13923287503,'yyl@qq.com','宁夏回族自治区固原地区固原县','学士',5,null,'学习上，自从参加工作以来，我从没有放弃学习理论知识和业务知识。不但掌握和提高了计算机以及互联网方面的知识，也有了一定的理论水平，完全达到了研究生所具有的水准。学习理论的同时，更加钻研业务，把学到的计算机以及网络的知识融会到工作中去，使业务水平不断提高。','2013-01-03 09:58:36',null)
insert EmployeeInfo values('唐三','111111','',1,441521198611243490,'1978/06/11',13923287503,'djs@qq.com','广东省汕尾市海丰县','学士',6,null,'学习上，自从参加工作以来，我从没有放弃学习理论知识和业务知识。不但掌握和提高了计算机以及互联网方面的知识，也有了一定的理论水平，完全达到了研究生所具有的水准。学习理论的同时，更加钻研业务，把学到的计算机以及网络的知识融会到工作中去，使业务水平不断提高。','2013-01-03 10:59:36',null)
insert EmployeeInfo values('小舞','111111','',1,440883198808182429,'1998/08/18',15123287503,'xyy@qq.com','广东省湛江市吴川市','学士',6,null,'性格活泼大方，工作务实认真，诚实守信，有较强的自学与创新能力，有良好的语言表达能力和极强的团队合作精神。此外还具有良好的写作能力，曾多次发表文章并多次参加征文比赛并获奖。','2013-01-04 11:59:36',4000)

go


----------------------------------------------角色表
create table RoleInfo
(
ID int primary key identity,
RoleName varchar(32),--角色名称
)
insert RoleInfo values('系统管理员')
insert RoleInfo values('普通管理员')
insert RoleInfo values('员工管理员')
insert RoleInfo values('部门管理员')
insert RoleInfo values('岗位管理员')

go
----------------------------------------------员工角色表
create table Employee_RoleInfo
(
ID int primary key identity,
EmployeeID int,--员工id
RoleID int,--角色id
)
go
/*
	系统管理员
*/
insert Employee_RoleInfo values(1,1)--具有所有权能力（路明非）
/*
	普通管理员
*/
insert Employee_RoleInfo values(5,2)
insert Employee_RoleInfo values(6,2)
insert Employee_RoleInfo values(7,2)
insert Employee_RoleInfo values(8,2)
insert Employee_RoleInfo values(9,2)
insert Employee_RoleInfo values(10,2)
insert Employee_RoleInfo values(11,2)
insert Employee_RoleInfo values(12,2)
insert Employee_RoleInfo values(13,2)
insert Employee_RoleInfo values(14,2)
insert Employee_RoleInfo values(15,2)
insert Employee_RoleInfo values(16,2)
insert Employee_RoleInfo values(17,2)
insert Employee_RoleInfo values(18,2)
insert Employee_RoleInfo values(19,2)
insert Employee_RoleInfo values(20,2)
insert Employee_RoleInfo values(21,2)
insert Employee_RoleInfo values(22,2)
insert Employee_RoleInfo values(23,2)
insert Employee_RoleInfo values(24,2)
insert Employee_RoleInfo values(25,2)
/*
	员工管理员
*/
insert Employee_RoleInfo values(2,3)
/*
	部门管理员
*/
insert Employee_RoleInfo values(3,4)
/*
	岗位管理员
*/
insert Employee_RoleInfo values(4,5)
go
select *from RoleInfo
select*from EmployeeInfo
select *from Employee_RoleInfo
--select *from PermissionInfo

----------------------------------------------工资表
create table Payroll
(
ID int primary key identity,
BasicWage int,--基本工资
LunchAllowance int,--午餐补助
MeritPay int,--绩效工资
TrafficAllowance int,--交通补助
CommunicationAllowance int,--通讯补助
EmployeeId  int,--员工编号 
)
insert Payroll values(5000,400,400,100,50,3)
insert Payroll values(5000,300,500,50,50,4)
insert Payroll values(3000,100,300,80,50,5)
insert Payroll values(3000,300,200,100,50,6)
insert Payroll values(2000,200,300,60,50,7)
insert Payroll values(2000,100,100,10,50,8)
go
----------------------------------------------工资发放记录表
create table SalaryRecord
(
ID int primary key identity,
EmployeeId  int,--员工编号
TotalSalary int,--最终工资
PayTime datetime,--发放时间
)
insert SalaryRecord values(3,4100,'2014/12/02')
insert SalaryRecord values(3,4500,'2015/12/02')
insert SalaryRecord values(4,4300,'2014/12/02')
insert SalaryRecord values(5,3200,'2014/12/02')
go
----------------------------------------------发邮件表(1'true' 0'false')
create table SendEmailMessages
(
ID int primary key identity,
SenderID int,--发送人ID
HaverID int,--接收人ID
Title varchar(32),--发送标题
Content text,--发送内容
SendTime varchar(64),--发送时间
IsSend int,--发送状态（‘1’代表已发送，‘0’代表未发送）
IsRead int,--查询状态（‘1’代表已读，‘0’代表未读）
IsDel int,--删除状态（‘1’代表已删，‘0’代表未删）
)
------------------------------------------------收邮件表(1'true' 0'false')
create table GetEmailMessages
(
ID int primary key identity,
SenderID int,--发送人ID
HaverID int,--接收人ID
Title varchar(32),--发送标题
Content text,--发送内容
SendTime varchar(64),--发送时间
IsRead int,--查询状态（‘1’代表已读，‘0’代表未读）
IsDel int,--删除状态（‘1’代表已删，‘0’代表未未删删）
)

insert into GetEmailMessages values(2,1,'闲聊','路明非，我是凯撒','2015-10-25',0,0)
insert into GetEmailMessages values(3,1,'闲聊','今晚一起出去','2015-10-25',0,0)
insert into GetEmailMessages values(5,1,'闲聊','好久不见','2015-10-25',0,0)
----------------------------------------------考勤表
create table TimeSheet
(
ID int primary key identity,
EmployeeID int,--员工ID
OfficeHour varchar(32),--上班时间
QuittingTime varchar(32),--下班时间
Check_inTime varchar(32),--签到时间
SignBackTime varchar(32),-- 签退时间
Check_inType int,--签到状态
SignBackType int,-- 签退状态
Remark varchar(32),--签卡备注
)
go

----------------------------------------------日程表
create table Calendar
(
ID int primary key identity,
EmployeeID int,--员工id
Schedule text,--日程安排
ScheduleDate varchar(32),--日程星期
ActionTime varchar(32),--添加时间
)
go
insert Calendar values(1,'春节祝福','好好工作，天天向上','2016/11/16 08:30')
insert Calendar values(1,'爬山观景','好好工作，天天向上','2016/11/18 13:30')
insert Calendar values(2,'放假旅游','好好工作，天天向上','2016/11/17 08:30')
insert Calendar values(2,'高峰对话','好好工作，天天向上','2016/11/16 14:30')
insert Calendar values(3,'战略与前景','好好工作，天天向上','2016/11/15 08:30')
insert Calendar values(8,'产业政策视角下的大数据发展','好好工作，天天向上','2016/11/16 09:30')
insert Calendar values(2,'会议签到','好好工作，天天向上','2016/11/14 08:30')
insert Calendar values(1,'高峰对话','好好工作，天天向上','2016/11/17 13:30')


----------------------------------------------权限表
create table PermissionInfo
(
ID int primary key identity,--页面权限编号 
PageUrl  varchar(64),--页面路由
PageIcon varchar(64),--页面图标
PageName  varchar(64),--页面名称 
ParentID int,--父级ID
Remark text,--页面信息描述 
)
go
insert PermissionInfo values(null,'icon-user','个人中心',null,'')
insert PermissionInfo values(null,'icon-cog','部门管理',null,'') 
insert PermissionInfo values(null,'icon-user-md','员工管理',null,'') 
insert PermissionInfo values(null,'icon-cogs','岗位管理',null,'') 
insert PermissionInfo values(null,'icon-money','薪酬管理',null,'') 
insert PermissionInfo values(null,'icon-envelope','信息交流管理',null,'')
insert PermissionInfo values(null,'icon-lock','员工权限管理',null,'')
insert PermissionInfo values(null,'icon-bell','员工考勤管理',null,'') 
insert PermissionInfo values(null,'icon-calendar','员工日程管理',null,'')--9
              
insert PermissionInfo values('/home/mylists',null,'我的日程',1,'归类于个人中心')--10
insert PermissionInfo values('/home/myinfo',null,'我的信息',1,'归类于个人中心')--11
 
insert PermissionInfo values('/dept/deptlist',null,'部门列表',2,'归类于部门管理')--12
insert PermissionInfo values('/dept/adddept',null,'新增部门',2,'归类于部门管理')--13

insert PermissionInfo values('/employeer/employeerlist',null,'员工列表',3,'归类于员工管理')--14
insert PermissionInfo values('/employeer/addemployeer',null,'新增员工',3,'归类于员工管理')--15
insert PermissionInfo values('/employeer/privileges',null,'员工调动',3,'归类于员工管理')--16

insert PermissionInfo values('/post/postlist',null,'岗位列表',4,'归类于岗位管理')--17
insert PermissionInfo values('/Post/AddPost',null,'添加岗位',4,'归类于岗位管理')--18

insert PermissionInfo values('/Payroll/Paylist',null,'查询工资',5,'归类于薪酬管理')--19
insert PermissionInfo values('/Payroll/PayRecordlist',null,'工资发放记录',5,'归类于薪酬管理')--20

insert PermissionInfo values('/email/emailmanage',null,'邮件管理',6,'归类于信息交流管理')--21
insert PermissionInfo values('/email/sendemail',null,'发送邮件',6,'归类于信息交流管理')--22

insert PermissionInfo values('/permission/permissionmanage',null,'权限管理',7,'归类于权限管理')--23
insert PermissionInfo values('/permission/rolemanage',null,'角色管理',7,'归类于权限管理')--24

insert PermissionInfo values('/TimeSheet/TimeManage',null,'考勤管理',8,'归类于考勤管理')--25
insert PermissionInfo values('/TimeSheet/CheckOrSignBack',null,'签到退到',8,'归类于考勤管理')--26
insert PermissionInfo values('/TimeSheet/checkRecord',null,'查看我的签到记录',8,'归类于考勤管理')--27

insert PermissionInfo values('/Calendar/AllCalendars',null,'查看日程',9,'归类于日程管理')--28

go
/*
	注：
	分配权限privileges
	我的日程表My Lists
	日程表Calendar
	部门表Dept
	用户访问权限表UAD
	角色表Role
	职位列表 Joblist
	薪酬列表Pay list
	邮件管理Mail Management
	邮件管理员postmaster
	星期一：Mon.=Monday 
	星期二：Tues.=Tuesday 
	星期三：Wed.=Wednesday 
	星期四：Thur.=Thursday 
	星期五：Fri.=Friday 
	星期六：Sat.=Saturday 
	星期天：Sun.=Sunday 
	发送邮件SMTP
*/
go

----------------------------------------------员工表与权限表的关联表
create table Employee_PermissionInfo
(
ID int primary key identity,
EmployeeId  int,--员工编号 
PermissionID int,--权限编号 
)
go
select*from Employee_PermissionInfo


select * from PermissionInfo a where ID in ( select PermissionID from Employee_PermissionInfo  where EmployeeID=5)
                                   
----------------------------------------------角色表与权限表的关联表
create table RoleInfo_PermissionInfo
(
ID int primary key identity,
RoleId  int,--角色编号 
PermissionID int,--权限编号 
)
/*
赋予‘普通员工’角色
‘个人中心（我的日程,我的信息）’，
‘信息交流管理（邮件管理，发送邮件）’，
‘考勤管理（考勤管理，签到/退到，查看我的签到记录）’，
*/
insert RoleInfo_PermissionInfo values(2,1)
insert RoleInfo_PermissionInfo values(2,10)
insert RoleInfo_PermissionInfo values(2,11)

insert RoleInfo_PermissionInfo values(2,6)
insert RoleInfo_PermissionInfo values(2,21)
insert RoleInfo_PermissionInfo values(2,22)

insert RoleInfo_PermissionInfo values(2,8)
insert RoleInfo_PermissionInfo values(2,25)
insert RoleInfo_PermissionInfo values(2,26)
insert RoleInfo_PermissionInfo values(2,27)

/*
赋予‘员工管理员’角色
‘个人中心（我的日程,我的信息）’，
‘信息交流管理（邮件管理，发送邮件）’，
‘考勤管理（考勤管理，签到/退到，查看我的签到记录）’，
‘薪酬管理（查询工资）’
‘员工管理’(员工列表,新增员工,员工调动)的权限 
*/
insert RoleInfo_PermissionInfo values(3,1)
insert RoleInfo_PermissionInfo values(3,10)
insert RoleInfo_PermissionInfo values(3,11)

insert RoleInfo_PermissionInfo values(3,6)
insert RoleInfo_PermissionInfo values(3,21)
insert RoleInfo_PermissionInfo values(3,22)

insert RoleInfo_PermissionInfo values(3,8)
insert RoleInfo_PermissionInfo values(3,25)
insert RoleInfo_PermissionInfo values(3,26)
insert RoleInfo_PermissionInfo values(3,27)

insert RoleInfo_PermissionInfo values(3,5)
insert RoleInfo_PermissionInfo values(3,19)
insert RoleInfo_PermissionInfo values(3,20)

insert into RoleInfo_PermissionInfo values(3,3)
insert into RoleInfo_PermissionInfo values(3,14)
insert into RoleInfo_PermissionInfo values(3,15)
insert into RoleInfo_PermissionInfo values(3,16)

/*赋予‘部门管理员’角色
‘个人中心（我的日程,我的信息）’，
‘信息交流管理（邮件管理，发送邮件）’，
‘考勤管理（考勤管理，签到/退到，查看我的签到记录）’，
‘部门管理’的权利 
*/
insert RoleInfo_PermissionInfo values(4,1)
insert RoleInfo_PermissionInfo values(4,10)
insert RoleInfo_PermissionInfo values(4,11)

insert RoleInfo_PermissionInfo values(4,6)
insert RoleInfo_PermissionInfo values(4,21)
insert RoleInfo_PermissionInfo values(4,22)

insert RoleInfo_PermissionInfo values(4,8)
insert RoleInfo_PermissionInfo values(4,25)
insert RoleInfo_PermissionInfo values(4,26)
insert RoleInfo_PermissionInfo values(4,27)

insert into RoleInfo_PermissionInfo values(4,2)
insert into RoleInfo_PermissionInfo values(4,12)
insert into RoleInfo_PermissionInfo values(4,13)

/*赋予‘岗位管理员’角色
‘个人中心（我的日程,我的信息）’，
‘信息交流管理（邮件管理，发送邮件）’，
‘考勤管理（考勤管理，签到/退到，查看我的签到记录）’，
‘岗位管理’的权利 
*/
insert RoleInfo_PermissionInfo values(5,1)
insert RoleInfo_PermissionInfo values(5,10)
insert RoleInfo_PermissionInfo values(5,11)

insert RoleInfo_PermissionInfo values(5,6)
insert RoleInfo_PermissionInfo values(5,21)
insert RoleInfo_PermissionInfo values(5,22)

insert RoleInfo_PermissionInfo values(5,8)
insert RoleInfo_PermissionInfo values(5,25)
insert RoleInfo_PermissionInfo values(5,26)
insert RoleInfo_PermissionInfo values(5,27)

insert into RoleInfo_PermissionInfo values(5,4)
insert into RoleInfo_PermissionInfo values(5,17)
insert into RoleInfo_PermissionInfo values(5,18)
go
/*
查询
*/
/*
--部门表
select *from Dept 
--岗位表
select *from PostInfo
--角色表
select *from RoleInfo
--员工角色表
select *from Employee_RoleInfo
--日程表
select *from Calendar
--员工表
select *from EmployeeInfo 
--工资表
select *from Payroll
--工资发放记录表
select *from SalaryRecord
--员工表与权限表的关联表
select *from Employee_PermissionInfo
--角色表与权限表的关联表
select *from RoleInfo_PermissionInfo
--权限表
select *from PermissionInfo
--考勤表
select *from TimeSheet
--邮件表
select *from SendEmailMessages
select *from GetEmailMessages

select *from Employee_RoleInfo where Employeeid=2
*/
/*
测试
*/

/*
select COUNT(1) from EmployeeInfo
select *from EmployeeInfo e where e.id=12
select *from EmployeeInfo e where e.DeptCode like '%s%'
select *from EmployeeInfo a join PostInfo b on a.PostID =b.ID left join Dept c on b.DeptID=c.ID 
delete TimeSheet
在权限表中查询用户没有的权限
select * from PermissionInfo a where not exists (select b.PermissionID from Employee_PermissionInfo b where b.EmployeeId=2 and b.PermissionID=a.ID)                                                                                           
在权限表中查询角色没有的权限
select * from PermissionInfo a where not exists (select b.PermissionID from RoleInfo_PermissionInfo b where b.RoleId=2 and b.PermissionID=a.ID)                                                                                           
select *from Employee_RoleInfo a where a.EmployeeId=3
select a.PermissionID from Employee_PermissionInfo a where a.EmployeeId=3 order by a.PermissionID
select a.PermissionID from RoleInfo_PermissionInfo a where a.RoleId=2 order by a.PermissionID
--查询用户所有权限(角色权限+个人权限(去掉重复的))
*/----except UNION INTERSECT
--select a.PermissionID from Employee_PermissionInfo a where a.EmployeeID=2 UNION
--(select a.PermissionID from RoleInfo_PermissionInfo a join Employee_RoleInfo b on a.RoleId=b.RoleID
--where b.EmployeeID=2 and b.RoleID=3)
--select COUNT(*)from PermissionInfo
--查询用户没有的权限(角色权限+个人权限)
--select * from PermissionInfo a where not exists (
--select * from(
--select a.PermissionID from Employee_PermissionInfo a where a.EmployeeID=2 UNION
--(select a.PermissionID from RoleInfo_PermissionInfo a join Employee_RoleInfo b on a.RoleId=b.RoleID
--where b.EmployeeID=2 and b.RoleID=3)
--)tb where a.ID=tb.PermissionID
--)                                                                                           

--insert GetEmailMessages values(2,	1	,'请假',	'dfg',	'2016/12/20 20:17:26',	0,	0)
--工作任务	请假	闲聊

--模糊查询
--select *from EmployeeInfo e join Dept d on e.DeptID=d.ID where d.DeptName like '%后%'
--分页
--select *from (
--select ROW_NUMBER()over (order by id)as 'rownumber',*from EmployeeInfo
--)temp where temp.rownumber between {0} and {1}
--分页+模糊查询（两表联查）
--select *from (
--select ROW_NUMBER()over (order by e.id)as 'rownumber',e.*from EmployeeInfo e join Dept d on e.DeptID=d.ID where d.DeptName like '%%'
--)temp where temp.rownumber between 1 and 5

--分页+模糊查询
----select *from (
--select ROW_NUMBER()over (order by e.id)as 'rownumber',e.*from EmployeeInfo e where e.EmployeeName like '%%'
--)temp where temp.rownumber between 1 and 5
--关键词 distinct用于返回唯一不同的值 select distinct name, id from A
--select*from SalaryRecord where YEAR(PayTime)='2017' and MONTH(PayTime)='1' and DAY(PayTime)='4'  

/*
添加页面权限时，必须添加完整个菜单栏
*/

--select *from (
--select ROW_NUMBER()over (order by e.id)as 'rownumber',e.* from EmployeeInfo e where e.LoginName like '%%'
--)temp where temp.rownumber between 1 and 5

