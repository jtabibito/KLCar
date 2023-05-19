using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 车的核心控制类.
/// 车的结构:
/// Car -
/// 	-CarBody	车的显示车体.
/// 		-car	显示的车模型.
/// 			-car_mod	车架
/// 			-car_BL		后左轮
/// 			-car_BR		后右轮
/// 			-car_FL		前左轮
/// 			-car_FR		前右轮
/// 		-role	显示的人物
/// 		-pet	显示的宠物
/// 		-effect	显示的各种车体技能特效.
/// 	-engine		车的真实物理引擎.
/// 		-effect	显示跟随物理引擎的特效.
/// </summary>
public class CarEngine : MonoBehaviour
{
	public enum CarType
	{
		UserCar,AICar,OtherCar
	}

	/// <summary>
	/// 车辆的碰撞级别.级别越高,越容易将对方撞飞.相同级别,用默认的碰撞效果.玩家为0.杂车<0.
	/// </summary>
	public int hitLevel;
	/// <summary>
	/// 车的特殊状态.通过一个32位整形表示车的32种特殊状态.每一位表示一个状态.
	/// </summary>
//	private int carState = 0;
	private StateMap<int> carState = new StateMap<int> ();
	public GameObject skill;
	bool isRun = false;
//		public AudioClip collisionSound;
	public Transform carBody;
	public GameObject respawnEffect;
//	public bool isAI;
	/// <summary>
	/// 车的类型.
	/// </summary>
	public CarType carType;
	
	// 轮子碰撞体
	public WheelCollider flWheelCollider;
	public WheelCollider frWheelCollider;
	public WheelCollider rlWheelCollider;
	public WheelCollider rrWheelCollider;
	
	// 轮子
	public Transform flWheel;
	public Transform frWheel;
	public Transform rlWheel;
	public Transform rrWheel;
	// 轮子进行转向的盒子.
	private Transform flWheelBox;
	private Transform frWheelBox;
	private Transform rlWheelBox;
	private Transform rrWheelBox;
	// 车子的基本属性    
	public float baseTorque = 150.0f;            // 基本推力，决定加速度
	public float baseBrakeTorque = 500.0f;         // 基本制动力
	public float maxSpeed = 200.0f;             // 最大速度
	public float manualHorzSpeed = 10f;           // 手动转向速度
	public float autoHorzSpeed = 10f;           // 碰撞造成的转向速度
	
	//碰撞参数
	public float bouncingRoadBrakeFactory = 0.2f;  //碰撞路边的制动参数
	public float bouncingRoadSteerFactory = 1f;   //碰撞路边的转向参数
	public float bouncingCarBrakeFactory = 0.5f;  //碰撞车子的制动参数
	public float bouncingCarSteerFactory = 0.5f;  //碰撞车子的转向参数
	
	// 赛道属性 （应该放到赛道里面）
	public int nRail = 9;
	public float gapBetweenRails = 1f;
	public float manualChangeRailFactor = 4f;
	
	////////////////////////////////////////////////////
	// 调试属性(隐藏)
	public float currentSpeed = 0.0f;
	public bool showDebug = false;
	public float centerOfMassY = 0;
	public int curRail = 0;
	private int _currentWaypoint = 0;
	/**
		 * 当前的路点.
		 */
	public Transform waypoint;
	
	//运行中的车辆属性
	/// <summary>
	/// T前碰撞造成的减速力
	/// </summary>
	public float _curBrakeFactor = 0f;//当前碰撞造成的减速力
	/// <summary>
	/// 当前碰撞造成的转向力
	/// </summary>
	private float curSteerFactor = 0f;//
	private float curSteerInputValue = 0;
	/// <summary>
	/// 当前车的扭矩.
	/// </summary>
	private float currentSteerAngle = 0f;
	/// <summary>
	/// 路点的扭矩
	/// </summary>
	private float currentTargetAngle = 0f;
	private bool bMannualSteering = false;
	
	// 路点
	public float horzSpeed = 10f;
	public int wpCheckFactory = 1;//路点判定系数
	private const float sqrDistanceToWaypoint = 36;
	private List<Transform> waypoints ;
	/**
		 * 当前路点的右方向.
		 */
	private Vector3 roadRight;
	/**
		 * 当前路点的前方向.
		 */
	private Vector3 roadForward;
	/**
	 * 车子与路点的偏移量.
	 */
	private float _horzOffsetFromWayPoint = 0;
	private bool bInitOffsetHorz = false;
	
	// 显示
	private GameObject wpTargetIndicator;
	private GameObject carTargetIndicator;
	private GameObject roadDirIndicator;
	private GameObject carDirIndicator;
	private Quaternion originEngineRotation;
	private float carBodyOffsetY = 0;
	/// <summary>
	/// 最大油量.
	/// </summary>
	public float maxOil=100;
	/// <summary>
	/// 燃油消耗.每秒钟的消耗量.
	/// </summary>
	public float oilWear=1;
//	/**
//	 * 车辆当前已经行驶的圈数.
//	 */
//	private float totalRound;
	public bool IsRun
	{
		get
		{
			return isRun;
		}
	}
	
	//    // 碰撞相关
	//    // 与路边相擦
	//    private bool isBouncingRoad = false;
	//    private int bounceDirFromRoadSide = 0;
	//    private float bounceRoadDistance = 0f;
	//    public float bounceRoadSpeed = 10f;
	//
	//    // 与车相撞
	//    private bool isBouncingCar = false;
	//	  private float bounceCarD;
	//    private float bounceCarTotalTime = 1f;
	//    private float bounceCarTime = 0f;
	
	//碰撞设定
	private bool isBouncing = false;
	private float bouncingTime = 0f;
	public float bounceTotalTime = 0.5f;
	private float bounceSpeed = 10f;
	
	
	// 胎痕位置
	private Vector3 lastSkidmarkPosR;
	private Vector3 lastSkidmarkPosL;
	/// <summary>
	/// 原来默认的角色动画控制器.
	/// </summary>
	private RoleAvtController _lastController;
	/// <summary>
	/// 角色动画控制器.
	/// </summary>
	private RoleAvtController _avtController;
	/// <summary>
	/// 当前车辆是不是使用手动模式.如果是,则需要手动操作漂移.
	/// </summary>
	public bool isManualModel=false;

	private CarEffectsConfig carEffect;
	public int currentWaypoint
	{
		get
		{
			return _currentWaypoint;
		}
		set
		{
			_currentWaypoint=RaceManager.Instance.trimWayProintIndex(value);
		}
	}
	// 重置相关
	private RespawnState respawnState = RespawnState.none; // 0 正常 1 开始播放重生动画 2 播放中
	enum RespawnState
	{
		none,
		ready,
		respawning
	}
	private float respawnTime = 0f;
	private float stuckTime = 0;
	private const float respawnDuration = 1f;
	//private Dictionary<CarState.CarStateType,CarState> stateDic = new Dictionary<CarState.CarStateType, CarState> ();
	/**
		 * 当前车辆是不是在弯道里.0表示直线.-1,1分别表示弯道.
		 */
	private int roadCurve;
	private int _currentGetGlod;
	private float _currentOil=-0.00001f;
	/// <summary>
	/// 当前圈数.
	/// </summary>
	private int _currentRoad;
	private int _currentScore;
	private int _currentGainItem;
	private int _roundNum;
	/// <summary>
	/// 用来装特效的盒子.
	/// </summary>
	public Transform effectBox;
	public Transform petEffectBox;
	/// <summary>
	/// 一个跟随车的特效盒子,放入到flow盒子中的对象,将会跟随车辆运动.但是不会跟随车辆转弯.
	/// 比如:护盾的效果不会跟随车辆转弯.
	/// </summary>
	private Transform _flowBox;
	/// <summary>
	/// 获得指定数量的金币.
	/// </summary>
	/// <param name="value">Value.</param>
	public void addGlod (int value)
	{
		_currentGetGlod += value;
	}
	/// <summary>
	/// 一个跟随车的特效盒子,放入到flow盒子中的对象,将会跟随车辆运动.但是不会跟随车辆转弯.
	/// 比如:护盾的效果不会跟随车辆转弯.
	/// </summary>
	/// <value>The flow box.</value>
	public Transform flowBox
	{
		get
		{
			return _flowBox;
		}
	}

