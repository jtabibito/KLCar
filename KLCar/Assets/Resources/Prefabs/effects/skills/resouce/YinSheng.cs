using UnityEngine;
using System.Collections;

/// <summary>
/// 隐身的技能效果.实际是其它车不会撞击当前车辆.
/// 通常需要指定隐身后,使用的shader.
/// </summary>
public class YinSheng : SkillBase
{
		/// <summary>
		/// 隐身时的特效材质.可以提供任何看起来像隐身的效果的材质.
		/// 如果需要配置原来的贴图.keepTexture=true.
		/// </summary>
		public Material effectMaterial;
		
		/// <summary>
		/// 如果特效材质需要使用原来的贴图.则为true.否则完全使用指定的特效材质显示对象.
		/// </summary>
		public bool keepTexture = true;
		/// <summary>
		/// 保存之前的所有的贴图.将来需要恢复原来的贴图.
		/// </summary>
		private Hashtable table = new Hashtable ();
		protected override void onPlay ()
		{
				
				base.onPlay ();
				if (carEngine.getCarState (CarState.YingShen)) {
					return;
				}
				carEngine.setCarState (CarState.YingShen, true);
				if (effectMaterial != null) {
//						useMaterial();
						GameObjectUtils.callOnChildren(carEngine.carBody.gameObject,true,true,new GameObjectUtils.CallOnChilden(useMaterial),"role","pet","car");
				}
		 
		}

		protected override void onStop ()
		{
				base.onStop ();
				carEngine.setCarState (CarState.YingShen, false); 
				if (effectMaterial != null) {
//						cancelMaterial ();
					GameObjectUtils.callOnChildren(carEngine.carBody.gameObject,true,true,new GameObjectUtils.CallOnChilden(cancelMaterial),"role","pet","car");
					table.Clear();
				}
		}

		private bool useMaterial (GameObject obj)
		{
			if (obj.renderer != null) {
					table.Add(obj,obj.renderer.material);
						Material m;
					if(keepTexture)
					{
					 	m=(Material)Material.Instantiate(effectMaterial);
						m.mainTexture=obj.renderer.material.mainTexture;	
					}else
					{
					  m= effectMaterial;
					}
					obj.renderer.material=m;
					obj.renderer.material.shader=effectMaterial.shader;
				}
//			foreach(string n in useEffectChildrens)
//			{
//				Transform t = carEngine.transform.parent.FindChild (n);
//				if(t!=null&&t.renderer!=null)
//				{
//					table.Add(t,t.renderer.material);
//				Material m;
//					if(keepTexture)
//					{
//					 	 m=(Material)Material.Instantiate(effectMaterial);
//						m.mainTexture=t.renderer.material.mainTexture;	
//					}else
//					{
//					  m= effectMaterial;
//					}
//					t.renderer.material=m;
////					t.renderer.material.shader=effectMaterial.shader;
//				}
//			}
			return true;	 
		}
	 
		private bool cancelMaterial (GameObject obj)
		{
				Material m = (Material)table[obj];
				if (m != null) {
					obj.renderer.material = m;
				}
//				foreach (DictionaryEntry d in table) {
//						Material m = (Material)d.Value;
//						Transform target = (Transform)d.Key;
//						if (m == null || target.renderer == null) {
//								continue;	
//						}
//						target.renderer.material = m;
//				}
//				table.Clear ();
				return true;
		}
}
