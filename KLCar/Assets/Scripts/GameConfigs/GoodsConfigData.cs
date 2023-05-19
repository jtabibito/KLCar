using UnityEngine;
using System.Collections;

public partial class GoodsConfigData : GameConfigDataBase
{
	public string id;
	public int goodsType;
	public int goodsNum;
	public int costType;
	public int costValue;
	public string img;
	protected override string getFilePath ()
	{
		return "GoodsConfigData";
	}
}
