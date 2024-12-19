using System.Windows.Media;

namespace Werwolf.GameMenu;

public class GameMenuViewModel : ViewModelBase
{
    private SolidColorBrush _backgroundColor;
    private SolidColorBrush _foregroundColor;
    private bool _timeDay;

    public GameMenuViewModel()
    {
        TimeDay = true;
    }

    public SolidColorBrush BackgroundColor
    {
        get => _backgroundColor;
        set
        {
            if (_backgroundColor != value)
            {
                _backgroundColor = value;
                OnPropertyChanged();
            }
        }
    }

    public SolidColorBrush ForegroundColor
    {
        get => _foregroundColor;
        set
        {
            if (_foregroundColor != value)
            {
                _foregroundColor = value;
                OnPropertyChanged();
            }
        }
    }

    public bool TimeDay
    {
        get => _timeDay;
        set
        {
            if (_timeDay != value)
            {
                _timeDay = value;
                switch (_timeDay)
                {
                    case true:
                        BackgroundColor = new SolidColorBrush(Colors.WhiteSmoke);
                        ForegroundColor = new SolidColorBrush(Colors.Black);
                        break;
                    case false:
                        BackgroundColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1f1f1f"));
                        ForegroundColor = new SolidColorBrush(Colors.White);
                        break;
                }

                OnPropertyChanged();
            }
        }
    }
}