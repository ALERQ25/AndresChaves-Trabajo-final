using AndresChaves.Context;
using AndresChaves.Models;
using AndresChaves.Services;
using System;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using System.Web.Security;

namespace AndresChaves.Controllers
{
    public class HomeController : Controller
    {
        private AndresChavesContext db = new AndresChavesContext();
        private readonly IAuthorizationService _authorizationService = new AuthorizationService();

        public ActionResult Gestion()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        // Acción para mostrar la vista del login
        public ActionResult Login()
        {
            return View();
        }

        // Acción para manejar el formulario de login
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel loginModel)
        {

            Usuarios usuario;
            var resultadoEncript = _authorizationService.Auth(loginModel.Usuario, loginModel.Contraseña, out usuario);
            switch (resultadoEncript)
            {
                // En caso que el resultado sea vacano
                case AuthResults.Success:
                    // Si ya existe una cookie de autenticacion se elimina
                    if (Request.Cookies["AuthCookie"] != null)
                    {
                        var existingCookie = new HttpCookie("AuthCookie")
                        {
                            Expires = DateTime.Now.AddDays(-1)
                        };
                        Response.Cookies.Add(existingCookie);
                    }
                    // se crea una nueva cookie de autenticacion con el nombre de usuario y una fecha de expiracion de 1 hora
                    var cookie = new HttpCookie("AuthCookie", loginModel.Usuario)
                    {
                        Expires = DateTime.Now.AddHours(1)
                    };
                    FormsAuthentication.SetAuthCookie(loginModel.Usuario, false);
                    Response.Cookies.Add(cookie);
                    // Redirige a la pagina principal despues de la autenticacion exitosa
                    return RedirectToAction("Gestion", "Home");

                // En caso de que el usuario no exista
                case AuthResults.NotExists:
                    // Se envia un mensaje de error al usuario
                    ModelState.AddModelError("", "El usuario no existe.");
                    break;

                // En caso de que la contraseña no coincida
                case AuthResults.PasswordNotMatch:
                    // Se envia un mensaje de error al usuario
                    ModelState.AddModelError("", "Contraseña incorrecta.");
                    break;

                // En caso de que el error sea desconocido
                default:
                    // Se envia un mensaje de error al usuario
                    ModelState.AddModelError("", "Error desconocido intenta nuevamente");
                    break;
            }


            // Si las credenciales son incorrectas, muestra un mensaje de error
            ViewBag.ErrorMessage = "Usuario o contraseña incorrectos.";
            return View();
        }

        // Acción para mostrar la vista de registro
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        // Acción para manejar el logout
        public ActionResult Logout()
        {
            // Eliminar la cookie de autenticación
            if (Request.Cookies["AuthCookie"] != null)
            {
                var existingCookie = new HttpCookie("AuthCookie")
                {
                    Expires = DateTime.Now.AddDays(-1) // Establecer la fecha de expiración para eliminar la cookie
                };
                Response.Cookies.Add(existingCookie);
            }

            // Cerrar la sesión actual
            FormsAuthentication.SignOut();

            // Redirigir al login o a la página principal
            return RedirectToAction("Index", "Home");
        }

    }
}

