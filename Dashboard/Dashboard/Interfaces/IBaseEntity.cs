using System.ComponentModel.DataAnnotations.Schema;

namespace Dashboard.Interfaces
{
    public interface IBaseEntity
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
    }
}
