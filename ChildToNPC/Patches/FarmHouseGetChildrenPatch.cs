﻿using System.Collections.Generic;
using StardewValley.Characters;

namespace ChildToNPC.Patches
{
    /* Prefix for getChildren
     * The game uses the FarmHouse to check the children that the player has,
     * so when ChildToNPC removes them from the FarmHouse, the game loses track of them.
     * This method is patched so that the game has access to children who aren't in the farmhouse.
     */
    class FarmHouseGetChildrenPatch
    {
        public static void Postfix(ref List<Child> __result)
        {
            List<Child> allChildList = ModEntry.allChildren;
            List<Child> resultList = __result;

            // Check if allChildren has been initialized yet
            if (allChildList != null && allChildList.Count > 0)
            {
                // Start with the allChildren list instead
                resultList = allChildList;

                // Verify that allChildren contains all children
                foreach (Child child in __result)
                {
                    if (!allChildList.Contains(child))
                    {
                        ModEntry.monitor.Log("The allChildren list is missing a child: " + child.Name);
                        resultList.Add(child);
                    }
                }
            }

            __result = resultList;
        }
    }
}
