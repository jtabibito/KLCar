using UnityEngine;
using System.Collections;

/// <summary>
/// 车的效果配置方案.并且播放车辆的所有效果.
/// </summary>

public class CarEffectsConfig : MonoBehaviour
{
	private Hashtable sounds=new Hashtable();
	class Wheel
	{
		public Vector3 lastPoint;
		public Transform wheel;
		public float size;
	}
	public static CarEffectsConfig DEFUALT_CONFIG;
	public enum CarEventType
	{
		run,
		brake,
		spurt,
		collidCar,
		collidOther
	} 
	/// <summary>
	/// 速度特效.当速度达到一定时,显示.低于时消失.
	/// </summary>
	public GameObject speedEffect;
	/**
	 * 汽车行驶的声音.
	 */
	public string runSound="SoundResources/xingshi";
	/**
	 * 汽车刹车和漂移的声音.
	 */
	public string brakeSound="SoundResources/piaoyi";
//	/**
//	 * 冲刺加速的声音.
//	 */
//	public string spurtSound;
	/**
	 * 碰撞车的声音.
	 */
	public string collidCarSound="SoundResources/pengzhuangdahua";
	/**
	 * 碰撞物体的声音.
	 */
	public string collidOtherSound="SoundResources/pengzhuangqiang";

	/**
	 * 碰撞车的特效.
	 */
	public GameObject collidCarEffect;
	/**
	 * 碰撞物体的特效.
	 */
	public GameObject collidOtherEffect;
	/**
	 * 冲刺加速的特效.可以设置很多个.比如车头和四个轮子.还有2个尾部喷气.数量不限制.
	 */
	public GameObject[] spurtEffect;
	/**
	 * 漂移的特效..可以设置很多个.比如车头和四个轮子.还有2个尾部喷气.数量不限制.
	 */
	public GameObject[] brakeEffect;
	/**
	 * 前轮的漂移路径贴图.没有就不填.
	 */
	public GameObject frontKidmark;
	/**
	 * 后轮的漂移路径贴图.没有就不填.
	 */
	public GameObject backKidmark;
	private AudioSource runSoundPlayer;
	private CarEngine _carEngine;
	/// <summary>
	/// 碰撞路面后,显示特效的位置.故意写死,方便美术调效果.物理计算真实,但是发现车速很快时看不到
	/// </summary>
	public Vector3 hitRoadEffectPoint = new Vector3 (1.5f, 1f, 1.5f);
	private GameObjectPool kidmarkPool;
	/// <summary>
	/// T需要做胎痕的后轮
	/// </summary>
	private Wheel[] backWheels;
	/// <summary>
	/// 玩家的车和警车相撞的技能效果.
	/// </summary>
	public GameObject bigHitEffect;
//		private Ray ray = new Ray ();//用来计算胎痕位置.
	public CarEngine carEngine
	{
		get
		{
			return _carEngine;
		}
		set
		{
			_carEngine = value;
		}
	}

//	private Vector3 lastPoint = Vector3.zero;
	private bool showSpeedEffect = false;//当前是否显示车速特效.
	void Update ()
	{

		if (carEngine.carType==CarEngine.CarType.UserCar)
		{
			if (carEngine.currentSpeed >= carEngine.maxSpeed / 3 * 2)
			{
				if (!showSpeedEffect)
				{
					showSpeedEffect = true;
					speedEffect = EffectManager.playEffect (transform, speedEffect);
				}
			} else
			{
				if (showSpeedEffect)
				{
					showSpeedEffect = false;
					speedEffect = EffectManager.stopEffect (speedEffect);
				}
			}
		}
				
		if (isStartBrake && backKidmark != null)
		{
			validateWheelMark (backWheels [0]);
			validateWheelMark (backWheels [1]);
		}
		updateFireTween ();
		if (isStartBrake&&carEngine.currentSpeed<=10)
		{//速度很慢时,停止刹车声音.
			stopBrake();
		}
	}
	public float upOffset=20;
	public float upDuration=0.5f;
	public float downDuration=0.5f;
	public float waitDuration=0f;
	public Easetype easeUpType; 
	public Easetype easeDownType; 
	void updateFireTween ()
	{
		if (fireType == 0)
		{
			return;
		} else if (fireType == 2)
		{
			fireProgress+=Time.deltaTime;
			if(fireProgress>waitDuration)
			{
				updateFireState(-1);
				return;
			}
		}
		else
		{
			fireProgress += Time.deltaTime / (fireType > 0 ?upDuration : downDuration);
			if (fireProgress >= 1)
			{
				fireProgress = 1;
			}
			float current = 0;
			TweenUtils.EasingFunction tween;
			if (fireType > 0)
			{//上升.
				tween=TweenUtils.GetEasingFunction(easeUpType);
				current =tween(0, -upOffset, fireProgress);
			} else
			{//下降.
				tween=TweenUtils.GetEasingFunction( easeDownType);
				current = tween (-upOffset, 0, fireProgress);
			}
			Vector3 center = Vector3.Lerp (backWheels [0].wheel.position, backWheels [1].wheel.position, 0.5f);
			Transform car = carEngine.carBody.Find ("car");
			float c = car.transform.localEulerAngles.x;
			float off = current - c;
			car.transform.RotateAround (center, car.transform.right, off);
			if (fireProgress == 1)
			{
				if (fireType > 0)
				{
					updateFireState(2);
				}else
				{
					updateFireState(0);
				}
			}
		}
	}
 
