using Microsoft.AspNetCore.Mvc;

namespace AlbumService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        public IAlbumService albumService { get; set; }
        public AlbumController(IAlbumService albumService)
        {
            this.albumService = albumService;
        }

        [Route("GetAll")]
        public Task<List<Album>> GetAlbums()
        {
            var albums = albumService.GetAllAsync();

            return albums;
        }
    }
}
