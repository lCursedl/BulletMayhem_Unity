using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Scriptable object to hold general data for th weapons
[CreateAssetMenu(fileName = "Weapon", menuName = "Gun/Weapon")]
public class WeaponData : ScriptableObject
{
    [Header("Info")]
    public string Name;
    [Header("Shoot parameters")]
    public float Damage;
    public float MaxRange;
    public float FireRate;
    public float ReloadTime;
    [Header("Ammo parameters")]
    public int Magazine;
    public int MaxMagazine;
    [HideInInspector]
    public bool m_reloading = false;
}
