namespace Warehouse.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class EntityBase
    {
        // This is the base class for all entities.
        // The DataAccess repositories have this class as constraint for entities that are persisted in the database.

        [Key]
        public long Id { get; set; }
    }
}