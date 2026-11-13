using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    //[SerializeField] -> private turidagi o'zgaruvchilarni koddan tashqarida (Unity Inspector oynasida) ko'rsatish va ularning qiymatini oson o'zgartirish uchun ishlatiladi.
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;

    private bool _stopSpawning = false; //!!!!!!!!
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
       
    }


    

    IEnumerator SpawnRoutine()
  {
    while (_stopSpawning == false)
    {
        Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
        GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);  //ushshu kodlarni o'rganib chiqish kerak32-34!
        newEnemy.transform.parent = _enemyContainer.transform;
        yield return new WaitForSeconds(5.0f);

        
    }
    // while loop (infinite loop)
    // -> O'yin davomida to'xtovsiz ishlab turadigan cheksiz sikl.
    // Instantiate enemy prefab
    // -> Dushman prefabini (andozasini) sahnada paydo qilish (yaratish).
    // yield wait for 5 seconds 
    // -> 5 sekund kutish (keyingi dushmanni chiqarishdan oldin 5 sekund pauza qilish).
  }

  public void OnPlayerDeath(){
    _stopSpawning = true;
  }
}
