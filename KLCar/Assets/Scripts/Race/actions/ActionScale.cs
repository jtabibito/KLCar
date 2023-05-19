using UnityEngine;
using System.Collections;

/// <summary>
/// 对目标大小进行缩放.
/// </summary>
public class ActionScale : ActionBase
{
		public enum ScaleType
		{
				ScaleTo,
				ScaleFrom,
				ScaleAdd,
				ScaleBy
		}
		public ScaleType scalType;
		public Vector3 values;
		public Easetype easetype;

		protected override void onStart ()
		{
				Hashtable h = iTween.Hash ();
				if (scalType == ScaleType.ScaleAdd) {
					h.Add ("amount", values);
				} else {
					h.Add ("scale", values);
				}
				h.Add ("time", time);
				if (easetype != Easetype.Default) {
						h.Add ("easetype", easetype.ToString ());
				}
				
		switch (scalType) {
				case ScaleType.ScaleAdd:
						iTween.ScaleAdd (gameObject, h);
						break;
				case ScaleType.ScaleFrom:
						iTween.ScaleFrom (gameObject, h);
						break;
				case ScaleType.ScaleBy:
						iTween.ScaleBy (gameObject, h);
						break;
				case ScaleType.ScaleTo:
						iTween.ScaleTo (gameObject, h);
						break;

				}
				
		}
	internal override void onCopyTo (ActionBase cloneTo)
	{
		ActionScale s=(ActionScale)cloneTo;
		s.scalType = scalType;
		s.values = values;
		s.easetype = easetype;
	}
}
