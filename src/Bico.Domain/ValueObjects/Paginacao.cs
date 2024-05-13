namespace Bico.Domain.ValueObjects;

public class Paginacao
{
    public Paginacao(int pagina, int quantidadeDeItens = 10)
    {
        Pagina = pagina == 0 ? 0 : pagina - 1;
        QuantidadeDeItens = quantidadeDeItens;
    }

    private int Pagina { get; }
    public int QuantidadeDeItens { get; }

    public int ObterSkip()
    {
        return Pagina * QuantidadeDeItens;
    }

}