	Vector3 getPos (Wheel w)
	{

		Vector3 f = -carEngine.carBody.up;
//			f.y = -f.y;
		RaycastHit hitInfo;
		if (Physics.Raycast (w.wheel.position, f, out hitInfo, w.size+10f, 1 << GameLayers.Road_Layer))
		{//,w.size,GameLayers.Road_Layer))
			return hitInfo.point + new Vector3 (0f, 0.1f, 0f);
		} else
		{
			return Vector3.zero;
		}
			
		//return new Vector3 (v.x,0.1f,v.z);
	}

	void validateWheelMark (Wheel w)
	{
		if (carEngine.carType != CarEngine.CarType.UserCar)
		{
			return;
		}
		if (w.lastPoint == Vector3.zero)
		{
			w.lastPoint = getPos (w);
		} else
		{
				
			Vector3 current = getPos (w);
			if (current == Vector3.zero)
			{
				return;
			} else
			{
				float d = Vector3.Distance (w.lastPoint, current);
				if (d >= 1 && d < 10)
				{
					Quaternion q = Quaternion.FromToRotation (current, w.lastPoint);
					GameObject obj = kidmarkPool.newInstance ();//(GameObject)Instantiate(backKidmark);
					Kidmark kd = obj.GetComponent<Kidmark> ();
					kd.pool = kidmarkPool;
					//							transform.localScale = new Vector3 (transform.localScale.x,transform.localScale.y,d);
					obj.transform.position = Vector3.Lerp (w.lastPoint, current, 0.5f);
					;//new Vector3(lastPoint,0.1f,carEngine.transform.position.z); 
					obj.transform.forward = current - w.lastPoint;
					obj.transform.localScale = new Vector3 (obj.transform.localScale.x, obj.transform.localScale.y, d  );
					w.lastPoint = current;
				}
			}
		}
	}

	void createBackWheels ()
	{
		backWheels = new Wheel[2];
		Wheel w = new Wheel ();
		backWheels [0] = w;
		w.wheel = carEngine.rlWheel;
		w.lastPoint = Vector3.zero;
		w.size = w.wheel.renderer.bounds.size.y / 2;
		w = new Wheel ();
		backWheels [1] = w;
		w.wheel = carEngine.rrWheel;
		w.lastPoint = Vector3.zero;
		w.size = w.wheel.renderer.bounds.size.y / 2;
	}

	void Start ()
	{
		createBackWheels ();
				
		kidmarkPool = GameObjectPools.getPool (backKidmark);
		//对配置的资源进行分析.如果用户没有配,就使用默认的配置来替换.
		if (spurtEffect.Length == 0 && DEFUALT_CONFIG != null)
		{
			spurtEffect = DEFUALT_CONFIG.spurtEffect;
		}
		if (brakeEffect.Length == 0 && DEFUALT_CONFIG != null)
		{
			brakeEffect = DEFUALT_CONFIG.brakeEffect;
		}
		for (int i=0; i<spurtEffect.Length; i++)
		{
			spurtEffect [i] = validateEffect (spurtEffect [i], DEFUALT_CONFIG == null ? null : DEFUALT_CONFIG.spurtEffect [i]);
		}
		for (int i=0; i<brakeEffect.Length; i++)
		{
			brakeEffect [i] = validateEffect (brakeEffect [i], DEFUALT_CONFIG == null ? null : DEFUALT_CONFIG.brakeEffect [i]);
		}
//		runSound = validateSound (runSound, DEFUALT_CONFIG == null ? null : DEFUALT_CONFIG.runSound);
//		speedEffect = validateEffect (speedEffect, DEFUALT_CONFIG == null ? null : DEFUALT_CONFIG.speedEffect);
//		runSoundPlayer =  SoundPlayManager.getSoundPlayer (transform, runSound);
//		if (runSoundPlayer != null)
//		{
//			runSoundPlayer.pitch = 1f;
//			runSoundPlayer.playOnAwake = true;
//			runSoundPlayer.loop = true;
//			runSoundPlayer.Play ();
//		}
//		brakeSound = validateSound (brakeSound, DEFUALT_CONFIG == null ? null : DEFUALT_CONFIG.brakeSound);
//		spurtSound = validateSound (spurtSound, DEFUALT_CONFIG == null ? null : DEFUALT_CONFIG.spurtSound);
//		collidCarSound = validateSound (collidCarSound, DEFUALT_CONFIG == null ? null : DEFUALT_CONFIG.collidCarSound);
//		collidOtherSound = validateSound (collidOtherSound, DEFUALT_CONFIG == null ? null : DEFUALT_CONFIG.collidOtherSound);
//		SoundManager.effect.play (runSound,true);
		if (collidCarEffect == null && DEFUALT_CONFIG != null)
			collidCarEffect = DEFUALT_CONFIG.collidCarEffect;
		if (collidOtherEffect == null && DEFUALT_CONFIG != null)
			collidOtherEffect = DEFUALT_CONFIG.collidOtherEffect;
		if (frontKidmark == null && DEFUALT_CONFIG != null)
			frontKidmark = DEFUALT_CONFIG.frontKidmark;
		if (backKidmark == null && DEFUALT_CONFIG != null)
			backKidmark = DEFUALT_CONFIG.backKidmark;
	
	}

