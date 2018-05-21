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

using System.Web.Http.Cors;
using CManager.Models;

namespace CalendarManager.Controllers
{
    [EnableCors(origins: "http://localhost:4297", headers: "*", methods: "*")]
    public class CalendarsController : ApiController
    {
        private CManagerContext db = new CManagerContext();

        [Route("Calendars")]
        [HttpGet]
        // GET: api/Calendars
        public IQueryable<CalendarModel> GetCalendars()
        {
            return db.CalendarModels;
        }

        [Route("Calendars/{id}")]
        [HttpGet]
        // GET: api/Calendars/5
        public IHttpActionResult Get(int id)
        {
            CalendarModel calendar = db.CalendarModels.Find(id);
            if (calendar == null)
            {
                return NotFound();
            }

            return Ok(calendar);
        }

        [Route("Calendars/{id}")]
        // PUT: api/Calendars/5
        [HttpPut]
        public IHttpActionResult Put(int id, CalendarModel calendar)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            if (id != calendar.Id)
            {
                return BadRequest();
            }

            db.Entry(calendar).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalendarExists(id))
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

        [Route("Calendars", Name = "post")]
        [HttpPost]
        public IHttpActionResult Post(CalendarModel calendar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CalendarModels.Add(calendar);
            db.SaveChanges();

            return CreatedAtRoute("post", new { id = calendar.Id }, calendar);
        }

        [Route("Calendars/{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            CalendarModel calendar = db.CalendarModels.Find(id);
            if (calendar == null)
            {
                return NotFound();
            }

            db.CalendarModels.Remove(calendar);
            db.SaveChanges();


            return Ok(calendar);
        }

        [Route("Calendars")]
        [HttpGet]
        // GET: api/Calendars
        public IQueryable<CalendarModel> GetByDay(DateTime  dt)
        {

            IQueryable<CalendarModel> model = from r in db.CalendarModels
                                              where r.Date == dt
                                              select r;

            

            return model;

            //foreach (var evnt in db.CalendarModels)
            //{
            //    if (evnt.Date.Year
            //}
        }

        //[Route("events/{year}/{month}/{day}")]
        //[HttpGet]
        //public IQueryable<CalendarModel> GetEventsByDate(int year, int month, int day)
        //{
        //    DateTime date = new DateTime(year, month, day);
        //    //var dt = new DateTime();
        //    //dt = DateTime.Parse("meme");

        //    return db.CalendarModels.Where(e => e.Date.Year == year && e.Day.Month == month && e.Day.Day == day);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CalendarExists(int id)
        {
            return db.CalendarModels.Count(e => e.Id == id) > 0;
        }

        private bool DateExists(int year, int month, int day)
        {
            DateTime date = new DateTime(year, month, day);
            return db.CalendarModels.Count(e => e.Date == date) > 0;
        }
    }
}