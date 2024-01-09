using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectorAvatar : MonoBehaviour
{
    //    [SerializeField] private GameObject listaAvatares;
    [SerializeField] public Sprite[] listaAvatares;
    [SerializeField] private GameObject image;

    private int nHijos;
    private int indiceAvatares;

    //private GameObject[] avatares;

    // Start is called before the first frame update
    void Start()
    {
        indiceAvatares = 0;
        nHijos = listaAvatares.Length;
//        avatares = new GameObject[nHijos];

/*        for (int i = 0; i < nHijos; i++)
        {
            avatares[i] = listaAvatares.transform.GetChild(i).gameObject;
        }
*/
        // cargar en el componente Image el avatar con indice indiceAvatares
        image.GetComponent<Image>().sprite = listaAvatares[indiceAvatares];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BotonDerecho()
    {
        indiceAvatares++;

        if (indiceAvatares >= nHijos)
        {
            indiceAvatares = 0;
        }

        image.GetComponent<Image>().sprite = listaAvatares[indiceAvatares];
        ControlConexion.conex.Estado("");
    }

    public void BotonIzquierdo()
    {
        indiceAvatares--;

        if (indiceAvatares < 0)
        {
            indiceAvatares = nHijos - 1;
        }

        image.GetComponent<Image>().sprite = listaAvatares[indiceAvatares];
        ControlConexion.conex.Estado("");
    }

    /// <summary>
    /// Guarda el número del avatar seleccionado en el atributo de la clase ControlConexion
    /// </summary>
    public void SeleccionarAvatar()
    {
        ControlConexion.conex.avatarSeleccionado = indiceAvatares;
        ControlConexion.conex.avatarJugador.GetComponent<Image>().sprite = listaAvatares[indiceAvatares];

        ControlConexion.conex.Estado("El avatar seleccionado es el: " + indiceAvatares);
    }
}
