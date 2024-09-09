using System;
using System.Collections;
using Main.Scripts.Data;
using Main.Scripts.Managers;
using UnityEngine;

namespace Main.Scripts.Game
{
    public class Ball : MonoBehaviour
    {
        private BallData _ballData;
        private IndicatorController indicator;
        private Rigidbody2D _rb2D;
    
        private Vector2 _currentDirection;
        private float _currentSpeed;
    
        private Coroutine _increaseSpeedCoroutine;
    
        public delegate void BallLost();
        public static event BallLost OnBallLost;

        public void Initialize(BallData data)
        {
            _ballData = data;
        
            _rb2D = GetComponent<Rigidbody2D>();
            indicator = GetComponentInChildren<IndicatorController>();

            transform.position = _ballData.StartPosition;
            _currentSpeed = _ballData.Speed;
        }

        private void FixedUpdate()
        {
            if (_currentDirection == Vector2.zero) return;
            _rb2D.velocity = _currentDirection * _currentSpeed;
            _rb2D.velocity = Vector2.ClampMagnitude(_rb2D.velocity, _ballData.Speed * 1.5f);
                
            if (_rb2D.velocity.magnitude < 0.1f)
            {
                _rb2D.velocity = _currentDirection * 0.1f;
            }
        }


        private void OnCollisionEnter2D(Collision2D other)
        {
            var block = other.gameObject.GetComponentInParent<Block>();
            var damageWall = other.gameObject.GetComponent<DamageLine>();

            if (other.contacts.Length > 0)
            {
                _currentDirection = Vector2.Reflect(_currentDirection, other.contacts[0].normal);
            }
        
            if (block != null)
            {
                block.OnHit();
            }

            if (damageWall != null)
            {
                OnBallLost?.Invoke();
                ResetBall();
            }
            
            SoundManager.Instance.PlaySound(SoundType.Hit);
        }

        public bool TryLaunchBall()
        {
            if(!indicator.IsCanLaunch) return false;
        
            _currentDirection = indicator.Direction.normalized;
            indicator.gameObject.SetActive(false);

            LevelManager.Instance.HandleCursorActive();
            return true;
        }

        private void ResetBall()
        {
            _currentDirection = Vector2.zero;
            _rb2D.velocity = _currentDirection;
            transform.position = _ballData.StartPosition;
            
            indicator.gameObject.SetActive(true);
        }

        public void HandleBlockDestroyed(EffectData data)
        {
            if (data.Type != EffectType.BallSpeed) return;
        
            if (_increaseSpeedCoroutine != null)
            {
                StopCoroutine(_increaseSpeedCoroutine);
            }
            _increaseSpeedCoroutine = StartCoroutine(IncreaseSpeedTemporarily(data.Duration, data.ChangeValue));
        }

        private IEnumerator IncreaseSpeedTemporarily(float duration , float value)
        {
            _currentSpeed *= value;

            yield return new WaitForSeconds(duration);

            _currentSpeed = _ballData.Speed;
        }
    }
}
