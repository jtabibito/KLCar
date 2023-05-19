using UnityEngine;
using System.Collections;

public class SkillBase : PlayAble {
	/// <summary>
	/// 配置的技能cd.如果是0,则取playAble的duration.
	/// </summary>
	public float cd;
	/// <summary>
	/// 技能实际的cd时间.
	/// </summary>
	/// <value>The play cd time.</value>
	[HideInInspector]
	public float playCdTime
	{
		get
		{
			if(cd==0)
			{
				return duration;
			}else{
				return cd;
			}
		}
	}
	 public CarEngine carEngine 
	{
		get{
			return transform.root.FindChild("Engine").GetComponent <CarEngine>();
		}
	}
}
