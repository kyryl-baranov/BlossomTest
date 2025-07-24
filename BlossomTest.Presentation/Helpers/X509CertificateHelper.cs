﻿using System.Security.Cryptography.X509Certificates;

namespace BlossomTest.Helpers
{
	public class X509CertificateHelper
	{
		public static X509Certificate2 GetCertificate(string thumbprint)
		{
			var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);

			try
			{
				store.Open(OpenFlags.ReadOnly);

				var certificatesInStore = store.Certificates;

				var certificateCollection = certificatesInStore.Find(X509FindType.FindByThumbprint, thumbprint, false);

				if (certificateCollection.Count == 0)
					throw new Exception("Certificate is not installed in local machine.");

				return certificateCollection[0];
			}
			finally
			{
				store.Close();
			}
		}
	}
}
