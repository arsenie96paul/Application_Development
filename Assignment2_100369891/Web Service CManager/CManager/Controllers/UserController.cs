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
using CManager.Models;
using System.Web.Http.Cors;

namespace CManager.Controllers
{
    [EnableCors(origins: "http://localhost:4297", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        private CManagerContext db = new CManagerContext();

        [Route("Users")]
        [HttpGet]
        // GET: api/User
        public IQueryable<UserModel> GetUserModels()
        {
            return db.UserModels;
        }

        [Route("Users/{email}")]
        [HttpGet]
        public IHttpActionResult GetUserModel(string email)
        {
            UserModel userModel = db.UserModels.Find(email);
            if (userModel == null)
            {
                return NotFound();
            }

            return Ok(userModel);
        }

        [Route("Users/{id}")]
        [HttpPut]
        // PUT: api/User/5
        public IHttpActionResult PutUserModel(int id, UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userModel.Id)
            {
                return BadRequest();
            }

            db.Entry(userModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserModelExists(id))
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

        [Route("Users", Name = "PostUser")]
        [HttpPost]
        // POST: api/User
        public IHttpActionResult PostUserModel(UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserModels.Add(userModel);
            db.SaveChanges();

            return CreatedAtRoute("PostUser", new { id = userModel.Id }, userModel);
        }

        [Route("Users/{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteUserModel(int id)
        {
            UserModel userModel = db.UserModels.Find(id);
            if (userModel == null)
            {
                return NotFound();
            }

            db.UserModels.Remove(userModel);
            db.SaveChanges();

            return Ok(userModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserModelExists(int id)
        {
            return db.UserModels.Count(e => e.Id == id) > 0;
        }
    }
}