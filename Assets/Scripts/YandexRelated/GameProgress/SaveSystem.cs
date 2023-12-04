using Agava.YandexGames;
using System;
using UnityEngine;

public static class SaveSystem
{
    private const int RowCount = 8;
    private const int ColumnCount = 2;
    private const int SceneNameColumn = 0;
    private const int StatusColumn = 1;

    private static readonly string[,] _levels = new string[RowCount, ColumnCount];

    private static SaveObject _saveObject;

    public static Action<string> LevelAccomplished = OnLevelAccomplished;

    private static bool TryFindScene(string sceneName, out int sceneIndex) 
    {
        sceneIndex = 0;
        
        for (int i = 0; i < RowCount; i++)
        {
            if (_levels[i, SceneNameColumn] == sceneName)
            {
                sceneIndex = i;
                return true;
            }
        }

        return false;
    }

    private static void OnLevelAccomplished(string accomplishedSceneName)
    {
        if (TryFindScene(accomplishedSceneName, out int accomplishedSceneIndex)) 
        {
            if (accomplishedSceneIndex + 1 > RowCount)
                return;
            
            _levels[accomplishedSceneIndex + 1, StatusColumn] = "true";
            _saveObject.GiveAccess(_levels[accomplishedSceneIndex + 1, SceneNameColumn]);
            Save();
        } 
    }

    private static void Save() 
    {
        string jsonString = JsonUtility.ToJson(_saveObject);
        PlayerAccount.SetCloudSaveData(jsonString, OnSuccessCloudSave, OnFailedCloudSave);
    }

    private static void OnSuccessDataReceipt(string jsonString)
    {
        if (jsonString == "{}")
            _saveObject = new();
        else
            _saveObject = JsonUtility.FromJson<SaveObject>(jsonString);
    }

    private static void OnFailedDataReceipt(string message)
    {
        Debug.Log(message);
        Debug.Log("Cloud save hasn't been retreived");

        _saveObject = new();
    }

    private static void OnSuccessCloudSave() =>
        Debug.Log("Cloud save is successful");

    private static void OnFailedCloudSave(string message)
    {
        Debug.Log(message);
        Debug.Log("Cloud save has failed");
    }

    public static bool VerifyAccesstoLevel(string sceneName)
    {
        if (TryFindScene(sceneName, out int sceneIndex))
            return Convert.ToBoolean(_levels[sceneIndex, StatusColumn]);

        return false;
    }

    public static void Load() 
    {       
        PlayerAccount.GetCloudSaveData(OnSuccessDataReceipt, OnFailedDataReceipt);

        _levels[0, SceneNameColumn] = SceneNames.Level1;
        _levels[1, SceneNameColumn] = SceneNames.Level2;
        _levels[2, SceneNameColumn] = SceneNames.Level3;
        _levels[3, SceneNameColumn] = SceneNames.Level4;
        _levels[4, SceneNameColumn] = SceneNames.Level5;
        _levels[5, SceneNameColumn] = SceneNames.Level6;
        _levels[6, SceneNameColumn] = SceneNames.Level7;
        _levels[7, SceneNameColumn] = SceneNames.CustomLevel;

        _levels[0, StatusColumn] = _saveObject.Level1.ToString();
        _levels[1, StatusColumn] = _saveObject.Level2.ToString();
        _levels[2, StatusColumn] = _saveObject.Level3.ToString();
        _levels[3, StatusColumn] = _saveObject.Level4.ToString();
        _levels[4, StatusColumn] = _saveObject.Level5.ToString();
        _levels[5, StatusColumn] = _saveObject.Level6.ToString();
        _levels[6, StatusColumn] = _saveObject.Level7.ToString();
        _levels[7, StatusColumn] = _saveObject.CustomLevel.ToString();
    }   
}