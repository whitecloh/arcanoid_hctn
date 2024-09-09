using Main.Scripts.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.UIMenu
{
    public class UIMenuLevelsPanelItem : MonoBehaviour
    {
        [SerializeField] private Image background;
        [SerializeField] private Image middleImage;
        [SerializeField] private Image closeImage;
        [SerializeField] private Image fadeImage;
        
        [SerializeField] private TMP_Text level;
        [SerializeField] private RectTransform closeState;
        [SerializeField] private RectTransform completeState; 
        [SerializeField] private RectTransform selectState;
        
        public UIMenuLevelsPanel Owner { get; set; }

        private int _levelIndex = 0;
        private bool _isComplete = false;
        private bool _isOpen = false;
   
        public void Fill(int index, bool isComplete, bool isOpen, bool isSelected, LevelArtData data)
        {
            _levelIndex = index;
            _isComplete = isComplete;
            _isOpen = isOpen;
            
            level.text = $"{_levelIndex + 1}";
            
            background.sprite = data.BackgroundImage;
            middleImage.sprite = data.MiddleImage;
            closeImage.sprite = data.CloseImage;
            fadeImage.sprite = data.FadeImage;
            
            closeState.gameObject.SetActive(!_isOpen);
            completeState.gameObject.SetActive(_isComplete);
            selectState.gameObject.SetActive(isSelected);
            level.gameObject.SetActive(!_isComplete);
        }

        public void UI_SetLevel()
        {
            if(!_isOpen || _isComplete) return;
            
            Owner.Owner.SetLevel(_levelIndex);
            Owner.UpdatePanel();
        }
    }
}
