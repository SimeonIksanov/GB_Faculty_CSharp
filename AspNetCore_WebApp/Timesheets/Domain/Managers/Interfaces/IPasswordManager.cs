namespace Domain.Managers.Interfaces
{
    public interface IPasswordManager
    {
        /// <summary>
        /// Creates a salt - random sequance to change a password hash
        /// </summary>
        /// <returns></returns>
        byte[] CreateSalt();

        /// <summary>
        /// Hashes a clear text password
        /// </summary>
        /// <param name="password">Password in clear text</param>
        /// <param name="salt">salt</param>
        /// <returns></returns>
        byte[] GetPasswordHashed(string password, byte[] salt);
    }
}