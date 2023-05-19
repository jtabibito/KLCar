using UnityEngine;
using System.Collections;

public partial class CarConfigData : GameConfigDataBase
{
	string[] _accLvupCostGoldArr;
	public string[] accLvupCostGoldArr{
		get
		{
			if(_accLvupCostGoldArr==null)
			{
				_accLvupCostGoldArr=accLvupCostGold.Split('#');
			}
			return _accLvupCostGoldArr;
		}
	}
	/// <summary>
	/// Gets the cost gold on acc lv.
	/// 获得提升到某个等级加速度时需要消耗的金币
	/// </summary>
	/// <returns>The cost gold on acc lv.</returns>
	/// <param name="lv">Lv.</param>
	public int GetCostGoldOnAccLv(int lv)
	{
		return int.Parse(accLvupCostGoldArr[lv]);
	}

	string[] _accLvupValueArr;
	public string[] accLvupValueArr{
		get
		{
			if(_accLvupValueArr==null)
			{
				_accLvupValueArr=accLvupValue.Split('#');
			}
			return _accLvupValueArr;
		}
	}
	/// <summary>
	/// Gets the cost gold on acc lv.
	/// 获得某个等级加速度的值
	/// </summary>
	/// <returns>The cost gold on acc lv.</returns>
	/// <param name="lv">Lv.</param>
	public int GetValueOnAccLv(int lv)
	{
		return int.Parse(accLvupValueArr[lv]);
	}

	string[] _speedLvupCostGoldArr;
	public string[] speedLvupCostGoldArr{
		get
		{
			if(_speedLvupCostGoldArr==null)
			{
				_speedLvupCostGoldArr=speedLvupCostGold.Split('#');
			}
			return _speedLvupCostGoldArr;
		}
	}
	/// <summary>
	/// Gets the cost gold on speed lv.
	/// 获得提升到某个等级最大速度时需要消耗的金币
	/// </summary>
	/// <returns>The cost gold on speed lv.</returns>
	/// <param name="lv">Lv.</param>
	public int GetCostGoldOnSpeedLv(int lv)
	{
		return int.Parse(speedLvupCostGoldArr[lv]);
	}

	string[] _speedLvupValueArr;
	public string[] speedLvupValueArr{
		get
		{
			if(_speedLvupValueArr==null)
			{
				_speedLvupValueArr=speedLvupValue.Split('#');
			}
			return _speedLvupValueArr;
		}
	}
	/// <summary>
	/// Gets the value on speed lv.
	/// 获得某个等级最大速度的值
	/// </summary>
	/// <returns>The value on speed lv.</returns>
	/// <param name="lv">Lv.</param>
	public int GetValueOnSpeedLv(int lv)
	{
		return int.Parse(speedLvupValueArr[lv]);
	}

	string[] _handlerLvupCostGoldArr;
	public string[] handlerLvupCostGoldArr{
		get
		{
			if(_handlerLvupCostGoldArr==null)
			{
				_handlerLvupCostGoldArr=handlerLvupCostGold.Split('#');
			}
			return _handlerLvupCostGoldArr;
		}
	}
	/// <summary>
	/// Gets the cost gold on handler lv.
	/// 获得提升到某个等级操控性能时需要消耗的金币
	/// </summary>
	/// <returns>The cost gold on handler lv.</returns>
	/// <param name="lv">Lv.</param>
	public int GetCostGoldOnHandlerLv(int lv)
	{
		return int.Parse(handlerLvupCostGoldArr[lv]);
	}

	string[] _handlerLvupValueArr;
	public string[] handlerLvupValueArr{
		get
		{
			if(_handlerLvupValueArr==null)
			{
				_handlerLvupValueArr=handlerLvupValue.Split('#');
			}
			return _handlerLvupValueArr;
		}
	}
	/// <summary>
	/// Gets the value on handler lv.
	/// 获得某个等级最大操控性能的值
	/// </summary>
	/// <returns>The value on handler lv.</returns>
	/// <param name="lv">Lv.</param>
	public int GetValueOnHandlerLv(int lv)
	{
		return int.Parse(handlerLvupValueArr[lv]);
	}
}
