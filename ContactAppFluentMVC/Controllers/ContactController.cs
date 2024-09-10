using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContactAppFluentMVC.Data;
using ContactAppFluentMVC.Models;

namespace ContactAppFluentMVC.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetContacts(int UserId)
        {
            Session["UserId"] = UserId;
            using (var session = NHibernateHelper.CreateSession())
            {
                var contacts = session.Query<Contact>().Where(c => c.User.Id == UserId).ToList();
                return View(contacts);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Create(Contact contact)
        {
            int id = (int)Session["UserId"];
            using (var session = NHibernateHelper.CreateSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    contact.User.Id = id;
                    session.Save(contact);
                    txn.Commit();
                    return RedirectToAction("GetContacts", new { UserId = id });
                }
            }

        }

        public ActionResult Edit(int id)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                var contact = session.Get<Contact>(id);
                return View(contact);
            }
        }

        [HttpPost]
        public ActionResult Edit(Contact contact)
        {
            int id = (int)Session["UserId"];
            using (var session = NHibernateHelper.CreateSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    contact.User.Id = id;
                    session.Update(contact);
                    txn.Commit();
                    return RedirectToAction("GetContacts", new { UserId = id });
                }
            }
        }

        public ActionResult Delete(int id)
        {

            using (var session = NHibernateHelper.CreateSession())
            {
                var contact = session.Get<Contact>(id);
                return View(contact);
            }
        }

        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteContact(int id)
        {
            int UserId = (int)Session["UserId"];
            using (var session = NHibernateHelper.CreateSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    var contact = session.Get<Contact>(id);
                    contact.IsActive = false;
                    session.Update(contact);
                    txn.Commit();
                    return RedirectToAction("GetContacts", new { UserId = UserId });
                }
            }

        }
    }
}