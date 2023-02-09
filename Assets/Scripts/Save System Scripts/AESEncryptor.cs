using System;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;

public class AESEncryptor
{
    private static readonly byte[] Salt = new byte[] { 0x26, 0xdc, 0xff, 0x00, 0xad, 0xed, 0x7a, 0xee, 0xc5, 0xfe, 0x07, 0xaf, 0x4d, 0x08, 0x22, 0x3c };
    private const int Iterations = 1000;

    public static void Encrypt(string plainText, string password, string outFilePath)
    {
        var key = new Rfc2898DeriveBytes(password, Salt, Iterations);

        // Set up the encryption objects
        var algorithm = Rijndael.Create();
        var encryptor = algorithm.CreateEncryptor(key.GetBytes(32), key.GetBytes(16));
        var fsEncrypt = new FileStream(outFilePath, FileMode.Create);
        var cryptoStream = new CryptoStream(fsEncrypt, encryptor, CryptoStreamMode.Write);

        // Write the encrypted data to the output file
        using (var swEncrypt = new StreamWriter(cryptoStream))
        {
            swEncrypt.Write(plainText);
        }
    }

    public static string Decrypt(string inFilePath, string password)
    {
        var key = new Rfc2898DeriveBytes(password, Salt, Iterations);

        // Set up the decryption objects
        var algorithm = Rijndael.Create();
        var decryptor = algorithm.CreateDecryptor(key.GetBytes(32), key.GetBytes(16));
        var fsDecrypt = new FileStream(inFilePath, FileMode.Open);
        var cryptoStream = new CryptoStream(fsDecrypt, decryptor, CryptoStreamMode.Read);

        // Read the decrypted data from the file
        using (var srDecrypt = new StreamReader(cryptoStream))
        {
            return srDecrypt.ReadToEnd();
        }
    }
}
