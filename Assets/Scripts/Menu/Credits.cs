using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Menu
{
    class Credits : MonoBehaviour
    {
        [SerializeField]
        private MoveToPoint[] credits;
        [SerializeField]
        private Transform left;
        [SerializeField]
        private Transform middle;
        [SerializeField]
        private Transform right;
        [SerializeField]
        private GameObject button;

        private int currentImage;

        void Start()
        {
            Init();
        }

        void Update()
        {
            if (EventSystem.current.currentSelectedGameObject == null)
                EventSystem.current.SetSelectedGameObject(button);

            if (currentImage > 0 && Util.CustomInput.BoolFreshPressDeleteOnRead(Util.CustomInput.UserInput.Left))
                RightShift();
            if (currentImage < credits.Length - 1 && Util.CustomInput.BoolFreshPressDeleteOnRead(Util.CustomInput.UserInput.Right))
                LeftShift();

            if (Util.CustomInput.BoolFreshPressDeleteOnRead(Util.CustomInput.UserInput.Accept))
            {
                if (currentImage < credits.Length - 1)
                    LeftShift();
                else
                {
                    Init();
                    Navigator.CallSubmit();
                }
            }
            if (Util.CustomInput.BoolFreshPressDeleteOnRead(Util.CustomInput.UserInput.Cancel))
            {
                Init();
                Navigator.CallSubmit();
            }
        }

        private void Init()
        {
            currentImage = 0;
            credits[0].gameObject.transform.position = middle.position;
            credits[0].MoveTo(middle);
            for (int i = 1; i < credits.Length; i++)
            {
                credits[i].gameObject.transform.position = right.position;
                credits[i].MoveTo(right);
            }
        }

        private void LeftShift()
        {
            credits[currentImage].MoveTo(left);
            credits[currentImage + 1].MoveTo(middle);
            currentImage++;
        }

        private void RightShift()
        {
            credits[currentImage].MoveTo(right);
            credits[currentImage - 1].MoveTo(middle);
            currentImage--;
        }
    }
}
