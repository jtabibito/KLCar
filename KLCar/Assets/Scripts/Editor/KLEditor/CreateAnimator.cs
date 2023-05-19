using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditorInternal;
using System.Collections.Generic;

public class CreateAnimator {

	[MenuItem ("Assets/生成基本动画配置文件")]
	static void Excute1()
	{
		Object[] objects = Selection.objects;
		for (int i = 0; i < objects.Length; i++)
		{
			AnimatorController ac = (AnimatorController)objects [i];
			if(ac!=null)
			{
				StateMachine sm=ac.GetLayer(0).stateMachine;

				string path = AssetDatabase.GetAssetPath (ac);
				path = path.Substring (0, path.LastIndexOf ('.'));
				path+="_Config.csv";

				CsvStreamWriter csw=new CsvStreamWriter(path);
				csw[1,1]="id";
				csw[2,1]="string";
				csw[1,2]="prefabPath";
				csw[2,2]="string";
				csw[1,3]="createControllerName";
				csw[2,3]="string";
				for(int j=0;j<sm.stateCount;j++)
				{
					csw[1,4+j]=sm.GetState(j).name;
					csw[2,4+j]="string";
				}
				csw.Save();
			}
		}
	}

	[MenuItem ("Assets/生成模型动画控制器")]
	static void Excute2()
	{
		Object[] objects=Selection.objects;
		for(int i=0;i<objects.Length;i++)
		{
			string path=AssetDatabase.GetAssetPath(objects[i]);
			if(path.Substring(path.Length-4,4)==".csv")
			{
				CsvStreamReader csr=new CsvStreamReader(path);

				string baseControllerPath=path.Substring(0,path.LastIndexOf('_'))+".controller";
				Debug.Log(baseControllerPath);
				for(int rowNum=3;rowNum<csr.RowCount+1;rowNum++)
				{
					string id=csr[rowNum,1];
					string prefabPath=csr[rowNum,2];
					string createControllerName=csr[rowNum,3];
					string createControllerPath="Assets/Anims/AnimatorControllers/"+createControllerName+".controller";
					AssetDatabase.DeleteAsset(createControllerPath);
					AssetDatabase.Refresh();
					if(!AssetDatabase.CopyAsset(baseControllerPath,createControllerPath))
					{
						Debug.Log("创建路径="+baseControllerPath+";生成路径="+createControllerPath);
						throw new UnityException("资源复制错误!");
					}
					AssetDatabase.Refresh();
					AnimatorController ac=(AnimatorController)AssetDatabase.LoadAssetAtPath(createControllerPath,typeof(AnimatorController));
					StateMachine sm=ac.GetLayer(0).stateMachine;

					for(int j=0;j<sm.stateCount;j++)
					{
						State s=sm.GetState(j);
						string acpName=csr[rowNum,4+j];
						if( acpName!=null && acpName!="" )
						{
							AnimationClip acp=(AnimationClip)AssetDatabase.LoadAssetAtPath("Assets/Anims/AnimationClips/"+acpName+".anim",typeof(AnimationClip));
							s.SetAnimationClip(acp);
						}
					}

					GameObject avtPrefab=(GameObject)AssetDatabase.LoadAssetAtPath(prefabPath,typeof(GameObject));
					Animator at=avtPrefab.GetComponent<Animator>();
					at.runtimeAnimatorController=ac;
				} 
			}

			AssetDatabase.SaveAssets();
		}
	}
}
