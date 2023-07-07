using Autofac;
using AutoMapper;
using DataImporter.Common.Utilities;
using DataImporter.ExcelFileReader;
using DataImporter.Transfer.BusinessObjects;
using DataImporter.Transfer.Services;
using ExcelDataReader;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web.Models.Imports
{
    public class UploadModel
    {
        public int Id { get; set; }
        //public int GroupId { get; set; }
        public string DateTo { get; set; }
        public string DateFrom { get; set; }
        public IFormFile XlsFile { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        //public string GroupName { get; set; }
        public string ExcelFileName { get; set; }
        public string FileExtension { get; set; }
        public DateTime ImportDate { get; set; }
        public IList<Group> groupsList { get; set; }
        public List<DataStore> ColumnDatas { get; set; }


        private IMapper _mapper;
        private ILifetimeScope _scope;
        private IDateTimeUtility _dateTimeUtility;
        private IImportService _importService;
        private IGroupService _groupService;
        private IWebHostEnvironment _webHostEnvironment;



        public UploadModel()
        {

        }
        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _groupService = _scope.Resolve<IGroupService>();
            _mapper = _scope.Resolve<IMapper>();
            _importService = _scope.Resolve<IImportService>();
            _dateTimeUtility = _scope.Resolve<IDateTimeUtility>();
            _webHostEnvironment = _scope.Resolve<IWebHostEnvironment>();
        }
        public UploadModel(IMapper mapper, IDateTimeUtility dateTimeUtility,
            IImportService importService, IGroupService groupService,
            IWebHostEnvironment webHostEnvironment)
        {
            _mapper = mapper;
            _groupService = groupService;
            _dateTimeUtility = dateTimeUtility;
            _importService = importService;
            _webHostEnvironment = webHostEnvironment;
        }

        HelperExcelDataRead helperExcelDataRead = new HelperExcelDataRead();


        public void LoadGroupProperty(Guid id)
        {
            groupsList = _groupService.LoadGroupProperty(id);
        }

        public void PreviewExcelData(string filePath)
        {
           ColumnDatas = helperExcelDataRead.ReadExcelData(5, filePath);
        }



        public void UploadExcelFile()
        {
            if (XlsFile != null)
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                string path = Path.Combine(this._webHostEnvironment.WebRootPath, "Uploads");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                FileName = Path.GetFileName(XlsFile.FileName);
                FilePath = Path.Combine(path, FileName);
                ExcelFileName = Path.GetFileNameWithoutExtension(FileName);
                FileExtension = Path.GetExtension(XlsFile.FileName);

                if (DuplicateFileExit(FilePath)) 
                    throw new InvalidOperationException("File Already Exit");

                if (!InvalidFileUoload(FileName)) 
                    throw new InvalidOperationException("Select Excel File");


                FileInfo file = new FileInfo(Path.Combine(path, FileName));
                using (var stream = new MemoryStream())
                {
                    XlsFile.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        package.SaveAs(file);
                    }
                }

            }
            else
                throw new InvalidOperationException("Select Excel File");


        }


        public void CreateImportHistory(int groupId)
        {
            var importsData = new Import()
            {
                GroupId = groupId,
                ImportDate = _dateTimeUtility.Now,
                ExcelFileName = ExcelFileName,
                FilePath = FilePath,
            };
            _importService.UploadExcelFile(importsData);
        }


        private bool DuplicateFileExit(string filePath) 
            => File.Exists(filePath) ? true : false;
        private bool InvalidFileUoload(string fileName) 
            => Path.GetExtension(XlsFile.FileName) == ".xlsx" ? true : false;
        
    }
}