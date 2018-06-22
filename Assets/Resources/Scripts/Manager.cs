using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Manager : NetworkBehaviour
{
    public AudioSource musica;
    public Vector3 posInicialFicha;
    public GameObject goFicha;
    public GameObject fichaActual;
    public Text txtPuntuacionJ1;
    public Text txtPuntuacionJ2;
    
    [SyncVar]public int puntuacionJ1 = 0;
    [SyncVar]public int puntuacionJ2 = 0;

    void OnEnable()
    {
        EventManager.OnAnotacionJ1 += AnotarJ1;
        EventManager.OnAnotacionJ2 += AnotarJ2;
        EventManager.OnFichaFuera += ReposicionarFicha;
    }

    void OnDisable()
    {
        EventManager.OnAnotacionJ1 -= AnotarJ1;
        EventManager.OnAnotacionJ2 -= AnotarJ2;
        EventManager.OnFichaFuera -= ReposicionarFicha;
    }

    private void Awake()
    {
        posInicialFicha = new Vector3(75, 13.5f, 0);
        txtPuntuacionJ1 = GameObject.Find("TxtPuntuacionJ1").GetComponent<Text>();
        txtPuntuacionJ2 = GameObject.Find("TxtPuntuacionJ2").GetComponent<Text>();
        goFicha = Resources.Load("Prefabs/Ficha") as GameObject;
        musica = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start () {
        Anotacion();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            musica.Play();
        }
        if (Input.GetMouseButtonDown(0))
        {
            fichaActual.transform.position = posInicialFicha; ;
        }
    }

    public void Reiniciar()
    {
        puntuacionJ1 = 0;
        puntuacionJ2 = 0;
        txtPuntuacionJ1.text = "0";
        txtPuntuacionJ2.text = "0";
        ReposicionarFicha();
    }

    private void FixedUpdate()
    {
        txtPuntuacionJ1.text = "" + puntuacionJ1;
        txtPuntuacionJ2.text = "" + puntuacionJ2;
    }

    public void AnotarJ1()
    {
        if (isServer)
        {
            puntuacionJ1++;
            Anotacion();
        }
    }

    public void AnotarJ2()
    {
        if (isServer)
        {
            puntuacionJ2++;
            Anotacion();
        }
    }

    public void ReposicionarFicha()
    {
        fichaActual.transform.position = posInicialFicha;
    }

    public void Anotacion()
    {
        txtPuntuacionJ1.text = "" + puntuacionJ1;
        txtPuntuacionJ2.text = "" + puntuacionJ2;
        if (isServer)
        {
            if(fichaActual != null)
                NetworkServer.Destroy(fichaActual);
            fichaActual = Instantiate(goFicha, posInicialFicha, Quaternion.identity);
            NetworkServer.Spawn(fichaActual);
        }
    }
}