	/// <summary>
	/// 车相对于赛道中线的水平偏移.
	/// </summary>
	/// <returns>The offset by way point.</returns>
	public float xOffsetByWayPoint 
	{
		get
		{
			if(waypoint==null)
			{
				return 0;
			}else{
				Transform point=waypoint;
				Vector3 of= transform.position-point.position;
				return Vector3.Dot(of,point.right);
			}
		}
	}
	public int currentGainItem
	{
		get
		{
			return _currentGainItem;
		}
	}
	public float horzOffsetFromWayPoint
	{
		get
		{
			return _horzOffsetFromWayPoint;
		}
		set
		{
			value=RaceManager.Instance.getSafeRoadOffset(value,0);
			_horzOffsetFromWayPoint=value;
		}
	}
	public int roundNum
	{
		get
		{
			return _roundNum;
		}
	}

	public int currentGetGlod
	{
		get
		{
			return _currentGetGlod;
		}
	}

	public float currentOil
	{
		get
		{
			return _currentOil;
		}
	}
	/// <summary>
	/// 增加一定量的油.
	/// </summary>
	/// <param name="value">Value.</param>
	public void addOil(float value)
	{
		if (_currentOil < 0&&value<0)
			return;
		_currentOil+=value;
		if (_currentOil > maxOil)
		{
			_currentOil = maxOil;
		} else if (_currentOil <= 0)
		{
			 //为了防止为负数的时候,表示当前场景油量不影响汽车行驶.0.000001表示下一帧就会把油耗完.
			_currentOil=0.000001f;
		}
	}
	/// <summary>
	/// 上一次击中的时间.
	/// </summary>
	private float lastHitTime = 0;
	/// <summary>
	/// 总共的连击分数.
	/// </summary>
	private int totalHitScore = 0;
	/// <summary>
	/// 当前累计的连击分数.
	/// </summary>
	private int totalHitTimes = 0;
	/// <summary>
	/// 增加破坏分数.
	/// </summary>
	/// <param name="value">Value.</param>
	public void addBreakScore(int value)
	{
		if (Time.time-lastHitTime>2)
		{
			onMultiHitOver();
		}
		lastHitTime = Time.time;
		totalHitTimes++;
		if (totalHitTimes > 1)
		{
			value*=(int)(1+totalHitTimes/100.0f);
		} else
		{

		}
		totalHitScore += value;
		_currentScore += value;
		if (carType==CarType.UserCar)
		{
			RaceManager.Instance.showMultiHit (totalHitTimes, totalHitScore, value);
		}
	}
	private void onMultiHitOver()
	{
		totalHitScore = 0;
		totalHitTimes = 0;
	}
	public int currentRoad
	{
		get
		{
			return _currentRoad;
		}
	}
	/// <summary>
	/// 当前圈数的百分百.1.5表示走了1圈半.
	/// </summary>
	/// <returns>The current progress.</returns>
	public float getCurrentProgress()
	{
		if (waypoints == null)
		{
			return 0;
		} else
		{
			return currentRoad + (float)currentWaypoint / waypoints.Count;
		}
	}
	public int currentScore
	{
		get
		{
			return _currentScore;
		}
	}

	void Awake ()
	{
		_flowBox = new GameObject ("flowBox").transform;
		_flowBox.parent = transform.parent;		 
		effectBox = carBody.FindChild ("effect");
		if (effectBox == null)
		{
			effectBox = new GameObject ().transform;
			effectBox.parent = carBody;
			effectBox.name = "effect";
			effectBox.localPosition = Vector3.zero;
		}
		petEffectBox = carBody.FindChild ("petEffect");
		if (petEffectBox == null)
		{
			petEffectBox = new GameObject ().transform;
			petEffectBox.parent = carBody;
			petEffectBox.name = "petEffect";
			petEffectBox.localPosition = Vector3.zero;
		}
		rigidbody.centerOfMass = new Vector3 (0, centerOfMassY, 0);
		carBodyOffsetY = carBody.position.y - transform.position.y;
		if (CarEffectsConfig.DEFUALT_CONFIG == null)
		{
			CarEffectsConfig.DEFUALT_CONFIG = (CarEffectsConfig)GetComponent<CarEffectsConfig> ();
//						CarEffectsConfig.DEFUALT_CONFIG=(CarEffectsConfig)CarEffectsConfig.Instantiate(GetComponent<CarEffectsConfig>());
		}
				 
	}

	public RoleAvtController roleAvtConteoller
	{
		get
		{
			if (_lastController == null)
			{
				Transform obj= carBody.FindChild ("car/car_transform/car_role/role");
				if(obj==null)
				{
					Debug.LogError("车体中没有添加角色.请把角色添加到car/car_transform/car_role/role");
				}else
				{
					_lastController =obj.gameObject.GetComponent<RoleAvtController> ();
					if(_lastController==null)
					{
						_lastController=obj.gameObject.AddComponent<RoleAvtController>();
					}
				}
			}
			if (_avtController == null)
			{
				return _lastController;
			} else
			{
				return _avtController;
			}
		}
		 
	}
	/// <summary>
	/// 当前是不是宠物模式.没有显示玩家的动作.
	/// </summary>
	/// <value><c>true</c> if is pet model; otherwise, <c>false</c>.</value>
	public bool isPetModel
	{
		get{
			return _avtController!=_lastController;
		}
	}
	/// <summary>
	/// 零时改变车的角色动画控制系统.
	/// </summary>
	/// <param name="avt">null表示恢复原来的动画系统</param>
	public void changeRoleAvtController (RoleAvtController avt)
	{
		if (avt!=null&&avt!=_lastController)
		{
			avt.replayce(_lastController);
			_avtController = avt;
		} else
		{
			_avtController = avt;
		}
	}

	/// <summary>
	/// 设置车的零时状态.包括无敌,隐身.
	/// </summary>
	/// <param name="pos">CarState类中取值.</param>
	/// <param name="value">If set to <c>true</c> value.</param>
	public void setCarState (int pos, bool value)
	{
//		carState = MathUtils.setBinaryValue (carState, pos, value);
		if (value)
		{
			carState.addState(pos);
		} else
		{
			carState.removeState(pos);
		}
	}
	/// <summary>
	/// 取得车的状态.
	/// </summary>
	/// <returns><c>true</c>, if car state was gotten, <c>false</c> otherwise.</returns>
	/// <param name="pos">Position.</param>
	public bool getCarState (int pos)
	{
//		return MathUtils.getBinaryValue (carState, pos);
		return carState.isHasState (pos);
	}
	/// <summary>
	/// 车辆当前是否包含指定状态.
	/// </summary>
	/// <returns><c>true</c>, if in car state was ised, <c>false</c> otherwise.</returns>
	/// <param name="mask">Mask.</param>
	public bool isInCarState(int mask)
	{
		bool ok = false;
		for (int i=0; i<32; i++)
		{
			bool b=(mask&(1<<i))!=0;
			if(b)
			{
				if(getCarState(i))
				{
					ok=true;
				}else
				{
					return false;
				}
			}
		}
		return ok;
//		return (mask & carState)!=0;
	}
	void Start ()
	{
		_currentRoad = 0;
		originEngineRotation = transform.localRotation;
		carEffect = (transform.parent.FindChild ("CarBody/car")).GetComponent<CarEffectsConfig> ();
		if (carEffect == null)
		{
			carEffect = GetComponent<CarEffectsConfig> ();
		}
		carEffect.carEngine = this;
		flWheelBox = createWheelBox (flWheel);
		frWheelBox = createWheelBox (frWheel);
		rlWheelBox = createWheelBox (rlWheel);
		rrWheelBox = createWheelBox (rrWheel);
		carBody.position = transform.position;
		carBody.localRotation = transform.localRotation;
	}
	/// <summary>
	/// C给轮胎外面添加一个转向的盒子.
	/// </summary>
	/// <returns>The wheel box.</returns>
	/// <param name="wheel">Wheel.</param>
	Transform createWheelBox (Transform wheel)
	{
		GameObject box = new GameObject (wheel.name + "Box");
		box.transform.parent = wheel.parent;
		box.transform.localRotation = wheel.localRotation;
		box.transform.localPosition = wheel.localPosition;
		wheel.parent = box.transform;
		return box.transform;
	}

