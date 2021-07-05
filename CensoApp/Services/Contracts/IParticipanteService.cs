using CensoApp.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CensoApp.Services.Contracts
{
    public interface IParticipanteService
    {
        Task<List<ParticipanteDto>> GetAll();
        Task<ParticipanteDto> Details(int?id);
        Task<ParticipanteUpdateDto> Edit(int? id);
        Task EditAsync(ParticipanteUpdateDto model);
        Task SaveInformation(ParticipanteCreateDto model);
        Task<bool> ExistAny(ParticipanteCreateDto model);
        Task<ParticipanteDto> Delete(int? id);
        Task SoftDeleteAsync(int? id);
    }
}
