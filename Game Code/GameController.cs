using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public enum Choice { Attack, Defend, Grab, Rest }
public enum BuffType { None, HadesHeal, HadesReflect, HadesUndying, PoseidonExtra, PoseidonStun, PoseidonDouble, ZeusFlat, ZeusProc, ZeusSplash }

public class GameController : MonoBehaviour
{

    [Header("Action Buttons + Counters")]
    public Button attackButton, defendButton, grabButton, restButton;
    public TextMeshProUGUI attackCounterText, defendCounterText, grabCounterText, restCounterText;

    [Header("Animation")]
    public Animator playerAnimator;

    [Header("Boss Setup")]
    public GameObject[] bossPrefabs;
    public Transform bossSpawnPoint;

    GameObject currentBossInstance;
    Animator enemyAnimator;

    [Header("CPU Move Counter Display")]
    public TextMeshProUGUI cpuMoveCounterText;

    [Header("Health UI")]
    public Slider playerHealthBar, enemyHealthBar;
    public TextMeshProUGUI playerHealthText, enemyHealthText, resultText;

    [Header("Buff Selection UI")]
    public GameObject buffPanel;
    public Button buffButton1, buffButton2, buffButton3;
    public TextMeshProUGUI buffButton1Text, buffButton2Text, buffButton3Text;
    public TextMeshProUGUI playerBuffsText;

    [Header("Cutscene UI")]
    public GameObject cutscenePanel;
    public Image cutsceneBackground;
    public TextMeshProUGUI dialogueText;
    public Button continueButton;
    [Tooltip("Array of 3 cutscene backgrounds: [0] = Hades, [1] = Poseidon, [2] = Zeus")]
    public Sprite[] cutsceneBackgrounds; 

    [Header("Backgrounds")]
    public Image backgroundImage;
    public Sprite[] levelBackgrounds; 

    [Header("Game Settings")]
    public int maxHealth = 10;
    public static int[] levelEnemyHealths = { 10, 12, 15 };
    public int damagePerWin = 2;
    public float buttonCooldown = 1.5f;
    public int totalLevels = 5;
    // public int enemyHealthIncreasePerLevel = 5;

    [Header("Base Move Limits (Level 1)")]
    public int baseAttackUses = 4;
    public int baseDefendUses = 4;
    public int baseGrabUses = 4;
    public int baseRestUses = 1;

    [Header("Per-Level Move Increments")]
    public int attackPerLevel = 1;   
    public int defendPerLevel = 1;   
    public int grabPerLevel = 1;     

    int currentLevel = 1;
    int playerHealth, enemyHealth;

    int attackUsesLeft, defendUsesLeft, grabUsesLeft, restUsesLeft;
    int cpuAttackUsesLeft, cpuDefendUsesLeft, cpuGrabUsesLeft;

    // --- Buff state variables (player-only) ---
    bool onFatalShield = false;
    System.Action onDefendEffect = null;
    int grabExtraDamage = 0;
    bool stunOnGrab = false;
    float grabDoubleChance = 0f;
    int attackFlatBonus = 0;
    float attackProcChance = 0f;
    bool splashOnAttack = false;
    private List<string> selectedBuffs = new List<string>();

    // --- Cutscene variables ---
    private Dictionary<string, string[]> godDialogues;
    private string[] currentDialogue;
    private int currentDialogueIndex;
    private bool inCutscene = false;

    void Start()
    {
        buffPanel.SetActive(false);
        cutscenePanel.SetActive(false);

        attackButton.onClick.AddListener(() => TryPlay(Choice.Attack));
        defendButton.onClick.AddListener(() => TryPlay(Choice.Defend));
        grabButton.onClick.AddListener(() => TryPlay(Choice.Grab));
        restButton.onClick.AddListener(() => TryPlay(Choice.Rest));

        continueButton.onClick.AddListener(AdvanceDialogue);

        InitializeDialogues();
        StartLevel();
    }

