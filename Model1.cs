namespace Project
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<tblBanner> tblBanners { get; set; }
        public virtual DbSet<tblBooking> tblBookings { get; set; }
        public virtual DbSet<tblCategory> tblCategories { get; set; }
        public virtual DbSet<tblComment> tblComments { get; set; }
        public virtual DbSet<tblItem> tblItems { get; set; }
        public virtual DbSet<tblRole> tblRoles { get; set; }
        public virtual DbSet<tblUser> tblUsers { get; set; }
        public virtual DbSet<tblUserRole> tblUserRoles { get; set; }
        public virtual DbSet<tblTestimony> tblTestimonies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblCategory>()
                .HasMany(e => e.tblItems)
                .WithOptional(e => e.tblCategory)
                .WillCascadeOnDelete();

            modelBuilder.Entity<tblComment>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<tblItem>()
                .HasMany(e => e.tblComments)
                .WithOptional(e => e.tblItem)
                .WillCascadeOnDelete();

            modelBuilder.Entity<tblRole>()
                .HasMany(e => e.tblUserRoles)
                .WithOptional(e => e.tblRole)
                .WillCascadeOnDelete();

            modelBuilder.Entity<tblUser>()
                .HasMany(e => e.tblUserRoles)
                .WithOptional(e => e.tblUser)
                .WillCascadeOnDelete();
        }
    }
}
