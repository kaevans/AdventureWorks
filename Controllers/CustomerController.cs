using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdventureWorks.Models;

namespace AdventureWorks.Controllers
{
    public class CustomerController : Controller
    {
        private readonly AdventureWorks2019Context _context;

        public CustomerController(AdventureWorks2019Context context)
        {
            _context = context;
        }

        // GET: Customer
        public async Task<IActionResult> Index()
        {
            var adventureWorks2019Context = _context.Customers.Include(c => c.Person).Include(c => c.Store).Include(c => c.Territory);
            return View(await adventureWorks2019Context.ToListAsync());
        }

        // GET: Customer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.Person)
                .Include(c => c.Store)
                .Include(c => c.Territory)
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customer/Create
        public IActionResult Create()
        {
            ViewData["PersonId"] = new SelectList(_context.People, "BusinessEntityId", "FirstName");
            ViewData["StoreId"] = new SelectList(_context.Stores, "BusinessEntityId", "Name");
            ViewData["TerritoryId"] = new SelectList(_context.SalesTerritories, "TerritoryId", "CountryRegionCode");
            return View();
        }

        // POST: Customer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,PersonId,StoreId,TerritoryId,AccountNumber,Rowguid,ModifiedDate")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonId"] = new SelectList(_context.People, "BusinessEntityId", "FirstName", customer.PersonId);
            ViewData["StoreId"] = new SelectList(_context.Stores, "BusinessEntityId", "Name", customer.StoreId);
            ViewData["TerritoryId"] = new SelectList(_context.SalesTerritories, "TerritoryId", "CountryRegionCode", customer.TerritoryId);
            return View(customer);
        }

        // GET: Customer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            ViewData["PersonId"] = new SelectList(_context.People, "BusinessEntityId", "FirstName", customer.PersonId);
            ViewData["StoreId"] = new SelectList(_context.Stores, "BusinessEntityId", "Name", customer.StoreId);
            ViewData["TerritoryId"] = new SelectList(_context.SalesTerritories, "TerritoryId", "CountryRegionCode", customer.TerritoryId);
            return View(customer);
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,PersonId,StoreId,TerritoryId,AccountNumber,Rowguid,ModifiedDate")] Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonId"] = new SelectList(_context.People, "BusinessEntityId", "FirstName", customer.PersonId);
            ViewData["StoreId"] = new SelectList(_context.Stores, "BusinessEntityId", "Name", customer.StoreId);
            ViewData["TerritoryId"] = new SelectList(_context.SalesTerritories, "TerritoryId", "CountryRegionCode", customer.TerritoryId);
            return View(customer);
        }

        // GET: Customer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.Person)
                .Include(c => c.Store)
                .Include(c => c.Territory)
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }
    }
}
