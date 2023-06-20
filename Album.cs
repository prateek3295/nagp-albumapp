using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AlbumService
{
    public class Album
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public int ReleaseYear { get; set; }
        public string LabelName { get; set; }

    }
}
