using UnityEngine;
using System.Collections;

public enum LogicReturn:int{
	LR_SUCCESS=2000,//顺利完成逻辑
	LR_NOTENOUGHGOLD=2001,//没有足够的金币
	LR_NOTENOUGHDIAMOND=2002,//没有足够的钻石
	LR_NOTENOUGHSCORE=2003,//没有足够的积分
	LR_REACHEDMAXLV=2004,//达到最高等级
}
