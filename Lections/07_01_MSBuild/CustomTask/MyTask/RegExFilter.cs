using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MyTask
{
    public class RegExFilter : Task
    {
        [Required]
        public ITaskItem[] InputStrings { get; set; }

        [Required]
        public string Filter { get; set; }

        [Output]
        public ITaskItem[] ResultStrings { get; set; }

        public override bool Execute()
        {
            var regEx = new Regex(Filter);
            var result = new List<TaskItem>();

            foreach (var value in InputStrings)
            {
                if (regEx.IsMatch(value.ItemSpec))
                    result.Add(new TaskItem(value));
                else
                    Log.LogWarning("Value \"{0}\" not match filter \"{1}\"", value.ItemSpec, Filter);
            }

            ResultStrings = result.OfType<TaskItem>().ToArray();
            return true;
        }
    }
}
