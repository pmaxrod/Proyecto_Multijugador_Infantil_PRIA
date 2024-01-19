using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class FinPartida : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Salir()
    {
        SceneManager.LoadScene("PantallaTitulo");
        ControlConexion.conex.AbandonarSala();
    }

    public void NuevaPartida()
    {
        SceneManager.LoadScene("PantallaTitulo");
        ControlConexion.conex.PanelSalaIniciarPartida();
    }
}
