namespace KursayinCalculator.Core;

public struct Number
{
    public double Value { get; set; }

    public Number(double value)
    {
        Value = value;
    }
    
    public static Number operator +(Number a1, Number a2)
    {
        return new Number(a1.Value + a2.Value);
    }

    public static Number operator -(Number a1, Number a2)
    {
        return new Number(a1.Value - a2.Value);
    }

    public static Number operator /(Number a1, Number a2)
    {
        return new Number(a1.Value / a2.Value);
    }

    public static Number operator *(Number a1, Number a2)
    {
        return new Number(a1.Value * a2.Value);
    }

    public static Number operator ^(Number a1, Number a2)
    {
        return new Number(Math.Pow(a1.Value, a2.Value));
    }

    public static Number operator %(Number a1, Number a2)
    {
        return new Number(a1.Value % a2.Value);
    }
}