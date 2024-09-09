using System.Collections;
using Main.Scripts.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.UIGame
{
    public class UIGameEffectsPanelItem : MonoBehaviour
    { 
        [SerializeField] private Image effectIcon;
        [SerializeField] private TMP_Text title;
        [SerializeField] private TMP_Text time;
        public UIGameEffectsPanel Owner { get; set; }

        public EffectType EffectType { get; private set; }
        private int _effectDuration = 0;

        private Coroutine _timeDecrease;

        public void Fill(Sprite icon, int timer , EffectType type)
        {
            effectIcon.sprite = icon;
        
            _effectDuration = timer;
        
            time.text = $"{timer}s";

            var text = string.Empty;

            EffectType = type;
        
            switch (EffectType)
            {
                case EffectType.None:
                    break;
                case EffectType.BallSpeed:
                    text = "Speedy ball";
                    break;
                case EffectType.PlateSize:
                    text = "Small plate";
                    break;
                case EffectType.ReverseInput:
                    text = "Reverse input";
                    break;
            }
            title.text = text;
        }

        public void ActivateTimer()
        {
            if (_timeDecrease != null)
            {
                StopCoroutine(_timeDecrease);
            } 
            _timeDecrease = StartCoroutine(TimeDecrease());
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private IEnumerator TimeDecrease()
        {
            var timer = _effectDuration;
        
            while (timer > 0)
            {
                timer--;
                time.text = $"{timer}s";
            
                yield return new WaitForSeconds(1f);
            }
        
            gameObject.SetActive(false);
        }
    }
}
