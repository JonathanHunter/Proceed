//Proceed: Jonathan Hunter, Larry Smith, Justin Coates, Chris Tansey
using UnityEngine;

namespace Assets.Scripts.Util
{
    class CameraTracking : MonoBehaviour
    {
        [SerializeField]
        private Transform player;
        public float speed = .5f;

        private float theta;
        public float Theta
        {
            get { return theta; }
            set { theta = value; }
        }

        void Start()
        {
            theta = 0f;
        }

        void Update()
        {
            theta += (Util.CustomInput.Raw(CustomInput.UserInput.TurnCameraLeft) + Util.CustomInput.Raw(CustomInput.UserInput.TurnCameraRight)) * Time.deltaTime * 90;
            if (theta > 360f)
                theta = 0f;
            if (theta < 0f)
                theta = 360f;

            if (Mathf.Abs(this.transform.position.x - player.position.x) > 15)
                this.transform.position = player.position;
            float speed = Time.deltaTime * ((Mathf.Ceil(Mathf.Abs(this.transform.position.x - player.position.x))) + 10);
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(player.position.x + 6 * Mathf.Cos((theta)*Mathf.Deg2Rad), player.transform.position.y + 4, player.transform.position.z + 6 * Mathf.Sin((theta) * Mathf.Deg2Rad)), speed);
            transform.LookAt(player.position + new Vector3(0,2,0));
            //transform.position = Vector3.Lerp(transform.position, new Vector3(player.position.x + 6, player.transform.position.y + 4, player.transform.position.z), speed);
        }
    }
}