	private float idleSoundPitch = 0f;
	private static float[] pitchs={0.5f,0.5f,0.5f,0.5f,0.5f,1f,1f,1f,1f,1f,1f,1.2f,1.2f,1.2f,1.2f};
//	private static float[] pitchs={0.5f,0.5f,0.5f,0.5f,0.5f,0.6f,0.7f,0.8f,0.9f,1.0f,1.1f,1.2f,1.3f,1.4f,1.5f};
	public void setRunSoundPich (float pitch)
	{
		return;
		float s = carEngine.currentSpeed / (carEngine.maxSpeed-10);
		int f = (int)Mathf.Abs(s * 10);
		if (f >= pitchs.Length)
		{
			f=pitchs.Length-1;
		}
		float v=pitchs[f];
		if (v != idleSoundPitch)
		{
			idleSoundPitch = v;
			if (runSoundPlayer != null)
			{
				runSoundPlayer.pitch = v;
			}
		}
	}

	public float runSoundPitch
	{
		get
		{
//						if (runSoundPlayer != null) {
//								return runSoundPlayer.pitch;
//						} else {
//								return 0;
//						}
			return idleSoundPitch;
		}
	}

	AudioClip validateSound (AudioClip sound, AudioClip defaultSound)
	{
		if (sound == null)
		{
			sound = defaultSound;
		}
		return sound;
	}
	/// <summary>
	/// 撞上了其他车辆.表示主动方.被动方没有这个事件.
	/// </summary>
	/// <param name="col">Col.</param>
	public void onHitCar (Collider col)
	{
		SoundManager.effect.play (collidCarSound, false, gameObject);
//		playSound (collidCarSound, false);
		Ray r = new Ray (transform.position, col.transform.position - transform.position);
		RaycastHit hit;
		if (col.Raycast (r, out hit, 1000f))
		{
			EffectManager.playEffect (null, collidCarEffect, hit.point);
		} else
		{

		}
	}
	/// <summary>
	/// 撞上了路面的等其他对象.
	/// </summary>
	/// <param name="col">Col.</param>
	/// 	<returns>返回碰撞哪一边.true表示右边.false表示左边.</returns>
	public bool onHitOther (Collider col)
	{
		bool isRigth = false;
//		playSound (collidOtherSound, false);
		SoundManager.effect.play (collidOtherSound, false, gameObject);
		Vector3 rv= carEngine.transform.right;
		Ray r = new Ray (carEngine.carBody.position,rv);
//		Ray r = new Ray (carEngine.carBody.position, carEngine.carBody.position + new Vector3 (1, 0, 1));
		RaycastHit hit;
		Vector3 vs;
		float power = carEngine.currentSpeed / 50f + 3;
		if (power > 10)
		{
			power = 10;
		}
		if (col.Raycast (r, out hit, 10f))
		{//右边.
			isRigth = true;
			vs = carEngine.transform.TransformPoint (hitRoadEffectPoint);//碰撞位置暂时写死了.有时间再计算.
			EffectManager.playEffect (null, collidOtherEffect, vs);
			iTween.MoveAdd (carEngine.gameObject, iTween.Hash ("time", 0.6f, "x", -power, "space", Space.Self, "easetype", "easeOutBack"));	
		} else
		{
			isRigth = false;
			//左边.
			hitRoadEffectPoint.x = -hitRoadEffectPoint.x;//反向.
			vs = carEngine.transform.TransformPoint (hitRoadEffectPoint);//碰撞位置暂时写死了.有时间再计算.
			hitRoadEffectPoint.x = -hitRoadEffectPoint.x;//用完再反回来.
			EffectManager.playEffect (null, collidOtherEffect, vs);
			iTween.MoveAdd (carEngine.gameObject, iTween.Hash ("time", 0.6f, "x", power, "space", Space.Self, "easetype", "easeOutBack"));	
		}
		if (carEngine.carType==CarEngine.CarType.UserCar)
		{
			SmoothFollow v = RaceManager.Instance.CarCamera.GetComponent<SmoothFollow> ();
			v.playSnake (v.snakeInTensity, v.snakeDuration);
		}
		return isRigth;
	}

