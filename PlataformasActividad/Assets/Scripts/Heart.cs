using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private float vidaExtra;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D elOtro)
    {
        if (elOtro.gameObject.CompareTag("DeteccionPlayer"))
        {
            Debug.Log("Player detectado!");
        }
        else if (elOtro.gameObject.CompareTag("PlayerHitbox"))
        {
            vidaExtra = -20;
            VidasPlayer vidasPlayer = elOtro.gameObject.GetComponent<VidasPlayer>();
            vidasPlayer.DanhoPlayer(vidaExtra);
        }
    }
}
