using UnityEngine;
using System.Collections;

public class PlayerMarkerParent : MonoBehaviour {

    PlayerMarkerDebug[] DebugPoints;

	// Use this for initialization
	void Start() 
    {
        DebugPoints = FindObjectsOfType<PlayerMarkerDebug>();
        int activeMarkerIndex = Random.Range(0, DebugPoints.Length-1);

        int i = 0;
        foreach(PlayerMarkerDebug pMD in DebugPoints)
        {
            if(i != activeMarkerIndex)
                pMD.gameObject.SetActive(false);
            i++;
        }

        FindObjectOfType<BasicWhiskerNav>().Target = DebugPoints[activeMarkerIndex].gameObject;
	}

    public void SwapMarkers(PlayerMarkerDebug indexValue)
    {
        int i = 0;
        foreach (PlayerMarkerDebug pMD in DebugPoints)
        {
            if(indexValue.Equals(pMD))
            {
                break;
            }
            i++;
        }

        int activeMarkerIndex = Random.Range(0, DebugPoints.Length-1);

        while(activeMarkerIndex == i)
        {
            activeMarkerIndex = Random.Range(0, DebugPoints.Length-1); 
        }

        DebugPoints[i].gameObject.SetActive(false);
        DebugPoints[activeMarkerIndex].gameObject.SetActive(true);
        FindObjectOfType<BasicWhiskerNav>().Target = DebugPoints[activeMarkerIndex].gameObject;
    }
}
