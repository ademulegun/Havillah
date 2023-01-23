using Havillah.Shared;

namespace Havillah.ApplicationServices.Interfaces;

public interface IUploadImageToStorage
{
    Task<Result<string>> Upload(string image);
}