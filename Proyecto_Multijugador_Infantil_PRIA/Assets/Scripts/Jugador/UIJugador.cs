using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIJugador : MonoBehaviourPunCallbacks
{
    [SerializeField] private TextMeshProUGUI nombreJugador_Cabeza;

    // Start is called before the first frame update
    void Start()
    {
        nombreJugador_Cabeza.text = photonView.Owner.NickName;
    }

}
