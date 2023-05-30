#if UNITY_IOS
using System;
using Gameplay.GameCore.States;
using Unity.Advertisement.IosSupport;
using UnityEngine;
using UnityEngine.iOS;

namespace Game.States
{
    public class SGameATTRequest: SGameAbstract
    {
        public override void Enter()
        {
            Debug.Log("att enter");
            if (Application.isEditor||OldVersion())
            {
                Debug.Log("att noNeed");
                GoNext();
                return;
            }

            if (NotDetermined())
            {
                ATTrackingStatusBinding.RequestAuthorizationTracking();
            }
            GoNext();
        }

        private static bool OldVersion()
        {
            var currentVersion = new Version(Device.systemVersion); 
            var ios14 = new Version("14.5");
            return currentVersion < ios14;
        }

        private static bool NotDetermined()
        {
            Debug.Log("att try");

            var status = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
            Debug.Log($"att {status}");
            return status == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED;
        }
        
        private void GoNext()
        {
            Debug.Log("att pass");
            ChangeState<SGameLevelLoad>();
        }
    }
}
#endif
