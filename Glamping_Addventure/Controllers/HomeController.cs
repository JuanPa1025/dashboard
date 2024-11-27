using Glamping_Addventure.Models;
using Glamping_Addventure.Models.View;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Glamping_Addventure.Controllers
{
    public class HomeController : Controller
    {
        private readonly GlampingAddventuresContext _dbcontext;

        public HomeController(GlampingAddventuresContext context)
        {
            _dbcontext = context;
        }

        public IActionResult resumenReserva()
        {
            DateTime FechaInicio = DateTime.Now;
            FechaInicio = FechaInicio.AddDays(-5);

            List<VMReserva> lista = (from tbreserva in _dbcontext.Reservas
                                      where tbreserva.FechaReserva.Value.Day >= FechaInicio.Day
                                      group tbreserva by tbreserva.FechaReserva.Value.Day into grupo
                                      select new VMReserva
                                      {
                                          FechaReserva = grupo.Key.ToString("yyyy-MM-dd"),
                                          Cantidad = grupo.Count(),
                                      }).ToList();

            return StatusCode(StatusCodes.Status200OK, lista);
        }

        public IActionResult resumenPaquete()
        {
            List<VMPaquete> lista = (from tbdetallerRsPqt in _dbcontext.DetalleReservaPaquetes
                                     join tbpaquete in _dbcontext.Paquetes on tbdetallerRsPqt.Idpaquete equals tbpaquete.Idpaquete
                                     group tbdetallerRsPqt by new { tbdetallerRsPqt.Idpaquete, tbpaquete.NombrePaquete } into grupo
                                     orderby grupo.Count() descending
                                     select new VMPaquete
                                     {
                                         NombrePaquete = grupo.Key.NombrePaquete,
                                         Cantidad = grupo.Count()
                                     }).Take(3).ToList();

            return StatusCode(StatusCodes.Status200OK, lista);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
