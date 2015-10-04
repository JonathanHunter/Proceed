namespace Assets.Scripts.Util{
using UnityEngine;
using System.Collections;

    public class PlayerController : MonoBehaviour {

        Animator animControl;

        void Awake()
        {
            animControl = gameObject.GetComponent<Animator>();
        }
        
        // Input Handling
        // Button Press:
            //(Fresh Press + Held)->(Release)->(Up)
            
        void PlayerInput()
        {
            #region Directional Inputs
            // Check Camera to Player angle difference
            // Convert Direction input to GameWorldDirection
            #endregion


            #region Action Inputs
            // Attack, Jump
            #endregion

            #region Menu Inputs
            // Pause, Accept, Cancel
            #endregion

        }


        // Animation Communication
        void UpdateAnimationCtrl()
        {
            //animControl.SetInteger("Horizontal", true);

        }
    }

}
