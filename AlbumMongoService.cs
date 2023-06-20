using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AlbumService
{
    public class AlbumMongoService : IAlbumService
    {
        public AlbumMongoService()
        {
        }

        public async Task<List<Album>> GetAllAsync()
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
           
            Console.WriteLine(mongoUrl);
            var client = new MongoClient(mongoUrl);
            var db = client.GetDatabase("Albums");
            IMongoCollection<Album> _albums = db.GetCollection<Album>("album");

            var emptyFilter = Builders<Album>.Filter.Empty;

            var cursor = _albums.Find(emptyFilter).CountDocuments();

            if (cursor == 0)
            {
                Console.WriteLine("No records exist in mongodb. We will insert the records");
                _albums.InsertOne(new Album { Genre = "Grunge", Name = "Jar of Files", ReleaseYear = 1994, LabelName = "Columbia Records" });
                _albums.InsertOne(new Album { Genre = "Progressive Rock", Name = "The Dark Side of the Moon", ReleaseYear = 1973, LabelName = "Harvest Records" });
                _albums.InsertOne(new Album { Genre = "Alternative Metal", Name = "Badmotorfinger", ReleaseYear = 1991, LabelName = "A&M Records" });
                _albums.InsertOne(new Album { Genre = "Art Rock", Name = "OK Computer", ReleaseYear = 1997, LabelName = "Parlophone, Capitol" });
                _albums.InsertOne(new Album { Genre = "Art Rock test", Name = "OK Computer test", ReleaseYear = 1997, LabelName = "Parlophone" });
                _albums.InsertOne(new Album { Genre = "Art Rock test A", Name = "OK Computer test B", ReleaseYear = 1997, LabelName = "Parlophone B" });

            }
            else
            {
                Console.WriteLine("Records exist in mongodb");
            }


            var result = _albums.Find(emptyFilter).ToList();

            return result;
        }
    }
}
