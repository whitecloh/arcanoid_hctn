using Main.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.UIGame
{
    public class UIGameBackground : MonoBehaviour
    {
        [SerializeField] private Image background;
        [SerializeField] private Image middleImage;
        [SerializeField] private Image middleImageAdd;
        [SerializeField] private Image closeImage;
        [SerializeField] private Image closeImageAdd;
        [SerializeField] private Image fadeImage;
        [SerializeField] private TMP_Text level;

        public UIGame Owner { get; set; }
    
        public void Show()
        {
            background.sprite = Owner.LevelData.Art.BackgroundImage;
            middleImage.sprite = Owner.LevelData.Art.MiddleImage;
            middleImageAdd.sprite = Owner.LevelData.Art.MiddleImage;
            closeImage.sprite = Owner.LevelData.Art.CloseImage;
            closeImageAdd.sprite = Owner.LevelData.Art.CloseImage;
            fadeImage.sprite = Owner.LevelData.Art.FadeImage;

            level.text = $"Level: {SaveManager.Instance.CurrentLevel + 1}";
        }
    }
}
