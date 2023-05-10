using UnityEngine;

public class BossBallAttack : MonoBehaviour
{
    [SerializeField] private UtilitySO utils;
    [SerializeField] private bool isChild = true;
    [SerializeField] private float timer = 0f;
    [SerializeField] private float lerpSpeed = 0.3f;
    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private bool isSetup = false;
    [SerializeField] private int bossState;
    [SerializeField] private Vector3 endingPos;
    [SerializeField] private Vector3 startingPos;
    [SerializeField] private GameObject childPrefab;
    private bool alreadyCreated = false;

    private void Update()
    {
        if (!isSetup) return;
        if (timer > 1f && isChild) Destroy(gameObject);
        if (timer > 1f) return;
        
        timer += Time.deltaTime * lerpSpeed;
        transform.position = utils.Arc(startingPos, endingPos, jumpHeight,timer);
    }

    public void SetupAttack(int state)
    {
        bossState = state;
        startingPos = transform.position;
        endingPos = GameObject.FindWithTag("Player").transform.position;
        isSetup = true;
        isChild = false;
    }

    public void SetupChildMovement(int state, Vector3 end)
    {
        bossState = state;
        startingPos = transform.position;
        endingPos = end;
        timer = 0;
        jumpHeight = 1f;
        lerpSpeed *= 1.3f;
        isSetup = true;
        isChild = true;
    }

    private void CreateChildren()
    {
        Vector3 pos = transform.position;
        GameObject child_1 = Instantiate(childPrefab, new Vector3(pos.x, pos.y + .1f, pos.z + .5f), transform.rotation);
        GameObject child_2 = Instantiate(childPrefab, new Vector3(pos.x, pos.y + .1f, pos.z - .5f), transform.rotation);
        GameObject child_3 = Instantiate(childPrefab, new Vector3(pos.x + .5f, pos.y + .1f, pos.z), transform.rotation);
        GameObject child_4 = Instantiate(childPrefab, new Vector3(pos.x - .5f, pos.y + .1f, pos.z), transform.rotation);

        child_1.GetComponent<BossBallAttack>().SetupChildMovement(bossState, new Vector3(pos.x, pos.y, pos.z + 20));
        child_2.GetComponent<BossBallAttack>().SetupChildMovement(bossState, new Vector3(pos.x, pos.y, pos.z - 20));
        child_3.GetComponent<BossBallAttack>().SetupChildMovement(bossState, new Vector3(pos.x + 20, pos.y, pos.z));
        child_4.GetComponent<BossBallAttack>().SetupChildMovement(bossState, new Vector3(pos.x - 20, pos.y, pos.z));

        if (bossState != 2) return;
        
        GameObject child_5 = Instantiate(childPrefab, new Vector3(pos.x + .5f, pos.y + .1f, pos.z + .5f), transform.rotation);
        GameObject child_6 = Instantiate(childPrefab, new Vector3(pos.x - .5f, pos.y + .1f, pos.z - .5f), transform.rotation);
        GameObject child_7 = Instantiate(childPrefab, new Vector3(pos.x + .5f, pos.y + .1f, pos.z - .5f), transform.rotation);
        GameObject child_8 = Instantiate(childPrefab, new Vector3(pos.x - .5f, pos.y + .1f, pos.z + .5f), transform.rotation);

        child_5.GetComponent<BossBallAttack>().SetupChildMovement(bossState, new Vector3(pos.x + 20, pos.y, pos.z + 20));
        child_6.GetComponent<BossBallAttack>().SetupChildMovement(bossState, new Vector3(pos.x - 20, pos.y, pos.z - 20));
        child_7.GetComponent<BossBallAttack>().SetupChildMovement(bossState, new Vector3(pos.x + 20, pos.y, pos.z - 20));
        child_8.GetComponent<BossBallAttack>().SetupChildMovement(bossState, new Vector3(pos.x - 20, pos.y, pos.z + 20));
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball")) return;

        if (!isChild && !collision.gameObject.CompareTag("Player") && !alreadyCreated)
        {
            alreadyCreated = true;
            CreateChildren();
        }
        
        if(collision.gameObject.CompareTag("Player")) FindObjectOfType<MummyBoss>().HitPlayer();
        
        Destroy(gameObject);
    }
}
