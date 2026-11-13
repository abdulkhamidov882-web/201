 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class Laser : MonoBehaviour



public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8.0f;

    void Update()
    {
        // Lazerni yuqoriga harakatlantirish
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        // Ekrandan chiqib ketsa xotiradan o'chirish
        if (transform.position.y > 8.0f)
        {
            Destroy(gameObject);
        }
    }

    // Lazer dushmanga tekkanini aniqlash uchun
    private void OnTriggerEnter(Collider other)
   {
    if (other.CompareTag("Enemy_cube"))
    {
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
   }
}