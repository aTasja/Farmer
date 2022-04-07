using UnityEngine;

namespace UI
{
    public class UIButtons:MonoBehaviour
    {
        public delegate void BombButtonAction();
        public static event BombButtonAction OnEventBomb;
        
        public void BombButtonHandler() => OnEventBomb?.Invoke();
    }
}
