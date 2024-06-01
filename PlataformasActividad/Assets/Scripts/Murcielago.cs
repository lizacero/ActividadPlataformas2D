using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Murcielago : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float velocidaPatrulla;
    [SerializeField] private Transform player;
    private Vector3 destinoActual;
    private int indiceActual = 0;

    [Header("Deteccion")]
    [SerializeField] private LayerMask queEsDetectable;
    [SerializeField] private Transform puntoDeteccion;
    [SerializeField] private float radioDeteccion;
    [SerializeField] private float tiempoAtaque;

    [Header("Ataque")]
    [SerializeField] private float danhoAtaque;
    private Animator anim;
    private bool ataque = false;
    [SerializeField] private AudioClip explotarSound;
    //private float explosionTime = 0.2f;


    // Start is called before the first frame update
    void Start()
    {
        destinoActual = waypoints[indiceActual].position;
        StartCoroutine(Patrulla());
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator Patrulla()
    {
        while (true)
        {
            while (transform.position != destinoActual)
            {
                transform.position = Vector3.MoveTowards(transform.position, destinoActual, velocidaPatrulla * Time.deltaTime);
                yield return null;
            }
            DefinirNuevoDestino();
            Detectar();
            if (ataque == true)
            {
                destinoActual = player.position;
                while (transform.position != destinoActual)
                {
                    transform.position = Vector3.MoveTowards(transform.position, destinoActual, velocidaPatrulla * Time.deltaTime);
                    yield return null;
                    Debug.Log("siguiendo");
                }
                Debug.Log("dejar de seguir");
                EnfocarDestino();
            }
            yield return null;
        }

    }

    private void DefinirNuevoDestino()
    {
        indiceActual++;
        if (indiceActual >= waypoints.Length)
        {
            indiceActual = 0;
        }
        destinoActual = waypoints[indiceActual].position;
        EnfocarDestino();
    }

    private void EnfocarDestino()
    {
        if (destinoActual.x > transform.position.x)
        {
            transform.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
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
            }
        }
    }

    IEnumerator RutinaAtaque()
    {
        while (true)
        {
            anim.SetTrigger("atacar");
            yield return new WaitForSeconds(tiempoAtaque);

        }
    }

    IEnumerator Explotar()
    {
        anim.SetTrigger("explosion");
        yield return new WaitForSeconds(0.1f);
        Destroy(this.gameObject);
        ControladorSonido.Instance.EjecutarSonido(explotarSound);

    }

    private void OnTriggerEnter2D(Collider2D elOtro)
    {
        if (elOtro.gameObject.CompareTag("DeteccionPlayer"))
        {
        }
        else if (elOtro.gameObject.CompareTag("PlayerHitbox"))
        {
            VidasPlayer vidasPlayer = elOtro.gameObject.GetComponent<VidasPlayer>();
            vidasPlayer.DanhoPlayer(danhoAtaque);
            StartCoroutine(Explotar());
        }
    }
}
