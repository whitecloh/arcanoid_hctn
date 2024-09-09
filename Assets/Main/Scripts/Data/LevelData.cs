using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Main.Scripts.Data
{
    [Serializable]
    public class LevelData
    {
        [SerializeField] private LevelArtData art;
        [SerializeField] private PlayerData playerData;
        [SerializeField] private BallData ballData;
        [SerializeField] private List<BlockData> blocksData;
        [SerializeField] private List<EffectData> effects;
    
        public LevelArtData Art => art;

        public PlayerData PlayerData => playerData;
        public BallData BallData => ballData;
        public IReadOnlyList<BlockData> BlocksData => blocksData;
        public List<EffectData> Effects => effects;

        public int SetBlockHp()
        { 
            return Random.Range(1, 4);
        }
    }

    [Serializable]
    public class PlayerData
    {
        [SerializeField] private Vector3 startPosition = new Vector3(0, -100, 0);
        [SerializeField] private Vector2 platformSize = new Vector2(40,5); 
        [SerializeField] private float moveSpeed = 75f;
        [SerializeField, Range(3, 5)] private int healths = 3;

        public Vector3 StartPosition => startPosition;
        public Vector2 Size => platformSize;
        public float MoveSpeed => moveSpeed;
        public int Healths => healths;
    }

    [Serializable]
    public class BallData
    {
        [SerializeField] private Vector3 startPosition = new Vector3(0, -90, 0);
        [SerializeField] private float speed = 100f;
    
        public Vector3 StartPosition => startPosition;
        public float Speed => speed;
    }

    [Serializable]
    public class BlockData
    {
        [SerializeField] private int blockIndex = 0;
        [SerializeField] private EffectType type;
        [SerializeField] private bool isActive = true;
        [SerializeField, Range(1, 3)] private int health = 0;

        public int Index => blockIndex;
        public EffectType Type => type;
        public bool IsActive => isActive;
        public int Health => health;
    }

    [Serializable]
    public class EffectData
    {
        [SerializeField] private EffectType type;
        [SerializeField] private int duration;
        [SerializeField] private float changeValue;
        [SerializeField] private Sprite sprite;

        public EffectType Type => type;
        public int Duration => duration;
        public float ChangeValue => changeValue;
        public Sprite Sprite => sprite;
    }

    [Serializable]
    public class LevelArtData
    {
        [SerializeField] private Sprite backgroundImage;
        [SerializeField] private Sprite middleImage;
        [SerializeField] private Sprite closeImage;
        [SerializeField] private Sprite fadeImage;

        public Sprite BackgroundImage => backgroundImage;
        public Sprite MiddleImage => middleImage;
        public Sprite CloseImage => closeImage;
        public Sprite FadeImage => fadeImage;
    }

    public enum EffectType
    {
        None,
        BallSpeed,
        PlateSize,
        ReverseInput
    }
}