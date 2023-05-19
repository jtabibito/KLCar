using UnityEngine;
using System.Collections;
/// <summary>
/// 缓动类型.
/// </summary>
public enum Easetype   {
	Default,
	linear,//线性.
	easeInSine,
	easeOutSine,
	easeInOutSine,
	easeInQuad,
	easeOutQuad,
	easeInOutQuad,
	easeInCubic,
	easeOutCubic,
	easeInOutCubic,
	easeInQuart,
	easeOutQuart,
	easeInOutQuart,
	easeInQuint,
	easeOutQuint,
	easeInOutQuint,
	easeInExpo,
	easeOutExpo,
	easeInOutExpo,
	spring,//弹簧.
	easeInCirc,//圆形.
	easeOutCirc,
	easeInOutCirc,
	easeInBounce,//弹跳.
	easeOutBounce,
	easeInOutBounce,
	easeInBack,//退回.
	easeOutBack,
	easeInOutBack,
	easeInElastic,//弹性.
	easeOutElastic,
	easeInOutElastic,
	punch//碰击.
}
