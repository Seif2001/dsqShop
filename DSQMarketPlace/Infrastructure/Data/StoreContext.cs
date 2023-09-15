using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        //public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Merchant> Merchants { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Bid>()
            .HasOne(b => b.Voucher)
            .WithMany(v => v.VoucherBids)
            .HasForeignKey(b => b.VoucherId);

            //modelBuilder.Entity<User>()
            //.HasMany(e => e.CurrentVouchers)
            //.WithOne(e => e.Owner)
            //.HasForeignKey(e => e.UserId)
            //.IsRequired();

            modelBuilder.Entity<Voucher>()
                .HasOne(x => x.Owner)
                .WithMany(x => x.CurrentVouchers)
                .HasForeignKey(x => x.UserId)
                .IsRequired();

            modelBuilder.Entity<Merchant>()
            .HasMany(e => e.Vouchers)
            .WithOne(e => e.Merchant)
            .HasForeignKey(e => e.MerchantId)
            .IsRequired();

            //modelBuilder.Entity<User>()
            //.HasMany(e => e.VoucherTrans)
            //.WithMany(e => e.UserTrans)
            //.UsingEntity<Transaction>();

        }

    }
}