	public void BeginRun ()
	{
		if (waypoints == null)
		{
			setWayPoints (true);
		}
//		startAccelerationX = Input.acceleration.x;
		this.isRun = true;
	}
	
	public void StopImmediately ()
	{
		this.rigidbody.velocity = Vector3.zero;
		this.isRun = false;
	}
	
 
	
	void FixedUpdate ()
	{
		if (isRun)
		{
			//UpdateCarStates ();
			
			ApplyTorque ();
			
			ApplySteer ();
			
			CheckStum ();
			
			ApplyBouncing ();
			
			ApplyInput ();
			
			//ApplyAntiRoll();
		}
	}  
//		/**
//		 * 改变车的零时状态.
//		 */
//		void UpdateCarStates ()
//		{
//				List<CarState.CarStateType> removeStateKey = new List<CarState.CarStateType> ();
//				foreach (KeyValuePair<CarState.CarStateType,CarState> kvp in this.stateDic) {
//						if (kvp.Value != null) {
//								kvp.Value.effectTime -= Time.fixedDeltaTime;
//								if (kvp.Value.effectTime <= 0) {
//										removeStateKey.Add (kvp.Key);
//								}
//						}
//				}
//				foreach (CarState.CarStateType carState in removeStateKey) {
//						this.stateDic.Remove (carState);
//				}
//		}
//	
//		public void AddState (CarState carState)
//		{
//				if (!this.stateDic.ContainsKey (carState.stateType)) {
//						this.stateDic [carState.stateType] = carState;
//				}
//		}
	
	void LateUpdate ()
	{
		List<int> wayPoints = this.UpdateWaypoint ();
		if (isRun && carType==CarType.UserCar&& wayPoints.Count > 0)
		{
			RaceManager.Instance.PlayerCarUpdateRoadPoint (wayPoints);
		}
	}
	/**
		 * 更新车的显示状态.
		 */
	void CheckStum ()
	{
		if (respawnState != RespawnState.none)
		{
			respawnTime += Time.fixedDeltaTime;
			if (respawnState == RespawnState.ready)
			{
				if (respawnTime > respawnDuration)
				{
					//Instantiate(respawnEffect, waypoints[currentWaypoint].position, waypoints[currentWaypoint].rotation);
					rigidbody.velocity = Vector3.zero;
					transform.position = waypoints [currentWaypoint].position + Vector3.up;
					transform.forward = roadForward;
					transform.rotation = waypoints [currentWaypoint].rotation;
					respawnState = RespawnState.respawning;
					horzOffsetFromWayPoint=0;
				}
			} else
			{
				if (respawnTime > 3f)
				{
					respawnTime = 0f;
					respawnState = RespawnState.none;
				}
			}
			
			return;
		}

		bool bNeedCovered = false;
		
		// 检测是否翻车
		if (transform.up.y < 0||transform.position.y<-1)
		{
			bNeedCovered = true;
		}
		
		// 检测是否在赛道外
		if (!bNeedCovered)
		{
			/*RaycastHit hit;
            int layerMask = (1 << (LayerMask.NameToLayer("Terrain"))) | (1 << (LayerMask.NameToLayer("Road")));

            Physics.Raycast(transform.position, -Vector3.up, out hit, int.MaxValue, layerMask);
            Vector3 posOnGround = transform.position;
            posOnGround.y = hit.point.y;

            if (!Physics.SphereCast(posOnGround, 100f, roadRight, out hit, maxRoadWidth, 1 << (LayerMask.NameToLayer("RoadSide")))
                || !Physics.SphereCast(posOnGround, 100f, -roadRight, out hit, maxRoadWidth, 1 << (LayerMask.NameToLayer("RoadSide"))))
            {
                Debug.DrawLine(posOnGround, posOnGround + maxRoadWidth * roadRight, Color.green);
                Debug.DrawLine(posOnGround, posOnGround - maxRoadWidth * roadRight, Color.red);
                bNeedCovered = true;
            }*/
		}
		if (bNeedCovered)
		{
			respawnState = RespawnState.ready;
			stuckTime = 0;
		}
		return;//暂时关闭后面的功能
		if (rigidbody.velocity.sqrMagnitude < 0.5)
		{
			stuckTime += Time.fixedDeltaTime;
			
			if (stuckTime > 2)
			{
				bNeedCovered = true;
			}
		} else
		{
			stuckTime = 0;
		}
	}
	/// <summary>
	/// 取得当前的制动力.
	/// </summary>
	/// <value>The current brake factor.</value>
	public float curBrakeFactor 
	{
		get
		{
			return _curBrakeFactor;
		}
	}
	/// <summary>
	/// 增加或者减少制动力.
	/// </summary>
	/// <returns>The current brake factor.</returns>
	public void addCurBrakeFactor(float addValue)
	{
		_curBrakeFactor += addValue;
	}
	/// <summary>
	/// 额外增加的最大速度.
	/// </summary>
	public int addedMaxSpeed;
	/// <summary>
	/// 设置一个当前的速度.
	/// </summary>
	/// <param name="speed">Speed.</param>
	public void setCurrentSpeed(float speed)
	{
		rigidbody.velocity = transform.forward * speed * MathUtils.KmH_To_MS;
	}
	/// <summary>
	/// 当燃油耗尽的事件.
	/// </summary>
	void onOilOver()
	{

	}
	// 轮子驱动力
	void ApplyTorque ()
	{
		if (_currentOil > 0)
		{
			_currentOil -= Time.deltaTime*oilWear;
			if(_currentOil<=0)
			{
				_currentOil=0;
				onOilOver();
			}
		}else if (_currentOil == 0)
		{
			return;
		}
		float curTorque = baseTorque;
		float curMaxSpeed = maxSpeed+addedMaxSpeed;
//				if (this.stateDic.ContainsKey (CarState.CarStateType.cst_speedUp)) {
//						curTorque = curTorque * 2f;
//						curMaxSpeed = curMaxSpeed * 1.5f;
//				}
		
		
		currentSpeed = (Mathf.PI * 2 * flWheelCollider.radius) * flWheelCollider.rpm * 60 / 1000;
		currentSpeed = Mathf.Round (currentSpeed);
		 
		if (curBrakeFactor != 0f)
		{
			//当前有制动,应用制动力
			flWheelCollider.motorTorque = 0f;
			frWheelCollider.motorTorque = 0f;
			flWheelCollider.brakeTorque = curBrakeFactor * baseBrakeTorque;
			frWheelCollider.brakeTorque = curBrakeFactor * baseBrakeTorque;
		} else if (currentSpeed < curMaxSpeed)
		{
			//当前速度没有达到最大速度,应用推力
			flWheelCollider.motorTorque = curTorque ;
			frWheelCollider.motorTorque = curTorque ;
			flWheelCollider.brakeTorque = 0f;
			frWheelCollider.brakeTorque = 0f;
		} else
		{
			flWheelCollider.motorTorque = 0f  ;
			frWheelCollider.motorTorque = 0f ;
			flWheelCollider.brakeTorque = 0f;
			frWheelCollider.brakeTorque = 0f;
		}
	}
	/**
		 * 添加碰撞效果.
		 */
	void ApplyBouncing ()
	{
		if (isBouncing)
		{
			bouncingTime += Time.deltaTime;
			if (Mathf.Abs (bouncingTime - bounceTotalTime) < 0.01f)
			{
				isBouncing = false;
				bouncingTime = 0;
				//解除当前撞击制动
//				addCurBrakeFactor(-bouncingRoadBrakeFactory);
				//解除当前撞击转向
				curSteerFactor = 0;
			}
		}
	}
	public float carBodyRotationSpeed=4f;
	public float carBodyRotationOffset=25f;
	/// <summary>
	/// 车辆在漂移时,车身侧转与道路弯度的关系.值越大,车辆侧转越明显.
	/// </summary>
	public float piaoyiPianZhuanXiShu=1.0f;
	/// <summary>
	/// 漂移时,车身旋转的基本偏移量.
	/// </summary>
	public float piaoyiPianZhuangJiShu=20f;
	/**
	* 更新显示状态.
	*/
	void Update ()
	{
		
		if (showDebug)
		{
			ShowDebug ();
		}
		RotateWheels ();
		SteelWheels ();
		carBody.position = transform.position + carBodyOffsetY * Vector3.up;
		_flowBox.position = carBody.position;
//		if (waypoint != null)
//		{
//			flowBox.rotation = waypoint.rotation;
//		}
 		if (!getCarState (CarState.IgnoreChangeRotation))
		{
			//更新车辆的显示.
			Quaternion rot = Quaternion.LookRotation (transform.forward);
			float off=0;
			off=carBodyRotationOffset*curSteerInputValue;
			if(roadCurve==0)
			{
			}else
			{
				off=piaoyiPianZhuangJiShu*roadCurve+lastCurveValue*piaoyiPianZhuanXiShu ;
			}
//			if(carType==CarType.UserCar)
//			{
//				Debug.Log("lastCurve:>"+lastCurveValue);
//			}
			if(playFireDuration==0)
			{
				rot.eulerAngles += new Vector3 (0, off, 0);
			}else
			{//冲刺技能中,车身旋转稍微平滑一点.
				rot.eulerAngles += new Vector3 (0, off*0.60f, 0);
			}
//			if(carType==CarType.UserCar)
//			{
//				Debug.Log(off+"   "+rot.eulerAngles.y);
//			}
//			carBody.localRotation =rot;// Quaternion.Slerp (carBody.localRotation, rot, carBodyRotationSpeed * Time.deltaTime);
			carBody.localRotation = Quaternion.Slerp (carBody.localRotation, rot, carBodyRotationSpeed * Time.deltaTime);
		}
		if (playFireDuration > 0)
		{
			playFireDuration -= Time.deltaTime;
			if (playFireDuration <= 0)
			{
				stopFire ();
			}
		}
		//更新引擎的声音.
		float pik = (currentSpeed / maxSpeed) * 1.5f;// + 0.3f;
		carEffect.setRunSoundPich (Mathf.Lerp (carEffect.runSoundPitch, pik, 0.1f));
		//brakeEffector.setRunSoundPich (1f);
		if (isManualModel)
		{
//			if(roadCurveValue!=0&&curSteerInputValue!=0)
//			{
//				onEntryCurve(MathUtils.sign0(roadCurveValue));
//			}else
//			{
//				onExitCurve();
//			}
		}
		updateCauve ();
	}
	void updateCauve()
	{
		if (lastExitCurveTime <= 0)
		{
			return;
		}
		if (!isInCurveModel)
		{
			lastExitCurveTime -= Time.deltaTime;
		}
		if (lastExitCurveTime <= 0)
		{
			if (roadCurve > 0)
			{
				roleAvtConteoller.StopDriftLeft ();
			} else if (roadCurve < 0)
			{
				roleAvtConteoller.StopDriftRight ();
			}
			roadCurve = 0;
			carEffect.stopBrake ();
			setCarState (CarState.ShowState_Drift, false);
		}
	}
	public float wheelRotate = 30f;
	public float wheelRotatePro = 5f;

