using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    // Lists to store keys and values during serialization
    [SerializeField] private List<TKey> keys = new List<TKey>();
    [SerializeField] private List<TValue> values = new List<TValue>();

    // Method called before serialization
    public void OnBeforeSerialize()
    {
        // Clear the lists
        keys.Clear();
        values.Clear();
        // Iterate over each key-value pair in the dictionary
        foreach (KeyValuePair<TKey, TValue> pair in this)
        {
            // Add keys and values to their respective lists
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }

    // Method called after deserialization
    public void OnAfterDeserialize()
    {
        // Clear the dictionary
        this.Clear();
        // Check if the count of keys matches the count of values
        if (keys.Count != values.Count)
        {
            Debug.LogError("Tried to deserialize a SerializableDictionary, but the amount of keys doesn't match: " + 
                "Keys: " + keys.Count + " Values: " + values.Count);
        }

        // Iterate over the lists of keys and values
        for (int i = 0; i < keys.Count; i++)
        {
            // Add key-value pairs to the dictionary
            this.Add(keys[i], values[i]);
        }
    }
}
