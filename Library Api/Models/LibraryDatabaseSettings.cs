namespace Library_Api.Models
{
    public class LibraryDatabaseSettings : ILibraryDatabaseSettings
    {

        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string LibraryCollectionName { get; set; } = null!;

    }
}
