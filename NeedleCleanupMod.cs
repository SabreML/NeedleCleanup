using BepInEx;
using System.Security.Permissions;
using UnityEngine;

#pragma warning disable CS0618
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
#pragma warning restore CS0618

namespace NeedleCleanup
{
	[BepInPlugin("sabreml.needlecleanup", "NeedleCleanup", "1.0.1")]
	public class NeedleCleanupMod : BaseUnityPlugin
	{
		public void OnEnable()
		{
			On.Room.Loaded += Room_LoadedHK;
		}

		private void Room_LoadedHK(On.Room.orig_Loaded orig, Room self)
		{
			if (self.abstractRoom.firstTimeRealized && self.abstractRoom.shelter)
			{
				int spearsRemoved = self.abstractRoom.entities.RemoveAll(entity =>
				{
					if (entity is AbstractSpear abstractSpear && abstractSpear.needle && !abstractSpear.stuckInWall)
					{
						return true;
					}
					return false;
				});
				if (spearsRemoved > 0)
				{
					Debug.Log($"(NeedleCleanup) {spearsRemoved} spears removed from shelter");
				}
			}
			orig(self);
		}
	}
}
