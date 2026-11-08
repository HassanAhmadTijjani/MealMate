using MealMate.Application.Common.Interfaces;

namespace MealMate.Infrastructure.Storage;

public sealed class LocalReceiptStorageService : IReceiptStorageService
{
    private readonly string _storageDirectory;

    public LocalReceiptStorageService()
    {
        _storageDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "MealMate", "PrivateReceipts");
        Directory.CreateDirectory(_storageDirectory);
    }

    public Task DeleteAsync(string storageKey, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(storageKey) ||
            storageKey != Path.GetFileName(storageKey))
        {
            throw new ArgumentException(
                "Invalid receipt storage key.",
                nameof(storageKey));
        }

        cancellationToken.ThrowIfCancellationRequested();

        var filePath = Path.Combine(_storageDirectory, storageKey);

        if (File.Exists(filePath))
            File.Delete(filePath);

        return Task.CompletedTask;
    }

    public async Task<string> UploadAsync(Stream fileStream, string fileName, string contentType, CancellationToken cancellationToken = default)
    {
        var storageKey = $"{Guid.NewGuid():N}.receipt";
        var filePath = Path.Combine(_storageDirectory, storageKey);
        await using var destination = new FileStream(
            filePath,
            FileMode.CreateNew,
            FileAccess.Write,
            FileShare.None,
            bufferSize: 81920,
            useAsync: true
        );
        await fileStream.CopyToAsync(destination, cancellationToken);
        return storageKey;
    }
}