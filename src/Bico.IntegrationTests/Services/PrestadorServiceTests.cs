using AutoFixture;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Bico.Domain.Entities;
using Bico.Domain.Interfaces;
using Bico.Domain.Services;
using Bico.Infra.DBContext;
using Bico.Infra.Repositories;
using Bico.IntegrationTests.Database;
using Bico.IntegrationTests.FixtureCustomizations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;
using NetTopologySuite.Geometries;

namespace Bico.IntegrationTests.Services;

public class PrestadorServiceTests
{
    private readonly DatabaseFixture _dbFixture;
    private readonly PrestadorService _prestadorService;
    private readonly Mock<IAvatarRepository> _mockAvatarRepository;
    private readonly IFixture _fixture;


    public PrestadorServiceTests()
    {
        _fixture = new Fixture();
        _fixture.Customize(new PointCustomization());

        _dbFixture = new DatabaseFixture();

        _mockAvatarRepository = new Mock<IAvatarRepository>();
        _prestadorService = new PrestadorService(new PrestadorRepository(_dbFixture.Context), _mockAvatarRepository.Object);
    }

    [Fact]
    public async Task ObterPrestadoresMaisProximosAsync_DeveRetornarPrestadoresProximosDeUmCliente()
    {
        //arrange
        await CriarCenarioComDoisPrestadoresEUmCliente();
        _mockAvatarRepository.Setup(repo => repo.GerarAvatarUrlSegura(It.IsAny<string>()))
                             .Returns((string filename) => $"http://test.com/{filename}");


        //act
        var prestadores = await _prestadorService.ObterPrestadoresMaisProximosAsync(clientId: 1, habilidadeId: 1, pagina: 1);

        //assert
        Assert.NotEmpty(prestadores);
        Assert.Single(prestadores);
        Assert.Equal(1, prestadores.First().Id);
    }

    private async Task CriarCenarioComDoisPrestadoresEUmCliente()
    {
        var categoria = _fixture.Build<Categoria>()
             .With(p => p.Nome, "Categoria").Create();

        var habilidade = _fixture.Build<Habilidade>()
            .With(p => p.Id, 1)
            .With(h => h.CategoriaId, categoria.Id)
            .Without(h => h.Prestadores)
            .With(p => p.Nome, "habilidade")
            .Without(h => h.Categoria)
            .Create();

        var prestadorSantoAndre = _fixture.Build<Prestador>()
            .With(p => p.Id, 1)
            .With(p => p.Habilidades, new List<Habilidade> { habilidade })
            .With(p => p.RaioDeAlcance, 5000)
             .With(p => p.Nome, "prestadorSantoAndre")
            .With(p => p.Localizacao, new Point(-46.53343257495357, -23.667905555073688) { SRID = 4326 })
            .Without(p => p.Avaliacoes)
            .Create();

        var prestadorMaua = _fixture.Build<Prestador>()
             .With(p => p.Nome, "prestadorMaua")
            .With(p => p.Habilidades, new List<Habilidade> { habilidade })
            .With(p => p.RaioDeAlcance, 5000)
            .With(p => p.Localizacao, new Point(-46.46253643299812, -23.663188794269047) { SRID = 4326 })
            .Without(p => p.Avaliacoes)
            .Create();

        var clienteSaoBernardo = _fixture.Build<Cliente>()
            .With(p => p.Id, 1)
            .With(p => p.Nome, "Cliente")
            .With(p => p.Localizacao, new Point(-46.54270228842959, -23.69777442035469) { SRID = 4326 })
            .Create();

        await _dbFixture.Context.Categorias.AddAsync(categoria);
        await _dbFixture.Context.Habilidades.AddAsync(habilidade);
        await _dbFixture.Context.Prestadores.AddAsync(prestadorSantoAndre);
        await _dbFixture.Context.Prestadores.AddAsync(prestadorMaua);
        await _dbFixture.Context.Clientes.AddAsync(clienteSaoBernardo);
          
        await _dbFixture.Context.SaveChangesAsync();
    }

}
