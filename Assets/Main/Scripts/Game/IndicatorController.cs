using UnityEngine;

namespace Main.Scripts.Game
{
    public class IndicatorController : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer bodyImage; 
        [SerializeField] private SpriteRenderer headImage;
    
        private Vector2 _direction;
        private bool _isCanLaunch;
        public Vector2 Direction => _direction;

        public bool IsCanLaunch => _isCanLaunch;

        private void LateUpdate()
        {
            Vector2 mousePosition = Input.mousePosition;
            Vector2 objectPosition = Camera.main.WorldToScreenPoint(transform.position);

            _direction = mousePosition - objectPosition;
            float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;

            _isCanLaunch = angle is >= 0f and <= 180f;

            bodyImage.color = _isCanLaunch ? Color.green : Color.red;
            headImage.color = _isCanLaunch ? Color.green : Color.red;

            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
