using Microsoft.EntityFrameworkCore;
using MyWebApiApp.Data;
using MyWebApiApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApiApp.Services
{
    public class HangHoaRepository : IHangHoaRepository
    {
        private readonly MyDbContext _context;
        public static int PAGE_SIZE { get; set; } = 3;

        public HangHoaRepository(MyDbContext context)
        {
            _context = context;
        }
        public List<HangHoaModel> Search(string search, int page = 1)
        {
            var allProducts = _context.HangHoas.Include(hh=>hh.Loai).AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                allProducts= allProducts.Where(hh => hh.TenHangHoa.Contains(search));
            }
            //var result = allProducts.Select(hh => new HangHoaModel
            //    {
            //        IdHangHoa = hh.IdHangHoa,
            //        TenHangHoa = hh.TenHangHoa,
            //        DonGia = hh.DonGia,
            //        TenLoai = hh.Loai.TenLoai
            //    });
            //    return result.ToList();   

            var result = PaginatedList<MyWebApiApp.Data.HangHoa>.Create(allProducts, page, PAGE_SIZE);
            return result.Select(hh => new HangHoaModel
            {
                       IdHangHoa = hh.IdHangHoa,
                       TenHangHoa = hh.TenHangHoa,
                       DonGia = hh.DonGia,
                       TenLoai = hh.Loai.TenLoai
            }).ToList();
        }
    }
}
