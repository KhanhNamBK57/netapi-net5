using MyWebApiApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApiApp.Services
{
    public interface IHangHoaRepository
    {
        List<HangHoaModel> Search(string search);
    }
}
