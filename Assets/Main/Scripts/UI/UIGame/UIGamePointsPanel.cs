using Main.Scripts.Managers;
using TMPro;
using UnityEngine;

namespace Main.Scripts.UI.UIGame
{
    public class UIGamePointsPanel : MonoBehaviour
    {
        private static readonly int PointsAdd = Animator.StringToHash("pointsAdd");
    
        [SerializeField] private TMP_Text pointsSum;
        [SerializeField] private TMP_Text pointsToAdd;
    
        [SerializeField] private Animator animator;

        public UIGame Owner { get; set; }

        public void Show()
        {
            pointsSum.text = $"{0}/{LevelManager.Instance.PointsInBlocks}";
        }

        public void UpdatePoints(int pointsAdded)
        {
            pointsSum.text = $"{LevelManager.Instance.PointsAdded}/{LevelManager.Instance.PointsInBlocks}";
            pointsToAdd.text = $"+{pointsAdded}";
            animator.SetTrigger(PointsAdd);
        }
    }
}
