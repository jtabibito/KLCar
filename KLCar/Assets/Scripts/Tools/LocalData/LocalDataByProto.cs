using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
using System;

public class LocalDataByProto
{
		/// <summary>
		/// 读取本地对象数据
		/// </summary>
		/// <returns>The data.</returns>
		/// <param name="key">Key.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static T LoadData<T> (string key)
		{  
				if (PlayerPrefs.HasKey (key)) {
						string loadStr = PlayerPrefs.GetString (key);
						Debug.Log ("loadStr:" + loadStr);
						byte[] ba = Convert.FromBase64String (loadStr);
						MemoryStream nms = new MemoryStream ();
						nms.Write (ba, 0, ba.Length);
						nms.Position = 0;
						return ProtoBuf.Serializer.Deserialize<T> (nms);
				} else {  
						return default(T);  
				}  
		}   
	
		/// <summary>
		/// 存储本地对象数据
		/// </summary>
		/// <param name="key">Key.</param>
		/// <param name="source">Source.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static void SaveData<T> (string key, T source)
		{   

				MemoryStream ms = new MemoryStream (); 
				ProtoBuf.Serializer.Serialize<T> (ms, source);
				ms.Position = 0;
				long length = ms.Length;
				string rdStr = Convert.ToBase64String (ms.ToArray ());
				Debug.Log ("saveStr:" + rdStr);
				PlayerPrefs.SetString (key, rdStr);  
		}  
	
		/// <summary>
		/// 清除数据
		/// </summary>
		/// <param name="key">Key.</param>
		public static void ClearData (string key)
		{  
				PlayerPrefs.DeleteKey (key);  
		} 

		/// <summary>
		/// Determines if hash data the specified key.
		/// 检测是否具有本地数据
		/// </summary>
		/// <returns><c>true</c> if hash data the specified key; otherwise, <c>false</c>.</returns>
		/// <param name="key">Key.</param>
		public static bool HashData (string key)
		{
				return PlayerPrefs.HasKey (key);
		}
}
