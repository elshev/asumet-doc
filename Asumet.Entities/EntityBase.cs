using System.ComponentModel.DataAnnotations;

namespace Asumet.Entities
{
    public class EntityBase<TKey>
    {
        [Key]
        public TKey Id { get; set; }
    }
}