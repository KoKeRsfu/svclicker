﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyThisAfter : MonoBehaviour
{
	
	[SerializeField] float seconds;
	
    // Start is called before the first frame update
    void Start()
    {
	    Destroy(this.gameObject, seconds);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
