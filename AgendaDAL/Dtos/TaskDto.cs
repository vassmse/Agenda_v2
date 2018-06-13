﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaDAL.Dtos
{
    public class TaskDto
    {
        public int TaskId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int State { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime DeadlineDate { get; set; }

        public DateTime ScheduledDate { get; set; }

        public List<Task> SubTasks { get; set; }

        public int ParentTaskId { get; set; }

        public int ParentCategoryId { get; set; }
    }
}
