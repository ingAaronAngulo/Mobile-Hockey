using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {
    public delegate void statusAccion();
    public static event statusAccion OnAnotacionJ1;
    public static event statusAccion OnAnotacionJ2;
    public static event statusAccion OnFichaFuera;

    private static EventManager instance;

    public void AnotacionJ1()
    {
        if (OnAnotacionJ1 != null)
            OnAnotacionJ1();
    }

    public void AnotacionJ2()
    {
        if (OnAnotacionJ2 != null)
            OnAnotacionJ2();
    }

    public void FichaFuera()
    {
        if (OnFichaFuera != null)
            OnFichaFuera();
    }

    /*
     * void OnEnable()
    {
        EventManager.OnToqueNodo += Contacto;
        EventManager.OnDisminuirVida += Renacer;
    }

    void OnDisable()
    {
        EventManager.OnToqueNodo -= Contacto;
        EventManager.OnDisminuirVida -= Renacer;
    }
     */

    private EventManager()
    {

    }

    public static EventManager Instance
    {
        get
        {
            if (instance == null)
                instance = new EventManager();
            return instance;
        }
    }
}
