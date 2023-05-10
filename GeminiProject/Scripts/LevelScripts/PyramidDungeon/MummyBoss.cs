using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public enum PressurePlateStages
{
    MovePressurePlateDown,
    MovePressurePlateUp
}

public class MummyBoss : MonoBehaviour
{
    private Coroutine turning;
    [SerializeField] private int playerHitAmount;
    [SerializeField] private GameObject teleport;
    [SerializeField] private BossLevel bossLevel;
    [SerializeField] private UtilitySO utils;

    [Header("Animation Variables")]
    [SerializeField] private Animator animator;
    [Range(0.1f, 1f)] [SerializeField] private float LerpSpeed;
    
    [Header("State / timing variables")]
    [SerializeField] private int state;
    [SerializeField] private float decisionTimer;
    [SerializeField] private float decisionTimerStartingTime;
    [SerializeField] private float playerHitTimer = 5f;

    [Header("Position Variables")]
    [SerializeField] private Vector3 yOffSet;
    [SerializeField] private Transform player;
    [SerializeField] private Transform currentPlatform;
    [SerializeField] private Transform innerPlatform;
    [SerializeField] private List<Transform> outerPlatforms;
    
    [Header("Prefabs")] [SerializeField]
    private GameObject ballPrefab;

    [Header("Visual Effects / UI")]
    [SerializeField] private List<WaveAttack> waveAttacks;
    [SerializeField] private List<GameObject> jumpIndicators;
    [SerializeField] private TextMeshProUGUI stageInfo;
    [SerializeField] private TextMeshProUGUI playerHits;
    [SerializeField] private GameObject PlayerDiedScreen;
    [SerializeField] private GameObject BossDiedScreen;

    // Start is called before the first frame update
    void Start()
    {
        decisionTimer = decisionTimerStartingTime;
        playerHitAmount = 3;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        decisionTimer -= Time.deltaTime;
        playerHitTimer -= Time.deltaTime;
        if (decisionTimer <= 0)
        {
            int rand = Random.Range(0, 1001);
            decisionTimer = ResetDecisionTimer() + ChooseAMoveSet(rand);
        }
    }
    
    private float ResetDecisionTimer()
    {
        return decisionTimerStartingTime - state * .3f;
    }
    
    #region Hit / death functions
    public void ChangeBossState(int change)
    {
        animator.Play("Hit");
        if (state + change == 3) BossDied();
        state += change;
        decisionTimer = 5f;
        stageInfo.text = state != 3 ? "Stage: " + (state + 1) : "Defeated";
        playerHitAmount = 3;
        UpdatePlayerChancesUI();
    }

    private void BossDied()
    {
        BossDiedScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void HitPlayer()
    {
        if (playerHitTimer > 0.0f) return;
        if (playerHitAmount - 1 == 0)
            PlayerDied();
        
        playerHitAmount--;
        playerHitTimer = 5f;
        UpdatePlayerChancesUI();
    }

    private void PlayerDied()
    {
        PlayerDiedScreen.SetActive(true);
        Time.timeScale = 0f;
    }
    
    private void UpdatePlayerChancesUI()
    {
        string hits = "Chances: ";
        for (int i = 0; i < playerHitAmount; i++)
            hits += "X";
        playerHits.text = hits;
    }
    #endregion

    #region Rotation functions
    private void RotateTowardObject(Vector3 objectsPosition)
    {
        if (turning != null)
            StopCoroutine(turning);
        
        turning = StartCoroutine(PerformRotation(objectsPosition));
    }

    private IEnumerator PerformRotation(Vector3 objectsPosition)
    {
        // Remove the y position so we only rotate on the Y axis
        objectsPosition = new Vector3(objectsPosition.x, 0, objectsPosition.z);
        Vector3 bossPos = new Vector3(transform.position.x, 0, transform.position.z);
        Quaternion lookRotation = Quaternion.LookRotation(objectsPosition - bossPos);
        float time = 0;

        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);
            time += Time.deltaTime;
            yield return new WaitForSeconds(0.01f);
        }
        
