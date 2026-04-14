using System;
using UnityEngine;

public class ShootAbility : MonoBehaviour, IAbility, IPerkHandler
{
    [SerializeField] GameObject bullet;
    [SerializeField] float _shootDelay;
    [SerializeField] float BulBouncePerkDuration;
    public PlayerStatistics stats; // модуль 4

    [NonSerialized] public float BulBouncePerkStart = float.MinValue;
    private float _shootTime = float.MinValue;

    private void Start() // модуль 4
    {
        var jsonString = PlayerPrefs.GetString("Stats");
        if (!jsonString.Equals(String.Empty, StringComparison.Ordinal))
        {
            stats = JsonUtility.FromJson<PlayerStatistics>(jsonString);
        }
        else
        {
            stats = new PlayerStatistics();
        }
    }

    public void Execute()
    {
        if (Time.time < _shootTime + _shootDelay) return;
        _shootTime = Time.time;

        if (bullet != null)
        {
           var currentBullet = Instantiate(bullet, transform.position, transform.rotation);
            stats.ShotCounts++; // модуль 4
            WriteStatistics(); // модуль 4

                if(Time.time < BulBouncePerkStart + BulBouncePerkDuration)
                {
                if(currentBullet.TryGetComponent<IBounceAbility>(out var bounceAbility))

                    bounceAbility.IsAbleToBounce = true;
                }
        }
        else
            Debug.LogError("Bullet field is empty");
    }
    
    public void ActivatePerk()
    {
        BulBouncePerkStart = Time.time;
    }

    private void WriteStatistics() // модуль 4
    {
        var jsonString = JsonUtility.ToJson(stats);
        Debug.Log(jsonString);
        PlayerPrefs.SetString("Stats", jsonString); 
    }

    
}
