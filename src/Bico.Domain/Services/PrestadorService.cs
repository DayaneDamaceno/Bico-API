using Bico.Domain.Entities;
using Bico.Domain.Interfaces;

namespace Bico.Domain.Services;

public class PrestadorService : IPrestadorService
{
    private readonly IPrestadorRepository _prestadorRepository;
    private readonly IAvatarRepository _avatarRepository;

    public PrestadorService(IPrestadorRepository prestadorRepository, IAvatarRepository avatarRepository)
    {
        _prestadorRepository = prestadorRepository;
        _avatarRepository = avatarRepository;
    }

    public async Task<List<Prestador>> ObterPrestadoresMaisProximosAsync(int clientId, int habilidadeId)
    {
        var prestadores = await _prestadorRepository.ObterPrestadoresMaisProximosAsync(clientId, habilidadeId);

        prestadores.ForEach(prestador =>
        {
            prestador.AvatarUrl = _avatarRepository.GerarAvatarUrlSegura(prestador.AvatarFileName);
        });

        return prestadores;
    }
}
