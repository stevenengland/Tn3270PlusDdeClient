using System;
using Tn3270PlusDde.DdeProvider;
using Tn3270PlusDde.Exceptions;
using Tn3270PlusDde.Extensions;

namespace Tn3270PlusDde.Client
{
    /*
    http://www.sdisw.com/tn3270/manual/index.html
    https://www.sdisw.com/customer_downloads.htm
    */

    public class Tn3270PlusDdeClient : ITn3270PlusDdeClient
    {
        private readonly IDdeProvider ddeProvider;

        public Tn3270PlusDdeClient(IDdeProvider ddeProvider)
        {
            this.ddeProvider = ddeProvider;
        }

        public Tn3270PlusDdeClient(IDdeProvider ddeProvider, int screenRowLength)
        {
            if (screenRowLength < 1)
            {
                throw new Tn3270PlusDdeException("The length of the screen row must be greater than 0.");
            }

            this.ddeProvider = ddeProvider;
            this.ScreenRowLength = screenRowLength;
        }

        public int ScreenRowLength { get; set; } = -1;

        public string RequestPresentationSpace(string topic, bool insertLinebreaks = false)
        {
            if (insertLinebreaks && this.ScreenRowLength < 1)
            {
                throw new Tn3270PlusDdeException("The length of the screen row must be greater than 0.");
            }

            return insertLinebreaks ? this.GetScreen(topic, this.ScreenRowLength) : this.ddeProvider.DdeRequest(topic, "PS");
        }

        public string RequestKeyboard(string topic)
        {
            return this.ddeProvider.DdeRequest(topic, "Keyboard");
        }

        public string RequestActiveSession()
        {
            return this.ddeProvider.DdeRequest("system", "activesession");
        }

        public string RequestSessions()
        {
            return this.ddeProvider.DdeRequest("system", "sessions");
        }

        public bool PokeKeystroke(string topic, string keys)
        {
            return this.ddeProvider.DdePoke(topic, "keystroke", keys);
        }

        public bool PokeCursor(string topic, int position)
        {
            return this.ddeProvider.DdePoke(topic, "cursor", position.ToString());
        }

        public bool PokeCursor(string topic, int x, int y)
        {
            int position = ((y - 1) * this.ScreenRowLength) + x;
            return this.ddeProvider.DdePoke(topic, "cursor", position.ToString());
        }

        private string GetScreen(string topic, int rowLength)
        {
            string rawScreen = this.ddeProvider.DdeRequest(topic, "PS");
            var parts = rawScreen.SplitInParts(rowLength);
            return string.Join(Environment.NewLine, parts);
        }
    }
}
