using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour {

    public void Posicionar(int id)
    {
        Transform posicionesCamara = GameObject.Find("TransformJ" + id).transform;
        transform.position = posicionesCamara.position;
        transform.rotation = posicionesCamara.rotation;
    }
}
