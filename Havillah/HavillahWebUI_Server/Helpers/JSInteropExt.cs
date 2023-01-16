using Microsoft.JSInterop;

namespace HavillahWebUI_Server.Helpers;

public static class JSInteropExt
{
    public static ValueTask<object> SaveAs(this IJSRuntime js, string filename, byte[] data)
    {
        var repsonse = js.InvokeAsync<object>("saveAsFile", filename, Convert.ToBase64String(data));
        return repsonse;
    }

    public static ValueTask<object> SaveAs(this IJSRuntime js, string filename, byte[] data, string type = "application/octect-stream")
    {
        try
        {
            var repsonse = js.InvokeAsync<object>("saveAsFile", filename, type, Convert.ToBase64String(data));
            return repsonse;
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    
    public static async ValueTask ToastrSuccess(this IJSRuntime js, string message)
    {
        await js.InvokeVoidAsync("ShowToastr", "success", message);
    }
    
    public static async ValueTask ToastrFailed(this IJSRuntime js, string message)
    {
        await js.InvokeVoidAsync("ShowToastr", "failed", message);
    }
    
    public static async ValueTask ToastrNotFound(this IJSRuntime js, string message)
    {
        await js.InvokeVoidAsync("ShowToastr", "notfound", message);
    }
    
    public static async ValueTask ToastrWarning(this IJSRuntime js, string message)
    {
        await js.InvokeVoidAsync("ShowToastr", "warning", message);
    }
}