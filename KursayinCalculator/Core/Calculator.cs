using System.Text.RegularExpressions;

namespace KursayinCalculator.Core;

public class Calculator
{
    public static double EvaluateExpression(string infixExpression)
    {
        var postfixExpression = Infix_To_Postfix(ref infixExpression);
        
        Stack<double> stack = new Stack<double>();
        string[] tokens = postfixExpression.Split(' ',StringSplitOptions.RemoveEmptyEntries);
        foreach (string token in tokens)
        {
            if (double.TryParse(token, out double value))
            {
                stack.Push(value);
            }
            else
            {
                double operand2 = stack.Pop();
                double operand1 = stack.Pop();
                switch (token)
                {
                    case "+":
                        stack.Push(operand1 + operand2);
                        break;
                    case "-":
                        stack.Push(operand1 - operand2);
                        break;
                    case "*":
                        stack.Push(operand1 * operand2);
                        break;
                    case "/":
                        stack.Push(operand1 / operand2);
                        break;
                    case "^":
                        stack.Push(Math.Pow(operand1,operand2));
                        break;
                    case "%":
                        stack.Push(operand1 % operand2);
                        break;
                }
            }
        }
        return stack.Pop();
    }
    
    private static string Infix_To_Postfix(ref string expn)
    {
        var stk = new Stack<char>();
        var output = "";
        char _out;
        foreach (var ch in expn)
        {
            var isAlphaBet = Regex.IsMatch(ch.ToString(), "[a-z]", RegexOptions.IgnoreCase);

            if (Char.IsDigit(ch) || isAlphaBet)
            {
                output += ch;
            }
            else
            {
                switch (ch)
                {
                    case '+':
                    case '-':
                    case '*':
                    case '/':
                    case '%':
                    case '^':
                        while (stk.Count > 0 && precedence(ch) <= precedence(stk.Peek()))
                        {
                            _out = stk.Peek();
                            stk.Pop();
                            output = output + " " + _out;
                        }
                        stk.Push(ch);
                        output += " ";
                        break;
                    case '(':
                        stk.Push(ch);
                        break;
                    case ')':
                        while (stk.Count > 0 && (_out = stk.Peek()) != '(')
                        {
                            stk.Pop();
                            output = output + " " + _out + " ";
                        }
                        if (stk.Count > 0 && (_out = stk.Peek()) == '(')
                            stk.Pop();
                        break;
                }
            }
        }
        while (stk.Count > 0)
        {
            _out = stk.Peek();
            stk.Pop();
            output = output+ " " + _out + " ";
        }
        return output;
    }
        
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