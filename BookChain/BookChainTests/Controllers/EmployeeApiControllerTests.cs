using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookChain.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookChain.Controllers.Tests
{
    [TestClass()]
    public class EmployeeApiControllerTests
    {
        [TestMethod()]
        public void  GetTest()
        {
            var controller = new EmployeeApiController();
            var result =  controller.Get();

            Assert.IsNotNull(result, "result should not be null");
        }

      
        }
    }
