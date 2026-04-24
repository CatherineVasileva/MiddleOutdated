using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using UnityEngine;

public class Button : MonoBehaviour
{
    public async void  LoadFromCloud()
    {
        try
        {
            await GoogleDriveTools.LoadFileFromCloudByName("Stats.json");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Ошибка загрузки: {e.Message}");
        }
    }

    public void LoadLocal()
    {
        GoogleDriveTools.LoadLocalFile<PlayerStatistics>("Stats.json");
    }
}
