using JrpShared.Data;
using System.Collections.Generic;
using static JrpServer.Log;

namespace JrpServer.Controllers
{
    sealed class GameController : IController
    {
        public IDictionary<string, IJob> Jobs;
        public IDictionary<string, IItem> Items;

        public void Boot()
        {
            Jobs = new Dictionary<string, IJob>();
            Items = new Dictionary<string, IItem>();

            RegisterNewJob("disoccupato", false, false, 50);

            RegisterNewItem("medkit", 0, true, true);
        }

        public void Init()
        {

        }

        public void RegisterNewJob(string title, bool isEMS, bool isPolice, uint pay)
        {
            if (Jobs.ContainsKey(title))
                WriteToConsole($"Non è stato possibile registrare il lavoro {title}", MessageType.Error);
            else
            {
                Jobs.Add(title, new Job(title, isEMS, isPolice, pay));

                WriteToConsole($"Lavoro registrato {title}");
            }
        }

        public void RegisterNewItem(string name, uint value, bool isLegal, bool canBeLooted)
        {
            if (Items.ContainsKey(name))
                WriteToConsole($"Non è stato possibile registrare l'oggetto {name}", MessageType.Error);
            else
            {
                Items.Add(name, new Item(name, value, isLegal, canBeLooted));

                WriteToConsole($"Oggetto registrato {name}");
            }
        }
    }
}
