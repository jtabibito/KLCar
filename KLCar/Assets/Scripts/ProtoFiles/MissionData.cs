using UnityEngine;
using System.Collections;

namespace MyGameProto
{
		public partial class MissionData
		{	
				public int tempPar;
				MissionConfigData configData;

				public MissionConfigData ConfigData {
						get {
								if (configData == null) {
										configData = MissionConfigData.GetConfigData<MissionConfigData> (this.id);
								}
								return configData;
						}
				}
		}
	
}
