
public class ViewParam
{
   
}

public class HomeViewParam : ViewParam
{

}

[System.Serializable]
public class MatchViewParam : ViewParam
{
    public ConfigWeaponRecord configGun;
    public int levelGun;
    public int idSkin;
    public int levelSkin;
}
