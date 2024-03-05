using System;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
   private void OnTriggerStay(Collider other)
   {
      if (other.CompareTag("ChunkTrigger"))
      {
         Chunk chunk = other.GetComponentInParent<Chunk>();
         chunk.TryUnlock();
      }
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.TryGetComponent(out AppleTree tree))
         TriggeredAppleTree(tree);
   }

   private void OnTriggerExit(Collider other)
   {
      if (other.TryGetComponent(out AppleTree tree))
         ExitedAppleTreeZone(tree);
   }

   private void TriggeredAppleTree(AppleTree tree)
   {
      Debug.Log(" We have entered a tree zone ");
   }

   private void ExitedAppleTreeZone(AppleTree tree)
   {
      Debug.Log(" We have exited a tree zone ");
   }
}
