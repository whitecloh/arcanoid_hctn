using System.Collections;
using Main.Scripts.Data;
using UnityEngine;

namespace Main.Scripts.Game
{
    public class PlayerController : MonoBehaviour
    {
        private Ball _ball;
        private PlayerData _playerData;

        private float _currentMoveSpeed;
        private bool _isInputInverted = false;
        private bool _isGameActive = false;
    
        private Coroutine _shrinkCoroutine;
        private Coroutine _revertCoroutine;

        public void Initialize(PlayerData data , Ball ball)
        {
            _playerData = data;
            _ball = ball;
            transform.position = _playerData.StartPosition;
            transform.localScale = _playerData.Size;
            _currentMoveSpeed = _playerData.MoveSpeed;
        }
    
        private void Update()
        {
            if (_isGameActive)
            {
                MovePlatform();
            }
        
            if (!_isGameActive && Input.GetMouseButtonDown(0))
            {
                _isGameActive = _ball.TryLaunchBall();
            }
        }
    
        private void MovePlatform()
        {
            var moveDirection = Input.GetAxis("Horizontal");

            if (_isInputInverted)
            {
                moveDirection = -moveDirection;
            }

            var newPosition = transform.position + new Vector3(moveDirection * _currentMoveSpeed * Time.deltaTime, 0f, 0f);
            newPosition.x = Mathf.Clamp(newPosition.x, -100f, 100f);
            transform.position = newPosition;
        }
    
        public void HandleBallLost()
        {
            _isGameActive = false;
        
            transform.position = _playerData.StartPosition;
        }

        public void HandleBlockDestroyed(EffectData data)
        {
            switch (data.Type)
            {
                case EffectType.PlateSize:
                {
                    if (_shrinkCoroutine != null)
                    {
                        StopCoroutine(_shrinkCoroutine);
                    }
                    _shrinkCoroutine = StartCoroutine(ShrinkPlatform(data.Duration,data.ChangeValue));
                    break;
                }
                case EffectType.ReverseInput:
                {
                    if (_revertCoroutine != null)
                    {
                        StopCoroutine(_revertCoroutine);
                    }
                    _revertCoroutine = StartCoroutine(RevertInput(data.Duration));
                    break;
                }
            }
        }

        private IEnumerator ShrinkPlatform(float duration , float value)
        {
            transform.localScale = _playerData.Size * value;

            yield return new WaitForSeconds(duration);

            transform.localScale = _playerData.Size;
        }

        private IEnumerator RevertInput(float duration)
        {
            _isInputInverted = true;
            yield return new WaitForSeconds(duration);
            _isInputInverted = false;
        }
    }
}
