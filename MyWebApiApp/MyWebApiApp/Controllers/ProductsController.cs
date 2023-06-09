using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebApiApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IHangHoaRepository _hangHoaRepository;

        public ProductsController(IHangHoaRepository hangHoaRepository)
        {
            _hangHoaRepository = hangHoaRepository;
        }

        [HttpGet]
        public IActionResult SerchProducts(string search, int page)
        {
            try
            {
                var result = _hangHoaRepository.Search(search, page);
                return Ok(result);
            }
            catch
            {

                return BadRequest("We can't get the product.");
            }
        }
    }
}
