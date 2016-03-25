using UnityEngine;
using System.Collections;

public class ObjectGun : MonoBehaviour
{
    public Camera pickingCamera;
    public GameObject objectToShoot;
    public float impulseAmount;
    public Vector3 spawnOffset;

    // Use this for initialization
    void Start()
    {
        if (objectToShoot == null || objectToShoot.GetComponent<Rigidbody>() == null)
        {
            Debug.Log("You're a terrible person for not having a valid object here. You should feel bad.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) == true)
        {
            Rigidbody rigidbody = objectToShoot.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                GameObject newGO = GameObject.Instantiate(objectToShoot);

                Vector3 positionToSpawnAt = transform.position + (transform.forward) + spawnOffset;
                newGO.transform.position = positionToSpawnAt;

                if (Input.GetMouseButtonDown(0) == true)
                {
                    // Creates a ray that is cast from the mouse's position into the world.
                    Vector3 mousePosition = Input.mousePosition;
                    Ray pickingRay = pickingCamera.ScreenPointToRay(mousePosition);

                    // Use the ray to see if any object collides with it.
                    RaycastHit hit;
                    bool success = Physics.Raycast(pickingRay, out hit);
                    if (success)
                    {
                        Debug.Log("The name of the picked object is: " + hit.collider.gameObject);
                    }

                    Vector3 rayDirection = pickingRay.direction;

                    //no longer used
                    //Vector3 shotDirection = hit.point - transform.position;

                    rigidbody = newGO.GetComponent<Rigidbody>();
                    rigidbody.AddForce(rayDirection.normalized * impulseAmount, ForceMode.Impulse);
                }
            }
        }
    }
}
