using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AlbumService
{
    public class AlbumMongoService : IAlbumService
    {
        private readonly IMongoCollection<Album> _albums;

        public AlbumMongoService()
        {
            MongoSettings mongoSettings = new MongoSettings
            {
                MongoUserName = Environment.GetEnvironmentVariable("MongoUserName"),
                MongoUserPassword = Environment.GetEnvironmentVariable("MongoUserPassword"),
                MongoHostName = Environment.GetEnvironmentVariable("MongoHostName"),
                Port = Convert.ToInt32(Environment.GetEnvironmentVariable("Port")),
                MongoDatabaseName = "Albums"
            };

            string mongoUrl = $"mongodb://{mongoSettings.MongoUserName}:{mongoSettings.MongoUserPassword}@{mongoSettings.MongoHostName}:{mongoSettings.Port}/{mongoSettings.MongoDatabaseName}?authSource=admin";
            Console.Write(mongoUrl);
            var client = new MongoClient(mongoUrl);
            var db = client.GetDatabase("Albums");
            _albums = db.GetCollection<Album>("album");
            var filter = Builders<Album>.Filter.Empty;

            var cursor = _albums.Find(filter);

            if (!cursor.Any())
            {
                _albums.InsertOne(new Album { Genre = "Grunge", Name = "Jar of Files", ReleaseYear = 1994, LabelName= "Columbia Records" });
                _albums.InsertOne(new Album { Genre = "Progressive Rock", Name = "The Dark Side of the Moon", ReleaseYear = 1973, LabelName = "Harvest Records" });
                _albums.InsertOne(new Album { Genre = "Alternative Metal", Name = "Badmotorfinger", ReleaseYear = 1991, LabelName = "A&M Records" });
                _albums.InsertOne(new Album { Genre = "Art Rock", Name = "OK Computer", ReleaseYear = 1997, LabelName = "Parlophone, Capitol" });

            }
        }

        public async Task<List<Album>> GetAllAsync()
        {
            // Define a filter to check if any documents exist in the collection
            var filter = Builders<Album>.Filter.Empty;

            var result = _albums.Find(filter).ToList();

            return result;
        }
    }
}
