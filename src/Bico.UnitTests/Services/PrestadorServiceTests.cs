using AutoFixture;
using Bico.Domain.Interfaces;
using Bico.Domain.Services;
using Bico.Domain.ValueObjects;
using Moq;

namespace Bico.UnitTests.Services;

public class PrestadorServiceTests
{
    private readonly Mock<IPrestadorRepository> _mockPrestadorRepository;
    private readonly Mock<IAvatarRepository> _mockAvatarRepository;
    private readonly IPrestadorService _prestadorService;
    private readonly IFixture _fixture;

    public PrestadorServiceTests()
    {
        _fixture = new Fixture();
        _mockPrestadorRepository = new Mock<IPrestadorRepository>();
        _mockAvatarRepository = new Mock<IAvatarRepository>();
        _prestadorService = new PrestadorService(_mockPrestadorRepository.Object, _mockAvatarRepository.Object);

    }

    [Fact]
    public async Task ObterPrestadoresMaisProximosAsync_DeveRetorarListaVazia_QuandoNaoEncontrarPrestadores()
    {
        //arrange
        _mockPrestadorRepository.Setup(repo => repo.ObterPrestadoresMaisProximosAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Paginacao>()))
                                .ReturnsAsync(() => null);

        _mockAvatarRepository.Setup(repo => repo.GerarAvatarUrlSegura(It.IsAny<string>()))
                             .Returns((string filename) => $"http://test.com/{filename}");


        //act
        var prestadores = await _prestadorService.ObterPrestadoresMaisProximosAsync(clientId: 1, habilidadeId: 1, pagina: 1);

        //assert
        Assert.Empty(prestadores);
    }
}