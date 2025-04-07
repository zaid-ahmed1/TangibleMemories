using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Postgres : MonoBehaviour
{
    // Base URL of your PostgREST API (replace with your actual EC2 public IP and port)
    private string baseUrl = "http://3.15.57.55:3000/";  // Replace with your EC2 IP and port

    // The table name you want to query
    private string tableName = "memories";     // Replace with your actual table name

    private List<Memory> memories;  // Store the list of memories locally

    void Start()
    {
        // Fetch data from the API
        StartCoroutine(GetDataFromPostgREST());
    }
    
    // Coroutine to make the GET request
    IEnumerator GetDataFromPostgREST()
    {
        // Create the full URL for the API request
        string url = baseUrl + tableName;

        // Send the GET request using UnityWebRequest
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Wait for the request to complete
            yield return webRequest.SendWebRequest();

            // Check for network errors or HTTP errors
            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                // Success! Parse the response
                Debug.Log("Response: " + webRequest.downloadHandler.text);

                // Process the JSON response
                ProcessResponse(webRequest.downloadHandler.text);
            }
        }
    }

// Process the response and populate the memories list
    void ProcessResponse(string jsonResponse)
    {
        try
        {
            // Deserialize the JSON array directly into a List<Memory>
            MemoryList memoryList = JsonUtility.FromJson<MemoryList>("{\"memories\":" + jsonResponse + "}");
            memories = memoryList.memories;

            // Debugging: Check if the list is populated
            if (memories != null && memories.Count > 0)
            {
                Debug.Log($"Loaded {memories.Count} memories.");
                foreach (var memory in memories)
                {
                    Debug.Log($"ID: {memory.id}, QR Code: {memory.qr_code}, AWS FileKey: {memory.filekey}");
                }
            }
            else
            {
                Debug.LogWarning("Deserialized memories list is null or empty.");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error parsing JSON: " + ex.Message);
        }
    }

    // Method to find a memory by its QR code
    public Memory FindMemoryByQRCode(string qrCode)
    {
        if (memories == null || memories.Count == 0)
        {
            Debug.LogWarning("Memories not loaded yet.");
            return null;
        }

        foreach (var memory in memories)
        {
            if (memory.qr_code == qrCode)
            {
                return memory;
            }
        }

        Debug.LogWarning("No memory found with QR code: " + qrCode);
        return null;
    }
}

[System.Serializable]
public class Memory
{
    public int id;
    public string title;
    public string created_at; // Store as string for manual parsing
    public string updated_at; // Store as string for manual parsing
    public int user_id;
    public string qr_code;
    public string filekey;

    // Add properties to parse DateTime manually
    public DateTime CreatedAt => DateTime.Parse(created_at);
    public DateTime UpdatedAt => DateTime.Parse(updated_at);
}

[System.Serializable]
public class MemoryList
{
    public List<Memory> memories;
}