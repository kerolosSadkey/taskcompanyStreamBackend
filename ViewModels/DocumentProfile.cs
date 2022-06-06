using AutoMapper;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    internal class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            ConfigureMappings();


        }

        public override string ProfileName
        {
            get
            {
                return "DomainToViewModelMappings";
            }
        }

       


        /// <summary>
        /// Creates a mapping between source (Domain) and destination (ViewModel)
        /// </summary>
        private void ConfigureMappings()
        {
            CreateMap<document, DocumentViewModel>().ReverseMap();
            CreateMap<document_files, document_fileVM>().ReverseMap();
        }
    }
}
