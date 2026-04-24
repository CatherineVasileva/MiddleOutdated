
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.Build.CacheServer;
using UnityEngine;
using UnityEngine.Networking;
using UnityGoogleDrive; 

public static class GoogleDriveTools 
{
    private const string API_KEY = "AIzaSyDx15NPc3WgBcY2STLfDfBLxFQi4_8qU_E";
    private const string FOLDER_ID = "1IO29YwBXB3wQqHBOgn1skH4GVTf0qaul";
    

    public static async Task LoadFileFromCloudByName(string fileName)
    {
        string query = $"name = '{fileName}' and '{FOLDER_ID}' in parents and trashed = false";

        string encodedQuery = UnityWebRequest.EscapeURL(query);

        string url = $"https://www.googleapis.com/drive/v3/files?q={encodedQuery}&key={API_KEY}";

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            var operation = request.SendWebRequest();
            while(!operation.isDone) await Task.Yield();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string foundId = ParseIDFromJson(request.downloadHandler.text);
                if (!string.IsNullOrEmpty(foundId))
                {
                    await DownloadFile(foundId, API_KEY, fileName);
                }
            }
            else
            {
                Debug.LogError($"[Cloud Error] Причина: {request.error}");
                Debug.Log($"[Cloud Response]: {request.downloadHandler.text}");
            }
        }
    }
    public static async Task DownloadFile(string foundId, string API_KEY, string fileName)
    {
        string url = $"https://www.googleapis.com/drive/v3/files/{foundId}?alt=media&key={API_KEY}";
        string localPath = Path.Combine(Application.persistentDataPath, fileName);

        using(UnityWebRequest request = UnityWebRequest.Get(url))
        {
            var operation = request.SendWebRequest();
            while(!operation.isDone) await Task.Yield();

            if(request.result == UnityWebRequest.Result.Success)
            {
                File.WriteAllText(localPath, request.downloadHandler.text);
                Debug.Log(request.downloadHandler.text + "скаченная инфа");
                Debug.Log($"[Service] Файл {fileName} скачан и сохранен в persistentDataPath.");
            }
            else
            {
                Debug.LogError($"[Service] Ошибка загрузки: {request.error}");
            }
        }
    }

    public static T LoadLocalFile<T>(string fileName) where T : new()
    {
        string localPath = Path.Combine(Application.persistentDataPath, fileName);

        if (File.Exists(localPath))
        {
            string json = File.ReadAllText(localPath);
            
            Debug.Log($"[Service] Данные {fileName} загружены.");
            Debug.Log(json);
            return JsonUtility.FromJson<T>(json);
        }
        Debug.Log($"[Service] Файл {fileName} не найден, создаем новый объект.");
        return new T();

    }

    private static string ParseIDFromJson(string json)
    {
        GoogleFileList list = JsonUtility.FromJson<GoogleFileList>(json);

        if(list != null && list.files != null && list.files.Count > 0)
        {
            return list.files[0].id;
        }
        return null;
    }

    [System.Serializable]
    public class GoogleFile
    {
        public string id;
        public string name;
    }

    [System.Serializable]
    public class GoogleFileList
    {
        public List<GoogleFile> files;
    }
}
