using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Race manager.
/// 赛程管理器,比赛逻辑和数据的处理中心
/// </summary>
public class RaceManager
{
	static RaceManager instance;
	/// <summary>
	/// Gets the instance.
	/// 获取赛程单例
	/// </summary>
	/// <value>The instance.</value>
	public static RaceManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new RaceManager ();
			}
			return instance;
		}
	}
	
	private List<Transform> wayPoints = new List<Transform> ();
	private List<Transform> _reverseWayPoints = new List<Transform> ();
	private List<CarEngine> allCars = new List<CarEngine> ();
	/// <summary>
	/// 正在比赛的车辆.玩家车辆为0,剩下的是AI车.
	/// </summary>
	private List<CarEngine> raceCars = new List<CarEngine> ();
	/// <summary>
	/// 上一次的排名顺序.
	/// </summary>
	private List<CarEngine> lastRanking = new List<CarEngine> ();
	/// <summary> 
	/// Gets the way points.
	/// 获取路点
	/// </summary>
	/// <value>The way points.</value>
	public List<Transform> WayPoints
	{
		get
		{
			return wayPoints;
		}
	}

	public List<CarEngine> allRaceCars
	{
		get
		{
			return raceCars;
		}
	}
	public List<Transform> reverseWayPoints
	{
		get
		{
			return _reverseWayPoints;
		}
	}

