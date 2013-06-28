using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RegApp2.Models
{
    public class ProfileRepository
    {
        private DataClasses1DataContext db = new DataClasses1DataContext();

        public IQueryable<profile> GetAll()
        {
            return db.profiles;
        }

        public void Add(profile entry)
        {
            db.profiles.InsertOnSubmit(entry);
        }

        public profile GetById(int id)
        {
            return db.profiles.SingleOrDefault(d => d.id == id);
        }

        public void Save()
        {
            db.SubmitChanges();
        }

        public void Delete(profile entry)
        {
            db.profiles.DeleteOnSubmit(entry);
        }
    }
}