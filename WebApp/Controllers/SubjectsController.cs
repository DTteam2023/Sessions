﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using WebApplication1.Models;

namespace WebApp.Controllers
{
    public class SubjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Subjects
        public ActionResult Index()
        {
            var sessionSubjects = db.SessionSubjects.Include(s => s.NewSession);
            return View(sessionSubjects.ToList());
        }

        public ActionResult CustIndex(int? id)
        {
            var sessionSubjects = db.SessionSubjects.Where(m => m.NewSessionID == 2).ToList();
            if (sessionSubjects==null)
            {
                return HttpNotFound();
            }
            else
                return View(sessionSubjects.ToList());
        }

        // GET: Subjects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SessionSubjects sessionSubjects = db.SessionSubjects.Find(id);
            if (sessionSubjects == null)
            {
                return HttpNotFound();
            }
            return View(sessionSubjects);
        }

        // GET: Subjects/Create
        public ActionResult Create()
        {
            ViewBag.NewSessionID = new SelectList(db.NewSessions, "ID", "Session_Num");
            return View();
        }

        // POST: Subjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Sub_ID,Sub_Name,Sub_Description,NewSessionID")] SessionSubjects sessionSubjects)
        {
            if (ModelState.IsValid)
            {
                db.SessionSubjects.Add(sessionSubjects);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.NewSessionID = new SelectList(db.NewSessions, "ID", "Session_Num", sessionSubjects.NewSessionID);
            return View(sessionSubjects);
        }

        // GET: Subjects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SessionSubjects sessionSubjects = db.SessionSubjects.Find(id);
            if (sessionSubjects == null)
            {
                return HttpNotFound();
            }
            ViewBag.NewSessionID = new SelectList(db.NewSessions, "ID", "Session_Num", sessionSubjects.NewSessionID);
            return View(sessionSubjects);
        }

        // POST: Subjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Sub_ID,Sub_Name,Sub_Description,NewSessionID")] SessionSubjects sessionSubjects)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sessionSubjects).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NewSessionID = new SelectList(db.NewSessions, "ID", "Session_Num", sessionSubjects.NewSessionID);
            return View(sessionSubjects);
        }

        // GET: Subjects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SessionSubjects sessionSubjects = db.SessionSubjects.Find(id);
            if (sessionSubjects == null)
            {
                return HttpNotFound();
            }
            return View(sessionSubjects);
        }

        // POST: Subjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SessionSubjects sessionSubjects = db.SessionSubjects.Find(id);
            db.SessionSubjects.Remove(sessionSubjects);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}