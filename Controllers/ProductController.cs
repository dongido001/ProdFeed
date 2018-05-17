
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProdFeed.Helpers;
using ProdFeed.Models;
namespace ProdFeed.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProdFeedContext _context;
        public ProductController(ProdFeedContext context) 
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // get all products..
            return View(await _context.Products.ToListAsync());
        }


        [HttpPost]
        public async Task<IActionResult> Create([Bind("ID,Name,Description,Status,Price")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();


                var data = new {
                    message = System.String.Format("New product with ID of #{0} added", product.ID)
                };

                await Channel.Trigger(data, "feed", "new_feed");

            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var product = new Product { ID = id };
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();


            var data = new {
                message = System.String.Format("Product with ID of #{0} deleted", product.ID)
            };
            await Channel.Trigger(data, "feed", "new_feed");

            return RedirectToAction(nameof(Index));
        }

       [HttpGet]
        public async Task<IActionResult> ChangeStatus(int id)
        {
            var product = await _context.Products.SingleOrDefaultAsync(m => m.ID == id);
            product.Status = !product.Status;
            await _context.SaveChangesAsync();

            var status = product.Status ? "In stock" : "Out of Stock";

            var data = new {
                message = System.String.Format("Status of product with ID #{0} changed to '{1}'", product.ID, status)
            };
          
            await Channel.Trigger(data, "feed", "new_feed");
            
            return RedirectToAction(nameof(Index));
        }

    }
}