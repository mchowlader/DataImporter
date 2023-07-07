using Autofac.Extras.Moq;
using AutoMapper;
using DataImporter.Common.Utilities;
using DataImporter.Transfer.BusinessObjects;
using DataImporter.Transfer.Repositories;
using DataImporter.Transfer.Services;
using DataImporter.Transfer.UnitOfWorks;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using EO = DataImporter.Transfer.Entities;

namespace DataImporter.Transfer.Tests
{
    public class GroupServiceTests
    {

        private AutoMock _mock;
        private Mock<ITransferUnitOfWork> _transferUnitOfWork;
        private Mock<IGroupRepository> _groupRepository;
        private Mock<IMapper> _mapperMock;
        private IGroupService _groupService;
        private DateTimeUtility _dateTimeUtility;


        [OneTimeSetUp]
        public void ClassSetup()
        {
            _mock = AutoMock.GetLoose();
        }

        [OneTimeTearDown]
        public void ClassCleanUp()
        {
            _mock?.Dispose();
        }
        [SetUp]
        public void Setup()
        {
            _mapperMock = _mock.Mock<IMapper>();
            _transferUnitOfWork = _mock.Mock<ITransferUnitOfWork>();
            _groupRepository = _mock.Mock<IGroupRepository>();
            _groupService = _mock.Create<GroupService>();
            _dateTimeUtility = _mock.Create<DateTimeUtility>();

        }
        [TearDown]
        public void TearDown()
        {
            _mapperMock?.Reset();
            _transferUnitOfWork?.Reset();
            _groupRepository?.Reset();
        }

        [Test]
        public void GroupCreate_GroupNameDoseNotExit_ThrowsException()
        {
            //Arrange
            Group group = null;

            //Act & Assert
            Should.Throw<InvalidOperationException>(
                () => _groupService.CreateGroup(group)
            );
        }

        [Test]
        public void GroupCreate_GroupDoseNotExit_ThrowsException()
        {
            //Arrange

            Group group = new Group()
            { 
                GroupName = null
            
            };

            //Act & Assert
            Should.Throw<InvalidOperationException>(
                () => _groupService.CreateGroup(group)
            );
        }

        [Test]
        public void UpdateGroup_GroupExit_UpdateGroup()
        {
            //Arrange
            var group = new Group
            {
                Id = 4,
                GroupName = "Asp",
                CreateDate = _dateTimeUtility.Now,
                UserId = Guid.Parse("0cb2f149-b11f-4faf-9f20-08d9859583c4")
            };

            var groupEntity = new EO.Group
            {
               Id = 4,
               GroupName = "Asp.Net",
               CreateDate = _dateTimeUtility.Now,
               UserId = Guid.Parse("0cb2f149-b11f-4faf-9f20-08d9859583c4")
            };

            _transferUnitOfWork.Setup(x => x.Groups).Returns(_groupRepository.Object);

            _groupRepository.Setup(x => x.GetById(group.Id)).Returns(groupEntity);
            _transferUnitOfWork.Setup(x => x.Save()).Verifiable();


            //Act 
            _groupService.UpdateGroup(group);

            //Assert
            this.ShouldSatisfyAllConditions(
                () => _transferUnitOfWork.VerifyAll(),
                () => _groupRepository.VerifyAll()
            );
        }

    }
}