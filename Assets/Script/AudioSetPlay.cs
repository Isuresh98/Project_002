using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSetPlay : MonoBehaviour
{
    //audio
    public AudioSource audioSource;

    
    // Start is called before the first frame update
    void Start()
    {
        //audio
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
