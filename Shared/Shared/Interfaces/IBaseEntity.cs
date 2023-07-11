namespace Shared.Interfaces
{
    public interface IBaseEntity
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
    }
}
