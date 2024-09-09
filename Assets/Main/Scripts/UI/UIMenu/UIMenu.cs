using Main.Scripts.Data;
using Main.Scripts.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main.Scripts.UI.UIMenu
{
    public class UIMenu : MonoBehaviour
    {
        [SerializeField] private UIMenuBackgroundPanel uiMenuBackgroundPanel;
        [SerializeField] private UIMenuLevelsPanel uiMenuLevelsPanel;
        [SerializeField] private UIMenuTutorialPanel uiMenuTutorialPanel;

        public LevelsData LevelsData { get; private set; }

        public void Initialize(LevelsData data)
        {
            LevelsData = data;
            Show();
        }

        private void Show()
        {
            uiMenuBackgroundPanel.Owner = this;
            uiMenuBackgroundPanel.Show();
        
            uiMenuLevelsPanel.Owner = this;
            uiMenuLevelsPanel.Show();

            uiMenuTutorialPanel.Owner = this;
            uiMenuTutorialPanel.Hide();
        }

        public void ShowLevelsPanel()
        {
            uiMenuLevelsPanel.gameObject.SetActive(true);
            uiMenuLevelsPanel.UpdatePanel();
            
            SoundManager.Instance.PlaySound(SoundType.ShowPanel);
        }

        public void ShowTutorialPanel()
        {
            uiMenuTutorialPanel.gameObject.SetActive(true);
            uiMenuTutorialPanel.Owner = this;
            uiMenuTutorialPanel.Show();
            
            SoundManager.Instance.PlaySound(SoundType.ShowPanel);
        }

        public void SetLevel(int level)
        {
            SaveManager.Instance.CurrentLevel = level;
            uiMenuBackgroundPanel.Show();
        }
        
        public void LoadLevel()
        {
            SceneManager.LoadScene("Game");
        }

        public void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
