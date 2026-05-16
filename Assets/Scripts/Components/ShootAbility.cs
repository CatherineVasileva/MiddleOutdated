using System;
using System.IO;
using UnityEngine;

public class ShootAbility : MonoBehaviour, IAbility, IPerkHandler
{
    [SerializeField] GameObject bullet;
    [SerializeField] float _shootDelay;
    [SerializeField] float BulBouncePerkDuration;
    //private PlayerStatistics stats; // модуль 4

    [NonSerialized] public float BulBouncePerkStart = float.MinValue;
    private float _shootTime = float.MinValue;

    private void Start() // модуль 4
    {
       // stats = GoogleDriveTools.LoadLocalFile<PlayerStatistics>("Stats.json");
        
    }

    public void Execute()
    {
        if (Time.time < _shootTime + _shootDelay) return;
        _shootTime = Time.time;

        if (bullet != null)
        {
           var currentBullet = Instantiate(bullet, transform.position, transform.rotation);
            //stats.ShotCounts++; 
            //WriteStatistics(); 

                if(Time.time < BulBouncePerkStart + BulBouncePerkDuration)
                {
                if(currentBullet.TryGetComponent<IBounceAbility>(out var bounceAbility))
                    bounceAbility.IsAbleToBounce = true;
                }
        }
        else
            Debug.LogError("Bullet field is empty");
    }

    //private void WriteStatistics() 
    //{
    //    string path = Path.Combine(Application.persistentDataPath, "Stats.json"); 
    //    var jsonString = JsonUtility.ToJson(stats); 
    //    File.WriteAllText(path, jsonString);
    //    Debug.Log(jsonString);
    //}

    public void ActivatePerk()
    {
        BulBouncePerkStart = Time.time;
    }
    
}
