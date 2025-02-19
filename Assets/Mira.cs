using UnityEngine;

public class Mira : MonoBehaviour
{
    public Camera secondCamera;
    public float distanceFromCamera = 50f; // Distancia fija donde estará la mira
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        MoveCrosshair();
    }

    private void MoveCrosshair()
    {
        Ray ray = secondCamera.ScreenPointToRay(Input.mousePosition);
        transform.position = ray.GetPoint(distanceFromCamera); // Calcula un punto en la dirección del rayo
    }
}
