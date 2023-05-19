using UnityEngine;
using System.Collections;
/// <summary>
/// 扩展系统的Mathf.提供更多游戏开发中需要使用的数学工具.
/// </summary>
public class MathUtils {
	/// <summary>
	/// 千米/时 转化为 米/秒 的比率.
	/// </summary>
	public static float KmH_To_MS=0.277777778f;
	/// <summary>
	/// 米/秒 转化为 千米/时 的比率.
	/// </summary>
	public static float MS_To_KmH=3.6f;
	/// <summary>
	/// 设置指定位置上的位为指定的值.比如说:将第14位设置为1.setBinaryValue(target,14,true);
	/// </summary>
	/// <returns>返回被修改后的数据.</returns>
	/// <param name="target">要修改的数据.</param>
	/// <param name="pos">要修改的位置..</param>
	/// <param name="value">要设置的值.true:1,false:0</param>
	public static int setBinaryValue(int src, int index, bool value)
	{
		bool binaryValue = getBinaryValue(src, index);
		if (binaryValue != value)
		{
			return src ^ (1 << index);
		} else
		{
			return src;
		}
	}
	/// <summary>
	/// 取得指定位置的值.取得32位int型数据中,指定位置为0还是1.
	/// </summary>
	/// <returns><c>true</c>, if binary value was gotten, <c>false</c> otherwise.</returns>
	/// <param name="src">原始数据</param>
	/// <param name="index">位置.</param>
	public static bool getBinaryValue(int src, int index)
	{
		if (index < 0 || index >= 32)
		{
			throw new  UnityException("设置二进制位的索引超出范围" + index);
		}
		return (src & (1 << index)) != 0;
	}
	/// <summary>
	/// 取得一个指定长度的向量.方向与给予的向量相同.
	/// </summary>
	/// <returns>The vector by length.</returns>
	/// <param name="v">V.</param>
	/// <param name="length">Length.</param>
	public static Vector3 getVectorByLength(Vector3 v,float length)
	{
//		return v.normalized * length;
		return Vector3.ClampMagnitude(v.normalized*length,length);
	}
	/// <summary>
	/// 将欧拉角转化为位置标示的Vector3
	/// </summary>
	/// <returns>The vector by rotation.</returns>
	/// <param name="v">V.</param>
	public static Vector3 getVectorByEulerAngles (Vector3 v)
	{
		return Vector3.zero;
	}
	/// <summary>
	/// 取得向量表示的旋转.向量的方向标示为旋转.
	/// </summary>
	/// <returns>The quaternion by vector.</returns>
	/// <param name="v">V.</param>
	public static Quaternion getRotationByVector(Vector3 v)
	{
		return Quaternion.FromToRotation (Vector3.zero, v);
	}
	/// <summary>
	/// 通过一个旋转,获得对应的Vector3坐标标示形式.
	/// </summary>
	/// <returns>The vector by rotation.</returns>
	/// <param name="rotation">Rotation.</param>
	public static Vector3 getVectorByRotation(Quaternion rotation)
	{
		return Vector3.zero;
	}
	/// <summary>
	/// 跟Math.sign相似,但是0返回0.
	/// </summary>
	/// <param name="f">F.</param>
	public static int sign0(float f)
	{
		if (f == 0)
		{
			return 0;
		} else
		{
			return f>0?1:-1;
		}
	}
	public static bool isInProbability(float rate)
	{
		return Random.value<rate;
	}
	public static bool isInRange(float value,float min,float max)
	{
		if (max < min)
		{
			float a=max;
			max=min;
			min=a;
		}
		return value>=min&&value<=max;
	}
	public static int getIntBetween(int start,int end)
	{
		return (int)Random.Range (start,end+1);
	}
	public static float getFloatBetween(float s,float e)
	{
		return Random.Range (s,e+1);
	}
	/// <summary>
	/// 将角度规范化为0~360度.
	/// </summary>
	/// <returns>The angle.</returns>
	/// <param name="r">The red component.</param>
	public static float trimAngle360(float r)
	{
		return (r % 360 + 360) % 360;
	}
	/// <summary>
	/// 将角度规范化为-180~180度之间
	/// </summary>
	/// <returns>The angle.</returns>
	/// <param name="r">The red component.</param>
	public static float trimAngle180(float r)
	{
		r=trimAngle360 (r);
		if (r > 180)
		{
			return r-360;
		} else
		{
			return r;
		}
	}
	/// <summary>
	/// 取得两个角之间的最小夹角
	/// </summary>
	/// <returns>The <see cref="System.Single"/>.</returns>
	/// <param name="s">S.</param>
	/// <param name="e">E.</param>
	public static float getAngleBetween(float s,float e)
	{
		return 0;
	}
	/// <summary>
	/// 计算出两圈之间的距离.相当于一个圆形跑道,当前两个对象之间的距离.忽略他们的圈数.
	/// 实际是就是从s追赶到e需要移动的距离.
	/// </summary>
	/// <returns>表示从起点到终点的偏移.</returns>
	/// <param name="s">起点的值.</param>
	/// <param name="e">终点的值</param>
	/// <param name="size">一圈的大小.</param>
	public static float getRoundOffset(float s,float e,float size)
	{
		s %= size;
		e %= size;
		float offset = e - s;
		if (offset > 0)
		{
			return offset;
		} else
		{
			return size+offset;
		}
	}
	/// <summary>
	/// 计算出e相对于s的距离差.最大距离为size/2.在后面为负数.
	/// 实际是计算出e在s的前面还是后面.以半圈为基准.
	/// </summary>
	/// <returns>e在s的前面还是后面</returns>
	/// <param name="s">S.</param>
	/// <param name="e">E.</param>
	/// <param name="size">Size.</param>
	public static float getRoundDiff(float s,float e,float size)
	{
		float f=getRoundOffset (s, e, size);
		if (f > size / 2)
		{
			f-=size;
		}
		return f;
	}
}
