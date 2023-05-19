using UnityEngine;
using System.Collections;

/// <summary>
/// 指定触发成功的概率.
/// </summary>
public class ConditionRandom : ConditionBase
{
		
		/// <summary>
		/// 碰撞体的名词或者Tag.
		/// </summary>
		public float value;
		public override bool isMatch (GameObject col)
		{
				return Random.Range (0,1)<value;
		}
}
