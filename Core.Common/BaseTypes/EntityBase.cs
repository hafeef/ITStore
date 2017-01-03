using Core.Common.Contracts;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Common.Enums;

namespace Core.Common.BaseTypes
{
    public abstract class EntityBase : IObjectState
    {
        [Column(TypeName = "datetime2")]
        public DateTime CreatedDateTime { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime ModifiedDateTime { get; set; }

        public bool IsActive { get; set; } = true;

        [NotMapped]
        public ObjectState EntityState { get; set; }
    }
}
