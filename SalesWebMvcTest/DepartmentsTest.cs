using SalesWebMvc.Controllers;
using SalesWebMvc.Models;
using System;
using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SalesWebMvcTest
{
    public class DepartmentsTest
    {
        [Fact]
        public async Task Details_ValidId_ReturnsViewResult()
        {
            //Arrange 

            int departmentId = 1;
            var department = new Department { Id = departmentId, Name = "Department test" };

            var dbContextMock = new Mock<DbContext>();
            var dbSetMock = new Mock<DbSet<Department>>();

            dbSetMock.Setup(m => m.FindAsync(departmentId)).ReturnsAsync(department);

            dbContextMock.Setup(c => c.Set<Department>()).Returns(dbSetMock.Object);

            var controller = new DepartmentsController(dbContextMock.Object);

            //Act 

            var result = await controller.Details(departmentId);

            //Assert

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Department>(viewResult.ViewData.Model);
            Assert.Equal(department, model);

        }

        [Fact]

        public async Task Details_NullId_ReturnsNotFoundResult()
        {
            int? departmentId = null;
            var department = new Department { Id = (int)departmentId, Name = "Department test" };

            var dbContextMock = new Mock<DbContext>();
            var dbSetMock = new Mock<DbSet<Department>>();

            dbSetMock.Setup(m => m.FindAsync(departmentId)).ReturnsAsync(department);

            dbContextMock.Setup(c => c.Set<Department>()).Returns(dbSetMock.Object);

            var controller = new DepartmentsController(dbContextMock.Object);

            // Act
            var result = await controller.Details(departmentId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

      /*  [Fact]
        public async Task Details_NonExistentId_ReturnsNotFoundResult()
        {
            // Arrange
            int departmentId = 1;

            var dbContextMock = new Mock<DbContext>();
            var dbSetMock = new Mock<DbSet<Department>>();

            dbSetMock.Setup(m => m.FindAsync(departmentId)).ReturnsAsync((Department)null);

            dbContextMock.Setup(c => c.Set<Department>()).Returns(dbSetMock.Object);

            var controller = new DepartmentController(dbContextMock.Object);

            // Act
             var result = await controller.Details(departmentId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }*/

    }
}


