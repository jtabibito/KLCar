using UnityEngine;
using System.Collections;

namespace MyGameProto
{
		public partial class PlayerInfo
		{	
				public bool needUpdate = false;
				public delegate void OnPlayerInfoUpdate ();

				public event OnPlayerInfoUpdate onPlayerInfoUpdate;

				void Update ()
				{
						if (needUpdate && this.onPlayerInfoUpdate != null) {
								this.onPlayerInfoUpdate ();
						}
				}
		}

}