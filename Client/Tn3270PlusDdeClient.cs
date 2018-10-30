using NDde;
using NDde.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tn3270PlusDde.Exceptions;
using Tn3270PlusDde.Extensions;
using Tn3270PlusDde.Interfaces;

namespace Tn3270PlusDde.Client
{
    //http://www.sdisw.com/tn3270/manual/index.html
    //https://www.sdisw.com/customer_downloads.htm

    public class Tn3270PlusDdeClient
    {
        string _serviceName = "tn3270";

        public int ScreenRowLength { get; private set; } 
        public int Timeout { get; set; } = 5;

        public Tn3270PlusDdeClient(int screenRowLength)
        {
            if (screenRowLength < 1)
                throw new Tn3270PlusDdeException("The length of the screen row must be greater than 0.");
            ScreenRowLength = screenRowLength;
        }

        public string GetScreen(string topic, bool insertLinebreaks = false)
        {
            return (insertLinebreaks) ? GetScreen(topic, ScreenRowLength): DdeRequest(topic, "PS");
        }

        private string GetScreen(string topic, int rowLength)
        {
            string rawScreen = DdeRequest(topic, "PS");
            var parts = rawScreen.SplitInParts(rowLength);
            return String.Join(Environment.NewLine, parts);
        }

        public string GetActiveSessionNumber()
        {
            return DdeRequest("system", "activesession");
        }

        public string GetOpenSessionCount()
        {
            return DdeRequest("system", "sessions");
        }

        public void SendKeyStrokes(string topic, string keys)
        {
            DdePoke(topic, "keystroke", keys);
        }

        
        public void SetCursorPosition(string topic, int position)
        {
            DdePoke(topic, "cursor", position.ToString());
        }

        public void SetCursorPosition(string topic, int x, int y)
        {
            int position = (y-1) * ScreenRowLength + x;
            DdePoke(topic, "cursor", position.ToString());
        }

        private string DdeRequest(string topic, string item)
        {
            /*
             * ArgumentException  This is thown when item exceeds 255 characters or timeout is negative.
             * ArgumentNullException  This is thrown when item or data is a null reference.  
             * InvalidOperationException  This is thrown when the client is not connected.  
             * DdeException  This is thrown when the server does not process the data.  
             */
            try
            {
                using (DdeClient client = new DdeClient(_serviceName, topic))
                {
                    // Connect to the server.  It must be running or an exception will be thrown.
                    client.Connect();
                    return client.Request(item, Timeout).TrimEnd('\0');
                }
            }
            catch (ArgumentNullException e)
            {
                throw new Tn3270PlusDdeException(e.Message, "Das Item oder die Daten zeigen auf Null!", e);
            }
            catch (ArgumentException e)
            {
                throw new Tn3270PlusDdeException(e.Message, "Die Länge des Items ist größer als 255 Stellen oder Timeout ist negativ.", e);
            }
            catch (InvalidOperationException e)
            {
                throw new Tn3270PlusDdeException(e.Message, "Der DDE Client besitzt keine Verbindung zum Server.", e);
            }
            catch (DdeException e)
            {
                throw new Tn3270PlusDdeException(e.Message, "Der Server konnte die Anforderung nicht ausführen.", e);
            }
            catch (Exception e)
            {
                throw new Tn3270PlusDdeException(e.Message, "Eine unbekannte Ausnahme ist aufgetreten.", e);
            }
        }

        private void DdePoke(string topic, string item, string data)
        {
            /*
             * ArgumentException  This is thown when item exceeds 255 characters or timeout is negative.
             * ArgumentNullException  This is thrown when item or data is a null reference.  
             * InvalidOperationException  This is thrown when the client is not connected.  
             * DdeException  This is thrown when the server does not process the data.  
             */

            try
            {
                using (DdeClient client = new DdeClient(_serviceName, topic))
                {
                    // Connect to the server.  It must be running or an exception will be thrown.
                    client.Connect();
                    client.Poke(item, data, Timeout);
                }
            }
            catch (ArgumentNullException e)
            {
                throw new Tn3270PlusDdeException(e.Message, "Das Item oder die Daten zeigen auf Null!", e);
            }
            catch (ArgumentException e)
            {
                throw new Tn3270PlusDdeException(e.Message, "Die Länge des Items ist größer als 255 Stellen oder Timeout ist negativ.", e);
            }
            catch (InvalidOperationException e)
            {
                throw new Tn3270PlusDdeException(e.Message, "Der DDE Client besitzt keine Verbindung zum Server.", e);
            }
            catch (DdeException e)
            {
                throw new Tn3270PlusDdeException(e.Message, "Der Server konnte die Anforderung nicht ausführen.", e);
            }
            catch (Exception e)
            {
                throw new Tn3270PlusDdeException(e.Message, "Eine unbekannte Ausnahme ist aufgetreten.", e);
            }
        }

    }
}