	GameObject validateEffect (GameObject obj, GameObject defaultObj)
	{
		if (obj == null)
		{
			obj = defaultObj;
		}
		if (obj != null)
		{
			if (obj.transform.root != transform.root)
			{
				GameObject last = obj;
				obj = (GameObject)Instantiate (obj);
				obj.transform.parent = carEngine.effectBox;
				obj.transform.localPosition = last.transform.localPosition;
				obj.transform.localRotation = last.transform.localRotation;
				obj.SetActive (false);
			}
		}  
		return obj;
	}

	GameObject setEffectState (GameObject obj, bool active)
	{		
		if (obj != null)
		{ 
			if (active)
			{
				return EffectManager.playEffect (carEngine.effectBox, obj);
			} else
			{
				return EffectManager.stopEffect (obj);
			}
//						if (active && obj.activeSelf) {
//								obj.SetActive (false);
//						}
//						obj.SetActive (active);
		} else
		{
			return null;
		}
	}

	public void playSound (string sound, bool loop)
	{
		if (sound != null&&sound!="")
		{
			stopSound(sound);
			sounds.Add( sound,SoundManager.effect.play(sound,loop,gameObject));
		}
	}

	public void stopSound (string sound)
	{
		if (sound != null)
		{
//			SoundPlayManager.stopSound (transform, sound);

			SoundItem it=(SoundItem) sounds[sound];
			if(it!=null)
			{
				it.stop();
			}
			sounds.Remove(sound);
		}
	}

	public void playEffect (GameObject obj)
	{
		EffectManager.playEffect (transform, obj);
	}

	public void playEffect (GameObject[] obj)
	{
		setEffectState (obj, true);
	}

	public void stopEffect (GameObject[] obj)
	{
		setEffectState (obj, false);
	}

	public void stopEffect (GameObject obj)
	{
		EffectManager.stopEffect (obj);
	}

	void setEffectState (GameObject[] objs, bool active)
	{
		for (int i=0; i<objs.Length; i++)
		{
			objs [i] = setEffectState (objs [i], active);
		}
//				foreach (GameObject obj in objs) {
//						setEffectState (obj, active);
//				}
	}

	public bool isStartBrake;

	public void playBrake ()
	{
		if (isStartBrake)
			return;
		isStartBrake = true;
		backWheels [0].lastPoint = Vector3.zero;
		backWheels [1].lastPoint = Vector3.zero;
		playSound (brakeSound, true);
		playEffect (brakeEffect);
	}

	public void stopBrake ()
	{
		if (!isStartBrake)
			return;
		isStartBrake = false;
		stopSound (brakeSound);
		stopEffect (brakeEffect); 
	}
	/// <summary>
	/// 当前冲刺的状态.0表示没有冲刺状态.1表示抬头.-1表示下降.2表示等待.
	/// </summary>
	private int fireType; 
	/// <summary>
	/// 一个冲刺动作的进度.
	/// </summary>
	private float fireProgress;
	private bool isPlayFire;
	/**
	 * 开始冲刺
	 */
	public void playFire ()
	{
		if (isPlayFire)
		{
			return;
		}
		isPlayFire = true;
		playEffect (spurtEffect);
//		playSound (spurtSound, false);
				
		carEngine.rigidbody.velocity += carEngine.rigidbody.velocity.normalized * 20;
		carEngine.addedMaxSpeed = 50;
		SmoothFollow vc = GameObject.Find ("CarCamera").GetComponent<SmoothFollow> ();
		if (carEngine.carType==CarEngine.CarType.UserCar)
		{
			vc.playFire ();
		}
		updateFireState (1);	 
	}
	/// <summary>
	/// U更新冲刺的状态.
	/// </summary>
	/// <param name="type">Type.</param>
	private void updateFireState (int type)
	{
		fireType =  type;
		fireProgress = 0;
	}
	/**
	 * 停止冲刺.
	 */
	public void stopFire ()
	{
		if (!isPlayFire)
			return;
		isPlayFire = false;
		carEngine.addedMaxSpeed = 0;
		stopEffect (spurtEffect);
		SmoothFollow vc = GameObject.Find ("CarCamera").GetComponent<SmoothFollow> ();
		if (carEngine.carType==CarEngine.CarType.UserCar)
		{
			vc.stopFire ();
		}
	}
}