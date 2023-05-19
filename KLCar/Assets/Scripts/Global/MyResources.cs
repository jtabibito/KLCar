using UnityEngine;
using System.Collections;

public class MyResources  {

	public static Object Load(string path,System.Type type)
	{
		return Resources.Load (path,type);
	}

	public static Object Load(string path)
	{
		return Resources.Load (path);
	}

	public static T Load<T>(string path) where T:Object
	{
		return (T)MyResources.Load(path,typeof(T));
	}

	private static T GetResourceAtResourceManager<T>(string path) where T:Object
	{
		if(!ResourceManager.Instance.IsResLoaded(path))
		{
			throw new UnityException("res not load over");
		}
		return (T)ResourceManager.Instance.GetResource(path);
	}
}
