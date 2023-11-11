
public static class DataUtilities
{
    public static string ToKey(this object data)
    {
        return "K_" + data.ToString();
    }
}
