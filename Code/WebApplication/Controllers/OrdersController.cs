using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class OrdersController : Controller
    {
        private readonly OrderDataContext _context;

        public OrdersController(OrderDataContext context)
        {
            _context = context;

        }

		public async Task<IActionResult> _CreateNew(Order order)
		{
			_context.Add(order);
			await _context.SaveChangesAsync();
			return RedirectToAction("Index");
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNew([Bind("Description,Email")]Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(order);
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> _EditOrderDetail(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var orderDetail = await _context.Details.SingleOrDefaultAsync(c => c.ID == id);
            if (await TryUpdateModelAsync(orderDetail))
			{
				try
				{
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateException /* ex */)
				{
					ModelState.AddModelError("ERR", "Unable to save changes. Contact your administrator");
				}
				return RedirectToAction("Index");
			}

			return View(orderDetail);
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditOrderDetail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.Details.SingleOrDefaultAsync(c => c.ID == id);
            if (await TryUpdateModelAsync(orderDetail, "", x => x.Ammount))
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    ModelState.AddModelError("ERR", "Unable to save changes. Contact your administrator");
                }
                return RedirectToAction("Index");
            }
            
            return View(orderDetail);
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            await _context.Database.EnsureCreatedAsync();
            return View(await _context.Orders.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .SingleOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }


        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.SingleOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Email,Description,TotalPrice")] Order order)
        {
            if (id != order.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .SingleOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(m => m.ID == id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.ID == id);
        }
    }
}
