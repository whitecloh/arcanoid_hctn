using Main.Scripts.Managers;
using UnityEngine;

namespace Main.Scripts.UI.UIMenu
{
    public class UIMenuTutorialPanel : MonoBehaviour
    {
        public UIMenu Owner { get; set; }

        public void Show()
        {
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void UI_Back()
        {
            Hide();
            SoundManager.Instance.PlaySound(SoundType.ButtonClick);
        }
    }
}
