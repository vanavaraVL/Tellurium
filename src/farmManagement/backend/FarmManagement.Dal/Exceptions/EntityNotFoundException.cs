using FarmManagement.Dal.Exceptions.Infrastructure;

namespace FarmManagement.Dal.Exceptions;

public sealed class EntityNotFoundException : Exception, IFarmerDalException
{
    public EntityNotFoundException(string name, Exception? innerException = null) : base($"Entity with {name} doesn't exist", innerException)
    {

    }

    public object ToJsonObject()
    {
        return new { Message, InnerException };
    }
}