using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Ficha : NetworkBehaviour {
    
    public Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        if (rb.velocity.x <= 0.1f && rb.velocity.z <= 0.1f)
            if(isServer)
                rb.AddForce(new Vector3(Random.Range(-75, 75), 0, Random.Range(-75, 75)));
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Porteria")
        {
            if (other.name == "TriggerPorteria1")
                EventManager.Instance.AnotacionJ2();
            if (other.name == "TriggerPorteria2")
                EventManager.Instance.AnotacionJ1();
        }

        if(other.tag == "Limites")
        {
            EventManager.Instance.FichaFuera();
        }
    }
}
