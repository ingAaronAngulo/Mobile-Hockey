using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Jugador : NetworkBehaviour {

    [SyncVar]
    public Color color;
    [SyncVar]
    public int id;
    public Rigidbody rb;
    public Vector3 posicionInicial;
    
    // Use this for initialization
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start ()
    {
        if (id > 0)
        {
            Material material = Resources.Load("Materials/Jugador" + id) as Material;
            material.color = color;
            GetComponent<MeshRenderer>().material = material;
            GameObject.Find("TxtPuntuacionJ" + id).GetComponent<Text>().color = color;

            Transform spawn = GameObject.Find("Spawn" + id).transform;
            transform.position = spawn.position;
            transform.rotation = spawn.rotation;
            posicionInicial = spawn.position;

            if (!isServer)
            {
                gameObject.layer = 12;
            }
            if (!isLocalPlayer)
            {
                Destroy(this);
                return;
            }

            Input.gyro.enabled = true;
            GameObject.Find("Camara").GetComponent<Camara>().Posicionar(id);
            Destroy(GameObject.Find("Manager"));
            Destroy(GameObject.Find("Puntuaciones"));
            Destroy(GameObject.Find("Controles"));
        }
        else
        {
            GameObject.Find("Camara").GetComponent<Camara>().Posicionar(id);
            Destroy(GameObject.Find("Instrucciones"));
            Destroy(gameObject);
            return;
        }

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            transform.position = posicionInicial;
            rb.velocity = Vector3.zero;
        }
	}

    private void FixedUpdate()
    {
        float multiplicador = 1000;
        Vector3 aceleracion = Input.gyro.userAcceleration;
        
        if(id == 1)
            multiplicador *= -1;

        if (Mathf.Abs(aceleracion.x) > 0.01f)
        {
            rb.AddForce(new Vector3(aceleracion.x * multiplicador, 0, 0));
            Debug.Log(aceleracion);
        }
        if (Mathf.Abs(aceleracion.y) > 0.01f)
        {
            rb.AddForce(new Vector3(0, 0, aceleracion.y * multiplicador));
            Debug.Log(aceleracion);
        }
        if (Mathf.Abs(aceleracion.x) <= 0.025f && Mathf.Abs(aceleracion.y) <= 0.025f)
            rb.velocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Limites")
        {
            transform.position = posicionInicial;
            rb.velocity = Vector3.zero;
        }
    }
}
