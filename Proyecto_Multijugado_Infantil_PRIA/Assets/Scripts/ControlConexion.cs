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
    // private GameObject panelSeleccionAvatar;
    [SerializeField] private GameObject panelSala;
    private GameObject[] paneles;

    [Header("Registro de usuario")]
    [SerializeField] private TMP_InputField textoNombreUsuarioRegistrar;
    [SerializeField] private Button botonPanelCrearSala;
    [SerializeField] private Button botonPanelConectarSala;

    [Header("Seleccionar Avatar")]
    static public ControlConexion conex;
    public int avatarSeleccionado;

    [Header("Crear Sala")]
    [SerializeField] private TMP_InputField textoNombreSala;
    [SerializeField] private TMP_InputField textoCapacidadMinima;
    [SerializeField] private TMP_InputField textoCapacidadMaxima;
    [SerializeField] private Toggle toggleSalaPrivada;

    [Header("Conectar a Sala")]
    [SerializeField] private TMP_InputField textoNombreSalaPrivada;
    [SerializeField] private GameObject elemSala;
    [SerializeField] private GameObject contenedorSala;

    Dictionary<string, RoomInfo> listaSalas;

    [Header("Paneles Superior e Inferior")]
    [SerializeField] private TMP_Text textoPanelSuperior;
    [SerializeField] private TMP_Text textoPanelInferior;
    public Image avatarJugador;
    [SerializeField] private TMP_Text textoNombreJugadorPanelSuperior;


    [Header("Sala con Jugadores")]
    [SerializeField] private TMP_InputField textoNombreSalaPanelSala;
    [SerializeField] private TMP_InputField textoCapacidadPanelSala;
    [SerializeField] private TMP_InputField textoListadoJugadores;

    [SerializeField] private GameObject elemJugador;
    [SerializeField] private GameObject contenedorJugador;

    ExitGames.Client.Photon.Hashtable propiedadesJugador;

    #endregion

    #region StartUpdate
    // Start is called before the first frame update
    void Start()
    {
        conex = this;

        avatarSeleccionado = -1;

        listaSalas = new Dictionary<string, RoomInfo>();

        propiedadesJugador = new ExitGames.Client.Photon.Hashtable();

//        paneles = new GameObject[] { panelRegistro, panelBienvenida, panelCrearSala, panelConectarSala, panelSeleccionAvatar, panelSala };
        paneles = new GameObject[] { panelRegistro, panelBienvenida, panelCrearSala, panelConectarSala, panelSala };

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
        /*
                else if (_panel == panelSeleccionAvatar)
                {
                    texto = "Seleccionar avatar";
                }
        */
        else if (_panel == panelSala)
        {
            texto = "Sala: " + PhotonNetwork.CurrentRoom.Name;
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
            textoNombreJugadorPanelSuperior.text = PhotonNetwork.NickName;

            AsignarAvatar();

            Estado("Conectando a Photon");
        }
        else
        {
            Estado("Introduzca un nombre de jugador válido");
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
/*
    public void SeleccionarAvatar()
    {
        ActivarPanel(panelSeleccionAvatar);
    }
*/

    /// <summary>
    /// Crea una sala
    /// </summary>
    public void ConfirmarCrearSala()
    {
        byte minJugadores;
        byte maxJugadores;


        minJugadores = byte.Parse(textoCapacidadMinima.text);
        maxJugadores = byte.Parse(textoCapacidadMaxima.text);

        if (!string.IsNullOrEmpty(textoNombreSala.text))
        {
            if (!(minJugadores > maxJugadores || maxJugadores > 20)
                || minJugadores > 20 || maxJugadores < 2
                || minJugadores < 2)
            {
                RoomOptions opcionesSala = new RoomOptions();

                opcionesSala.MaxPlayers = maxJugadores;
                opcionesSala.IsVisible = !toggleSalaPrivada.isOn;

                Estado("Creando la nueva sala: " + textoNombreSala.text);

                PhotonNetwork.CreateRoom(textoNombreSala.text,
                    opcionesSala, TypedLobby.Default);

                Estado("Creando la nueva sala: " + textoNombreSala.text);
            }
            else
            {
                Estado("Valores de capacidad de sala incorrectos");
            }
        }
        else
        {
            Estado("Introduzca un nombre de sala correcto.");
        }
    }

    /// <summary>
    /// Se conecta a una sala
    /// </summary>
    public void ConfirmarConectarSala()
    {
        /*        if (!string.IsNullOrEmpty(txtNombreSalaAUnirse.text))
                {
                    PhotonNetwork.JoinRoom(txtNombreSalaAUnirse.text);
                }

                else
                    Estado("Introduzca un nombre correcto para la sala");
        */
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
    }

    /// <summary>
    /// Abandona la sala
    /// </summary>
    public void AbandonarSala()
    {
        ActivarPanel(panelBienvenida);
        PhotonNetwork.LeaveRoom();
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
        Estado("Desconectado de Photon: " + cause);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Estado("No ha sido posible crear la sala: " + message);
    }

    public override void OnCreatedRoom()
    {
        string mensaje = PhotonNetwork.NickName + " se ha conectado a "
            + PhotonNetwork.CurrentRoom.Name;

        Estado(mensaje);

        ActivarPanel(panelSala);
    }

    public override void OnJoinedRoom()
    {
        string mensaje = PhotonNetwork.NickName + " se ha unido a "
            + PhotonNetwork.CurrentRoom.Name;

        Estado(mensaje);

        ActivarPanel(panelSala);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Estado("No ha sido posible unirse a la sala: " + message);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
    }
    #endregion

    #region Otros Metodos
    /// <summary>
    /// La cadena de texto pasada por parámetro es el mensaje del panel inferior
    /// </summary>
    /// <param name="_msg"></param>
    public void Estado(string _msg)
    {
        textoPanelInferior.text = _msg;
        Debug.Log(_msg);
    }

    /// <summary>
    /// Asigna el avatar
    /// </summary>
    public void AsignarAvatar()
    {
        //ActivarPanel(panelBienvenida);
        if (avatarSeleccionado >= 0)
        {
            Estado("Seleccionado avatar " + avatarSeleccionado);

            //Activamos los botones si tenemos un avatar seleccionado
            //            botonPanelCrearSala.interactable = true;
            //            botonPanelConectarSala.interactable = true;

            propiedadesJugador["avatar"] = avatarSeleccionado;

            PhotonNetwork.LocalPlayer.SetCustomProperties(propiedadesJugador);

            Debug.Log(propiedadesJugador["avatar"]);
        }
        else
        {
            Estado("No hay avatar seleccionado");
        }
    }
    #endregion

}
