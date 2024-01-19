using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ControlJugador : MonoBehaviourPun
{
    [Header("Caracteristicas")]
    [SerializeField] private float velocidad;

    Rigidbody2D rigi;
    Animator anim;

    private PhotonView photonView;
    private int monedas;

    // Start is called before the first frame update
    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        photonView = GetComponent<PhotonView>();
        monedas = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            rigi.AddForce(new Vector2(Input.GetAxis("Horizontal") * Time.deltaTime * velocidad, 0));

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Moneda"))
        {
            PhotonNetwork.Destroy(collision.gameObject);
            monedas += 1;
            ControlConexion.conex.propiedadesJugador["monedas"] = monedas;
        }
    }
}
