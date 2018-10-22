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
    using System.ComponentModel.DataAnnotations;

    public partial class userSecurity
    {
        public int userId { get; set; }
        [Required, StringLength(maximumLength:50, MinimumLength =3, ErrorMessage ="用户名必须大于三位")]
        public string loginId { get; set; }
        [Required, StringLength(maximumLength: 50, MinimumLength = 6, ErrorMessage = "密码必须大于六位")]
        public string userPwd { get; set; }
    
        public virtual userInfo userInfo { get; set; }
    }
}
