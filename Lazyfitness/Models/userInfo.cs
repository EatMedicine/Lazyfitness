//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Lazyfitness.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    public partial class userInfo
    {
        public int userId { get; set; }
        [Required]
        public string userName { get; set; }
        public Nullable<int> userAge { get; set; }
        public Nullable<int> userSex { get; set; }
        public string userTel { get; set; }
        public Nullable<int> userStatus { get; set; }
        public Nullable<int> userAccount { get; set; }
    
        public virtual userSecurity userSecurity { get; set; }
    }
}
