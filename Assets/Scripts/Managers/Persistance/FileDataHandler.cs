using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;

public class FileDataHandler<T> where T : class
{
    private string fileDirPath = "";

    private string fileName = "";

    public FileDataHandler(string fileDirPath,string fileName)
    {
        this.fileDirPath = fileDirPath;
        this.fileName = fileName;
    }


    public T Load(){
        string  fullPath = Path.Combine(fileDirPath,fileName);
        T loadedData = null;
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
                loadedData = JsonUtility.FromJson<T>(dataToLoad);
            }   
            catch (Exception e)
            {
                Debug.LogError("Error ocurred when trying to load data from: "+fullPath+"\n"+e);
            }
        }
        return loadedData;
    }

    public void Save(T data){
        string  fullPath = Path.Combine(fileDirPath,fileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(data,true);

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