//	private GameObject playerCar;
	private CarEngine playerCarEngine;
	/// <summary>
	/// Gets the player car.
	/// 获取玩家车辆
	/// </summary>
	/// <value>The player car.</value>
	public GameObject PlayerCar
	{
		get
		{
			return playerCarEngine.transform.parent.gameObject;
		}
	}

	public CarEngine userCar
	{
		get
		{
			return playerCarEngine;
		}
	}

	private GameObject carCamera;
	/// <summary>
	/// Gets the car camera.
	/// 获取相机
	/// </summary>
	/// <value>The car camera.</value>
	public GameObject CarCamera
	{
		get
		{
			return carCamera;
		}
	}

	private List<GameObject> obstacleCars = new List<GameObject> ();
	/// <summary>
	/// Gets the obstacle cars.
	/// 获取障碍车数组
	/// </summary>
	/// <value>The obstacle cars.</value>
	public List<GameObject> ObstacleCars
	{
		get
		{
			return obstacleCars;
		}
	}

	private RaceData raceData;
	/// <summary>
	/// Gets the race data.
	/// 获取赛程数据
	/// </summary>
	/// <value>The race data.</value>
	public RaceData RaceData
	{
		get
		{
			return raceData;
		}
	}
	/// <summary>
	/// 比赛达到结束条件的事件.准备结束比赛.
	/// </summary>
	public void onBeforRaceOver ()
	{
		Debug.Log ("结束:>"+raceCounter.curResult);
		bool ok = false;
		switch (raceCounter.curResult)
		{
			case RaceCounter.RaceResult.RR_Victory:
				ok = runScenceAction ("end_victory");
				break;
			case RaceCounter.RaceResult.RR_Lose:
				ok = runScenceAction ("end_failed");
				break;
			default:
				
				break;
		}
		if (!ok)
		{
			if (!runScenceAction ("end"))
			{
				PanelMainUIController.Instance.OverRace ();
			}
		}
	}

	private RaceConfigData raceConfig;
	/// <summary>
	/// Gets the race config.
	/// 获取赛程配置数据
	/// </summary>
	/// <value>The race config.</value>
	public RaceConfigData RaceConfig
	{
		get
		{
			return raceConfig;
		}
	}

	private List<TriggerItemData> triggerItems = new List<TriggerItemData> ();
	/// <summary>
	/// Gets the trigger items.
	/// 获取赛程道具数组
	/// </summary>
	/// <value>The trigger items.</value>
	public List<TriggerItemData> TriggerItems
	{
		get
		{
			return triggerItems;
		}
	}

	private List<TriggerGoldCreateCase> triggerGoldCreateCases = new List<TriggerGoldCreateCase> ();
	/// <summary>
	/// Gets the trigger item create cases.
	/// 获取金币道具生成方案数组
	/// </summary>
	/// <value>The trigger item create cases.</value>
	public List<TriggerGoldCreateCase> TriggerItemCreateCases
	{
		get
		{
			return triggerGoldCreateCases;
		}
	}

	private TriggerGoldCreateCase curCase;
	private int creatItemCaseSpaceCounter = 0;
	private int creatItemSpaceCounter;
	private int creatIndex;
	private int creatObstacleCarCounter = 0;
	private bool isOver;	

	/// <summary>
	/// The race counter.
	/// 赛程记录
	/// </summary>
	private RaceCounter raceCounter;

	public RaceCounter RaceCounterInstance 
	{
		get
		{
			return raceCounter;
		}
	}
	
	/// <summary>
	/// Inits the race.
	/// 初始赛程,由逻辑调用
	/// </summary>
	/// <param name="rd">Rd.</param>
	/// <param name="rc">Rc.</param>
	public void InitRace (RaceData rd, RaceConfigData rc)
	{
				
		this.raceData = rd;
		this.raceConfig = rc;
		this.raceCounter = new RaceCounter ();
		this.createCar ();

	}

	/// <summary>
	/// 创建车辆.准备开始.
	/// </summary>
	public void createCar ()
	{
		this.CreatGoldCreateCases ();
		this.CreatWayPoints ();
		this.CreatPlayerCar ();
		this.SetCarCamera ();
//				if (autoStart) {
//						startRace ();
//				}
		validateStart ();
	}

	private GameObject storyAction; 
	/// <summary>
	/// 执行一个场景脚本.
	/// </summary>
	/// <returns><c>true</c>, if scence action was run, <c>false</c> otherwise.</returns>
	/// <param name="action">Action.</param>
	public bool runScenceAction (string child)
	{
//		if (storyAction == null)
//		{
//
//		} else
//		{
//			storyAction.transform.FindChild("");
//		}
//		GameObject actions = GameObject.Find ("SenceActionCodes");
//		if (actions == null) {
//			actions = (GameObject)GameObject.Instantiate (GameResourcesManager.GetRaceObject ("SenceActionCodes"));
//			actions.name = "SenceActionCodes";
//		}
//		if (actions != null) {
//			Transform t = actions.transform.FindChild (action);
//			if (t != null) {
//				ActionBase ac = t.gameObject.GetComponent<ActionBase> ();
//				if (ac != null) {
//					ac.enabled = true;
//					return true;
//				}
//			}
//		}
		if (int.Parse (raceConfig.id) >= 1000)
		{
			storyActionIndex = "story/story_" + raceConfig.id;
		} else
		{
			storyActionIndex = "race/race_" + raceConfig.id;
		}

		if (storyActionIndex == null || storyActionIndex == "")
		{
			return ActionUtils.runPrefab ("race/race_1-1", child) != null;
		} else
		{
			bool b = ActionUtils.runPrefab (storyActionIndex, child) != null;
			storyActionIndex = null;
			return b;
		}
	}
	/// <summary>
	/// 当前比赛使用哪个动作脚本.
	/// </summary>
	public static string storyActionIndex;

	void validateStart ()
	{
		if (!runScenceAction ("start"))
		{
			startRace ();
		}
		return;
	}
	/// <summary>
	/// 正式开始比赛.
	/// </summary>
	public void startRace ()
	{
		TweenUtils.CameraFadeTo (0f, 0.5f);
		this.BeginRace ();
		PanelMainUIController.Instance.EnterRace ();
	}
	public bool isRaceOver 
	{
		get
		{
			return isOver;
		}
	}
	/// <summary>
	/// Begins the race.
	/// 开始比赛
	/// </summary>
	public void BeginRace ()
	{
		isOver = false;
		CarEngine engine = playerCarEngine;
		this.raceCounter.SetRaceMode ((RaceCounter.RaceMode)this.raceConfig.raceMode, this.raceConfig.racePar1, this.raceConfig.racePar2, this.raceConfig.racePar3);
		this.raceCounter.beginWaypoint = engine.currentWaypoint;
		this.raceCounter.beginTime = Time.time;
//		engine.BeginRun ();
		foreach (CarEngine e in allCars)
		{
			if (!e.IsRun)
			{
				e.BeginRun ();
			}
		}
	}

	/// <summary>
	/// Overs the race.
	/// 结束比赛
	/// </summary>
	public void OverRace ()
	{
		isOver = true;
//		foreach (GameObject go in this.obstacleCars) {
//			GameObject.Destroy (go);
//		}
		int user = userCar.fowardWaypointIndex;
		foreach (CarEngine car in this.allCars)
		{
			if (car != userCar && car != null)
			{//这里有对象泄露.所以判断null,以后需要修复.
				float off = MathUtils.getRoundDiff (user, car.fowardWaypointIndex, RaceManager.instance.wayPointNumber);
				if (off < 0)
				{//在用户后面.
					GameObject.Destroy (car.transform.parent.gameObject);
				} else
				{//在用户前面.
					car.setCarState (CarState.YingShen, true);
					if (car.gameObject.GetComponent<AutoRemoveAI> () == null)
					{
						car.gameObject.AddComponent<AutoRemoveAI> ();
					}
				}
			}
		}
		this.obstacleCars.Clear ();
		foreach (TriggerItemData tid in this.triggerItems)
		{
			GameObject.Destroy (tid.triggerItem);
		}
		this.triggerItems.Clear ();

		RaceManager.instance.onBeforRaceOver ();
		LogicManager.Instance.ActNewLogic<LogicEndRace> (null, null);
	}

	/// <summary>
	/// Resets the race.
	/// 复位比赛
	/// </summary>
	public void ResetRace ()
	{
		raceCars.Clear ();
		wayPoints.Clear ();
		playerCarEngine = null;
		carCamera = null;
		obstacleCars.Clear ();
		allCars.Clear ();
		lastRanking.Clear ();
		raceData = null;
		raceConfig = null;
		triggerItems.Clear ();
		triggerGoldCreateCases.Clear ();
		curCase = null;
		creatItemCaseSpaceCounter = 0;
		creatItemSpaceCounter = 0;
		creatIndex = 0;
		creatObstacleCarCounter = 0;
		isOver = false;
		raceCounter = null;
		triggerItemPool.Clear ();
		obstacleCarPool.Clear ();
		instance = new RaceManager ();//最后把整个管理者清除.用新的管理者,防止有数据没有释放造成异常.
	}

	/// <summary>
	/// Players the car update road point.
	/// 玩家赛车更新路点
	/// </summary>
	/// <param name="wayPoints">Way points.更新的路点列表</param>
	public void PlayerCarUpdateRoadPoint (List<int> wayPoints)
	{
		if (isOver)
		{
			return;
		}
		CarEngine ce = playerCarEngine;// playerCar.transform.FindChild ("Engine").GetComponent<CarEngine> ();
		int wayPointIndex = ce.currentWaypoint;
		if (creatObstacleCarCounter < raceConfig.obstacleCarCreateSpace)
		{
			creatObstacleCarCounter += 1;
		} else
		{
			if (Random.Range (0, 100) < raceConfig.obstacleCarCreatePro && this.obstacleCars.Count < raceConfig.obstacleCarNum)
			{
				int creatWayPoint = (wayPointIndex + raceConfig.obstacleCarCreateDistance) % this.wayPoints.Count;
				this.CreatObstacleCarOnRoadPoint (creatWayPoint);
				creatObstacleCarCounter = 0;
			}
		}
		if (this.curCase == null)
		{
			if (this.creatItemCaseSpaceCounter < raceConfig.triggerGoldCaseCreatSpace)
			{
				this.creatItemCaseSpaceCounter += 1;
			} else
			{
				if (Random.Range (0, 100) < raceConfig.triggerGoldCaseCreatPro && this.triggerGoldCreateCases.Count > 0)
				{
					this.curCase = this.triggerGoldCreateCases [Random.Range (0, this.triggerGoldCreateCases.Count)];
					this.creatItemCaseSpaceCounter = 0;
					this.creatIndex = 0;
				}
			}
		} else
		{
			if (this.creatItemSpaceCounter < this.curCase.createSpace)
			{
				this.creatItemSpaceCounter += 1;
			} else
			{
				int creatPosition = this.curCase.createPositions [this.creatIndex];
				int creatWayPoint = (wayPointIndex + raceConfig.triggerGoldCreatDistance) % this.wayPoints.Count;
				if (creatPosition != 0)
				{
					this.CreatTriggerGoldOnRoadPoint (creatWayPoint, creatPosition, this.curCase.destroyIfMiss);
				}
				this.creatItemSpaceCounter = 0;
				this.creatIndex += 1;
				if (this.creatIndex >= this.curCase.createPositions.Count)
				{
					this.curCase = null;
				}
			}
		}
		for (int i=this.triggerItems.Count-1; i>=0; i--)
		{
			TriggerItemData tid = this.triggerItems [i];
			if (!tid.destroyIfMiss)
				continue;
			int value = wayPointIndex - tid.wayPointIndex;
			if (value < 0)
			{
				value += this.wayPoints.Count;
			}
			if (value < this.wayPoints.Count / 2 && value >= raceConfig.triggerGoldDestroyDistance)
			{
				this.triggerItems.RemoveAt (i);
				tid.Destroy ();
			}
		}
		 
		for (int i=this.obstacleCars.Count-1; i>=0; i--)
		{
			GameObject obstacleCar = this.obstacleCars [i];
			CarEngine occe = obstacleCar.transform.FindChild ("Engine").GetComponent<CarEngine> ();
			int value = wayPointIndex - occe.currentWaypoint;
			if (value < 0)
			{
				value += this.wayPoints.Count;
			}
			if (value < this.wayPoints.Count / 2 && value >= raceConfig.obstacleCarDestroyDistance)
			{
				this.obstacleCars.RemoveAt (i);
				this.RecoveryObstacleCar (obstacleCar);
			}
		}

		if (wayPoints.Contains (this.raceCounter.beginWaypoint))
		{
			//更新的路点中包含起始路点,表示车子已跑完一圈
//			this.raceCounter.roundNum += 1;
		}
		if (raceCounter.CurMode != RaceCounter.RaceMode.RM_Test)
		{
			Hashtable logicPar = new Hashtable ();
			logicPar.Add ("raceCounter", this.raceCounter);
			LogicManager.Instance.ActNewLogic<LogicRaceDetermine> (logicPar, this.OnDetermmineOver);
		}
	}

	/// <summary>
	/// Creats the gold create cases.
	/// 创建金币道具触发方案组,金币创建时从方案组中随机获取方案来生成金币道具
	/// </summary>
	void CreatGoldCreateCases ()
	{
		string[] cases = this.raceConfig.triggerGoldCases.Split ('#');
		foreach (string caseId in cases)
		{
			TriggerGoldCreateCaseConfigData tgccd = GameConfigDataBase.GetConfigData<TriggerGoldCreateCaseConfigData> (caseId);
			TriggerGoldCreateCase tgcc = new TriggerGoldCreateCase ();
			string[] positions = tgccd.createPositions.Split ('#');
			foreach (string position in positions)
			{
				tgcc.createPositions.Add (int.Parse (position));
			}
			tgcc.createSpace = tgccd.createSpace;
			tgcc.destroyIfMiss = true;
			this.triggerGoldCreateCases.Add (tgcc);
		}
	}

	void OnDetermmineOver (Hashtable logicPar)
	{
		if (raceCounter.curResult != RaceCounter.RaceResult.RR_Continue)
		{
//			Debug.LogError("结束了......");
			this.OverRace ();
		}
	}

	/// <summary>
	/// Creats the way points.
	/// 创建路点
	/// </summary>
	void CreatWayPoints ()
	{
		GameObject wayPointEditor = GameObject.Find ("WayPointsEditor");
		this.wayPoints.Clear ();
		this._reverseWayPoints.Clear ();
		GameObject WayPoints_create = GameObject.Find ("WayPoints_create");
		WayPoints_create.SetActive (false);
		if (WayPoints_create == null)
		{
			throw new UnityException ("no way points");
		}
		foreach (Transform tf in WayPoints_create.transform)
		{
			this.wayPoints.Add (tf);
		}
		GameObject re = (GameObject)GameObject.Instantiate (WayPoints_create);
		re.SetActive (false);
		foreach (Transform tf in re.transform)
		{
			this._reverseWayPoints.Insert (0, tf);
			Vector3 v = tf.forward;
			v.x = -v.x;
			v.z = -v.z;
			tf.forward = v;
		}
	}

	private int CarNumberIndex = 1;
	/// <summary>
	/// Creats the player car.
	/// 创建玩家自身的赛车
	/// </summary>
	void CreatPlayerCar ()
	{
		GameObject carPrefab = GameResourcesManager.GetRaceObject ("Car");
		GameObject car = (GameObject)GameObject.Instantiate (carPrefab);
		playerCarEngine = car.transform.FindChild ("Engine").GetComponent<CarEngine> ();
		CarEngine ce = playerCarEngine;

		MyGameProto.CarData cd = null;
		foreach (MyGameProto.CarData checkCd in MainState.Instance.playerInfo.carDatas)
		{
			if (checkCd.id == this.raceData.carId)
			{
				cd = checkCd;
			}
		}
		int speed = cd == null ? this.raceData.CarConfig.beginSpeed : this.raceData.CarConfig.GetValueOnSpeedLv (cd.speedLv);
		int acc = cd == null ? this.raceData.CarConfig.beginAcc : this.raceData.CarConfig.GetValueOnAccLv (cd.accLv);
		int handler = cd == null ? this.raceData.CarConfig.beginHandler : this.raceData.CarConfig.GetValueOnHandlerLv (cd.handlerLv);

		Debug.Log ("id:" + this.raceData.carId + ";maxSpeed=" + speed + ";acc=" + acc + ";handler=" + handler);
		ce.maxSpeed = speed;
		ce.baseTorque = acc;

		this.AddCarBody (ce, this.raceData.CarConfig.carAvt, this.raceData.RoleConfig.roleAvt);
		RaceManager.instance.RaceCounterInstance.reSetValues (ce);
		//UserRaceInfo.instance.reSetValues (ce);//初始化比赛数据.
		ce.carType = CarEngine.CarType.UserCar;
		//这里可以设置玩家车辆的属性;
		ce.currentWaypoint = 1;
		ce.isManualModel = true;
		Transform beginWayPoint = wayPoints [0];
		//这里可以设置车辆的出发路点;
		Vector3 v = beginWayPoint.position;
		v.y = 0;
		ce.transform.position = v;
		ce.carBody.transform.forward = beginWayPoint.transform.forward;
		ce.transform.rotation = Quaternion.FromToRotation (Vector3.forward, beginWayPoint.forward);
		allCars.Add (ce);
		raceCars.Add (ce);
		if (raceCounter.CurMode == RaceCounter.RaceMode.RM_ArderInfinite)
		{
			ce.addOil (ce.maxOil);
		}
//		ce.gameObject.AddComponent <CarAutoDriver>();
	}
	/// <summary>
	/// 取得指定车辆的当前圈数.1表示刚好一圈.1.25,表示一圈,还多跑了四分之一圈.
	/// </summary>
	/// <returns>如果找不到指定编号的车,则返回0.</returns>
	/// <param name="index">0表示玩家的车辆.</param>
	public float getCarRound (int index)
	{
		if (index >= getRaceCarNum ())
		{
			return 0;
		} else
		{
			return raceCars [index].getCurrentProgress ();
		}
	}
	/// <summary>
	/// 取得当前比赛车辆的数量.
	/// </summary>
	/// <returns>The race car number.</returns>
	public int getRaceCarNum ()
	{
		return raceCars.Count;
	}
	/// <summary>
	/// 取得指定用户的当前排名.0表示没有排名信息.1表示第一名.
	/// </summary>
	/// <returns>The race ranking.</returns>
	/// <param name="index">Index.</param>
	public int getRaceRanking (int index)
	{
		if (index >= raceCars.Count)
		{
			return 0;
		}
		validateRanking ();
		CarEngine c = raceCars [index];
		return lastRanking.IndexOf (c) + 1;
	}
	/// <summary>
	/// 验证并且更新车辆的新排行.
	/// </summary>
	void validateRanking ()
	{
		if (lastRanking.Count == raceCars.Count)
		{
			bool err = false;
			if (raceCars.Count > 1)
			{
				int l = lastRanking.Count - 1;
				for (int i=0; i<l; i++)
				{
					if (lastRanking [i].getCurrentProgress () < lastRanking [i + 1].getCurrentProgress ())
					{
						err = true;
						break;
					}
				}
			}
			if (!err)
			{
				return;
			}
		} else
		{
			for (int i=lastRanking.Count; i<raceCars.Count; i++)
			{
				lastRanking.Add (raceCars [i]);
			}
		}
		lastRanking.Sort (sortCar);
	}

	int sortCar (CarEngine car1, CarEngine car2)
	{
		float f1 = car1.getCurrentProgress ();
		float f2 = car2.getCurrentProgress ();
		if (f1 == f2)
		{
			return 0;
		} else if (f1 > f2)
		{
			return -1;
		} else
		{
			return 1;
		}
	}
	/// <summary>
	/// Sets the car camera.
	/// 设置相机,绑定在玩家车辆上
	/// </summary>
	void SetCarCamera ()
	{
		this.carCamera = GameObject.Find ("CarCamera");
		SmoothFollow sf = this.carCamera.GetComponent<SmoothFollow> ();
		Transform cetf = playerCarEngine.transform;
		sf.target = cetf;
	}

	/// <summary>
	/// Creats the obstacle car on road point.
	/// 在某个路点创建障碍车(在比赛过程中动态调用创建)
	/// </summary>
	/// <param name="wayPointIndex">Way point index.</param>
	void CreatObstacleCarOnRoadPoint (int wayPointIndex)
	{
		return;
		Transform wayPoint = this.WayPoints [wayPointIndex];
		Vector3 roadRight = Vector3.Cross (Vector3.up, wayPoint.forward);
		float randOffset = Random.Range (this.raceConfig.minOffset, this.raceConfig.maxOffset);
		Vector3 setPosition = wayPoint.position + roadRight * randOffset;

		GameObject obstacleCar = this.CreatObstacleCar ();
		CarEngine ce = obstacleCar.transform.FindChild ("Engine").GetComponent<CarEngine> ();
		ce.currentWaypoint = wayPointIndex;
		ce.transform.position = setPosition;
		ce.transform.rotation = Quaternion.FromToRotation (Vector3.forward, wayPoint.forward);
		ce.BeginRun ();
		this.obstacleCars.Add (obstacleCar);
	}
	/// <summary>
	/// 在指定道路位置创建一个对象.
	/// </summary>
	/// <returns>The object in road.</returns>
	/// <param name="index">Index.</param>
	/// <param name="xOffset">X offset.</param>
	/// <param name="obj">Object.</param>
	public GameObject CreateObjInRoad (int index, float xoffset, GameObject obj, bool isCreateOnOver=false)
	{
		if (obj == null || (!isCreateOnOver && isOver))
		{
			return null;
		} else
		{
			GameObject ce = (GameObject)GameObject.Instantiate (obj);
			xoffset = getSafeRoadOffset (xoffset, 2);
			Transform wayPoint = getWayPoint (index); //this.WayPoints [0];
			Vector3 roadRight = Vector3.Cross (Vector3.up, wayPoint.forward);
			Vector3 setPosition = wayPoint.position + roadRight * xoffset;
			Vector3 v = setPosition;
			v.y = 0;
			ce.transform.position = v;
			ce.transform.position = setPosition;
			ce.transform.forward = wayPoint.forward;
			return ce;
		}
	}
	/// <summary>
	/// 在场景的指定位置创建一辆车.
	/// </summary>
	/// <returns>The obstacle car.</returns>
	/// <param name="pos">Position.</param>
	/// <param name="isAI">是ai车还是普通障碍车.</param>
	/// <param name="speed">当前的移动速度.0表示停止,-100表示逆行的车辆.</param>
	/// <param name="autoStart">If set to <c>true</c> auto start.</param>
	public CarEngine CreateObstacleCar (int index, float xoffset, bool isAI, float speed, string carModel=null, string roleModel=null)
	{
		if (isOver)
		{
			return null;
		}
		xoffset = getSafeRoadOffset (xoffset, 2);
		Transform wayPoint = getWayPoint (index); //this.WayPoints [0];
		Vector3 roadRight = Vector3.Cross (Vector3.up, wayPoint.forward);
		Vector3 setPosition = wayPoint.position + roadRight * xoffset;
		
		GameObject obstacleCarPrefab = GameResourcesManager.GetRaceObject ("Car");
		GameObject obstacleCar = (GameObject)GameObject.Instantiate (obstacleCarPrefab);
		GameObject ceObj = obstacleCar.transform.FindChild ("Engine").gameObject;
		
		ceObj.layer = LayerMask.NameToLayer ("ObstacleCar");
		
		CarEngine ce = ceObj.GetComponent<CarEngine> ();
		if (carModel == null)
		{
			carModel = "CarAvt3";
		}
		if (roleModel == null)
		{
			roleModel = "RoleAvt1";
		}
		this.AddCarBody (ce, carModel, roleModel);
		ce.carType = isAI ? CarEngine.CarType.AICar : CarEngine.CarType.OtherCar;
		if (ce.carType == CarEngine.CarType.AICar)
		{
			ce.gameObject.AddComponent<CarAutoDriver> ();
			raceCars.Add (ce);
		}
		//这里可以设置障碍车辆的属性;
		ce.baseTorque = 500;
		ce.maxSpeed = 120;
		ce.currentWaypoint = (speed >= 0) ? index : getReverseIndex (index);
		Vector3 v = setPosition;
		v.y = 0;
		ce.transform.position = v;
		ce.transform.position = setPosition;
		ce.carBody.transform.position = setPosition;
//		ce.transform.rotation = Quaternion.FromToRotation (Vector3.forward, wayPoint.forward);
		ce.transform.forward = speed >= 0 ? wayPoint.forward : -wayPoint.forward;
		ce.carBody.transform.forward = ce.transform.forward;
		allCars.Add (ce);
		speed = Mathf.Clamp (speed, -ce.maxSpeed, ce.maxSpeed);
		if (speed != 0)
		{
			ce.setWayPoints (speed > 0);
			ce.BeginRun ();
		}
		obstacleCarPool.Add (obstacleCar);
		return ce;
	}
	/// <summary>
	/// 所有车的特效.
	/// </summary>
	/// <returns>The all car list.</returns>
	public List<CarEngine> getAllCarList ()
	{
		return allCars;
	}

	List<GameObject> obstacleCarPool = new List<GameObject> ();

	GameObject CreatObstacleCar ()
	{
			
		if (isOver)
		{
			return null;
		}
//		if (obstacleCarPool.Count > 0)
//		{
//			GameObject obstacleCar = obstacleCarPool [0];
//			obstacleCar.SetActive (true);
//			obstacleCarPool.RemoveAt (0);
//			return obstacleCar;
//		} else
//		{
		GameObject obstacleCarPrefab = GameResourcesManager.GetRaceObject ("Car");
		GameObject obstacleCar = (GameObject)GameObject.Instantiate (obstacleCarPrefab);
		GameObject ceObj = obstacleCar.transform.FindChild ("Engine").gameObject;
		ceObj.layer = LayerMask.NameToLayer ("ObstacleCar");

		CarEngine ce = ceObj.GetComponent<CarEngine> ();

		this.AddCarBody (ce, "CarAvt7", "RoleAvt1");
		ce.carType = CarEngine.CarType.AICar;
		//这里可以设置障碍车辆的属性;
		ce.baseTorque = 500;
		ce.maxSpeed = 120;
		return obstacleCar;
//		}
	}

	void RecoveryObstacleCar (GameObject obstacleCar)
	{
		if (obstacleCarPool.Count >= RaceConfig.obstacleCarNum)
		{
			GameObject.Destroy (obstacleCar);
		} else
		{
			obstacleCar.SetActive (false);
			CarEngine ce = obstacleCar.transform.FindChild ("Engine").GetComponent<CarEngine> ();
			ce.StopImmediately ();
			obstacleCarPool.Add (obstacleCar);
		}
	}

	/// <summary>
	/// Creats the trigger gold on road point.
	/// 在某个路点创建金币触发道具
	/// </summary>
	/// <param name="wayPointIndex">Way point index.</param>
	/// <param name="createPosition">Create position.</param>
	/// <param name="destroyIfMiss">If set to <c>true</c> destroy if miss.</param>
	void CreatTriggerGoldOnRoadPoint (int wayPointIndex, int createPosition, bool destroyIfMiss)
	{
		Transform wayPoint = this.WayPoints [wayPointIndex];
		Vector3 roadRight = Vector3.Cross (Vector3.up, wayPoint.forward);
		float offset = (this.raceConfig.maxOffset - this.raceConfig.minOffset) * createPosition / 6 + this.raceConfig.minOffset;
		Vector3 setPosition = wayPoint.position + roadRight * offset;

		GameObject triggerItem = this.CreatTriggerItem ();
		triggerItem.transform.position = setPosition;
		TriggerItemData tid = new TriggerItemData ();
		tid.triggerItem = triggerItem;
		tid.destroyIfMiss = destroyIfMiss;
		tid.wayPointIndex = wayPointIndex;
		this.triggerItems.Add (tid);
	}
	
	public class TriggerItemData
	{
		public GameObject triggerItem;
		public bool destroyIfMiss;
		public int wayPointIndex;

		public void Destroy ()
		{
			RaceManager.Instance.RecoveryTriggerItem (triggerItem);
			triggerItem = null;
		}
	}

	GameObject CreatTriggerItem ()
	{
		if (triggerItemPool.Count > 0)
		{
			GameObject triggerItem = triggerItemPool [0];
			triggerItem.SetActive (true);
			triggerItemPool.RemoveAt (0);
			return triggerItem;
		} else
		{
			GameObject triggerItemPrefab = GameResourcesManager.GetRaceObject ("TriggerGold");
			GameObject triggerItem = (GameObject)GameObject.Instantiate (triggerItemPrefab);
			return triggerItem;
		}
	}

	List<GameObject> triggerItemPool = new List<GameObject> ();
	int triggerItemPoolSize = 20;

	void RecoveryTriggerItem (GameObject triggerItem)
	{
		if (triggerItemPool.Count >= triggerItemPoolSize)
		{
			GameObject.Destroy (triggerItem);
		} else
		{
			triggerItem.SetActive (false);
			triggerItemPool.Add (triggerItem);
		}
	}


	public enum InputType:int
	{
		it_leftDown=-1,
		it_rightDown=1,
		it_noInput=0,
	}

	/// <summary>
	/// Adds the car body.
	/// 添加车身,包括车身模型和人物模型
	/// </summary>
	/// <param name="ce">Ce.</param>
	/// <param name="carAvtPath">Car avt path.</param>
	/// <param name="roleAvtPath">Role avt path.</param>
	void AddCarBody (CarEngine ce, string carAvtPath, string roleAvtPath)
	{
		GameObject carAvtPrefab = GameResourcesManager.GetCarAvtPrefab (carAvtPath);
		GameObject carTarget = (GameObject)GameObject.Instantiate (carAvtPrefab);
		carTarget.name = "car";
		carTarget.transform.parent = ce.carBody;
		carTarget.transform.localPosition = Vector3.zero;
		carTarget.transform.localRotation = Quaternion.Euler (Vector3.zero);
		carTarget.transform.localScale = Vector3.one;
		
		GameObject roleAvtPrefab = GameResourcesManager.GetRolePrefab (roleAvtPath);
		GameObject roleTarget = (GameObject)GameObject.Instantiate (roleAvtPrefab);
		roleTarget.name = "role";
		roleTarget.transform.parent = carTarget.transform.FindChild ("car_transform/car_role");
		roleTarget.transform.localPosition = Vector3.zero;
		roleTarget.transform.localRotation = Quaternion.Euler (Vector3.zero);
		roleTarget.transform.localScale = Vector3.one;
		
		ce.flWheel = carTarget.transform.FindChild ("car_FL");
		ce.frWheel = carTarget.transform.FindChild ("car_FR");
		ce.rlWheel = carTarget.transform.FindChild ("car_BL");
		ce.rrWheel = carTarget.transform.FindChild ("car_BR");
	}

	InputType it;

	/// <summary>
	/// Gets it.
	/// 获取输入操作类型
	/// </summary>
	/// <value>It.</value>
	public InputType It
	{
		get
		{
			return it;
		}
	}

	public void OnInput (InputType it)
	{
		this.it = it;
	}
	/// <summary>
	/// 找到距离指定位置最近的路点.
	/// </summary>
	/// <returns>The nearest way port.</returns>
	public int getNearestWayPort (Vector3 pos)
	{
		int curr = 0;
		int index = 0;
		float min = getWayPointDistance (pos, index);
		int le = (int)Mathf.Ceil (wayPoints.Count / 2);
		float value = 0;
		for (int i=1; i<le; i++)
		{
			value = getWayPointDistance (pos, curr + i);
			if (min > value)
			{
				index = curr + i;
				min = value;
			}
			value = getWayPointDistance (pos, curr - i);
			if (min > value)
			{
				index = curr + i;
				min = value;
			}
			if (min <= 0)
			{
				break;
			}
		}
		return index; 
	}
	/// <summary>
	/// 取得某个路点到指定位置的距离.
	/// </summary>
	/// <returns>The distance.</returns>
	/// <param name="pos">Position.</param>
	/// <param name="index">Index.</param>
	private float getWayPointDistance (Vector3 pos, int index)
	{
		return Vector3.Distance (pos, getWayPoint (index).position);
	}
	/// <summary>
	/// 根据路点的索引取得指定的路点.
	/// </summary>
	/// <returns>The way point.</returns>
	/// <param name="index">Index.</param>
	public Transform getWayPoint (int index)
	{
		return wayPoints [trimWayProintIndex (index)];
	}
	/// <summary>
	/// 规范化路点的索引.
	/// </summary>
	/// <returns>The way proint index.</returns>
	/// <param name="index">Index.</param>
	public int trimWayProintIndex (int index)
	{
		if (wayPoints.Count == 0)
		{
			return index;
		} else
		{
			return (index + wayPoints.Count) % wayPoints.Count;
		}
	}

	public int getReverseIndex (int index)
	{
		return trimWayProintIndex (wayPoints.Count - 1 - index);
	}
	/// <summary>
	/// 判断某个偏移量是不是在道路的安全范围内.
	/// </summary>
	/// <returns><c>true</c>, if in safe road offset was ised, <c>false</c> otherwise.</returns>
	/// <param name="offset">Offset.</param>
	/// <param name="safeValue">安全值.1表示左右各保留1米的安全距离.</param>
	public bool isInSafeRoadOffset (float offset, float safeValue)
	{
		return MathUtils.isInRange (offset, this.raceConfig.minOffset + safeValue, this.raceConfig.maxOffset - safeValue);
	}

	public float getSafeRoadOffset (float offset, float safeValue)
	{
		return Mathf.Clamp (offset, this.raceConfig.minOffset + safeValue, this.raceConfig.maxOffset - safeValue);
	}

	public int wayPointNumber
	{
		get
		{
			return wayPoints.Count;
		}
	}
	/// <summary>
	/// 开启虚拟现实模式.
	/// </summary>
	/// <param name="open">If set to <c>true开,false关</c> open.</param>
	public void openVRModel (bool open)
	{
		VRCamera v = carCamera.GetComponent <VRCamera> ();
		if (v == null)
		{
			v = carCamera.AddComponent<VRCamera> ();
		}
		v.enabled = open;
	}
	/// <summary>
	/// 销毁指定的车.
	/// </summary>
	/// <param name="en">En.</param>
	public void destoryCar (CarEngine en)
	{
		allCars.Remove (en);
		obstacleCars.Remove (en.transform.root.gameObject);
		GameObject.DestroyObject (en.transform.root.gameObject);
	}
	/// <summary>
	/// 显示一个黑色背景的屏幕tips.如果之前已经有显示,则替换.3秒钟内如果没有新tips,则自动关闭消失.
	/// </summary>
	/// <param name="info">Info.</param>
	public void showScreenTips (string info)
	{
		ContainerStoryduihuakuangUIController.Instance.ShowMiddleTips (info);
	}
	/// <summary>
	/// 关闭屏幕提示.
	/// </summary>
	public void hiddrenScreenTips ()
	{
		ContainerStoryduihuakuangUIController.Instance.HideMiddleTips ();
	}
	/// <summary>
	/// 进入对话模式,隐藏所有UI.增加一个灰色透明的遮罩遮住场景.突出对话UI.
	/// </summary>
	public void startChat ()
	{
		ContainerStoryduihuakuangUIController.Instance.Begin (null);
	}
	/// <summary>
	/// 显示一个对话.如果之前没有调用startChat(),也可以直接显示对话.但是不显示进入对话模式的效果(不遮挡背景,不隐藏UI).
	/// </summary>
	/// <param name="roleName">显示的说话角色的名字,null或空字符则不显示名字</param>
	/// <param name="image">图片的名称.直接显示这个头像图片.null或空字符则不显示.</param>
	/// <param name="pos">true头像显示在左边,false显示在右边</param>
	/// <param name="info">要显示的对话内容</param>
	public void showChat (string roleName, string image, bool pos, string info)
	{
		ContainerStoryduihuakuangUIController.Instance.ShowChat (roleName, image, pos, info);
	}
	/// <summary>
	/// 结束对话模式.去掉遮罩,如果进入对话模式时,有显示ui则显示UI.
	/// </summary>
	public void endChat ()
	{
		ContainerStoryduihuakuangUIController.Instance.EndChat ();
	}
	/// <summary>
	/// 显示一个图片提示.居中在屏幕中间.如果原来就有则替换.并且要灰掉场景
	/// </summary>
	/// <param name="image">Image.</param>
	public void showImageTips (string image)
	{
		ContainerStoryduihuakuangUIController.Instance.showImageTips (image);
	}
	/// <summary>
	/// 关闭一个图片提示.
	/// </summary>
	public void hiddenImageTips ()
	{
		ContainerStoryduihuakuangUIController.Instance.hiddenImageTips ();
	}
	/// <summary>
	/// 在UI界面中显示一次连击提示.
	/// </summary>
	/// <param name="hitTimes">当前的连击次数</param>
	/// <param name="totalScore">累计的连击分数</param>
	/// <param name="currentScore">此次获得的分数</param>
	public void showMultiHit (int hitTimes, int totalScore, int currentScore)
	{
		Debug.Log (hitTimes + "连击,+" + currentScore + ",共:" + totalScore);

		if (RaceManager.instance.raceCounter.CurMode == RaceCounter.RaceMode.RM_ArderDestroy 
			|| RaceManager.instance.raceCounter.CurMode == RaceCounter.RaceMode.RM_Destroy)
			//PanelMainUIController.Instance.ShowUIMsgBox ("破坏 " + hitTimes + "连击, +" + currentScore + ",共:" + totalScore + "分", 1.0f);
			ContainerLianjiUIController.Instance.ShowMsg(hitTimes.ToString(),1.0f);
	}
	/// <summary>
	/// 进入剧情模式关卡.
	/// </summary>
	/// <param name="page">P第几页,第几章的意思.</param>
	/// <param name="level">第几个关卡.</param>
	public void startStoryGame (int page, int level)
	{
		Application.targetFrameRate = 60;
		string id = FindStoryIdByPageAndLevel (page, level);
		Hashtable logicPar = new Hashtable ();
		logicPar.Add ("storyId", id);
		LogicManager.Instance.ActNewLogic<LogicEnterRace> (logicPar, null);
	}

	string FindStoryIdByPageAndLevel (int page, int level)
	{
		List<StoryConfigData> lists = StoryConfigData.GetConfigDatas<StoryConfigData> ();
		foreach (StoryConfigData scd in lists)
		{
			if (scd.chapterIndexValue == page && scd.stageIndexValue == level)
			{
				return scd.id;
			}
		}
		throw new UnityException ("the story not exist");
		return "";
	}

	/// <summary>
	/// 进入普通比赛模式.
	/// 通过地图ID和模式确定赛程ID
	/// </summary>
	/// <param name="mapIndex">第几个地图</param>
	/// <param name="modelIndex">第几个模式.</param>
	public void startRaceGame (int mapIndex, int modelIndex)
	{
		string id = (modelIndex * 100 + mapIndex).ToString ();
		this.startRaceGame (id);
	}

	/// <summary>
	/// Starts the race game.
	/// 直接通过赛程ID启动比赛
	/// </summary>
	/// <param name="raceId">Race identifier.</param>
	public void startRaceGame (string raceId)
	{
		Application.targetFrameRate = 60;
		Hashtable logicPar = new Hashtable ();
		logicPar.Add ("raceId", raceId);
		LogicManager.Instance.ActNewLogic<LogicEnterRace> (logicPar, null);
	}
}
