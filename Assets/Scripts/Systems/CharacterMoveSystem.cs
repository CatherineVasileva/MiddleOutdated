using UnityEngine;

public class CharacterMoveSystem : MonoBehaviour, IMoveable
{
    [SerializeField] float _speed;


    public void Move(Vector2 direction)
    {
        var pos = transform.position;
        var newDirection = new Vector3(direction.x, 0, direction.y);
        pos += newDirection * _speed * Time.deltaTime;
        transform.position = pos;
           
        if(newDirection != Vector3.zero)
            {
                transform.forward = newDirection;
            }
    }
}
