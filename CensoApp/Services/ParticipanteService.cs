using AutoMapper;
using CensoApp.Dtos;
using CensoApp.Entities;
using CensoApp.Persistence;
using CensoApp.Services.Contracts;
using Microsoft.EntityFrameworkCore;
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
        public ParticipanteService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

       

        public async Task<List<ParticipanteDto>> GetAll()
        {
            var dbEntities = await _context.Participantes
                .Where(x => x.Status == Entities.Helpers.Status.Active)
                .ToListAsync();

            var dtos = _mapper.Map<List<ParticipanteDto>>(dbEntities);
            return dtos;
        }
        public async Task<ParticipanteDto> Details(int? id)
        {
            try
            {
                var dbEntity = await _context.Participantes.FindAsync(id);
                var dto = _mapper.Map<ParticipanteDto>(dbEntity);
                return dto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ParticipanteUpdateDto> Edit(int? id)
        {
            try
            {
                var dbEntity = await _context.Participantes.SingleAsync(x => x.Id == id);
                var dto = _mapper.Map<ParticipanteUpdateDto>(dbEntity);
                return dto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task EditAsync(ParticipanteUpdateDto model)
        {
            using var Transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var dbEntity = await _context.Participantes.FindAsync(model.Id);
                _mapper.Map(model, dbEntity);
                _context.Entry(dbEntity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                await Transaction.CommitAsync();
            }
            catch (Exception)
            {
                await Transaction.RollbackAsync();
                throw;
            }
        }

        public async Task SaveInformation(ParticipanteCreateDto model)
        {
            using var Transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var dbEntity = _mapper.Map<Participante>(model);
                _context.Add(dbEntity);
                await _context.SaveChangesAsync();
                await Transaction.CommitAsync();
            }
            catch (Exception)
            {
                await Transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> ExistAny(ParticipanteCreateDto model)
        {
            try
            {
                var exist = await _context.Participantes.AnyAsync(x => x.Credencial == model.Credencial);
                return exist;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ParticipanteDto> Delete(int? id)
        {
            try
            {
                var dbEntity = await _context.Participantes.FindAsync(id);
                var dto = _mapper.Map<ParticipanteDto>(dbEntity);
                return dto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task SoftDeleteAsync(int? id)
        {
            using var Transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var dbEntity = await _context.Participantes.FindAsync(id);
                dbEntity.Status = Entities.Helpers.Status.DeActive;
                _context.Entry(dbEntity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
               await Transaction.RollbackAsync();
            }
            catch (Exception)
            {
                await Transaction.RollbackAsync();
                throw;
            }
        }
    }
}
