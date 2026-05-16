using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerData", menuName = "Data/Player")]
public class PlayerData : ScriptableObject
{
    public string PlayerName;
    public int HP;
    public int DMG;
}
