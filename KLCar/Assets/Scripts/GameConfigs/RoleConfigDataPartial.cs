using UnityEngine;
using System.Collections;

public partial class RoleConfigData : GameConfigDataBase
{
	string[] _costGoldOfLvupArr;
	public string[] costGoldOfLvupArr{
		get
		{
			if(_costGoldOfLvupArr==null)
			{
				_costGoldOfLvupArr=costGoldOfLvup.Split('#');
			}
			return _costGoldOfLvupArr;
		}
	}
	/// <summary>
	/// Gets the cost gold on lv.
	/// 获取人物提升到某等级需要的金币
	/// </summary>
	/// <returns>The cost gold on lv.</returns>
	/// <param name="lv">Lv.</param>
	public int GetCostGoldOnLv(int lv)
	{
		return int.Parse(costGoldOfLvupArr[lv]);
	}

	string[] _atr1AddValueArr;
	public string[] atr1AddValueArr{
		get
		{
			if(_atr1AddValueArr==null)
			{
				_atr1AddValueArr=atr1AddValue.Split('#');
			}
			return _atr1AddValueArr;
		}
	}
	/// <summary>
	/// Gets the atr1 add value on lv.
	/// 获取人物在某等级上第一个属性的值
	/// </summary>
	/// <returns>The atr1 add value on lv.</returns>
	/// <param name="lv">Lv.</param>
	public int GetAtr1AddValueOnLv(int lv)
	{
		return int.Parse(atr1AddValueArr[lv]);
	}

	string[] _atr2AddValueArr;
	public string[] atr2AddValueArr{
		get
		{
			if(_atr2AddValueArr==null)
			{
				_atr2AddValueArr=atr2AddValue.Split('#');
			}
			return _atr2AddValueArr;
		}
	}
	/// <summary>
	/// Gets the atr2 add value on lv.
	/// 获取人物在某等级上第二个属性的值
	/// </summary>
	/// <returns>The atr2 add value on lv.</returns>
	/// <param name="lv">Lv.</param>
	public int GetAtr2AddValueOnLv(int lv)
	{
		return int.Parse(atr2AddValueArr[lv]);
	}

	string[] _atr3AddValueArr;
	public string[] atr3AddValueArr{
		get
		{
			if(_atr3AddValueArr==null)
			{
				_atr3AddValueArr=atr3AddValue.Split('#');
			}
			return _atr3AddValueArr;
		}
	}
	/// <summary>
	/// Gets the atr3 add value on lv.
	/// 获取人物在某等级上第三个属性的值
	/// </summary>
	/// <returns>The atr3 add value on lv.</returns>
	/// <param name="lv">Lv.</param>
	public int GetAtr3AddValueOnLv(int lv)
	{
		return int.Parse(atr3AddValueArr[lv]);
	}
}
