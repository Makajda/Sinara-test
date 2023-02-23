using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SinaraUnitTest {
    [TestClass]
    public class DeleteTest {

        //проверка, что после удаления запись осталась в таблице

        [TestMethod]
        public async Task TestId() {
            // Arrange
            var db = new HrDbContext();
            var customer = new Customer { LastName = "Тестовый" };
            db.Customers.Add(customer);
            await db.SaveChangesAsync();
            var id = customer.Id;

            var moqDirectoryEntryService = new Mock<IDirectoryEntryService>();
            var moqLogger = new Mock<ILogger<CustomerController>>();

            var controller = new CustomerController(db, moqDirectoryEntryService.Object, moqLogger.Object);

            // Act
            var result = await controller.Delete(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            var has = await db.Customers.AnyAsync(n => n.Id == id);
            Assert.IsTrue(has);
        }
    }
}