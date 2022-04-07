using System.Collections;
using Grid;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public float Speed = 6;
        
        private bool _isMoving;
        
        private Camera _mainCamera;
        private DestinationSprite _destinationSprite;

        private Vector3 _currentDirection = Vector3.zero;
        private Vector3 _newDirection = Vector3.zero;
        private Vector3 _previousTouchDir = Vector3.zero;
        
        private void Start()
        {
            _mainCamera = Camera.main;
            _destinationSprite = GetComponent<DestinationSprite>();
        }
        
        private void Update()
        {
            if (Input.touchCount > 0 && !_isMoving) {
                var moveToPosition = ChooseDirection();

                if (GridContainer.IsCellClear(moveToPosition)) {
                    SetDestinationSprite();
                    StartCoroutine(Move(moveToPosition));
                }
            }
        }

        private Vector3 ChooseDirection()
        {
            var touch = Input.GetTouch(0);
            Vector3 newTouchDir = _mainCamera.ScreenToWorldPoint(touch.position) - transform.position;
            var x = newTouchDir.x;
            var y = newTouchDir.y;
            
            if (_previousTouchDir != newTouchDir)
            {
                if (x != 0 || y != 0) {
                    if (Mathf.Abs(x) > Mathf.Abs(y)) {
                        _newDirection = x > 0 ? Vector3.right : Vector3.left;
                    }
                    else {
                        _newDirection = y > 0 ? Vector3.up : Vector3.down;
                    }
                    _previousTouchDir = newTouchDir;
                }
            }
            return transform.position + _newDirection;
        }
        
        private void SetDestinationSprite()
        {
            if (_currentDirection == _newDirection) return;
          
            _currentDirection = _newDirection;
            _destinationSprite.ChangeSprite(_newDirection);
        }

        private IEnumerator Move(Vector3 newPos)
        {
            _isMoving = true;
            while (Vector3.Distance(transform.position, newPos) > Mathf.Epsilon)
            {
                transform.position = Vector3.MoveTowards(transform.position, newPos, Speed * Time.deltaTime);
                yield return null;
            }
            transform.position = newPos;

            _isMoving = false;
        }
    }
}
