using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.turbo.core
{
    public class AppContextContainer : MonoBehaviour
    {
        [SerializeReference]
        public AppContext context;

        void Start()
        {
            Debug.Log("Context Added");
        }

        // Update is called once per frame
        void Update()
        {
            Debug.Log("Call update on dirty context children");
        }
    }
}
