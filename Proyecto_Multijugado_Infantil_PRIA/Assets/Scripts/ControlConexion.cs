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

    [Header("Botones")]
    [SerializeField] private Button botonCrearSala;
    [SerializeField] private Button botonConectarSala;

    [Header("Texto Paneles Superior e Inferior")]
    [SerializeField] private TMP_Text textoPanelSuperior;
    [SerializeField] private TMP_Text textoPanelInferior;

    [Header("Cajas de texto")]
    [SerializeField] private TMP_InputField textoNombreUsuarioRegistrar;

    ExitGames.Client.Photon.Hashtable propiedadesJugador;
    #endregion

    #region StartUpdate
    // Start is called before the first frame update
    void Start()
    {
        EstadoInicialPaneles();
    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    #region Paneles
    /*
        /// <summary>
        /// Activar o desactivar panel
        /// </summary>
        /// <param name="_panel">Panel a activar o desactivar</param>
        /// <param name="_estado">true -> activar; false -> desactivar</param>
        private void EstadoPanel(GameObject _panel, bool _estado)
        {
            _panel.SetActive(_estado);
        }

        /// <summary>
        /// El primer panel se desactiva y el segundo se activa.
        /// </summary>
        /// <param name="_panel1"></param>
        /// <param name="_panel2"></param>
        private void CambiarPanel(GameObject _panel1, GameObject _panel2)
        {
            _panel1.SetActive(false);
            _panel2.SetActive(true);

            TextoPanelSuperior(_panel2);
        }
    */
    private void ActivarPanel(GameObject _panel)
    {
        panelRegistro.SetActive(false);
        panelBienvenida.SetActive(false);

        _panel.SetActive(true);
        TextoPanelSuperior(_panel);

    }

    /// <summary>
    /// Estado inicial del menú
    /// </summary>
    private void EstadoInicialPaneles()
    {
        //      EstadoPanel(panelRegistro, true);
        //      EstadoPanel(panelBienvenida, false);
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

            //            EstadoPanel(panelRegistro, false);
            //ActivarPanel(panelBienvenida);
        }
        else
        {
            Estado("Introduzca un nombre de jugador válido");
        }

    }

    public void CrearSala()
    {
        Estado("Pantalla de crear sala");
    }

    public void ConectarSala()
    {
        Estado("Pantalla de conectarse a una sala");

    }
    #endregion

    #region Callbacks
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Estado("Conectado a Photon");


        //CambiarPanel(panelRegistro, panelBienvenida);
        ActivarPanel(panelBienvenida);

        // txtBienvenida.text = "Bienvenido al juego: " + PhotonNetwork.NickName;

        PhotonNetwork.JoinLobby(); // unirse al lobby por defecto
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        //base.OnDisconnected(cause);

        Estado("Desconectado de Photon: " + cause);
    }
    #endregion

    #region Otros Metodos
    private void Estado(string _msg)
    {
        textoPanelInferior.text = _msg;
        Debug.Log(_msg);
    }
    #endregion

}
