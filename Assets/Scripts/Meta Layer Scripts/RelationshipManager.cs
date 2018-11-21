using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class RelationshipManager : MonoBehaviour {

    public Slider moneySlider;
    public Slider votesSlider;

    public float votes;
    public float money;

    public Button button1;
    public Button button2;

    public List<int> levels;

    private void Start()
    {
        UpdateUI();
        GenerateButtons();
        CheckSquads();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            ChangeVotes(0.1f);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            ChangeMoney(0.1f);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            GenerateButtons();
        }
    }

    public void ChangeVotes(float amount)
    {
        votes += amount;
        UpdateUI();
    }

    public void ChangeMoney(float amount)
    {
        money += amount;
        UpdateUI();
    }

    public void UpdateUI()
    {
        votesSlider.value = votes;
        moneySlider.value = money;

        if (votes < 0.1 || money < 0.1)
        {
            Debug.Log("You Lose!");
        }
    }

    public void GenerateButtons()
    {
        GenerateEvent(button1,true);
        GenerateEvent(button2,false);
    }

    public void GenerateEvent(Button buttonToChange, bool positive)
    {
        float randNum = Random.value;
        randNum = (randNum - 0.5f) / 2;
        randNum = RoundToSF(randNum, 2);

        if (Mathf.Sign(randNum) == 1f && positive == false)
        {
            randNum = randNum * -1f;
        }else if (Mathf.Sign(randNum) == -1f && positive == true)
        {
            randNum = randNum * -1f;
        }

        //Debug.Log(randNum.ToString());

        //Change the text of the button
        buttonToChange.GetComponentInChildren<Text>().text = "M " + randNum + " | V " + (-randNum);
        buttonToChange.onClick.RemoveAllListeners();
        buttonToChange.onClick.AddListener(delegate { ChangeMoney(randNum); });
        buttonToChange.onClick.AddListener(delegate { ChangeVotes(-randNum); });
        buttonToChange.onClick.AddListener(GenerateButtons);
        /*
        ChangeVotes(randNum);
        ChangeMoney(-randNum);*/
    }

    float RoundToSF(float value, int digits)
    {
        if (value == 0.0f)
        {
            return (0.0f);
        }

        float scale = Mathf.Pow(10, digits);
        return Mathf.Round(value * scale) / scale;
    }

    public void CheckSquads()
    {
        for (int i = 0; i < GameManager.instance.playerSquads.Count; i++)
        {
            int multiplier = 1;
            if (GameManager.instance.playerSquads[i].dead)
            {
                //remove some score
                multiplier = -1;
            }

            if (GameManager.instance.playerSquads[i].type == PlayerType.Money)
            {
                ChangeMoney(0.1f * multiplier);
            }
            else
            {
                ChangeVotes(0.1f * multiplier);
            }
        }
    }

    public void StartLevel()
    {
        int level = GameManager.instance.level;
        if (level < levels.Count)
        {
            SceneManager.LoadScene(levels[level]);
            GameManager.instance.level += 1;
        }
    }
}
