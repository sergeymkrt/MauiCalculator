using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using KursayinCalculator.Core;

namespace KursayinCalculator.ViewModels;

public class KeyPadViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    public ObservableCollection<ExpressionHistoryModel> Expressions { get; private set; } = new ObservableCollection<ExpressionHistoryModel>();
    #region Commands
    public ICommand AddCharCommand { get; private set; }
    public ICommand DeleteCharCommand { get; private set; }
    public ICommand ClearTextCommand { get; private set; }
    public ICommand EvaluateExpressionCommand { get; private set; }
    public ICommand RefreshCommand { get; private set; }
    #endregion

    #region Properties
    private bool _isRefreshing;
    private string _inputString = "0";
    private string _displayText = "0";

    public bool IsRefreshing { get { return _isRefreshing; }
        set
        {
            _isRefreshing = value;
            OnPropertyChanged();
        }
    }

    public string InputString
    {
        get => _inputString;
        private set
        {
            if (_inputString != value)
            {
                _inputString = value;
                OnPropertyChanged();
                DisplayText = _inputString;

                // Perhaps the delete button must be enabled/disabled.
                ((Command)DeleteCharCommand).ChangeCanExecute();
                ((Command)ClearTextCommand).ChangeCanExecute();
                ((Command)EvaluateExpressionCommand).ChangeCanExecute();
            }
        }
    }

    public string DisplayText
    {
        get => _displayText;
        private set
        {
            if (_displayText != value)
            {
                _displayText = value;
                OnPropertyChanged();
            }
        }
    }
    #endregion


    public KeyPadViewModel()
    {
        // Command to add the key to the input string
        AddCharCommand = new Command<string>(key =>
        {
            if (InputString == "0")
            {
                InputString = key;
            }
            else
            {
                InputString += key;
            }
        });

        // Command to delete a character from the input string when allowed
        DeleteCharCommand =
            new Command(
                // Command will strip a character from the input string
                () => InputString = InputString.Substring(0, InputString.Length - 1),

                // CanExecute is processed here to return true when there's something to delete
                () => InputString.Length > 0
            );
        ClearTextCommand =
            new Command(
                //Clearing the InputString
                () => InputString = "0",

                //CanExecute, execute only when there is something to delete
                () => InputString.Length > 0
            );
        EvaluateExpressionCommand =
            new Command(
                //Evaluating the expression
                () => {
                    EvaluateExpression();
                    IsRefreshing = false;
                },
                
                //CanExecute, execute only when there is something to evaluate
                () => InputString.Length > 0
            );
        RefreshCommand =
            new Command(
                () => {
                    Expressions.Clear();
                    IsRefreshing = false;
                },
                () => InputString.Length > 0
                );
    }

    #region Methods

    void EvaluateExpression()
    {
        var expression = InputString.Clone() as string;
        var value = Calculator.EvaluateExpression(expression);
        InputString = value.ToString(CultureInfo.InvariantCulture);
        Expressions.Insert(0,new ExpressionHistoryModel(expression, value));
    }

    // string FormatText(string str)
    // {
    //     bool hasNonNumbers = str.IndexOfAny(_specialChars) != -1;
    //     string formatted = str;
    //
    //     // Format the string based on the type of data and the length
    //     if (hasNonNumbers || str.Length < 4 || str.Length > 10)
    //     {
    //         // Special characters exist, or the string is too small or large for special formatting
    //         // Do nothing
    //     }
    //
    //     else if (str.Length < 8)
    //         formatted = string.Format("{0}-{1}", str.Substring(0, 3), str.Substring(3));
    //
    //     else
    //         formatted = string.Format("({0}) {1}-{2}", str.Substring(0, 3), str.Substring(3, 3), str.Substring(6));
    //
    //     return formatted;
    // }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion
}