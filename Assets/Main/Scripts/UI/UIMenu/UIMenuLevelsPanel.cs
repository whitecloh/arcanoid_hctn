using System.Collections.Generic;
using Main.Scripts.Managers;
using UnityEngine;

namespace Main.Scripts.UI.UIMenu
{
    public class UIMenuLevelsPanel : MonoBehaviour
    {
        [SerializeField] private UIMenuLevelsPanelItem uiGameEffectesPanelItem;
        [SerializeField] private RectTransform itemsParent;

        private List<UIMenuLevelsPanelItem> ItemsPool = new();

        public UIMenu Owner { get; set; }

        public void Show()
        {
            var levelIndex = Owner.LevelsData.GetIndex(SaveManager.Instance.CurrentLevel);
            
            for (var i = 0 ; i < Owner.LevelsData.Levels.Count ; i++)
            {
                var level = Owner.LevelsData.Levels[i];
                var firstLevel = levelIndex + i;
                
                var prefab = Instantiate(uiGameEffectesPanelItem, itemsParent);
                
                var isSelected = SaveManager.Instance.CurrentLevel == firstLevel;
                var isOpen = SaveManager.Instance.GetLevelOpenState(firstLevel);
                var isComplete = SaveManager.Instance.GetLevelCompleteState(firstLevel);
                
                prefab.Owner = this;
                prefab.Fill(firstLevel ,isComplete, isOpen, isSelected, level.Art);
                ItemsPool.Add(prefab);
            }
            
            Hide();
        }

        public void UpdatePanel()
        {
            var levelIndex = Owner.LevelsData.GetIndex(SaveManager.Instance.CurrentLevel);
            
            for (var i = 0 ; i < ItemsPool.Count ; i++)
            {
                var level = Owner.LevelsData.Levels[i];
                var firstLevel = levelIndex + i;
                var isSelected = SaveManager.Instance.CurrentLevel == firstLevel;
                var isOpen = SaveManager.Instance.GetLevelOpenState(firstLevel);
                var isComplete = SaveManager.Instance.GetLevelCompleteState(firstLevel);
                
                ItemsPool[i].Fill(firstLevel ,isComplete, isOpen, isSelected, level.Art);
            }
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }
        
        public void UI_Start()
        {
            Owner.LoadLevel();
            
            SoundManager.Instance.PlaySound(SoundType.ButtonClick);
        }

        public void UI_BackToMenu()
        {
            Hide();
            
            SoundManager.Instance.PlaySound(SoundType.ButtonClick);
        }
        
        public void UI_Tutorial()
        {
            Owner.ShowTutorialPanel();
            
            SoundManager.Instance.PlaySound(SoundType.ButtonClick);
        }
    }
}
