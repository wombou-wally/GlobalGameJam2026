using System;

class MyRandom
{
    private static Random _instance;
    public static Random Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Random();
            }
            return _instance;
        }
    }
}