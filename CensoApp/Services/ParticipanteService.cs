using AutoMapper;
using CensoApp.Dtos;
using CensoApp.Entities;
using CensoApp.Persistence;
using CensoApp.Services.Contracts;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CensoApp.Services
{
    public class ParticipanteService:IParticipanteService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ParticipanteService> _logger;

        public ParticipanteService(ApplicationDbContext context,
            IMapper mapper,
            ILogger<ParticipanteService>logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<List<ParticipanteDto>> GetAll()
        {
            _logger.LogInformation("Looking for the entities on the db.");
            var dbEntities = await _context.Participantes
                .Where(x => x.Status == Entities.Helpers.Status.Active).OrderByDescending(x=>x.Id)
                .ToListAsync();

            _logger.LogInformation("Mapping the entities to the dtos and returning.");
            var dtos = _mapper.Map<List<ParticipanteDto>>(dbEntities);
            return dtos;
        }
        public async Task<ParticipanteDto> Details(int? id)
        {
            try
            {
                _logger.LogInformation($"Looking for the entity with id: {id} on the db and return it");
                var dbEntity = await _context.Participantes.FindAsync(id);
                var dto = _mapper.Map<ParticipanteDto>(dbEntity);
                return dto;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something wrong happens. {ex.Message}");
                throw;
            }
        }

        public async Task<ParticipanteUpdateDto> Edit(int? id)
        {
            try
            {
                _logger.LogInformation($"Looking for the entity wicth id: {id} on the db and return it");
                var dbEntity = await _context.Participantes.SingleAsync(x => x.Id == id);
                var dto = _mapper.Map<ParticipanteUpdateDto>(dbEntity);
                return dto;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something wrong happens. {ex.Message}");
                throw;
            }
        }

        public async Task EditAsync(ParticipanteUpdateDto model)
        {
            using var Transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _logger.LogInformation($"Looking for the entity wicth id: {model.Id} on the db and mapping it");
                var dbEntity = await _context.Participantes.FindAsync(model.Id);
                _mapper.Map(model, dbEntity);

                _logger.LogInformation("Modifiying the entity and saving changes.");
                _context.Entry(dbEntity).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                _logger.LogInformation("Making a commit to the db.");
                await Transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something wrong happens. {ex.Message}");
                await Transaction.RollbackAsync();
                throw;
            }
        }

        public async Task SaveInformation(ParticipanteCreateDto model)
        {
            using var Transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _logger.LogInformation("Mapping the dto to the dbentity and saving the information on the db.");
                var dbEntity = _mapper.Map<Participante>(model);

                _context.Add(dbEntity);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Making a commit to the db.");
                await Transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something wrong happens. {ex.Message}");
                await Transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> ExistAny(ParticipanteCreateDto model)
        {
            try
            {
                _logger.LogInformation($"Determinating if the credencial {model.Credencial} exist.");
                var exist = await _context.Participantes.AnyAsync(x => x.Credencial == model.Credencial);
                return exist;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something wrong happens. {ex.Message}");
                throw;
            }
        }

        public async Task<ParticipanteDto> Delete(int? id)
        {
            try
            {
                _logger.LogInformation($"Looking the id: {id} on the db and returning it.");
                var dbEntity = await _context.Participantes.FindAsync(id);
                var dto = _mapper.Map<ParticipanteDto>(dbEntity);
                return dto;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something wrong happens. {ex.Message}");
                throw;
            }
        }

        public async Task SoftDeleteAsync(int? id)
        {
            using var Transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _logger.LogInformation($"Looking the id: {id} on the db and adding the softdelete.");
                var dbEntity = await _context.Participantes.FindAsync(id);
                dbEntity.Status = Entities.Helpers.Status.DeActive;

                _context.Entry(dbEntity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
               await Transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something wrong happens. {ex.Message}");
                await Transaction.RollbackAsync();
                throw;
            }
        }
        public int AgeCalculation(ParticipanteCreateDto model)
        {
            var fechaActual = Convert.ToInt32(DateTime.Now.Year);
            var fechaNacimiento = Convert.ToInt32(model.FechaNacimiento.Year);
            int edad = Convert.ToInt32(fechaActual - fechaNacimiento);
            return edad;
        }

        public string RolePreAsigned(ParticipanteCreateDto model)
        {
            if (model.Edad >= 18 && model.NivelAcademico == "Bachiller") { model.CargoPreasignado = "Empadronador(ra)"; }
            else if (model.Edad >= 18 && model.NivelAcademico == "Universitario") { model.CargoPreasignado = "Supervisor(ra)"; }
            if (model.Edad >= 18 && model.NivelAcademico == "Superior") { model.CargoPreasignado = "Coordinador(ra)"; }

            return model.CargoPreasignado;
        }

        public async Task<List<SelectListItem>> SelectProvincia()
        {
            var dbEntities = await _context.Provincias.OrderBy(x => x.Id).ToListAsync();
            var dtos = _mapper.Map<List<ProvinciaDto>>(dbEntities);
            var items = new List<SelectListItem>();

            items = dtos.ConvertAll(x =>
            {
                return new SelectListItem()
                {
                    Text = x.Name.ToString(),
                    Value = x.Name.ToString(),
                    Selected = false
                };
            });

            return items;
        }

        public async Task<List<SelectListItem>> SelectMunicipio()
        {
            var dbEntities = await _context.Municipios.ToListAsync();
            var dtos = _mapper.Map<List<MunicipioDto>>(dbEntities);
            var items = new List<SelectListItem>();
            items = dtos.ConvertAll(x =>
            {
                return new SelectListItem()
                {
                    Text = x.Name.ToString(),
                    Value = x.Name.ToString(),
                    Selected = false
                };
            });

            return items;
        }

    }
}
