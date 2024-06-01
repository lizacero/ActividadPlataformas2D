using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Mago : MonoBehaviour
{
    [SerializeField] private GameObject bolaFuego;
    [SerializeField] private Transform puntoSpawn;
    [SerializeField] private float tiempoAtaques;
    [SerializeField] private Transform player;

    [Header("Deteccion")]
    [SerializeField] private LayerMask queEsDetectable;
    [SerializeField] private Transform puntoDeteccion;
    [SerializeField] private float radioDeteccion;
    
    private Animator anim;

    [Header("UI")]
    [SerializeField] private GameObject botonAgain;
    [SerializeField] private GameObject botonExit;
    [SerializeField] private TextMeshProUGUI textoGameOver;

    private bool ataque = false;
    private Vector3 destinoActual;
    private SpriteRenderer mySpriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Detectar();

    }
    private void Detectar()
    {
        Collider2D[] colidersDetectados = Physics2D.OverlapCircleAll(puntoDeteccion.position, radioDeteccion, queEsDetectable);
        if (colidersDetectados.Length > 0)
        {
            ataque = true;
            Debug.Log("Se detectó!");
            if (ataque == true)
            {
                StartCoroutine(RutinaAtaque());
                
                destinoActual = player.position;
                if (destinoActual.x > transform.position.x)
                {
                    transform.eulerAngles = Vector3.zero;
                }
                else
                {                    
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
            }
        }
    }


    IEnumerator RutinaAtaque()
    {
        while (true)
        {
            anim.SetTrigger("atacar");
            yield return new WaitForSeconds(tiempoAtaques);
        }
    }
    private void OnDestroy()
    {
        botonAgain.SetActive(true); // Se activan los botones PlayAgain y Exit
        botonExit.SetActive(true);
        textoGameOver.text = "You Won";
        Time.timeScale = 0;
    }


    private void LanzarBola()
    {
        Instantiate(bolaFuego, puntoSpawn.position, transform.rotation);
    }
}
