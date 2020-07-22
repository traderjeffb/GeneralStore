﻿using GeneralStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GeneralStore.Controllers
{
    public class ProductController : ApiController
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        [HttpPost]
        public async Task<IHttpActionResult> Post(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Product> products = await _context.Products.ToListAsync();
            return Ok(products);
        }
        [HttpGet]
        public async Task<IHttpActionResult> GetById([FromUri] int id)
        {
            Product product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        //Update
        [HttpPut]
        public async Task<IHttpActionResult> Put([FromUri] int id, [FromBody] Product model)
        {
            if (ModelState.IsValid)
            {

                if (id != model.Id)
                {
                    return BadRequest(" product id mismatch");
                }

                _context.Entry(model).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest(ModelState);
        }
        //Delete
        [HttpDelete]
        public async Task<IHttpActionResult> Delete([FromUri] int id)
        {
            Product product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Products.Remove(product);

            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
