﻿using AutoMapper;
using CensoApp.Dtos;
using CensoApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CensoApp.AutoMapperConfiguration
{
    public class AutoMapperConfig:Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<ParticipanteCreateDto, Participante>();
        }
    }
}