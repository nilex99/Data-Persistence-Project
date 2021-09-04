using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager INSTANCE;
    private string saveDataPath;
    private string userName;
    
    private void Awake()
    {
        if (INSTANCE != null)
        {
            Destroy(gameObject);
            return;
        }

        INSTANCE = this;
        DontDestroyOnLoad(gameObject);
       
    }
    public string GetUserName()
    {
        return userName;
    }
    public void SetUserName(string newUserName)
    {
        userName = newUserName;
    }
    public HighScore GetCurrentHighScore()
    {
        return currentHighScore;
    }
    public bool RecordScore(int score)
    {

        if (currentHighScore == null || currentHighScore.score < score)
        {
            SetNewHighScore(score);
            return true;
        }

        return false;
    }
    
    private void SetNewHighScore(int score)
    {
        currentHighScore = new HighScore(GetUserName(), score);
        StoreHighScore();
    }

    private void LoadHighScore()
    {
        if (File.Exists(GetSaveDataPath()))
        {
            currentHighScore = JsonUtility.FromJson<HighScore>(File.ReadAllText(GetSaveDataPath()));
        }
    }

    private void StoreHighScore()
    {
        if (currentHighScore == null)
        {
            return;
        }
        
        File.WriteAllText(GetSaveDataPath(), JsonUtility.ToJson(currentHighScore));
    }
    
    string GetSaveDataPath()
    {
        if (saveDataPath == null)
        {
            saveDataPath = Application.persistentDataPath + "/savefile.json";
        }

        return saveDataPath;
    }

    [Serializable]
    public class HighScore
    {
        public string userName;
        public int score;

        public HighScore(string userName, int score)
        {
            this.userName = userName;
            this.score = score;
        }
    }

    
}
