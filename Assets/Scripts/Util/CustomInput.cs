using UnityEngine;

namespace Assets.Scripts.Util
{
    class CustomInput : MonoBehaviour
    {
        /// <summary> This is used to define user inputs, changed to add or remove buttons. </summary>
        public enum UserInput { Up, Down, Left, Right, Attack, Jump, Pause, Accept, Cancel }

        /// <summary> This is used to define the default keybindings. </summary>
        public static void DefaultKey()
        {
            if (keyBoard == null)
                throw new System.AccessViolationException(UnitializedMessage);
            keyBoard[(int)UserInput.Up] = KeyCode.W;
            keyBoard[(int)UserInput.Down] = KeyCode.S;
            keyBoard[(int)UserInput.Left] = KeyCode.A;
            keyBoard[(int)UserInput.Right] = KeyCode.D;
            keyBoard[(int)UserInput.Attack] = KeyCode.K;
            keyBoard[(int)UserInput.Jump] = KeyCode.J;
            keyBoard[(int)UserInput.Pause] = KeyCode.Escape;
            keyBoard[(int)UserInput.Accept] = KeyCode.K;
            keyBoard[(int)UserInput.Cancel] = KeyCode.J;
        }

        /// <summary> This is used to define the default controller bindings. </summary>
        public static void DefaultPad()
        {
            if (gamePad == null)
                throw new System.AccessViolationException(UnitializedMessage);
            gamePad[(int)UserInput.Up] = LEFT_STICK_UP;
            gamePad[(int)UserInput.Down] = LEFT_STICK_DOWN;
            gamePad[(int)UserInput.Left] = LEFT_STICK_LEFT;
            gamePad[(int)UserInput.Right] = LEFT_STICK_RIGHT;
            gamePad[(int)UserInput.Attack] = A;
            gamePad[(int)UserInput.Jump] = B;
            gamePad[(int)UserInput.Pause] = START;
            gamePad[(int)UserInput.Accept] = A;
            gamePad[(int)UserInput.Cancel] = B;
        }

        // Modification of the code below this should be unecessary.

        // Constants used to define the possible controller buttons.
        public const string LEFT_STICK_RIGHT = "Left Stick Right";
        public const string LEFT_STICK_LEFT = "Left Stick Left";
        public const string LEFT_STICK_UP = "Left Stick Up";
        public const string LEFT_STICK_DOWN = "Left Stick Down";
        public const string RIGHT_STICK_RIGHT = "Right Stick Right";
        public const string RIGHT_STICK_LEFT = "Right Stick Left";
        public const string RIGHT_STICK_UP = "Right Stick Up";
        public const string RIGHT_STICK_DOWN = "Right Stick Down";
        public const string DPAD_RIGHT = "Dpad Right";
        public const string DPAD_LEFT = "Dpad Left";
        public const string DPAD_UP = "Dpad Up";
        public const string DPAD_DOWN = "Dpad Down";
        public const string LEFT_TRIGGER = "Left Trigger";
        public const string RIGHT_TRIGGER = "Right Trigger";
        public const string A = "A";
        public const string B = "B";
        public const string X = "X";
        public const string Y = "Y";
        public const string LB = "LB";
        public const string RB = "RB";
        public const string BACK = "Back";
        public const string START = "Start";
        public const string LEFT_STICK = "Left Stick Click";
        public const string RIGHT_STICK = "Right Stick Click";

        private const string UnitializedMessage = "Input has not been initialized.Make sure it is in the scene.";

        // Arrays used to store input booleans.
        private static bool[] bools;
        private static bool[] boolsUp;
        private static bool[] boolsHeld;
        private static bool[] boolsFreshPress;
        private static bool[] boolsFreshPressAccessed;
        private static bool[] boolsFreshPressDeleteOnRead;

        // Arrays used to store raw input data for analog input.
        private static float[] raws;
        private static float[] rawsUp;
        private static float[] rawsHeld;
        private static float[] rawsFreshPress;
        private static bool[] rawsFreshPressAccessed;
        private static float[] rawsFreshPressDeleteOnRead;

        /// <summary> Getter for if a button is pressed. </summary>
        /// <param name="input"> The button to check. </param>
        /// <returns> True as long as the button is held. </returns>
        public static bool Bool(UserInput input)
        {
            if (bools == null)
                throw new System.AccessViolationException(UnitializedMessage);
            return bools[(int)input];
        }

