using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

/// <summary>
/// 人车宠，主界面，四个界面公用的代码------控制2d视图
/// 2015年5月9日16:28:53
/// </summary>
public partial class ui_show_2DUIController : UIControllerBase
{
		private GameObject carAvt = null;
		private GameObject petAvt = null;
		private GameObject roleAvt = null;
		private GameObject lightFLAvt = null;
		private GameObject lightFRAvt = null;
		private float sinAngle;
		private	float axisx;
		private	float axisy;
		private	float axisz;
		private Vector3 UIOriginalRotationCar;
		private Vector3 UIOriginalRotationTai;
		private Vector3 UIOriginalRotationRole;
		private Vector3 UIOriginalRotationPet;
		private Vector3 UIOriginalRotationCarRole;
		float roleDisplayTime = 0;
		float petDisplayTime = 0;
		bool roleNeedDisplay = false;
		bool petNeedDisplay = false;
	
		// Use this for initialization
		void Start ()
		{
				this.sinAngle = Mathf.Sqrt (1 - Mathf.Pow (this.Tai.transform.localRotation.w, 2));
				this.axisx = this.Tai.transform.localRotation.x / sinAngle;
				this.axisy = this.Tai.transform.localRotation.y / sinAngle;
				this.axisz = this.Tai.transform.localRotation.z / sinAngle;

				this.UIOriginalRotationCar = this.Car.transform.localRotation.eulerAngles;
				this.UIOriginalRotationTai = this.Tai.transform.localRotation.eulerAngles;
				this.UIOriginalRotationRole = this.Role.transform.localRotation.eulerAngles;
				this.UIOriginalRotationPet = this.Pet.transform.localRotation.eulerAngles;
				this.UIOriginalRotationCarRole = this.Car_role.transform.localRotation.eulerAngles;
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (roleNeedDisplay) {
						if (roleDisplayTime < 10) {
								roleDisplayTime += Time.deltaTime;
						} else {
								ShowRoleDisplay ();
								roleDisplayTime = 0;
						}
				}
				if (petNeedDisplay) {
						if (petDisplayTime < 12) {
								petDisplayTime += Time.deltaTime;
						} else {
								ShowPetDisplay ();
								petDisplayTime = 0;
						}
				}
		}

		void BeginRoleDisplay ()
		{
				
				if (roleAvt != null) {
						Animator roleAnimator = this.roleAvt.GetComponent<Animator> ();
						roleAnimator.applyRootMotion = true;
						roleAnimator.SetInteger ("roleState", 6);
						roleNeedDisplay = true;
						roleDisplayTime = 9;
				}
		}

		void StopRoleDisplay ()
		{
				if (roleAvt != null) {
						Animator anim = this.roleAvt.GetComponent<Animator> ();
						anim.applyRootMotion = false;
						roleNeedDisplay = false;
				}
				
		}

		void ShowRoleDisplay ()
		{
				if (roleAvt != null) {
						Animator anim = this.roleAvt.GetComponent<Animator> ();
						anim.SetTrigger ("t_display");
				}
		}

		void BeginPetDisplay ()
		{
		
				if (petAvt != null) {
						Animator petAnimator = this.petAvt.GetComponent<Animator> ();
						petAnimator.applyRootMotion = true;
						petAnimator.SetInteger ("roleState", 6);
						petNeedDisplay = true;
						petDisplayTime = 11;
				}
		}
	
		void StopPetDisplay ()
		{
				if (petAvt != null) {
						Animator anim = this.petAvt.GetComponent<Animator> ();
						anim.applyRootMotion = false;
						petNeedDisplay = false;
				}
		
		}
	
		void ShowPetDisplay ()
		{
				if (petAvt != null) {
						Animator anim = this.petAvt.GetComponent<Animator> ();
						anim.SetTrigger ("t_display");
				}
		}
	
