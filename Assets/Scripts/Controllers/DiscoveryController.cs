using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoveryController : MonoBehaviour
{
    /*Note:
        The idea here is to have your current land immediatly available.
        To expand more (and also to get wood) you must destroy forests to make the land usable.
        Each area of land has a finite amount of forest, so the player must eventually use a scout
        to look for more land. You can always scout ahead, and rack up a bunch of discovered land 
        that can later be deforested and utilized. 

        Pending idea - You will eventually encounter other kingdoms that you will have to take
        land away from

        Discover land -> Deforest land -> build on land
    */

    //PUBLICS



    //PRIVATES
    //Amount of land available to be discovered
    private int totalLand = 2147483647;        //largest int readily available to unity
    private int discoveredLand = 1;
    private int ownedLand = 1;

    private static DiscoveryController mInstance;

    //TODO update for unity
    public static DiscoveryController GetInstance()
    {
        if (mInstance == null)
        {
            mInstance = new DiscoveryController();
        }
        return mInstance;
    }

}
