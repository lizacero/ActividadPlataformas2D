using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaVidas : MonoBehaviour
{
    [SerializeField] private float vidas;
    [SerializeField] private GameObject elementoPrefab;

    public void RecibirDanho(float danhoRecibido)
    {
        vidas -= danhoRecibido;
        if(vidas <=0)
        {
            Destroy(this.gameObject);
            //spawn corazón
            SpawnElemento();

        }
    }

    private void SpawnElemento()
    {
        Vector3 punto = new Vector3(transform.position.x, transform.position.y,0);
        Instantiate(elementoPrefab, punto, Quaternion.identity);
    }
}
