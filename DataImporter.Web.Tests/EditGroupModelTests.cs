using Autofac.Extras.Moq;
using AutoMapper;
using DataImporter.Common.Utilities;
using DataImporter.Transfer.BusinessObjects;
using DataImporter.Transfer.Services;
using DataImporter.Web.Models.Groups;
using Moq;
using NUnit.Framework;
using System;
using System.Diagnostics.CodeAnalysis;

namespace DataImporter.Web.Tests
{
    [ExcludeFromCodeCoverage]
    public class EditGroupModelTests
    {
        private AutoMock _mock;
        private Mock<IMapper> _mapperMock;
        private Mock<IGroupService> _groupServiceMock;
        private EditGroupModel _model;
        private DateTimeUtility  _dateTimeUtility;

        

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _mock = AutoMock.GetLoose();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _mock?.Dispose();
        }

        [SetUp]
        public void TestSetup()
        {
            _mapperMock = _mock.Mock<IMapper>();
            _model = _mock.Create<EditGroupModel>();
            _groupServiceMock = _mock.Mock<IGroupService>();
            _dateTimeUtility = _mock.Create<DateTimeUtility>();

        }

        [TearDown]
        public void TestCleanUp()
        {
            _mapperMock.Reset();
            _groupServiceMock.Reset();
        }

        [Test]
        public void EditGroup_GroupExits_LoadPropety()
        {
            //Arrange
            const int id = 2;

            var group = new Group
            {
                Id = 2,
                GroupName = "Test",
                CreateDate = _dateTimeUtility.Now,
                UserId = Guid.Parse("0cb2f149-b11f-4faf-9f20-08d9859583c4")

            };


            _groupServiceMock.Setup(m => m.GetGroup(id)).Returns(group).Verifiable();

            _mapperMock.Setup(m => m.Map(
                group, It.IsAny<EditGroupModel>()
                )).Verifiable();


            //Art
            _model.EditGroup(id);

            //Assert
            _groupServiceMock.VerifyAll();
            _mapperMock.VerifyAll();
        }
    }
}