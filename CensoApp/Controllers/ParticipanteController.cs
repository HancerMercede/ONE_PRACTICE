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
        public async Task<ActionResult<List<ParticipanteDto>>> GetAll(ParticipanteDto model)
        {
            var dbEntities = await _context.Participantes.Where(x=>x.Status==Entities.Helpers.Status.Active).ToListAsync();

            var dtos = _mapper.Map<List<ParticipanteDto>>(dbEntities);
            return View(dtos);
        }
        public async Task<IActionResult> Details(int? Id) 
        {
            var dbEntity = await _context.Participantes.FindAsync(Id);

            if (Id == null) { return NotFound(); }

            var dto = _mapper.Map<ParticipanteDto>(dbEntity);

            return View(dto);
        }
        public async Task<IActionResult> Edit(int? Id)
        {
            var dbEntity = await _context.Participantes.SingleAsync(x => x.Id == Id);

            if (Id == null) { return NotFound(); }

            var dto = _mapper.Map<ParticipanteUpdateDto>(dbEntity);
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ParticipanteUpdateDto model) 
        {
            var dbEntity = await _context.Participantes.FindAsync(model.Id);

            if (ModelState.IsValid) 
            {
                _mapper.Map(model, dbEntity);
                _context.Entry(dbEntity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(GetAll));
            }

            return View(model);
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

                if (model.Edad >= 18 && model.NivelAcademico == "Bachiller") { model.CargoPreasignado = "Empadronador(ra)"; }
                else if (model.Edad >= 18 && model.NivelAcademico == "Universitario") { model.CargoPreasignado = "Supervisor(ra)"; }
                if (model.Edad >= 18 && model.NivelAcademico == "Superior") { model.CargoPreasignado = "Coordinador(ra)"; }

                model.FechaSolicitud = Convert.ToDateTime(DateTime.Now.ToShortDateString());

                var dbEntity = _mapper.Map<Participante>(model);

                if (ModelState.IsValid)
                {
                    _context.Add(dbEntity);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Confirmation), model);
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

            if (exist == true)
            {
                return RedirectToAction(nameof(NoSePuedeInsertarCedulasRepetidas));
            }

            return RedirectToAction(nameof(DatosPersonales), model);
        }

        [HttpPost]
        public IActionResult SaveInformation(ParticipanteCreateDto model)
        {
            try
            {
                return RedirectToAction(nameof(FormacionEducativa), model);
            }
            catch
            {
                return RedirectToAction(nameof(DatosPersonales), model);
            } 
        }
        private int calcularEdad(ParticipanteCreateDto model)
        {
            var fechaActual = DateTime.Now.Year;
            var fechaNacimiento = model.FechaNacimiento.Year;
            int edad = Convert.ToInt32(fechaActual - fechaNacimiento);
            return edad;
        }

        public IActionResult Confirmation()
        {
            return View();
        }

        public async Task<IActionResult> Delete(int? Id)
        {
            var dbEntity = await _context.Participantes.FindAsync(Id);
            if (Id == null) { return NotFound(); }
            var dto = _mapper.Map<ParticipanteDto>(dbEntity);
            return View(dto);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteSuccess(int?Id) 
        {
            var dbEntity = await _context.Participantes.FindAsync(Id);
            dbEntity.Status = Entities.Helpers.Status.DeActive;

            if (ModelState.IsValid) 
            {
                _context.Entry(dbEntity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(GetAll));
            }
            return RedirectToAction(nameof(Delete));
        }
    }
}
