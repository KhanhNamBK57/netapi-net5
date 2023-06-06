using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebApiApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApiApp.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class HangHoaController : ControllerBase
	{

		private readonly MyDbContext _context;
		public HangHoaController(MyDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult GetAll()
		{
			var dsHangHoa = _context.HangHoas.ToList();
			return Ok(dsHangHoa);
		}
		[HttpGet("{id}")]
		public IActionResult GetById(string id)
		{
			try
			{
				var hangHoa = _context.HangHoas.SingleOrDefault(hh => hh.IdHangHoa == Guid.Parse(id));
				if (hangHoa == null)
				{
					return NotFound();
				}
				return Ok(hangHoa);
			}
			catch
			{
				return BadRequest();
			}
		}
		[HttpPost]
		public IActionResult Create(HangHoa hangHoa)
		{
			var hanghoa = new HangHoa
			{
				IdHangHoa = Guid.NewGuid(),
				TenHangHoa = hangHoa.TenHangHoa,
				DonGia = hangHoa.DonGia
			};
			_context.Add(hanghoa);
			_context.SaveChanges();

			return Ok(new
			{
				Success = true,
				Data = hanghoa
			});
		}

		[HttpPut("{id}")]
		public IActionResult Edit(string id, HangHoa hangHoaEdit)
		{
			try
			{
				if (Guid.Parse(id) != hangHoaEdit.IdHangHoa)
				{
					return BadRequest();
				}
				var hangHoa = _context.HangHoas.SingleOrDefault(hh => hh.IdHangHoa == Guid.Parse(id));
				if (hangHoa != null)
				{
					// Update
					hangHoa.TenHangHoa = hangHoaEdit.TenHangHoa;
					hangHoa.DonGia = hangHoaEdit.DonGia;
					_context.SaveChanges();
					return NoContent();	
				}else
				{
					return NotFound();
				}
			}
			catch
			{
				return BadRequest();
			}
		}
		[HttpDelete("{id}")]
		public IActionResult Remove(string id)
		{
			try
			{

				var hangHoa = _context.HangHoas.SingleOrDefault(hh => hh.IdHangHoa == Guid.Parse(id));
				if (hangHoa != null)
				{
					// Update
					_context.Remove(hangHoa);
					_context.SaveChanges();
					return NoContent();
				}
				else
				{
					return BadRequest();
				}
				
			}
			catch
			{
				return BadRequest();

			}
		}
	}
}
