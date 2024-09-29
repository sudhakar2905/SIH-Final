using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public TextMeshProUGUI moneyText;         // Reference to the Text UI element to display money
    public int startingBalance = 1000;  // Initial amount of money the player has
    private int currentBalance;     // Current amount of money

    void Start()
    {
        currentBalance = startingBalance;
        UpdateMoneyUI(); // Update the UI at the start
    }

    // Function to update the displayed money value
    void UpdateMoneyUI()
    {
        moneyText.text = "Money: $" + currentBalance.ToString();
    }

    // Function to deduct cost and update the UI
    public bool DeductCost(int cost)
    {
        if (currentBalance >= cost)
        {
            currentBalance -= cost;
            UpdateMoneyUI();
            return true; // Successfully deducted cost
        }
        else
        {
            Debug.Log("Not enough money!");
            return false; // Failed to deduct cost due to insufficient funds
        }
    }

    // Function to add money (can be expanded for future features)
    public void AddMoney(int amount)
    {
        currentBalance += amount;
        UpdateMoneyUI();
    }
}
