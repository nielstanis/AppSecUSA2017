using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Data
{
    public class OrderDataContext : DbContext
    {

        public OrderDataContext(DbContextOptions<OrderDataContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Order>().ToTable("Order");
            builder.Entity<OrderDetail>().ToTable("OrderDetail");
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> Details { get; set; }
    }
}
