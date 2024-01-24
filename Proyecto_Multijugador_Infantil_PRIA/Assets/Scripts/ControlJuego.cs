using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ControlJuego : MonoBehaviourPunCallbacks
{
    [SerializeField] private float cuentaAtras = 20;
    private bool cuentaAtrasActiva = false;

    [SerializeField] private TMP_Text textoCuentaAtras;
    [SerializeField] public TMP_Text textoMonedasRecogidas;

    public static ControlJuego instance;
    public Player[] jugadores; // porque PlayerList desaparece al cargar la escena de fin de juego.

    //private PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        cuentaAtrasActiva = true;
        instance = this;
        jugadores = PhotonNetwork.PlayerList;
        //photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cuentaAtrasActiva)
        {
            if (cuentaAtras <= 0)
            {
                cuentaAtrasActiva = false;
                SceneManager.LoadSceneAsync("FinDeJuego");
            }
            else
            {
                photonView.RPC("CuentaAtras", RpcTarget.All);
            }
        }
    }

    [PunRPC]
    private void CuentaAtras()
    {
        cuentaAtras -= Time.deltaTime;
        ActualizarTextoCuentaAtras(cuentaAtras);
    }

    private void ActualizarTextoCuentaAtras(float tiempo)
    {
        float minutos = Mathf.FloorToInt(tiempo / 60);
        float segundos = Mathf.FloorToInt(tiempo % 60);
        textoCuentaAtras.text = string.Format("{0:00}:{1:00}", minutos, segundos);
    }
}
