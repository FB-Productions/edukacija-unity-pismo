using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    // Attach to and rotate the camera, but refference the Player's rotation for the X axis

    /*public Transform player;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        player.transform.Rotate(Vector3.up * Input.GetAxis("Mouse X")); // We rotate the Player (our parent) for the X, so the camera auto rotates with it

        float mouseY = -Input.GetAxis("Mouse Y");
        mouseY = Mathf.Clamp(mouseY, -45f, 45f); // Limiting the vertical mouse movement
        transform.localRotation = Quaternion.Euler(mouseY, transform.localRotation.y, transform.localRotation.z); // and setting it
    }*/

    public float mouseSensitivity = 100f; // varijabla za brzinu kretanja mi�a
    public Transform playerBody; // referenca na transform na�eg lika
    /*public*/ float xRotation = 0f; // varijabla koja je odgovorna za rotaciju gore dolje

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // dio koda pomo�u kojega maknemo mi� sa ekrana
    }
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        playerBody.Rotate(Vector3.up * mouseX);
        xRotation -= mouseY; // kod odgovoran za rotaciju kamere gore dolje, zna�i oduzimamo vrijednos koju dobijemo
                             // od mi�a kako ga pomi�emo od stvarne x kordinate na transformu
        xRotation = Mathf.Clamp(xRotation, -45f, 45f); // ograni�avamo da mo�emo i�i samo 90 stupnjeva gore i 90
                                                       //stupnjeva dolje
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0); // linija koda koja nam slu�i za postavljanje
        // lokalne rotacije objekta (naj�e��e kamere �to je i u na�em slu�aju)
    }
}
