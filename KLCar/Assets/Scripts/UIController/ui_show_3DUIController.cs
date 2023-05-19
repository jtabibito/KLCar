using UnityEngine;
using System.Collections;
using DG.Tweening;

/// <summary>
/// 人车宠，主界面，四个界面公用的代码------控制3D视图
/// 2015年4月27日15:40:45
/// </summary>
public partial class ui_show_3DUIController : UIControllerBase {

	private GameObject carAvt = null;
	private GameObject petAvt = null;
	private GameObject roleAvt = null;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public GameObject CreateCarRole(string  carPrefebName,string rolePrefebName)
	{
		carAvt = GameResourcesManager.GetCarAvtPrefab (carPrefebName);
		carAvt = NGUITools.AddChild (this.Car_role, carAvt);

		//添加人到车上
		roleAvt = GameResourcesManager.GetRolePrefab (rolePrefebName);
		GameObject roleSlot = carAvt.transform.FindChild ("role").gameObject;
		roleAvt = NGUITools.AddChild (roleSlot, roleAvt);
		//car_mod/role

		return  carAvt;
	}

	public GameObject CreateCar(string prefebName)
	{
		carAvt = GameResourcesManager.GetCarAvtPrefab (prefebName);
		carAvt = NGUITools.AddChild (this.Car, carAvt);
		return  carAvt;
	}
	
	public GameObject CreatePet(string prefebName)
	{
		petAvt = GameResourcesManager.GetPetAvtPrefab (prefebName);
		petAvt = NGUITools.AddChild (this.Pet, petAvt);
		return  petAvt;
	}

	public GameObject CreateRole(string prefebName)
	{
		roleAvt = GameResourcesManager.GetRolePrefab (prefebName);
		roleAvt = NGUITools.AddChild (this.Role, roleAvt);
		return  roleAvt;
	}
	
	////////////////////////////////////
	public void CleanCarAvt()
	{
		if(this.carAvt!=null)
			Destroy (this.carAvt);
	}

	public void CleanRoleAvt()
	{
		if(this.roleAvt!=null)
			Destroy (this.roleAvt);
	}

	public void CleanPetAvt()
	{
		if(this.petAvt!=null)
			Destroy (this.petAvt);
	}

	/// <summary>
	/// Cleans all avt.
	/// 退场动画后续添加
	/// </summary>
	public void CleanAllAvt()
	{
		CleanCarAvt ();
		CleanRoleAvt ();
		CleanPetAvt ();
		CloseUI ();
	}
	
	//////////////////////////////////////////动画
	public void StartCarAnim()
	{
		if (this.carAvt == null) {
			Debug.Log("StartCarAnim fail,because carAvt is null,please fix it");
			return;
		}
		this.carAvt.transform.localScale = new Vector3 (0.1f,0.1f,0.1f);

		Sequence mySeq = DOTween.Sequence ();
		mySeq.Append (this.carAvt.transform.DOScale(Vector3.one,1).SetEase(Ease.OutBounce));
//		mySeq.Append (this.carAvt.transform.DOBlendableLocalRotateBy(new Vector3(0,360,0),12,RotateMode.FastBeyond360));
//		this.carAvt.transform.DOBlendableLocalRotateBy (new Vector3 (0, 360, 0), 12, RotateMode.FastBeyond360).SetLoops (-1);
		//mySeq.Append (this.carAvt.transform.DOBlendableLocalRotateBy(new Vector3(0,-360,0),12,RotateMode.FastBeyond360).SetLoops(2));
		//mySeq.Append (this.carAvt.transform.DOScale (new Vector3 (0.1f, 0.1f, 0.1f), 1));
		//mySeq.SetLoops(-1);
	}

	public void StartPetAnim()
	{
		if (this.petAvt == null) {
			Debug.Log("StartPetAnim fail,because carAvt is null,please fix it");
			return;
		}
		this.petAvt.transform.localScale = new Vector3 (0.1f,0.1f,0.1f);
		
		Sequence mySeq = DOTween.Sequence ();
		mySeq.Append (this.petAvt.transform.DOScale(new Vector3 (1, 1, 1),1).SetEase(Ease.OutBounce));
		mySeq.Append (this.petAvt.transform.DOBlendableLocalRotateBy(new Vector3(0,360,0),6,RotateMode.FastBeyond360).SetLoops(2));
		mySeq.Append (this.petAvt.transform.DOBlendableLocalRotateBy(new Vector3(0,-360,0),6,RotateMode.FastBeyond360).SetLoops(2));
		//mySeq.Append (this.petAvt.transform.DOScale (new Vector3 (0.1f, 0.1f, 0.1f), 1));
		mySeq.SetLoops(-1);
	}

	public void StartRoleAnim()
	{
		if (this.roleAvt == null) {
			Debug.Log("StartRoleAnim fail,because carAvt is null,please fix it");
			return;
		}
		this.roleAvt.transform.localScale = new Vector3 (0.1f,0.1f,0.1f);
		
		Sequence mySeq = DOTween.Sequence ();
		mySeq.Append (this.roleAvt.transform.DOScale(Vector3.one,1).SetEase(Ease.OutBounce));
		mySeq.Append (this.roleAvt.transform.DOBlendableLocalRotateBy(new Vector3(0,360,0),6,RotateMode.FastBeyond360).SetLoops(2));
		mySeq.Append (this.roleAvt.transform.DOBlendableLocalRotateBy(new Vector3(0,-360,0),6,RotateMode.FastBeyond360).SetLoops(2));
		mySeq.Append (this.roleAvt.transform.DOScale (new Vector3 (0.1f, 0.1f, 0.1f), 1));
		mySeq.SetLoops(-1);
	}


}
