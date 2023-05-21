using UnityEngine;

public class Parallaxing : MonoBehaviour
{
    [SerializeField] private Transform[] backgrounds;
    [SerializeField] float smoothing = 1f;

    private float[] parallaxScales;
    private Transform camera;
    private Vector3 previousCamPosition;


    private void Awake()
    {
        camera = Camera.main.transform;
    }

    private void Start()
    {
        previousCamPosition = camera.position;

        parallaxScales = new float[backgrounds.Length];
        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].GetComponent<SpriteRenderer>().sortingOrder;
        }

        //renderers = new Renderer[transform.childCount];
        //initialPositionsX = new float[transform.childCount];

        //for (int i = 0; i < transform.childCount; i++)
        //{
        //    renderers[i] = transform.GetChild(i).GetComponent<Renderer>();
        //    initialPositionsX[i] = transform.GetChild(i).position.x;
        //}
    }

    private void LateUpdate()
    {
        if (camera != null)
        {
            for (int i = 0; i < backgrounds.Length; i++)
            {
                float parallax = (previousCamPosition.x - camera.position.x) * parallaxScales[i];

                float backgroungTargetPosX = backgrounds[i].position.x + parallax;

                Vector3 backgroungTargetPos = new Vector3(backgroungTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

                backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroungTargetPos, smoothing * Time.deltaTime);

                //float parallaxOffset = (initialPositionsX[i] - player.position.x) * parallaxSpeeds[i];

                //Vector3 newPosition = transform.GetChild(i).position;
                //newPosition.x = initialPositionsX[i] + parallaxOffset;
                //transform.GetChild(i).position = newPosition;

                //int newSortingOrder = Mathf.RoundToInt(transform.GetChild(i).position.y) * -sortingOrderOffset;
                //renderers[i].sortingOrder = newSortingOrder;
            }

            previousCamPosition = camera.position;
        }
    }
}
