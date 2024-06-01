using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaFuego : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float impulsoDisparo;
    [SerializeField] private float danhoAtaque;
    [SerializeField] private AudioClip ataqueSound;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //forward = eje z (azul) -- up = eje Y (verde) -- right == eje x (rojo)
        rb.AddForce(transform.right * impulsoDisparo, ForceMode2D.Impulse);
        ControladorSonido.Instance.EjecutarSonido(ataqueSound);
        StartCoroutine(AutoDestruccion());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator AutoDestruccion()
    {
        yield return new WaitForSeconds(5F);
        Destroy(this.gameObject);
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
        }
    }
    private void OnCollisionEnter2D(Collision2D elOtro)
    {
        if (elOtro.gameObject.CompareTag("PlayerHitbox"))
        {
            VidasPlayer vidasPlayer = elOtro.gameObject.GetComponent<VidasPlayer>();
            vidasPlayer.DanhoPlayer(danhoAtaque);
        }
    }
}
