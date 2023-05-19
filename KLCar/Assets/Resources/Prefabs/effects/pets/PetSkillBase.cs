using UnityEngine;
using System.Collections;
/// <summary>
/// 宠物技能效果.
/// </summary>
public class PetSkillBase : SkillBase {
	public GameObject petModel;
	private GameObject pet;
	protected override void onPlay ()
	{
		base.onPlay ();
		GameObjectUtils.callOnChildren (carEngine.carBody.gameObject, false, false, new GameObjectUtils.CallOnChilden (hiddrenLast), "car", "role","effect");
		Vector3 lastPos = petModel.transform.localPosition;
		Vector3 lastAngle = petModel.transform.localEulerAngles;
		pet=(GameObject)GameObject.Instantiate (petModel);
		pet.name = "pet";
		pet.transform.parent = carEngine.carBody.transform;
		pet.transform.localPosition = lastPos;
		pet.transform.localEulerAngles = lastAngle;
		RoleAvtController avt=pet.AddComponent <RoleAvtController>();
		carEngine.changeRoleAvtController (avt);
//		avt.CurState = RoleAvtController.RoleAnimatorState.RAS_Ready;// RoleAnimatorState.RAS_Ready;

	}
	protected override void onStop ()
	{
		base.onStop ();
		GameObjectUtils.callOnChildren (carEngine.carBody.gameObject, false, false, new GameObjectUtils.CallOnChilden (showLast), "car", "role","effect");
		carEngine.changeRoleAvtController (null);
		DestroyObject (pet);
	}
	bool hiddrenLast(GameObject obj)
	{
		obj.SetActive (false);
//		Renderer[] r=obj.GetComponentsInChildren<Renderer>();
//		foreach (Renderer rr in r) {
//			rr.enabled=false;
//				}
		return false;
	}
	bool showLast(GameObject obj)
	{
		obj.SetActive (true);
//		Renderer[] r=obj.GetComponentsInChildren<Renderer>();
//		foreach (Renderer rr in r) {
//			rr.enabled=true;
//		}
		return false;
	}
}