		public GameObject CreateCarRole (string  carPrefebName, string rolePrefebName)
		{
				carAvt = GameResourcesManager.GetCarAvtPrefab (carPrefebName);
				carAvt = NGUITools.AddChild (this.Car_role, carAvt);
		
				//添加人到车上
				roleAvt = GameResourcesManager.GetRolePrefab (rolePrefebName);
				GameObject roleSlot = carAvt.transform.FindChild ("car_transform/car_role").gameObject;
				roleAvt = NGUITools.AddChild (roleSlot, roleAvt);

				//添加车灯到车上
				lightFLAvt = GameResourcesManager.GetRaceObject ("car_fx_02_qianchedeng");
				GameObject flSlot = carAvt.transform.FindChild ("car_transform/car_light_FL").gameObject;
				lightFLAvt = NGUITools.AddChild (flSlot, lightFLAvt);

				lightFRAvt = GameResourcesManager.GetRaceObject ("car_fx_02_qianchedeng");
				GameObject frSlot = carAvt.transform.FindChild ("car_transform/car_light_FR").gameObject;
				lightFRAvt = NGUITools.AddChild (frSlot, lightFRAvt);

				//this.Car_role.transform.DOShakePosition(1,0.1f,1,2,false).SetLoops(-1,LoopType.Yoyo).SetUpdate(true);

				return  carAvt;
		}
	
		public GameObject CreateCar (string prefebName)
		{
				carAvt = GameResourcesManager.GetCarAvtPrefab (prefebName);
				carAvt = NGUITools.AddChild (this.Car, carAvt);

				//添加车灯到车上
				lightFLAvt = GameResourcesManager.GetRaceObject ("car_fx_02_qianchedeng");
				GameObject flSlot = carAvt.transform.FindChild ("car_transform/car_light_FL").gameObject;
				lightFLAvt = NGUITools.AddChild (flSlot, lightFLAvt);
		
				lightFRAvt = GameResourcesManager.GetRaceObject ("car_fx_02_qianchedeng");
				GameObject frSlot = carAvt.transform.FindChild ("car_transform/car_light_FR").gameObject;
				lightFRAvt = NGUITools.AddChild (frSlot, lightFRAvt);

				return  carAvt;
		}
		
		//pet_fx_01
		public GameObject CreatePet (string prefebName)
		{
				petAvt = GameResourcesManager.GetPetAvtPrefab (prefebName);
				petAvt = NGUITools.AddChild (this.Pet, petAvt);

				//删除特效--------临时代码
				Transform skill = petAvt.transform.FindChild ("pet_fx_01");
				if (skill != null) {
						skill.gameObject.SetActive (false);
				}
			
				return  petAvt;
		}
	
		public GameObject CreateRole (string prefebName)
		{
				roleAvt = GameResourcesManager.GetRolePrefab (prefebName);
				roleAvt = NGUITools.AddChild (this.Role, roleAvt);

				return  roleAvt;
		}
	
		////////////////////////////////////
		public void CleanCarAvt ()
		{
				if (this.carAvt != null)
						Destroy (this.carAvt);

				if (this.lightFLAvt != null)
						Destroy (this.lightFLAvt);
				if (this.carAvt != null)
						Destroy (this.lightFRAvt);
		}
	
		public void CleanRoleAvt ()
		{
				if (this.roleAvt != null) {
						this.StopRoleDisplay ();
						Destroy (this.roleAvt);
				}
		}
	
		public void CleanPetAvt ()
		{
				if (this.petAvt != null) {
						this.StopPetDisplay ();
						Destroy (this.petAvt);
				}
		}
	
		/// <summary>
		/// Cleans all avt.
		/// 退场动画后续添加
		/// </summary>
		public void CleanAllAvt ()
		{
				CleanCarAvt ();
				CleanRoleAvt ();
				CleanPetAvt ();
				CloseUI ();
		}
	
		//////////////////////////////////////////动画
		public void StartCarAnim ()
		{
				if (this.carAvt == null) {
						Debug.Log ("StartCarAnim fail,because carAvt is null,please fix it");
						return;
				}
				this.carAvt.transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f);
		
