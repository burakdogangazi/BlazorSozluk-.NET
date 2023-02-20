using BlazorSozluk.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Infrastructure.Persistence.Context
{
    public class BlazorSozlukContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "dbo";

        public BlazorSozlukContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Entry> Entries { get; set; }

        public DbSet<EntryComment> EntryComments { get; set; }  

        public DbSet<EntryFavorite> EntryFavorites { get; set; }

        public DbSet<EntryCommentVote> EntryCommentVote { get; set; }

        public DbSet<EntryCommentFavorite> EntryCommentFavorites { get; set; }

        public DbSet<EmailConfirmation> EmailConfirmations { get; set; }

        public DbSet<EntryVote> EntryVotes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            // Oluşturacağımız entity configurasyonlarının efcore tarafından algılanması için assembly yazıyoruz.
            
        }

        public override int SaveChanges()
        {
            OnBeforeSave();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSave();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            OnBeforeSave();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            OnBeforeSave();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void OnBeforeSave()
        {
            var addedEntities = ChangeTracker.Entries()
                                .Where(i => i.State == EntityState.Added)
                                .Select(i => (BaseEntity)i.Entity);

            PrepareAddedEntites(addedEntities);
        }

        private void PrepareAddedEntites(IEnumerable<BaseEntity> entities) 
        {

            // veri tabanı kayıt işlemleri sırasında kayıt etmeden önce tüm tarihleri şuan olarak set eder
            foreach (var entity in entities)
            {
                if (entity.CreateDate == DateTime.MinValue)
                {
                    entity.CreateDate = DateTime.Now;
                }
            }
        }
    }
}
