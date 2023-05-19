using UnityEngine;
using System.Collections;
/// <summary>
/// 可以挂在GameObject上的脚本.说个脚本可以按顺序执行.
/// </summary>
public abstract class ActionBase :MonoBehaviour
{
	public string info;
	/// <summary>
	/// 脚本在哪个目标对象上运行.支持GameObjectAgent
	/// </summary>
	public GameObject runTarget;
	internal static bool isWating = false;
	/// <summary>
	/// 是否暂时放弃这个节点.直接运行后续节点.
	/// </summary>
	public bool ignore;
//		public string actionName;
	public float time = 0;
	public bool waitOver = true;
	public bool openNext = true;
	/// <summary>
	/// 当前已经消耗的时间.
	/// </summary>
	internal float duration;
//	public Transform target;
	private bool _isStart = false;
	internal bool distoryOnOver;
//		internal ActionRunOther parentAction;
//		internal bool notInit=false;
	void Start ()
	{

	}
	/// <summary>
	/// 设置当前的最大等待时间.
	/// </summary>
	/// <param name="max">Max.</param>
	public void setMaxWait(float max)
	{
		if (time - duration > max)
		{
			duration=time-max;
		}
	}
	/// <summary>
	/// 设置等待时间.
	/// </summary>
	/// <param name="wait">Wait.</param>
	public void setWait(float wait)
	{
		duration = time - wait;
	}
	public float progress
	{
		get
		{
			return duration / time;
		}
	}
 
	void OnEnable ()
	{
		if (isWating)
		{
			return;
		}
		if (isStart)
			return;
			
		duration = 0;
		_isStart = true;
		if (!ignore)
		{
			onStart ();
		}
		if (time == 0&&duration==0)
		{
			over ();
		}
		if (openNext && !waitOver)
		{
			openNextAction ();
		}
	}

	protected virtual void onStart ()
	{

	}
		
	void LateUpdate ()
	{
		duration += Time.deltaTime;
		if (duration >= time)
		{
			over (); 
		}
	}
	/// <summary>
	/// 结束当前动作,进行下一个动作.
	/// </summary>
	public void over ()
	{
		onOver ();
		enabled = false;
	}
	/// <summary>
	/// D立刻停止所有的动作.包括后续动作.
	/// </summary>
	public void doOver ()
	{
		bool openNext = this.openNext;
		_isStart = false;
		this.openNext = false;
		enabled = false;
		this.openNext = openNext;
	}

	protected virtual void onOver ()
	{

	}

	public bool isStart
	{
		get
		{
			return _isStart;
		}
	}
	/// <summary>
	/// 取得存放脚本的GameObject.
	/// </summary>
	/// <returns>The action parent.</returns>
	public GameObject getActionParent ()
	{
		return base.gameObject;
	}
	 
	public new GameObject gameObject
	{
		get
		{
			if (runTarget == null)
			{

				return base.gameObject;
			} else
			{
				return GameObjectAgent.GetAgentGameObject (base.gameObject, runTarget);
			}
		}
			
	}

	public new Transform transform
	{
		get
		{
			if (runTarget == null)
			{
				return base.transform;
			} else
			{
				return GameObjectAgent.getAgentTransform (base.gameObject, runTarget);
			}
		}
		
	}

	[ExecuteInEditMode]
	void OnDisable ()
	{
		if (!_isStart)
		{
			return;
		}
		if (isWating)
		{
			return;
		}
		if (!isStart)
		{
			return;	
		}
		_isStart = false;
		if (openNext && waitOver)
		{
			openNextAction ();
		}
		if (distoryOnOver)
		{
			DestroyObject (this);
		}
	}

	public bool openNextAction ()
	{
		bool find = false;
		ActionBase[] list = base.gameObject.GetComponents <ActionBase> ();
		foreach (ActionBase b in list)
		{
			if (find)
			{
				b.enabled = true;
				return true;
			} else
			{
				if (b == this)
				{
					find = true;
				}
			}
		}
		return false;
	}

	public void copyData (ActionBase a)
	{
		a.enabled = enabled;
		a.info = info;
		a.ignore = ignore;
//				a.actionName = actionName;
		a.time = time;
		a.waitOver = waitOver;
		a.openNext = openNext;
		a.runTarget = runTarget;
		onCopyTo (a);
	}

	internal abstract void onCopyTo (ActionBase cloneTo);
}
