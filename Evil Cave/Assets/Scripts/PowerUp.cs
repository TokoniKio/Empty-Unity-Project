using UnityEngine;

public enum PowerUpType

{
    None, 
    Health, 
    Strength
}

public class PowerUp : MonoBehaviour
{
    public PowerUpType powerUpType;
}
