using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Net;

public class HttpRequestByProto
{
		public delegate void ResponseFromServer (System.Object responsObj);

		/// <summary>
		/// Requests the by proto.
		/// 发送proto的请求
		/// </summary>
		/// <param name="responseFromServer">Response from server.</param>
		/// <param name="requestObj">Request object.</param>
		/// <param name="address">Address.</param>
		/// <param name="timeout">Timeout.</param>
		/// <param name="method">Method.</param>
		/// <param name="contentType">Content type.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		/// <typeparam name="K">The 2nd type parameter.</typeparam>
		public static void RequestByProto<T,K> (ResponseFromServer responseFromServer, T requestObj, string address, int timeout, string method, string contentType)
		{
				bool netError = false;
				MemoryStream protobuffStream = new MemoryStream ();
				ProtoBuf.Serializer.Serialize<T> (protobuffStream, requestObj);
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create (new Uri (address));
				request.Method = method;
				request.ContentType = contentType;
				//request.Timeout = timeOut;
				Debug.Log ("发送长度=" + protobuffStream.Length);
				request.ContentLength = protobuffStream.Length;
				//数据是否缓冲 false 提高效率            
				request.AllowWriteStreamBuffering = false;
				try {
						Stream requestStream = request.GetRequestStream ();
						requestStream.Write (protobuffStream.ToArray (), 0, (int)protobuffStream.Length);
						protobuffStream.Close ();
						requestStream.Close ();
				} catch (Exception e) {
						Debug.Log ("net error:" + e.Message);
						netError = true;
				}
				if (responseFromServer != null) {
						K responseObj = default(K);
						if (!netError) {
								WebResponse response = request.GetResponse ();
								Stream responseStream = response.GetResponseStream ();
								responseObj = ProtoBuf.Serializer.Deserialize<K> (responseStream);
								responseStream.Close ();
								response.Close ();
						}
						responseFromServer (responseObj);
				}
		}

		/// <summary>
		/// Requests the by proto.
		/// 发送proto的请求
		/// </summary>
		/// <param name="responseFromServer">Response from server.</param>
		/// <param name="requestObj">Request object.</param>
		/// <param name="address">Address.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		/// <typeparam name="K">The 2nd type parameter.</typeparam>
		public static void RequestByProto<T,K> (ResponseFromServer responseFromServer, T requestObj, string address)
		{
				RequestByProto<T,K> (responseFromServer, requestObj, address, 100, "POST", "application/x-www-form-urlencoded");
		}
}
