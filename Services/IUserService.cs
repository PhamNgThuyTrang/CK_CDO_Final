namespace CK_CDO_Final.Services
{
    public interface IUserService
    {
        string GetUserId();
        bool IsAuthenticated();
    }
    
}