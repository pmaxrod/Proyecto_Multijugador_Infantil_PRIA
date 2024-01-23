using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class ControlJugador : MonoBehaviourPun
{
    [Header("Caracteristicas")]
    [SerializeField] private float velocidad;

    Rigidbody2D rigi;
    Animator anim;

    private PhotonView photon;
    private int monedas;
    private float horizontalInput;
    private float verticalInput;

    // Start is called before the first frame update
    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        photon = GetComponent<PhotonView>();
        monedas = 0;
        ActualizarMonedas(monedas);
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");

            Vector2 vector = new Vector2(horizontalInput * Time.deltaTime * velocidad, verticalInput * Time.deltaTime * velocidad);
            anim.SetFloat("velocidadX", horizontalInput);

            transform.Translate(vector);
            //            rigi.AddForce(vector);

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Moneda"))
        {
            PhotonNetwork.Destroy(collision.gameObject);
            monedas += 1;
            ActualizarMonedas(monedas);
        }
    }
    /// <summary>
    /// Actualiza la cantidad de monedas que tiene el jugador
    /// </summary>
    /// <param name="_cantidad"></param>
    private void ActualizarMonedas(int _cantidad)
    {
        monedas = _cantidad;
        ControlConexion.conex.propiedadesJugador["monedas"] = monedas;
        PhotonNetwork.LocalPlayer.SetCustomProperties(ControlConexion.conex.propiedadesJugador);
        ControlJuego.instance.textoMonedasRecogidas.text = "Monedas: " + monedas;
    }
}
