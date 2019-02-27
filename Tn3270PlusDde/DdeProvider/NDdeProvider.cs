using System;
using NDde;
using NDde.Client;
using StEn.Tn3270PlusDde.Exceptions;

namespace StEn.Tn3270PlusDde.DdeProvider
{
    public class NDdeProvider : IDdeProvider
    {
        private string serviceName = "tn3270";
        private int timeout;

        public NDdeProvider(int timeout)
        {
            this.timeout = timeout;
        }

        public bool DdePoke(string topic, string item, string data)
        {
            /*
             * ArgumentException  This is thown when item exceeds 255 characters or timeout is negative.
             * ArgumentNullException  This is thrown when item or data is a null reference.
             * InvalidOperationException  This is thrown when the client is not connected.
             * DdeException  This is thrown when the server does not process the data.
             */

            try
            {
                using (DdeClient client = new DdeClient(this.serviceName, topic))
                {
                    // Connect to the server.  It must be running or an exception will be thrown.
                    client.Connect();
                    client.Poke(item, data, this.timeout);

                    // NDde poke only supports void -> return fake bool
                    return true;
                }
            }
            catch (ArgumentNullException e)
            {
                throw new Tn3270PlusDdeException(e.Message, "Either 'item' or 'data' are pointing to null.", e);
            }
            catch (ArgumentException e)
            {
                throw new Tn3270PlusDdeException(e.Message, "Item is exceeding 255 characters or the timeout value is negative.", e);
            }
            catch (InvalidOperationException e)
            {
                throw new Tn3270PlusDdeException(e.Message, "The DDE client is not connected to the server.", e);
            }
            catch (DdeException e)
            {
                throw new Tn3270PlusDdeException(e.Message, "The server could not process the request.", e);
            }
            catch (Exception e)
            {
                throw new Tn3270PlusDdeException(e.Message, "An unknown exception occured.", e);
            }
        }

        public string DdeRequest(string topic, string item)
        {
            /*
             * ArgumentException  This is thown when item exceeds 255 characters or timeout is negative.
             * ArgumentNullException  This is thrown when item or data is a null reference.
             * InvalidOperationException  This is thrown when the client is not connected.
             * DdeException  This is thrown when the server does not process the data.
             */
            try
            {
                using (DdeClient client = new DdeClient(this.serviceName, topic))
                {
                    // Connect to the server.  It must be running or an exception will be thrown.
                    client.Connect();
                    return client.Request(item, this.timeout).TrimEnd('\0');
                }
            }
            catch (ArgumentNullException e)
            {
                throw new Tn3270PlusDdeException(e.Message, "Either 'item' or 'data' are pointing to null.", e);
            }
            catch (ArgumentException e)
            {
                throw new Tn3270PlusDdeException(e.Message, "Item is exceeding 255 characters or the timeout value is negative.", e);
            }
            catch (InvalidOperationException e)
            {
                throw new Tn3270PlusDdeException(e.Message, "The DDE client is not connected to the server.", e);
            }
            catch (DdeException e)
            {
                throw new Tn3270PlusDdeException(e.Message, "The server could not process the request.", e);
            }
            catch (Exception e)
            {
                throw new Tn3270PlusDdeException(e.Message, "An unknown exception occured.", e);
            }
        }
    }
}
