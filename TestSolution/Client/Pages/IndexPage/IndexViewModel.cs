using System;
using System.Collections.Generic;
using TestSolution.Client.Pages.Dto;

namespace TestSolution.Client.Pages.IndexPage
{
    public class IndexViewModel
    {
        public List<TaskItem> GetTasks()
        {
            return new List<TaskItem>
            {
                new TaskItem(1, "Enable Auth", DateTime.Parse("2020-07-16"),
                    "Change Auth Db Connection And Test It Via Register and Login Again"),
                new TaskItem(2, "Add My Name To Page", DateTime.Parse("2020-07-16"),
                    "Change Hello World to Have My Introduction "),
                new TaskItem(3, "Create TaskList & TaskItem Component", DateTime.Parse("2020-07-16"),
                    "To Show All Tasks That I'v Done I Create it")
            };
        }
    }
}

