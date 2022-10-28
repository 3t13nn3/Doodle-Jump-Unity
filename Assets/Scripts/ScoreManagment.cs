using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManagment : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;

    public TextMeshProUGUI highestScoreText;

    public InputField nameText;

    private string savedName;

    [SerializeField]
    private int savedHighScore;

    [SerializeField]
    private playersScore _playersScore = new playersScore();

    void LateUpdate()
    {
        finalScoreText.text = GameElementController.savedScore + "";
        saveHighestScore();
        GetPlayerName();
        _playersScore.playerName = savedName;
        _playersScore.playerScore = GameElementController.savedScore;
    }

    public void saveHighestScore()
    {
        if (PlayerPrefs.HasKey("highestScore"))
        {
            if (
                PlayerPrefs.GetInt("highestScore") <
                GameElementController.savedScore
            )
            {
                savedHighScore = GameElementController.savedScore;
                PlayerPrefs.SetInt("highestScore", savedHighScore);
            }
        }
        else
        {
            savedHighScore = GameElementController.savedScore;
            PlayerPrefs.SetInt("highestScore", savedHighScore);
        }
        highestScoreText.text = PlayerPrefs.GetInt("highestScore").ToString();
    }

    public void GetPlayerName()
    {
        savedName = nameText.text;
    }

    public void SaveIntoJson()
    {
        List<playersScore> listAllScore;
        if (File.Exists(Application.dataPath + "/PlayersScores.json"))
            listAllScore = LoadFromJson();
        else
            listAllScore = new List<playersScore>();
        Boolean delete = false;
        Boolean find = false;
        foreach (playersScore pl in listAllScore)
        {
            if (_playersScore.playerName == pl.playerName)
            {
                //Debug.Log("on a trouver ce player");
                find = true;
                if (_playersScore.playerScore > pl.playerScore)
                {
                    listAllScore.Remove (pl);
                    delete = true;
                    break;
                }
            }
        }
        if (delete)
        {
            listAllScore.Add (_playersScore);
            File.Delete(Application.dataPath + "/PlayersScores.json");
            foreach (playersScore pl in listAllScore)
            {
                string scorePlayer = JsonUtility.ToJson(pl);
                File
                    .AppendAllText(Application.dataPath + "/PlayersScores.json",
                    scorePlayer);
            }
        }
        else
        {
            if (!find)
            {
                Debug.Log("iciiiii");
                string scorePlayer = JsonUtility.ToJson(_playersScore);
                File
                    .AppendAllText(Application.dataPath + "/PlayersScores.json",
                    scorePlayer);
            }
        }
    }

    public static List<playersScore> LoadFromJson()
    {
        string jsonString =
            File.ReadAllText(Application.dataPath + "/PlayersScores.json");
        List<String> jsonListstring = jsonString.Split('}').ToList();
        List<playersScore> listAllScore = new List<playersScore>();
        for (int i = 0; i < jsonListstring.Count - 1; i++)
        {
            jsonListstring[i] = jsonListstring[i] + "}";
            Debug.Log(jsonListstring[i]);
            listAllScore
                .Add(JsonUtility.FromJson<playersScore>(jsonListstring[i]));
        }
        return listAllScore;
    }
}
