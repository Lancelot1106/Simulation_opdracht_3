using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionEventArgs
{
    public Collider collider;
}

public class Sight : MonoBehaviour
{
    private const Modality MODALITY = Modality.SIGHT;
    
    [SerializeField] private float angle = 60f;
    [SerializeField] private float distance = 20f;
    [SerializeField] private float aspect = 2f;
    [SerializeField] private Color gizmoColor = Color.red;
    [SerializeField] private LayerMask layerMask;
    internal HashSet<Collider> inSight;
    private HashSet<Collider> lastInSight;
    private float coneRadius;

    public delegate void VisionEventHandler(object sender, VisionEventArgs args);
    public event VisionEventHandler OnEnterVision;

    void Awake()
    {
        inSight = new HashSet<Collider>();
        coneRadius = Mathf.Tan(Mathf.Deg2Rad *angle / 2f) * distance;
    }

    //private Collider[] overlaps;
    private Collider collider;
    private RaycastHit[] coneHits;
    private RaycastHit hitInfo;
    
    void FixedUpdate()
    {
        lastInSight = new HashSet<Collider>(inSight);
        
        //overlaps = Physics.OverlapSphere(this.transform.position, distance, layerMask);
        //foreach (Collider collider in overlaps)

        coneHits = ConeCast.ConeCastAll(this.transform.position, coneRadius, this.transform.forward, distance, angle);

        foreach (RaycastHit coneHit in coneHits)
        {
            collider = coneHit.collider;
            if(Physics.Raycast(this.transform.position, collider.transform.position - this.transform.position, out hitInfo, distance, layerMask)
            && hitInfo.collider == collider)
            {
                if (!inSight.Contains(collider))
                {
                    inSight.Add(collider);
                    //Debug.Log($"Sighted: {collider.gameObject.name}");
                    OnEnterVision.Invoke(this, new VisionEventArgs() {collider = collider});
                }
                else
                {
                    lastInSight.Remove(collider);
                }
            }
        }

        foreach (Collider collider in lastInSight)
        {
            inSight.Remove(collider);
            Debug.Log($"Lost sight: {collider.gameObject.name}");
        }
    }

    void OnDrawGizmos()
    {
        if (inSight != null)
        {
            Gizmos.color = Color.magenta;
            foreach (Collider collider in inSight)
            {
                Gizmos.DrawLine(this.transform.position, collider.transform.position);
            }
        }

        Gizmos.color = gizmoColor;
        Gizmos.matrix = Matrix4x4.TRS(this.transform.position, this.transform.rotation, this.transform.lossyScale);
        Gizmos.DrawFrustum(Vector3.zero, angle, distance, .5f, aspect);
    }
}
