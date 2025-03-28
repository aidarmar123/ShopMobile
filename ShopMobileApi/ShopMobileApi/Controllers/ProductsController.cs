using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using Swashbuckle.Swagger.Annotations;
using System.Web.Http;
using System.Web.Http.Description;
using ShopMobileApi.Models;

namespace ShopMobileApi.Controllers
{
    public class ProductsController : ApiController
    {
        private ShopMobileEntities db = new ShopMobileEntities();

        // GET: api/Products
        [SwaggerResponse(HttpStatusCode.OK, Description ="Возращает все Product")]
        public IQueryable<Product> GetProduct()
        {
            return db.Product;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Products/5
        [ResponseType(typeof(Product))]
        [SwaggerResponse(HttpStatusCode.OK, Description ="Возращает Product по Id")]
[SwaggerResponse(HttpStatusCode.NotFound, Description = "Возращает когда Product по указоному Id не найден")]
        public IHttpActionResult GetProduct(int id)
        {
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/Products/5
        [ResponseType(typeof(void))]
         [SwaggerResponse(HttpStatusCode.NoContent, Description = "Возращает когда  Product успешно изменен")]
 [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Возращает когда  Product не валиден")]
 [SwaggerResponse(HttpStatusCode.NotFound, Description = "Возращает когда  Product по указоному Id не найден")]
        public IHttpActionResult PutProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.Id)
            {
                return BadRequest();
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/Products
        [ResponseType(typeof(Product))]
        [SwaggerResponse(HttpStatusCode.Created, Description = "Возращает когда Product успешно сохранен в Базе Данных")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Возращает когда Product не валиден")]
        public IHttpActionResult PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Product.Add(product);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [ResponseType(typeof(Product))]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Возращает когда Product успешно удален из Базы Данных")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "Возращает когда Product по указоному Id не найден")]
        public IHttpActionResult DeleteProduct(int id)
        {
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            db.Product.Remove(product);
            db.SaveChanges();

            return Ok(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return db.Product.Count(e => e.Id == id) > 0;
        }
    }
}