				Sequence mySeq = DOTween.Sequence ();
				mySeq.Append (this.carAvt.transform.DOScale (Vector3.one, 1).SetEase (Ease.OutBounce));
				//		mySeq.Append (this.carAvt.transform.DOBlendableLocalRotateBy(new Vector3(0,360,0),12,RotateMode.FastBeyond360));
				//		this.carAvt.transform.DOBlendableLocalRotateBy (new Vector3 (0, 360, 0), 12, RotateMode.FastBeyond360).SetLoops (-1);
				//mySeq.Append (this.carAvt.transform.DOBlendableLocalRotateBy(new Vector3(0,-360,0),12,RotateMode.FastBeyond360).SetLoops(2));
				//mySeq.Append (this.carAvt.transform.DOScale (new Vector3 (0.1f, 0.1f, 0.1f), 1));
				//mySeq.SetLoops(-1);
		}
	
		public void StartPetAnim ()
		{
				if (this.petAvt == null) {
						Debug.Log ("StartPetAnim fail,because carAvt is null,please fix it");
						return;
				}
				this.petAvt.transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f);

				this.BeginPetDisplay ();
		
				Sequence mySeq = DOTween.Sequence ();
				mySeq.Append (this.petAvt.transform.DOScale (new Vector3 (0.8f, 0.8f, 0.8f), 0.8f).SetEase (Ease.OutBounce));
//				mySeq.Append (this.petAvt.transform.DOBlendableLocalRotateBy (new Vector3 (0, 360, 0), 6, RotateMode.FastBeyond360).SetLoops (2));
//				mySeq.Append (this.petAvt.transform.DOBlendableLocalRotateBy (new Vector3 (0, -360, 0), 6, RotateMode.FastBeyond360).SetLoops (2));
//				//mySeq.Append (this.petAvt.transform.DOScale (new Vector3 (0.1f, 0.1f, 0.1f), 1));
//				mySeq.SetLoops (-1);
		}
	
		public void StartRoleAnim ()
		{
				if (this.roleAvt == null) {
						Debug.Log ("StartRoleAnim fail,because carAvt is null,please fix it");
						return;
				}
				this.roleAvt.transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f);
				
				this.BeginRoleDisplay ();
		
				Sequence mySeq = DOTween.Sequence ();
				mySeq.Append (this.roleAvt.transform.DOScale (Vector3.one, 0.8f).SetEase (Ease.OutBounce));
