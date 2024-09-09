using Main.Scripts.Managers;
using UnityEngine;

namespace Main.Scripts.UI.UIMenu
{
    public class UIMenuBackgroundPanel : MonoBehaviour
    {
        [SerializeField] private UIMenuBackgroundPanelItem uiMenuBackgroundPanelItem;

        public UIMenu Owner { get; set; }

        public void Show()
        {
            var levelIndex = SaveManager.Instance.CurrentLevel;
            var level = Owner.LevelsData.Levels[levelIndex % Owner.LevelsData.Levels.Count];
            var isOpen = SaveManager.Instance.GetLevelOpenState(levelIndex);
            var isComplete = SaveManager.Instance.GetLevelCompleteState(levelIndex);
            
            uiMenuBackgroundPanelItem.Owner = this;
            
            uiMenuBackgroundPanelItem.Fill(levelIndex ,isComplete, isOpen, level.Art);
        }

        public void UI_Start()
        {
            Owner.LoadLevel();
            
            SoundManager.Instance.PlaySound(SoundType.ButtonClick);
        }

        public void UI_Levels()
        {
            Owner.ShowLevelsPanel();
            
            SoundManager.Instance.PlaySound(SoundType.ButtonClick);
        }

        public void UI_Tutorial()
        {
            Owner.ShowTutorialPanel();
            
            SoundManager.Instance.PlaySound(SoundType.ButtonClick);
        }

        public void UI_Exit()
        {
            Owner.ExitGame();
            
            SoundManager.Instance.PlaySound(SoundType.ButtonClick);
        }
    }
}
