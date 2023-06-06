using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApiApp.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options) { }

        #region DBSet
        public DbSet<HangHoa> HangHoas { get; set; }
        public DbSet<Loai> Loais { get; set; }
        public DbSet<DonHang> DonHangs { get; set; }
        public DbSet<DonHangChiTiet> DonHangChiTiets { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DonHang>(e =>
            {
                e.ToTable("DonHang");
                e.HasKey(dh => dh.IdDonHang);
            });

            modelBuilder.Entity<DonHangChiTiet>(entity =>
            {
                entity.ToTable("ChiTietDonHang");
                entity.HasKey(e => new { e.IdDonHang, e.IdHangHoa });
                entity.HasOne(e => e.DonHang)
                      .WithMany(e => e.DonHangChiTiets)
                      .HasForeignKey(e => e.IdDonHang)
                      .HasConstraintName("FK_DonHangCT_DonHang");
                entity.HasOne(e=>e.HangHoa)
                      .WithMany(e=>e.DonHangChiTiets)
                      .HasForeignKey(e => e.IdHangHoa)
                      .HasConstraintName("FK_DonHangCT_HangHoa");
            });
        }
    }
}
