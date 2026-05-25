namespace Calculator
{
    public partial class MainPage : ContentPage
    {
        private double accumulator = 0;
        private double operand = 0;
        private string currentOperator = "";   // "+", "-", "*", "/"
        private string currentInput = "";       // Det tal användaren skriver just nu
        private bool justPressedEquals = false; // För att hantera fortsatt inmatning efter =

        public MainPage()
        {
            InitializeComponent();
            EntryUnder.Text = "0";
        }

        private void NumberButton(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            // Om man precis tryckt "=" och skriver ett nytt tal → börja om
            if (justPressedEquals)
            {
                accumulator = 0;
                currentOperator = "";
                EntryOver.Text = "";
                justPressedEquals = false;
            }

            currentInput += button.Text;
            EntryUnder.Text = currentInput; // Visa vad användaren skriver
        }

        private void OperatorButton(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            justPressedEquals = false;

            // Om användaren skrivit ett tal, behandla det
            if (currentInput != "")
            {
                operand = double.Parse(currentInput);

                if (currentOperator == "")
                {
                    // Första talet → sätt som accumulator
                    accumulator = operand;
                }
                else
                {
                    // Räkna ut med föregående operator
                    Calculate();
                }

                currentInput = "";
            }

            currentOperator = button.Text;
            EntryOver.Text += $"{operand} {currentOperator} ";
            EntryUnder.Text = accumulator.ToString(); // Visa löpande resultat
        }

        private void EqualButton(object sender, EventArgs e)
        {
            if (currentInput != "")
            {
                operand = double.Parse(currentInput);
            }

            Calculate();

            EntryOver.Text += $"{operand} =";
            EntryUnder.Text = accumulator.ToString();

            currentInput = "";
            currentOperator = "";
            justPressedEquals = true; // Markera att = precis trycktes
        }

        private void Calculate()
        {
            switch (currentOperator)
            {
                case "+":
                    accumulator += operand;
                    break;
                case "-":
                    accumulator -= operand;
                    break;
                case "*":
                case "x":       // Hanterar både "x" (XAML) och "*"
                    accumulator *= operand;
                    break;
                case "/":
                    if (operand == 0)
                    {
                        DisplayAlert("Fel!", "Division med noll är ej tillåtet.", "OK");
                        Clear();
                        return;
                    }
                    accumulator /= operand;
                    break;
            }
        }

        private void ClearButton(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            accumulator = 0;
            operand = 0;
            currentOperator = "";
            currentInput = "";
            justPressedEquals = false;
            EntryOver.Text = "";
            EntryUnder.Text = "0";
        }

        public void CEButton(object sender, EventArgs e)
        {
            // CE rensar bara aktuell inmatning, inte hela uttrycket
            currentInput = "";
            EntryUnder.Text = "0";
        }

        public void DecimalButton(object sender, EventArgs e)
        {
            if (!currentInput.Contains(",") && !currentInput.Contains("."))
            {
                currentInput += ",";
                EntryUnder.Text = currentInput;
            }
        }
    }
}