using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

using TMPro;


using UnityEngine.SceneManagement;

public class ControlConexion : MonoBehaviourPunCallbacks
{
    #region Variables privadas

    [Header("Paneles")]
    [SerializeField] private GameObject panelRegistro;
    [SerializeField] private GameObject panelBienvenida;
    [SerializeField] private GameObject panelCrearSala;
    [SerializeField] private GameObject panelConectarSala;
    [SerializeField] private GameObject panelSeleccionAvatar;
    [SerializeField] private GameObject panelSala;

    [Header("Registro de usuario")]
    [SerializeField] private TMP_InputField textoNombreUsuarioRegistrar;

    [Header("Crear Sala")]
    [SerializeField] private TMP_InputField textoCapacidadMinima;
    [SerializeField] private TMP_InputField textoCapacidadMaxima;
    [SerializeField] private Toggle toggleSalaPrivada;
    [SerializeField] private Button botonCrearSala;

    [Header("Conectar a Sala")]
    [SerializeField] private TMP_InputField textoNombreSalaPrivada;
    [SerializeField] private Button botonConectarSala;

    [Header("Seleccionar Avatar")]
    [SerializeField] private Button botonSeleccionarAvatar;

    [Header("Texto Paneles Superior e Inferior")]
    [SerializeField] private TMP_Text textoPanelSuperior;
    [SerializeField] private TMP_Text textoPanelInferior;

    ExitGames.Client.Photon.Hashtable propiedadesJugador;
    private GameObject[] paneles;
    #endregion

    #region StartUpdate
    // Start is called before the first frame update
    void Start()
    {
        paneles = new GameObject[] { panelRegistro, panelBienvenida, panelCrearSala, panelConectarSala, panelSeleccionAvatar, panelSala };
        EstadoInicialPaneles();
    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    #region Paneles
    /// <summary>
    /// El panel pasado por parámetro se activa después de que se hayan desactivado el resto.
    /// </summary>
    /// <param name="_panel"></param>
    private void ActivarPanel(GameObject _panel)
    {
        foreach (GameObject panel in paneles)
        {
            panel.SetActive(false);
        }

        _panel.SetActive(true);
        TextoPanelSuperior(_panel);

    }

    /// <summary>
    /// Estado inicial del menú
    /// </summary>
    private void EstadoInicialPaneles()
    {
        ActivarPanel(panelRegistro);
    }

    /// <summary>
    /// Determina el texto del panel superior según el panel activo
    /// </summary>
    private void TextoPanelSuperior(GameObject _panel)
    {
        string texto = "";

        if (_panel == panelRegistro)
        {
            texto = "Registro de usuario";
        }
        else if (_panel == panelBienvenida)
        {
            texto = "Bienvenido al juego: " + PhotonNetwork.NickName;
        }
        else if (_panel == panelCrearSala)
        {
            texto = "Crear sala";
        }
        else if (_panel == panelConectarSala)
        {
            texto = "Conectar a sala";
        }
        else if (_panel == panelSeleccionAvatar)
        {
            texto = "Seleccionar avatar";
        }
        else if (_panel == panelSala)
        {
            texto = "Sala: ";
        }

        textoPanelSuperior.text = texto;
    }
    #endregion

    #region Botones
    /// <summary>
    /// Se encarga de registrar un usuario
    /// </summary>
    public void RegistroUsuario()
    {
        if (!string.IsNullOrEmpty(textoNombreUsuarioRegistrar.text)
    && !string.IsNullOrWhiteSpace(textoNombreUsuarioRegistrar.text))

        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.AutomaticallySyncScene = true;

            PhotonNetwork.NickName = textoNombreUsuarioRegistrar.text;

            Estado("Conectando a Photon");
        }
        else
        {
            Estado("Introduzca un nombre de jugador vúlido");
        }

    }

    /// <summary>
    /// Va a la pantalla de crear una sala
    /// </summary>
    public void CrearSala()
    {
        ActivarPanel(panelCrearSala);
    }

    /// <summary>
    /// Va a la pantalla de conectarse a una sala
    /// </summary>
    public void ConectarSala()
    {
        ActivarPanel(panelConectarSala);
    }

    /// <summary>
    /// Va a la pantalla de seleccionar un avatar
    /// </summary>
    public void SeleccionarAvatar()
    {
        ActivarPanel(panelSeleccionAvatar);
    }

    /// <summary>
    /// Crea una sala
    /// </summary>
    public void ConfirmarCrearSala()
    {
    }

    /// <summary>
    /// Se conecta a una sala
    /// </summary>
    public void ConfirmarConectarSala()
    {
        ActivarPanel(panelSala);
    }

    /// <summary>
    /// Selecciona un avatar
    /// </summary>
    public void ConfirmarSeleccionarAvatar()
    {
    }

    public void IniciarPartida()
    {

    }

    /// <summary>
    /// Se sale del juego
    /// </summary>
    public void SalirJuego()
    {
        Application.Quit();
    }

    /// <summary>
    /// Vuelve a la pantalla de registro de usuario
    /// </summary>
    public void AtrasRegistro()
    {
        ActivarPanel(panelRegistro);
        PhotonNetwork.Disconnect();
    }
    /// <summary>
    /// Vuelve a la pantalla de bienvenida
    /// </summary>
    public void AtrasBienvenida()
    {
        ActivarPanel(panelBienvenida);
        Estado("");
    }
    /// <summary>
    /// Abandona la sala
    /// </summary>
    public void AbandonarSala()
    {
        ActivarPanel(panelConectarSala);
        PhotonNetwork.LeaveRoom();
        Estado("");
    }
    #endregion

    #region Callbacks
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Estado("Conectado a Photon");

        ActivarPanel(panelBienvenida);

        PhotonNetwork.JoinLobby(); // unirse al lobby por defecto
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        //base.OnDisconnected(cause);

        Estado("Desconectado de Photon: " + cause);
    }

    public override void OnJoinedRoom()
    {

    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {

    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {

    }

    public override void OnLeftRoom()
    {

    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {

    }
    #endregion

    #region Otros Metodos
    /// <summary>
    /// La cadena de texto pasada por parámetro es el mensaje del panel inferior
    /// </summary>
    /// <param name="_msg"></param>
    private void Estado(string _msg)
    {
        textoPanelInferior.text = _msg;
        Debug.Log(_msg);
    }
    #endregion

}
