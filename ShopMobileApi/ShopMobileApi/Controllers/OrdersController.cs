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
    public class OrdersController : ApiController
    {
        private ShopMobileEntities db = new ShopMobileEntities();

        // GET: api/Orders
        [SwaggerResponse(HttpStatusCode.OK, Description ="Возращает все Order")]
        public IQueryable<Order> GetOrder()
        {
            return db.Order;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Orders/5
        [ResponseType(typeof(Order))]
        [SwaggerResponse(HttpStatusCode.OK, Description ="Возращает Order по Id")]
[SwaggerResponse(HttpStatusCode.NotFound, Description = "Возращает когда Order по указоному Id не найден")]
        public IHttpActionResult GetOrder(int id)
        {
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // PUT: api/Orders/5
        [ResponseType(typeof(void))]
         [SwaggerResponse(HttpStatusCode.NoContent, Description = "Возращает когда  Order успешно изменен")]
 [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Возращает когда  Order не валиден")]
 [SwaggerResponse(HttpStatusCode.NotFound, Description = "Возращает когда  Order по указоному Id не найден")]
        public IHttpActionResult PutOrder(int id, Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order.Id)
            {
                return BadRequest();
            }

            db.Entry(order).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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

        // POST: api/Orders
        [ResponseType(typeof(Order))]
        [SwaggerResponse(HttpStatusCode.Created, Description = "Возращает когда Order успешно сохранен в Базе Данных")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Возращает когда Order не валиден")]
        public IHttpActionResult PostOrder(Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Order.Add(order);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = order.Id }, order);
        }

        // DELETE: api/Orders/5
        [ResponseType(typeof(Order))]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Возращает когда Order успешно удален из Базы Данных")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "Возращает когда Order по указоному Id не найден")]
        public IHttpActionResult DeleteOrder(int id)
        {
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            db.Order.Remove(order);
            db.SaveChanges();

            return Ok(order);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderExists(int id)
        {
            return db.Order.Count(e => e.Id == id) > 0;
        }
    }
}