        /// <summary> Getter for if a button has been released. </summary>
        /// <param name="input"> The button to check. </param>
        /// <returns> True for one frame after button is let go. returns>
        public static bool BoolUp(UserInput input)
        {
            if (boolsUp == null)
                throw new System.AccessViolationException(UnitializedMessage);
            return boolsUp[(int)input];
        }

        /// <summary> Getter for if a button is held. </summary>
        /// <param name="input"> The button to check. </param>
        /// <returns> True until the button is let go.  </returns>
        public static bool BoolHeld(UserInput input)
        {
            if (boolsHeld == null)
                throw new System.AccessViolationException(UnitializedMessage);
            return boolsHeld[(int)input];
        }

        /// <summary> Getter for if a button has been pressed. </summary>
        /// <param name="input"> The button to check. </param>
        /// <returns> True as until the end of the frame after the data is read or the key is released. </returns>
        public static bool BoolFreshPress(UserInput input)
        {
            if (boolsFreshPress == null)
                throw new System.AccessViolationException(UnitializedMessage);
            boolsFreshPressAccessed[(int)input] = true;
            return boolsFreshPress[(int)input];
        }

        /// <summary> Getter for if a button has been pressed. </summary>
        /// <param name="input"> The button to check. </param>
        /// <returns> True as until the data is read or the key is released. </returns>
        public static bool BoolFreshPressDeleteOnRead(UserInput input)
        {
            if (boolsFreshPressDeleteOnRead == null)
                throw new System.AccessViolationException(UnitializedMessage);
            bool temp = boolsFreshPressDeleteOnRead[(int)input];
            boolsFreshPressDeleteOnRead[(int)input] = false;
            return temp;
        }

        /// <summary> Getter for if a button is pressed. </summary>
        /// <param name="input"> The button to check. </param>
        /// <returns> A non-zero value as long as the button is held. </returns>
        public static float Raw(UserInput input)
        {
            if (raws == null)
                throw new System.AccessViolationException(UnitializedMessage);
            return raws[(int)input];
        }

        /// <summary> Getter for if a button has been released. </summary>
        /// <param name="input"> The button to check. </param>
        /// <returns> A non-zero value for one frame after button is let go. returns>
        public static float RawUp(UserInput input)
        {
            if (rawsUp == null)
                throw new System.AccessViolationException(UnitializedMessage);
            return rawsUp[(int)input];
        }

        /// <summary> Getter for if a button is held. </summary>
        /// <param name="input"> The button to check. </param>
        /// <returns> A non-zero value until the button is let go.  </returns>
        public static float RawHeld(UserInput input)
        {
            if (rawsHeld == null)
                throw new System.AccessViolationException(UnitializedMessage);
            return rawsHeld[(int)input];
        }

        /// <summary> Getter for if a button has been pressed. </summary>
        /// <param name="input"> The button to check. </param>
        /// <returns> A non-zero value as until the end of the frame after the data is read or the key is released. </returns>
        public static float RawFreshPress(UserInput input)
        {
            if (rawsFreshPress == null)
                throw new System.AccessViolationException(UnitializedMessage);
            rawsFreshPressAccessed[(int)input] = true;
            return rawsFreshPress[(int)input];
        }

        /// <summary> Getter for if a button has been pressed. </summary>
        /// <param name="input"> The button to check. </param>
        /// <returns> A non-zero value as until the data is read or the key is released. </returns>
        public static float RawFreshPressDeleteOnRead(UserInput input)
        {
            if (rawsFreshPressDeleteOnRead == null)
                throw new System.AccessViolationException(UnitializedMessage);
            float temp = rawsFreshPressDeleteOnRead[(int)input];
            rawsFreshPressDeleteOnRead[(int)input] = 0;
            return temp;
        }

        // Array to hold which keys correspond to which inputs.
        private static KeyCode[] keyBoard;

        /// <summary> Getter for the keys attached to inputs. </summary>
        /// <param name="input"> The key to get. </param>
        /// <returns> The keycode corresponding to that input. </returns>
        public static KeyCode keyBoardKey(UserInput input)
        {
            if (keyBoard == null)
                throw new System.AccessViolationException(UnitializedMessage);
            return keyBoard[(int)input];
        }

