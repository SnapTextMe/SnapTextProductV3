using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

using Microsoft.AspNet.Identity.EntityFramework;

using SnapTextWeb.DAL.Entities;
using SnapTextWeb.Models;

namespace SnapTextWeb.DAL
{
    public class SnapTextDataContext : DbContext
    {
        // Your context has been configured to use a 'SnapTextMe' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'SnapTextMeDbModel.DAL.Model.SnapTextMe' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'SnapTextMe' 
        // connection string in the application configuration file.
        public SnapTextDataContext() : base("name=DefaultConnection") { }

        //Database.SetInitializer<SnapTextDataContext>(new CreateDatabaseIfNotExists<SnapTextDataContext>());
        //Database.SetInitializer<SnapTextDataContext>(new DropCreateDatabaseIfModelChanges<SnapTextDataContext>());
        //Database.SetInitializer<SnapTextDataContext>(new DropCreateDatabaseAlways<SnapTextDataContext>());
        //Database.SetInitializer<SnapTextDataContext>(new DbInitializer());

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Subscription> Subscriptions { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        //public virtual DbSet<ApplicationUser> Users { get; set; }
        //public virtual DbSet<User> Users { get; set; }

        public override int SaveChanges()
        {
            ObjectContext context = ((IObjectContextAdapter)this).ObjectContext;
             
            // Find all Entities that are Added/Modified that inherit from my EntityBase.
            IEnumerable<ObjectStateEntry> objectStateEntries =
                from e in context.ObjectStateManager.GetObjectStateEntries(EntityState.Added | EntityState.Modified)
                where
                    e.IsRelationship == false &&
                    e.Entity != null &&
                    typeof(EntityBase).IsAssignableFrom(e.Entity.GetType())
                select e;

            var auditUserId = Extensions.Current.UserId;
            var currentDate = DateTime.Now;

            foreach (var entry in objectStateEntries)
            {
                var entityBase = entry.Entity as EntityBase;

                switch (entry.State)
                {
                    case EntityState.Added:
                        entityBase.CreatedDate = currentDate;
                        entityBase.CreatedByUserId = auditUserId;
                        break;
                    case EntityState.Modified:
                        entityBase.LastModifiedDate = currentDate;
                        entityBase.LastModifiedByUserId = auditUserId;
                        break;
                    case EntityState.Deleted:
                        entityBase.DeletedDate = currentDate;
                        entityBase.DeletedByUserId = auditUserId;
                        break;
                    default:
                        break;
                }
            }

            return base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Create many-to-many Account/Subscription relationship.
            modelBuilder.Entity<Account>().HasMany<Subscription>(a => a.Subscriptions).WithMany(s => s.Accounts).Map(c =>
            {
                c.MapLeftKey("AccountId");
                c.MapRightKey("SubscriptionId");
                c.ToTable("AccountSubscription");
            });

            //modelBuilder.Entity<User>()
            //    .HasRequired(u => u.AspNetUser)
            //    .WithOptional()
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            //modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            //modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
        }
    }
}