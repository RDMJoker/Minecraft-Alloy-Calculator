namespace Minecraft_Alloy_Calculator;

public class WeakRedSteelIngot : Alloy
{
    public WeakRedSteelIngot()
    {
        Name = EAlloyName.WeakRedSteelIngot;
        alloyInformation = new Dictionary<Alloy, float[]>()
        {
            { new BlackSteel(), [0.5f, 0.55f] },
            { new Steel(), [0.2f, 0.25f] },
            { new RoseGold(), [0.1f, 0.15f] },
            { new Brass(), [0.1f, 0.15f] }
        };
    }
}