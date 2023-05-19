using UnityEngine;
using System.Collections;

public class HallModelController : MonoBehaviour {

	Transform carPos;
	Transform petPos;
	Transform rolePos;

	void Awake(){
		carPos = this.transform.FindChild ("carPos");
		petPos = this.transform.FindChild ("petPos");
		rolePos = this.transform.FindChild ("rolePos");
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowRole(string roleName)
	{
		ResourceLoaderComponent.Instance.CreatGameObject (roleName, this.OnRoleLoadOver);
	}

	void OnRoleLoadOver(string resourceName,GameObject role)
	{
		this.PlaceModel (role, this.rolePos);
	}

	public void ShowCar(string carName)
	{
		ResourceLoaderComponent.Instance.CreatGameObject (carName, this.OnCarLoadOver);
	}

	void OnCarLoadOver(string resourceName,GameObject car)
	{
		this.PlaceModel (car, this.carPos);
	}

	public void ShowPet(string petName)
	{
		if(petName!="")
		{
			ResourceLoaderComponent.Instance.CreatGameObject (petName, this.OnPetLoadOver);
		}
	}

	void OnPetLoadOver(string resourceName,GameObject pet)
	{
		this.PlaceModel (pet, this.petPos);
	}

	void PlaceModel(GameObject go,Transform pos)
	{
		go.transform.parent=pos;
		go.transform.localPosition=Vector3.zero;
		go.transform.localEulerAngles=Vector3.zero;
		go.transform.localScale=Vector3.one;
	}
}
