using UnityEngine;

namespace Farmer
{
    public class FarmerCollision : MonoBehaviour
    {
        public delegate void DirtyFarmerAction(int score);
        public static event DirtyFarmerAction OnEventDirtyFarmer;
        
        public delegate void PlayerCaughtByFarmerAction();
        public static event PlayerCaughtByFarmerAction OnEventPlayerCaughtByFarmer;

        public static int BombScore { get; private set; }
        
        public DestinationSprite DestinationSprite { get; private set; }

        private DirtyFarmerTimer _dirtyFarmerTimer;
        private Collider2D _playerCollider;
        private Collider2D _farmerCollider;

        private void Start()
        {
            BombScore = 0;
            _dirtyFarmerTimer = new DirtyFarmerTimer();
            _farmerCollider = GetComponent<Collider2D>();
            DestinationSprite = GetComponent<DestinationSprite>();
            _playerCollider = GetComponent<FarmerMovement>().PlayerCollider;
        }
        
        public bool IsDirty => _dirtyFarmerTimer.IsDirty;
        
        private void Update()
        {
            if (!_dirtyFarmerTimer.IsDirty && _farmerCollider.bounds.Intersects(_playerCollider.bounds))
                OnEventPlayerCaughtByFarmer?.Invoke();
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Bomb")) {
                BombScore++;
                DestinationSprite.SetDirtySprite();
                _dirtyFarmerTimer.FarmerIsDirty();
                OnEventDirtyFarmer?.Invoke(BombScore);
                Destroy(col.gameObject);
            }
        }
    }
}
