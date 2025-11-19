using UnityEngine;

public class ParticleDisappear : MonoBehaviour
{
    public float duration = 2f;
    private float t = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (t<duration)
        {
            t += Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
        }
       
    }
}
