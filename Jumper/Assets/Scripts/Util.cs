using System;

public static class Util
{
    public static int randomLeftOrRight()
    {
        Random rng = new Random();
        int num = rng.Next(-1, 1);
        if (num % 2 == 0)
            return 1;
        else
            return -1;
    }

    public static float NumToPerc(float number, float maxNumber)
    {
        return (float)Math.Round((number / maxNumber),2);
    }

    public static string FormatTime(float time)
    {
        int  t = (int)Math.Round(time);

        if (t < 60)
            return $"{t} sec";
        else
        {
            int sec = t % 60;
            int min = (int)Math.Floor((double)t / 60);
            if (sec == 0)
                return $"{min} min";
            else
                return $"{min} min {sec} sec";
        }    
    }

}

