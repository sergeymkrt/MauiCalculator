using System;
namespace KursayinCalculator.Core
{
	public class ExpressionHistoryModel
	{
		public string Expression { get; set; }
		public double Value { get; set; }
		public ExpressionHistoryModel(string expression, double value)
		{
			Expression = expression;
			Value = value;
		}
	}
}

