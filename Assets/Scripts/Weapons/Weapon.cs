using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

//Script that holds methods and behaviour for player usable weapons
public class Weapon : MonoBehaviour
{
    //Variable for scriptable object data
    [SerializeField]
    private WeaponData Data;
    //Time since the last shot fired
    private float m_timeLastShot = 0f;
    //Player's camera reference
    [SerializeField]
    private Transform Camera;
    //Weapon's model muzzle reference
    [SerializeField]
    private Transform Muzzle;
    //Reference for bullet trail asset
    [SerializeField]
    private TrailRenderer BulletTrail;
    //Reference for particles used when shooting
    [SerializeField]
    private ParticleSystem MuzzleFlash;
    //Vector to determine the amount of spread the gun has
    public Vector3 BulletSpread;
    //Flag to determine if gun will have spread applied or not
    public bool SpreadBullets;

    //Obtain values and variables necessary
    void Start() {
        Data.Magazine = Data.MaxMagazine;
        PlayerShoot.m_shootAction += Shoot;
        PlayerShoot.m_reloadAction += StartReload;
    }

    //Update the time since last shot
    void Update() {
        m_timeLastShot += Time.deltaTime;
    }

    //Calculate if gun can fire if not reloading and the time since the last shot is valid for the RMP
    private bool CanShoot() => !Data.m_reloading && m_timeLastShot > 1f / (Data.FireRate / 60f);

    //Main method for shooting the gun, only supports weapons wich fire one bullet at a time (ARs, SMGs, Handguns, Snipers)
    public void Shoot() {
        if (Data.Magazine > 0) {
            if (CanShoot()) {
                MuzzleFlash.Play();
                TrailRenderer trail = Instantiate(BulletTrail, Muzzle.position, Quaternion.identity);
                Vector3 direction = RandomDirection();
                StartCoroutine(SpawnTrail(trail, direction));
                if (Physics.Raycast(Camera.position, direction, out RaycastHit hitInfo, Data.MaxRange))
                {
                    Debug.Log(hitInfo.transform.name);
                }

                Data.Magazine--;
                m_timeLastShot = 0;
            }
        }
        else {
            StartReload();
        }
    }

    //Method to start a coroutine to reload the weapon
    public void StartReload() {
        if (!Data.m_reloading)
        {
            StartCoroutine(Reload());
        }
    }

    //Coroutine for reloading
    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(Data.ReloadTime);
        Data.Magazine = Data.MaxMagazine;
    }

    //Coroutine for trail spawning
    private IEnumerator SpawnTrail(TrailRenderer trail, Vector3 finishPoint)
    {
        float time = 0;
        Vector3 startPos = trail.transform.position;
        Vector3 endPos = Camera.transform.position + finishPoint * Data.MaxRange;
        while (time < 1) {
            trail.transform.position = Vector3.Lerp(startPos, endPos, time);
            time += Time.deltaTime / trail.time;

            yield return null;
        }
        Destroy(trail.gameObject, trail.time);
    }

    //Method to assign spread if enabled
    private Vector3 RandomDirection()
    {
        Vector3 v3 = Camera.forward;
        if (SpreadBullets)
        {
            v3 += new Vector3(
                Random.Range(-BulletSpread.x, BulletSpread.x),
                Random.Range(-BulletSpread.y, BulletSpread.y),
                Random.Range(-BulletSpread.z, BulletSpread.z));
            v3.Normalize();
        }
        return v3;
    }
}
