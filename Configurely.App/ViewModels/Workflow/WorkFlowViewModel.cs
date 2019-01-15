using Configurely.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Configurely.App.ViewModels.Workflow
{
    public class WorkFlowViewModel
    {
        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public List<Field> CurrentFields { get; set; }

        public List<Field> AvailableFields { get; set; }
    }
}