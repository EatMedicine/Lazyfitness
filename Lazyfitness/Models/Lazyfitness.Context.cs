﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class LazyfitnessEntities : DbContext
    {
        public LazyfitnessEntities()
            : base("name=LazyfitnessEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<backManager> backManager { get; set; }
        public virtual DbSet<postArea> postArea { get; set; }
        public virtual DbSet<postInfo> postInfo { get; set; }
        public virtual DbSet<postReply> postReply { get; set; }
        public virtual DbSet<quesAnswInfo> quesAnswInfo { get; set; }
        public virtual DbSet<quesAnswReply> quesAnswReply { get; set; }
        public virtual DbSet<quesArea> quesArea { get; set; }
        public virtual DbSet<recharge> recharge { get; set; }
        public virtual DbSet<resourceArea> resourceArea { get; set; }
        public virtual DbSet<resourceInfo> resourceInfo { get; set; }
        public virtual DbSet<serverShowInfo> serverShowInfo { get; set; }
        public virtual DbSet<userInfo> userInfo { get; set; }
        public virtual DbSet<userSecurity> userSecurity { get; set; }
        public virtual DbSet<userStatusName> userStatusName { get; set; }
    }
}
