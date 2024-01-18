using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ControlJugador : MonoBehaviour
{
    [Header("Caracteristicas")]
    [SerializeField] private float velocidad;

    Rigidbody rigi;
    Animator anim;

    private PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        rigi = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            /*        rigi.velocity = transform.forward * Input.GetAxis("Vertical") * velocidad +
                            transform.right * Input.GetAxis("Horizontal") * velocidad +
                            transform.up * rigi.velocity.y;
                    anim.SetFloat("velocidad", rigi.velocity.magnitude);
                    anim.SetFloat("velocidadY", new Vector3(0, rigi.velocity.y, 0).magnitude);
            */
        }
    }
}
