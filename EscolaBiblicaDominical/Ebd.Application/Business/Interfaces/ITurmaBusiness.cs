﻿using Ebd.Application.Responses.Turma;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ebd.Application.Business.Interfaces
{
    public interface ITurmaBusiness
    {
        Task<IEnumerable<TurmaResponse>> ObterTodas();
    }
}