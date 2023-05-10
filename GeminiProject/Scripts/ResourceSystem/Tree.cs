//Code written by Eric Valdez

using LevelScripts;
using Unity.VisualScripting;
using UnityEngine;

public class Tree : InteractableObject
{
    //Tree object and health creation
    //public GameObject currTree;
    private SO_TreeEvent treeChop;
    private SO_SoundManager soundManager;
    public GameObject treeLog;
    public GameObject treeStump;
    public int healthTree = 3;
    public SoundType[] rockSmash;
    public SoundType[] treeSmash;

    private void Awake()
    {
        soundManager = Resources.Load<SO_SoundManager>("SO_SoundManager");
        treeChop = Resources.Load<SO_TreeEvent>("Events/TreeChop");
        treeChop.OnEventCall += TreeChop;
    }

    private void OnDestroy()
    {
        treeChop.OnEventCall -= TreeChop;
    }

    private void TreeChop(Tree tree)
    {
        if (tree == GetComponent<Tree>())
        {
            if (descriptionString == "Mine")
            {
                var soundIndex = Random.Range(0, rockSmash.Length);
                soundManager.PlayOnGameObjectWithAudioSource(rockSmash[soundIndex], GameObject.FindGameObjectWithTag("Player"), true);
                Debug.Log("PLAY ROCK SOUND");
            }
            else if (descriptionString is "Chop" or "Chop Log")
            {
                var soundIndex = Random.Range(0, treeSmash.Length);
                soundManager.PlayOnGameObjectWithAudioSource(treeSmash[soundIndex], GameObject.FindGameObjectWithTag("Player"), true);
                Debug.Log("PLAY Tree SOUND");
            }

            healthTree -= 1;
        }

        CheckHealth();
    }

    private void CheckHealth()
    {
        //Check that tree still has health
        if(healthTree <= 0)
        {
            var itemTransform = gameObject.transform;
            var transformPosition = itemTransform.position;
            var transformRotation = itemTransform.rotation;
            //Instantiate the log and stump of chopped trees
            Instantiate(treeLog, transformPosition + transform.up * .5f, transformRotation);
            Instantiate(treeStump, transformPosition, transformRotation);

            //Destroy original tree object
            TreeDestruct();
        }
    }

    private void TreeDestruct()
    {
        endInteractableEvent.EventCall(this);
        GameObject.FindGameObjectWithTag("PlayerInteractVolume").GetComponent<TestSoftTarget>().RemoveObject(this);
        //left as a function to add more components such as spawning particles, etc.
        Destroy(gameObject);
    }

    public Tree(bool isPlayerInRange) : base(isPlayerInRange)
    {
    }
}
