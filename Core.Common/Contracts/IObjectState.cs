using Core.Common.Enums;

namespace Core.Common.Contracts
{
    public interface IObjectState
    {
        ObjectState EntityState { get; set; }
    }
}
