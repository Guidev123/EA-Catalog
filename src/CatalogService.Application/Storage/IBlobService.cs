namespace CatalogService.Application.Storage
{
    public interface IBlobService
    {
        Task<Guid> UploadAsync(Stream stream, string contentType, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid fileId, CancellationToken cancellationToken = default);
    }
}
