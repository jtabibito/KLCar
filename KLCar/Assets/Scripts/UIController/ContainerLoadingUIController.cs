using UnityEngine;
using System.Collections;

public partial class ContainerLoadingUIController : UIControllerBase {
	void Start(){
	}

	void Update () {
		if(SceneLoader.Async!=null)
		{
			float loadingProgress = SceneLoader.Async.progress;
			this.ProgressLoading.GetComponent<UIProgressBar> ().value = loadingProgress;
			this.LabelLaoding.GetComponent<UILabel> ().text = ((int)(100*loadingProgress)).ToString () + " %";
		}
	}


}
