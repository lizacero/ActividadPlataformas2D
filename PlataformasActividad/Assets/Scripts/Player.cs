using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private float inputH;
    [Header("Sistema de movimiento")]
    [SerializeField] private Transform pies;
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float fuerzaSalto;
    [SerializeField] private float distanciaDeteccionSuelo;
    [SerializeField] private LayerMask queEsSaltable;
    [SerializeField] private AudioClip saltoSound;

    [Header("Sistema de combate")]
    [SerializeField] private Transform puntoAtaque;
    [SerializeField] private float radioAtaque;
    [SerializeField] private float danhoAtaque;
    [SerializeField] private LayerMask queEsDanhable;
    private Animator anim;
    [SerializeField] private AudioClip ataqueSound;

    //Canvas de game over
    [Header("UI")]
    [SerializeField] private GameObject botonAgain;
    [SerializeField] private GameObject botonExit;
    [SerializeField] private TextMeshProUGUI textoGameOver;
    [SerializeField] private TextMeshProUGUI textoLives;
    [SerializeField] private GameObject mago;

    [SerializeField] private AudioClip colectarH;
    [SerializeField] private AudioClip colectarC;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Time.timeScale = 1;

    }

    // Update is called once per frame
    void Update()
    {
        Movimiento();

        Saltar();

        LanzarAtaque();

        
    }

    private void LanzarAtaque()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("attack");
            ControladorSonido.Instance.EjecutarSonido(ataqueSound);
        }
    }

    //Se ejecuta desde evento de animación
    private void Ataque()
    {
        //Lanzar el trigger instantaneo
        Collider2D[] colidersTocados = Physics2D.OverlapCircleAll(puntoAtaque.position, radioAtaque, queEsDanhable);
        foreach(Collider2D item in colidersTocados)
        {
            SistemaVidas sistemaVidas = item.gameObject.GetComponent<SistemaVidas>();
            sistemaVidas.RecibirDanho(danhoAtaque);

        }
    }

    private void Saltar()
    {
        if (Input.GetKeyDown(KeyCode.Space) && EstoyEnSuelo())
        {
            rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            anim.SetTrigger("saltar");
            ControladorSonido.Instance.EjecutarSonido(saltoSound);
        }
    }

    private bool EstoyEnSuelo()
    {
        bool tocado = Physics2D.Raycast(pies.position, Vector3.down, distanciaDeteccionSuelo, queEsSaltable);
        return tocado;
    }

    private void Movimiento()
    {
        inputH = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(inputH * velocidadMovimiento, rb.velocity.y); //velocity ya es unidades por segundo, no se multiplica por deltaTime
        if (inputH != 0)
        {
            anim.SetBool("running", true);
            if (inputH > 0)
            {
                transform.eulerAngles = Vector3.zero;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }
        else
        {
            anim.SetBool("running", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D elOtro)
    {
        if (elOtro.gameObject.CompareTag("limite"))
        {
            botonAgain.SetActive(true); // Se activan los botones PlayAgain y Exit
            botonExit.SetActive(true);
            textoGameOver.text = "Game Over"; //Se visualiza el texto GameOver
            textoLives.text = "0";
            Time.timeScale = 0;
        }
        if (elOtro.gameObject.CompareTag("coin"))
        {
            Destroy(elOtro.gameObject);
            ControladorSonido.Instance.EjecutarSonido(colectarC);
        }
        if (elOtro.gameObject.CompareTag("heart"))
        {
            Destroy(elOtro.gameObject);
            ControladorSonido.Instance.EjecutarSonido(colectarH);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(puntoAtaque.position, radioAtaque);
    }
}
