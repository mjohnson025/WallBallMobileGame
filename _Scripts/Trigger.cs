using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Ball"){
            Invoke("RemovePlatform", .5f);
            
        }
    }

    void RemovePlatform(){
        GetComponentInParent<Rigidbody>().useGravity = true;
        GetComponentInParent<Rigidbody>().isKinematic = false;
        Debug.Log("Removed Platform");
        Destroy(transform.parent.gameObject, 2f);
    }
}
