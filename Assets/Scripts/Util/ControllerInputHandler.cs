using UnityEngine;

namespace Assets.Scripts.Util
{
    public static class ControllerInputHandler
    {
        //Enums to handle all possible inputs on a controller. 3 Broad categories "Buttons", "Triggers" and "Axis". 
        //Linux environment is supported but for Dpad Keys only wired controllers are supported.
        //PS3 stuff yet to come. A,B,X and Y will map to corresponding buttons on PS3
        //A -> Square
        //B -> X
        //X -> Circle
        //Y -> Triangle
        public enum Axis { LeftStickY, LeftStickX, RightStickY, RightStickX, DPadY, DPadX };
        public enum Buttons { A, B, X, Y, RightBumper, LeftBumper, Back, Start, LeftStickClick, RightStickClick };
        public enum Triggers { RightTrigger, LeftTrigger };
        public enum ControlType { Xbox, PS3, PS4 };
        private const string LEFT_OSX_TRIGGER = "LeftOSXTrigger";
        private const string LEFT_LINUX_TRIGGER = "LeftLinuxTrigger";
        private const string LEFT_WIN_TRIGGER = "LeftWinTrigger";
        private const string RIGHT_OSX_TRIGGER = "RightOSXTrigger";
        private const string RIGHT_LINUX_TRIGGER = "RightLinuxTrigger";
        private const string RIGHT_WIN_TRIGGER = "RightWinTrigger";
        private const string RIGHT_OSX_STICK_Y = "RightOSXStickY";
        private const string RIGHT_WIN_STICK_Y = "RightWinStickY";
        private const string RIGHT_OSX_STICK_X = "RightOSXStickX";
        private const string RIGHT_WIN_STICK_X = "RightWinStickX";
        private const string RIGHT_PS_STICK_X = "RightPSStickX";
        private const string RIGHT_PS_STICK_Y = "RightPSStickY";
        private const string LEFT_STICK_Y = "LeftStickY";
        private const string LEFT_STICK_X = "LeftStickX";
        private const string DPAD_WIN_STICK_Y = "DpadWinStickY";
        private const string DPAD_WIN_STICK_X = "DpadWinStickX";
        private const string DPAD_LINUX_STICK_X = "DpadLinuxStickX";
        private const string DPAD_LINUX_STICK_Y = "DpadLinuxStickY";
        private const string DPAD_PS_STICK_X = "DpadPSStickX";
        private const string DPAD_PS_STICK_Y = "DpadPSStickY";
        private const string RIGHT_PS4_STICK_X = "RightPS4StickX";
        private const string RIGHT_PS4_STICK_Y = "RightPS4StickY";
        private const string DPAD_PS4_STICK_X = "DpadPS4StickX";
        private const string DPAD_PS4_STICK_Y = "DpadPS4StickY";

        /// <summary> A tuple for defining a controllers type and number. </summary>
        public struct Controller { public ControlType type; public int joysticNum; }

        public static bool GetButton(Buttons btn, Controller controller)
        {
            if (controller.joysticNum < 0)
                return false;
            KeyCode btnKeyCode = GetKeyCode(btn, controller);
            return Input.GetKey(btnKeyCode);
        }

        public static bool GetButtonUp(Buttons btn, Controller controller)
        {
            if (controller.joysticNum < 0)
                return false;
            KeyCode btnKeyCode = GetKeyCode(btn, controller);
            return Input.GetKeyUp(btnKeyCode);
        }

        public static bool GetButtonDown(Buttons btn, Controller controller)
        {
            if (controller.joysticNum < 0)
                return false;
            KeyCode btnKeyCode = GetKeyCode(btn, controller);
            return Input.GetKeyDown(btnKeyCode);
        }

