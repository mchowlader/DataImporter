using Autofac.Extras.Moq;
using AutoMapper;
using DataImporter.Common.Utilities;
using DataImporter.Transfer.Entities;
using DataImporter.Transfer.Repositories;
using DataImporter.Transfer.Services;
using DataImporter.Transfer.UnitOfWorks;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO = DataImporter.Transfer.BusinessObjects;

namespace DataImporter.Transfer.Tests
{
    public class ImportServiceTest
    {
        private AutoMock _mock;
        private Mock<ITransferUnitOfWork> _transferUnitOfWork;
        private Mock<IImportRepository> _importRepository;
        private Mock<IMapper> _mapperMock;
        private ImportService _importService;
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
            _importRepository = _mock.Mock<IImportRepository>();
            _importService = _mock.Create<ImportService>();
            _dateTimeUtility = _mock.Create<DateTimeUtility>();

        }
        [TearDown]
        public void TearDown()
        {
            _mapperMock?.Reset();
            _transferUnitOfWork?.Reset();
            _importRepository?.Reset();
        }

        [Test]
        public void ImportService_StatusUpdate_UpdateStatus()
        {
            //Arrange
            var  id = 4;

            var importEntity = new Import()
            {
                Status = "Pending"
            };


            var import = new BO.Import()
            {
                Status = "Processing"
            };

            _transferUnitOfWork.Setup(x => x.Imports).Returns(_importRepository.Object);

            _importRepository.Setup(x => x.GetById(id)).Returns(importEntity);
            _transferUnitOfWork.Setup(x => x.Save()).Verifiable();
            //Act 
            _importService.StatusUpdate();

            //Assert
            this.ShouldSatisfyAllConditions(
                () => _transferUnitOfWork.VerifyAll(),
                () => _importRepository.VerifyAll()
            );

        }
    }
}
