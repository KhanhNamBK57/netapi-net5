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

        public HangHoaRepository(MyDbContext context)
        {
            _context = context;
        }
        public List<HangHoaModel> Search(string search)
        {
            var allProducts = _context.HangHoas.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                allProducts= allProducts.Where(hh => hh.TenHangHoa.Contains(search));
            }
            var result = allProducts.Select(hh => new HangHoaModel
                {
                    IdHangHoa = hh.IdHangHoa,
                    TenHangHoa = hh.TenHangHoa,
                    DonGia = hh.DonGia,
                    TenLoai = hh.Loai.TenLoai
                });
                return result.ToList();   
        }
    }
}