        public static float GetAxis(Axis axisName, Controller controller)
        {
            if (controller.joysticNum < 0)
                return 0;
            float result = 0;
            ControlType inControlType = controller.type;
            int joyStickNumber = controller.joysticNum;
            switch (axisName)
            {
                case Axis.RightStickY:
                    if (inControlType == ControlType.PS3)
                    {
                        result = Input.GetAxisRaw(RIGHT_PS_STICK_Y + joyStickNumber);
                        break;
                    }
                    if (inControlType == ControlType.PS4)
                    {
                        result = Input.GetAxisRaw(RIGHT_PS4_STICK_Y + joyStickNumber);
                        break;
                    }
                    switch (Application.platform)
                    {
                        case RuntimePlatform.OSXDashboardPlayer:
                        case RuntimePlatform.OSXEditor:
                        case RuntimePlatform.OSXPlayer:
                        case RuntimePlatform.OSXWebPlayer:
                            result = Input.GetAxisRaw(RIGHT_OSX_STICK_Y + joyStickNumber);
                            break;
                        case RuntimePlatform.LinuxPlayer:
                        default:
                            result = Input.GetAxisRaw(RIGHT_WIN_STICK_Y + joyStickNumber);
                            break;
                    }
                    break;
                case Axis.RightStickX:
                    if (inControlType == ControlType.PS3)
                    {
                        result = Input.GetAxisRaw(RIGHT_PS_STICK_X + joyStickNumber);
                        break;
                    }
                    if (inControlType == ControlType.PS4)
                    {
                        result = Input.GetAxisRaw(RIGHT_PS4_STICK_X + joyStickNumber);
                        break;
                    }
                    switch (Application.platform)
                    {
                        case RuntimePlatform.OSXDashboardPlayer:
                        case RuntimePlatform.OSXEditor:
                        case RuntimePlatform.OSXPlayer:
                        case RuntimePlatform.OSXWebPlayer:
                            result = Input.GetAxisRaw(RIGHT_OSX_STICK_X + joyStickNumber);
                            break;
                        case RuntimePlatform.LinuxPlayer:
                        default:
                            result = Input.GetAxisRaw(RIGHT_WIN_STICK_X + joyStickNumber);
                            break;
                    }
                    break;
                case Axis.LeftStickY:
                    switch (Application.platform)
                    {
                        case RuntimePlatform.OSXDashboardPlayer:
                        case RuntimePlatform.OSXEditor:
                        case RuntimePlatform.OSXPlayer:
                        case RuntimePlatform.OSXWebPlayer:
                        case RuntimePlatform.LinuxPlayer:
                        default:
                            result = Input.GetAxisRaw(LEFT_STICK_Y + joyStickNumber);
                            break;
                    }
                    break;
                case Axis.LeftStickX:
                    switch (Application.platform)
                    {
                        case RuntimePlatform.OSXDashboardPlayer:
                        case RuntimePlatform.OSXEditor:
                        case RuntimePlatform.OSXPlayer:
                        case RuntimePlatform.OSXWebPlayer:
                        case RuntimePlatform.LinuxPlayer:
                        default:
                            result = Input.GetAxisRaw(LEFT_STICK_X + joyStickNumber);
                            break;
                    }
                    break;
                case Axis.DPadY:
                    if (inControlType == ControlType.PS3)
                    {
                        result = Input.GetAxisRaw(DPAD_PS_STICK_Y + joyStickNumber);
                        break;
                    }
                    if (inControlType == ControlType.PS4)
                    {
                        result = Input.GetAxisRaw(DPAD_PS4_STICK_Y + joyStickNumber);
                        break;
                    }
                    switch (Application.platform)
                    {
                        case RuntimePlatform.LinuxPlayer:
                            result = Input.GetAxisRaw(DPAD_LINUX_STICK_Y + joyStickNumber);
                            break;
                        case RuntimePlatform.OSXDashboardPlayer:
                        case RuntimePlatform.OSXEditor:
                        case RuntimePlatform.OSXPlayer:
                        case RuntimePlatform.OSXWebPlayer:
                            result = (Input.GetKey(KeyCode.JoystickButton6) ? -1 : 0);
                            if (result == 0) result = (Input.GetKey(KeyCode.JoystickButton5) ? 1 : 0);
                            break;
                        default:
                            result = Input.GetAxisRaw(DPAD_WIN_STICK_Y + joyStickNumber);
                            break;
                    }
                    break;
                case Axis.DPadX:
                    if (inControlType == ControlType.PS3)
                    {
                        result = Input.GetAxisRaw(DPAD_PS_STICK_X + joyStickNumber);
                        break;
                    }
                    if (inControlType == ControlType.PS4)
                    {
                        result = Input.GetAxisRaw(DPAD_PS4_STICK_X + joyStickNumber);
                        break;
                    }
                    switch (Application.platform)
                    {
                        case RuntimePlatform.LinuxPlayer:
                            result = Input.GetAxisRaw(DPAD_LINUX_STICK_X + joyStickNumber);
                            break;
                        case RuntimePlatform.OSXDashboardPlayer:
                        case RuntimePlatform.OSXEditor:
                        case RuntimePlatform.OSXPlayer:
                        case RuntimePlatform.OSXWebPlayer:
                            result = (Input.GetKey(KeyCode.JoystickButton7) ? -1 : 0);
                            if (result == 0) result = (Input.GetKey(KeyCode.JoystickButton8) ? 1 : 0);
                            break;
                        default:
                            result = Input.GetAxisRaw(DPAD_WIN_STICK_X + joyStickNumber);
                            break;
                    }
                    break;
            }
            return result;
        }

