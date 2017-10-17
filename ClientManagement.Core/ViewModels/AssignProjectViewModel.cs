using System;
using System.Collections.Generic;

namespace ClientManagement.Core.ViewModels
{
    public class AssignProjectViewModel
    {
        public string Employee { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid ProjectId { get; set; }

        public IEnumerable<KeyValuePair<bool, Guid>> ProjectIds { get; set; }
    }
}