        /// <summary> Setter used to define which keys correspond to which inputs. </summary>
        /// <param name="input"> The button to define. </param>
        /// <param name="key"> The key to attach to it. </param>
        public static void setKeyBoardKey(UserInput input, KeyCode key)
        {
            if (keyBoard == null)
                throw new System.AccessViolationException(UnitializedMessage);
            keyBoard[(int)input] = key;
        }

        // Array to hold which buttons correspond to which inputs.
        private static string[] gamePad;

        /// <summary> Getter for the buttons attached to inputs. </summary>
        /// <param name="input"> The button to get. </param>
        /// <returns> The string corresponding to that input. </returns>
        public static string gamePadButton(UserInput input)
        {
            if (gamePad == null)
                throw new System.AccessViolationException(UnitializedMessage);
            return gamePad[(int)input];
        }
        /// <summary> Setter used to define which buttons correspond to which inputs. </summary>
        /// <param name="input"> The button to define. </param>
        /// <param name="button"> The button to attach to it. </param>
        public static void setGamePadButton(UserInput input, string button)
        {
            if (gamePad == null)
                throw new System.AccessViolationException(UnitializedMessage);
            gamePad[(int)input] = button;
        }

        // Boolean as to whether or not a controller is being used.
        private static bool usingPad = false;

        /// <summary> Is the player using a controller. </summary>
        public static bool UsingPad
        {
            get { return usingPad; }
        }

        void Awake()
        {
            bools = new bool[System.Enum.GetNames(typeof(UserInput)).Length];
            boolsUp = new bool[System.Enum.GetNames(typeof(UserInput)).Length];
            boolsHeld = new bool[System.Enum.GetNames(typeof(UserInput)).Length];
            boolsFreshPress = new bool[System.Enum.GetNames(typeof(UserInput)).Length];
            boolsFreshPressAccessed = new bool[System.Enum.GetNames(typeof(UserInput)).Length];
            boolsFreshPressDeleteOnRead = new bool[System.Enum.GetNames(typeof(UserInput)).Length];

            raws = new float[System.Enum.GetNames(typeof(UserInput)).Length];
            rawsUp = new float[System.Enum.GetNames(typeof(UserInput)).Length];
            rawsHeld = new float[System.Enum.GetNames(typeof(UserInput)).Length];
            rawsFreshPress = new float[System.Enum.GetNames(typeof(UserInput)).Length];
            rawsFreshPressAccessed = new bool[System.Enum.GetNames(typeof(UserInput)).Length];
            rawsFreshPressDeleteOnRead = new float[System.Enum.GetNames(typeof(UserInput)).Length];

            keyBoard = new KeyCode[System.Enum.GetNames(typeof(UserInput)).Length];
            gamePad = new string[System.Enum.GetNames(typeof(UserInput)).Length];

            Default();
        }

        void Update()
        {
            if (Input.anyKey)
                usingPad = false;
            if (AnyPadInput())
                usingPad = true;
            if (!usingPad)
            {
                for (int i = 0; i < keyBoard.Length; i++)
                    updateKey(i);
            }
            else
            {
                for (int i = 0; i < gamePad.Length; i++)
                    updatePad(i);
            }
        }

