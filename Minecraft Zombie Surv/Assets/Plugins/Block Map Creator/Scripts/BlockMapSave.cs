using NaughtyAttributes;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Services.CloudSave;
using UnityEngine;

public class BlockMapSave : MonoBehaviour
{
    [SerializeField] private BlockMapManager manager;

    [Space]
    [SerializeField] private string savePath = "Asstes/Block Map Creator/Saved Maps";
    [SerializeField] private string mapName = "map_1";

    private string filePath => savePath + "/" + mapName + ".json";

    #region Save Load

    [Button]
    public void SaveMap()
    {
        if (string.IsNullOrEmpty(savePath))
        {
            Debug.LogError("Путь для сохранения файла не указан.");
            return;
        }

        string directoryPath = Path.GetDirectoryName(savePath);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        MapData saveData = new MapData(manager.Blocks);
        string json = JsonConvert.SerializeObject(saveData);

        File.WriteAllText(filePath, json);
        Debug.Log($"Карта {mapName} успешно сохранена в виде JSON по пути: {savePath}");
    }

    [Button]
    public void LoadMap()
    {
        if (!File.Exists(filePath))
        {
            Debug.LogError("Файл не найден: " + filePath);
            return;
        }

        string json = File.ReadAllText(filePath);

        MapData loadedData = JsonConvert.DeserializeObject<MapData>(json);
        manager.Blocks = loadedData.Blocks;
    }

    #endregion
}

[Serializable]
public class MapData
{
    public int[,,] Blocks;

    public MapData(int[,,] blocks)
    {
        Blocks = blocks;
    }
}
