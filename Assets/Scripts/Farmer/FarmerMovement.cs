using System.Collections;
using Grid;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Farmer
{
    public class FarmerMovement : MonoBehaviour
    {
        public float Speed = 1f;

        [SerializeField] private GameObject _player;
        
        private bool _isMoving;
        private FarmerCollision _farmerCollision;
        private Vector3 _playerPos;
        private Vector3 _currentDirection = Vector3.zero;
        private Vector3 _newDirection = Vector3.zero;

        private void Awake() => _farmerCollision = GetComponent<FarmerCollision>();
        
        public Collider2D PlayerCollider => _player.GetComponent<Collider2D>();

        void Update()
        {
            if (_farmerCollision.IsDirty || _isMoving) return;

            var directionsToPLayer = GetDirectionsToPlayer();
            _newDirection = RandomlyChooseStartDirection(directionsToPLayer);
            
            SetDestinationSprite();
            StartCoroutine(Move(transform.position + _newDirection));
        }

        private float[] GetDirectionsToPlayer()
        {
            _playerPos = _player.GetComponent<Transform>().position;
            return new []
            {
                transform.position.x - _playerPos.x,
                transform.position.y - _playerPos.y,
            };
        }

        private Vector3 RandomlyChooseStartDirection(float[] xANDy)
        {
            Vector3 finalDest;
            if (Random.Range(0, xANDy.Length) == 0)
            {
                finalDest =  xANDy[0] >= 0 ? Vector3.left : Vector3.right;
            }
            else
            {
                finalDest =  xANDy[1] >= 0 ? Vector3.down : Vector3.up;
            }
        
            Vector3 moveToPosition = transform.position + finalDest;
            ChangeDirectionIfObstacleOnTheWay(xANDy, moveToPosition, ref finalDest);
            return finalDest;
        }

        private void ChangeDirectionIfObstacleOnTheWay(float[] xANDy, Vector3 moveToPosition, ref Vector3 finalDest)
        {
            if (!GridContainer.IsCellClear(moveToPosition))
            {
                if (finalDest == Vector3.left || finalDest == Vector3.right)
                {
                    finalDest = xANDy[1] >= 0 ? Vector3.down : Vector3.up;
                }
                else
                {
                    finalDest = xANDy[0] >= 0 ? Vector3.left : Vector3.right;
                }
            }
        }

        private void SetDestinationSprite()
        {
            if (_currentDirection == _newDirection) return;
            _currentDirection = _newDirection;
            _farmerCollision.DestinationSprite.ChangeSprite(_newDirection);
        }

        private IEnumerator Move(Vector3 newPos)
        {
            _isMoving = true;
            
            while (Vector3.Distance(newPos, transform.position) > Mathf.Epsilon)
            {
                transform.position = Vector3.MoveTowards(transform.position, newPos, Speed * Time.deltaTime);
                yield return null;
            }
            transform.position = newPos;
            
            _isMoving = false;
        }
    }
}