    void InitializeDialogues()
    {
        godDialogues = new Dictionary<string, string[]>
        {
            ["Hades"] = new string[]
            {
                "Mortal... you've come to the Underworld in these dark times.",
                "Chronos has corrupted everything, even my beloved Cerberus.",
                "My poor dog is under his control... I cannot bring myself to attack him.",
                "I am bound by Chronos' curse, unable to strike back at my own companion.",
                "Please, save me from this torment and I will grant you a blessing for your journey to reclaim Olympus!"
            },
            ["Poseidon"] = new string[]
            {
                "Hero... Chronos has awakened my greatest shame.",
                "He has corrupted Medusa, once a beautiful priestess before... before my transgression.",
                "The guilt of what I caused her weighs heavy on my soul.",
                "Now Chronos uses her curse against us both - she cannot control her deadly gaze.",
                "Free her from this torment, and the seas will forgive us both. Help me right this ancient wrong!"
            },
            ["Zeus"] = new string[]
            {
                "Champion... Chronos has turned my most loyal servants against me.",
                "The Cyclops, who forged my very thunderbolts, now serve the Titan.",
                "These noble craftsmen once helped me defeat his kind in ages past.",
                "Now their hammers strike for him, their lightning empowers my enemy.",
                "Break their chains, restore their freedom, and together we shall forge victory anew!"
            },
            ["Final"] = new string[]
            {
                "You have done it, brave hero! Chronos has been defeated!",
                "You brought the gods together to defeat him.",
                "You will be remembered as the greatest champion in the history of the gods!",
                "Thank you for bringing peace back to our realm."
            }
        };
    }

    void TryPlay(Choice choice)
    {
        if (buffPanel.activeSelf || inCutscene) return;

        switch (choice)
        {
            case Choice.Attack:
                if (attackUsesLeft-- <= 0) return;
                break;
            case Choice.Defend:
                if (defendUsesLeft-- <= 0) return;
                break;
            case Choice.Grab:
                if (grabUsesLeft-- <= 0) return;
                break;
            case Choice.Rest:
                if (restUsesLeft-- <= 0) return;
                break;
        }

        PlayRound(choice);
    }

    void PlayRound(Choice playerChoice)
    {
        if (IsCpuExhausted())
        {
            resultText.text = "Opponent Exhausted. You Win!";
            enemyHealth = 0; 
            UpdateHealthUI();
            Invoke(nameof(EndLevel), buttonCooldown);
            return;
        }


        switch (playerChoice)
        {
            case Choice.Attack:
                playerAnimator.SetTrigger("Attack");
                break;
            case Choice.Defend:
                playerAnimator.SetTrigger("Defend");
                break;
            case Choice.Grab:
                playerAnimator.SetTrigger("Grab");
                break;
            case Choice.Rest:
                playerAnimator.SetTrigger("Rest");
                break;
        }

        DisableActionButtons();

        Choice cpuChoice = GetCpuChoice();

        // ─── BOSS ACTION ANIMATION ────────────────────────────────────────
        switch (cpuChoice)
        {
            case Choice.Attack:
                enemyAnimator.SetTrigger("Attack");
                break;
            case Choice.Defend:
                enemyAnimator.SetTrigger("Defend");
                break;
            case Choice.Grab:
                enemyAnimator.SetTrigger("Grab");
                break;
        }
        // ────────────────────────────────────────────────────────────────

        bool skipEnemy = false;
        if (stunOnGrab && playerChoice == Choice.Grab)
        {
            skipEnemy = true;
            stunOnGrab = false;
        }

        var (pDmg, eDmg) = GetOutcome(playerChoice, cpuChoice);

        if (playerChoice == Choice.Defend && onDefendEffect != null)
            onDefendEffect.Invoke();
        if (playerChoice == Choice.Grab)
        {
            pDmg += grabExtraDamage;
            if (Random.value < grabDoubleChance) pDmg += damagePerWin;
            if (skipEnemy) eDmg = 0;
        }
        if (playerChoice == Choice.Attack)
        {
            pDmg += attackFlatBonus;
            if (Random.value < attackProcChance) pDmg += damagePerWin;
            if (splashOnAttack) enemyHealth = Mathf.Max(0, enemyHealth - 1);
        }
        if (playerChoice == Choice.Rest)
        {
            attackUsesLeft++;
            defendUsesLeft++;
            grabUsesLeft++;
            UpdateMoveUI(); 
        }


        int oldHealth = playerHealth;
        int oldBossHp = enemyHealth;

        playerHealth = Mathf.Max(0, playerHealth - pDmg);
        enemyHealth = Mathf.Max(0, enemyHealth - eDmg);


        if (oldHealth > 0 && playerHealth <= 0)
        {
            playerAnimator.SetTrigger("Death");
        }
        else if (playerHealth < oldHealth)
        {
            playerAnimator.SetTrigger("Hurt");
        }

        if (playerHealth == 0 && onFatalShield)
        {
            playerHealth = 1;
            onFatalShield = false;
        }

        // ─── BOSS HURT / DEATH ANIMATION ─────────────────────────────────
        if (oldBossHp > 0 && enemyHealth == 0)
        {
            enemyAnimator.SetTrigger("Death");
        }
        else if (enemyHealth < oldBossHp)
        {
            enemyAnimator.SetTrigger("Hurt");
        }
        // ────────────────────────────────────────────────────────────────

        UpdateHealthUI();
        UpdateMoveUI();

        string res = pDmg > eDmg ? "You Lose…" : eDmg > pDmg ? "You Win!" : "Draw!";
        resultText.text = $"You: {playerChoice} – Enemy: {cpuChoice}\n{res}";

        if (playerHealth == 0 || enemyHealth == 0)
        {
            Invoke(nameof(EndLevel), buttonCooldown);
        }
        else
        {
            Invoke(nameof(EnableActionButtons), buttonCooldown);
        }
    }

