using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using CRUD_Alumnos.Models;
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;

namespace CRUD_Alumnos.Controllers{
    public class AlumnoController : Controller{

        //GET: Alumno
        public ActionResult Index(){
            try{
                using (var db = new AlumnoContext()){
                    return View(db.Alumno.ToList());
                }
            }
            catch (Exception){
                throw;
            }
            //List<Alumno>lista = db.Alumno.Where(a => a.Edad > 18).ToList();
        }

        public ActionResult Agregar(){
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Agregar(Alumno a){
            if(!ModelState.IsValid)
                return View();

            try{
                using (var db = new AlumnoContext()){
                    a.fechaRegistro = DateTime.Now;

                    db.Alumno.Add(a);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex){

                ModelState.AddModelError("","Error al registrar" + ex.Message);
                return View();
            }
        }

        public ActionResult Editar(int id){
            using (var db = new AlumnoContext()){
                  Alumno al = db.Alumno.Find(id);
                  return View(al);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Alumno a){
            if (!ModelState.IsValid)
                return View();

            try{
                using (var db = new AlumnoContext()){
                    Alumno al = db.Alumno.Find(a.id);
                    al.Nombres = a.Nombres;
                    al.Apellidos = a.Apellidos;
                    al.Sexo = a.Sexo;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
      
            }
            catch (Exception){

                throw;
            }
        
        }

        public ActionResult Detalles(int id){
            using (var db = new AlumnoContext()){
                Alumno al = db.Alumno.Find(id);
                return View(al);
            }
        }

        public ActionResult Eliminar(int id){
            using (var db = new AlumnoContext()){
                Alumno al = db.Alumno.Find(id);
                db.Alumno.Remove(al);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}
