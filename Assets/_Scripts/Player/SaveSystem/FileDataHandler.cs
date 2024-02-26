using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    private string dataDirPath = string.Empty;
    private string dataFileName = string.Empty;

    public FileDataHandler(string _dataDirectionPath, string _dataFileName)
    {
        this.dataDirPath = _dataDirectionPath;
        this.dataFileName = _dataFileName;
    }

    public void Save(GameData _data)
    {
        string fullPath = Path.Combine(this.dataDirPath, this.dataFileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(_data, true);

            using(FileStream stream = new FileStream(fullPath,FileMode.Create))
            {
                using(StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {

            Debug.LogError("Error trying to save data to file: " + fullPath + "\n" + e);
        }
    }

    public GameData Load()
    {
        string fullPath = Path.Combine(this.dataDirPath, this.dataFileName);
        GameData loadData = null;

        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = string.Empty;

                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                loadData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Error on trying to load data from file: " + fullPath + "\n" + e);
            }
        }

        return loadData;
    }
}