    Choice GetCpuChoice()
    {
        List<Choice> options = new List<Choice>();
        if (cpuAttackUsesLeft > 0) options.Add(Choice.Attack);
        if (cpuDefendUsesLeft > 0) options.Add(Choice.Defend);
        if (cpuGrabUsesLeft > 0) options.Add(Choice.Grab);

        int idx = Random.Range(0, options.Count);
        Choice pick = options[idx];

        switch (pick)
        {
            case Choice.Attack: cpuAttackUsesLeft--; break;
            case Choice.Defend: cpuDefendUsesLeft--; break;
            case Choice.Grab: cpuGrabUsesLeft--; break;
        }
        return pick;
    }

    (int p, int e) GetOutcome(Choice p, Choice e)
    {
        int b = damagePerWin;
        int q = Mathf.Max(1, b / 4), f = b, x = b + 1;

        if (p == Choice.Attack && e == Choice.Attack) return (1, 1);
        else if (p == Choice.Attack && e == Choice.Defend) return (q, 0);
        else if (p == Choice.Attack && e == Choice.Grab) return (0, x);
        else if (p == Choice.Defend && e == Choice.Attack) return (0, q);
        else if (p == Choice.Defend && e == Choice.Grab) return (f, 0);
        else if (p == Choice.Grab && e == Choice.Attack) return (x, 0);
        else if (p == Choice.Grab && e == Choice.Defend) return (0, f);
        else return (0, 0);
    }

    void EndLevel()
    {
        DisableActionButtons();

        if (enemyHealth == 0 && currentLevel < totalLevels)
        {
            resultText.text = $"Level {currentLevel} cleared!\nChoose your {GetGodName(currentLevel)} buff:";
            ShowBuffOptions();
        }
        else if (enemyHealth == 0)
        {
            buffPanel.SetActive(false);
            ShowCutscene("Final");
        }
        else
        {
            resultText.text = "Defeated… Game Over.";
        }
    }

