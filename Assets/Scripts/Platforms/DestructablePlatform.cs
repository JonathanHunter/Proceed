﻿//Proceed: Jonathan Hunter, Larry Smith, Justin Coates, Chris Tansey
using UnityEngine;

namespace Assets.Scripts.Platforms
{
    class DestructablePlatform : MonoBehaviour
    {
        public float deathTime = 1f;
        public bool die = false;
        public Collider[] floor;

        void Update()
        {
            if (Util.GameState.state != Util.GameState.State.Playing)
                return;
            if (die)
            {
                transform.Translate(-transform.up * Time.deltaTime * 2);
                deathTime -= Time.deltaTime;
                if (deathTime < 0)
                {
                    Player.PlayerController player = GetComponentInChildren<Player.PlayerController>();
                    if (player != null)
                    {
                        player.gameObject.transform.parent = null;
                        foreach (Collider c in floor)
                            c.enabled = false;
                    }
                    else
                        Destroy(this.gameObject);
                }
            }
        }

        void OnTriggerEnter(Collider collider)
        {
            if (Util.GameState.state != Util.GameState.State.Playing)
                return;
            die = true;
        }
    }
}
