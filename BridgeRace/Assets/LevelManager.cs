using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public bool isFinished;
    private Level currentLevel;
    private int levelIndex = 0;

    private GameObject currentLevelObject;
    [SerializeField] private List<Character> characters;
    [SerializeField] private Level[] levels;
    private void Start()
    {
        LoadLevel();
    }

    public void ReplayLevel()
    {
        LoadLevel();
    }
    public void LevelUp()
    {
        levelIndex++;
        LoadLevel();
    }
    public void ResetLevel()
    {
        levelIndex = 0;
        LoadLevel();
    }
    private void LoadLevel()
    {
        isFinished = false;
        if (currentLevel != null)
        {
            currentLevel.OnDespawn();
            Destroy(currentLevelObject);
        }
        currentLevel = levels[levelIndex];
        OnInit();
    }

    private void OnInit()
    {
        currentLevelObject = Instantiate(currentLevel.gameObject);
        currentLevelObject.transform.position = currentLevel.levelPosition;
        currentLevel.SpawnChar(characters);
        foreach(Character character in characters)
        {
            if(character.gameObject.GetComponent<Bot>() is Bot)
            {
                character.gameObject.GetComponent<Bot>().finalTarget = currentLevel.finalTarget;
            }
        }
    }
}
