using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VidasPlayer : MonoBehaviour
{
    [SerializeField] private float vidas;
    //Canvas de game over
    [Header("UI")]
    [SerializeField] private GameObject botonAgain;
    [SerializeField] private GameObject botonExit;
    [SerializeField] private TextMeshProUGUI textoGameOver;
    [SerializeField] private TextMeshProUGUI textoLives;
    [SerializeField] private TextMeshProUGUI textoMonedas;
    private int monedas;

    public void DanhoPlayer(float danhoPlayer)
    {
        vidas -= danhoPlayer;
        Debug.Log(vidas);
        textoLives.text = "" + vidas;
        if (vidas <= 0)
        {
            Destroy(this.gameObject);
            botonAgain.SetActive(true); // Se activan los botones PlayAgain y Exit
            botonExit.SetActive(true);
            textoGameOver.text = "Game Over";
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

     public void MonedasRecolectadas(int monedasRecolectadas)
    {
        monedas += monedasRecolectadas;
        textoMonedas.text =""+ monedas;
    }
}
