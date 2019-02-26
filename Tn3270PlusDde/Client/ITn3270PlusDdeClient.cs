namespace Tn3270PlusDde.Client
{
    public interface ITn3270PlusDdeClient
    {
        string RequestPresentationSpace(string topic, bool insertLinebreaks = false);

        string RequestKeyboard(string topic);

        string RequestActiveSession();

        string RequestSessions();

        bool PokeKeystroke(string topic, string keys);

        bool PokeCursor(string topic, int position);

        bool PokeCursor(string topic, int x, int y);
    }
}
