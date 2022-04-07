using UnityEngine;

namespace Farmer
{
    public class DirtyFarmerTimer
    {
        public bool IsDirty { get; private set; }
        
        private float _dirtyDelayDuration = 5f;
        private float _dirtyDelayElapsed = 0;

        private void Update()
        {
            if (IsDirty && _dirtyDelayElapsed <= _dirtyDelayDuration)
                _dirtyDelayElapsed += Time.deltaTime;
            else
                IsDirty = false;
        }
        
        public void FarmerIsDirty()
        {
            if (IsDirty) return;
            _dirtyDelayElapsed = 0;
            IsDirty = true;
        }
    }
}
