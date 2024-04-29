using NUnit.Framework;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using thesis_lawyer.Models;

namespace thesis_lawyer.Tests.Models
{
    [TestFixture]
    public class HistoryChatTests
    {
        [Test]
        public void Id_Property_Should_Have_KeyAttribute()
        {
            // Arrange
            var idProperty = typeof(HistoryChat).GetProperty("Id");

            // Act
            var keyAttribute = idProperty.GetCustomAttributes(typeof(KeyAttribute), true).FirstOrDefault() as KeyAttribute;

            // Assert
            Assert.NotNull(keyAttribute);
        }
        [Test]
        public void UserForeignKey_Property_Should_Not_Have_NotNullAttribute()
        {
            // Arrange
            var userForeignKeyProperty = typeof(HistoryChat).GetProperty("UserForeignKey");

            // Act
            var requiredAttribute = userForeignKeyProperty.GetCustomAttributes(typeof(RequiredAttribute), true).FirstOrDefault() as RequiredAttribute;

            // Assert
            Assert.Null(requiredAttribute);
        }



        [Test]
        public void UserForeignKey_Property_Should_Not_Have_KeyAttribute()
        {
            // Arrange
            var userForeignKeyProperty = typeof(HistoryChat).GetProperty("UserForeignKey");

            // Act
            var keyAttribute = userForeignKeyProperty.GetCustomAttributes(typeof(KeyAttribute), true).FirstOrDefault() as KeyAttribute;

            // Assert
            Assert.Null(keyAttribute);
        }

        [Test]
        public void Chat_Property_Should_Not_Have_NotMappedAttribute()
        {
            // Arrange
            var chatProperty = typeof(HistoryChat).GetProperty("Chat");

            // Act
            var notMappedAttribute = chatProperty.GetCustomAttributes(typeof(NotMappedAttribute), true).FirstOrDefault() as NotMappedAttribute;

            // Assert
            Assert.Null(notMappedAttribute);
        }



     

    }
}
