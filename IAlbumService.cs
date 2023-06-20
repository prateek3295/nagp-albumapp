namespace AlbumService
{
    public interface IAlbumService
    {            
        Task<List<Album>> GetAllAsync();
    }
}
