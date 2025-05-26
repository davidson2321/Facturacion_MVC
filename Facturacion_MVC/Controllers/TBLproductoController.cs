using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Facturacion_MVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Facturacion_MVC.Controllers
{
    public class TBLproductoController : Controller
    {
        public IActionResult Index()
        {
            DbfacturasContext db = new DbfacturasContext();
            var productos = db.Tblproductos.ToList();
            return View(productos);
        }

        public IActionResult Nuevo()
        {
            DbfacturasContext db = new DbfacturasContext();
            // Corregido el ViewBag para que coincida con la vista
            ViewBag.Categorias = new SelectList(db.TblcategoriaProds, "IdCategoria", "StrDescripcion");
            return View();
        }

        [HttpPost]
        public IActionResult Nuevo(IFormCollection collection, IFormFile? StrFotoFile)
        {
            try
            {
                DbfacturasContext db = new DbfacturasContext();
                var NuevoDatos = new Tblproducto();

                // Asignación de todos los campos
                NuevoDatos.StrNombre = collection["StrNombre"];
                NuevoDatos.StrCodigo = collection["StrCodigo"];
                NuevoDatos.StrDetalle = collection["StrDetalle"];

                // Corregido el nombre del campo
                NuevoDatos.NumPrecioCompra = double.Parse(collection["NumPrecioCompra"]);
                NuevoDatos.NumPrecioVenta = double.Parse(collection["NumPrecioVenta"]);
                NuevoDatos.NumStock = int.Parse(collection["NumStock"]);
                NuevoDatos.IdCategoria = int.Parse(collection["IdCategoria"]);

                // Manejo de la imagen
                if (StrFotoFile != null && StrFotoFile.Length > 0)
                {
                    // Aquí puedes guardar la imagen en el servidor
                    var fileName = Path.GetFileName(StrFotoFile.FileName);
                    var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "productos");

                    // Crear directorio si no existe
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }

                    var filePath = Path.Combine(uploadPath, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        StrFotoFile.CopyTo(stream);
                    }

                    NuevoDatos.StrFoto = fileName;
                }
                else
                {
                    NuevoDatos.StrFoto = "ninguna"; // valor por defecto
                }

                NuevoDatos.DtmFechaModifica = DateTime.Now.Date;
                NuevoDatos.StrUsuarioModifica = collection["StrUsuarioModifica"];

                db.Tblproductos.Add(NuevoDatos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Recargar las categorías en caso de error
                DbfacturasContext db = new DbfacturasContext();
                ViewBag.Categorias = new SelectList(db.TblcategoriaProds, "IdCategoria", "StrDescripcion");

                ModelState.AddModelError("", "Error al guardar el producto: " + ex.Message);
                return View();
            }
        }


        public IActionResult Editar(int id)
        {
            DbfacturasContext db = new DbfacturasContext();
            var producto = db.Tblproductos.Find(id);
            if (producto == null)
            {
                return NotFound();
            }
            // Cargar las categorías para el dropdown
            ViewBag.Categorias = new SelectList(db.TblcategoriaProds, "IdCategoria", "StrDescripcion", producto.IdCategoria);
            return View(producto);
        }
        [HttpPost]
        public IActionResult Editar(int id, IFormCollection collection, IFormFile? StrFotoFile)
        {
            try
            {
                DbfacturasContext db = new DbfacturasContext();
                var producto = db.Tblproductos.Find(id);
                if (producto == null)
                {
                    return NotFound();
                }
                // Asignación de todos los campos
                producto.StrNombre = collection["StrNombre"];
                producto.StrCodigo = collection["StrCodigo"];
                producto.StrDetalle = collection["StrDetalle"];
                producto.NumPrecioCompra = double.Parse(collection["NumPrecioCompra"]);
                producto.NumPrecioVenta = double.Parse(collection["NumPrecioVenta"]);
                producto.NumStock = int.Parse(collection["NumStock"]);
                producto.IdCategoria = int.Parse(collection["IdCategoria"]);
                // Manejo de la imagen
                if (StrFotoFile != null && StrFotoFile.Length > 0)
                {
                    var fileName = Path.GetFileName(StrFotoFile.FileName);
                    var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "productos");
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }
                    var filePath = Path.Combine(uploadPath, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        StrFotoFile.CopyTo(stream);
                    }
                    producto.StrFoto = fileName;
                }
                producto.DtmFechaModifica = DateTime.Now.Date;
                producto.StrUsuarioModifica = collection["StrUsuarioModifica"];
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Recargar las categorías en caso de error
                DbfacturasContext db = new DbfacturasContext();
                ViewBag.Categorias = new SelectList(db.TblcategoriaProds, "IdCategoria", "StrDescripcion");
                ModelState.AddModelError("", "Error al actualizar el producto: " + ex.Message);
                return View();
            }

           
        }
        [HttpGet]
        public IActionResult Borrar(int id)
        {
            try
            {
                DbfacturasContext db = new DbfacturasContext();
                var producto = db.Tblproductos.Find(id);
                if (producto == null)
                {
                    return NotFound();
                }
                db.Tblproductos.Remove(producto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al eliminar el producto: " + ex.Message);
                return RedirectToAction("Index");
            }
        }
    }
} 