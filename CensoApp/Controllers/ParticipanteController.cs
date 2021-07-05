using AutoMapper;
using CensoApp.Dtos;
using CensoApp.Entities;
using CensoApp.Persistence;
using CensoApp.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CensoApp.Controllers
{
    public class ParticipanteController : Controller
    {
        private readonly IParticipanteService _participanteService;
     
        public ParticipanteController(IParticipanteService participanteService)
        {
            _participanteService = participanteService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult<List<ParticipanteDto>>> GetAll(ParticipanteDto model)
        {
            var dtos = await _participanteService.GetAll();
            return View(dtos);
        }
        public async Task<IActionResult> Details(int? Id) 
        {
            if (Id == null) { return NotFound(); }
            var dto = await _participanteService.Details(Id);

            return View(dto);
        }
        public async Task<IActionResult> Edit(int? Id)
        {

            if (Id == null) { return NotFound(); }

            var dto = await _participanteService.Edit(Id);
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ParticipanteUpdateDto model) 
        {

            if (ModelState.IsValid) 
            {
                await _participanteService.EditAsync(model);
                return RedirectToAction(nameof(GetAll));
            }

            return View(model);
        }

        public IActionResult DatosPersonales(ParticipanteCreateDto model)
        {
            if (ModelState.IsValid)
            {
                return View(model);
            }
            return RedirectToAction(nameof(Index));
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

                model.CargoPreasignado = AsignarCargo(model);

                model.FechaSolicitud = Convert.ToDateTime(DateTime.Now.ToShortDateString());

                if (ModelState.IsValid)
                {
                    await _participanteService.SaveInformation(model);
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
            var exist = await _participanteService.ExistAny(model);

            if (exist == true)
            {
                return RedirectToAction(nameof(NoSePuedeInsertarCedulasRepetidas));
            }

            return RedirectToAction(nameof(DatosPersonales), model);
        }

        [HttpPost]
        public IActionResult SaveInformation(ParticipanteCreateDto model)
        {
                if (ModelState.IsValid) 
                {
                    return RedirectToAction(nameof(FormacionEducativa), model);
                }
                return RedirectToAction(nameof(DatosPersonales), model);
        }
        

        public IActionResult Confirmation()
        {
            return View();
        }
      
        public async Task<IActionResult> Delete(int? Id)
        {
            
            if (Id == null) { return NotFound(); }
            var dto = await _participanteService.Delete(Id);
            return View(dto);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteSuccess(int?Id) 
        {
            

            if (ModelState.IsValid) 
            {
                await _participanteService.SoftDeleteAsync(Id);
                return RedirectToAction(nameof(GetAll));
            }
            return RedirectToAction(nameof(Delete));
        }
        #region Methods
        private int calcularEdad(ParticipanteCreateDto model)
        {
            var fechaActual = Convert.ToInt32(DateTime.Now.Year);
            var fechaNacimiento = Convert.ToInt32(model.FechaNacimiento.Year);
            int edad = Convert.ToInt32(fechaActual - fechaNacimiento);
            return edad;
        }
        private string AsignarCargo(ParticipanteCreateDto model)
        {
            if (model.Edad >= 18 && model.NivelAcademico == "Bachiller") { model.CargoPreasignado = "Empadronador(ra)"; }
            else if (model.Edad >= 18 && model.NivelAcademico == "Universitario") { model.CargoPreasignado = "Supervisor(ra)"; }
            if (model.Edad >= 18 && model.NivelAcademico == "Superior") { model.CargoPreasignado = "Coordinador(ra)"; }

            return model.CargoPreasignado;
        }
        #endregion
    }
}
