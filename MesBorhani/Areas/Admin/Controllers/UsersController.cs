using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer;

namespace MesBorhani.Areas.Admin.Controllers
{
	public class UsersController : Controller
	{
		private MesContext _db;
		private IUser _user;
		public UsersController()
		{
			_db = new MesContext();
			_user = new UserRepo(_db);
		}

		public ActionResult Index()
		{
			var a = _user.GetAllUser();
			return View(_user.GetAllUser());
		}

		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "UserId,Name,UserName,Password,CreatDate")] User user)
		{
			if (ModelState.IsValid)
			{
				if (_user.HasUser(user))
				{
					ModelState.AddModelError("UserName", "کاربری با این نام کاربری از قبل ثبت شده است ! لطفا نام کاربری دیگری را انتخاب کنید .");
					return View(user);
				}
				user.CreatDate = DateTime.Now;
				_user.AddUser(user);
				_user.Save();
				return RedirectToAction("Index");
			}

			return View(user);
		}

		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			User user = _user.GetUserById(id.Value);
			if (user == null)
			{
				return HttpNotFound();
			}
			return View(user);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			var userDelete = _user.GetUserById(id);
			if (userDelete.UserName == "itali6267@gmail.com")
			{
				ViewBag.admin = "admin";
				return View();
			}
			else
			{
				_user.DeleteUser(id);
				_user.Save();
			}
			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				_user.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
