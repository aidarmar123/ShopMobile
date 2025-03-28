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
    public class UsersController : ApiController
    {
        private ShopMobileEntities db = new ShopMobileEntities();

        // GET: api/Users
        [SwaggerResponse(HttpStatusCode.OK, Description ="Возращает все User")]
        public IQueryable<User> GetUser()
        {
            return db.User;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Users/5
        [ResponseType(typeof(User))]
        [SwaggerResponse(HttpStatusCode.OK, Description ="Возращает User по Id")]
[SwaggerResponse(HttpStatusCode.NotFound, Description = "Возращает когда User по указоному Id не найден")]
        public IHttpActionResult GetUser(int id)
        {
            User user = db.User.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
         [SwaggerResponse(HttpStatusCode.NoContent, Description = "Возращает когда  User успешно изменен")]
 [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Возращает когда  User не валиден")]
 [SwaggerResponse(HttpStatusCode.NotFound, Description = "Возращает когда  User по указоному Id не найден")]
        public IHttpActionResult PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        [ResponseType(typeof(User))]
        [SwaggerResponse(HttpStatusCode.Created, Description = "Возращает когда User успешно сохранен в Базе Данных")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Возращает когда User не валиден")]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.User.Add(user);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Возращает когда User успешно удален из Базы Данных")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "Возращает когда User по указоному Id не найден")]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = db.User.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.User.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.User.Count(e => e.Id == id) > 0;
        }
    }
}