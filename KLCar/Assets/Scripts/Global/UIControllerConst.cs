
using UnityEngine;
using System.Collections;

/// <summary>
/// UI界面的一些常量定义
/// Copyright © 2015
/// 2015-4-15 13:54:20
/// </summary>
public class UIControllerConst
{
		///true 开启调试帧率， false 关闭调试帧率
		public readonly static bool DEBUG_FPS = true;
	
		////操作UI Prefeb名称定义
		public readonly static string UIPrefebOperationjinsumoshi = "ContainerOperationjinsumoshi";
		public readonly static string UIPrefebOperationpohuaisai = "ContainerOperationpohuaisai";
		public readonly static string UIPrefebOperationtiaozhansai = "ContainerOperationtiaozhansai";
		public readonly static string UIPrefebOperationjixiansai = "ContainerOperationjixiansai";

		/// <summary>
		/// 暂停 UI Prefeb名称定义
		/// </summary>
		public readonly static string UIPrefebOperationzantingshezhi = "ContainerOperationzantingshezhi";
	
		/// <summary>
		/// 结算 UI Prefeb名称定义
		/// </summary>
		public readonly static string UIPrefebOperationJiesuanFenshu = "ContainerOperationJiesuanFenshu";
		public readonly static string UIPrefebOperationJiesuanHaoshi = "ContainerOperationJiesuanHaoshi";
		public readonly static string UIPrefebOperationJiesuanMingci = "ContainerOperationJiesuanMingci";
		public readonly static string UIPrefebOperationJiesuanWancheng = "ContainerOperationJiesuanWancheng";
		public readonly static string UIPrefebOperationJiesuanWeiwancheng = "ContainerOperationJiesuanWeiwancheng";
		public readonly static string UIPrefebOperationJiesuanXingcheng = "ContainerOperationJiesuanXingcheng";

		/// <summary>
		/// 模式选择界面,主界面 Prefeb文件
		/// </summary>
		public readonly static string UIPrefebMainUI= "ContainerMainzhujiemian";
		public readonly static string UIPrefebCheliang= "ContainerCheliang";
		public readonly static string UIPrefebJuese= "ContainerJuese";
		public readonly static string UIPrefebChongwu= "ContainerChongwu";
		public readonly static string UIPrefebMoShixuanze= "ContainerMoshixuanze";
		public readonly static string UIPrefebStory= "ContainerStory";
		public readonly static string UIPrefebTask= "ContainerRenwu";

		//设置
		public readonly static string UIPrefebShezhi= "ContainerShezhi";
		//邮件容器
		public readonly static string UIPrefebYoujian= "ContainerYoujian";
		//更好头像
		public readonly static string UIPrefebTouxiang= "ContainerTouxiang";
		//公告
		public readonly static string UIPrefebGonggao= "ContainerGonggao";
		//每日登陆奖励
		public readonly static string UIPrefebMeiridenglu= "ContainerMeiridenglu";

		/// <summary>
		/// 当前功能未开发完成提示框 Prefeb文件
		/// </summary>
		public readonly static string UIPrefebTankuang= "ContainerTankuang";
		/// <summary>
		/// 人车宠 3D 容器 Prefeb名称定义
		/// </summary>
		public readonly static string UIPrefebUIShow3D= "ui_show_3D";			//no use
		public readonly static string UIPrefebUIShow2D= "ui_show_2D";
		public readonly static string UIPrefebUIShop= "ContainerShop";

		/// <summary>
		/// 技能特效 Prefeb名称定义
		/// </summary>
		public readonly static string SkillPrefebOperationJiaSu = "JiaSu";
		public readonly static string SkillPrefebOperationYinSheng = "YinSheng";
		public readonly static string SkillPrefebOperationFeiDan = "FeiDan";
		public readonly static string SkillPrefebOperationHuDun = "HuDun";
		public readonly static string SkillPrefebOperationPet = "PetSkill";
		/// <summary>
		/// 技能特效 Prefeb名称定义
		/// </summary>
		public readonly static string UIPrefebOperationKaishiDaoJiShi = "ContainerOperationKaishiDaoJiShi";


		/// 操作模式UI一些常量
		//指针的旋转点，此参数需要美工给出
		public readonly static Vector3 Vector3DashBoardPointerLocalOffset = new Vector3 (44.0f, 12.0f, 0.0f);
		//从美术给图结合Unity的Inspector计算出的角度
		public readonly static float   FloatDashBoardStartAngle = -87.0f;
		public readonly static float   FloatDashBoardEndAngle = 115.0f;

		public static bool UIPrefebPauseUIActive = false;
		
		public readonly static string InputTouch = "touch";
		public readonly static string InputGravity = "gravity";

		//模式选择界面上显示的体力消耗---------不是从配置表中读取
		public readonly static int  relaxationConsumpPower = 9;
}
