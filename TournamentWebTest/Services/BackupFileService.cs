using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System.Text;
using System.Text.Json;

namespace TournamentWebTest.Services
{
    public interface IBackupFileService
    {
        Task SaveAsync<T>(T data, string fileName, string contentType = "application/json", JsonSerializerOptions? options = null);
        Task<T?> LoadAsync<T>(IBrowserFile file, long maxAllowedBytes = 5 * 1024 * 1024, JsonSerializerOptions? options = null);
    }

    public class BackupFileService : IBackupFileService
    {
        private readonly IJSRuntime _js;

        public BackupFileService(IJSRuntime js)
        {
            _js = js;
        }

        public async Task SaveAsync<T>(T data, string suggestedFileName, string contentType = "application/json", JsonSerializerOptions? options = null)
        {
            //options ??= new JsonSerializerOptions(JsonSerializerDefaults.Web) { WriteIndented = true };
            //var json = JsonSerializer.Serialize(data, options);
            var json = TournamentLibrary.Utilities.JsonConverter.Serialize(data);

            // Versuch: echter "Speichern unter..." Dialog
            var result = await _js.InvokeAsync<SavePickerResult>("saveWithPicker", suggestedFileName, contentType, json);

            if (result?.ok == true)
                return;

            // Fallback: normaler Download
            var bytes = Encoding.UTF8.GetBytes(json);
            var base64 = Convert.ToBase64String(bytes);
            await _js.InvokeVoidAsync("downloadFile", suggestedFileName, contentType, base64);
        }

        private sealed class SavePickerResult
        {
            public bool ok { get; set; }
            public string? reason { get; set; }
        }

        public async Task<T?> LoadAsync<T>(IBrowserFile file, long maxAllowedBytes = 5 * 1024 * 1024, JsonSerializerOptions? options = null)
        {
            options ??= new JsonSerializerOptions(JsonSerializerDefaults.Web);

            await using var stream = file.OpenReadStream(maxAllowedBytes);
            using var reader = new StreamReader(stream, Encoding.UTF8);
            var json = await reader.ReadToEndAsync();

            //return JsonSerializer.Deserialize<T>(json, options);
            return TournamentLibrary.Utilities.JsonConverter.Deserialize<T>(json);
        }
    }
}
