using System;

namespace TestSolution.Client.Pages.Dto
{
    public class TaskItem
    {
        public TaskItem(int order, string title, DateTime startDate, string description)
        {
            Title = title;
            StartDate = startDate.ToShortDateString();
            Description = description;
            Order = order;
        }
        public string Title { get;  }
        public string StartDate { get;  }
        public string Description { get;  }
        public int Order { get; }
    }
}