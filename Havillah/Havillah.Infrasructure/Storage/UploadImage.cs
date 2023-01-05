<<<<<<< HEAD
<<<<<<< HEAD
using Havillah.ApplicationServices.Interfaces;
using Havillah.Shared;

namespace Havillah.Infrasructure.Storage;

public class UploadImage: IUploadImageToStorage
{
    public async Task<Result<string>> Upload(string image)
    {
        throw new NotImplementedException();
        await Task.Delay(1000);
        return Result.Ok<string>("");
    }
=======
=======
using Havillah.ApplicationServices.Interfaces;
using Havillah.Shared;

>>>>>>> e34493e (modified espense with constructor)
namespace Havillah.Infrasructure.Storage;

public class UploadImage: IUploadImageToStorage
{
<<<<<<< HEAD
    
>>>>>>> 56eb5a1 (trying)
=======
    public async Task<Result<string>> Upload(string image)
    {
        throw new NotImplementedException();
        await Task.Delay(1000);
        return Result.Ok<string>("");
    }
>>>>>>> e34493e (modified espense with constructor)
}