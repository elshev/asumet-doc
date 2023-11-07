using System.ComponentModel.DataAnnotations;

namespace Asumet.Entities
{
    public class EntityBase<TKey>
        where TKey : unmanaged
    {
        [Key]
        public virtual TKey Id { get; set; }
    }
}