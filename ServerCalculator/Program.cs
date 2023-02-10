// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

//set Variable Referance value
Dictionary<string, object> d1 = new();
d1.Add("x", 5);
d1.Add("y", 10);

//First Calculation
Operation o1 = new Operation(new Constant(6), '*', new VarReferance("x"));
Console.WriteLine(o1.Evaluate(d1));




//Second Calculation
Operation o2 = new Operation(new Constant(300), '/', new VarReferance("y"));
Console.WriteLine(o2.Evaluate(d1));

//thired Calculater
Operation o3 = new(new Operation(new Constant(6), '*', new VarReferance("x")), '-', new Operation(new Constant(300), '/', new VarReferance("y")));
Console.WriteLine(o3.Evaluate(d1));

#region content for using abstract,override

public abstract class Expression
{
    public abstract double Evaluate(Dictionary<string, object> x);
}


public class Constant : Expression
{
    private readonly double _value;
    public Constant(double value)
    {
        _value = value;
    }
    public override double Evaluate(Dictionary<string, object> x)
    {
        return _value;
    }
}



public class VarReferance : Expression
{
    private readonly string _name;
    public VarReferance(string name)
    {
        _name = name;
    }

    public override double Evaluate(Dictionary<string, object> vars)
    {
        object res = vars[_name] ?? throw new Exception($"the Key {_name} is not Found");
        return Convert.ToDouble(res);
    }

}

public class Operation : Expression
{
    private readonly Expression _left;
    private readonly char _operation;
    private readonly Expression _right;

    public Operation(Expression left, char operation, Expression right)
    {
        _left = left;
        _operation = operation;
        _right = right;
    }
    public override double Evaluate(Dictionary<string, object> vars)
    {
        var x = _left.Evaluate(vars);
        var y = _right.Evaluate(vars);

        switch (_operation)
        {
            case '+': return x + y;
            case '*': return x * y;
            case '-': return x - y;
            case '/': return x / y;
            case '%': return x % y;

            default: throw new Exception("Unknown Operator");
        }
    }
}
#endregion