using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContactAppFluentMVC.Data;
using ContactAppFluentMVC.Models;
using NHibernate.Linq;

namespace ContactAppFluentMVC.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                var user = session.Query<User>().FetchMany(u => u.Contacts).ToList();
                return View(user);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Create(User user)
        {

            using (var session = NHibernateHelper.CreateSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    session.Save(user);
                    txn.Commit();
                    return RedirectToAction("Index");
                }
            }
        }

        public ActionResult Edit(int id)
        {

            using (var session = NHibernateHelper.CreateSession())
            {
                var user = session.Get<User>(id);
                return View(user);
            }
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                using (var txn = session.BeginTransaction())
                {

                    session.Update(user);
                    txn.Commit();
                    return RedirectToAction("Index");
                }
            }

        }

        public ActionResult Delete(int id)
        {

            using (var session = NHibernateHelper.CreateSession())
            {
                var user = session.Get<User>(id);
                return View(user);
            }
        }


        [HttpPost ,ActionName("Delete")]

        public ActionResult DeleteUser(int id) {

            using (var session = NHibernateHelper.CreateSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    var user = session.Get<User>(id);
                    user.IsActive = false;
                    session.Update(user);
                    txn.Commit();
                    return RedirectToAction("Index");
                }
            }
        }
    }
}