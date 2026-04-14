using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] float health;

    public float Health
    {
        get => health;
        private set => health = value;
    }

    public void AddHealth(float amount)
    {
        Health += amount;
    }
}
