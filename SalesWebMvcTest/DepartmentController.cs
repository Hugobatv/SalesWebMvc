using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace SalesWebMvcTest
{
    internal class DepartmentController
    {
        private DbContext @object;

        public DepartmentController(DbContext @object)
        {
            this.@object = @object;
        }

        internal Task Details(int? departmentId)
        {
            throw new NotImplementedException();
        }
    }
}