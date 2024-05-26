using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;

public class FileDataHandler
{
    private string fileDirPath = "";

    private string fileName = "";

    public FileDataHandler(string fileDirPath,string fileName)
    {
        this.fileDirPath = fileDirPath;
        this.fileName = fileName;
    }


    public GameData Load(){
        string  fullPath = Path.Combine(fileDirPath,fileName);
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using(FileStream stream = new FileStream(fullPath,FileMode.Open)){
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();                    
                    }
                }
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }   
            catch (Exception e)
            {
                Debug.LogError("Error ocurred when trying to load data from: "+fullPath+"\n"+e);
            }
        }
        return loadedData;
    }

    public void Save(GameData data){
        string  fullPath = Path.Combine(fileDirPath,fileName);
        Debug.Log(fullPath);
        try
        {
            Debug.Log(data);
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(data,true);

            Debug.Log("Data to Store "+dataToStore);

            using(FileStream stream = new FileStream(fullPath,FileMode.Create)){
                using(StreamWriter writer = new StreamWriter(stream)){
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
             Debug.LogError("Erroc ocurred when trying to save on: "+fullPath+"\n"+e);
        }

    }
}
