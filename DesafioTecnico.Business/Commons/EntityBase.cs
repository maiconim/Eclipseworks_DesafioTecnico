namespace DesafioTecnico.Business.Commons
{
    public abstract class EntityBase
    {
        public Guid Id { get; protected set; } = Guid.Empty;

        internal IDictionary<string, ChangesInfo> Changes = new Dictionary<string, ChangesInfo>();
    }
}