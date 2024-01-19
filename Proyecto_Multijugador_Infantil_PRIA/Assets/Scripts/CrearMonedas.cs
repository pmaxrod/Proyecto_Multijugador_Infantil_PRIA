using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrearMonedas : MonoBehaviour
{
    [SerializeField] private GameObject moneda;
    [SerializeField] private float tiempoInicio;
    [SerializeField] private float tiempoGeneracion;

    private float limitX = 5;
    private float limitY = 5;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("GenerarMoneda", tiempoInicio, tiempoGeneracion);
    }


    // Update is called once per frame
    void Update()
    {

    }

    private void GenerarMoneda()
    {
        float x = Random.Range(-limitX, limitX);
        float y = Random.Range(-limitY, limitY);

        moneda.transform.position = new Vector2(x, y);

        PhotonNetwork.Instantiate(moneda.name, moneda.transform.position, Quaternion.identity);
    }
}
