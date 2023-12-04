public static class CustomLevelConfiguration
{
    private const int MinTrackPartCount = 40;
    private const int StandardTrackPartCount = 80;
    private const int MaxTrackPartCount = 120;
    private const int MinLevel = 1;
    private const int StandardLevel = 2;
    private const int MaxLevel = 3;
    private const float MinTrackTime = 100f;
    private const float StandardTrackTime = 200f;
    private const float MaxTrackTime = 300f;


    private static int _trackLength = StandardLevel;
    private static int _trackPartCount = StandardTrackPartCount;
    private static int _difficultyLevel = StandardLevel;
    private static float _trackTime = StandardTrackTime;

    public static int TrackLength
    {
        get
        {
            return _trackLength;
        }


        set
        {
            switch (value) 
            {
                case MinLevel:
                    _trackLength = MinLevel;
                    _trackPartCount = MinTrackPartCount;
                    _trackTime = MinTrackTime;
                    break;

                case MaxLevel:
                    _trackLength = MaxLevel;
                    _trackPartCount = MaxTrackPartCount;
                    _trackTime = MaxTrackTime;
                    break;

                default:
                    _trackLength = StandardLevel;
                    _trackPartCount = StandardTrackPartCount;
                    _trackTime = StandardTrackTime;
                    break;
            }
        }
    }

    public static int DifficultyLevel
    {
        get
        {
            return _difficultyLevel;
        }

        set
        {
            if (value != MinLevel && value != MaxLevel)
                _difficultyLevel = StandardLevel;
            else
                _difficultyLevel = value;
        }
    }

    public static float TrackTime => _trackTime;
    public static int TrackPartCount => _trackPartCount;
}   