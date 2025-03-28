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
    public class RolesController : ApiController
    {
        private ShopMobileEntities db = new ShopMobileEntities();

        // GET: api/Roles
        [SwaggerResponse(HttpStatusCode.OK, Description ="Возращает все Role")]
        public IQueryable<Role> GetRole()
        {
            return db.Role;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Roles/5
        [ResponseType(typeof(Role))]
        [SwaggerResponse(HttpStatusCode.OK, Description ="Возращает Role по Id")]
[SwaggerResponse(HttpStatusCode.NotFound, Description = "Возращает когда Role по указоному Id не найден")]
        public IHttpActionResult GetRole(int id)
        {
            Role role = db.Role.Find(id);
            if (role == null)
            {
                return NotFound();
            }

            return Ok(role);
        }

        // PUT: api/Roles/5
        [ResponseType(typeof(void))]
         [SwaggerResponse(HttpStatusCode.NoContent, Description = "Возращает когда  Role успешно изменен")]
 [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Возращает когда  Role не валиден")]
 [SwaggerResponse(HttpStatusCode.NotFound, Description = "Возращает когда  Role по указоному Id не найден")]
        public IHttpActionResult PutRole(int id, Role role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != role.Id)
            {
                return BadRequest();
            }

            db.Entry(role).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleExists(id))
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

        // POST: api/Roles
        [ResponseType(typeof(Role))]
        [SwaggerResponse(HttpStatusCode.Created, Description = "Возращает когда Role успешно сохранен в Базе Данных")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Возращает когда Role не валиден")]
        public IHttpActionResult PostRole(Role role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Role.Add(role);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = role.Id }, role);
        }

        // DELETE: api/Roles/5
        [ResponseType(typeof(Role))]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Возращает когда Role успешно удален из Базы Данных")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "Возращает когда Role по указоному Id не найден")]
        public IHttpActionResult DeleteRole(int id)
        {
            Role role = db.Role.Find(id);
            if (role == null)
            {
                return NotFound();
            }

            db.Role.Remove(role);
            db.SaveChanges();

            return Ok(role);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RoleExists(int id)
        {
            return db.Role.Count(e => e.Id == id) > 0;
        }
    }
}