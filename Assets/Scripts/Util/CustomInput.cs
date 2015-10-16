using UnityEngine;

namespace Assets.Scripts.Util
{
    class CustomInput : MonoBehaviour
    {
        /// <summary> This is used to define user inputs, changed to add or remove buttons. </summary>
        public enum UserInput { Up, Down, Left, Right, Attack, Jump, Pause, Accept, Cancel, TurnCameraLeft, TurnCameraRight }

        /// <summary> This is used to define whether to return a positive or negative value for a specfic raw input. </summary>
        public static void RawSign()
        {
            if (rawSign == null)
                throw new System.AccessViolationException(UnitializedMessage);
            rawSign[(int)UserInput.Up] = 1;
            rawSign[(int)UserInput.Down] = -1;
            rawSign[(int)UserInput.Left] = -1;
            rawSign[(int)UserInput.Right] = 1;
            rawSign[(int)UserInput.Attack] = 1;
            rawSign[(int)UserInput.Jump] = 1;
            rawSign[(int)UserInput.Pause] = 1;
            rawSign[(int)UserInput.Accept] = 1;
            rawSign[(int)UserInput.Cancel] = 1;
            rawSign[(int)UserInput.TurnCameraLeft] = -1;
            rawSign[(int)UserInput.TurnCameraRight] = 1;
        }

        /// <summary> 
        /// This is used to define the default keybindings. 
        /// Syntax: keyBoard[INPUT, PLAYER_NUM] = KEYCODE;
        /// PLAYER_NUM is any number from 0 - 6, where 0 represents all controllers and 1-6 represents their respective player.
        ///  </summary>
        public static void DefaultKey()
        {
            if (keyBoard == null)
                throw new System.AccessViolationException(UnitializedMessage);
            keyBoard[(int)UserInput.Up, 0] = KeyCode.W;
            keyBoard[(int)UserInput.Down, 0] = KeyCode.S;
            keyBoard[(int)UserInput.Left, 0] = KeyCode.A;
            keyBoard[(int)UserInput.Right, 0] = KeyCode.D;
            keyBoard[(int)UserInput.Attack, 0] = KeyCode.K;
            keyBoard[(int)UserInput.Jump, 0] = KeyCode.J;
            keyBoard[(int)UserInput.Pause, 0] = KeyCode.Escape;
            keyBoard[(int)UserInput.Accept, 0] = KeyCode.K;
            keyBoard[(int)UserInput.Cancel, 0] = KeyCode.J;
            keyBoard[(int)UserInput.TurnCameraLeft, 0] = KeyCode.Q;
            keyBoard[(int)UserInput.TurnCameraRight, 0] = KeyCode.E;
        }

