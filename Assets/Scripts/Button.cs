using AB_GoogleSheetImporter.Editor;
using UnityEngine;
#if UNITY_EDITOR 
using UnityEditor;
#endif

public class Button : MonoBehaviour
{
    private string _sheetURL = "https://docs.google.com/spreadsheets/d/1BNeOX1DThC6F4rhAg-2mH22ozioGpgM_DFNcGNXvth0/edit?usp=drive_link";
    
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

    public async void LoadGoogleSheet()
    {
        var csv = await GSImporter.DownloadCsvAsync(_sheetURL);

        string[] lines = csv.Split('\n');
        for (int i =1; i< lines.Length;i++)
        {
            string line = lines[i].Trim();
            if(string.IsNullOrEmpty(line)) continue;

            string [] cells = line.Split(',');
            CreateOrUpdateAsset(cells[0], int.Parse(cells[1]), int.Parse(cells[2]));
        }
    }
#if UNITY_EDITOR
    private void CreateOrUpdateAsset(string name, int hp, int dmg)
    {
        string path = $"Assets/PlayersData/{name}.asset";

        PlayerData asset = AssetDatabase.LoadAssetAtPath<PlayerData>(path);

        if(asset == null)
        {
            asset = ScriptableObject.CreateInstance<PlayerData>();
            AssetDatabase.CreateAsset(asset, path);
        }

        asset.PlayerName = name;
        asset.HP = hp;
        asset.DMG = dmg;

        EditorUtility.SetDirty(asset);
        AssetDatabase.SaveAssets();
    }
#endif
}
