namespace Bico.Domain.Interfaces
{
    public interface IAvatarRepository
    {
        string GerarAvatarUrlSegura(string blobName);
    }
}