	void RotateWheels ()
	{
		float r = 360*Time.deltaTime* currentSpeed / 20;// Time.deltaTime*flWheelCollider.rpm / 60 * 360 
		
		flWheel.Rotate (r , 0, 0);
		frWheel.Rotate (r , 0, 0);
		rlWheel.Rotate (r , 0, 0);
		rrWheel.Rotate (r , 0, 0);
		rotateFontWheelBox (flWheelBox);
		rotateFontWheelBox (frWheelBox);
//		rotateBackWheelBox (rlWheelBox);
//		rotateBackWheelBox (rrWheelBox);
	}

	void rotateFontWheelBox (Transform box)
	{
		float r = curSteerInputValue * wheelRotate + roadCurve * wheelRotate;
		r = Mathf.Clamp (r, -wheelRotate, wheelRotate);
		Vector3 last = box.transform.localEulerAngles;
		if (last.y > 180)
		{
			last.y = last.y - 360;
		}
		float next = Mathf.Lerp (last.y, r, wheelRotatePro * Time.deltaTime);
		last.y = next;
		box.transform.localEulerAngles = last;
	}

	void rotateBackWheelBox (Transform box)
	{
		if (rigidbody.velocity.magnitude >= 1)
		{
			box.transform.forward = Vector3.RotateTowards (box.transform.forward, rigidbody.velocity, wheelRotatePro, wheelRotatePro);               
			Vector3 last = box.transform.localEulerAngles;
			if (last.y > 180)
			{
				last.y = last.y - 360;
			}
			last.y = Mathf.Clamp (last.y, -wheelRotate, wheelRotate);
			box.transform.localEulerAngles = last;
		} else
		{
			box.transform.localEulerAngles=Vector3.zero;;
		}
	}

	void SteelWheels ()
	{
		//flWheel.localEulerAngles = new Vector3(flWheel.localEulerAngles.x, curSteerInputValue * 2, flWheel.localEulerAngles.z);
		//frWheel.localEulerAngles = new Vector3(frWheel.localEulerAngles.x, curSteerInputValue * 2, frWheel.localEulerAngles.z);
	}

	public float testData = 10;
	/**
		 * 撞上一辆车.只要管自己的动作就可以了.
		 */
	void onHitCar (Collider col)
	{
		CarEngine car=col.gameObject.GetComponent <CarEngine>();
		if (car.hitLevel == hitLevel)
		{//相同碰撞级别.
			hitSameLevel(col);
			addBreakScore(2);
		} else if (car.hitLevel > hitLevel)
		{//我撞了更强的车.
			addBreakScore(3);
			hitHeightLevel(col);
		} else
		{//我撞了比我弱的车.
			addBreakScore(1);
			hitSameLevel(col);
		}
		doSnakeScreen ();
		addOil (-5);
		//brakeEffector.stopBrake ();
//				onExitCurve ();
	}
	/// <summary>
	/// 撞飞的方向.
	/// </summary>
	[HideInInspector]
	public int hitFlyDirection=0;
	void hitHeightLevel(Collider col)
	{
		float power = (rigidbody.velocity - col.rigidbody.velocity).magnitude;//碰撞的强度.
		//我在后面.
		Vector3 posOffset = -rigidbody.velocity * 0.15f;
		Vector3 v = -rigidbody.velocity * 0.8f;
//		rigidbody.AddForce (v, ForceMode.VelocityChange);
		roleAvtConteoller.TriggerHitF ();
		carEffect.onHitCar (col);
//		if (col == null || Vector3.Dot ((col.transform.position - transform.position), transform.forward) > 0)
//		{
//			iTween.MoveAdd (gameObject, iTween.Hash ("time", 0.5f, "z", -5f, "islocal", true));
//			iTween.PunchRotation (carBody.FindChild ("car").gameObject, iTween.Hash ("time", 2f, "y", 20, "space", Space.Self)); 
//			carEffect.onHitCar (col);
//		} else
//		{
			hitFlyDirection=(int)Mathf.Sign(Vector3.Dot ((transform.position-col.transform.position ), transform.right));
			//我在前面.
			if (!getCarState (CarState.WuDi))
			{
				if(carType==CarType.OtherCar)
				{
					if(ActionUtils.runAction(gameObject,"zhaFei"))
					{
						return;
					}
				}
				doUseSkill (carEffect.bigHitEffect);
			}
//			iTween.RotateAdd(carBody.gameObject, iTween.Hash ("time",2.5f, "y", 720, "islocal", true,"easetype","easeOutQuint"));
//		}
	}
 	
