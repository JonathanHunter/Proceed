using Assets.Scripts.Util;

namespace Assets.Scripts.Player
{
    /* This file controls all of the transitions between states*/
    class PlayerStateMachine
    {
        private delegate Enums.PlayerState machine(bool inAir, bool move, bool hit, bool animDone);//function pointer
        private machine[] getNextState;//array of function pointers
        private Enums.PlayerState currState;
        private static float hold = 0;//used for delays
        private static bool die = false;

        public PlayerStateMachine()
        {
            currState = Enums.PlayerState.Idle;
            //fill array with functions
            getNextState = new machine[] { Idle, Moving, InAir, Jump, Attack, Hit, Dead };
        }

        public Enums.PlayerState update(bool inAir, bool move, bool hit, bool animDone)
        {
            currState = getNextState[((int)currState)](inAir, move, hit, animDone);//gets te next Enums.PlayerState
            return currState;
        }


        //The following methods control when and how you can transition between states

        private static Enums.PlayerState Idle(bool inAir, bool move, bool hit, bool animDone)
        {
            if (hit)
                return Enums.PlayerState.Hit;
            if (move)
                return Enums.PlayerState.Moving;
            if (inAir)
                return Enums.PlayerState.InAir;
            if (CustomInput.BoolFreshPress(CustomInput.UserInput.Jump))
                return Enums.PlayerState.Jump;
            if (CustomInput.BoolFreshPress(CustomInput.UserInput.Attack))
                return Enums.PlayerState.Attack;
            return Enums.PlayerState.Idle;
        }
        private static Enums.PlayerState Moving(bool inAir, bool move, bool hit, bool animDone)
        {
            if (hit)
                return Enums.PlayerState.Hit;
            if (move)
            {
                if (inAir)
                    return Enums.PlayerState.InAir;
                if (CustomInput.BoolFreshPress(CustomInput.UserInput.Jump))
                    return Enums.PlayerState.Jump;
                if (CustomInput.BoolFreshPress(CustomInput.UserInput.Attack))
                    return Enums.PlayerState.Attack;
                return Enums.PlayerState.Moving;
            }
            return Enums.PlayerState.Idle;
        }
        private static Enums.PlayerState InAir(bool inAir, bool move, bool hit, bool animDone)
        {
            if (hit)
                return Enums.PlayerState.Hit;
            if (!inAir)
            {
                if (CustomInput.BoolFreshPress(CustomInput.UserInput.Jump))
                    return Enums.PlayerState.Jump;
                if (move)
                    return Enums.PlayerState.Moving;
                if (CustomInput.BoolFreshPress(CustomInput.UserInput.Attack))
                    return Enums.PlayerState.Attack;
                return Enums.PlayerState.Idle;
            }
            return Enums.PlayerState.InAir;
        }
        private static Enums.PlayerState Jump(bool inAir, bool move, bool hit, bool animDone)
        {
            if (hit)
                return Enums.PlayerState.Hit;
            if (animDone)
                return Enums.PlayerState.InAir;
            if (!inAir)
                return Enums.PlayerState.Idle;
            return Enums.PlayerState.Jump;
        }
        private static Enums.PlayerState Attack(bool inAir, bool move, bool hit, bool animDone)
        {
            if (hit)
                return Enums.PlayerState.Hit;
            if (animDone)
                return Enums.PlayerState.Idle;
            return Enums.PlayerState.Attack;
        }

        private static Enums.PlayerState Hit(bool inAir, bool move, bool hit, bool animDone)
        {
            hold += UnityEngine.Time.deltaTime;
            if (hold > .4f)
            {
                hold = 0;
                if (die)
                    return Enums.PlayerState.Dead;
                return Enums.PlayerState.Idle;
            }
            return Enums.PlayerState.Hit;
        }

        //this is used to prevent the player character from doing any thing while dead
        private static Enums.PlayerState Dead(bool inAir, bool move, bool hit, bool animDone)
        {
            return Enums.PlayerState.Dead;
        }

        internal void Die()
        {
            die = true;
        }

        internal void Revive()
        {
            currState = Enums.PlayerState.Idle;
            die = false;
        }
    }
}
