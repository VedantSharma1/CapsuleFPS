using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    private GameManager gameManager;
    public Camera cam;
    private float xRotation = 0f;

    public float xSenstivity = 30f;
    public float ySenstivity = 30f;

    public void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    public void ProcessLook(Vector2 input)
    {
        if (gameManager.isGameActive)
        {
            float mouseX = input.x;
            float mouseY = input.y;

            //calculate camera rotation
            xRotation -= (mouseY * Time.deltaTime) * ySenstivity;
            xRotation = Mathf.Clamp(xRotation, -80f, 80f);
            //apply to our camera rotation
            cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
            //rotate the player to look left and right
            transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSenstivity);
        }
        
    }
}
