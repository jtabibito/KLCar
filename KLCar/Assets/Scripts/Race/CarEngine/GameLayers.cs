using UnityEngine;
using System.Collections;
public class GameLayers {
	public static int User_Car_Layer = LayerMask.NameToLayer ("PlayerCar");
	public static int Other_Car_Layer=LayerMask.NameToLayer("ObstacleCar");
	public static int RoadSide_Layer=LayerMask.NameToLayer("RoadSide");
	public static int Road_Layer=LayerMask.NameToLayer("Road");
}
