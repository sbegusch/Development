using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace myAdminTool.OTCS
{
	public static class Util
	{
		private static readonly byte[] fKey = ASCIIEncoding.ASCII.GetBytes( "otservic" );

		/// <summary>
		/// Encrypt a string.
		/// </summary>
		/// <param name="originalString">The original string.</param>
		/// <returns>The encrypted string.</returns>
		/// <exception cref="ArgumentNullException">This exception will be thrown when the original string is null or empty.</exception>
		public static string Encrypt(string originalString)
		{
			string	retval = null;


			if ( String.IsNullOrEmpty( originalString ) )
			{
				retval = originalString;
			}
			else
			{
				try
				{
					DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
					MemoryStream memoryStream = new MemoryStream();
					CryptoStream cryptoStream = new CryptoStream( memoryStream, cryptoProvider.CreateEncryptor( fKey, fKey ), CryptoStreamMode.Write );

					using ( StreamWriter writer = new StreamWriter( cryptoStream ) )
					{
						writer.Write( originalString );
						writer.Flush();
						cryptoStream.FlushFinalBlock();
						writer.Flush();

						retval = Convert.ToBase64String( memoryStream.GetBuffer(), 0, (int) memoryStream.Length );
					}
				}
				catch ( CryptographicException ) { }
			}

			return retval;
		}

		/// <summary>
		/// Decrypt a crypted string.
		/// </summary>
		/// <param name="cryptedString">The crypted string.</param>
		/// <returns>The decrypted string.</returns>
		/// <exception cref="ArgumentNullException">This exception will be thrown when the crypted string is null or empty.</exception>
		public static string Decrypt(string cryptedString)
		{
			string	retval = null;


			if ( String.IsNullOrEmpty( cryptedString ) )
			{
				retval = cryptedString;
			}
			else
			{
				try
				{
					DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
					MemoryStream memoryStream = new MemoryStream( Convert.FromBase64String( cryptedString ) );
					CryptoStream cryptoStream = new CryptoStream( memoryStream, cryptoProvider.CreateDecryptor( fKey, fKey ), CryptoStreamMode.Read );

					using ( StreamReader reader = new StreamReader( cryptoStream ) )
					{
						retval = reader.ReadToEnd();
					}
				}
				catch ( CryptographicException ) { }
			}

			return retval;
		}
	}
}
