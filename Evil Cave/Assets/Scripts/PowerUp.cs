using UnityEngine;

public enum PowerUpType

{
    None, 
    Health, 
    Strength,
    Speed
}

public class PowerUp : MonoBehaviour
{
    public PowerUpType powerUpType;
}
