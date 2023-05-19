using UnityEngine;
using System.Collections;

/// <summary>
/// 触发指定次数之后,才成功.成功之后,又会重新开始计数.并且可以指定每次触发的间隔时间.
/// </summary>
public class ConditionTimesAndDelay : ConditionBase
{
		/// <summary>
		/// 需要触发的次数.要触发这么多次数,才会返回一次触发成功.
		/// </summary>
		public int times;
		/// <summary>
		/// 最小的触发间隔时间.如果小于这个时间,那么这一次触发不算.
		/// </summary>
		public float minDelay;
		/// <summary>
		/// 最大的触发间隔时间.如果大于这个时间.触发又要从0开始计数.
		/// </summary>
		public float maxDelay;
		/// <summary>
		/// 从第一次到最后一次的触发最大间隔时间.如果超过了.那么又要从新开始.
		/// </summary>
		public float maxTatalDelay;
		/// <summary>
		/// 一共能触发成功几次.0表示不限次数.1表示触发成功之后,再也无法触发.
		/// </summary>
		public int maxTriggerTimes;
		private int current;
		private float lastTime;
		private float startTime;
		private int triggerTimes;

		public override bool isMatch (GameObject col)
		{
				if (minDelay != 0 && Time.time < lastTime + minDelay) {
						return false;
				}
				if (maxDelay != 0 && Time.time > lastTime + maxDelay) {
						lastTime = Time.time;
						return false;
				}
				if (maxTatalDelay != 0 && Time.time > startTime + maxTatalDelay) {
						lastTime = Time.time;
						startTime = lastTime;
						current = 0;
						return false;
				}
				lastTime = Time.time;
				if (startTime == 0) {
						startTime = Time.time;
				}
				current++;
				if (current >= times) {
						current = 0;
						triggerTimes++;
						if (maxTriggerTimes != 0 && triggerTimes > maxTriggerTimes) {
								return false;
						} else {
								return true;
						}
				} else {
						return false;
				}
		}
}
