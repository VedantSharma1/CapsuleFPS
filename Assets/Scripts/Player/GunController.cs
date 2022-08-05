using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GunController : MonoBehaviour
{
    private GameManager gameManager;
    [Header("Gun Settings")]
    //what different settings he guns have
    public float fireRate = 0.5f;
    public int clipSize = 30;
    public int reservedAmmoCapaccity = 270;

    //Variables that chnage throughout code
    bool _canShoot;
    int _currentAmmoInClip;
    int _ammoReserve;

    //Aiming
    public Vector3 normalLocalPosition;
    public Quaternion normalLocalRotation;
    public Vector3 aimingLocalPosition; //aiming gun postion
    public Quaternion aimingLocalRotation; // aiming gun rotation
    public float aimSmoothing = 10;

    [Header("Mouse Settings")]
    public float weaponSwayAmount = -3.2f;

    //Recoil
    [Header("Recoil Settings")]
    public float recoilValue = 0.5f;

    //sprint
    [Header("Sprint Settings")]
    public Quaternion sprintLocalPostion;
    public float sprintSmoothing = 10;

    [Header("Particle Settings")]
    public ParticleSystem[] particles;
    //public ParticleSystem explosion;
    //public ParticleSystem spark;

    [Header("Projectile Settings")]
    public GameObject projectile;

    public int _damage = 20;
    void Awake()
    {
        
       
    }

    private void Start()
    {   

        _currentAmmoInClip = clipSize;
        _ammoReserve = reservedAmmoCapaccity;
        _canShoot = true;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (gameManager.isGameActive)
        {
            DetermineAim();
            UpdateAmmo();

            //shoot
            if (Input.GetMouseButton(0) && _canShoot && _currentAmmoInClip > 0)
            {
                _canShoot = false;
                _currentAmmoInClip--;
                
                StartCoroutine(ShootGun()); //co routines run on main thread so we can have a timer without freezing the unity engine
            }
            //reload
            else if (Input.GetKeyDown(KeyCode.R) && _currentAmmoInClip < clipSize && _ammoReserve > 0)
            {
                
                int amountNeeded = clipSize - _currentAmmoInClip;
                if (amountNeeded >= _ammoReserve)
                {
                    _currentAmmoInClip += _ammoReserve;
                    _ammoReserve -= amountNeeded;
                }
                else
                {
                    _currentAmmoInClip = clipSize;
                    _ammoReserve -= amountNeeded;
                }
                
            }
        }
       
    }

    private void FixedUpdate()
    {
        //DetermineAim();
    }

    public void UpdateAmmo()
    {
        gameManager.ammoText.text = "Ammo: " + _currentAmmoInClip + "/" + _ammoReserve;
    }

    public void WeaponSway(Vector2 _input)
    {

        Vector2 mouseAxis = new Vector2(_input.x, _input.y);
        //Debug.Log(mouseAxis);
        transform.localPosition += (Vector3)mouseAxis * weaponSwayAmount / 1000 ;
    }
    
    void DetermineAim()
    {
        Vector3 targetPos = normalLocalPosition;
        Quaternion targetRot = normalLocalRotation;
        if (Input.GetMouseButton(1))
        {
            targetPos = aimingLocalPosition;
            targetRot = aimingLocalRotation;
        }

        Vector3 desiredPositon = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime * aimSmoothing);
        Quaternion desiredRot = Quaternion.Slerp(transform.localRotation, targetRot, Time.deltaTime * aimSmoothing);

        transform.localPosition = desiredPositon;
        transform.localRotation = desiredRot;
    }

    void Recoil()
    {
        transform.localPosition += Vector3.back * recoilValue;
    }

    public void WeaponDown()
    {
        Quaternion targetSprintpos = sprintLocalPostion;
        Quaternion desiredSprintPostion = Quaternion.Slerp(transform.localRotation, targetSprintpos, Time.deltaTime * sprintSmoothing);
        transform.localRotation = desiredSprintPostion;
    }
    IEnumerator ShootGun()
    {
        RaycastForEnemy();
        Recoil();
        MuzzleFlash();
        yield return new WaitForSeconds(fireRate);
        _canShoot = true;
    }

    void MuzzleFlash()
    {
        particles[0].Play();
        particles[1].Play();
    }
    
    void RaycastForEnemy()
    {
        //Vector3 point = new Vector3(cam.pixelWidth / 2, cam.pixelHeight / 2, 0);
        //Ray ray = cam.ScreenPointToRay(point);
        RaycastHit hit;
        
        //check if enemy is hit
        if (Physics.Raycast(transform.parent.position, transform.parent.forward, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;
            EnemyTarget Enemy = hitObject.GetComponent<EnemyTarget>();

            if(Enemy != null)
            {
                Enemy.DamageTaken(_damage); //each bullet does 20 damage 
            }
            
            //StartCoroutine(BulletIndicator(hit.point));
            
        }
    }
    /*IEnumerator BulletIndicator(Vector3 pos)
    {
        
        
        var copy = Instantiate(projectile); // cant destroy the original prefab
        copy.transform.position = pos;
        yield return new WaitForSeconds(0.2f);
        Destroy(copy);
    }*/
}