        /// <summary> Updates all the values for a specific input based on the keyboard. </summary>
        /// <param name="input"> The input to update. </param>
        private void updateKey(int input)
        {
            bool key = false, keyUp = false;
            if (Input.GetKeyDown(keyBoard[input]))
                key = true;
            else if (Input.GetKeyUp(keyBoard[input]))
                keyUp = true;

            if (boolsFreshPressAccessed[input])
            {
                boolsFreshPressAccessed[input] = false;
                boolsFreshPress[input] = false;
                boolsFreshPressDeleteOnRead[input] = false;
            }
            if (bools[input] && key)
            {
                boolsFreshPress[input] = true;
                boolsFreshPressDeleteOnRead[input] = true;
            }
            if (key)
            {
                bools[input] = true;
                boolsHeld[input] = true;
                boolsUp[input] = false;
            }
            else if (keyUp)
            {
                bools[input] = false;
                boolsHeld[input] = false;
                boolsFreshPress[input] = false;
                boolsFreshPressDeleteOnRead[input] = false;
                boolsFreshPressAccessed[input] = false;
                boolsUp[input] = true;
            }
            else
                boolsUp[input] = false;

            if (rawsFreshPressAccessed[input])
            {
                rawsFreshPressAccessed[input] = false;
                rawsFreshPress[input] = 0f;
                rawsFreshPressDeleteOnRead[input] = 0f;
            }
            if (raws[input] != 0 && key)
            {
                rawsFreshPress[input] = 1f;
                rawsFreshPressDeleteOnRead[input] = 1f;
            }
            if (key)
            {
                raws[input] = 1f;
                rawsHeld[input] = 1f;
                rawsUp[input] = 0f;
            }
            else if (keyUp)
            {
                raws[input] = 0f;
                rawsHeld[input] = 0f;
                rawsFreshPress[input] = 0f;
                rawsFreshPressDeleteOnRead[input] = 0f;
                rawsFreshPressAccessed[input] = false;
                rawsUp[input] = 1f;
            }
            else
                rawsUp[input] = 0f;
        }
        private static void updatePad(int state, string axes)
        {
            float input = Input.GetAxis(axes);
            bool key = false, keyUp = false;
            if (axes == LEFT_STICK_LEFT || axes == LEFT_STICK_UP || axes == RIGHT_STICK_LEFT || axes == RIGHT_STICK_UP || axes == DPAD_LEFT || axes == DPAD_DOWN || axes == RIGHT_TRIGGER)
            {
                if (input < 0)
                    key = true;
                else if ((bools & state) != 0)
                    keyUp = true;
            }
            else if (input > 0)
                key = true;
            else if ((bools & state) != 0)
                keyUp = true;

            if ((boolsFreshPressAccessed & state) != 0)
            {
                boolsFreshPressAccessed = (boolsFreshPressAccessed & ~state);
                boolsFreshPress = (boolsFreshPress & ~state);
                boolsFreshPressDeleteOnRead = (boolsFreshPressDeleteOnRead & ~state);
            }
            if (((bools & state) == 0) && key)
            {
                boolsFreshPress = boolsFreshPress | state;
                boolsFreshPressDeleteOnRead = (boolsFreshPressDeleteOnRead | state);
            }
            if (key)
            {
                bools = bools | state;
                boolsHeld = boolsHeld | state;
                boolsUp = boolsUp & ~state;
            }
            else if (keyUp)
            {
                bools = bools & ~state;
                boolsHeld = boolsHeld & ~state;
                boolsFreshPress = boolsFreshPress & ~state;
                boolsFreshPressDeleteOnRead = (boolsFreshPressDeleteOnRead & ~state);
                boolsUp = boolsUp | state;
                boolsFreshPressAccessed = (boolsFreshPressAccessed & ~state);
            }
            else
                boolsUp = boolsUp & ~state;
        }

        public static void Default()
        {
            DefaultKey();
            DefaultPad();
        }

        public static bool AnyInput()
        {
            return bools != 0 || boolsFreshPress != 0 || boolsHeld != 0 || boolsUp != 0;
        }

        public static bool AnyPadInput()
        {
            if (UserInput.GetAxis(LEFT_STICK_RIGHT) != 0)
                return true;
            if (UserInput.GetAxis(LEFT_STICK_UP) != 0)
                return true;
            if (UserInput.GetAxis(RIGHT_STICK_RIGHT) != 0)
                return true;
            if (UserInput.GetAxis(RIGHT_STICK_UP) != 0)
                return true;
            if (UserInput.GetAxis(DPAD_RIGHT) != 0)
                return true;
            if (UserInput.GetAxis(DPAD_UP) != 0)
                return true;
            if (UserInput.GetAxis(LEFT_TRIGGER) != 0)
                return true;
            if (UserInput.GetAxis(RIGHT_TRIGGER) != 0)
                return true;
            if (UserInput.GetAxis(A) != 0)
                return true;
            if (UserInput.GetAxis(B) != 0)
                return true;
            if (UserInput.GetAxis(X) != 0)
                return true;
            if (UserInput.GetAxis(Y) != 0)
                return true;
            if (UserInput.GetAxis(LB) != 0)
                return true;
            if (UserInput.GetAxis(RB) != 0)
                return true;
            if (UserInput.GetAxis(BACK) != 0)
                return true;
            if (UserInput.GetAxis(START) != 0)
                return true;
            if (UserInput.GetAxis(LEFT_STICK) != 0)
                return true;
            if (UserInput.GetAxis(RIGHT_STICK) != 0)
                return true;
            return false;
        }
    }
}
