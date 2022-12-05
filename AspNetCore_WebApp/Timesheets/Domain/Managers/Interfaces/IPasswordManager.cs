namespace Domain.Managers.Interfaces
{
    public interface IPasswordManager
    {
        byte[] CreateSalt();
        byte[] GetPasswordHashed(string password, byte[] salt);
    }
}