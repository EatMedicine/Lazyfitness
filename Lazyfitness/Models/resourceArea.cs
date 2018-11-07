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
    public partial class resourceArea
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public resourceArea()
        {
            this.resourceInfo = new HashSet<resourceInfo>();
        }
    
        public int areaId { get; set; }
        [Required(ErrorMessage ="分区名不能为空"), StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessage = "分区名过长")]
        public string areaName { get; set; }
        [StringLength(maximumLength: 50, MinimumLength = 0, ErrorMessage = "简介不超过50个字")]
        public string areaBrief { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<resourceInfo> resourceInfo { get; set; }
    }
}