        /// <summary> 
        /// This is used to define the default controller bindings.
        /// gamePad[INPUT, PLAYER_NUM] = ONE OF THE STRING CONSTANTS BELOW;
        /// PLAYER_NUM is any number from 0 - 6, where 0 represents all controllers and 1-6 represents their respective player. 
        /// </summary>
        public static void DefaultPad()
        {
            if (gamePad == null)
                throw new System.AccessViolationException(UnitializedMessage);
            gamePad[(int)UserInput.Up, 0] = LEFT_STICK_UP;
            gamePad[(int)UserInput.Down, 0] = LEFT_STICK_DOWN;
            gamePad[(int)UserInput.Left, 0] = LEFT_STICK_LEFT;
            gamePad[(int)UserInput.Right, 0] = LEFT_STICK_RIGHT;
            gamePad[(int)UserInput.Attack, 0] = A;
            gamePad[(int)UserInput.Jump, 0] = B;
            gamePad[(int)UserInput.Pause, 0] = START;
            gamePad[(int)UserInput.Accept, 0] = A;
            gamePad[(int)UserInput.Cancel, 0] = B;
            gamePad[(int)UserInput.TurnCameraLeft, 0] = RIGHT_STICK_LEFT;
            gamePad[(int)UserInput.TurnCameraRight, 0] = RIGHT_STICK_RIGHT;
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
        private static bool[,] bools;
        private static bool[,] boolsUp;
        private static bool[,] boolsHeld;
        private static bool[,] boolsFreshPress;
        private static bool[,] boolsFreshPressAccessed;
        private static bool[,] boolsFreshPressDeleteOnRead;

        // Arrays used to store raw input data for analog input.
        private static float[,] raws;
        private static float[,] rawsUp;
        private static float[,] rawsHeld;
        private static float[,] rawsFreshPress;
        private static bool[,] rawsFreshPressAccessed;
        private static float[,] rawsFreshPressDeleteOnRead;

        /// <summary> Getter for if a button is pressed. </summary>
        /// <param name="input"> The button to check. </param>
        /// <returns> True as long as the button is held. </returns>
        public static bool Bool(UserInput input, int playerNumber = 0)
        {
            if (bools == null)
                throw new System.AccessViolationException(UnitializedMessage);
            return bools[(int)input, playerNumber];
        }

        /// <summary> Getter for if a button has been released. </summary>
        /// <param name="input"> The button to check. </param>
        /// <returns> True for one frame after button is let go. returns>
        public static bool BoolUp(UserInput input, int playerNumber = 0)
        {
            if (boolsUp == null)
                throw new System.AccessViolationException(UnitializedMessage);
            return boolsUp[(int)input, playerNumber];
        }

        /// <summary> Getter for if a button is held. </summary>
        /// <param name="input"> The button to check. </param>
        /// <returns> True until the button is let go.  </returns>
        public static bool BoolHeld(UserInput input, int playerNumber = 0)
        {
            if (boolsHeld == null)
                throw new System.AccessViolationException(UnitializedMessage);
            return boolsHeld[(int)input, playerNumber];
        }

        /// <summary> Getter for if a button has been pressed. </summary>
        /// <param name="input"> The button to check. </param>
        /// <returns> True as until the end of the frame after the data is read or the key is released. </returns>
        public static bool BoolFreshPress(UserInput input, int playerNumber = 0)
        {
            if (boolsFreshPress == null)
                throw new System.AccessViolationException(UnitializedMessage);
            boolsFreshPressAccessed[(int)input, playerNumber] = true;
            return boolsFreshPress[(int)input, playerNumber];
        }

        /// <summary> Getter for if a button has been pressed. </summary>
        /// <param name="input"> The button to check. </param>
        /// <returns> True as until the data is read or the key is released. </returns>
        public static bool BoolFreshPressDeleteOnRead(UserInput input, int playerNumber = 0)
        {
            if (boolsFreshPressDeleteOnRead == null)
                throw new System.AccessViolationException(UnitializedMessage);
            bool temp = boolsFreshPressDeleteOnRead[(int)input, playerNumber];
            boolsFreshPressDeleteOnRead[(int)input, playerNumber] = false;
            return temp;
        }

        /// <summary> Getter for if a button is pressed. </summary>
        /// <param name="input"> The button to check. </param>
        /// <returns> A non-zero value as long as the button is held. </returns>
        public static float Raw(UserInput input, int playerNumber = 0)
        {
            if (raws == null)
                throw new System.AccessViolationException(UnitializedMessage);
            return raws[(int)input, playerNumber];
        }

        /// <summary> Getter for if a button has been released. </summary>
        /// <param name="input"> The button to check. </param>
        /// <returns> A non-zero value for one frame after button is let go. returns>
        public static float RawUp(UserInput input, int playerNumber = 0)
        {
            if (rawsUp == null)
                throw new System.AccessViolationException(UnitializedMessage);
            return rawsUp[(int)input, playerNumber];
        }

        /// <summary> Getter for if a button is held. </summary>
        /// <param name="input"> The button to check. </param>
        /// <returns> A non-zero value until the button is let go.  </returns>
        public static float RawHeld(UserInput input, int playerNumber = 0)
        {
            if (rawsHeld == null)
                throw new System.AccessViolationException(UnitializedMessage);
            return rawsHeld[(int)input, playerNumber];
        }

        /// <summary> Getter for if a button has been pressed. </summary>
        /// <param name="input"> The button to check. </param>
        /// <returns> A non-zero value as until the end of the frame after the data is read or the key is released. </returns>
        public static float RawFreshPress(UserInput input, int playerNumber = 0)
        {
            if (rawsFreshPress == null)
                throw new System.AccessViolationException(UnitializedMessage);
            rawsFreshPressAccessed[(int)input, playerNumber] = true;
            return rawsFreshPress[(int)input, playerNumber];
        }

        /// <summary> Getter for if a button has been pressed. </summary>
        /// <param name="input"> The button to check. </param>
        /// <returns> A non-zero value as until the data is read or the key is released. </returns>
        public static float RawFreshPressDeleteOnRead(UserInput input, int playerNumber = 0)
        {
            if (rawsFreshPressDeleteOnRead == null)
                throw new System.AccessViolationException(UnitializedMessage);
            float temp = rawsFreshPressDeleteOnRead[(int)input, playerNumber];
            rawsFreshPressDeleteOnRead[(int)input, playerNumber] = 0;
            return temp;
        }

        // Array to hold which keys correspond to which inputs.
        private static KeyCode[,] keyBoard;

        /// <summary> Getter for the keys attached to inputs. </summary>
        /// <param name="input"> The key to get. </param>
        /// <returns> The keycode corresponding to that input. </returns>
        public static KeyCode keyBoardKey(UserInput input, int playerNumber = 0)
        {
            if (keyBoard == null)
                throw new System.AccessViolationException(UnitializedMessage);
            return keyBoard[(int)input, playerNumber];
        }

        /// <summary> Setter used to define which keys correspond to which inputs. </summary>
        /// <param name="input"> The button to define. </param>
        /// <param name="key"> The key to attach to it. </param>
        public static void setKeyBoardKey(UserInput input, KeyCode key, int playerNumber = 0)
        {
            if (keyBoard == null)
                throw new System.AccessViolationException(UnitializedMessage);
            keyBoard[(int)input, playerNumber] = key;
        }

        // Array to hold which buttons correspond to which inputs.
        private static string[,] gamePad;

        /// <summary> Getter for the buttons attached to inputs. </summary>
        /// <param name="input"> The button to get. </param>
        /// <returns> The string corresponding to that input. </returns>
        public static string gamePadButton(UserInput input, int playerNumber = 0)
        {
            if (gamePad == null)
                throw new System.AccessViolationException(UnitializedMessage);
            return gamePad[(int)input, playerNumber];
        }
        /// <summary> Setter used to define which buttons correspond to which inputs. </summary>
        /// <param name="input"> The button to define. </param>
        /// <param name="button"> The button to attach to it. </param>
        public static void setGamePadButton(UserInput input, string button, int playerNumber = 0)
        {
            if (gamePad == null)
                throw new System.AccessViolationException(UnitializedMessage);
            gamePad[(int)input, playerNumber] = button;
        }

        // Array to for the user to specify the sign of the number they want from raw data
        private static int[] rawSign;

        // Boolean as to whether or not a controller is being used.
        private static bool usingPad = false;

        /// <summary> Is the player using a controller. </summary>
        public static bool UsingPad
        {
            get { return usingPad; }
        }

        void Awake()
        {
            bools = new bool[System.Enum.GetNames(typeof(UserInput)).Length, 7];
            boolsUp = new bool[System.Enum.GetNames(typeof(UserInput)).Length, 7];
            boolsHeld = new bool[System.Enum.GetNames(typeof(UserInput)).Length, 7];
            boolsFreshPress = new bool[System.Enum.GetNames(typeof(UserInput)).Length, 7];
            boolsFreshPressAccessed = new bool[System.Enum.GetNames(typeof(UserInput)).Length, 7];
            boolsFreshPressDeleteOnRead = new bool[System.Enum.GetNames(typeof(UserInput)).Length, 7];

            raws = new float[System.Enum.GetNames(typeof(UserInput)).Length, 7];
            rawsUp = new float[System.Enum.GetNames(typeof(UserInput)).Length, 7];
            rawsHeld = new float[System.Enum.GetNames(typeof(UserInput)).Length, 7];
            rawsFreshPress = new float[System.Enum.GetNames(typeof(UserInput)).Length, 7];
            rawsFreshPressAccessed = new bool[System.Enum.GetNames(typeof(UserInput)).Length, 7];
            rawsFreshPressDeleteOnRead = new float[System.Enum.GetNames(typeof(UserInput)).Length, 7];

            keyBoard = new KeyCode[System.Enum.GetNames(typeof(UserInput)).Length, 7];
            gamePad = new string[System.Enum.GetNames(typeof(UserInput)).Length, 7];
            rawSign = new int[System.Enum.GetNames(typeof(UserInput)).Length];

            RawSign();

            Default();
        }

        /// <summary> Resets all the bindings to default. </summary>
        public static void Default()
        {
            DefaultKey();
            DefaultPad();
        }

        void Update()
        {
            if (Input.anyKey)
                usingPad = false;
            if (AnyPadInput())
                usingPad = true;
            if (!usingPad)
            {
                for (int i = 0; i < System.Enum.GetNames(typeof(UserInput)).Length; i++)
                {
                    for (int p = 0; p < 7; p++)
                    {
                        if (keyBoard[i, p] != KeyCode.None)
                            updateKey(i, p);
                    }
                }
            }
            else
            {
                for (int i = 0; i < System.Enum.GetNames(typeof(UserInput)).Length; i++)
                {
                    for (int p = 0; p < 7; p++)
                    {
                        if (gamePad[i, p] != null)
                            updatePad(i, p);
                    }
                }
            }
        }

        /// <summary> Used to see if the user has pressed any monitored input. </summary>
        /// <returns> True if any of the buttons have been pressed. </returns>
        public static bool AnyInput()
        {
            foreach (bool b in bools)
                if (b) return true;
            foreach (bool b in boolsUp)
                if (b) return true;
            foreach (bool b in boolsHeld)
                if (b) return true;
            foreach (bool b in boolsFreshPress)
                if (b) return true;
            foreach (bool b in boolsFreshPressDeleteOnRead)
                if (b) return true;
            return false;
        }

        /// <summary> Used to see if the user hit any button on the controller. </summary>
        /// <returns> True if the user hit any input on the controller. </returns>
        public static bool AnyPadInput()
        {
            if (ControllerInputHandler.GetAxis(ControllerInputHandler.Axis.LeftStickX) != 0)
                return true;
            if (ControllerInputHandler.GetAxis(ControllerInputHandler.Axis.LeftStickY) != 0)
                return true;
            if (ControllerInputHandler.GetAxis(ControllerInputHandler.Axis.RightStickX) != 0)
                return true;
            if (ControllerInputHandler.GetAxis(ControllerInputHandler.Axis.RightStickY) != 0)
                return true;
            if (ControllerInputHandler.GetAxis(ControllerInputHandler.Axis.DPadX) != 0)
                return true;
            if (ControllerInputHandler.GetAxis(ControllerInputHandler.Axis.DPadY) != 0)
                return true;
            if (ControllerInputHandler.GetTrigger(ControllerInputHandler.Triggers.LeftTrigger) != 0)
                return true;
            if (ControllerInputHandler.GetTrigger(ControllerInputHandler.Triggers.RightTrigger) != 0)
                return true;
            if (ControllerInputHandler.GetButton(ControllerInputHandler.Buttons.A))
                return true;
            if (ControllerInputHandler.GetButton(ControllerInputHandler.Buttons.B))
                return true;
            if (ControllerInputHandler.GetButton(ControllerInputHandler.Buttons.X))
                return true;
            if (ControllerInputHandler.GetButton(ControllerInputHandler.Buttons.Y))
                return true;
            if (ControllerInputHandler.GetButton(ControllerInputHandler.Buttons.LeftBumper))
                return true;
            if (ControllerInputHandler.GetButton(ControllerInputHandler.Buttons.RightBumper))
                return true;
            if (ControllerInputHandler.GetButton(ControllerInputHandler.Buttons.Back))
                return true;
            if (ControllerInputHandler.GetButton(ControllerInputHandler.Buttons.Start))
                return true;
            if (ControllerInputHandler.GetButton(ControllerInputHandler.Buttons.LeftStickClick))
                return true;
            if (ControllerInputHandler.GetButton(ControllerInputHandler.Buttons.RightStickClick))
                return true;
            return false;
        }

        /// <summary> Updates all the values for a specific input based on the keyboard. </summary>
        /// <param name="input"> The input to update. </param>
        private void updateKey(int input, int playerNumber)
        {
            bool key = false, keyUp = false;
            if (Input.GetKeyDown(keyBoard[input, playerNumber]))
                key = true;
            else if (Input.GetKeyUp(keyBoard[input, playerNumber]))
                keyUp = true;

            UpdateBools(key, keyUp, input, 1f);
        }

        /// <summary> Updates all the values for a specific input based on a controller. </summary>
        /// <param name="input"> The input to update. </param>
        private void updatePad(int input, int playerNumber)
        {
            switch (gamePad[input, playerNumber])
            {
                case LEFT_STICK_RIGHT: UpdateAxis(input, ControllerInputHandler.GetAxis(ControllerInputHandler.Axis.LeftStickX, playerNumber), playerNumber); break;
                case LEFT_STICK_LEFT: UpdateAxis(input, ControllerInputHandler.GetAxis(ControllerInputHandler.Axis.LeftStickX, playerNumber), playerNumber); break;
                case LEFT_STICK_UP: UpdateAxis(input, ControllerInputHandler.GetAxis(ControllerInputHandler.Axis.LeftStickY, playerNumber), playerNumber); break;
                case LEFT_STICK_DOWN: UpdateAxis(input, ControllerInputHandler.GetAxis(ControllerInputHandler.Axis.LeftStickY, playerNumber), playerNumber); break;
                case RIGHT_STICK_RIGHT: UpdateAxis(input, ControllerInputHandler.GetAxis(ControllerInputHandler.Axis.RightStickX, playerNumber), playerNumber); break;
                case RIGHT_STICK_LEFT: UpdateAxis(input, ControllerInputHandler.GetAxis(ControllerInputHandler.Axis.RightStickX, playerNumber), playerNumber); break;
                case RIGHT_STICK_UP: UpdateAxis(input, ControllerInputHandler.GetAxis(ControllerInputHandler.Axis.RightStickY, playerNumber), playerNumber); break;
                case RIGHT_STICK_DOWN: UpdateAxis(input, ControllerInputHandler.GetAxis(ControllerInputHandler.Axis.RightStickY, playerNumber), playerNumber); break;
                case DPAD_RIGHT: UpdateAxis(input, ControllerInputHandler.GetAxis(ControllerInputHandler.Axis.DPadX, playerNumber), playerNumber); break;
                case DPAD_LEFT: UpdateAxis(input, ControllerInputHandler.GetAxis(ControllerInputHandler.Axis.DPadX, playerNumber), playerNumber); break;
                case DPAD_UP: UpdateAxis(input, ControllerInputHandler.GetAxis(ControllerInputHandler.Axis.DPadY, playerNumber), playerNumber); break;
                case DPAD_DOWN: UpdateAxis(input, ControllerInputHandler.GetAxis(ControllerInputHandler.Axis.DPadY, playerNumber), playerNumber); break;
                case LEFT_TRIGGER: UpdateAxis(input, ControllerInputHandler.GetTrigger(ControllerInputHandler.Triggers.LeftTrigger, playerNumber), playerNumber); break;
                case RIGHT_TRIGGER: UpdateAxis(input, ControllerInputHandler.GetTrigger(ControllerInputHandler.Triggers.RightTrigger, playerNumber), playerNumber); break;
                default: UpdateButton(input, playerNumber); break;
            }
        }

        /// <summary> Update the buttons corresponding to axis. </summary>
        /// <param name="input"> The input to update. </param>
        /// <param name="data"> The data from the axis. </param>
        private void UpdateAxis(int input, float data, int playerNumber)
        {
            bool key = false, keyUp = false;

            if (gamePad[input, playerNumber] == LEFT_STICK_LEFT || gamePad[(int)input, playerNumber] == LEFT_STICK_DOWN || gamePad[(int)input, playerNumber] == RIGHT_STICK_LEFT ||
                gamePad[(int)input, playerNumber] == RIGHT_STICK_DOWN || gamePad[input, playerNumber] == DPAD_LEFT || gamePad[input, playerNumber] == DPAD_LEFT)
            {
                if (data < 0)
                    key = true;
                else if (bools[input, playerNumber])
                    keyUp = true;
            }
            else
            {
                if (data > 0)
                    key = true;
                else if (bools[input, playerNumber])
                    keyUp = true;
            }

            UpdateBools(key, keyUp, input, data);
        }

        /// <summary> Update the buttons corresponding to buttons. </summary>
        /// <param name="input"> The input to update. </param>
        private void UpdateButton(int input, int playerNumber)
        {
            bool key = false, keyUp = false;

            if (GetButton(gamePad[input, playerNumber]))
                key = true;
            else if (GetButtonUp(gamePad[input, playerNumber]))
                keyUp = true;

            UpdateBools(key, keyUp, input, 1f);
        }

        /// <summary> Input.GetKey for the specific controller button. </summary>
        /// <param name="button"> The specific controller button. </param>
        /// <returns> True if that button has been pressed. </returns>
        private bool GetButton(string button)
        {
            switch (button)
            {
                case A: return ControllerInputHandler.GetButton(ControllerInputHandler.Buttons.A);
                case B: return ControllerInputHandler.GetButton(ControllerInputHandler.Buttons.B);
                case X: return ControllerInputHandler.GetButton(ControllerInputHandler.Buttons.X);
                case Y: return ControllerInputHandler.GetButton(ControllerInputHandler.Buttons.Y);
                case RB: return ControllerInputHandler.GetButton(ControllerInputHandler.Buttons.RightBumper);
                case LB: return ControllerInputHandler.GetButton(ControllerInputHandler.Buttons.LeftBumper);
                case START: return ControllerInputHandler.GetButton(ControllerInputHandler.Buttons.Start);
                case BACK: return ControllerInputHandler.GetButton(ControllerInputHandler.Buttons.Back);
                case LEFT_STICK: return ControllerInputHandler.GetButton(ControllerInputHandler.Buttons.LeftStickClick);
                default: return ControllerInputHandler.GetButton(ControllerInputHandler.Buttons.RightStickClick);
            }
        }

        /// <summary> Input.GetKeyUp for the specific controller button. </summary>
        /// <param name="button"> The specific controller button. </param>
        /// <returns> True if that button has been released. </returns>
        private bool GetButtonUp(string button)
        {
            switch (button)
            {
                case A: return ControllerInputHandler.GetButtonUp(ControllerInputHandler.Buttons.A);
                case B: return ControllerInputHandler.GetButtonUp(ControllerInputHandler.Buttons.B);
                case X: return ControllerInputHandler.GetButtonUp(ControllerInputHandler.Buttons.X);
                case Y: return ControllerInputHandler.GetButtonUp(ControllerInputHandler.Buttons.Y);
                case RB: return ControllerInputHandler.GetButtonUp(ControllerInputHandler.Buttons.RightBumper);
                case LB: return ControllerInputHandler.GetButtonUp(ControllerInputHandler.Buttons.LeftBumper);
                case START: return ControllerInputHandler.GetButtonUp(ControllerInputHandler.Buttons.Start);
                case BACK: return ControllerInputHandler.GetButtonUp(ControllerInputHandler.Buttons.Back);
                case LEFT_STICK: return ControllerInputHandler.GetButtonUp(ControllerInputHandler.Buttons.LeftStickClick);
                default: return ControllerInputHandler.GetButtonUp(ControllerInputHandler.Buttons.RightStickClick);
            }
        }

        /// <summary> Actually does the updating of the bools. </summary>
        /// <param name="key"> Whether this input has been activated. </param>
        /// <param name="keyUp"> Whether this input has just been released. </param>
        /// <param name="input"> The input to update. </param>
        /// <param name="data"> The value for the raw data. </param>
        private void UpdateBools(bool key, bool keyUp, int input, float data, int playerNumber = 0)
        {
            if (boolsFreshPressAccessed[input, playerNumber])
            {
                boolsFreshPressAccessed[input, playerNumber] = false;
                boolsFreshPress[input, playerNumber] = false;
                boolsFreshPressDeleteOnRead[input, playerNumber] = false;
            }
            if (!bools[input, playerNumber] && key)
            {
                boolsFreshPress[input, playerNumber] = true;
                boolsFreshPressDeleteOnRead[input, playerNumber] = true;
            }
            if (key)
            {
                bools[input, playerNumber] = true;
                boolsHeld[input, playerNumber] = true;
                boolsUp[input, playerNumber] = false;
            }
            else if (keyUp)
            {
                bools[input, playerNumber] = false;
                boolsHeld[input, playerNumber] = false;
                boolsFreshPress[input, playerNumber] = false;
                boolsFreshPressDeleteOnRead[input, playerNumber] = false;
                boolsFreshPressAccessed[input, playerNumber] = false;
                boolsUp[input, playerNumber] = true;
            }
            else
                boolsUp[input, playerNumber] = false;

            if (rawsFreshPressAccessed[input, playerNumber])
            {
                rawsFreshPressAccessed[input, playerNumber] = false;
                rawsFreshPress[input, playerNumber] = 0f;
                rawsFreshPressDeleteOnRead[input, playerNumber] = 0f;
            }
            if (raws[input, playerNumber] != 0 && key)
            {
                rawsFreshPress[input, playerNumber] = data;
                if (Mathf.Sign(rawsFreshPress[input, playerNumber]) != Mathf.Sign(rawSign[input]))
                    rawsFreshPress[input, playerNumber] = -rawsFreshPress[input, playerNumber];
                rawsFreshPressDeleteOnRead[input, playerNumber] = data;
                if (Mathf.Sign(rawsFreshPressDeleteOnRead[input, playerNumber]) != Mathf.Sign(rawSign[input]))
                    rawsFreshPressDeleteOnRead[input, playerNumber] = -rawsFreshPressDeleteOnRead[input, playerNumber];
            }
            if (key)
            {
                raws[input, playerNumber] = data;
                if (Mathf.Sign(raws[input, playerNumber]) != Mathf.Sign(rawSign[input]))
                    raws[input, playerNumber] = -raws[input, playerNumber];
                rawsHeld[input, playerNumber] = data;
                if (Mathf.Sign(rawsHeld[input, playerNumber]) != Mathf.Sign(rawSign[input]))
                    rawsHeld[input, playerNumber] = -rawsHeld[input, playerNumber];
                rawsUp[input, playerNumber] = 0f;
            }
            else if (keyUp)
            {
                raws[input, playerNumber] = 0f;
                rawsHeld[input, playerNumber] = 0f;
                rawsFreshPress[input, playerNumber] = 0f;
                rawsFreshPressDeleteOnRead[input, playerNumber] = 0f;
                rawsFreshPressAccessed[input, playerNumber] = false;
                rawsUp[input, playerNumber] = data;
                if (Mathf.Sign(rawsUp[input, playerNumber]) != Mathf.Sign(rawSign[input]))
                    rawsUp[input, playerNumber] = -rawsUp[input, playerNumber];
            }
            else
                rawsUp[input, playerNumber] = 0f;
        }
    }
}