        public static float GetTrigger(Triggers trgName, Controller controller)
        {
            if (controller.joysticNum < 0)
                return 0;
            float result = 0;
            ControlType inControlType = controller.type;
            int joyStickNumber = controller.joysticNum;
            switch (trgName)
            {
                case Triggers.LeftTrigger:
                    if (inControlType == ControlType.PS3)
                    {
                        switch (joyStickNumber)
                        {
                            case 1: result = (Input.GetKey(KeyCode.Joystick1Button6) ? 1 : 0); break;
                            case 2: result = (Input.GetKey(KeyCode.Joystick2Button6) ? 1 : 0); break;
                            case 3: result = (Input.GetKey(KeyCode.Joystick3Button6) ? 1 : 0); break;
                            case 4: result = (Input.GetKey(KeyCode.Joystick4Button6) ? 1 : 0); break;
                            case 5: result = (Input.GetKey(KeyCode.Joystick5Button6) ? 1 : 0); break;
                            case 6: result = (Input.GetKey(KeyCode.Joystick6Button6) ? 1 : 0); break;
                            default: result = (Input.GetKey(KeyCode.JoystickButton6) ? 1 : 0); break;
                        }
                        break;
                    }
                    if (inControlType == ControlType.PS4)
                    {
                        switch (joyStickNumber)
                        {
                            case 1: result = (Input.GetKey(KeyCode.Joystick1Button6) ? 1 : 0); break;
                            case 2: result = (Input.GetKey(KeyCode.Joystick2Button6) ? 1 : 0); break;
                            case 3: result = (Input.GetKey(KeyCode.Joystick3Button6) ? 1 : 0); break;
                            case 4: result = (Input.GetKey(KeyCode.Joystick4Button6) ? 1 : 0); break;
                            case 5: result = (Input.GetKey(KeyCode.Joystick5Button6) ? 1 : 0); break;
                            case 6: result = (Input.GetKey(KeyCode.Joystick6Button6) ? 1 : 0); break;
                            default: result = (Input.GetKey(KeyCode.JoystickButton6) ? 1 : 0); break;
                        }
                        break;
                    }
                    switch (Application.platform)
                    {
                        case RuntimePlatform.OSXDashboardPlayer:
                        case RuntimePlatform.OSXEditor:
                        case RuntimePlatform.OSXPlayer:
                        case RuntimePlatform.OSXWebPlayer:
                            result = Input.GetAxisRaw(LEFT_OSX_TRIGGER + joyStickNumber);
                            break;
                        case RuntimePlatform.LinuxPlayer:
                            result = Input.GetAxisRaw(LEFT_LINUX_TRIGGER + joyStickNumber);
                            break;
                        default:
                            result = Input.GetAxisRaw(LEFT_WIN_TRIGGER + joyStickNumber);
                            break;
                    }
                    break;
                case Triggers.RightTrigger:
                    if (inControlType == ControlType.PS3)
                    {
                        switch (joyStickNumber)
                        {
                            case 1: result = (Input.GetKey(KeyCode.Joystick1Button7) ? 1 : 0); break;
                            case 2: result = (Input.GetKey(KeyCode.Joystick2Button7) ? 1 : 0); break;
                            case 3: result = (Input.GetKey(KeyCode.Joystick3Button7) ? 1 : 0); break;
                            case 4: result = (Input.GetKey(KeyCode.Joystick4Button7) ? 1 : 0); break;
                            case 5: result = (Input.GetKey(KeyCode.Joystick5Button7) ? 1 : 0); break;
                            case 6: result = (Input.GetKey(KeyCode.Joystick6Button7) ? 1 : 0); break;
                            default: result = (Input.GetKey(KeyCode.JoystickButton7) ? 1 : 0); break;
                        }
                        break;
                    }
                    if (inControlType == ControlType.PS4)
                    {
                        switch (joyStickNumber)
                        {
                            case 1: result = (Input.GetKey(KeyCode.Joystick1Button7) ? 1 : 0); break;
                            case 2: result = (Input.GetKey(KeyCode.Joystick2Button7) ? 1 : 0); break;
                            case 3: result = (Input.GetKey(KeyCode.Joystick3Button7) ? 1 : 0); break;
                            case 4: result = (Input.GetKey(KeyCode.Joystick4Button7) ? 1 : 0); break;
                            case 5: result = (Input.GetKey(KeyCode.Joystick5Button7) ? 1 : 0); break;
                            case 6: result = (Input.GetKey(KeyCode.Joystick6Button7) ? 1 : 0); break;
                            default: result = (Input.GetKey(KeyCode.JoystickButton7) ? 1 : 0); break;
                        }
                        break;
                    }
                    switch (Application.platform)
                    {
                        case RuntimePlatform.OSXDashboardPlayer:
                        case RuntimePlatform.OSXEditor:
                        case RuntimePlatform.OSXPlayer:
                        case RuntimePlatform.OSXWebPlayer:
                            result = Input.GetAxisRaw(RIGHT_OSX_TRIGGER + joyStickNumber);
                            break;
                        case RuntimePlatform.LinuxPlayer:
                            result = Input.GetAxisRaw(RIGHT_LINUX_TRIGGER + joyStickNumber);
                            break;
                        default:
                            result = Input.GetAxisRaw(RIGHT_WIN_TRIGGER + joyStickNumber);
                            break;
                    }
                    break;
            }
            return result;
        }

