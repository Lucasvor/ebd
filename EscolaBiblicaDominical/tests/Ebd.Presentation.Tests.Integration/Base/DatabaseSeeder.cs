using Ebd.Domain.Core.Entities;
using Ebd.Infra.Data;
using Ebd.Presentation.Tests.Integration.Setup;

namespace Ebd.Presentation.Tests.Integration.Base;

public class DatabaseSeeder
{
    private readonly MainContext Context = null!;

    public DatabaseSeeder(MainContext context)
    {
        Context = context;
    }

    public async Task InserirUsuarios()
    {
        var bairros = new List<Bairro>
        {
            new Bairro
            {
                BairroId = Constants.BairroIdPacaembu,
                Nome = "Pacaembu"
            },
                new Bairro
            {
                BairroId = Constants.BairroIdTibery,
                Nome = "Tibery"
            }
        };
        await Context.Bairros.AddRangeAsync(bairros);

        var turmas = new List<Turma>
        {
            new Turma(nome:Constants.Turma.TurmaNomeMensageiros, Constants.Turma.TurmaIdadeMinimaMensageiros, idadeMaxima: Constants.Turma.TurmaIdadeMaximaMensageiros),
            new Turma(nome:Constants.Turma.TurmaNomeVencedores, Constants.Turma.TurmaIdadeMinimaVencedores, idadeMaxima: Constants.Turma.TurmaIdadeMaximaVencedores),
            new Turma(nome:Constants.Turma.TurmaNomeEsqueci, Constants.Turma.TurmaIdadeMinimaEsqueci, idadeMaxima: Constants.Turma.TurmaIdadeMaximaEsqueci)
        };
        await Context.Turmas.AddRangeAsync(turmas);

        await Context.SaveChangesAsync();
    }
}
