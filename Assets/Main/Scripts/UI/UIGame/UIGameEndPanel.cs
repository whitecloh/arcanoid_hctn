using Main.Scripts.Managers;
using TMPro;
using UnityEngine;

namespace Main.Scripts.UI.UIGame
{
    public class UIGameEndPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text title;
        [SerializeField] private RectTransform nextButton;
        [SerializeField] private RectTransform restartButton;
    
        public UIGame Owner { get; set; }

        public void Show(bool isLevelComplete = false , bool isEscape = false)
        {
            title.text = isLevelComplete ? "Congratulations" : isEscape ? "Menu" : "Try again";
            nextButton.gameObject.SetActive(isLevelComplete);
            restartButton.gameObject.SetActive(!isLevelComplete);
            gameObject.SetActive(true);
        }
    
        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void UI_NextLevel()
        {
            LevelManager.Instance.LoadScene(1);
            
            SoundManager.Instance.PlaySound(SoundType.ButtonClick);
        }

        public void UI_RestartLevel()
        {
            LevelManager.Instance.LoadScene(1);
            
            SoundManager.Instance.PlaySound(SoundType.ButtonClick);
        }

        public void UI_Menu()
        {
            LevelManager.Instance.LoadScene(0);
            
            SoundManager.Instance.PlaySound(SoundType.ButtonClick);
        }
    }
}
