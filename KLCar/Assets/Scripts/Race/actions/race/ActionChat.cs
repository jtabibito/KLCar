using UnityEngine;
using System.Collections;
/// <summary>
///显示一个对话.
/// </summary>
public class ActionChat : ActionBase {
	public string roleName="{user}";
	public string roleImage="{userPhoto}";
	public bool left;
	public string msg;
	private bool isOver=false;
	public float minWait=0.3f;
	private float waitTime=0;
	internal override void onCopyTo (ActionBase cloneTo)
	{

	}
	protected override void onStart ()
	{
		validateData ();
		waitTime = minWait;
		isOver = false;
		if (time == 0)
		{
			setWait (5);
		}
		RaceManager.Instance.showChat (roleName,roleImage,left,msg);
		Debug.Log (roleName+"说:"+msg+"  "+Time.time);
	}
	void validateData()
	{
		if (roleName == "{user}")
		{
			roleName=MainState.Instance.playerInfo.nickname;
		}
		string xx = MainState.Instance.playerInfo.userRoleImgID < 10 ? ("0" + MainState.Instance.playerInfo.userRoleImgID) : MainState.Instance.playerInfo.userRoleImgID.ToString ();
		if (roleImage == "{userPhoto}")
		{
			roleImage= "ui_rolebanshen_" + xx.ToString ();
		}
	}
	void Update ()
	{
		waitTime -= Time.deltaTime;
		if (waitTime > 0)
		{
			return;
		}
		if (Input.GetMouseButton (0))
		{
			isOver=true;
		} else
		{
			if (isOver)
			{
				over ();
			}
		}
	}
}

