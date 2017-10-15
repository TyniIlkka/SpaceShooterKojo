using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class PlayerDeath : Health
    {

        
        public GameObject playerObject;

        [SerializeField]
        public int lives = 3;
        [SerializeField]
        public Transform playerPrefab;

        // Use this for initialization
        void Start()
        {
             playerObject = GameObject.FindGameObjectWithTag("Player");
        }

        // Update is called once per frame
        IEnumerable Death()
        {
            if(playerObject != null)
            Debug.Log("Dead");
            
            yield return new WaitForSeconds(5);

        }
    }
}


