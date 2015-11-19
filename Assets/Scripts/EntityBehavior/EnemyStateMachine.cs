//Proceed: Jonathan Hunter, Larry Smith, Justin Coates, Chris Tansey
using UnityEngine;

namespace Assets.Scripts.EntityBehavior
{
    class EnemyStateMachine
    {
        public enum State
        {
            Wait = 0, Patrol, Flee, Chase, Tired, Attack, Hit
        };

        private State currState;
        private double hold;
        private int moveCount;
        private float aggresionRadius, fleeDistance, timeCanChase, waitTime, restTime;
        private bool coward, fearless;

        public EnemyStateMachine(float aggresionRadius, float fleeDistance, float timeCanChase, float waitTime, float restTime, bool coward, bool fearless)
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
            Random.seed = System.DateTime.Today.Millisecond;
        }

        public int Run(bool animDone, bool playerSpotted, bool playerDead, bool hpLow, bool infrontOfPlayer, float distance, bool hit)
        {
            switch (currState)
            {
                case State.Wait: currState = Wait(hit, playerSpotted, distance); break;
                case State.Patrol: currState = Patrol(playerSpotted, distance, hit); break;
                case State.Flee: currState = Flee(playerDead, distance, hit, hpLow); break;
                case State.Chase: currState = Chase(playerDead, infrontOfPlayer, distance, hpLow, hit); break;
                case State.Tired: currState = Tired(hit); break;
                case State.Attack: currState = Attack(animDone, playerDead, infrontOfPlayer, hpLow, hit); break;
                case State.Hit: currState = Hit(animDone, hpLow); break;
            }
            return (int)currState;
        }

        private State Wait(bool hit, bool playerSpotted, float distance)
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
                return State.Patrol;
            }
            if ((playerSpotted && distance < aggresionRadius) || (distance < aggresionRadius / 2f))
            {
                if (coward)
                    return State.Flee;
                return State.Chase;
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
            float r = Random.Range(0f, 1f);
            if (r < .01f)
                return State.Wait;
            return State.Patrol;
        }

        private State Flee(bool playerDead, float distance, bool hit, bool hpLow)
        {
            if (hit)
                return State.Hit;
            if (distance > fleeDistance || playerDead)
                return State.Patrol;
            if (hpLow && fearless)
                return State.Chase;
            return State.Flee;
        }

        private State Chase(bool playerDead, bool infrontOfPlayer, float distance, bool hpLow, bool hit)
        {
            if (hit)
            {
                hold = 0;
                return State.Hit;
            }
            if (hpLow && !fearless)
            {
                hold = 0;
                return State.Flee;
            }
            if (distance > fleeDistance || playerDead)
            {
                hold = 0;
                return State.Patrol;
            }
            if (infrontOfPlayer)
            {
                hold = 0;
                return State.Attack;
            }
            hold += Time.deltaTime;
            if (hold > timeCanChase)
            {
                hold = 0;
                return State.Tired;
            }
            return State.Chase;
        }

        private State Tired(bool hit)
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
                return State.Chase;
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

        private State Hit(bool animDone, bool hpLow)
        {
            if (animDone)
            {
                if (coward || (hpLow && !fearless))
                    return State.Flee;
                return State.Chase;
            }
            return State.Hit;
        }
    }
}
