using System;

namespace IO.Swagger.Services
{
    public interface ICryptoService
    {
        byte[] Encrypt(string text);
        
        string Decrypt(byte[] cipherText);
        
    }
}