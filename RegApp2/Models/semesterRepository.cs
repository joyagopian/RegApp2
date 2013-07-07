using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RegApp2.Models
{
    public class semesterRepository
    {
        private DataClasses1DataContext db = new DataClasses1DataContext();
        public IQueryable<semester> getAll()
        {
            var allSemesters =  db.semesters;
            return allSemesters;
        }
    }
}