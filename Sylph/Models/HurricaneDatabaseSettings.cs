namespace Sylph.Models
{
    public class HurricaneDatabaseSettings : HurricaneDatabaseSettings.IHurricaneDatabaseSettings
    {
        public string HurricaneCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }


        public interface IHurricaneDatabaseSettings
        {
            string HurricaneCollectionName { get; set; }
            string ConnectionString { get; set; }
            string DatabaseName { get; set; }
        }
    }
}