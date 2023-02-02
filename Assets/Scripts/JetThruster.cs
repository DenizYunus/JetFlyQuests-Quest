using UnityEngine;

public class JetThruster : MonoBehaviour
{
    Rigidbody thrustedRigidbody;
    LineRenderer laserLineRenderer;

    [Header("Variables")]
    public int thrustPower = 10;
    Vector3 forceVector;

    public float laserWidth = 0.1f;
    public float laserMaxLength = 5f;

    [Space(10)]
    [Header("Drag Drop")]
    ParticlesController particlesController;
    [SerializeField] GameObject thrustedObject;
    public OVRInput.Controller controller;

    bool lastPrimaryIndexTouchTemp = false;
    bool lastPrimaryHandButtonTemp = false;
    bool lastPrimaryIndexButtonTemp = false;

    bool raycastHitting = false;
    RaycastHit raycastHit;


    void Start()
    {
        forceVector = new Vector3(-thrustPower, 0, 0);
        thrustedRigidbody = thrustedObject.GetComponent<Rigidbody>();

        laserLineRenderer = GetComponentInChildren<LineRenderer>();
        Vector3[] initLaserPositions = new Vector3[2] { Vector3.zero, Vector3.zero };
        laserLineRenderer.SetPositions(initLaserPositions);
        laserLineRenderer.startWidth = laserWidth;
        laserLineRenderer.endWidth = laserWidth;

        particlesController = GetComponentInChildren<ParticlesController>();
    }

    void FixedUpdate()
    {
        if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, controller) || Input.GetKey(KeyCode.W))
        {
            if (!lastPrimaryHandButtonTemp)
            {
                particlesController?.EnableParticles();
            }
            thrustedRigidbody?.AddForceAtPosition(gameObject.transform.rotation * forceVector, gameObject.transform.position);
        }
        else
        {
            particlesController?.DisableParticles();
            lastPrimaryHandButtonTemp = false;
        }


        if (OVRInput.Get(OVRInput.Touch.PrimaryIndexTrigger, controller))
        {
            if (!lastPrimaryIndexTouchTemp)
            {
                laserLineRenderer.enabled = true;
            }
            ShootLaserFromTargetPosition(laserLineRenderer.transform.position + (laserLineRenderer.transform.right * 0.1f), laserLineRenderer.transform.right, laserMaxLength);
        }
        else
        {
            laserLineRenderer.enabled = false;
            lastPrimaryIndexTouchTemp = false;
        }

        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, controller))
        {
            if (!lastPrimaryIndexButtonTemp && raycastHitting)
            {
                lastPrimaryIndexButtonTemp = true;
                raycastHit.transform.gameObject.TryGetComponent(out GameObjectButtonAction act);
                act?.action.Invoke();
            }
        }
        else
        {
            lastPrimaryIndexButtonTemp = false;
        }
    }

    void ShootLaserFromTargetPosition(Vector3 targetPosition, Vector3 direction, float length)
    {
        Ray ray = new Ray(targetPosition, direction);
        Vector3 endPosition = targetPosition + (length * direction);

        if (Physics.Raycast(ray, out raycastHit, length))
        {
            endPosition = raycastHit.point;
            raycastHitting = true;
        }
        else raycastHitting = false;

        laserLineRenderer.SetPosition(0, targetPosition);
        laserLineRenderer.SetPosition(1, endPosition);
    }

    public int ThrustPower { get { return thrustPower; } set { thrustPower = value; forceVector = new Vector3(-thrustPower, 0, 0); } }
}