	void hitSameLevel(Collider col)
	{
		float power = (rigidbody.velocity - col.rigidbody.velocity).magnitude;//碰撞的强度.
		if (col == null || Vector3.Dot ((col.transform.position - transform.position), rigidbody.velocity) > 0)
		{
			if(!getCarState(CarState.WuDi))
			{
				//我在后面.
				Vector3 posOffset = -rigidbody.velocity * 0.15f;
				Vector3 v = -rigidbody.velocity * 0.8f;
				rigidbody.AddForce (v, ForceMode.VelocityChange);
				iTween.MoveAdd (gameObject, iTween.Hash ("time", 0.5f, "z", -5f, "islocal", true));
				iTween.PunchRotation (carBody.FindChild ("car").gameObject, iTween.Hash ("time", 2f, "y", 20, "space", Space.Self)); 
				roleAvtConteoller.TriggerHitF ();
			}
			carEffect.onHitCar (col);
			
		} else
		{
			if(!getCarState(CarState.WuDi))
			{
				//我在前面.                                             
				//Vector3 posOffset= rigidbody.velocity*0.05f; 
				float v = rigidbody.velocity.magnitude;
				iTween.MoveAdd (gameObject, iTween.Hash ("time", 0.25f, "z", 5f, "islocal", true));
				iTween.PunchRotation (carBody.FindChild ("car").gameObject, iTween.Hash ("time", 2f, "y", 45, "space", Space.Self)); 
				roleAvtConteoller.TriggerHitB ();
			}
		}
	}

	/// <summary>
	/// 快速抖动屏幕.
	/// </summary>
	public void doSnakeScreen ()
	{
		if (carType==CarType.UserCar)
		{
			SmoothFollow v = RaceManager.Instance.CarCamera.GetComponent<SmoothFollow> ();
			v.playSnake (v.snakeInTensity, v.snakeDuration);
		}
	}

	void HandleTrigger (Collider col)
	{
		if (col.gameObject.layer == GameLayers.RoadSide_Layer && !isBouncing)
		{
			isBouncing = true;
			bouncingTime = 0f;

//			addCurBrakeFactor( bouncingRoadBrakeFactory);
			curSteerFactor = (horzOffsetFromWayPoint < 0) ? bouncingRoadSteerFactory : -bouncingRoadSteerFactory;
			Vector3 v = -rigidbody.velocity * 0.8f;
			rigidbody.AddForce (v, ForceMode.VelocityChange);
			iTween.PunchRotation (carBody.FindChild ("car").gameObject, iTween.Hash ("time", 2f, "y", 20, "space", Space.Self)); 
			if (carEffect.onHitOther (col))
			{
				roleAvtConteoller.TriggerHitR ();
			} else
			{
				roleAvtConteoller.TriggerHitL ();
			}
			addOil(-3);
		} else if (!isBouncing && (col.gameObject.layer == GameLayers.User_Car_Layer || col.gameObject.layer == GameLayers.Other_Car_Layer))
		{
			if (getCarState (CarState.YingShen))
				return;//自己是隐身的.那么可以穿过车辆.
			Transform obj = (Transform)col.transform.root.transform.FindChild ("Engine");
			if (obj != null && obj.GetComponent<CarEngine> ().getCarState (CarState.YingShen))
			{//别人是隐身的,那么我可以穿过别人.
				return;
			}
			isBouncing = true;
			bouncingTime = 0f;
			
			Vector3 bouncingDir = (col.transform.position - transform.position).normalized;
			float bouncingOnForward = Vector3.Dot (bouncingDir, transform.forward);
			float bouncingOnRight = Vector3.Dot (bouncingDir, transform.right);
			
			if (bouncingOnForward > 0)
			{
				//curBrakeFactor = bouncingOnForward;
			}
			curSteerFactor = bouncingRoadBrakeFactory * -bouncingOnRight;
			
			//            if (isBouncingCar)
			//                return;
			//
			//			Vector3 bouncingDir=(col.transform.position-transform.position).normalized;
			//			float bouncingD=Vector3.Dot(transform.forward,bouncingDir);
			//
			//			if(!isAI)
			//			{
			//				Debug.Log("bouncingD="+bouncingD);
			//			}
			//
			//            bounceCarTime = 0;
			//            isBouncingCar = true;
			onHitCar (col);
		} else if (col.gameObject.layer == LayerMask.NameToLayer ("TriggerItem"))
		{
			//			if(this.gameObject.layer==LayerMask.NameToLayer("PlayerCar"))
			//			{
			TriggerItemBase tib = col.GetComponent<TriggerItemBase> ();
			tib.TriggerByCar (this);
			//			}
		}
	}
	
	void OnTriggerStay (Collider col)
	{
		//HandleTrigger(col);
	}
	
	void OnTriggerEnter (Collider col)
	{
		HandleTrigger (col);
	}

	void OnCollisionEnter (Collision collisionInfo)
	{
//			Debug.Log ("碰撞"+collisionInfo);
//			Collider co=GetComponent<Collider> ();
//			co.isTrigger = true;
//			//rigidbody.isKinematic = false;
//
//			isBouncing = true;
//			bouncingTime = 0f;
//			
//			curBrakeFactor = bouncingRoadBrakeFactory;
//			curSteerFactor = (horzOffsetFromWayPoint < 0) ? bouncingRoadSteerFactory : -bouncingRoadSteerFactory;
//			iTween.MoveAdd (gameObject, iTween.Hash ("time",0.5f,"x",-3,"space",Space.Self));
		//iTween.PunchPosition(gameObject,iTween.Hash("time",0.3f,"x",1,"space",Space.Self));
	}

	void OnTriggerExit (Collider col)
	{
		//        if (col.gameObject.layer == LayerMask.NameToLayer("RoadSide"))
		//        {
		//            bounceRoadDistance = 0;
		//            isBouncingRoad = false;
		//
		//        }
		//        else if (col.gameObject.layer == LayerMask.NameToLayer("CarTrigger"))
		//        {
		//            bounceCarTime = 0;
		//            isBouncingCar = false;
		//        }

	}

	private bool useScreenInput = false;
	private float startAccelerationX=0.0f;
	void ApplyInput ()
	{
		if (carType!=CarType.UserCar || useScreenInput)
		{
			return;
		}
		float input;
		if (Input.acceleration!=Vector3.zero&& MainState.Instance.playerInfo.nowInputName.Equals (UIControllerConst.InputGravity))
		{
			if(MathUtils.isInRange(Input.acceleration.x,startAccelerationX-0.075f,startAccelerationX+0.075f))
			{
				input=0;
			}else
			{
				input=startAccelerationX+Input.acceleration.x;
			}
		} else
		{
			input= Input.GetAxis ("Horizontal");
		}

		turnCar (input); 
		bool isBrake = Input.GetButtonDown ("Jump");
						
		if (isBrake)
		{
//						playFire (0.3f);
			//doUseSkill(skill);
			RaceManager.Instance.RaceCounterInstance.doUseSkill (skill);
//						UserRaceInfo.instance.doUseSkill (skill);
		} else
		{
			//stopFire ();	
		}
		if (Input.GetButtonDown ("Fire1"))
		{
//				onHitCar(null);
		}
	}

