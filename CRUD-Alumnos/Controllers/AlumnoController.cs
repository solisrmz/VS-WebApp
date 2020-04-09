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

        //Obtiene todos los alumnos
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

        //Devuelve la vista para agregar un alumno
        public ActionResult Agregar(){
            return View();
        }


        //Guarda los datos en la BD
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


        //Devuelve la vista para poder editar GET
        public ActionResult Editar(int id){
            using (var db = new AlumnoContext()){
                  Alumno al = db.Alumno.Find(id);
                  return View(al);
            }
        }

        //Hace la inserción de los datos ya editados POST
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

        //Muestra los detalles de un alumno
        public ActionResult Detalles(int id){
            using (var db = new AlumnoContext()){
                Alumno al = db.Alumno.Find(id);
                return View(al);
            }
        }

        //Método para eliminar elemento de la BD
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
