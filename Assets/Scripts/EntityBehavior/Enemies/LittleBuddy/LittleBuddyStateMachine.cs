﻿//Proceed: Jonathan Hunter, Larry Smith, Justin Coates, Chris Tansey
using UnityEngine;
using System.Collections;

namespace Assets.Scripts.EntityBehavior
{
    class LittleBuddyStateMachine
    {
        public enum State
        {
            Wait = 0, Patrol, Flee, Chase, Tired, Attack, Hit
        };

        private State currState;
        private double hold;
        private int moveCount;
        private float aggresionRadius, fleeDistance, timeCanChase, waitTime, restTime, followDistance;
        private bool coward, fearless;

        public LittleBuddyStateMachine(float aggresionRadius, float fleeDistance, float timeCanChase, float waitTime, float restTime, bool coward, bool fearless, float followDistance)
        {
            currState = 0;
            hold = 0;
            moveCount = 0;
            this.aggresionRadius = aggresionRadius;
            this.fleeDistance = fleeDistance;
            this.timeCanChase = timeCanChase;
            this.waitTime = waitTime;
            this.restTime = restTime;
            this.coward = coward;
            this.fearless = fearless;
            this.followDistance = followDistance;
            Random.seed = System.DateTime.Today.Millisecond;
        }

        public int Run(bool animDone, bool playerSpotted, bool playerDead, bool hpLow, bool infrontOfPlayer, float distance, bool hit, AnimatorClipInfo[] state)
        {
            //Debug.Log(currState);
            switch (currState)
            {
                case State.Wait: currState = Wait(hit, distance); break;
                case State.Patrol: currState = Patrol(playerSpotted, distance, hit); break;
                case State.Flee: currState = Flee(playerDead, distance, hit); break;
                case State.Chase: currState = Chase(playerDead, infrontOfPlayer, distance, hpLow, hit, followDistance); break;
                case State.Tired: currState = Tired(hit, distance); break;
                case State.Attack: currState = Attack(animDone, playerDead, infrontOfPlayer, hpLow, hit); break;
                case State.Hit: currState = Hit(animDone, hpLow, state); break;
            }
            return (int)currState;
        }

        private State Wait(bool hit, float distance)
        {
            if (hit)
            {
                hold = 0;
                return State.Hit;
            }
            hold += Time.deltaTime;
            
            if (hold > waitTime && distance > followDistance)
            {
                hold = 0;
                return State.Patrol;
            }
            
            return State.Wait;
        }

        private State Patrol(bool playerSpotted, float distance, bool hit)
        {
            if (hit)
                return State.Hit;
            if ((playerSpotted && distance < aggresionRadius) || (distance < aggresionRadius / 2f))
            {
                if (coward)
                    return State.Flee;
                return State.Chase;
            }
            float r = Random.Range(0f, 5f);
            if (r < .01f)
                return State.Wait;
            return State.Patrol;
        }

        private State Flee(bool playerDead, float distance, bool hit)
        {
            if (hit)
                return State.Hit;
            if (distance > fleeDistance || playerDead)
                return State.Patrol;

            return State.Flee;
        }

        private State Chase(bool playerDead, bool infrontOfPlayer, float distance, bool hpLow, bool hit, float followDistance)
        {
            
            if (hit)
            {
                hold = 0;
                return State.Hit;
            }
            /*
            if (hpLow && !fearless)
            {
                hold = 0;
                return State.Flee;
            }
            */
            /*
            if (distance > fleeDistance || playerDead)
            {
                hold = 0;
                return State.Patrol;
            }
            */
            if(distance < followDistance
                )
            {
                return State.Tired;
            }
            hold += Time.deltaTime;
            if (hold > timeCanChase)
            {
                hold = 0;
                return State.Tired;
            }
            return State.Chase;
        }

        private State Tired(bool hit, float distance)
        {
            if (hit)
            {
                hold = 0;
                return State.Hit;
            }
            hold += Time.deltaTime;
            if (hold > waitTime)
            {
                hold = 0;
                if (distance <= followDistance)
                    return State.Hit;
                return State.Patrol;
            }
            return State.Tired;
        }

        private State Attack(bool animDone, bool playerDead, bool infrontOfPlayer, bool hpLow, bool hit)
        {
            if (hit)
                return State.Hit;
            if (animDone)
            {
                if (playerDead)
                    return State.Patrol;
                if (infrontOfPlayer && (!hpLow || fearless))
                    return State.Attack;
                return State.Chase;
            }
            return State.Attack;
        }

        private State Hit(bool animDone, bool hpLow, AnimatorClipInfo[] state)
        {
            if (animDone && state.Length > 0 && state[0].clip.name.Equals("Hurt"))
            {
                if (coward || (hpLow && !fearless))
                    return State.Flee;
                return State.Wait;
            }
            return State.Hit;
        }
    }
}

