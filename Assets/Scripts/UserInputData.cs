using UnityEngine;

public class UserInputData : MonoBehaviour
{
    public MonoBehaviour _attackScript;
    public float Speed;
    public float JerkSpeed;
    public float JerkDuration;
    public float TimeToJerkAgain;
   // public float Health;

   
}

public struct InputData 
    {
        public Vector2 move;
        public float shoot;
        public float jerk;
    }

public struct MoveData 
{
    public float speed;
}

public struct JerkData 
{
    public float jerkSpeed;
    public float jerkDuration;
    public float timerJerkDuration;
    public bool canJerk;
    public float timeToJerkAgain;
    public float timerBetweenJerks;
}

public struct PlayerHealth 
{
    public float health;
}
