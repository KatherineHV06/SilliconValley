using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SilliconValley.Integration.dto;
using SilliconValley.Integration;




namespace SilliconValley.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly ListarUsuarioAPIIntegration _listUsers;
        private readonly ListarUnUsuarioAPIIntegration _unUser;
        private readonly CrearUsuarioAPIIntegration _createUser;

        public UsuarioController(ILogger<UsuarioController> logger,
        ListarUsuarioAPIIntegration listUsers,
         ListarUnUsuarioAPIIntegration unUser,
       CrearUsuarioAPIIntegration createUser)

        {
            _logger = logger;
            _listUsers = listUsers;
            _unUser = unUser;
            _createUser = createUser;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Usuario> users = await _listUsers.GetAllUser();
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Perfil(int Id)
        {
            Usuario user = await _unUser.GetUser(Id);
            return View(user);
        }

         public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(string name, string job)
        {
            try
            {

                var response = await _createUser.CreateUser(name, job);


                if (response != null)
                {

                    TempData["SuccessMessage"] = "Usuario creado correctamente.";
                }
                else
                {

                    ModelState.AddModelError("", "Error al crear el usuario");
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Error al crear el usuario: {ex.Message}");
                ModelState.AddModelError("", "Error al crear el usuario");
            }


            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}