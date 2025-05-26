using Facturacion_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Facturacion_MVC.Controllers
{
    public class TBLclienteController : Controller
    {
        public IActionResult Index()
        {
            DbfacturasContext db = new DbfacturasContext();
            var clientes = db.Tblclientes.ToList();
            return View(clientes);
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
                    var NuevoDatos = new Tblcliente();
                    NuevoDatos.StrNombre = collection["StrNombre"];
                    NuevoDatos.NumDocumento = long.Parse(collection["NumDocumento"]);
                    NuevoDatos.StrDireccion = collection["StrDireccion"];
                    NuevoDatos.StrTelefono = collection["StrTelefono"];
                    NuevoDatos.StrEmail = collection["StrEmail"];
                    NuevoDatos.DtmFechaModifica = DateTime.Now.Date;
                    NuevoDatos.StrUsuarioModifica = collection["StrUsuarioModifica"];
                    db.Tblclientes.Add(NuevoDatos);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(collection);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al guardar el cliente: " + ex.Message);
                return View(collection);
            }
        }
        public IActionResult Editar(int id)
        {
            DbfacturasContext db = new DbfacturasContext();
            var cliente = db.Tblclientes.Find(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }
        [HttpPost]
        public IActionResult Editar(int id, IFormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DbfacturasContext db = new DbfacturasContext();
                    var cliente = db.Tblclientes.Find(id);
                    if (cliente == null)
                    {
                        return NotFound();
                    }
                    cliente.StrNombre = collection["StrNombre"];
                    cliente.NumDocumento = long.Parse(collection["NumDocumento"]);
                    cliente.StrDireccion = collection["StrDireccion"];
                    cliente.StrTelefono = collection["StrTelefono"];
                    cliente.StrEmail = collection["StrEmail"];
                    cliente.DtmFechaModifica = DateTime.Now.Date;
                    cliente.StrUsuarioModifica = collection["StrUsuarioModifica"];
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(collection);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al actualizar el cliente: " + ex.Message);
                return View(collection);
            }
        }
        [HttpGet]
        public IActionResult Borrar(int id)
        {
            try
            {
                DbfacturasContext db = new DbfacturasContext();
                var cliente = db.Tblclientes.Find(id);
                if (cliente == null)
                {
                    return NotFound();
                }
                db.Tblclientes.Remove(cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al eliminar el cliente: " + ex.Message);
                return RedirectToAction("Index");
            }
        }
    }
}