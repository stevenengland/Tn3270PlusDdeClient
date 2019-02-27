namespace StEn.Tn3270PlusDde.DdeProvider
{
    public interface IDdeProvider
    {
        string DdeRequest(string topic, string item);

        bool DdePoke(string topic, string item, string data);
    }
}
