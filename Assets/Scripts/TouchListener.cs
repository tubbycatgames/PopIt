using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchListener : MonoBehaviour
{
    public RandomPop randomPop;

    private GameObject body;
    private float initialDiameter;
    private Vector3 initialBodyScale;

    private GameObject head;
    private Vector3 initialHeadScale;

    private ParticleSystem projectiles;

    private bool popping = false;

    void Start()
    {
        body = GameObject.Find("Body");
        initialDiameter = body.GetComponent<SphereCollider>().radius * 2;
        initialBodyScale = body.transform.localScale;

        head = GameObject.Find("Head");
        initialHeadScale = head.transform.localScale;

        projectiles = GameObject.Find("Projectiles").GetComponent<ParticleSystem>();
        projectiles.Stop();
    }

    void Update()
    {
        if (!popping)
        {
            if (Input.touchCount == 2)
            {
                var firstContact = GetContactPoint(0);
                var secondContact = GetContactPoint(1);
                if (firstContact != Vector3.zero
                    && secondContact != Vector3.zero)
                {
                    var squeezeSize = Vector3.Distance(firstContact, secondContact);

                    var reductionRatio = squeezeSize / initialDiameter;
                    if (reductionRatio < 0.3f)
                    {
                        popping = true;
                        projectiles.Play();
                        randomPop.PlayPop();
                    }
                    UpdateScale(reductionRatio);
                    return;
                }
            }
            UpdateScale(1.0f);
        }
        else
        {
            var dt = Time.deltaTime;
            
            var headDiff = dt / 2;
            var hls = head.transform.localScale;
            head.transform.localScale = new Vector3(
                ZeroMin(hls.x - headDiff),
                ZeroMin(hls.y - headDiff),
                ZeroMin(hls.z - headDiff)
            );

            var bodyDiff = dt / 3;
            var bls = body.transform.localScale;
            body.transform.localScale = new Vector3(
                ZeroMin(bls.x - bodyDiff),
                ZeroMin(bls.y - bodyDiff),
                ZeroMin(bls.z - bodyDiff)
            );

            if (head.transform.localScale == Vector3.zero &&
                head.transform.localScale == Vector3.zero)
            {
                projectiles.Stop();
                SceneManager.LoadScene("MainScene");
            }
        }
    }

    private Vector3 GetContactPoint(int touchIndex)
    {
        var touch = Input.GetTouch(touchIndex);
        var ray = Camera.main.ScreenPointToRay(touch.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var hitObject = hit.collider.gameObject;
            if (hitObject == body || hitObject == head)
            {
                return hit.point;
            }
        }
        return Vector3.zero;
    }

    private void UpdateScale(float ratio)
    {
        head.transform.localScale = new Vector3(
            initialHeadScale.x,
            initialHeadScale.y * (2 - ratio),
            initialHeadScale.z
        );

        body.transform.localScale = new Vector3(
            initialBodyScale.x * ratio,
            initialBodyScale.y,
            initialBodyScale.z
        );
    }

    private float ZeroMin(float val)
    {
        return Mathf.Max(val, 0.0f);
    }
}
