namespace Library_Api.Models
{
    public interface ILibraryDatabaseSettings
    {

        string ConnectionString { get; set; }

        string DatabaseName { get; set; }

        string LibraryCollectionName { get; set; }
    }
}