        turning = null;
    }
    #endregion

    #region State Move Sets
    private float ChooseAMoveSet(int rand)
    {
        if (state == 0)
            return StateZeroMoveSet(rand);

        if (state == 1)
            return StateOneMoveSet(rand);

        return StateTwoMoveSet(rand);
    }
    
    private float StateZeroMoveSet(int rand)
    {
        if (rand <= 400)
        {
            StartCoroutine(OuterJump(FindNewOuterPlatform()));
            return 3f;
        }
        
        if (rand >= 800)
        {
            StartCoroutine(StompAttack());;
            return 8f;
        }
        
        StartCoroutine(BallAttack());
        return 2f;
    }

    private float StateOneMoveSet(int rand)
    {
        if (rand <= 350)
        {
            StartCoroutine(OuterJump(FindNewOuterPlatform()));
            return 3f;
        }
        
        if (rand >= 700)
        {
            StartCoroutine(StompAttack());;
            return 8f;
        }
        
        StartCoroutine(BallAttack());
        return 2f;
    }

    private float StateTwoMoveSet(int rand)
    {
        if (rand <= 300)
        {
            StartCoroutine(OuterJump(FindNewOuterPlatform()));
            return 3f;
        }
        
        if (rand >= 650)
        {
            StartCoroutine(StompAttack());;
            return 8f;
        }
        
        StartCoroutine(BallAttack());
        return 2f;
    }
    #endregion

    #region Boss Moves
    private IEnumerator BallAttack()
    {
        RotateTowardObject(player.position);
        animator.Play("Ball Attack");
        yield return new WaitForSeconds(1.5f);
        
        Vector3 pos = transform.position;
        GameObject ball = Instantiate(ballPrefab, new Vector3(pos.x, pos.y + 9, pos.z), transform.rotation);
        ball.GetComponent<BossBallAttack>().SetupAttack(state);
    }

    private IEnumerator StompAttack()
    {
        RotateTowardObject(innerPlatform.position);
        animator.Play("Jump Attack");
        yield return new WaitForSeconds(1f);

        OuterWaveAttacks();
        Vector3 startingPos = transform.position;
        float time = 0f;
        while (time < 1)
        {
            transform.position = utils.Arc(startingPos, innerPlatform.position + yOffSet, 10f, time);
            time += Time.deltaTime * (LerpSpeed * 2);
            yield return null;
        }
        currentPlatform = innerPlatform;
        waveAttacks[0].ActivateWaveAttack();

        yield return new WaitForSeconds(3f);
        StartCoroutine(OuterJump(FindNewOuterPlatform()));
    }

    private IEnumerator OuterJump(int index)
    {
        RotateTowardObject(outerPlatforms[index].position);
        animator.Play("Jump");
        yield return new WaitForSeconds(1f);
        
        bossLevel.BossJump((int)PressurePlateStages.MovePressurePlateUp);
        Vector3 startingPos = transform.position;
        float time = 0f;
        while (time < 1)
        {
            transform.position = utils.Arc(startingPos , outerPlatforms[index].position + yOffSet, 50f, time);
            time += Time.deltaTime * LerpSpeed;
            yield return null;
        }
        currentPlatform = outerPlatforms[index];
        bossLevel.BossJump((int)PressurePlateStages.MovePressurePlateDown);
        
        RotateTowardObject(player.position);
    }

    private void OuterWaveAttacks()
    {
        int amount = (state + 1) * 3;
        List<WaveAttack> temp = waveAttacks.ToList();
        for (int i = 0; i < amount; i++)
        {
            int location = Random.Range(1, temp.Count);
            temp[location].ActivateWaveAttack();
            temp.RemoveAt(location);
        }
    }
    
    private int FindNewOuterPlatform()
    {
        int index = Random.Range(0, outerPlatforms.Count);
        while (currentPlatform == outerPlatforms[index])
            index = Random.Range(0, outerPlatforms.Count);

        return index;
    }
    #endregion
}
