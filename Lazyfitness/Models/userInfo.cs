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
        [Required(ErrorMessage = "用户名不能为空"), StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessage = "用户名不能超过50位")]
        public string userName { get; set; }     
        [Display(Name ="年龄")]
        public Nullable<int> userAge { get; set; }
        [Required(ErrorMessage ="必须选择一个性别")]
        public Nullable<int> userSex { get; set; }
        [StringLength(maximumLength:11, MinimumLength =11, ErrorMessage ="请输入正确的11位电话号码")]
        public string userTel { get; set; }
        public Nullable<int> userStatus { get; set; }
        public Nullable<int> userAccount { get; set; }
    
        public virtual userSecurity userSecurity { get; set; }
    }
}
