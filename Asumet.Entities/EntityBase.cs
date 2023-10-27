using System.ComponentModel.DataAnnotations;

namespace Asumet.Entities
{
    public class EntityBase<TKey>
    {
        [Key]
        public virtual TKey Id { get; set; }
    }
}