using UnityEngine;
using System.Collections;

/// <summary>
/// 判断是不是拥有指定状态的对象.比如:名称,tag,碰撞层等信息.
/// </summary>
public class ConditionLabel : ConditionBase
{
		
		/// <summary>
		/// 碰撞体的名词或者Tag.
		/// </summary>
		public string label;
		/// <summary>
		/// 判断的内容.
		/// </summary>
		public enum LabelType
		{
				name,
				tag,
				layerName,
				layerInt
		}
		/// <summary>
		/// 判断的label的类型.如果是layerInt,则label将被转换为int型数字.
		/// </summary>
		public LabelType labelType;

		public override bool isMatch (GameObject gameObject)
		{
				switch (labelType) {
				case LabelType.name:
						return gameObject.name == label;
				case LabelType.tag:
						return gameObject.CompareTag (label);
				case LabelType.layerName:
						return gameObject.layer == LayerMask.NameToLayer (label);
				case LabelType.layerInt:
						return gameObject.layer== int.Parse (label) ;
				default:
						return false;
				}
		}
}
