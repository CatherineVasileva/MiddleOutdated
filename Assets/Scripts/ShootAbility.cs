using UnityEngine;

public class ShootAbility : MonoBehaviour, IAbility
{
    [SerializeField] GameObject bullet;

    [SerializeField] float _shootDelay;
    private float _shootTime = float.MinValue;

    public void Execute()
    {
        if (Time.time < _shootTime + _shootDelay) return;
        _shootTime = Time.time;

        if (bullet != null)
        {
            var currentBullet = Instantiate(bullet, transform.position, transform.rotation);
        }
        else
            Debug.LogError("Bullet field is empty");
    }
}
