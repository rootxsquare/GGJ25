using UnityEngine;
using UnityEngine.UIElements;


public class BubbleScript : MonoBehaviour
{
     float t = 1;
    [SerializeField] float bubbleLife = 5;
    public GameObject bubblePopParticle;
    bool isSimulating = false;
    [SerializeField] float maxSize;
    [SerializeField] float growSpeed;

   

    // Update is called once per frame
    void Update()
    {
        if(isSimulating) t -= Time.deltaTime / bubbleLife;



    

        if (Input.GetMouseButton(1) && !isSimulating)
        {
            
            transform.localScale += Vector3.one * Time.deltaTime * growSpeed;
            
            if (transform.localScale.x > maxSize) bubblePop();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            if (transform.localScale.x < 0.25f) bubblePop();
            bubbleLife = transform.localScale.x * 10;

            bubbleSimulate();
            GetComponent<AudioSource>().Stop();
        }

        transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material.SetFloat("_FresnelPower", t);

        if(t<= 0.1f)
        {
            bubblePop();
        }
    }

    void bubblePop()
    {
       
        GameObject popParticle = Instantiate(bubblePopParticle, transform.GetChild(1).position, Quaternion.identity);
        popParticle.transform.localScale = transform.localScale;
        Destroy(popParticle, 2);
        Destroy(gameObject);

    }

    public void bubbleSimulate()
    {
        isSimulating = true;
        transform.GetChild(1).gameObject.SetActive(true);
        foreach(Rigidbody rb in GetComponentsInChildren<Rigidbody>())
        {
            rb.AddForce(Vector3.up *3);
        }
    }
}
