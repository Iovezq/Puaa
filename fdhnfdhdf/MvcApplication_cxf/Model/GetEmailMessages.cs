//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class GetEmailMessages
    {
        public int ID { get; set; }
        public Nullable<int> SenderID { get; set; }
        public Nullable<int> HaverID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string SendTime { get; set; }
        public Nullable<int> IsRead { get; set; }
        public Nullable<int> IsDel { get; set; }
    }
}