        //Function to return Keycode of a particular button based on OS and joystick number. Joystick number 0 is considered for "Any" joystick.
        public static KeyCode GetKeyCode(Buttons btn, Controller controller)
        {
            if (controller.joysticNum < 0)
                return KeyCode.None;
            ControlType inControlType = controller.type;
            int joyStickNumber = controller.joysticNum;
            switch (joyStickNumber)
            {
                #region joystick 1
                case 1:
                    switch (btn)
                    {
                        case Buttons.A: //X for playstation
                            if (inControlType == ControlType.PS3 || inControlType == ControlType.PS4)
                                return KeyCode.Joystick1Button1;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick1Button16;
                                default:
                                    return KeyCode.Joystick1Button0;
                            }
                        case Buttons.B: //Circle for playstation
                            if (inControlType == ControlType.PS3 || inControlType == ControlType.PS4)
                                return KeyCode.Joystick1Button2;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick1Button17;
                                default:
                                    return KeyCode.Joystick1Button1;
                            }
                        case Buttons.X:  //Square for playstation
                            if (inControlType == ControlType.PS3 || inControlType == ControlType.PS4)
                                return KeyCode.Joystick1Button0;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick1Button18;
                                default:
                                    return KeyCode.Joystick1Button2;
                            }
                        case Buttons.Y: //Triangle for playstation
                            if (inControlType == ControlType.PS3 || inControlType == ControlType.PS4)
                                return KeyCode.Joystick1Button3;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick1Button19;
                                default:
                                    return KeyCode.Joystick1Button3;
                            }
                        case Buttons.RightBumper:   //R1 for playstation
                            if (inControlType == ControlType.PS3 || inControlType == ControlType.PS4)
                                return KeyCode.Joystick1Button5;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                    return KeyCode.Joystick1Button14;
                                case RuntimePlatform.OSXEditor:
                                    return KeyCode.Joystick1Button14;
                                case RuntimePlatform.OSXPlayer:
                                    return KeyCode.Joystick1Button14;
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick1Button14;
                                default:
                                    return KeyCode.Joystick1Button5;
                            }
                        case Buttons.LeftBumper:    //L1 for playstation
                            if (inControlType == ControlType.PS3)
                                return KeyCode.Joystick1Button6;
                            if (inControlType == ControlType.PS4)
                                return KeyCode.Joystick1Button4;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick1Button13;
                                default:
                                    return KeyCode.Joystick1Button4;
                            }
                        case Buttons.Back:      //Select for playstation
                            if (inControlType == ControlType.PS3 || inControlType == ControlType.PS4)
                                return KeyCode.Joystick1Button8;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick1Button10;
                                default:
                                    return KeyCode.Joystick1Button6;
                            }
                        case Buttons.Start:     //Start for playstation
                            if (inControlType == ControlType.PS3 || inControlType == ControlType.PS4)
                                return KeyCode.Joystick1Button9;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick1Button9;
                                default:
                                    return KeyCode.Joystick1Button7;
                            }
                        case Buttons.RightStickClick:
                            if (inControlType == ControlType.PS3)
                                return KeyCode.Joystick1Button13;
                            if (inControlType == ControlType.PS4)
                                return KeyCode.Joystick1Button11;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick1Button12;
                                case RuntimePlatform.LinuxPlayer:
                                    return KeyCode.Joystick1Button10;
                                default:
                                    return KeyCode.Joystick1Button9;
                            }
                        case Buttons.LeftStickClick:
                            if (inControlType == ControlType.PS3)
                                return KeyCode.Joystick1Button12;
                            if (inControlType == ControlType.PS4)
                                return KeyCode.Joystick1Button10;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick1Button11;
                                case RuntimePlatform.LinuxPlayer:
                                    return KeyCode.Joystick1Button9;
                                default:
                                    return KeyCode.Joystick1Button8;
                            }
                    }
                    break;
                #endregion
                #region joystick 2
                case 2:
                    switch (btn)
                    {
                        case Buttons.A: //X for playstation
                            if (inControlType == ControlType.PS3 || inControlType == ControlType.PS4)
                                return KeyCode.Joystick2Button1;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick2Button16;
                                default:
                                    return KeyCode.Joystick2Button0;
                            }
                        case Buttons.B: //Circle for playstation
                            if (inControlType == ControlType.PS3 || inControlType == ControlType.PS4)
                                return KeyCode.Joystick2Button2;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick2Button17;
                                default:
                                    return KeyCode.Joystick2Button1;
                            }
                        case Buttons.X:  //Square for playstation
                            if (inControlType == ControlType.PS3 || inControlType == ControlType.PS4)
                                return KeyCode.Joystick2Button0;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick2Button18;
                                default:
                                    return KeyCode.Joystick2Button2;
                            }
                        case Buttons.Y: //Triangle for playstation
                            if (inControlType == ControlType.PS3 || inControlType == ControlType.PS4)
                                return KeyCode.Joystick2Button3;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick2Button19;
                                default:
                                    return KeyCode.Joystick2Button3;
                            }
                        case Buttons.RightBumper:   //R1 for playstation
                            if (inControlType == ControlType.PS3 || inControlType == ControlType.PS4)
                                return KeyCode.Joystick2Button5;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                    return KeyCode.Joystick2Button14;
                                case RuntimePlatform.OSXEditor:
                                    return KeyCode.Joystick2Button14;
                                case RuntimePlatform.OSXPlayer:
                                    return KeyCode.Joystick2Button14;
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick2Button14;
                                default:
                                    return KeyCode.Joystick2Button5;
                            }
                        case Buttons.LeftBumper:    //L1 for playstation
                            if (inControlType == ControlType.PS3)
                                return KeyCode.Joystick2Button6;
                            if (inControlType == ControlType.PS4)
                                return KeyCode.Joystick2Button4;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick2Button13;
                                default:
                                    return KeyCode.Joystick2Button4;
                            }
                        case Buttons.Back:      //Select for playstation
                            if (inControlType == ControlType.PS3 || inControlType == ControlType.PS4)
                                return KeyCode.Joystick2Button8;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick2Button10;
                                default:
                                    return KeyCode.Joystick2Button6;
                            }
                        case Buttons.Start:     //Start for playstation
                            if (inControlType == ControlType.PS3 || inControlType == ControlType.PS4)
                                return KeyCode.Joystick2Button9;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick2Button9;
                                default:
                                    return KeyCode.Joystick2Button7;
                            }
                        case Buttons.RightStickClick:
                            if (inControlType == ControlType.PS3)
                                return KeyCode.Joystick2Button13;
                            if (inControlType == ControlType.PS4)
                                return KeyCode.Joystick2Button11;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick2Button12;
                                case RuntimePlatform.LinuxPlayer:
                                    return KeyCode.Joystick2Button10;
                                default:
                                    return KeyCode.Joystick2Button9;
                            }
                        case Buttons.LeftStickClick:
                            if (inControlType == ControlType.PS3)
                                return KeyCode.Joystick2Button12;
                            if (inControlType == ControlType.PS4)
                                return KeyCode.Joystick2Button10;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick2Button11;
                                case RuntimePlatform.LinuxPlayer:
                                    return KeyCode.Joystick2Button9;
                                default:
                                    return KeyCode.Joystick2Button8;
                            }
                    }
                    break;
                #endregion
                #region joystick 3
                case 3:
                    switch (btn)
                    {
                        case Buttons.A: //X for playstation
                            if (inControlType == ControlType.PS3 || inControlType == ControlType.PS4)
                                return KeyCode.Joystick3Button1;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick3Button16;
                                default:
                                    return KeyCode.Joystick3Button0;
                            }
                        case Buttons.B: //Circle for playstation
                            if (inControlType == ControlType.PS3 || inControlType == ControlType.PS4)
                                return KeyCode.Joystick3Button2;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick3Button17;
                                default:
                                    return KeyCode.Joystick3Button1;
                            }
                        case Buttons.X:  //Square for playstation
                            if (inControlType == ControlType.PS3 || inControlType == ControlType.PS4)
                                return KeyCode.Joystick3Button0;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick3Button18;
                                default:
                                    return KeyCode.Joystick3Button2;
                            }
                        case Buttons.Y: //Triangle for playstation
                            if (inControlType == ControlType.PS3 || inControlType == ControlType.PS4)
                                return KeyCode.Joystick3Button3;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick3Button19;
                                default:
                                    return KeyCode.Joystick3Button3;
                            }
                        case Buttons.RightBumper:   //R1 for playstation
                            if (inControlType == ControlType.PS3 || inControlType == ControlType.PS4)
                                return KeyCode.Joystick3Button5;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                    return KeyCode.Joystick3Button14;
                                case RuntimePlatform.OSXEditor:
                                    return KeyCode.Joystick3Button14;
                                case RuntimePlatform.OSXPlayer:
                                    return KeyCode.Joystick3Button14;
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick3Button14;
                                default:
                                    return KeyCode.Joystick3Button5;
                            }
                        case Buttons.LeftBumper:    //L1 for playstation
                            if (inControlType == ControlType.PS3)
                                return KeyCode.Joystick3Button6;
                            if (inControlType == ControlType.PS4)
                                return KeyCode.Joystick3Button4;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick3Button13;
                                default:
                                    return KeyCode.Joystick3Button4;
                            }
                        case Buttons.Back:      //Select for playstation
                            if (inControlType == ControlType.PS3 || inControlType == ControlType.PS4)
                                return KeyCode.Joystick3Button8;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick3Button10;
                                default:
                                    return KeyCode.Joystick3Button6;
                            }
                        case Buttons.Start:     //Start for playstation
                            if (inControlType == ControlType.PS3 || inControlType == ControlType.PS4)
                                return KeyCode.Joystick3Button9;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick3Button9;
                                default:
                                    return KeyCode.Joystick3Button7;
                            }
                        case Buttons.RightStickClick:
                            if (inControlType == ControlType.PS3)
                                return KeyCode.Joystick3Button13;
                            if (inControlType == ControlType.PS4)
                                return KeyCode.Joystick3Button11;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick3Button12;
                                case RuntimePlatform.LinuxPlayer:
                                    return KeyCode.Joystick3Button10;
                                default:
                                    return KeyCode.Joystick3Button9;
                            }
                        case Buttons.LeftStickClick:
                            if (inControlType == ControlType.PS3)
                                return KeyCode.Joystick3Button12;
                            if (inControlType == ControlType.PS4)
                                return KeyCode.Joystick3Button10;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick3Button11;
                                case RuntimePlatform.LinuxPlayer:
                                    return KeyCode.Joystick3Button9;
                                default:
                                    return KeyCode.Joystick3Button8;
                            }
                    }
                    break;
                #endregion
                #region joystick 4
                case 4:
                    switch (btn)
                    {
                        case Buttons.A: //X for playstation
                            if (inControlType == ControlType.PS3 || inControlType == ControlType.PS4)
                                return KeyCode.Joystick4Button1;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick4Button16;
                                default:
                                    return KeyCode.Joystick4Button0;
                            }
                        case Buttons.B: //Circle for playstation
                            if (inControlType == ControlType.PS3 || inControlType == ControlType.PS4)
                                return KeyCode.Joystick4Button2;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick4Button17;
                                default:
                                    return KeyCode.Joystick4Button1;
                            }
                        case Buttons.X:  //Square for playstation
                            if (inControlType == ControlType.PS3 || inControlType == ControlType.PS4)
                                return KeyCode.Joystick4Button0;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick4Button18;
                                default:
                                    return KeyCode.Joystick4Button2;
                            }
                        case Buttons.Y: //Triangle for playstation
                            if (inControlType == ControlType.PS3 || inControlType == ControlType.PS4)
                                return KeyCode.Joystick4Button3;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick4Button19;
                                default:
                                    return KeyCode.Joystick4Button3;
                            }
                        case Buttons.RightBumper:   //R1 for playstation
                            if (inControlType == ControlType.PS3 || inControlType == ControlType.PS4)
                                return KeyCode.Joystick4Button5;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                    return KeyCode.Joystick4Button14;
                                case RuntimePlatform.OSXEditor:
                                    return KeyCode.Joystick4Button14;
                                case RuntimePlatform.OSXPlayer:
                                    return KeyCode.Joystick4Button14;
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick4Button14;
                                default:
                                    return KeyCode.Joystick4Button5;
                            }
                        case Buttons.LeftBumper:    //L1 for playstation
                            if (inControlType == ControlType.PS3)
                                return KeyCode.Joystick4Button6;
                            if (inControlType == ControlType.PS4)
                                return KeyCode.Joystick4Button4;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick4Button13;
                                default:
                                    return KeyCode.Joystick4Button4;
                            }
                        case Buttons.Back:      //Select for playstation
                            if (inControlType == ControlType.PS3 || inControlType == ControlType.PS4)
                                return KeyCode.Joystick4Button8;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick4Button10;
                                default:
                                    return KeyCode.Joystick4Button6;
                            }
                        case Buttons.Start:     //Start for playstation
                            if (inControlType == ControlType.PS3 || inControlType == ControlType.PS4)
                                return KeyCode.Joystick4Button9;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick4Button9;
                                default:
                                    return KeyCode.Joystick4Button7;
                            }
                        case Buttons.RightStickClick:
                            if (inControlType == ControlType.PS3)
                                return KeyCode.Joystick4Button13;
                            if (inControlType == ControlType.PS4)
                                return KeyCode.Joystick4Button11;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick4Button12;
                                case RuntimePlatform.LinuxPlayer:
                                    return KeyCode.Joystick4Button10;
                                default:
                                    return KeyCode.Joystick4Button9;
                            }
                        case Buttons.LeftStickClick:
                            if (inControlType == ControlType.PS3)
                                return KeyCode.Joystick4Button12;
                            if (inControlType == ControlType.PS4)
                                return KeyCode.Joystick4Button10;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick4Button11;
                                case RuntimePlatform.LinuxPlayer:
                                    return KeyCode.Joystick4Button9;
                                default:
                                    return KeyCode.Joystick4Button8;
                            }
                    }
                    break;
                #endregion
                #region joystick 5
                case 5:
                    switch (btn)
                    {
                        case Buttons.A: //X for playstation
                            if (inControlType == ControlType.PS3 || inControlType == ControlType.PS4)
                                return KeyCode.Joystick5Button1;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick5Button16;
                                default:
                                    return KeyCode.Joystick5Button0;
                            }
                        case Buttons.B: //Circle for playstation
                            if (inControlType == ControlType.PS3 || inControlType == ControlType.PS4)
                                return KeyCode.Joystick5Button2;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick5Button17;
                                default:
                                    return KeyCode.Joystick5Button1;
                            }
                        case Buttons.X:  //Square for playstation
                            if (inControlType == ControlType.PS3 || inControlType == ControlType.PS4)
                                return KeyCode.Joystick5Button0;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick5Button18;
                                default:
                                    return KeyCode.Joystick5Button2;
                            }
                        case Buttons.Y: //Triangle for playstation
                            if (inControlType == ControlType.PS3 || inControlType == ControlType.PS4)
                                return KeyCode.Joystick5Button3;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick5Button19;
                                default:
                                    return KeyCode.Joystick5Button3;
                            }
                        case Buttons.RightBumper:   //R1 for playstation
                            if (inControlType == ControlType.PS3 || inControlType == ControlType.PS4)
                                return KeyCode.Joystick5Button5;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                    return KeyCode.Joystick5Button14;
                                case RuntimePlatform.OSXEditor:
                                    return KeyCode.Joystick5Button14;
                                case RuntimePlatform.OSXPlayer:
                                    return KeyCode.Joystick5Button14;
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick5Button14;
                                default:
                                    return KeyCode.Joystick5Button5;
                            }
                        case Buttons.LeftBumper:    //L1 for playstation
                            if (inControlType == ControlType.PS3)
                                return KeyCode.Joystick5Button6;
                            if (inControlType == ControlType.PS4)
                                return KeyCode.Joystick5Button4;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick5Button13;
                                default:
                                    return KeyCode.Joystick5Button4;
                            }
                        case Buttons.Back:      //Select for playstation
                            if (inControlType == ControlType.PS3 || inControlType == ControlType.PS4)
                                return KeyCode.Joystick5Button8;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick5Button10;
                                default:
                                    return KeyCode.Joystick5Button6;
                            }
                        case Buttons.Start:     //Start for playstation
                            if (inControlType == ControlType.PS3 || inControlType == ControlType.PS4)
                                return KeyCode.Joystick5Button9;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick5Button9;
                                default:
                                    return KeyCode.Joystick5Button7;
                            }
                        case Buttons.RightStickClick:
                            if (inControlType == ControlType.PS3)
                                return KeyCode.Joystick5Button13;
                            if (inControlType == ControlType.PS4)
                                return KeyCode.Joystick5Button11;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick5Button12;
                                case RuntimePlatform.LinuxPlayer:
                                    return KeyCode.Joystick5Button10;
                                default:
                                    return KeyCode.Joystick5Button9;
                            }
                        case Buttons.LeftStickClick:
                            if (inControlType == ControlType.PS3)
                                return KeyCode.Joystick5Button12;
                            if (inControlType == ControlType.PS4)
                                return KeyCode.Joystick5Button10;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick5Button11;
                                case RuntimePlatform.LinuxPlayer:
                                    return KeyCode.Joystick5Button9;
                                default:
                                    return KeyCode.Joystick5Button8;
                            }
                    }
                    break;
                #endregion
                #region joystick 6
                case 6:
                    switch (btn)
                    {
                        case Buttons.A: //X for playstation
                            if (inControlType == ControlType.PS3 || inControlType == ControlType.PS4)
                                return KeyCode.Joystick6Button1;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick6Button16;
                                default:
                                    return KeyCode.Joystick6Button0;
                            }
                        case Buttons.B: //Circle for playstation
                            if (inControlType == ControlType.PS3 || inControlType == ControlType.PS4)
                                return KeyCode.Joystick6Button2;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick6Button17;
                                default:
                                    return KeyCode.Joystick6Button1;
                            }
                        case Buttons.X:  //Square for playstation
                            if (inControlType == ControlType.PS3 || inControlType == ControlType.PS4)
                                return KeyCode.Joystick6Button0;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick6Button18;
                                default:
                                    return KeyCode.Joystick6Button2;
                            }
                        case Buttons.Y: //Triangle for playstation
                            if (inControlType == ControlType.PS3 || inControlType == ControlType.PS4)
                                return KeyCode.Joystick6Button3;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick6Button19;
                                default:
                                    return KeyCode.Joystick6Button3;
                            }
                        case Buttons.RightBumper:   //R1 for playstation
                            if (inControlType == ControlType.PS3 || inControlType == ControlType.PS4)
                                return KeyCode.Joystick6Button5;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                    return KeyCode.Joystick6Button14;
                                case RuntimePlatform.OSXEditor:
                                    return KeyCode.Joystick6Button14;
                                case RuntimePlatform.OSXPlayer:
                                    return KeyCode.Joystick6Button14;
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick6Button14;
                                default:
                                    return KeyCode.Joystick6Button5;
                            }
                        case Buttons.LeftBumper:    //L1 for playstation
                            if (inControlType == ControlType.PS3)
                                return KeyCode.Joystick6Button6;
                            if (inControlType == ControlType.PS4)
                                return KeyCode.Joystick6Button4;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick6Button13;
                                default:
                                    return KeyCode.Joystick6Button4;
                            }
                        case Buttons.Back:      //Select for playstation
                            if (inControlType == ControlType.PS3 || inControlType == ControlType.PS4)
                                return KeyCode.Joystick6Button8;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick6Button10;
                                default:
                                    return KeyCode.Joystick6Button6;
                            }
                        case Buttons.Start:     //Start for playstation
                            if (inControlType == ControlType.PS3 || inControlType == ControlType.PS4)
                                return KeyCode.Joystick6Button9;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick6Button9;
                                default:
                                    return KeyCode.Joystick6Button7;
                            }
                        case Buttons.RightStickClick:
                            if (inControlType == ControlType.PS3)
                                return KeyCode.Joystick6Button13;
                            if (inControlType == ControlType.PS4)
                                return KeyCode.Joystick6Button11;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick6Button12;
                                case RuntimePlatform.LinuxPlayer:
                                    return KeyCode.Joystick6Button10;
                                default:
                                    return KeyCode.Joystick6Button9;
                            }
                        case Buttons.LeftStickClick:
                            if (inControlType == ControlType.PS3)
                                return KeyCode.Joystick6Button12;
                            if (inControlType == ControlType.PS4)
                                return KeyCode.Joystick6Button10;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.Joystick6Button11;
                                case RuntimePlatform.LinuxPlayer:
                                    return KeyCode.Joystick6Button9;
                                default:
                                    return KeyCode.Joystick6Button8;
                            }
                    }
                    break;
                #endregion
                #region joystick 0
                default:
                    switch (btn)
                    {
                        case Buttons.A: //X for playstation
                            if (inControlType == ControlType.PS3 || inControlType == ControlType.PS4)
                                return KeyCode.JoystickButton1;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.JoystickButton16;
                                default:
                                    return KeyCode.JoystickButton0;
                            }
                        case Buttons.B: //Circle for playstation
                            if (inControlType == ControlType.PS3 || inControlType == ControlType.PS4)
                                return KeyCode.JoystickButton2;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.JoystickButton17;
                                default:
                                    return KeyCode.JoystickButton1;
                            }
                        case Buttons.X:  //Square for playstation
                            if (inControlType == ControlType.PS3 || inControlType == ControlType.PS4)
                                return KeyCode.JoystickButton0;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.JoystickButton18;
                                default:
                                    return KeyCode.JoystickButton2;
                            }
                        case Buttons.Y: //Triangle for playstation
                            if (inControlType == ControlType.PS3 || inControlType == ControlType.PS4)
                                return KeyCode.JoystickButton3;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.JoystickButton19;
                                default:
                                    return KeyCode.JoystickButton3;
                            }
                        case Buttons.RightBumper:   //R1 for playstation
                            if (inControlType == ControlType.PS3 || inControlType == ControlType.PS4)
                                return KeyCode.JoystickButton5;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                    return KeyCode.JoystickButton14;
                                case RuntimePlatform.OSXEditor:
                                    return KeyCode.JoystickButton14;
                                case RuntimePlatform.OSXPlayer:
                                    return KeyCode.JoystickButton14;
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.JoystickButton14;
                                default:
                                    return KeyCode.JoystickButton5;
                            }
                        case Buttons.LeftBumper:    //L1 for playstation
                            if (inControlType == ControlType.PS3)
                                return KeyCode.JoystickButton6;
                            if (inControlType == ControlType.PS4)
                                return KeyCode.JoystickButton4;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.JoystickButton13;
                                default:
                                    return KeyCode.JoystickButton4;
                            }
                        case Buttons.Back:      //Select for playstation
                            if (inControlType == ControlType.PS3 || inControlType == ControlType.PS4)
                                return KeyCode.JoystickButton8;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.JoystickButton10;
                                default:
                                    return KeyCode.JoystickButton6;
                            }
                        case Buttons.Start:     //Start for playstation
                            if (inControlType == ControlType.PS3 || inControlType == ControlType.PS4)
                                return KeyCode.JoystickButton9;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.JoystickButton9;
                                default:
                                    return KeyCode.JoystickButton7;
                            }
                        case Buttons.RightStickClick:
                            if (inControlType == ControlType.PS3)
                                return KeyCode.JoystickButton13;
                            if (inControlType == ControlType.PS4)
                                return KeyCode.JoystickButton11;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.JoystickButton12;
                                case RuntimePlatform.LinuxPlayer:
                                    return KeyCode.JoystickButton10;
                                default:
                                    return KeyCode.JoystickButton9;
                            }
                        case Buttons.LeftStickClick:
                            if (inControlType == ControlType.PS3)
                                return KeyCode.JoystickButton12;
                            if (inControlType == ControlType.PS4)
                                return KeyCode.JoystickButton10;
                            switch (Application.platform)
                            {
                                case RuntimePlatform.OSXDashboardPlayer:
                                case RuntimePlatform.OSXEditor:
                                case RuntimePlatform.OSXPlayer:
                                case RuntimePlatform.OSXWebPlayer:
                                    return KeyCode.JoystickButton11;
                                case RuntimePlatform.LinuxPlayer:
                                    return KeyCode.JoystickButton9;
                                default:
                                    return KeyCode.JoystickButton8;
                            }
                            #endregion
                    }
                    break;
            }
            return KeyCode.None;
        }
    }
}
