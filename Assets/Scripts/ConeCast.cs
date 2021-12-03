using System.Collections.Generic;
using UnityEngine;

public static class ConeCast
{
    public static RaycastHit[] ConeCastAll(Vector3 origin, float maxRadius, Vector3 direction, float maxDistance, float coneAngle)
    {
        RaycastHit[] sphereCastHits = Physics.SphereCastAll(origin - new Vector3(0, 0, maxRadius), maxRadius, direction, maxDistance);
        HashSet<RaycastHit> coneCastHits = new HashSet<RaycastHit>();
        
        if (sphereCastHits.Length > 0)
        {
            for (int i = 0; i < sphereCastHits.Length; i++)
            {
                Vector3 hitPoint = sphereCastHits[i].point;
                Vector3 directionToHit = hitPoint - origin;
                float angleToHit = Vector3.Angle(direction, directionToHit);

                if (angleToHit < coneAngle)
                {
                    coneCastHits.Add(sphereCastHits[i]);
                }
            }
        }

        RaycastHit[] result = new RaycastHit[coneCastHits.Count];
        coneCastHits.CopyTo(result);

        return result;
    }
}