using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] Transform crosshair;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] Camera secondCamera;
    public float distanceFromCamera = 50f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        AimWeapon();
    }

    void AimWeapon()
    {
        Ray ray = secondCamera.ScreenPointToRay(Input.mousePosition);      
        Vector3 direction = ray.GetPoint(distanceFromCamera);

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        targetRotation *= Quaternion.Euler(90,0,0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
