﻿namespace AdminDashboard.DocumentService
{
    public interface IDocummentService
    {
        Task<string> uploadFile(IFormFile file, string foldername);

        Task<bool> DeleteFile(string pictureUrl, string foldername);

    }
}
