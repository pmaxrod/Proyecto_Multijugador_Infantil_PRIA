using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GenerarJugadores : MonoBehaviour
{
    [Header("Prefab Jugador")]
    [SerializeField] private GameObject jugador;

    [Header("Límites")]
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 posicion = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        PhotonNetwork.Instantiate(jugador.name, posicion, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
