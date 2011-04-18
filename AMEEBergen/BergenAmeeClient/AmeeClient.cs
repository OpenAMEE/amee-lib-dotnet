using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BergenAmee;
using BergenAmee.Model;

namespace BergenAmeeClient
{
    class AmeeClient
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AMEEBergen Test: Start");

            try
            {
                AmeeHelper.ameeCharSetName = /*"ISO-8859-1";//*/"UTF-8";
                AmeeHelper.authToken = "";
                AmeeHelper.ameeHost = "platform-science.amee.com";
                AmeeHelper.ameePort = 80;
                AmeeHelper.ameeLogin = "calexander";
                AmeeHelper.ameePassword = "is@stat1c";
                AmeeHelper.ameeAccept = "application/json";

                /* We could also read from the app settings - supplied by Zen'to

                   AmeeHelper.ameeHost = System.Configuration.ConfigurationManager.AppSettings["ameeHost"]; ;
                   AmeeHelper.ameePort = int.Parse(System.Configuration.ConfigurationManager.AppSettings["ameePort"]);
                   AmeeHelper.ameeLogin = System.Configuration.ConfigurationManager.AppSettings["ameeLogin"];
                   AmeeHelper.ameePassword = System.Configuration.ConfigurationManager.AppSettings["ameePassword"];
                   AmeeHelper.ameeCharSetName = System.Configuration.ConfigurationManager.AppSettings["ameeCharSetName"];
                   AmeeHelper.ameeAccept = System.Configuration.ConfigurationManager.AppSettings["ameeAccept"];

                */
                AmeeHelper.ipAddress = Dns.GetHostEntry(AmeeHelper.ameeHost);
                AmeeHelper.ip = new IPEndPoint(AmeeHelper.ipAddress.AddressList[0], AmeeHelper.ameePort);


                // Not obvious that this needed to be set from the code 
                // causes an exception fault if omitted - should be defaulted
                // Added from the Zen'to mail
                AmeeHelper.ameePrefix = "{\"";
                if (AmeeHelper.ameeAccept == "application/xml")
                {
                    AmeeHelper.ameePrefix = "<?xml";
                }

                AmeeHelper.authToken = AmeeHelper.getAuthToken();

                Console.WriteLine("AMEEBergen Test - token: " + AmeeHelper.authToken);


                // This is a simple create a profile 
                // Therefore we don't need the meterId - it's only meaningful
                // to Bergen
                String response = AmeeHelper.createProfile(12345678);

                Console.WriteLine("AMEEBergen Test - response: " + response);

                // parse the profile id out
                String profileId = AmeeHelper.getProfileId(response);
                Console.WriteLine("AMEEBergen Test - profileId: " + profileId);

                //get data item id :
                string dataPathDrillDown = "transport/ghgp/passenger/drill?type=taxi&subtype=none&class=none&region=other";
                string dataItemId = AmeeHelper.getDataItemId(AmeeHelper.getDataItemByDrillDown(dataPathDrillDown));
                Console.WriteLine("AMEEBergen Test - dataItemId: " + dataItemId);

                // set data item value
                String distance = "100"; // String for simplicity 
                String unit = "km"; // String for simplicity 
                String passenger = "1.0"; // String for simplicity 
                string body = "representation=full&name=bergen&dataItemUid=" + dataItemId + "&distance=" + distance + "&distanceUnit=" + unit + "&passengers=" + passenger;
                string dataPath = "transport/ghgp/passenger";
                String profileItemValueResponse = AmeeHelper.setProfilItemValues(profileId, dataPath, body);
                Console.WriteLine("AMEEBergen Test - profileItemValueResponse: " + profileItemValueResponse);

            }
            catch (Exception err)
            {
                Console.WriteLine("AMEEBergen Test - something screwed up: "+ err.Message);
            }

            Console.WriteLine("AMEEBergen Test: Completed Successfully");
        }
    }
}
