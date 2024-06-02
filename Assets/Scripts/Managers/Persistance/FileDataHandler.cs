using System;
using System.IO;
using UnityEngine;

public class FileDataHandler<T> where T : class
{
    // File directory path
    private string fileDirPath = "";

    // File name
    private string fileName = "";

    // Constructor
    public FileDataHandler(string fileDirPath, string fileName)
    {
        this.fileDirPath = fileDirPath;
        this.fileName = fileName;
    }

    // Load data from file
    public T Load()
    {
        string fullPath = Path.Combine(fileDirPath, fileName);
        T loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                // Read data from file
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                // Deserialize JSON data
                loadedData = JsonUtility.FromJson<T>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Error occurred when trying to load data from: " + fullPath + "\n" + e);
            }
        }
        return loadedData;
    }

    // Save data to file
    public void Save(T data)
    {
        string fullPath = Path.Combine(fileDirPath, fileName);
        try
        {
            // Create directory if it doesn't exist
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            // Convert data to JSON format
            string dataToStore = JsonUtility.ToJson(data, true);

            // Write data to file
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error occurred when trying to save on: " + fullPath + "\n" + e);
        }
    }

    // Delete file
    public void DeleteFile()
    {
        string fullPath = Path.Combine(fileDirPath, fileName);
        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }
    }
}
