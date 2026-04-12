using System;
using UnityEngine;

public class ShootAbility : MonoBehaviour, IAbility
{
    [SerializeField] GameObject bullet;
    [SerializeField] float _shootDelay;
    [SerializeField] float BulBouncePerkDuration;

    [NonSerialized] public float BulBouncePerkStart = float.MinValue;
    
    private float _shootTime = float.MinValue;

    public void Execute()
    {
        if (Time.time < _shootTime + _shootDelay) return;
        _shootTime = Time.time;

        if (bullet != null)
        {
           var currentBullet = Instantiate(bullet, transform.position, transform.rotation);

                if(Time.time < BulBouncePerkStart + BulBouncePerkDuration)
                {
                    currentBullet.GetComponent<BulletData>().isAbleToBounce = true;
                }
        }
        else
            Debug.LogError("Bullet field is empty");
    }
}
