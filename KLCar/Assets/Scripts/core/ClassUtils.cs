using UnityEngine;
using System.Collections;

public class ClassUtils   {

	 public static void copyData(object last,object target)
	{
		System.Type l=last.GetType();
		System.Type t = target.GetType ();
		if (l != t) {
						return;
				} else {
//					l.GetFields()
				}
	}
}
