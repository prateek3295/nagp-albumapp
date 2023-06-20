namespace AlbumService
{
    public class MongoSettings
    {

        public int Port { get; set; }

        public string MongoUserName { get; set; }
        public string MongoUserPassword{ get; set; }
        public string MongoHostName { get; set; }
        
        public string MongoDatabaseName { get; set; }

        public string MongoCollectionName { get; set; }

    }
}
