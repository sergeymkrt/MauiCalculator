namespace KursayinCalculator.Core;

public class Calculator
{
    private static int precedence(char op)
    {
        switch (op)
        {
            case '*':
            case '/':
            case '%':
                return 3;
            case '+':
            case '-':
                return 2;
            case '^':
                return 1;
            default:
                return -1;
        }
    }
    
    
}