using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class WorldVariables
{
   public static int currentlyCollectedNumber = 0;
   public static int schoolRepaired = 0;
   public static bool isInCutscene;
   public static bool startOfGame;

   public static int GetCurrentlyCollectedNumber()
   {
      return currentlyCollectedNumber;
   }
   
   public static int GetSchoolRepairStatus()
   {
      return schoolRepaired;
   }

   public static void SetCurrentlyCollected(int newCollected)
   {
      currentlyCollectedNumber += newCollected;
   }
   public static void SetRepairStatus(int newStatus)
   {
      schoolRepaired += newStatus;
   }

   public static void ResetStats()
   {
      currentlyCollectedNumber = 0;
      schoolRepaired = 0;
      startOfGame = true;
   }
}