    void ShowBuffOptions()
    {
        buffPanel.SetActive(true);
        buffButton1.onClick.RemoveAllListeners();
        buffButton2.onClick.RemoveAllListeners();
        buffButton3.onClick.RemoveAllListeners();

        string god = GetGodName(currentLevel);
        if (god == "Hades")
        {
            buffButton1Text.text = "Soul Shield\n(Heal +1 on Defend)";
            buffButton2Text.text = "Shadow Thorns\n(Reflect 1 dmg)";
            buffButton3Text.text = "Undying Will\n(Survive fatal hit)";
            buffButton1.onClick.AddListener(() => ChooseBuff(BuffType.HadesHeal));
            buffButton2.onClick.AddListener(() => ChooseBuff(BuffType.HadesReflect));
            buffButton3.onClick.AddListener(() => ChooseBuff(BuffType.HadesUndying));
        }
        else if (god == "Poseidon")
        {
            buffButton1Text.text = "Tidal Pull\n(Grab +1 dmg)";
            buffButton2Text.text = "Stun Current\n(Grab stuns)";
            buffButton3Text.text = "Wave Surge\n(25% double Grab)";
            buffButton1.onClick.AddListener(() => ChooseBuff(BuffType.PoseidonExtra));
            buffButton2.onClick.AddListener(() => ChooseBuff(BuffType.PoseidonStun));
            buffButton3.onClick.AddListener(() => ChooseBuff(BuffType.PoseidonDouble));
        }
    }

    void ChooseBuff(BuffType b)
    {
        buffPanel.SetActive(false);
        ApplyBuff(b);

        string buffDescription = b switch
        {
            BuffType.HadesHeal     => "Soul Shield (Heal +1 on Defend)",
            BuffType.HadesReflect  => "Shadow Thorns (Reflect 1 dmg)",
            BuffType.HadesUndying  => "Undying Will (Survive fatal hit)",
            BuffType.PoseidonExtra => "Tidal Pull (Grab +1 dmg)",
            BuffType.PoseidonStun  => "Stun Current (Grab stuns)",
            BuffType.PoseidonDouble=> "Wave Surge (25% double Grab)",
            BuffType.ZeusFlat      => "Lightning Strike (+2 Attack)",
            BuffType.ZeusProc      => "Chain Bolt (10% extra Attack)",
            BuffType.ZeusSplash    => "Storm's Wrath (Attack splash -1)",
            _                     => "Unknown Buff"
        };
        selectedBuffs.Add(buffDescription);

        currentLevel++;
        Invoke(nameof(StartLevel), 0.5f);
    }

    void ApplyBuff(BuffType b)
    {
        switch (b)
        {
            case BuffType.HadesHeal:
                onDefendEffect += () => playerHealth = Mathf.Min(playerHealth + 1, maxHealth);
                break;
            case BuffType.HadesReflect:
                onDefendEffect += () => enemyHealth = Mathf.Max(enemyHealth - 1, 0);
                break;
            case BuffType.HadesUndying:
                onFatalShield = true;
                break;
            case BuffType.PoseidonExtra:
                grabExtraDamage += 1;
                break;
            case BuffType.PoseidonStun:
                stunOnGrab = true;
                break;
            case BuffType.PoseidonDouble:
                grabDoubleChance = 0.25f;
                break;
            case BuffType.ZeusFlat:
                attackFlatBonus += 2;
                break;
            case BuffType.ZeusProc:
                attackProcChance = 0.10f;
                break;
            case BuffType.ZeusSplash:
                splashOnAttack = true;
                break;
        }
    }

    void StartLevel()
    {
        attackUsesLeft = baseAttackUses + (currentLevel - 1) * attackPerLevel;
        defendUsesLeft = baseDefendUses + (currentLevel - 1) * defendPerLevel;
        grabUsesLeft   = baseGrabUses   + (currentLevel - 1) * grabPerLevel;
        restUsesLeft   = 1;

        cpuAttackUsesLeft = attackUsesLeft;
        cpuDefendUsesLeft = defendUsesLeft;
        cpuGrabUsesLeft   = grabUsesLeft;

        playerHealth = maxHealth;
        enemyHealth  = levelEnemyHealths[currentLevel - 1];
        UpdateHealthUI();
        UpdateMoveUI();
        UpdateBackground();

        // ── BOSS SPAWN HERE ────────────────────────────────────────
        if (currentBossInstance != null)
            Destroy(currentBossInstance);

        currentBossInstance = Instantiate(
            bossPrefabs[currentLevel - 1],
            bossSpawnPoint != null ? bossSpawnPoint.position : Vector3.zero,
            Quaternion.identity
        );
        enemyAnimator = currentBossInstance.GetComponent<Animator>();
        // ────────────────────────────────────────────────────────────────
        if (currentLevel <= 3)
        {
            ShowCutscene(GetGodName(currentLevel));
        }
        else
        {
            resultText.text = $"Level {currentLevel}: Choose your move!";
            EnableActionButtons();
        }
    }

