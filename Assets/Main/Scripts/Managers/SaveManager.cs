using UnityEngine;

namespace Main.Scripts.Managers
{
    public class SaveManager : MonoBehaviour
    {
        private static SaveManager instance;
        public static SaveManager Instance => instance;

        public int CurrentLevel
        {
            get => PlayerPrefs.GetInt("SelectedLevel", 0);
            set => PlayerPrefs.SetInt("SelectedLevel", value);
        }

        private void Awake()
        {
            if (instance == null) 
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            } 
            else
            {
                Destroy(gameObject);
            }
        
            SetFirstLevel();
        }

        public void SetLevelComplete()
        {
            PlayerPrefs.SetInt($"Level{CurrentLevel}_IsComplete", 1);
            PlayerPrefs.SetInt($"Level{CurrentLevel + 1}_IsOpen", 1);
            PlayerPrefs.SetInt("SelectedLevel", CurrentLevel + 1);
            
            PlayerPrefs.Save();
        }

        public bool GetLevelCompleteState(int level)
        {
            var state =  PlayerPrefs.GetInt($"Level{level}_IsComplete", 0);
            return state == 1;
        }

        public bool GetLevelOpenState(int level)
        {
            var state =  PlayerPrefs.GetInt($"Level{level}_IsOpen", 0);
            return state == 1;
        }

        private static void SetFirstLevel()
        {
            PlayerPrefs.SetInt("Level0_IsOpen", 1);
            PlayerPrefs.Save();
        }
    }
}
