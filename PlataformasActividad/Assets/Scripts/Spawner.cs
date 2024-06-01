using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject murcielagoPrefab;
    [Header("Deteccion")]
    [SerializeField] private LayerMask queEsDetectable;
    [SerializeField] private Transform puntoSpawn;
    [SerializeField] private Transform puntoDeteccion;
    [SerializeField] private float sizeDeteccion;

    private bool spawn = true;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RutinaSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator RutinaSpawn()
    {
        while(spawn==true)
        {
            Collider2D[] colidersDetectados = Physics2D.OverlapCircleAll(puntoDeteccion.position, sizeDeteccion, queEsDetectable);
            if (colidersDetectados.Length > 0)
            {
                Debug.Log(colidersDetectados.Length);
                int i = 5;
                while (i>0)
                {
                    Spawnear();
                    i--;
                    yield return new WaitForSeconds(5);
                }
                spawn = false;
            }
            yield return null;
        }
    }
    private void Spawnear()
    {
        Instantiate(murcielagoPrefab, puntoSpawn.position, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(puntoDeteccion.position, sizeDeteccion);
    }

}
