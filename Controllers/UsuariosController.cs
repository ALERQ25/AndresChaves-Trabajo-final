using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AndresChaves.Context;
using AndresChaves.Models;
using System.Security.Cryptography;
using System.Text;
using AndresChaves.Utils;
using AndresChaves.Security;


namespace AndresChaves.Controllers
{
    public class UsuariosController : Controller
    {
        private AndresChavesContext db = new AndresChavesContext();
        private readonly IPasswordEncripter _passwordEncripter = new PasswordEncripter();

        // Método para encriptar la contraseña
        private string EncriptarContraseña(string contraseña)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(contraseña));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        // GET: Usuarios
        public ActionResult Index()
        {
            return View(db.Usuarios.ToList());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Usuarios,Usuario,Contraseña,Fecha_creacion,HashKey,HashIV")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                var key = new byte[32];
                var iv = new byte[16];
                // Se usa RNGCryptoServiceProvider para generar valores aleatorios seguros
                using (var rng = new RNGCryptoServiceProvider())
                {
                    rng.GetBytes(key);
                    rng.GetBytes(iv);
                }
                // se encripta la contraseña utilizando la Key y IV generados
                var passwordEncrypted = _passwordEncripter.Encript(usuarios.Contraseña, new List<byte[]> { key, iv });

                // Se crea un nuevo objeto de User donde se pone la contraseña cifrada y las claves generadas
                var newUser = new Usuarios
                {
                    Usuario = usuarios.Usuario,
                    Contraseña = passwordEncrypted,
                    HashKey = key,
                    HashIV = iv,
                    Fecha_creacion = DateTime.UtcNow
                };

                // Se añade el nuevo usuario a la base de datos
                db.Usuarios.Add(newUser);
                // Se guardan los cambios en la base de datos
                db.SaveChanges();
                return RedirectToAction("Index", "Home");

            }

            return View(usuarios);
        }



        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Usuarios,Usuario,Contraseña,Fecha_creacion,HashKey,HashIV")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                // Si la contraseña ha sido modificada, encriptarla antes de guardarla
                if (!string.IsNullOrEmpty(usuarios.Contraseña))
                {
                    usuarios.Contraseña = EncriptarContraseña(usuarios.Contraseña);
                }
                db.Entry(usuarios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usuarios);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuarios usuarios = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuarios);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

