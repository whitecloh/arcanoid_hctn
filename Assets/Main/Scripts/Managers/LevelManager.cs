using System;
using System.Linq;
using Main.Scripts.Data;
using Main.Scripts.Game;
using Main.Scripts.UI.UIGame;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main.Scripts.Managers
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Transform levelParent;
        [SerializeField] private LevelsData levelsData;
        [SerializeField] private BlocksGenerator blocksGenerator;
        [SerializeField] private Ball ball;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private UIGame uiGame;

        private LevelData _levelData;

        private int _playerHealths = 0;
        public int PointsInBlocks => blocksGenerator.PointsInBlocks;
        public int PointsAdded { get; set; }
    
        private static LevelManager instance;
        public static LevelManager Instance => instance;

        private void Awake()
        {
            if (instance == null) 
            {
                instance = this;
            } 
            else
            {
                Destroy(gameObject);
            }

            var isNeedRandomGeneration = SaveManager.Instance.CurrentLevel > levelsData.Levels.Count - 1;
        
            _levelData = levelsData.Levels[SaveManager.Instance.CurrentLevel % levelsData.Levels.Count];
            blocksGenerator.Initialize(_levelData, isNeedRandomGeneration);
            ball.Initialize(_levelData.BallData);
            playerController.Initialize(_levelData.PlayerData, ball);
            uiGame.Initialize(_levelData);

            _playerHealths = _levelData.PlayerData.Healths;
        }
    
        private void OnEnable()
        {
            Block.OnBlockDestroyed += HandleBlockDestroyed;
            Ball.OnBallLost += HandleBallLost;
        }

        private void OnDisable()
        {
            Block.OnBlockDestroyed -= HandleBlockDestroyed;
            Ball.OnBallLost -= HandleBallLost;
        }

        private void LateUpdate()
        {
            InputHandler();
        }

        public void HandleCursorActive(bool isActive = false)
        {
            Cursor.visible = isActive;
        }

        private void HandleBlockDestroyed(EffectData data, int healths)
        {
            PointsAdded += healths;
        
            uiGame.UIHandleBlockDestroyed(data, healths);
            ball.HandleBlockDestroyed(data);
            playerController.HandleBlockDestroyed(data);

            SoundManager.Instance.PlaySound(data.Type > 0 ? SoundType.AddEffect : SoundType.BlockDestroy);

            if(PointsAdded < PointsInBlocks) return;

            HandleCursorActive(true);
            ShowHideLevel(false);
            
            SaveManager.Instance.SetLevelComplete();
            
            uiGame.ShowEndPanel(true);
            
            SoundManager.Instance.PlaySound(SoundType.WinLevel);
        }

        private void HandleBallLost()
        {
            _playerHealths--;
            
            playerController.HandleBallLost();
            uiGame.UIHandleBallLost();

            if (_playerHealths <= 0)
            {
                ShowHideLevel(false);
                uiGame.ShowEndPanel();
            }
            
            HandleCursorActive(true);
            SoundManager.Instance.PlaySound(_playerHealths <= 0 ? SoundType.LostLevel : SoundType.DamageHit);
        }

        private void InputHandler()
        {
            if(!Input.GetKeyDown(KeyCode.Escape)) return;
            
            LoadScene(0);
        }

        public void LoadScene(int index)
        {
            SceneManager.LoadScene(index);
        }

        public void ShowHideLevel(bool isActive)
        {
            levelParent.gameObject.SetActive(isActive);
        }
    }
}
