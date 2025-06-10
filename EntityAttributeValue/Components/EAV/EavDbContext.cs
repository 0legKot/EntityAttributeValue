using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EntityAttributeValue.Components.EAV {
    public class EavDbContext : DbContext {
        public EavDbContext(DbContextOptions<EavDbContext> options)
            : base(options) { }

        public DbSet<Entity> Entities { get; set; }

        public DbSet<AttributeDefinitionString> AttributesString { get; set; }
        public DbSet<AttributeDefinitionInt> AttributesInt { get; set; }

        public DbSet<EntityAttributeValueString> EntityAttributeValuesString { get; set; }
        public DbSet<EntityAttributeValueInt> EntityAttributeValuesInt { get; set; }
    }

    public class Entity {
        [Key]
        public int Id { get; set; }

        public required string Name { get; set; }
        public ICollection<EntityAttributeValueString> ValuesText { get; set; } = [];
        public ICollection<EntityAttributeValueInt> ValuesInt { get; set; } = [];
    }

    public class AttributeDefinitionString {
        [Key]
        public int Id { get; set; }

        public required string Name { get; set; }
        public ICollection<EntityAttributeValueString> Values { get; set; } = [];
    }
    public class AttributeDefinitionInt {
        [Key]
        public int Id { get; set; }

        public required string Name { get; set; }
        public ICollection<EntityAttributeValueInt> Values { get; set; } = [];
    }

    public class EntityAttributeValueString {
        [Key]
        public int Id { get; set; }

        public int EntityId { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public Entity Entity { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        public int AttributeId { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public AttributeDefinitionString Attribute { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        public string? Value { get; set; }
    }
    public class EntityAttributeValueInt {
        [Key]
        public int Id { get; set; }

        public int EntityId { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public Entity Entity { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        public int AttributeId { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public AttributeDefinitionInt Attribute { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        public int? Value { get; set; }
    }
}