	public void doTurnCar (float input)
	{
		if (input == 0)
		{
			useScreenInput = false;
		} else
		{
			useScreenInput = true;
		}
		turnCar (input);
	}
	/// <summary>
	/// 控制车辆的方向,让车转弯.
	/// </summary>
	/// <param name="direction">需要转向的方向. 0表示暂停转向,小于0表示左转,大于0表示右转.(暂时用1表示强度.以后可能需要使用强度信息.)</param>
	public void turnCar (float input)
	{
		if(input==curSteerInputValue){
			return;
		}
		bool bSteeringBefore = bMannualSteering;
		bMannualSteering = false;
		if (input != 0)
		{
			if (!isBouncing)
			{
				bMannualSteering = true;
			}
		} else
		{
			bMannualSteering = false;
		}
		if (input == 0)
		{
			if (curSteerInputValue > 0)
			{
				roleAvtConteoller.StopTurnRight ();
			} else if (curSteerInputValue < 0)
			{
				roleAvtConteoller.StopTurnLeft ();
			}
		} else if (input > 0)
		{
			roleAvtConteoller.ActTurnRight ();
		} else
		{
			roleAvtConteoller.ActTurnLeft ();
		}
		if (bMannualSteering)
		{
			curSteerInputValue = Mathf.Sign (input);
		} else
		{
			curSteerInputValue = 0;
		}
	}

	private float playFireDuration = 0;
	/**
	 * 播放多长时间的冲刺.如果duration为0,表示需要手动停止.
	 */
	public void playFire (float duration)
	{
		if (playFireDuration > 0)
		{
			playFireDuration = Mathf.Max (playFireDuration, duration);
		} else
		{
			playFireDuration = duration;
		}
		carEffect.playFire ();
		onExitCurve ();
		roleAvtConteoller.ActSpeedUp ();
		setCarState (CarState.ShowState_AddSpeed, true);
	}
	/**
	 * 立即停止冲刺.
	 */
	public void stopFire ()
	{
		playFireDuration = 0;
		carEffect.stopFire ();
		roleAvtConteoller.StopSpeedUp ();
		setCarState (CarState.ShowState_AddSpeed, false);
	}
	/**
		 *添加刹车痕迹效果.
		 */
	private void addBrakeEffects ()
	{
		
	}
	// 计算路的方向
	void UpdateRoadDir ()
	{
		if (waypoints.Count == 0)
		{
			return;
		}
		Transform start = waypoints [(currentWaypoint - 1 + waypoints.Count) % waypoints.Count];
		waypoint = start;
		roadForward = start.forward;
		roadRight = Vector3.Cross (Vector3.up, roadForward);
	}
	
	int getRailIndexFromOffset (float offset)
	{
		float roadWidth = gapBetweenRails * nRail;
		return (int)Mathf.Clamp (Mathf.Round ((offset + roadWidth / 2) / gapBetweenRails), 0, nRail - 0.1f);
	}
	
	float getRailOffset (int railIndex)
	{
		return (railIndex - (float)nRail / 2) * gapBetweenRails;
	}
	private float _carInRoadSide;
	/// <summary>
	/// 车相对于路径中心的位置.
	/// </summary>
	/// <value>The car in road side.</value>
	public float carInRoadSide
	{
		get
		{
			return _carInRoadSide;
		}
	}
	/// <summary>
	/// 手动漂移时,车辆的转弯速度.
	/// </summary>
	public float piaoYiZhuanWanSuDu=0.6f;
	// 计算车子与路径的偏移
	void UpdateHorzOffsetToWp ()
	{
		Transform wpCur = waypoints [RaceManager.Instance.trimWayProintIndex (currentWaypoint)].transform;
		float curOffset = Vector3.Dot (transform.position - wpCur.position, roadRight);
		_carInRoadSide = curOffset;
		//        curRail = getRailIndexFromOffset(curOffset);
		if (!bInitOffsetHorz)
		{
			horzOffsetFromWayPoint = curOffset;
			bInitOffsetHorz = true;
		} else
		{
			if (curSteerFactor != 0)
			{
				horzOffsetFromWayPoint = horzOffsetFromWayPoint + Time.fixedDeltaTime * autoHorzSpeed * curSteerFactor;
			} else if (bMannualSteering)
			{
				//                int prevWaypoint = (currentWaypoint - 1 + waypoints.Count) % waypoints.Count;
				//                float deltaOffset = Mathf.Sign(curSteerInputValue) * gapBetweenRails;
				//                float nextOffset = curOffset + deltaOffset * manualChangeRailFactor;
				//				float roadAngle = Vector3.Angle(waypoints[currentWaypoint].forward, waypoints[(currentWaypoint+wpCheckFactory)% waypoints.Count].forward);
				
				// 根据角度，调整转弯力度
				//                int dirTurn = (int)Mathf.Sign(Vector3.Dot(waypoints[prevWaypoint].forward, waypoints[currentWaypoint].forward));
				/*if (dirTurn == Mathf.Sign(curSteerInputValue))
                {
                    float initiaFactor = Mathf.Clamp(1 - roadAngle * roadAngle * 3, 0.2f, 1);
                    horzOffsetFromWayPoint = Mathf.Lerp(horzOffsetFromWayPoint, curOffset + deltaOffset, Time.fixedDeltaTime * steerInputFactor * initiaFactor);
                }
                else*/
				{
					//					float nowHorzSpeed=manualHorzSpeed*(1-Mathf.Abs(roadAngle/18));
					//					nowHorzSpeed=Mathf.Clamp(nowHorzSpeed,manualHorzSpeed/2,manualHorzSpeed);

					//                    float oldOffset = horzOffsetFromWayPoint;
					float manualHorzSpeed=this.manualHorzSpeed;
					if(IsInCurve)
					{
						manualHorzSpeed*=piaoYiZhuanWanSuDu; 
					}
					 if (curSteerInputValue < 0)
						horzOffsetFromWayPoint = horzOffsetFromWayPoint + Time.fixedDeltaTime * manualHorzSpeed * -1;
					else
						horzOffsetFromWayPoint = horzOffsetFromWayPoint + Time.fixedDeltaTime * manualHorzSpeed;
					//horzOffsetFromWayPoint = Mathf.Clamp(horzOffsetFromWayPoint, -gapBetweenRails * nRail / 2, gapBetweenRails * nRail / 2);
				}
			} else
			{
				// 回轮中
				//                if (Mathf.Abs(curSteerInputValue) > 0.1f)
				//                {
				//                    // 回轮
				//                    float curRailOffset = getRailOffset(getRailIndexFromOffset(curOffset));
				//                    if (Mathf.Sign(curSteerInputValue) == -1)
				//                    {
				//                        // 向左转
				//                        if (curRailOffset < curOffset)
				//                        {
				//                            // 就到当前赛道中央即可
				//                            horzOffsetFromWayPoint = Mathf.Lerp(horzOffsetFromWayPoint, curRailOffset, Time.fixedDeltaTime * horzSpeed);
				//                        }
				//                        else
				//                        {
				//                            // 到下一赛道中央
				//                            if (curRail > 0)
				//                                horzOffsetFromWayPoint = Mathf.Lerp(horzOffsetFromWayPoint, curRailOffset - gapBetweenRails, Time.fixedDeltaTime * horzSpeed);
				//                            else
				//                                horzOffsetFromWayPoint = Mathf.Lerp(horzOffsetFromWayPoint, curRailOffset, Time.fixedDeltaTime * horzSpeed);
				//                        }
				//                    }
				//                    else
				//                    {
				//                        // 向右转
				//                        if (curRailOffset > curOffset)
				//                        {
				//                            // 就到当前赛道中央即可
				//                            horzOffsetFromWayPoint = Mathf.Lerp(horzOffsetFromWayPoint, curRailOffset, Time.fixedDeltaTime * horzSpeed);
				//                        }
				//                        else
				//                        {
				//                            // 到下一赛道中央
				//                            if (curRail + 1 < nRail)
				//                                horzOffsetFromWayPoint = Mathf.Lerp(horzOffsetFromWayPoint, curRailOffset + gapBetweenRails, Time.fixedDeltaTime * horzSpeed);
				//                            else
				//                                horzOffsetFromWayPoint = Mathf.Lerp(horzOffsetFromWayPoint, curRailOffset, Time.fixedDeltaTime * horzSpeed);
				//                        }
				//                    }
				//                }
				//                else
				//                {
				//                    float curRailOffset = getRailOffset(getRailIndexFromOffset(curOffset));
				//                    horzOffsetFromWayPoint = Mathf.Lerp(horzOffsetFromWayPoint, curRailOffset, Time.fixedDeltaTime * horzSpeed);
				//                }
			}
		}
	}
		