//				mySeq.Append (this.roleAvt.transform.DOBlendableLocalRotateBy (new Vector3 (0, 360, 0), 6, RotateMode.FastBeyond360).SetLoops (2));
//				mySeq.Append (this.roleAvt.transform.DOBlendableLocalRotateBy (new Vector3 (0, -360, 0), 6, RotateMode.FastBeyond360).SetLoops (2));
//				//mySeq.Append (this.roleAvt.transform.DOScale (new Vector3 (0.1f, 0.1f, 0.1f), 1));
//				mySeq.SetLoops (-1);
		}
		
		/// <summary>
		/// 旋转车辆
		/// </summary>
		/// <param name="delta">Delta.</param>
		/// <param name="scrollWidth">Scroll width.</param>
		/// <param name="back">If set to <c>true</c> back.</param>
		public void ManualRotateCar (float delta, float scrollWidth, bool back = false)
		{
				if (this.carAvt == null) {
						Debug.Log ("ManualRotateCar fail,because carAvt is null,please fix it");
						return;
				}

				//多次滑动导致再次旋转不运行，所以停掉上一次的Tween
				this.Car.transform.DOKill ();
				this.Tai.transform.DOKill ();

				if (back == false) {
						this.Car.transform.RotateAround (this.Car.transform.position, new Vector3 (axisx, axisy, axisz), -delta * 360 / scrollWidth);
						this.Tai.transform.RotateAround (this.Tai.transform.position, new Vector3 (axisx, axisy, axisz), -delta * 360 / scrollWidth);
				} else {
						this.Car.transform.DOLocalRotate (this.UIOriginalRotationCar, 0.75f).SetEase (Ease.OutBack);
						this.Tai.transform.DOLocalRotate (this.UIOriginalRotationTai, 0.75f).SetEase (Ease.OutBack);
				}
		}

		/// <summary>
		/// 主界面的旋转
		/// </summary>
		/// <param name="delta">Delta.</param>
		/// <param name="scrollWidth">Scroll width.</param>
		/// <param name="back">If set to <c>true</c> back.</param>
		public void ManualRotateCarRole (float delta, float scrollWidth, bool back = false)
		{
				if (this.carAvt == null) {
						Debug.Log ("ManualRotateCarRole fail,because carAvt is null,please fix it");
						return;
				}
				
				//多次滑动导致再次旋转不运行，所以停掉上一次的Tween
				this.Car_role.transform.DOKill ();
				this.Tai.transform.DOKill ();

				if (back == false) {
						this.Car_role.transform.RotateAround (this.Car_role.transform.position, new Vector3 (axisx, axisy, axisz), -delta * 360 / scrollWidth);
						this.Tai.transform.RotateAround (this.Tai.transform.position, new Vector3 (axisx, axisy, axisz), -delta * 360 / scrollWidth);
				} else {
						this.Car_role.transform.DOLocalRotate (this.UIOriginalRotationCar, 0.75f, RotateMode.FastBeyond360).SetEase (Ease.OutBack);
						this.Tai.transform.DOLocalRotate (this.UIOriginalRotationTai, 0.75f, RotateMode.FastBeyond360).SetEase (Ease.OutBack);
				}
		}


		/// <summary>
		/// 主界面的旋转
		/// </summary>
		/// <param name="delta">Delta.</param>
		/// <param name="scrollWidth">Scroll width.</param>
		/// <param name="back">If set to <c>true</c> back.</param>
		public void ManualRotateRole (float delta, float scrollWidth, bool back = false)
		{
				if (this.roleAvt == null) {
						Debug.Log ("ManualRotateRole fail,because roleAvt is null,please fix it");
						return;
				}

				//多次滑动导致再次旋转不运行，所以停掉上一次的Tween
				this.Role.transform.DOKill ();
				this.Tai.transform.DOKill ();

				if (back == false) {
						this.Role.transform.RotateAround (this.Role.transform.position, new Vector3 (axisx, axisy, axisz), -delta * 360 / scrollWidth);
						this.Tai.transform.RotateAround (this.Tai.transform.position, new Vector3 (axisx, axisy, axisz), -delta * 360 / scrollWidth);
				} else {
						this.Role.transform.DOLocalRotate (this.UIOriginalRotationRole, 0.75f).SetEase (Ease.OutBack);
						this.Tai.transform.DOLocalRotate (this.UIOriginalRotationTai, 0.75f).SetEase (Ease.OutBack);
				}
		}

		/// <summary>
		/// 宠物界面的旋转
		/// </summary>
		/// <param name="delta">Delta.</param>
		/// <param name="scrollWidth">Scroll width.</param>
		/// <param name="back">If set to <c>true</c> back.</param>
		public void ManualRotatePet (float delta, float scrollWidth, bool back = false)
		{
				if (this.petAvt == null) {
						Debug.Log ("ManualRotatePet fail,because petAvt is null,please fix it");
						return;
				}
			
				//多次滑动导致再次旋转不运行，所以停掉上一次的Tween
				this.Pet.transform.DOKill ();
				this.Tai.transform.DOKill ();

				if (back == false) {
						this.Pet.transform.RotateAround (this.Pet.transform.position, new Vector3 (axisx, axisy, axisz), -delta * 360 / scrollWidth);
						this.Tai.transform.RotateAround (this.Tai.transform.position, new Vector3 (axisx, axisy, axisz), -delta * 360 / scrollWidth);
				} else {
						this.Pet.transform.DOLocalRotate (this.UIOriginalRotationPet, 0.75f).SetEase (Ease.OutBack);
						this.Tai.transform.DOLocalRotate (this.UIOriginalRotationTai, 0.75f).SetEase (Ease.OutBack);
				}
		}
}
