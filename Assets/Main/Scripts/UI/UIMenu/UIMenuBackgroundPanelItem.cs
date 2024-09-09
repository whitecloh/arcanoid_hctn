using Main.Scripts.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.UIMenu
{
    public class UIMenuBackgroundPanelItem : MonoBehaviour
    {
        [SerializeField] private Image background;
        [SerializeField] private Image middleImage;
        [SerializeField] private Image closeImage;
        [SerializeField] private Image fadeImage;
        
        [SerializeField] private TMP_Text level;
        [SerializeField] private RectTransform closeState;
        [SerializeField] private RectTransform completeState; 
        
        public UIMenuBackgroundPanel Owner { get; set; }

        private int _levelIndex = 0;
   
        public void Fill(int index, bool isComplete, bool isOpen, LevelArtData data)
        {
            _levelIndex = index;
            level.text = $"{_levelIndex + 1}";
            
            background.sprite = data.BackgroundImage;
            middleImage.sprite = data.MiddleImage;
            closeImage.sprite = data.CloseImage;
            fadeImage.sprite = data.FadeImage;
            
            closeState.gameObject.SetActive(!isOpen);
            completeState.gameObject.SetActive(isComplete);
            level.gameObject.SetActive(isOpen && !isComplete);
        }
    }
}
