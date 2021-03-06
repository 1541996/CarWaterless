﻿using Infra.Helper;
using Infra.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.helper
{
    public class FileUploadApiRequestHelper
    {
        public static async Task<string> upload(FileUploadViewModel fvm)
        {
            string url = string.Format("api/file/upload");
            return await ApiRequest<FileUploadViewModel, string>.PostDiffRequest(url, fvm);
        }

        public static async Task<string> uploadwithoutbase64(FileUploadViewModel fvm)
        {
            string url = string.Format("api/file/uploadwithoutbase64");
            return await ApiRequest<FileUploadViewModel, string>.PostDiffRequest(url, fvm);
        }

        

        public static async Task<string> uploadgif(FileUploadViewModel fvm)
        {
            string url = string.Format("api/file/uploadgif");
            return await ApiRequest<FileUploadViewModel, string>.PostDiffRequest(url, fvm);
        }

        public static async Task<List<string>> uploadlist(List<FileUploadViewModel> fvm)
        {
            string url = string.Format("api/file/uploadmultiple");
            return await ApiRequest<List<FileUploadViewModel>,List<string>>.PostDiffRequest(url, fvm);
        }
        public static async Task<string> uploadsrt(FileUploadViewModel fvm)
        {
            string url = string.Format("api/file/uploadsrt");
            return await ApiRequest<FileUploadViewModel, string>.PostDiffRequest(url, fvm);
        }


        public static async Task<string> deletefile(FileUploadViewModel fvm)
        {
            string url = string.Format("api/file/deletefile");
            return await ApiRequest<FileUploadViewModel, string>.PostDiffRequest(url, fvm);
        }



    }
}
