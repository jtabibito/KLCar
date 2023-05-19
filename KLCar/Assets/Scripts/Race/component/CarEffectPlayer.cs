using UnityEngine;
using System.Collections;

/// <summary>
/// 会根据车子状态进行播放盒关闭的组件.
/// </summary>
public class CarEffectPlayer : MonoBehaviour
{
	public bool isActive = false;
	public enum Type
	{
		/// <summary>
		/// 加速时开启.
		/// </summary>
		JiaSu=10,
		/// <summary>
		/// 漂移或减速时开启.
		/// </summary>
		PiaoYi=11
	}
	private CarEngine car;
	/// <summary>
	/// 操作的时机.
	/// </summary>
	public Type when;
//	public OpenType operate;
	private Vector3 lastScale;
	/// <summary>
	/// 是开启还是关闭.
	/// </summary>
	public bool isOpen=true;
	void Start ()
	{
		Transform t= transform.root.FindChild ("Engine");
		if (t == null)
		{
			enabled=false;
			return;
		}
		car = t.GetComponent<CarEngine> ();
		lastScale = transform.localScale;
		if (isActive)
		{
			open ();
		} else
		{
			close ();
		}
	}
	
	void Update ()
	{
		if (isActive)
		{
			if (!car.getCarState ((int)when))
			{
				close ();
			}
		} else
		{
			if (car.getCarState ((int)when))
			{
				open ();
			}
		}
	}
	void open ()
	{
		if (isOpen)
		{
			doOpen();
		} else
		{
			doClose();
		}
	}
	void close ()
	{
		if (isOpen)
		{
			doClose();
		} else
		{
			doOpen();
		}
	}
	void doClose()
	{
		bool ok = false;
		isActive = false;
		//		if (operate == OpenType.Play)
		//		{
		if (particleSystem!=null)
		{
			particleSystem.Stop ();
			ok = true;
		}
		if (particleEmitter!=null)
		{
			particleEmitter.emit = false;
			ok = true;
		}
		if (!ok)
		{
			transform.localScale = Vector3.zero;
		}
		//		} else
		//		{
		//			transform.localScale=Vector3.zero;
		//		}
	}
	void doOpen()
	{
		bool ok = false;
		isActive = true;
		//		if (operate == OpenType.Play)
		//		{
		if (particleSystem!=null)
		{
			particleSystem.Play ();
			ok = true;
		}
		if (particleEmitter!=null)
		{
			particleEmitter.emit = true;
			ok = true;
		}
		if (!ok)
		{
			transform.localScale = lastScale;
		}
		//		} else
		//		{
		//			transform.localScale = lastScale;
		//		}
	}

}
