using System;
using System.IO;
using System.Web.Hosting;

namespace CouponPaymentSystem.Common;

public static class PathHelper
{
    public static string RootDir => HostingEnvironment.MapPath("~/") ?? AppContext.BaseDirectory;

    public static string AppDataDir => Path.Combine(RootDir, "App_Data");

    public static string BlobDir => Path.Combine(AppDataDir, "Blobs");
}
