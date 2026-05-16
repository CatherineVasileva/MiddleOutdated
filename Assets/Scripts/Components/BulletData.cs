using UnityEngine;

public class BulletData : MonoBehaviour, IBounceAbility
{
    [SerializeField] float Speed;
    [SerializeField] float TimeToDestroy;
    private Rigidbody rb;
    public bool IsAbleToBounce { get; set; } = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * Speed, ForceMode.Impulse);
    }

    public void Execute()
    {
        Destroy(gameObject);
    }

    //public void ReflectDirection(Vector3 chosenDirection)
    //{
    //    var direction = Vector3.Reflect(transform.forward.normalized, chosenDirection.normalized);
    //    transform.forward = direction;
    //    Debug.Log("ReflectDirection");
    //}

    public void Update()
    {
        TimeToDestroy -= Time.deltaTime;
        if(TimeToDestroy <= 0 )
        {
            Destroy(gameObject);
        }
    }

    //private void FixedUpdate()
    //{
    //    transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    //}
}

