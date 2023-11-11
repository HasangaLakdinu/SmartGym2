namespace AuthService.Services
{
    public interface ITokenService
    {
        string BuildToken(IConfiguration configuration, string userName, int branchId);
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}
