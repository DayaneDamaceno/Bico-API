using Bico.Domain.Entities;
using Bico.Domain.Interfaces;
using Bico.Domain.ValueObjects;
using Bico.Infra.DBContext;
using Bico.Infra.Extensions;
using Microsoft.EntityFrameworkCore;
using System;

namespace Bico.Infra.Repositories;

public class PrestadorRepository : IPrestadorRepository
{
    private readonly BicoContext _context;

    public PrestadorRepository(BicoContext bicoContext)
    {
        _context = bicoContext;
    }

    public async Task<List<Prestador>> ObterPrestador(int prestadorId)
    {

        var p = await _context.Prestadores.Where(x => x.Id.Equals(prestadorId)).ToListAsync();
        var ph = await _context.PrestadoresHabilidades.Where(ph => ph.PrestadorId.Equals(prestadorId)).ToListAsync();

        if (p.LastOrDefault() != null && ph.LastOrDefault() != null)
        {
            p.LastOrDefault().FotosServico = await _context.FotosServicos.Where(fs => fs.PrestadorId.Equals(prestadorId)).ToListAsync(); ;
            p.LastOrDefault().Habilidades = new List<Habilidade>();
            foreach (var habilidade in ph)
            {
                var x = _context.Habilidades.Where(h => h.Id.Equals(habilidade.HabilidadeId)).ToList();
                if (x.LastOrDefault() != null)
                {
                    p.LastOrDefault().Habilidades.Add(x.LastOrDefault());
                }
            }
        }

        return p;
    }

    public async Task<List<Prestador>> ObterPrestadoresMaisProximosAsync(int clienteId, int habilidadeId, Paginacao paginacao)
    {
        bool contemHabilidade = false;
        var localizacaoDoCliente = await _context.Clientes
                             .Where(c => c.Id == clienteId)
                             .Select(c => c.Localizacao)
                             .FirstOrDefaultAsync();

        var prestadoresProximos = _context.Prestadores
            .Where(p => p.Localizacao.StDWithin(localizacaoDoCliente, p.RaioDeAlcance, true))
            .OrderBy(p => p.Localizacao.StDistance(localizacaoDoCliente, true))
            .Select(p => new Prestador
            {
                Id = p.Id,
                Nome = p.Nome,
                AvatarFileName = p.AvatarFileName,
                Avaliacoes = p.Avaliacoes,
                MediaEstrelas = p.Avaliacoes.Any() ? p.Avaliacoes.Average(a => (double)a.QuantidadeEstrelas) : 0,
            })
            .Skip(paginacao.ObterSkip())
            .Take(paginacao.QuantidadeDeItens)
            .ToList();
        var prestadoresProximosNovo = new List<Prestador>();

        foreach (var p in prestadoresProximos)
        {
            var ph = await _context.PrestadoresHabilidades.Where(ph => ph.PrestadorId.Equals(p.Id)).ToListAsync();
            p.FotosServico = await _context.FotosServicos.Where(fs => fs.PrestadorId.Equals(p.Id)).ToListAsync(); ;
            p.Habilidades = new List<Habilidade>();
            foreach (var habilidade in ph)
            {
                var x = _context.Habilidades.Where(h => h.Id.Equals(habilidade.HabilidadeId)).ToList();
                if (x.LastOrDefault() != null)
                {
                    p.Habilidades.Add(x.LastOrDefault());
                }
                if (x.LastOrDefault().Id == habilidadeId)
                    contemHabilidade = true;
            }
            if (contemHabilidade)
                prestadoresProximosNovo.Add(p);
            contemHabilidade = false;
        }

        return prestadoresProximosNovo;
    }
}
