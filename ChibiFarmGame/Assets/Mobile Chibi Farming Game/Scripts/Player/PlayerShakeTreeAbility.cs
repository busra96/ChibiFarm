using UnityEngine;

public class PlayerShakeTreeAbility : MonoBehaviour
{

    [Header(" Settings ")] 
    [SerializeField] private float distanceToTree;
    
    [Header(" Elements ")] 
    private AppleTree currentTree;
    
    private void Awake()
    {
        AppleTreeManager.onTreeModeStarted += TreeModeStartedCallback;
    }

    private void OnDestroy()
    {
        AppleTreeManager.onTreeModeStarted -= TreeModeStartedCallback;
    }

    private void TreeModeStartedCallback(AppleTree tree)
    {
        currentTree = tree;

        MoveTowardsTree();
    }

    private void MoveTowardsTree()
    {
        Vector3 treePos = currentTree.transform.position;
        Vector3 dir = transform.position - treePos;

        Vector3 flatDir = dir;
        flatDir.y = 0;

        Vector3 targetPos = treePos + flatDir.normalized * distanceToTree;

        LeanTween.move(gameObject, targetPos, .5f);

    }
}
