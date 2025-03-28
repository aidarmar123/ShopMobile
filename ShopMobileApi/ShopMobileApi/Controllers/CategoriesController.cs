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
    public class CategoriesController : ApiController
    {
        private ShopMobileEntities db = new ShopMobileEntities();

        // GET: api/Categories
        [SwaggerResponse(HttpStatusCode.OK, Description = "Возращает все Category")]
        public IQueryable<Category> GetCategory()
        {
            return db.Category;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Categories/5
        [ResponseType(typeof(Category))]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Возращает Category по Id")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "Возращает когда Category по указоному Id не найден")]
        public IHttpActionResult GetCategory(int id)
        {
            Category category = db.Category.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // PUT: api/Categories/5
        [ResponseType(typeof(void))]
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Возращает когда  Category успешно изменен")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Возращает когда  Category не валиден")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "Возращает когда  Category по указоному Id не найден")]
        public IHttpActionResult PutCategory(int id, Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != category.Id)
            {
                return BadRequest();
            }

            db.Entry(category).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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

        // POST: api/Categories
        [ResponseType(typeof(Category))]
        [SwaggerResponse(HttpStatusCode.Created, Description = "Возращает когда Category успешно сохранен в Базе Данных")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Возращает когда Category не валиден")]
        public IHttpActionResult PostCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Category.Add(category);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = category.Id }, category);
        }

        // DELETE: api/Categories/5
        [ResponseType(typeof(Category))]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Возращает когда Category успешно удален из Базы Данных")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "Возращает когда Category по указоному Id не найден")]
        public IHttpActionResult DeleteCategory(int id)
        {
            Category category = db.Category.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            db.Category.Remove(category);
            db.SaveChanges();

            return Ok(category);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoryExists(int id)
        {
            return db.Category.Count(e => e.Id == id) > 0;
        }
    }
}