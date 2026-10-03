namespace Minecraft_Alloy_Calculator;

public class WeakBlueSteelIngot : Alloy
{
    public WeakBlueSteelIngot()
    {
        Name = EAlloyName.WeakBlueSteelIngot;
        alloyInformation = new Dictionary<Alloy, float[]>()
        {
            {new BlackSteel(), [0.5f,0.55f]},
            {new Steel(), [0.2f,0.25f]},
            {new SterlingSilver(), [0.1f,0.15f]},
            {new BismuthBronze(), [0.1f, 0.15f]}
        };
    }
}