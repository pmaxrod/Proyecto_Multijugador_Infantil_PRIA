using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using TMPro;

public class FinPartida : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text textoGanador;

    // Start is called before the first frame update
    void Start()
    {
        TextoGanador();
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

    private void TextoGanador()
    {
        int monedasGanador = 0;
        foreach (Player jugador in ControlJuego.instance.jugadores)
        {
            int monedas = (int)jugador.CustomProperties["monedas"];

            if(monedas >= monedasGanador)
            {
                monedasGanador = monedas;
            }
        }

        textoGanador.text = "Ganador: " + monedasGanador;

    }
}
