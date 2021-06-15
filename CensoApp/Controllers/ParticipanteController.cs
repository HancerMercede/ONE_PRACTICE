using AutoMapper;
using CensoApp.Dtos;
using CensoApp.Entities;
using CensoApp.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CensoApp.Controllers
{
    public class ParticipanteController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ParticipanteController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DatosPersonales(ParticipanteCreateDto model)
        {
            return View(model);
        }
        public IActionResult FormacionEducativa(ParticipanteCreateDto model) 
        {
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> GuardarInformacionParticipante(ParticipanteCreateDto model) {
            try
            {
                model.Edad = calcularEdad(model);
                var dbEntity = _mapper.Map<Participante>(model);
                if (ModelState.IsValid) 
                {

                    _context.Add(dbEntity);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index),model);
                }

                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        
        }
        public IActionResult NoSePuedeInsertarCedulasRepetidas()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveCredential(ParticipanteCreateDto model) 
        {
            var exist = await _context.Participantes.AnyAsync(x => x.Credencial == model.Credencial);

            var dbEntity = _mapper.Map<Participante>(model);
            if (exist == true)
            {
                return RedirectToAction(nameof(NoSePuedeInsertarCedulasRepetidas));
            }

            if (ModelState.IsValid) 
            {
                return RedirectToAction(nameof(DatosPersonales), model);
            }
            return View();
        }
        [HttpPost]
        public IActionResult SaveInformation(ParticipanteCreateDto model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(FormacionEducativa), model);
            }
            return View();
        }
        private int calcularEdad(ParticipanteCreateDto model) 
        {
            var fechaActual = DateTime.Now.Year;
            var fechaNacimiento = model.FechaNacimiento.Year;
            int edad =  Convert.ToInt32(fechaActual - fechaNacimiento);
            return edad;
        }
    }
}
