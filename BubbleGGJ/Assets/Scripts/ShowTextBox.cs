using UnityEngine;

public class ShowTextBox : MonoBehaviour
{
    public GameObject textBox;
    void OnTriggerEnter(Collider other)
   {
    if(other.tag == "Player")
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        textBox.SetActive(true);
        Destroy(gameObject);
    }
   }
}
