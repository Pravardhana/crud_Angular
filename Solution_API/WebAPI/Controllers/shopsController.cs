using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class shopsController : ApiController
    {
        private Db_Model db = new Db_Model();

        // GET: api/shops
        public IQueryable<shop> Getshops()
        {
            return db.shops;
        }

        // GET: api/shops/5
        [ResponseType(typeof(shop))]
        public IHttpActionResult Getshop(int id)
        {
            shop shop = db.shops.Find(id);
            if (shop == null)
            {
                return NotFound();
            }

            return Ok(shop);
        }

        // PUT: api/shops/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putshop(int id, shop shop)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
                
            if (id != shop.Id)
            {
                return BadRequest();
            }

            db.Entry(shop).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!shopExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/shops
        [ResponseType(typeof(shop))]
        public IHttpActionResult Postshop(shop shop)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.shops.Add(shop);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = shop.Id }, shop);
        }

        // DELETE: api/shops/5
        [ResponseType(typeof(shop))]
        public IHttpActionResult Deleteshop(int id)
        {
            shop shop = db.shops.Find(id);
            if (shop == null)
            {
                return NotFound();
            }

            db.shops.Remove(shop);
            db.SaveChanges();

            return Ok(shop);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool shopExists(int id)
        {
            return db.shops.Count(e => e.Id == id) > 0;
        }
    }
}