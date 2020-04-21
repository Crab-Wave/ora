namespace ORA.API.Encryption
{
    /// <summary>
    ///     An object capable of Encrypting data or Decrypting data that has been previously encrypted with the same algorithm.
    /// </summary>
    /// <example>
    ///     The following code
    ///     <code>
    ///         ICipher cipher = ...;
    ///         byte[] data = ...;
    ///         byte[] encrypted = cipher.Encrypt(data);
    ///
    ///         Console.WriteLine(cipher.Decrypt(encrypted).Equals(data));
    ///     </code>
    ///     will print the value true.
    /// </example>
    public interface ICipher
    {
        /// <summary>
        ///     Encrypts the given data following a specific algorithm.
        /// </summary>
        /// <param name="data">The data to encrypt</param>
        /// <returns>The encrypted data</returns>
        byte[] Encrypt(byte[] data);

        /// <summary>
        ///     Decrypts the given data if it has previously been encrypted using the same algorithm.
        /// </summary>
        /// <param name="data">The data to decrypt</param>
        /// <returns>The Decrypted data</returns>
        byte[] Decrypt(byte[] data);
    }
}
