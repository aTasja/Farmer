using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public float Speed = 6;


        private bool _walking;

        private Vector3 _moveToPosition;
        private Vector3 _currentDirection;
        private Vector3 _targetDirection;

        private Vector3 _previousDir = Vector3.zero;
        private Camera _mainCamera;

        private void Start()
        {
            _mainCamera = Camera.main;
            _currentDirection = Vector3.zero;
            _targetDirection = Vector3.zero;
            _walking = false;
        }
        
        private void Update()
        {
            if (Input.touchCount > 0 && !_walking) {
                ChooseDirection();

                if (GameManager.IsCellClear(_moveToPosition)) {
                    StartCoroutine(MovePlayer(_moveToPosition));
                }
            }
        }

        private void ChooseDirection()
        {
            var touch = Input.GetTouch(0);
            Vector3 absoluteDir = _mainCamera.ScreenToWorldPoint(touch.position) - transform.position;
            var x = absoluteDir.x;
            var y = absoluteDir.y;
            
            if (_previousDir != absoluteDir)
            {
                _previousDir = absoluteDir;

                if (x != 0 || y != 0) {
                    if (Mathf.Abs(x) > Mathf.Abs(y)) {
                        _targetDirection = x > 0 ? Vector3.right : Vector3.left;
                    }
                    else {
                        _targetDirection = y > 0 ? Vector3.up : Vector3.down;
                    }

                   
                }
            }
            _moveToPosition = transform.position + _targetDirection;
        }

        private IEnumerator MovePlayer(Vector3 newPos)
        {
            _walking = true;

            if (_currentDirection != _targetDirection)
            {
                _currentDirection = _targetDirection;
                gameObject.GetComponent<DestinationSprite>().ChangeSprite(_targetDirection);
            }

            while ((newPos - transform.position).sqrMagnitude > Mathf.Epsilon)
            {
                transform.position = Vector3.MoveTowards(transform.position, newPos, Speed * Time.deltaTime);
                yield return null;
            }
            transform.position = newPos;

            _walking = false;
        }
    }
    
    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (OnEventCattorHarvested != null)
                OnEventCattorHarvested();
            Destroy(gameObject);
        }
    }
}
