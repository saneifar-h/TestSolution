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
                    "To Show All Tasks That I'v Done I Create it"),
                new TaskItem(4, "Start Changing Structure To MVVM", DateTime.Parse("2020-07-16"),
                    "At First Separating View from Viewmodel So We Have Cleaner Code and more Readability"),
                new TaskItem(5, "Joining First Solution To The Project", DateTime.Parse("2020-07-16"),
                    "All First Chat Application Feature Added To this Project"),
                new TaskItem(6, "Start Joining Second One", DateTime.Parse("2020-07-16"),
                    "After First joining Try I Get Error And I have to go Somewhere So Still Needs To complete "),
                new TaskItem(7, "Merging New Chat Solution Complete", DateTime.Parse("2020-07-17"),
                    "Now you can send Emoji and files using previous sample"),

            };
        }
    }
}

