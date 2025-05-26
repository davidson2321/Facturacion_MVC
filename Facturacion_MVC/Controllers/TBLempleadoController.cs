
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Facturacion_MVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Facturacion_MVC.Controllers
{
    public class TBLempleadoController : Controller
    {
        // GET: TBLempleado
        public IActionResult Index()
        {
            DbfacturasContext db = new DbfacturasContext();
            var empleados = db.Tblempleados.ToList();

            return View(empleados);
        }
        public IActionResult Nuevo()
        {
            DbfacturasContext db = new DbfacturasContext();
            ViewBag.IdRolEmpleado = new SelectList(db.Tblroles, "IdRolEmpleado", "StrDescripcion");
            return View();
        }
        [HttpPost]
        public IActionResult Nuevo(IFormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DbfacturasContext db = new DbfacturasContext();
                    var NuevoDatos = new Tblempleado();
                    NuevoDatos.StrNombre = collection["StrNombre"];
                    NuevoDatos.NumDocumento = long.Parse(collection["NumDocumento"]);
                    NuevoDatos.StrDireccion = collection["StrDireccion"];
                    NuevoDatos.StrTelefono = collection["StrTelefono"];
                    NuevoDatos.StrEmail = collection["StrEmail"];
                    NuevoDatos.IdRolEmpleado = int.Parse(collection["IdRolEmpleado"]);
                    NuevoDatos.DtmIngreso = Convert.ToDateTime(collection["DtMIngreso"]);
                    NuevoDatos.DtmRetiro = Convert.ToDateTime(collection["DtMRetiro"]);
                    NuevoDatos.StrDatosAdicionales = collection["StrDatosAdicionales"];
                    NuevoDatos.DtmFechaModifica = DateTime.Now.Date;
                    NuevoDatos.StrUsuarioModifico = collection["StrUsuarioModifico"];
                    db.Tblempleados.Add(NuevoDatos);
                    db.SaveChanges();
                    return RedirectToAction("Index");




                }
                return View(collection);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al guardar el empleado: " + ex.Message);
                return View(collection);
            }
        }

        public IActionResult Editar(int id)
        {
            DbfacturasContext db = new DbfacturasContext();
            var empleado = db.Tblempleados.Find(id);
            if (empleado == null)
            {
                return NotFound();
            }
            ViewBag.IdRolEmpleado = new SelectList(db.Tblroles, "IdRolEmpleado", "StrDescripcion", empleado.IdRolEmpleado);
            return View(empleado);
        }
        [HttpPost]
        public IActionResult Editar(int id, IFormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DbfacturasContext db = new DbfacturasContext();
                    var empleado = db.Tblempleados.Find(id);
                    if (empleado == null)
                    {
                        return NotFound();
                    }
                    empleado.StrNombre = collection["StrNombre"];
                    empleado.NumDocumento = long.Parse(collection["NumDocumento"]);
                    empleado.StrDireccion = collection["StrDireccion"];
                    empleado.StrTelefono = collection["StrTelefono"];
                    empleado.StrEmail = collection["StrEmail"];
                    empleado.IdRolEmpleado = int.Parse(collection["IdRolEmpleado"]);
                    empleado.DtmIngreso = Convert.ToDateTime(collection["DtMIngreso"]);
                    empleado.DtmRetiro = Convert.ToDateTime(collection["DtMRetiro"]);
                    empleado.StrDatosAdicionales = collection["StrDatosAdicionales"];
                    empleado.DtmFechaModifica = DateTime.Now.Date;
                    empleado.StrUsuarioModifico = collection["StrUsuarioModifico"];
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(collection);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al editar el empleado: " + ex.Message);
                return View(collection);
            }
        }
        [HttpGet]
        public IActionResult Borrar(int id)
        {
            try
            {
                DbfacturasContext db = new DbfacturasContext();
                var empleado = db.Tblempleados.Find(id);
               
                db.Tblempleados.Remove(empleado);
                db.SaveChanges();
               
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al eliminar el empleado: " + ex.Message);
                
            }
            return RedirectToAction("Index");
        }
    }
}