	// 更新路点
	List<int> UpdateWaypoint ()
	{
		List<int> returnList = new List<int> ();
		if (waypoints==null||waypoints.Count == 0)
		{
			return returnList;	
		}
		
		// 更新路点
		//bool bUpdate = false;
		int nTryMax = 5;
		int nTry = 0;
		while (true)
		{
			Transform wpCur = waypoints [currentWaypoint];
			Vector3 carTarget = wpCur.position + roadRight * horzOffsetFromWayPoint;
			Vector3 moveDirection = carTarget - transform.position;
			float sqrMagnitude = moveDirection.sqrMagnitude;
			
			if (sqrMagnitude < sqrDistanceToWaypoint)
			{ //2011-12-25-E
				returnList.Add (currentWaypoint);
				NextWaypoint ();
				//bUpdate = true;
			} else if (Vector3.Cross (moveDirection, roadRight).y < 0)
			{
				returnList.Add (currentWaypoint);
				NextWaypoint ();
				//bUpdate = true;
			} else
			{
				break;
			}
			
			nTry += 1;
			if (nTry > nTryMax)
				break;
		}
		
		return returnList;
	}
	/**
	 * 当前是不是正在弯道里.
	 */
	public bool IsInCurve
	{
		get
		{
			return roadCurve != 0;
		}
	}
	/// <summary>
	/// 当前道路的弯曲程度.实际就是周围10个路点总共的偏移角度值.
	/// </summary>
	private float lastCurveValue=0f;
	private void validateIsInCurve ()
	{
		int max = 10;
		int offset = -max / 2;
		float total = 0;
		float last = waypoints [(currentWaypoint + offset + waypoints.Count) % waypoints.Count].eulerAngles.y;
		for (int i=offset+1; i<=max; i+=2)
		{
			float current = waypoints [(currentWaypoint + i + offset + waypoints.Count) % waypoints.Count].eulerAngles.y;
			float of = current - last;
			if (of > 180)
			{
				of = 360 - of % 360;
			} else if (of < -180)
			{
				of = 360 + of % 360;
			}
			total += of;
			last = current;
		}

		float speedV=currentSpeed/maxSpeed;
		lastCurveValue = total;
		if (Mathf.Abs (total) > 10  )
		{
			float v=total < 0 ? 1 : -1;
			if (isManualModel)
			{
				Transform cu= waypoints [(currentWaypoint) % waypoints.Count];
				Transform next= waypoints [(currentWaypoint+1) % waypoints.Count];
				float vct= Vector3.Dot(next.position-cu.position,cu.right);
//				Debug.Log("offset\t"+(next.position-cu.position).magnitude+"\t"+vct);
				 
				horzOffsetFromWayPoint+=(vct*6)*steerCarSpeed*speedV;
//				horzOffsetFromWayPoint+=v*steerCarSpeed*speedV;
			}
			roadCurveValue=total < 0 ? -1 : 1;
		} else
		{
			roadCurveValue=0;
		}
		if (autoEntryCurve)
		{
//			//Debug.Log (total);
//			if (Mathf.Abs (total) > 10 && currentSpeed > maxSpeed / 3 * 2)
//			{
//				onEntryCurve (total < 0 ? -1 : 1);
//			} else
//			{
//				onExitCurve ();
//			}
			if(roadCurveValue==0)
			{
				onExitCurve();
			}else{
				if(( isManualModel&&curSteerInputValue!=0)&&speedV>0.7)
//				if((!isManualModel||( curSteerInputValue!=0&&Mathf.Sign(curSteerInputValue)==Mathf.Sign(roadCurveValue)))&&speedV>0.7)
				{
					onEntryCurve (total < 0 ? -1 : 1);
				}else
				{
					onExitCurve();
				}
			}
		}
	}
	/// <summary>
	/// 道路本身的弯度.值不为0,表示车辆在弯道内.
	/// </summary>
	private float roadCurveValue=0f;
	/// <summary>
	/// T是否自动进入漂移状态.
	/// </summary>
	public bool autoEntryCurve=true;
	private float lastExitCurveTime=0;
	/// <summary>
	/// 是否进入了漂移模式.如果离开漂移模式,车辆也会有短暂的漂移.
	/// </summary>
	private bool isInCurveModel=false;
	/**
	 * 进入拐弯.
	 */
	 void onEntryCurve (int cv)
	{
		if (isInCurveModel||carType==CarType.OtherCar)
			return;
		isInCurveModel = true;
		roadCurve = cv;
		carEffect.playBrake ();
		if (cv > 0)
		{
			roleAvtConteoller.ActDriftLeft ();
		} else
		{
			roleAvtConteoller.ActDriftRight ();
		}
		setCarState (CarState.ShowState_Drift, true);
	}
	/// <summary>
	/// 播放刹车特效.
	/// </summary>
	/// <param name="cv">Cv.</param>
	public void doShowBrakeEffect(int cv)
	{
		if (cv != 0)
		{
			roadCurve = cv;
			if (cv > 0)
			{
				roleAvtConteoller.ActDriftLeft ();
			} else
			{
				roleAvtConteoller.ActDriftRight ();
			}
		}
		carEffect.playBrake ();
		setCarState (CarState.ShowState_Drift, true);
	}
	public void stopShowBrakeEffect(int cv)
	{
		if (cv != 0)
		{
			roadCurve = 0;
			if (cv > 0)
			{
				roleAvtConteoller.StopDriftLeft ();
			} else
			{
				roleAvtConteoller.StopDriftRight ();
			}
		}
		carEffect.stopBrake ();
		setCarState (CarState.ShowState_Drift, false);
	}
	/**
	 * 离开拐弯.
	 */
	void onExitCurve ()
	{
		if (!isInCurveModel||carType==CarType.OtherCar)
			return;
		isInCurveModel = false;
		lastExitCurveTime = 0.5f;

	}
	/// <summary>
	/// 车辆辅助导航权重.1为百分百按路径行走.0表示完全手动.
	/// </summary>
	public float steerCarSpeed=0.1f;
	/**
		 * 改变车的弯度.
		 */
	void ApplySteer ()
	{
		UpdateRoadDir ();
		UpdateHorzOffsetToWp ();
		
		//        bool bNewWaypoint = UpdateWaypoint();
		//        if (bNewWaypoint)
		//        {
		//            // 重新计算
		//            UpdateRoadDir();
		//            UpdateHorzOffsetToWp();
		//        }
		
		// 车子的下一个目标点
		// 自动修正方向
		
		//if (!bMannualSteering)
		{
			Transform wpCur = waypoints [(currentWaypoint + wpCheckFactory) % waypoints.Count];
			validateIsInCurve ();
			Vector3 carTarget = wpCur.position + roadRight * horzOffsetFromWayPoint;
			Vector3 moveDirection = carTarget - transform.position;//准备移动的方向.
			moveDirection.y = 0;
			
			Vector3 localTarget = transform.InverseTransformDirection (moveDirection);
			
			//            float speedProcent = currentSpeed / maxSpeed;
			//            speedProcent = Mathf.Clamp(1 - speedProcent, 0.8f, 1);
			//
			//            float currentMaxSteerAngle = bMannualSteering ? maxMannualSteerAngle : autoSteerAngle;
			//            currentMaxSteerAngle *= speedProcent;
			currentTargetAngle = Mathf.Atan2 (localTarget.x, localTarget.z) * Mathf.Rad2Deg;

			currentSteerAngle = currentTargetAngle;
			float steer = currentSteerAngle;
			flWheelCollider.steerAngle = steer;
			frWheelCollider.steerAngle = steer;
		}
		/*else
        {
            float steer = maxMannualSteerAngle * Mathf.Sign(curSteerInputValue);
            float angle = Vector3.Angle(transform.forward, roadForward);
            steer *= (1 - angle / 45f);
            flWheelCollider.steerAngle = steer;
            frWheelCollider.steerAngle = steer;
        }*/
		
		//        UpdateWaypoint();
	}
	
