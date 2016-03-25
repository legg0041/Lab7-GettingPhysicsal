using UnityEngine;
using System.Collections;

public class Tooth : MonoBehaviour {

    public InfectedTeeth skull;
    public Infection infection;
    private bool _isInfected = false;
    void Update()
    {
        if(_isInfected == false)
        {
            infection = GetComponent<Infection>();
            if(infection != null)
            {
                _isInfected = true;
            }
        }
        
    }

	public void OnCollisionEnter(Collision collision)
    {
        GameObject otherGO = collision.collider.gameObject;
        if(otherGO.tag == "Sweets")
        {

            if(_isInfected == true)
            {
                if (infection.isHurting == true)
                {
                    infection.isHurting = false;
                }
            }
            else
            {
                skull.MakeMistake();
            }
            

            //make movable
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.isKinematic = false;
            rigidbody.useGravity = true;

            //propgate velocity
            Rigidbody otherGORigidBody = otherGO.GetComponent<Rigidbody>();
            rigidbody.velocity = otherGORigidBody.velocity;
            rigidbody.angularVelocity = otherGORigidBody.angularVelocity;

            //notify teeth that we've been removed
            skull.RemoveTooth(gameObject);

            Destroy(gameObject, 2.0f);
        }
    }
}
