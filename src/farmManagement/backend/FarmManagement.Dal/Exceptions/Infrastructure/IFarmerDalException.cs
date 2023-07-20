namespace FarmManagement.Dal.Exceptions.Infrastructure;

public interface IFarmerDalException
{
    string Message { get; }

    object ToJsonObject();
}