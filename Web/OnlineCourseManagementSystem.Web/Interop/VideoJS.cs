namespace OnlineCourseManagementSystem.Web.Interop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.JSInterop;
    using OnlineCourseManagementSystem.Web.ViewModels.VideoConferences;

    public static class VideoJS
    {
        public static ValueTask<Device[]> GetVideoDevicesAsync(
              this IJSRuntime jsRuntime) =>
              jsRuntime?.InvokeAsync<Device[]>(
                  "getVideoDevices") ?? default(ValueTask<Device[]>);

        public static ValueTask StartVideoAsync(
            this IJSRuntime jSRuntime,
            string deviceId,
            string selector) =>
            jSRuntime?.InvokeVoidAsync(
                "startVideo",
                deviceId,
                selector) ?? default(ValueTask);

        public static ValueTask<bool> CreateOrJoinRoomAsync(
            this IJSRuntime jsRuntime,
            string roomName,
            string token) =>
            jsRuntime?.InvokeAsync<bool>(
                "createOrJoinRoom",
                roomName,
                token) ?? new ValueTask<bool>(false);

        public static ValueTask LeaveRoomAsync(
            this IJSRuntime jsRuntime) =>
            jsRuntime?.InvokeVoidAsync(
                "leaveRoom") ?? default(ValueTask);
    }
}
