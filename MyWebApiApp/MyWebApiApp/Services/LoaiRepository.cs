using MyWebApiApp.Data;
using MyWebApiApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApiApp.Services
{
    public class LoaiRepository : ILoaiRepository
    {
        private readonly MyDbContext _context;

        public LoaiRepository(MyDbContext context)
        {
            _context = context;
        }
        public LoaiVM Add(LoaiModel loai)
        {
            var _loai = new Loai
            {
                IdLoai = Guid.NewGuid(),
                TenLoai = loai.TenLoai
            };
            _context.Add(_loai);
            _context.SaveChanges();

            return new LoaiVM
            {
                IdLoai = _loai.IdLoai,
                TenLoai = _loai.TenLoai
            };
        }

        public void Delete(string id)
        {
            var loai = _context.Loais.SingleOrDefault(lo => lo.IdLoai == Guid.Parse(id));
            if (loai != null)
            {
                _context.Remove(loai);
                _context.SaveChanges();
                    
            }
        }

        public List<LoaiVM> GetAll()
        {
            var loais = _context.Loais.Select(lo => new LoaiVM
            {
                IdLoai = lo.IdLoai,
                TenLoai = lo.TenLoai
            });
            return loais.ToList();
        }

        public LoaiVM GetById(string id)
        {
            var loai = _context.Loais.SingleOrDefault(lo => lo.IdLoai == Guid.Parse(id));
            if(loai != null)
            {
                return new LoaiVM
                {
                    IdLoai = loai.IdLoai,
                    TenLoai = loai.TenLoai
                };
            }
            return null;
        }

        public void Update(LoaiVM loai)
        {
            var _loai = _context.Loais.SingleOrDefault(lo => lo.IdLoai == loai.IdLoai);
            loai.TenLoai = _loai.TenLoai;
            _context.SaveChanges();
        }
    }
}
