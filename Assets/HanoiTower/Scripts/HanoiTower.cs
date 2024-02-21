using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HanoiTower : MonoBehaviour
{
    [SerializeField] private GameObject peg1;
    [SerializeField] private GameObject peg2;
    [SerializeField] private GameObject peg3;
    [SerializeField] private Transform peg1Transform;
    [SerializeField] private Transform peg2Transform;
    [SerializeField] private Transform peg3Transform;
    [SerializeField] GameObject winLabel;
    [SerializeField] GameObject selectButton;
    [SerializeField] TextMeshProUGUI selectedLabel;
    [SerializeField] private int[] peg1data = { 1, 2, 3, 4 };
    [SerializeField] private int[] peg2data = { 0, 0, 0, 0 };
    [SerializeField] private int[] peg3data = { 0, 0, 0, 0 };

    [SerializeField] private int currentPeg = 1;

    
    public void SelectMoveRight()
    {
        if (currentPeg != 3) 
        {
            currentPeg++;
        }
        
    }

    public void SelectMoveLeft()
    {
        if (currentPeg != 1)
        {
            currentPeg--;
        }
    }

    [ContextMenu("Move Right")]
    void MoveRight()
    {
        //Make sure we aren't the right most peg
        if (CanMoveRight() == false) return;

        //Check to see what index and number we are moving from THIS peg
        int[] fromArray = GetPeg(currentPeg);
        int fromIndex = GetTopNumberIndex(fromArray);

        //If there wasn't anything to move then don't try to move
        if (fromIndex == -1) return;

        //Check to see where in the peg we are moving to that the number
        //should be placed into
        int[] toArray = GetPeg(currentPeg + 1);
        int toIndex = GetBottomNumberIndex(toArray);

        //If the adjacent peg is FULL then we cannot move anything into it
        //This probably will never happen since the max number of numbers
        //we have is the size of each peg
        if (toIndex == -1) return;

        //Lastly check to verify the number we are moving is not larger
        //than whatever number we may be placing this number on top of
        //on the adjacent peg
        if (CanAddToPeg(fromArray[fromIndex], toArray) == false) return;

        //If all checks PASS then go aheand and move the number
        //out of THIS array into the adjacent array
        MoveNumber(fromArray, fromIndex, toArray, toIndex);

        Transform disc = PopDiscFromPeg();
        Transform toPeg = GetPegTransform(currentPeg + 1);

        disc.SetParent(toPeg);
        disc.SetAsFirstSibling();
    }

    [ContextMenu("Move Left")]
    void MoveLeft()
    {
        //Make sure we aren't the left most peg
        if (CanMoveLeft() == false) return;

        //Check to see what index and number we are moving from THIS peg
        int[] fromArray = GetPeg(currentPeg);
        int fromIndex = GetTopNumberIndex(fromArray);

        //If there wasn't anything to move then don't try to move
        if (fromIndex == -1) return;

        //Check to see where in the peg we are moving to that the number
        //should be placed into
        int[] toArray = GetPeg(currentPeg - 1);
        int toIndex = GetBottomNumberIndex(toArray);

        //If the adjacent peg is FULL then we cannot move anything into it
        //This probably will never happen since the max number of numbers
        //we have is the size of each peg
        if (toIndex == -1) return;

        //Lastly check to verify the number we are moving is not larger
        //than whatever number we may be placing this number on top of
        //on the adjacent peg
        if (CanAddToPeg(fromArray[fromIndex], toArray) == false) return;

        //If all checks PASS then go aheand and move the number
        //out of THIS array into the adjacent array
        MoveNumber(fromArray, fromIndex, toArray, toIndex);

        Transform disc = PopDiscFromPeg();
        Transform toPeg = GetPegTransform(currentPeg - 1);

        disc.SetParent(toPeg);
        disc.SetAsFirstSibling();
    }

    Transform PopDiscFromPeg()
    {
        Transform currentPegTransform = GetPegTransform(currentPeg);
        int index = currentPegTransform.childCount; //finds top ring... pop!
        Transform disc = currentPegTransform.GetChild(0);
        return disc;
    }

    void MoveNumber(int[] fromArr, int fromIndex, int[] toArr, int toIndex)
    {
        int value = fromArr[fromIndex];
        fromArr[fromIndex] = 0;

        toArr[toIndex] = value;
    }

    bool CanMoveRight()
    {
        //If peg 1 or 2 then can move right
        return currentPeg < 3;
    }

    bool CanAddToPeg(int value, int[] peg)
    {
        int topNumberIndex = GetTopNumberIndex(peg);
        if(topNumberIndex == -1) return true;

        int topNumber = peg[topNumberIndex];
        return topNumber > value;
    }

    bool CanMoveLeft()
    {
        //If peg 2 or 3 then can move right
        return currentPeg > 1;
    }

    int[] GetPeg(int pegNumber)
    {
        if (pegNumber == 1) return peg1data;

        if (pegNumber == 2) return peg2data;

        return peg3data;
    }

    Transform GetPegTransform(int pegNumber)
    {
        //Button[] buttons = GameObject.FindObjectsByType

        return GameObject.Find($"Peg-{pegNumber}").transform;

        //if (pegNumber == 1) return peg1Transform;

        //if (pegNumber == 2) return peg2Transform;

        //return peg3Transform;
    }

    int GetTopNumberIndex(int[] peg)
    {
        for (int i = 0; i < peg.Length; i++)
        {
            if (peg[i] != 0) return i;
        }

        return -1;
    }

    int GetBottomNumberIndex(int[] peg)
    {
        for (int i = peg.Length - 1; i >= 0; i--)
        {
            if (peg[i] == 0) return i;
        }

        return -1;
    }

    private void Update()
    {
        WinCheck();
        selectedLabel.text = $"Currently Selected: Peg {currentPeg.ToString()}";
    }



    void WinCheck()
    {
        if (peg3data[0] == 1)
        {
            WinDisplay();
        }
    }

    void WinDisplay()
    {
        winLabel.SetActive(true);
    }
}
