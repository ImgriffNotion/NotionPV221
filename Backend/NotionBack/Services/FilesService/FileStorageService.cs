using NotionBack.Models;
using NotionBack.Models.FormBody;
using NotionBack.Models.ModelsDTO;
using System.Text.Json;

namespace NotionBack.Services.FilesService
{
    public class FileStorageService(IHttpClientFactory httpClientFactory,
        AppUserContext userContext) : IFileStorageService
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient();
        private readonly AppUserContext _userContext = userContext;

        public Task DeleteFile(string path)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetFileUrl(FileDTO file)
        {
            using var content = new MultipartFormDataContent();
            content.Add(new StringContent(_userContext.userId), "userId");
            content.Add(new StringContent(_userContext.userEmail), "userEmail");

            var response = await _httpClient.GetAsync($"{RedirectionURLs._fileLocalUrl}?userId={_userContext.userId}&userEmail={_userContext.userEmail}&fileName={file.Name}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<Dictionary<string, string>>(json);

            return data?["url"];
        }



        public async Task<string> UploadFile(FileFormBody file)
        {
            if(file == null || file.uploadedFile == null)
            {
                return null;
            }

            using var content = new MultipartFormDataContent();

            using var stream = file.uploadedFile.OpenReadStream();
            var streamContent = new StreamContent(stream);
            streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.uploadedFile.ContentType);

            content.Add(streamContent, "formFile", file.uploadedFile.FileName);
            content.Add(new StringContent(_userContext.userId), "userId");
            content.Add(new StringContent(_userContext.userEmail), "userEmail");

            var response = await _httpClient.PostAsync($"{RedirectionURLs._fileLocalUrl}/upload", content);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<Dictionary<string, string>>(json);

            return data?["url"];
        }
    }
}
