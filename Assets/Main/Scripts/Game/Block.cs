using System.Collections;
using System.Collections.Generic;
using Main.Scripts.Data;
using UnityEngine;

namespace Main.Scripts.Game
{
    public class Block : MonoBehaviour
    {
        [SerializeField] private List<Color> color;
        [SerializeField] private SpriteRenderer blockImage; 
        [SerializeField] private SpriteRenderer destroyImage;
        [SerializeField] private List<Sprite> destroyEffectList;
        [SerializeField] private float fadeDuration = 0.1f;
        [SerializeField] private float animationSpeed = 0.05f;
    
        private EffectData _data;
        private bool _isActive;
        private int _maxHealth;
        private int _health;

        public delegate void BlockDestroyedHandler(EffectData data, int health);
        public static event BlockDestroyedHandler OnBlockDestroyed;

        public void Initialize(EffectData effectType, bool isActive , int hp , Sprite icon)
        {
            _data = effectType;
            _isActive = isActive;
            _maxHealth = hp;
            _health = _maxHealth;
            blockImage.sprite = icon;
            blockImage.color = color[_health - 1];
        
            gameObject.SetActive(isActive);
            destroyImage.gameObject.SetActive(false);
        }

        public void OnHit()
        {
            if (!_isActive) return;

            _health--;
        
            if (_health <= 0)
            {
                _isActive = false;
                StartCoroutine(HandleDestruction());
            
                OnBlockDestroyed?.Invoke(_data,_maxHealth);
            }
            else
            {
                blockImage.color = color[_health - 1];
            }
        }
        
        private IEnumerator HandleDestruction()
        {
            var elapsedTime = 0f;

            var originalColor = blockImage.color;

            blockImage.GetComponent<Collider2D>().enabled = false;

            while (elapsedTime < fadeDuration)
            {
                blockImage.color = Color.Lerp(originalColor, new Color(originalColor.r, originalColor.g, originalColor.b, 0), elapsedTime / fadeDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            blockImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
            
            destroyImage.gameObject.SetActive(true);

            foreach (var sprite in destroyEffectList)
            {
                destroyImage.sprite = sprite;
                yield return new WaitForSeconds(animationSpeed);
            }

            destroyImage.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}