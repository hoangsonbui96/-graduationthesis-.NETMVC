using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MyStore.Models;

namespace MyStore.Data
{
    public class MyStoreContext : DbContext
    {
        public Controller Controller;
        public MyStoreContext(DbContextOptions<MyStoreContext> options)
            : base(options)
        {
        }

        public override EntityEntry<TEntity> Add<TEntity>(TEntity entity)
        {
            if (entity != null && entity.GetType().BaseType == typeof(BaseDataModel))
            {
                entity.GetType().GetMethod(nameof(BaseDataModel.Initial)).Invoke(entity, new object[] { GetAuthorizedUser() });
            }
            return base.Add(entity);
        }

        public override void AddRange(IEnumerable<object> entities)
        {
            foreach (var entity in entities)
            {
                if (entity != null && entity.GetType().BaseType == typeof(BaseDataModel))
                {
                    entity.GetType().GetMethod(nameof(BaseDataModel.Initial)).Invoke(entity, new object[] { GetAuthorizedUser() });
                }
            }
            base.AddRange(entities);
        }

        public override EntityEntry<TEntity> Update<TEntity>(TEntity entity)
        {
            if (entity != null && entity.GetType().BaseType == typeof(BaseDataModel))
            {
                entity.GetType().GetMethod(nameof(BaseDataModel.Update)).Invoke(entity, new object[] { GetAuthorizedUser() });
            }
            return base.Update(entity);
        }

        public override void UpdateRange(IEnumerable<object> entities)
        {
            foreach (var entity in entities)
            {
                if (entity != null && entity.GetType().BaseType == typeof(BaseDataModel))
                {
                    entity.GetType().GetMethod(nameof(BaseDataModel.Update)).Invoke(entity, new object[] { GetAuthorizedUser() });
                }
            }
            base.UpdateRange(entities);
        }

        public string GetAuthorizedUser()
        {

            return "system";

            //if (_controller != null && _controller.User.Identity.IsAuthenticated)
            //{
            //	return _controller.User.Identity.Name;
            //}
            //return string.Empty;
        }


        public DbSet<MyStore.Models.Product> Product { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderDetail>()
                .HasKey(i => new { i.OrderId, i.ProductId });

            modelBuilder.Entity<RolePermision>()
                .HasKey(i => new { i.RoleCode, i.FunctionRole });


        }

        public DbSet<MyStore.Models.OrderDetail> OrderDetail { get; set; }

        public DbSet<MyStore.Models.Account> Account { get; set; }

        public DbSet<MyStore.Models.Customer> Customer { get; set; }

        public DbSet<MyStore.Models.RoleMaster> RoleMaster { get; set; }

        public DbSet<MyStore.Models.RolePermision> RolePermision { get; set; }



    }
}
