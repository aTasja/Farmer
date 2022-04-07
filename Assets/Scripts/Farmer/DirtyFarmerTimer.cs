using System;
using UnityEngine;

namespace Farmer
{
    public class _dirtyFarmerTimer:MonoBehaviour
    {
        public float DirtyDelayDuration = 5f;
        public bool IsDirty { get; private set; }

        private float dirtyDelayElapsed = 0;
        
        private void OnEnable()
        {
            FarmerCollision.OnEventDirtyFarmer += FarmerIsDirty;
        }

        private void OnDisable()
        {
            FarmerCollision.OnEventDirtyFarmer -= FarmerIsDirty;
        }

        private void Update()
        {
            if (IsDirty && dirtyDelayElapsed <= DirtyDelayDuration)
                dirtyDelayElapsed += Time.deltaTime;
            else
                IsDirty = false;
        }
        
        private void FarmerIsDirty(int v)
        {
            if (!IsDirty)
            {
                dirtyDelayElapsed = 0;
                IsDirty = true;
            }
        }
    }
}
