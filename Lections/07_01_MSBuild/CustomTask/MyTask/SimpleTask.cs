using Microsoft.Build.Utilities;

namespace MyTask
{
    public class SimpleTask : Task
    {
        public string Name { get; set; }

        public override bool Execute()
        {
            Log.LogMessage("Hello, {0}", Name);

            return true;
        }

    }
}
