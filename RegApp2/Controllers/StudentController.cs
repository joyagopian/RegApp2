using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RegApp2.Models;

namespace RegApp2.Controllers
{
    public class StudentController : Controller
    {
        //
        // GET: /Student/

        ProfileRepository repo = new ProfileRepository();
        semesterRepository SRepo = new semesterRepository();
        const int pageSize = 10;
        [HttpGet]
        public ActionResult Index(int page = 1, int sortBy = 1, bool isAsc = true)
        {
            IEnumerable<RegApp2.Models.profile> profiles;


            #region Sorting
            switch (sortBy)
            {
                case 1:
                    profiles = isAsc ? repo.GetAll().OrderBy(p => p.uid) : repo.GetAll().OrderByDescending(p => p.uid);
                    break;

                case 2:
                    profiles = isAsc ? repo.GetAll().OrderBy(p => p.firstname) : repo.GetAll().OrderByDescending(p => p.firstname);
                    break;

                case 3:
                    profiles = isAsc ? repo.GetAll().OrderBy(p => p.lastname) : repo.GetAll().OrderByDescending(p => p.lastname);
                    break;

                case 4:
                    profiles = isAsc ? repo.GetAll().OrderBy(p => p.email) : repo.GetAll().OrderByDescending(p => p.email);
                    break;

                case 5:
                    profiles = isAsc ? repo.GetAll().OrderBy(p => p.dob) : repo.GetAll().OrderByDescending(p => p.dob);
                    break;

                case 6:
                    profiles = isAsc ? repo.GetAll().OrderBy(p => p.gender) : repo.GetAll().OrderByDescending(p => p.gender);
                    break;

                case 7:
                    profiles = isAsc ? repo.GetAll().OrderBy(p => p.active) : repo.GetAll().OrderByDescending(p => p.active);
                    break;
                case 8:
                    profiles = isAsc ? repo.GetAll().OrderBy(p => p.semester) : repo.GetAll().OrderByDescending(p => p.semester);
                    break;


                default:
                    profiles = isAsc ? repo.GetAll().OrderBy(p => p.uid) : repo.GetAll().OrderByDescending(p => p.uid);
                    break;
            }
            #endregion

           // profiles = repo.GetAll().OrderBy(x => x.id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            profiles = repo.GetAll();
            ViewBag.currentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.totalPages = Math.Ceiling((double)repo.GetAll().Count() / pageSize);
            return View(profiles);
        }

        public ActionResult Create()
        {
            var gender = new SelectList(new[]
                                          {
                                              new {ID="Male",Name="Male"},
                                              new{ID="Female",Name="Female"}
                                          },
                          "ID", "Name", 1);
            ViewData["gender"] = gender;
            var semestersList = new SelectList(SRepo.getAll().OrderBy(s => s.semesterName), "id", "semesterName", null);
            ViewData["semestersList"] = semestersList;
            return View();
        }

        [HttpPost]
        public ActionResult Create(profile entry)
        {
            try
            {

                var gender = new SelectList(new[]
                                          {
                                              new {ID="Male",Name="Male"},
                                              new{ID="Female",Name="Female"}
                                          },
              "ID", "Name", 1);
                ViewData["gender"] = gender;
                var semestersList = new SelectList(SRepo.getAll().OrderBy(s => s.semesterName), "id", "semesterName", null);
                ViewData["semestersList"] = semestersList;

                entry.semester.id= 1;
                UpdateModel(entry);
                repo.Add(entry);
                repo.Save();

                return View("Index", repo.GetAll());
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Edit(int id) {
            return View(repo.GetById(id));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int id, profile profile)
        {

            profile = repo.GetById(id);

            UpdateModel(profile);

            repo.Save();

            return RedirectToAction("index");
        }

        public ActionResult Delete(int id)
        {
            repo.Delete(repo.GetById(id));
            repo.Save();
            return RedirectToAction("Index");
        }

    }
}

