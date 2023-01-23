using Havillah.ApplicationServices.Interfaces;
using Havillah.Shared;

namespace Havillah.Infrasructure.Storage;

public class UploadImage : IUploadImageToStorage
{
    public async Task<Result<string>> Upload(string image)
    {
        throw new NotImplementedException();
        await Task.Delay(1000);
        return Result.Ok<string>("");
    }
}