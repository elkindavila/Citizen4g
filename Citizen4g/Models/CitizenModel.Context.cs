﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Citizen4g.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class db_citizen4Entities2 : DbContext
    {
        public db_citizen4Entities2()
            : base("name=db_citizen4Entities2")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<candidate> candidates { get; set; }
        public virtual DbSet<citizen4> citizen4 { get; set; }
        public virtual DbSet<CivilStatu> CivilStatus { get; set; }
        public virtual DbSet<EconomicActivity> EconomicActivities { get; set; }
        public virtual DbSet<EducationLevel> EducationLevels { get; set; }
        public virtual DbSet<focus> foci { get; set; }
        public virtual DbSet<focus_citizen4> focus_citizen4 { get; set; }
        public virtual DbSet<FocusNewAdmin> FocusNewAdmins { get; set; }
        public virtual DbSet<focusNewAdmin_candidate> focusNewAdmin_candidate { get; set; }
        public virtual DbSet<messagetype> messagetypes { get; set; }
        public virtual DbSet<msg_citizen4_candidates> msg_citizen4_candidates { get; set; }
        public virtual DbSet<need> needs { get; set; }
        public virtual DbSet<needs_citizen4> needs_citizen4 { get; set; }
        public virtual DbSet<profile> profiles { get; set; }
        public virtual DbSet<sector> sectors { get; set; }
        public virtual DbSet<town> towns { get; set; }
        public virtual DbSet<TypeTrasnport> TypeTrasnports { get; set; }
        public virtual DbSet<user> users { get; set; }
        public virtual DbSet<users_profiles> users_profiles { get; set; }
    }
}
