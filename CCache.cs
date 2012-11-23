using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CompressedCache
{
	public class CCache
	{
		public static void Insert(string cachekey, object objToCache, int minutesToCache)
		{
			HttpContext.Current.Cache.Insert(cachekey, Compress.CompressGZip(ConvertHelper.ObjectToByteArray(objToCache)), null, DateTime.Now.AddMinutes(minutesToCache), System.Web.Caching.Cache.NoSlidingExpiration);
		}

		public static int CalcSize(object objToCache)
		{
			return Compress.CompressGZip(ConvertHelper.ObjectToByteArray(objToCache)).Length;
		}

		public static object Get(string cachekey)
		{
			if (HttpContext.Current.Cache[cachekey] != null)
			{
				//HttpContext.Current.Response.Write("CACHE_" + cachekey + "_");
				return ConvertHelper.ByteArrayToObject(Compress.DecompressGZip((byte[])HttpContext.Current.Cache[cachekey]));
			}
			return null;
		}
	}
}
