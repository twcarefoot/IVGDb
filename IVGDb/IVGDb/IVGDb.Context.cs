﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IVGDb
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ivgdb_Entities : DbContext
    {
        public ivgdb_Entities()
            : base("name=ivgdb_Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<VideoGame> VideoGames { get; set; }
        public virtual DbSet<Console> Consoles { get; set; }
        public virtual DbSet<GamesFor> GamesFors { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
    }
}
