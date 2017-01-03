using Core.Common.Contracts;
using System;
using Core.Common.Enums;

namespace Core.Common.BaseTypes
{
    [Serializable]
    public abstract class EntityBaseVM : IObjectState
    {
        public bool IsActive { get; set; } = true;
        public ObjectState EntityState { get; set; }
    }
}