	void ShowDebug ()
	{
		
		// 车子的下一个目标点
		Transform wpCur = waypoints [currentWaypoint].transform;
		Vector3 carTarget = wpCur.position + roadRight * horzOffsetFromWayPoint;
		
		// 下一个路点
		if (wpTargetIndicator == null)
		{
			GameObject prefab = Resources.LoadAssetAtPath ("Assets/Resources/Prefabs/Race/Waypoint 1.prefab", typeof(GameObject)) as GameObject;
			wpTargetIndicator = GameObject.Instantiate (prefab) as GameObject;
			wpTargetIndicator.name = "Wp curr Target";
			wpTargetIndicator.transform.parent = transform.parent;
			wpTargetIndicator.transform.position = wpCur.position;
		} else
		{
			wpTargetIndicator.transform.position = wpCur.position;
		}
		
//		// 车子的下一个目标
//		if (carTargetIndicator == null) {
//			GameObject prefab = Resources.LoadAssetAtPath ("Assets/Resources/Prefabs/Race/Waypoint 1.prefab", typeof(GameObject)) as GameObject;
//			carTargetIndicator = GameObject.Instantiate (prefab) as GameObject;
//			carTargetIndicator.name = "Car Target";
//			carTargetIndicator.renderer.material.color = Color.blue;
//			carTargetIndicator.transform.parent = transform.parent;
//			carTargetIndicator.transform.position = carTarget;
//		} else {
//			carTargetIndicator.transform.position = carTarget;
//		}
		
		if (roadDirIndicator == null)
		{
			GameObject prefab = Resources.LoadAssetAtPath ("Assets/Resources/Prefabs/Race/RoadDir.prefab", typeof(GameObject)) as GameObject;
			roadDirIndicator = GameObject.Instantiate (prefab) as GameObject;
			roadDirIndicator.name = "Car Target Angle";
			roadDirIndicator.renderer.material.color = Color.green;
			roadDirIndicator.transform.parent = transform;
			roadDirIndicator.transform.position = transform.position + transform.forward * 2;
			roadDirIndicator.transform.forward = roadForward;
		} else
		{
			roadDirIndicator.transform.position = transform.position + transform.forward * 2;
			roadDirIndicator.transform.forward = rigidbody.transform.forward;//roadForward;
		}
		/*

                if (carDirIndicator == null)
                {
                    GameObject prefab = Resources.LoadAssetAtPath("Assets/AIDriverToolkit/Prefabs/RoadDir.prefab", typeof(GameObject)) as GameObject;
                    carDirIndicator = GameObject.Instantiate(prefab) as GameObject;
                    carDirIndicator.name = "Road Right";
                    carDirIndicator.renderer.material.color = Color.yellow;
                    carDirIndicator.transform.parent = transform;
                    carDirIndicator.transform.position = transform.position + roadRight * 2;
                    carDirIndicator.transform.forward = roadRight;
                }
                else
                {
                    carDirIndicator.transform.position = transform.position + roadRight * 2;
                    carDirIndicator.transform.forward = roadRight;
                }*/
		
		//targetAngle
	}
	/// <summary>
	/// 当前车是不是向前开的.
	/// </summary>
	/// <returns><c>true</c>, if foward was ised, <c>false</c> otherwise.</returns>
	public bool isFoward 
	{
		get {
			return waypoints != RaceManager.Instance.reverseWayPoints;
		}
	}
	/// <summary>
	/// 取得向前走的路点索引.
	/// </summary>
	/// <returns>The waypoint index.</returns>
	public int fowardWaypointIndex 
	{
		get
		{
			if (isFoward)
			{
				return currentWaypoint;
			} else
			{
				return RaceManager.Instance.getReverseIndex(currentWaypoint);
			}
		}
	}
	/// <summary>
	/// 改变车的路点信息.车会自动根据路点信息,改变车的运动方向.
	/// </summary>
	/// <param name="foward">If set to <c>true</c> foward.</param>
	public void setWayPoints (bool foward)
	{
		this.waypoints =foward? RaceManager.Instance.WayPoints: RaceManager.Instance.reverseWayPoints;
 
	}
	/// <summary>
	/// 给汽车添加一个制动力系数.
	/// </summary>
	/// <param name="value">Value.</param>
	public void addBrakeFactor (float value)
	{
		_curBrakeFactor += value;
		if (value > 0)
		{
			setCarState (CarState.ShowState_Drift, true);
		} else
		{
			setCarState (CarState.ShowState_Drift, false);
		}
	}
	/// <summary>
	/// T用一个变量来统计车辆行驶过的总的路点数量.只有走了一定路程的车辆,过起点时,才能算作跑了一圈	/// </summary>
	private int runIndex = 0;
	public void NextWaypoint ()
	{ 
		int c = currentWaypoint;
		currentWaypoint = (currentWaypoint + 1) % waypoints.Count;
		runIndex++;
		if (c > currentWaypoint)
		{
			int r=waypoints.Count/2;
			if(c>r&&currentWaypoint<r)
			{
				onNextRound();
			}
		}
	}
	void onNextRound()
	{
		if (runIndex > 10)
		{
			_currentRoad++;
		}
	}
	private GameObject petPrefab;
	private GameObject petSkill;
	/// <summary>
	/// D释放一个技能(不同宠物也是不同的技能).可以传入任何一个GameObject作为技能对象.如果该GameObject拥有PlayAble组件.将直接启动PlayAble.否则将直接添加到车辆内部.
	/// </summary>
	/// <returns><c>true</c>技能释放成功. <c>false</c>通常技能不是释放失败.将来可能有特殊技能.</returns>
	/// <param name="skill">Skill.</param>
	public bool doUseSkill (GameObject skill)
	{
		if (skill.name == "PetSkill")
		{
			PetSkillBase sk = skill.GetComponent<PetSkillBase> ();
			if (sk != null)
			{
				initPetSkill(sk);
				sk.petModel = petPrefab;
				if(petSkill!=null)
				{
					doUseSkill(petSkill);
				}
			}
		} else
		{
			roleAvtConteoller.TriggerAttack ();
		}
		return addEffect (skill);
	}
	private void initPetSkill(PetSkillBase sk)
	{
		if (petPrefab == null)
		{
			PetConfigData conf = PetConfigData.GetConfigData<PetConfigData> (MainState.Instance.playerInfo.nowPetId);
			string avt = conf.petAvt;
			GameObject obj = GameResourcesManager.GetPetAvtPrefab (avt);
			if (obj != null)
			{
				petPrefab = obj;//(GameObject)GameObject.Instantiate (obj);
			} else
			{
				petPrefab = new GameObject ("petNotFind");
			}
			petSkill = GameResourcesManager.GetPetSkillPrefab (conf.skillPrefab);
		}
	}
	/// <summary>
	/// 在车体上添加特效.
	/// </summary>
	/// <returns><c>true</c>, if effect was added, <c>false</c> otherwise.</returns>
	/// <param name="skill">Skill.</param>
	public bool addEffect (GameObject skill)
	{
		if (skill.GetComponent<PetSkillBase> () != null)
		{
			return EffectManager.playEffect (transform, skill) != null;
		} else
		{
			return EffectManager.playEffect (transform, skill) != null;
		}
	}
	/// <summary>
	/// 被箭头攻击的事件.
	/// </summary>
	/// <param name="hit">Hit.</param>
	void onHitByJianTou (JianTou hit)
	{
		//Debug.Log ("被攻击");
	}

	public void doChangeRoleModel (GameObject role)
	{

	}

	public void doChangeCarModel (GameObject car)
	{

	}
}
