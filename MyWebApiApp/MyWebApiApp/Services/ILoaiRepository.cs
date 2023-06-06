using MyWebApiApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApiApp.Services
{
    public interface ILoaiRepository
    {
        List<LoaiVM> GetAll();
        LoaiVM GetById(string id);
        LoaiVM Add(LoaiModel loai);
        void Update(LoaiVM loai);
        void Delete(string id);
    }
}
