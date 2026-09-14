using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager> {
    public GameObject cardSelectionUI;
    public Button[] cardButtons;

    public void ShowCardSelectionUI(bool show) {
        cardSelectionUI.SetActive(show);
    }

    public void InjectCardInfo() {
        List<Card> cardList = new List<Card>(CardManager.Instance.cardPool);
        
        for (int i = 0; i < cardButtons.Length; i++) {
            Card card = cardList[i];
            Button button = cardButtons[i];
            TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
            
            if(card.cardType == CardType.NewWeapon) {
                buttonText.text = card.weaponType.ToString();
            } else if(card.cardType == CardType.WeaponUpgrade) {
                buttonText.text = card.weaponType.ToString() + " " + card.weaponAbilityType.ToString();
            } else if(card.cardType == CardType.SpeedBoost) {
                buttonText.text = card.cardType.ToString();
            }
            
            buttonText.fontSize = 40;
            button.onClick.RemoveAllListeners();

            int capturedIndex = i;
            button.onClick.AddListener(() => OnCardSelected(capturedIndex));
        }
    }

    public void OnCardSelected(int index) {
        Card selectedCard = new List<Card>(CardManager.Instance.cardPool)[index];
        CardManager.Instance.ApplyCard(selectedCard);
        ShowCardSelectionUI(false);
        
        GameManager gameManager = GameManager.Instance;
        gameManager.PauseGame(false);
    }
}
