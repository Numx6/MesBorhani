using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
	public class UserRepo:IUser
	{
		MesContext _db;
		public UserRepo(MesContext db)
		{
			this._db = db;
		}
		public bool AddUser(User user)
		{
			try
			{
				_db.Users.Add(user);
				return true;
			}
			catch (Exception)
			{

				return false;
			}
		}

		public bool DeleteUser(User user)
		{
			try
			{
				_db.Entry(user).State = EntityState.Deleted;
				return true;
			}
			catch (Exception)
			{

				return false;
			}
		}

		public bool DeleteUser(int id)
		{
			try
			{
				var user = GetUserById(id);
				DeleteUser(user);
				return true;
			}
			catch (Exception)
			{

				return false;
			}
		}

		public void Dispose()
		{
			_db.Dispose();
		}

		public IEnumerable<User> GetAllUser()
		{
			return _db.Users.ToList();
		}

		public User GetUserById(int id)
		{
			return _db.Users.Find(id);
		}

		public bool HasUser(User user)
		{
			try
			{
				var u = _db.Users.Any(p=>p.UserName==user.UserName);
				return u;
			}
			catch (Exception)
			{

				return false;
			}
		}

		public void Save()
		{
			_db.SaveChanges();
		}

		public bool UpdateUser(User user)
		{
			try
			{
				_db.Entry(user).State = EntityState.Modified;
				return true;
			}
			catch (Exception)
			{

				return false;
			}
		}
	}
}
