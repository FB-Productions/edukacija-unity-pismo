using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI Text")]
    [SerializeField] Text timeText;
    [SerializeField] Text populationText;
    [SerializeField] Text workerText;
    //[SerializeField] Text criminalText;
    [SerializeField] Text foodText;
    [SerializeField] Text woodText;
    [SerializeField] Text ironText;
    [SerializeField] Text goldText;
    [SerializeField] Text farmsText;
    [SerializeField] Text ironMineText;
    [SerializeField] Text notificationText;

    [Header("Values")]
    [SerializeField] int time = 1;
    [SerializeField] int population = 50;
    [SerializeField] int workers = 0;
    //[SerializeField] int criminals = 0;
    [SerializeField] int food = 10;
    [SerializeField] int wood = 5;
    [SerializeField] int iron = 0;
    [SerializeField] int gold = 0;
    [SerializeField] int farms = 0;
    [SerializeField] int ironMines = 0;
    [SerializeField] int day = 24;

    [Header("Buttons")]
    [SerializeField] Button huntButton;
    [SerializeField] Button woodButton;
    [SerializeField] Button ironButton;
    [SerializeField] Button exploreButton;
    [SerializeField] Button raidButton;
    [SerializeField] Button buyFoodButton;
    [SerializeField] Button buildFarmButton;
    [SerializeField] Button buildIronMineButton;
    [SerializeField] Button dismantleFarmButton;
    [SerializeField] Button dismantleIronMineButton;

    [Header("Animators")]
    [SerializeField] Animator huntAnimator;
    [SerializeField] Animator raidAnimator;

    [SerializeField] List<string> notifs = new List<string>();
    //[SerializeField] Queue<string> queue = new Queue<string>();
    //Queue<GameObject> queue = new Queue<GameObject>();

    bool isGameOver;
    bool isPlodan = true;

    private void Start()
    {
        notificationText.text = "";
        NewValues();
        StartCoroutine(DayIncrease());
        StartCoroutine(FoodLose());
        StartCoroutine(PopulationGain());
        StartCoroutine(RandomEventGenerator());
        StartCoroutine(FarmingFood());
        StartCoroutine(IronMining());
    }

    private void NewValues()
    {
        populationText.text = population.ToString();
        goldText.text = gold.ToString();
        foodText.text = food + " Kg";
        woodText.text = $"{wood} m";
        ironText.text = $"{iron} Kg";
        farmsText.text = farms + " farms";
        ironMineText.text = ironMines + " farms";
        //notificationText.text = "";
    }

    IEnumerator DayIncrease()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(day);
            time++;
            timeText.text = $"{time}. day";
        }
    }

    IEnumerator FoodLose()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(day);
            food -= (int) Random.Range((population + workers) * 0.3f, (population + workers) * 0.9f);
            foodText.text = food + "Kg";

            if(food <= 0)
            {
                if (population == 1)
                {
                    population = 0;
                }
                else
                {
                    population -= (int)Random.Range(population * 0.1f, population * 0.5f);
                }
                populationText.text = population.ToString();
                Notifications("\nNot enough food. Do something or lose.");
            }
        }
    }

    IEnumerator PopulationGain()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(Random.Range(day, day*3));

            if (isPlodan)
            {
                if (population > 2)
                {
                    int popBoost = Random.Range(0, 21);

                    population = population + popBoost;
                }

                populationText.text = population.ToString();
            }
        }
    }

    public void HuntButton()
    {
        Invoke(nameof(HuntedFood), day);
        huntButton.gameObject.SetActive(false);
        huntAnimator.SetBool("isStart", false);
    }

    private void HuntedFood()
    {
        int foodChange = Random.Range(-2, population);
        food += foodChange;
        foodText.text = food + " Kg";
        exploreButton.interactable = true;
    }

    public void GatherWoodButton()
    {
        Invoke(nameof(GatherWood), day / 2/*day * 2*/);
        woodButton.interactable = false;
    }

    private void GatherWood()
    {
        int woodChange = Random.Range(5, population);
        wood += woodChange;
        woodText.text = $"{wood} m";
        woodButton.interactable = true;
    }
    
    public void GatherIronButton()
    {
        Invoke(nameof(GatherIron), day * 2/*day * 6*/);
        ironButton.interactable = false;
    }

    private void GatherIron()
    {
        int ironChange = Random.Range(population/2, population);
        iron += ironChange;
        ironText.text = $"{iron} Kg";
        ironButton.interactable = true;
    }

    IEnumerator Taxes()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(day * 7);

            while (population >= 200)
            {
                TaxThemToDeath();
            }
        }
    }

    private void TaxThemToDeath()
    {
        int goldIncrease = (int)(population * 0.4f);
        gold += goldIncrease;
        goldText.text = gold.ToString();
    }

    public void SellWoodButton()
    {
        if(wood > 10)
        {
            int woodDecrease = 10;
            gold += 2;
            wood -= woodDecrease;
            woodText.text = $"{wood} m";
            goldText.text = gold.ToString();
            NewValues();
        }
        else
        {
            Notifications("\nGather more wood");
        }
    }

    public void SellIronButton()
    {
        if (iron >= 10)
        {
            int ironDecrease = 10;
            iron -= ironDecrease;
            gold += 6;
            ironText.text = iron + " Kg";
            NewValues();
        }

        else
        {
            Notifications("\nGather more iron.");
        }
    }


    public void ExplorationButton()
    {
        StartCoroutine(Explore());
    }

    IEnumerator Explore()
    {
        exploreButton.interactable = false;
        yield return new WaitForSeconds(day);

        int coinFlip = Random.Range(0, 3);

        if (coinFlip == 2)
        {
            huntButton.gameObject.SetActive(true);
            huntAnimator.SetBool("isStart", true);
            notificationText.text = "\nYou have discovered hunting grounds.";
        }
        else if (coinFlip == 1)
        {
            raidButton.gameObject.SetActive(true);
            raidAnimator.SetBool("isStart", true);
            notificationText.text = "\nYou have discovered an enemy establishment.";
        }
        else
        {
            exploreButton.interactable = true;
            Notifications("\nExploration was not successful.");
        }
    }
    
    public void RaidButton()
    {
        if (population >= 200)
        {
            Invoke(nameof(Raid), day * 4);
        }
        else
        {
            Notifications("\nMoš ga jebat");
            exploreButton.interactable = true;
        }
        raidButton.gameObject.SetActive(false);
        raidAnimator.SetBool("isStart", false);
    }

    private void Raid()
    {
        exploreButton.interactable = true;
        
        int coinFlip = Random.Range(0, 3);

        if (coinFlip == 0)
        {
            int populationChange = Random.Range(population / 2, population);
            int goldChange = Random.Range(gold / 2, gold);
            int foodChange = Random.Range(food / 2, food);
            int woodChange = Random.Range(wood / 2, wood);
            int ironChange = Random.Range(iron / 2, iron);

            population -= populationChange;
            gold -= goldChange;
            food -= foodChange;
            wood -= woodChange;
            iron -= ironChange;

            Notifications("\nGit Gud");

            NewValues();
        }
        else if (coinFlip == 1)
        {
            int populationChange = Random.Range(-population / 2, population / 2);
            int goldChange = Random.Range(-gold / 2, gold / 2);
            int foodChange = Random.Range(-food / 2, food / 2);
            int woodChange = Random.Range(-wood / 2, wood / 2);
            int ironChange = Random.Range(-iron / 2, iron / 2);

            population -= populationChange;
            gold -= goldChange;
            food -= foodChange;
            wood -= woodChange;
            iron -= ironChange;

            Notifications("\nSome times may be good, some times may be shit.");

            NewValues();

        }
        else
        {
            int populationChange = Random.Range(population / 2, population);
            int goldChange = Random.Range(gold / 2, gold);
            int foodChange = Random.Range(food / 2, food);
            int woodChange = Random.Range(wood / 2, wood);
            int ironChange = Random.Range(iron / 2, iron);

            population += populationChange;
            gold += goldChange;
            food += foodChange;
            wood += woodChange;
            iron += ironChange;

            Notifications("\nEverything is cache money");

            NewValues();
        }
    }

    IEnumerator RandomEventGenerator()
    {
        yield return new WaitForSeconds(Random.Range(day, day * 7));

        isPlodan = true; // resetira dugotrajne efekte proslih eventa

        int coinFlip = Random.Range(1, 101);

        if (coinFlip <= 7)
        {
            Flood();
        }
        else if (coinFlip <= 15)
        {
            Sifilis();
        }
        else if (coinFlip <= 25)
        {
            Festival();
        }
        else
        {

        }
    }

    private void Flood()
    {
        int populationChange = (int)Random.Range(population / 5, population-2);
        int foodChange = (int)Random.Range(food * 0.2f, food * 0.5f);
        int goldChange = (int)Random.Range(gold * 0.3f, gold * 0.7f);

        population -= populationChange;
        food -= foodChange;
        gold -= goldChange;

        int coinFlip = Random.Range(0, 2);
        if (coinFlip == 0)
        {
            Notifications("\nNije voda nego more");
        }
        else
        {
            Notifications("\nKako se zove ovo jezero");
        }

        NewValues();
    }

    private void Sifilis()
    {
        int populationChange = (int)(population * 0.69f);
        population -= populationChange;
        isPlodan = false;
        //Invoke(nameof(SifilisReset), day * 6.9f);
        
        int coinFlip = Random.Range(0, 2);
        if (coinFlip == 0)
        {
            Notifications("\nNisi pazio");
        }
        else
        {
            Notifications("\nBurn baby, burn!");
        }

        NewValues();
    }

    /*private void SifilisReset()
    {
        isPlodan = true;
    }*/

    private void Covid19()
    {
        int populationChange = (int)Random.Range(population * 0.25f, population * 0.4f);
    }

    private void Festival()
    {
        int populationChange = (int)Random.Range(population * 0.25f, population * 0.4f);
        int foodChange = (int)Random.Range(population * 0.1f, population * 0.25f);
        int goldChange = (int)Random.Range(population * 0.25f, population * 0.4f);

        population += populationChange;
        food -= foodChange;
        gold += goldChange;

        Notifications("\nDodji na Ultru [Includes Paid Promotion]");

        NewValues();

        int coinFlip = Random.Range(0, 5);

        if (coinFlip == 0)
        {
            Sifilis();
        }
    }

    public void BuyFood()
    {
        if (gold >= 10)
        {
            gold -= 10;
            food += 50;

            NewValues();
        }
        else
        {
            Notifications("Nema se para");
        }
    }

    public void BuildFarm()
    {
        if (wood >= 20 && iron >= 10 && population >= 2)
        {
            wood -= 20;
            iron -= 10;
            population -= 2;
            workers += 2;
            farms++;
            Notifications("We've built a farm.");
            NewValues();
        }
        else
        {
            Notifications("Not enough resources to build.");
        }
    }

    public void BuildIronMine()
    {
        if (wood >= 20 && gold >= 50 && population >= 5)
        {
            wood -= 20;
            gold -= 50;
            population -= 5;
            workers += 5;
            ironMines++;
            Notifications("We've built an iron mine.");
            NewValues();
        }
        else
        {
            Notifications("Not enough resources to build.");
        }
    }

    IEnumerator FarmingFood()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(day);
            int foodGain = Random.Range(0, 25) * farms;
            food += foodGain;
            gold -= 1;

            NewValues();
        }
    }

    IEnumerator IronMining()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(day);
            int foodChange = ironMines * 15;
            int woodChange = ironMines * 5;
            food -= foodChange;
            wood -= woodChange;
            iron += 20;

            NewValues();
        }
    }

    public void DismantleFarm()
    {
        if (farms >= 1)
        {
            farms--;
            workers -= 2;
            population += 2;
            NewValues();
            Notifications("The farm is no more.");
        }
    }

    public void DismantleIronMine()
    {
        if (ironMines >= 1)
        {
            ironMines--;
            workers -= 5;
            population += 5;
            NewValues();
            Notifications("The iron mine is no more.");
        }
    }

    private void Notifications(string notification)
    {
        /*
        if (textPrefab == null || textPosition == null)
        {
            return;
        }
        
        Text tempText = textPrefab.GetComponent<Text>();
        tempText.text = notification;

        GameObject tempObject = Instantiate(textPrefab, textPosition);

        queue.Enqueue(tempObject);

        if (queue.Count > 5)
        {
            GameObject oldQueue = queue.Dequeue();
            Destroy(oldQueue);
        }
        */
        
        /*queue.Enqueue(notification);

        notificationText.text = "";

        for (int i = 0; i < queue.Count; i++)
        {
            if (i >= 5)
            {
                queue.Dequeue();
            }
            else
            {
                notificationText.text += queue[i];
            }
        }*/
        
        notifs.Add(notification);

        notificationText.text = "";

        for (int i = notifs.Count-1; i >= 0; i--)
        {
            if (i >= 5)
            {
                //notifs[i].Replace(notifs[notifs.Count - i], notification);
                //Debug.Log("");
                notifs.RemoveAt(0);
            }
            else
            {
                notificationText.text += notifs[i] + "\n";
            }
        }
    }
}
