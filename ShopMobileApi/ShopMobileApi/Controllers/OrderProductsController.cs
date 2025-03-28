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
    public class OrderProductsController : ApiController
    {
        private ShopMobileEntities db = new ShopMobileEntities();

        // GET: api/OrderProducts
        [SwaggerResponse(HttpStatusCode.OK, Description ="Возращает все OrderProduct")]
        public IQueryable<OrderProduct> GetOrderProduct()
        {
            return db.OrderProduct;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/OrderProducts/5
        [ResponseType(typeof(OrderProduct))]
        [SwaggerResponse(HttpStatusCode.OK, Description ="Возращает OrderProduct по Id")]
[SwaggerResponse(HttpStatusCode.NotFound, Description = "Возращает когда OrderProduct по указоному Id не найден")]
        public IHttpActionResult GetOrderProduct(int id)
        {
            OrderProduct orderProduct = db.OrderProduct.Find(id);
            if (orderProduct == null)
            {
                return NotFound();
            }

            return Ok(orderProduct);
        }

        // PUT: api/OrderProducts/5
        [ResponseType(typeof(void))]
         [SwaggerResponse(HttpStatusCode.NoContent, Description = "Возращает когда  OrderProduct успешно изменен")]
 [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Возращает когда  OrderProduct не валиден")]
 [SwaggerResponse(HttpStatusCode.NotFound, Description = "Возращает когда  OrderProduct по указоному Id не найден")]
        public IHttpActionResult PutOrderProduct(int id, OrderProduct orderProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orderProduct.Id)
            {
                return BadRequest();
            }

            db.Entry(orderProduct).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderProductExists(id))
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

        // POST: api/OrderProducts
        [ResponseType(typeof(OrderProduct))]
        [SwaggerResponse(HttpStatusCode.Created, Description = "Возращает когда OrderProduct успешно сохранен в Базе Данных")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Возращает когда OrderProduct не валиден")]
        public IHttpActionResult PostOrderProduct(OrderProduct orderProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OrderProduct.Add(orderProduct);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = orderProduct.Id }, orderProduct);
        }

        // DELETE: api/OrderProducts/5
        [ResponseType(typeof(OrderProduct))]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Возращает когда OrderProduct успешно удален из Базы Данных")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "Возращает когда OrderProduct по указоному Id не найден")]
        public IHttpActionResult DeleteOrderProduct(int id)
        {
            OrderProduct orderProduct = db.OrderProduct.Find(id);
            if (orderProduct == null)
            {
                return NotFound();
            }

            db.OrderProduct.Remove(orderProduct);
            db.SaveChanges();

            return Ok(orderProduct);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderProductExists(int id)
        {
            return db.OrderProduct.Count(e => e.Id == id) > 0;
        }
    }
}