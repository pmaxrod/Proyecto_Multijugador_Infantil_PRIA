using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrearMonedas : MonoBehaviour
{
    [SerializeField] private GameObject moneda;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerarMoneda()
    {
        PhotonNetwork.Instantiate(moneda.name, moneda.transform.position, Quaternion.identity);
    }
}
