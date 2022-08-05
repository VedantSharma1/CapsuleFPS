using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    
    [HideInInspector]
    public PlayerInput.OnFootActions onFoot;
    [HideInInspector]
    public PlayerInput playerInput;

    
    private PlayerController playerController;
    private PlayerLook look;
    private GunController gunController;
    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        playerController = GetComponent<PlayerController>();
        onFoot.Jump.performed += ctx => playerController.Jump();
        /* basically anytime our onFoot.Jump is performed we are using a callback 
         * syntax called ctx to call our jump fxn in other script , there is also .started , .canceled depending on your 
         * functionality you going for we change the callback syntax you are listening for*/
        //onFoot.Sprint.performed += ctx => playerController.Sprint();

        look = GetComponent<PlayerLook>();
        gunController = GameObject.Find("ak47").GetComponent<GunController>();
        
    }

    private void FixedUpdate()
    {   
        //tel the playercontroller to move using the value from our movement action
        playerController.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
        
    }

    private void LateUpdate()
    {

        gunController.WeaponSway(onFoot.Look.ReadValue<Vector2>());

    }
    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
