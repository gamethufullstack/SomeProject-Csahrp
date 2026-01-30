class Program
{
    static void Play(int freq, int duration)
    {
        Console.Beep(freq, duration);
        Thread.Sleep(20);
    }

    static void ThemeA()
    {
        Play(659, 300);
        Play(622, 300);
        Play(659, 300);
        Play(622, 300);
        Play(659, 300);
        Play(493, 300);
        Play(587, 300);
        Play(523, 400);

        Play(440, 600);
        Play(659, 600);
    }

    static void Bridge()
    {
        Play(440, 300);
        Play(493, 300);
        Play(523, 300);
        Play(587, 300);
        Play(659, 400);
        Play(587, 400);
    }

    static void Main()
    {
        for(int i = 0; i < 3; i++)
        {
            ThemeA();
            Bridge();
        }
        Play(659, 400);
        Play(622, 400);
        Play(659, 800);
    }
}