using UnityEngine;
using System.Collections;

public class SkillManager  
{
	public static SkillManager _instance;
	public static SkillManager instance
	{
		get
		{
			if(_instance==null)
			{
				_instance=new SkillManager();
			}
			return _instance;
		}
	}
	public GameObject jiashuSkill;
	public GameObject daodanSkill;
	public SkillManager()
	{
		jiashuSkill = GameResourcesManager.GetSkillPrefab ("JiaSu");
		daodanSkill = GameResourcesManager.GetSkillPrefab ("FeiDan");
	}
	public void playJiaSu(CarEngine car)
	{
//		GameObject obj=	(GameObject)GameObject.Instantiate ();
		car.doUseSkill (jiashuSkill);
	}
	public void playFeiDan(CarEngine car)
	{
		car.doUseSkill (daodanSkill);
	}
}