    void ShowCutscene(string godName)
    {
        inCutscene = true;
        cutscenePanel.SetActive(true);
        buffPanel.SetActive(false);
        DisableActionButtons();


        if (cutsceneBackgrounds != null && cutsceneBackgrounds.Length >= 3)
        {
            int backgroundIndex = godName switch
            {
                "Hades"   => 0,    
                "Poseidon"=> 1,    
                "Zeus"    => 2,    
                _         => 0
            };

            if (cutsceneBackgrounds[backgroundIndex] != null)
                cutsceneBackground.sprite = cutsceneBackgrounds[backgroundIndex];
        }
        else
        {
            Debug.LogWarning("Cutscene backgrounds array must contain exactly 3 sprites for Hades, Poseidon, and Zeus!");
        }

       
        currentDialogue = godDialogues[godName];
        currentDialogueIndex = 0;
        dialogueText.text = currentDialogue[currentDialogueIndex];
    }

    void UpdateHealthUI()
    {
        playerHealthBar.value = (float)playerHealth / maxHealth;
        enemyHealthBar.value  = (float)enemyHealth  / levelEnemyHealths[currentLevel - 1];
        playerHealthText.text = $"{playerHealth}/{maxHealth}";
        enemyHealthText.text  = $"{enemyHealth}/{levelEnemyHealths[currentLevel - 1]}";
    }

    void UpdateMoveUI()
    {
        attackCounterText.text = $"{attackUsesLeft}/{baseAttackUses + (currentLevel - 1) * attackPerLevel}";
        defendCounterText.text = $"{defendUsesLeft}/{baseDefendUses + (currentLevel - 1) * defendPerLevel}";
        grabCounterText.text   = $"{grabUsesLeft}/{baseGrabUses + (currentLevel - 1) * grabPerLevel}";
        restCounterText.text   = $"{restUsesLeft}/1";

        cpuMoveCounterText.text =
            "CPU Moves\n" +
            $"Attack: x{cpuAttackUsesLeft}\n" +
            $"Defend: x{cpuDefendUsesLeft}\n" +
            $"Grab: x{cpuGrabUsesLeft}";

        playerBuffsText.text = selectedBuffs.Count > 0
            ? "Player Buffs:\n" + string.Join("\n", selectedBuffs)
            : "Player Buffs:\nNone";
    }

    void UpdateBackground()
    {
        if (backgroundImage != null && levelBackgrounds.Length >= currentLevel)
            backgroundImage.sprite = levelBackgrounds[currentLevel - 1];
    }

    void DisableActionButtons()
    {
        attackButton.interactable = false;
        defendButton.interactable = false;
        grabButton.interactable   = false;
        restButton.interactable   = false;
    }

    void EnableActionButtons()
    {
        attackButton.interactable = (attackUsesLeft > 0);
        defendButton.interactable = (defendUsesLeft > 0);
        grabButton.interactable   = (grabUsesLeft > 0);
        restButton.interactable   = (restUsesLeft > 0);
    }

    bool IsCpuExhausted()
    {
        return cpuAttackUsesLeft <= 0 && cpuDefendUsesLeft <= 0 && cpuGrabUsesLeft <= 0;
    }

    string GetGodName(int lvl)
    {
        return lvl == 1 ? "Hades"
             : lvl == 2 ? "Poseidon"
             : lvl == 3 ? "Zeus"
             : "God";
    }

    void AdvanceDialogue()
    {
        if (currentDialogueIndex < currentDialogue.Length - 1)
        {
            currentDialogueIndex++;
            dialogueText.text = currentDialogue[currentDialogueIndex];
        }
        else
        {
            EndCutscene();
        }
    }

    void EndCutscene()
    {
        inCutscene = false;
        cutscenePanel.SetActive(false);

        if (currentLevel > totalLevels)
        {
            resultText.text = "All gods defeated! You win!";
        }
        else
        {
            resultText.text = $"Level {currentLevel}: Choose your move!";
            EnableActionButtons();
        }
    }
}