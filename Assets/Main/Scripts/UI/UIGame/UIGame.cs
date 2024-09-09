using Main.Scripts.Data;
using UnityEngine;

namespace Main.Scripts.UI.UIGame
{
    public class UIGame : MonoBehaviour
    {
        [SerializeField] private UIGameBackground uiGameBackground;
        [SerializeField] private UIGameEffectsPanel uiGameEffectsPanel;
        [SerializeField] private UIGameHealthPanel uiGameHealthPanel;
        [SerializeField] private UIGamePointsPanel uiGamePointsPanel;
        [SerializeField] private UIGameEndPanel uiGameEndPanel;

        public LevelData LevelData { get; private set; }

        public void Initialize(LevelData data)
        {
            LevelData = data;
            Show();
        }
    
        private void Show()
        {
            uiGameBackground.Owner = this;
            uiGameBackground.Show();
        
            uiGameEffectsPanel.Owner = this;
            uiGameEffectsPanel.Show();
        
            uiGameHealthPanel.Owner = this;
            uiGameHealthPanel.Show();
        
            uiGamePointsPanel.Owner = this;
            uiGamePointsPanel.Show();

            uiGameEndPanel.Owner = this;
            uiGameEndPanel.Hide();
        }

        public void ShowEndPanel(bool isLevelComplete = false, bool isEscape = false)
        {
            uiGameEndPanel.Owner = this;
            uiGameEndPanel.Show(isLevelComplete);
        }

        public void UIHandleBlockDestroyed(EffectData data ,int pointsAdded)
        {
            uiGamePointsPanel.UpdatePoints(pointsAdded);
            uiGameEffectsPanel.ShowEffect(data.Type);
        }
    }
}
