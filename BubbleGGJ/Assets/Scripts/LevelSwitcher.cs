using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelSwitcher : MonoBehaviour
{
    public GameObject circleTransition;
    public int sceneToLoad;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

IEnumerator switchScene()
{
    FindAnyObjectByType<PlayerController>().enabled = false;
    circleTransition.GetComponent<Animator>().enabled = true;
    yield return new WaitForSeconds(2);
    SceneManager.LoadScene(sceneToLoad);


}
    
   void OnTriggerEnter(Collider other)
   {
    if(other.tag == "Player")
    {
        StartCoroutine(switchScene());
    }
   }
}
