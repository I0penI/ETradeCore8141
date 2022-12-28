namespace AppCore.Records.Bases
{
    public abstract class RecordBase
    {
        public int Id { get; set; } // public int? Id { get; set; } = null; (int i null kullanabilmek için)
        public string Guid { get; set; } 

    }
}
