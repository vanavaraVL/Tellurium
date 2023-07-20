using FarmManagement.Dal.Exceptions.Infrastructure;

namespace FarmManagement.Dal.Exceptions;

public sealed class EntityUniqueException : Exception, IFarmerDalException
{
    public EntityUniqueException(string uniqueName, Exception? innerException = null) : base($"The name {uniqueName} should be unique", innerException)
    {

    }

    public object ToJsonObject()
    {
        return new { Message, InnerException };
    }
}