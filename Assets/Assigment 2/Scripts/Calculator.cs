using UnityEngine;
using TMPro;

public class Calculator : MonoBehaviour
{
    public TextMeshProUGUI label;

    public float prevInput;

    public bool clearPrevInput = false;

    private EquationType equationType;

    private void Start()
    {
        Clear();
    }
    /// <summary>
    /// Checks to see if display needs to be cleared, then adds string input from button press to current string in display
    /// </summary>
    /// <param name="input"></param>
    public void AddInput(string input)
    {
        if (clearPrevInput == true) 
        {
            label.text = string.Empty;
            clearPrevInput = false;
        }
        //amends newest button input to end of string in display
        label.text = label.text + input;
    }
    /// <summary>
    /// Parses current displayed string and saves as prevInput
    /// </summary>
    public void parsePrevInput()
    {
        prevInput = float.Parse(label.text);
        clearPrevInput = true;
    }

    //Sets operation to addition, subtraction, multiplication, or division, and calls for the current string to be parsed and saved
    #region SET EQUATION AS
    public void SetEquationAsAdd()
    {
        parsePrevInput();
        equationType = EquationType.ADD;
    }

    public void SetEquationAsSubtract()
    {
        parsePrevInput();
        equationType = EquationType.SUBTRACT;
    }

    public void SetEquationAsMultiply()
    {
        parsePrevInput();
        equationType = EquationType.MULTIPLY;
    }

    public void SetEquationAsDivide()
    {
        parsePrevInput();
        equationType = EquationType.DIVIDE;
    }
    #endregion

    //Contains operation logic and displays output
    #region OPERATIONS
    public void Add()
    {
        float currentInput = float.Parse(label.text);
        label.text = (currentInput + prevInput).ToString();
    }

    public void Subtract()
    {
        float currentInput = float.Parse(label.text);
        label.text = (prevInput - currentInput).ToString();
    }

    public void Multiply()
    {
        float currentInput = float.Parse(label.text);
        label.text = (currentInput * prevInput).ToString();
    }

    public void Divide()
    {
        float currentInput = float.Parse(label.text);
        label.text = (prevInput/currentInput).ToString();
    }
    #endregion

    /// <summary>
    /// Resets display and previous inputs upon click
    /// </summary>
    public void Clear()
    {
        label.text = string.Empty;
        clearPrevInput = true;
        prevInput = 0;

        equationType = EquationType.None;        
    }
    /// <summary>
    /// Calls operation methods when = is clicked
    /// </summary>
    public void Calculate()
    {
        switch (equationType)
        {
            case EquationType.ADD:
                Add(); break;
            case EquationType.SUBTRACT:
                Subtract(); break;
            case EquationType.MULTIPLY:
                Multiply(); break;
            case EquationType.DIVIDE:
                Divide(); break;
        }
     }
            
    public enum EquationType
    {
        None = 0,
        ADD = 1,
        SUBTRACT = 2,
        MULTIPLY = 3,
        DIVIDE = 4
    }
}
