using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public TextMeshProUGUI bestScoretxt;
    public TMP_InputField nameInputField;

    public string playerName;
    public int highScore;
    public static MenuManager Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadSaveData(bestScoretxt);
    }

    public void StartButton()
    {
        playerName = nameInputField.text;
        SceneManager.LoadScene(1);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void LoadSaveData(TextMeshProUGUI bestScoreText)
    {
        string filePath = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(filePath))
        {
            string data = File.ReadAllText(filePath);

            Player playerData = JsonUtility.FromJson<Player>(data);

            bestScoreText.text = $"Best Score: {playerData.PlayerName} : {playerData.score}";
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                nameInputField.text = playerData.PlayerName;
                highScore = playerData.score;
            }
        }
        else return;
    }
}
