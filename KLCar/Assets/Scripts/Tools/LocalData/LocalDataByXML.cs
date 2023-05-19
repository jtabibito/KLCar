using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.IO;

public class LocalDataByXML {

	/// <summary>
	/// 读取本地对象数据
	/// </summary>
	/// <returns>The data.</returns>
	/// <param name="key">Key.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public static T LoadData<T>( string key ){  
		if ( PlayerPrefs.HasKey( key ) ) {    
			XmlSerializer serializer = new XmlSerializer( typeof( T ) );  
			StringReader sr = new StringReader( PlayerPrefs.GetString( key ) );  
			return ( T )serializer.Deserialize( sr );  
		}else{  
			return default(T);  
		}  
	}   

	/// <summary>
	/// 存储本地对象数据
	/// </summary>
	/// <param name="key">Key.</param>
	/// <param name="source">Source.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public static void SaveData<T>( string key, T source ){  
		XmlSerializer serializer = new XmlSerializer( typeof( T ) );  
		StringWriter sw = new StringWriter();  
		serializer.Serialize( sw, source );  
		Debug.Log (sw.ToString ());
		PlayerPrefs.SetString( key, sw.ToString() );  
	}  

	/// <summary>
	/// 清除数据
	/// </summary>
	/// <param name="key">Key.</param>
	public static void ClearData( string key ){  
		PlayerPrefs.DeleteKey( key );  
	}  
}
