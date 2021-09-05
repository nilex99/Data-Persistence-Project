using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public HighScore highScore;

    [SerializeField] InputField nameInputField;
    public string playerName;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(Instance);
        LoadHighScore();
        
    }

    public void RecordHighScore(int points)
    {
        highScore = new HighScore();
        highScore.name = playerName;
        highScore.score = points;
        SaveHighScore();
    }

    public void SaveHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        string json = JsonUtility.ToJson(highScore);
        File.WriteAllText(path, json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            highScore = JsonUtility.FromJson<HighScore>(json);
        }
    }

    public void InputName()
    {
        playerName = nameInputField.text;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    [System.Serializable]
    public class HighScore
    {
        public string name;
        public int score;
    }
}