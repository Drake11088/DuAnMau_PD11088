using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Playables;

public class Finish : MonoBehaviour
{
    [SerializeField] GameObject informationCanvas;
    [SerializeField] GameObject finishCanvas;

    [SerializeField] GameObject Row;
    
    private storageHelper storageHelper;
    private GameDataPlayed played;
    void Start()
    {
        storageHelper = new storageHelper();
        storageHelper.LoadData();
        played = storageHelper.played;
        Debug.Log(message: "count: " + played.plays.Count);


        played.plays.Sort();
        var plays = played.plays.GetRange(0, Math.Min(5, played.plays.Count));


        for (int i = 0; i < plays.Count; i++)
        {
            var rowInstance = Instantiate(Row, Row.transform.parent);
            rowInstance.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = ( i + 1).ToString();
            rowInstance.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = plays[i].score.ToString();
            rowInstance.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = plays[i].timePlayed;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            finishCanvas.SetActive(true);
        }
    }
}
[Serializable]
public class GameData
{
    public int score;
    public string timePlayed;
}
public class StorageManager
{
    public static bool SaveToFile(string fileName,  string json)
    {
        try
        {
            var fileStream = new FileStream(fileName, FileMode.Create);
            using (var writer = new StreamWriter(fileStream))
            {
                writer.Write(json);
            }
            return true;
        }
        catch (System.Exception e)
        {
            Debug.Log(message:"Error saving file: " + e.Message);
            return false;
        }
    }
    public static string LoadFromFile(string fileName)
    {
        try
        {
            if (File.Exists(fileName))
            {
                var fileStream = new FileStream(fileName, FileMode.Open);
                using (var reader = new StreamReader(fileStream))
                {
                    return reader.ReadToEnd();
                }
            }
            else
            {
                Debug.Log(message: "file not found: " + fileName);
                return null;
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(message: "error loading file:  " + e.Message);
            return null;
        }
    }
}
public class storageHelper
{
    private readonly string filename = "game_data.txt";
    public List<GameData> plays;
    public GameDataPlayed played;

    public void LoadData()
    {
        played = new GameDataPlayed();
        {
            plays = new List<GameData>();
        };
        string datajson = StorageManager.LoadFromFile(filename);
        if (datajson != null)
        {
            played = JsonUtility.FromJson<GameDataPlayed>(datajson);
        }
    }
    public void SaveData()
    {
        string  dataAsjson = JsonUtility.ToJson(played);
        StorageManager.SaveToFile(filename, dataAsjson);
    }
}
[Serializable]
public class GameDataPlayed
{
    public List<GameData> plays;
}