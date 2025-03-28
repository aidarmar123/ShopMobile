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
    public class PhotoProductsController : ApiController
    {
        private ShopMobileEntities db = new ShopMobileEntities();

        // GET: api/PhotoProducts
        [SwaggerResponse(HttpStatusCode.OK, Description ="Возращает все PhotoProduct")]
        public IQueryable<PhotoProduct> GetPhotoProduct()
        {
            return db.PhotoProduct;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/PhotoProducts/5
        [ResponseType(typeof(PhotoProduct))]
        [SwaggerResponse(HttpStatusCode.OK, Description ="Возращает PhotoProduct по Id")]
[SwaggerResponse(HttpStatusCode.NotFound, Description = "Возращает когда PhotoProduct по указоному Id не найден")]
        public IHttpActionResult GetPhotoProduct(int id)
        {
            PhotoProduct photoProduct = db.PhotoProduct.Find(id);
            if (photoProduct == null)
            {
                return NotFound();
            }

            return Ok(photoProduct);
        }

        // PUT: api/PhotoProducts/5
        [ResponseType(typeof(void))]
         [SwaggerResponse(HttpStatusCode.NoContent, Description = "Возращает когда  PhotoProduct успешно изменен")]
 [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Возращает когда  PhotoProduct не валиден")]
 [SwaggerResponse(HttpStatusCode.NotFound, Description = "Возращает когда  PhotoProduct по указоному Id не найден")]
        public IHttpActionResult PutPhotoProduct(int id, PhotoProduct photoProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != photoProduct.Id)
            {
                return BadRequest();
            }

            db.Entry(photoProduct).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhotoProductExists(id))
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

        // POST: api/PhotoProducts
        [ResponseType(typeof(PhotoProduct))]
        [SwaggerResponse(HttpStatusCode.Created, Description = "Возращает когда PhotoProduct успешно сохранен в Базе Данных")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Возращает когда PhotoProduct не валиден")]
        public IHttpActionResult PostPhotoProduct(PhotoProduct photoProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PhotoProduct.Add(photoProduct);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = photoProduct.Id }, photoProduct);
        }

        // DELETE: api/PhotoProducts/5
        [ResponseType(typeof(PhotoProduct))]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Возращает когда PhotoProduct успешно удален из Базы Данных")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "Возращает когда PhotoProduct по указоному Id не найден")]
        public IHttpActionResult DeletePhotoProduct(int id)
        {
            PhotoProduct photoProduct = db.PhotoProduct.Find(id);
            if (photoProduct == null)
            {
                return NotFound();
            }

            db.PhotoProduct.Remove(photoProduct);
            db.SaveChanges();

            return Ok(photoProduct);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PhotoProductExists(int id)
        {
            return db.PhotoProduct.Count(e => e.Id == id) > 0;
        }
    }
}