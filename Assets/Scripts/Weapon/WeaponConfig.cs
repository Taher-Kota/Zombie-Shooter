using UnityEngine;

public enum TypeControl
{
    Click,
    Hold
}

public enum TypeWeapon
{
    Meele,
    OneHanded,
    TwoHanded
}

[System.Serializable]
public struct DefaultConfig
{
    public TypeControl typeControl;
    public TypeWeapon typeWeapon;
    [Range(0f, 100f)]
    public int Damage;

    [Range(0f,100f)]
    public int CriticalDamage;

    [Range(0.01f, 1f)]
    public float FireRate;

    [Range(0.01f, 1f)]
    public int CriticalFireRate;
}
