using UnityEngine;
using System.Collections;

namespace GameConsts 
{
	public enum ItemType:int
	{
		IT_Daodan=1,//导弹
		IT_Jiasu=2,//加速
		IT_Yinshen=3,//隐身
		IT_Hudun=4,//护盾
	}

	public enum GoodsType:int
	{
		GT_Power=1,//体力(爱心)
		GT_Gold=2,//金币
		GT_Diamond=3,//钻石
		GT_Daodan=4,//导弹
		GT_Jiasu=5,//加速
		GT_Yinshen=6,//隐身
		GT_Hudun=7,//护盾
	}

	public enum CostType:int
	{
		CT_Gold=1,//金币
		CT_Diamond=2,//钻石
		CT_Score=3,//积分
		CT_RMB=4,//人民币
	}

}
