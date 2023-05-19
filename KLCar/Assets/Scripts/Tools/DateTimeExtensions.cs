using UnityEngine;
using System.Collections;
using System;

public class DateTimeExtensions{

	private static DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
	public static long CurrentTimeSeconds()
	{
		return (long) ((DateTime.UtcNow - Jan1st1970).TotalSeconds);
	}

	public static DateTime DateTimeFromSeconds(long seconds)
	{
		return Jan1st1970.AddSeconds(seconds);
	}
}
