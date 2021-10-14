using NUnit.Framework;
using System.Linq;

namespace Tests
{
    [TestFixture]
    public class DatabaseTests
    {
        private const int databaseLimit = 16;
        const int numToAdd = 777;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CheckDatabaseCountWithRandomSet()
        {
            // Arrange
            int[] nums = Enumerable.Range(1, databaseLimit - 10).ToArray();
            var db = new Database.Database(nums);

            int expectedCount = nums.Length;

            // Assert
            Assert.That(db.Count, Is.EqualTo(expectedCount));
        }

        [Test]
        // Storing array's capacity must be exactly 16 integers
        public void CheckDatabaseCountWithFull16IntegersSet()
        {
            // Arrange
            var dbWith16Nums = new Database.Database(Enumerable.Range(1, databaseLimit).ToArray());

            int expectedCount = databaseLimit;

            // Act

            // Assert
            Assert.That(dbWith16Nums.Count, Is.EqualTo(expectedCount));
        }

        [Test]
        public void DatabaseShouldThrowExceptionIfMoreThan16Integers()
        {
            // Assert
            Assert
                .That(() => new Database.Database(Enumerable.Range(1, databaseLimit + 10).ToArray()), // Act
                Throws.InvalidOperationException.With.Message.EqualTo("Database capacity is no bigger than 16 integers!"));
        }


        [Test]
        public void AddOperationShouldAddElementAtNextFreeCell()
        {
            // Arrange
            int[] nums = Enumerable.Range(1, databaseLimit - 10).ToArray();
            var db = new Database.Database(nums);

            var count = db.Count;

            // Act
            db.Add(numToAdd);

            // Assert
            Assert.That(db.Fetch()[count], Is.EqualTo(numToAdd));
        }

        [Test]
        public void AddOperationShouldThrowExceptionIf17thIntegerIsAdded()
        {
            // Arrange
            var db = new Database.Database(Enumerable.Range(1, databaseLimit).ToArray());

            // Assert
            Assert
                .That(() => db.Add(numToAdd), // Act
                Throws.InvalidOperationException.With.Message.EqualTo("Database capacity is exactly 16 integers!"));
        }


        [Test]
        public void RemoveOperationShouldSupportOnlyRemovingElementAtLastIndex()
        {
            // Arrange
            var db = new Database.Database(Enumerable.Range(1, databaseLimit).ToArray());

            // Act
            db.Remove();

            // Assert
            Assert.That(db.Count, Is.EqualTo(databaseLimit - 1));
            Assert.That(db.Fetch()[db.Count - 1], Is.EqualTo(15));
        }

        [Test]
        public void RemoveOperationShouldThrowExceptionIfDatabaseIsEmpty()
        {
            // Arrange
            var db = new Database.Database();

            // Assert
            Assert.That(() => db.Remove(),
                Throws.InvalidOperationException.With.Message.EqualTo("The collection is empty!"));
        }


        [Test]
        public void FetchMethodShouldReturnElementsAsArray()
        {
            // Arrange
            var db = new Database.Database(Enumerable.Range(1, databaseLimit).ToArray());

            // Act
            int[] expectedArray = Enumerable.Range(1, databaseLimit).ToArray();

            // Assert
            CollectionAssert.AreEqual(db.Fetch(), expectedArray);
        }
    }
}