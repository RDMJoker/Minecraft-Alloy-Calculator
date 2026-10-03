namespace Minecraft_Alloy_Calculator;

public class BlackBronze : Alloy
{
    public BlackBronze()
    {
        Name = EAlloyName.BlackBronze;
        alloyInformation = new Dictionary<Alloy, float[]>()
        {
            {new Copper(), [0.5f,0.7f]},
            {new Silver(), [0.1f,0.25f]},
            {new Gold(), [0.1f,0.25f]}
        };
    }
}