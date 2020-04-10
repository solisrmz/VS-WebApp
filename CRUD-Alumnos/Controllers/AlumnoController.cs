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
              
                //uso de linq para estructurar consultas más complejas entre tablas
                using (var db = new AlumnoContext()){
                    var data = from a in db.Alumno
                               join c in db.Ciudad on a.CodCiudad equals c.id
                               select new AlumnoCE(){
                                   id = a.id,
                                   Nombres = a.Nombres,
                                   Apellidos = a.Apellidos,
                                   Edad = a.Edad,
                                   Sexo = a.Sexo,
                                   NombreCiudad = c.Nombre
                               };

                    //List<Alumno>lista = db.Alumno.Where(a => a.Edad > 18).ToList();
                    return View(data.ToList());
                }
            }
            catch (Exception){
                throw;
            }
        }

        //Devuelve la vista para agregar un alumno
        public ActionResult Agregar(){
            return View();
        }

        //Ejemplo para ver el nuevo form de registro, no se utiliza
        public ActionResult AgregarCiudad(){
            return View();
        }

        //Vista parcial para mostrar las ciudades
        public ActionResult ListarCiudades(){
            using (var db = new AlumnoContext()){
                return PartialView(db.Ciudad.ToList());
            }
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

        //Se obtiene el nombre de la ciudad usando el id de la ciudad en alumno
        public static string NombreCiudad(int CodCiudad){
            using(var db = new AlumnoContext()){
                return db.Ciudad.Find(CodCiudad).Nombre;
            }
        }
    }
}
