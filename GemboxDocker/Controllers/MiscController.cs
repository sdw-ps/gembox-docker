using GemBox.Document;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace GemboxDocker.Controllers;

[ApiController]
[Route("[controller]")]
public class MiscController : ControllerBase
{
    

    private readonly ILogger<MiscController> _logger;

    public MiscController(ILogger<MiscController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Get()
    {
        ComponentInfo.SetLicense("FREE-LIMITED-KEY");

        // Create new document.
        var document = new DocumentModel();
        FontSettings.FontsBaseDirectory = AppContext.BaseDirectory + "/Fonts";
        document.DefaultCharacterFormat.FontName = "Roboto";

        // Add sample text.
        document.Content.End
            .LoadText("Lorem Ipsum\n", new CharacterFormat() { FontColor = GemBox.Document.Color.Red, Bold = true })
            .LoadText("Lorem Ipsum\n", new CharacterFormat() { FontColor = GemBox.Document.Color.Green, Italic = true })
            .LoadText("Lorem Ipsum\n", new CharacterFormat() { FontColor = GemBox.Document.Color.Blue, UnderlineStyle = UnderlineType.Single });

        // does not work with a remote image using SkiaSharp.NativeAssets.Linux
        // requires SkiaSharp.NativeAssets.Linux.NoDependencies
        /*
        System.InvalidOperationException: 'Bitmap decoding failed. 'WPF' (https://docs.microsoft.com/en-us/dotnet/desktop/wpf/) was not found on the system. If the platform on which the application is running supports 'WPF', make sure that your project uses it. 'System.Drawing' (https://docs.microsoft.com/en-us/dotnet/api/system.drawing) was not found on the system. If the platform on which the application is running supports 'System.Drawing', make sure that your project uses it. 'SkiaSharp' threw an exception of type 'System.TypeInitializationException' and with message 'The type initializer for 'SkiaSharp.SKData' threw an exception.'. For more details about the exception, see the inner exception. 'ImageSharp' (https://github.com/SixLabors/ImageSharp/) was not found on the system. If the platform on which the application is running supports 'ImageSharp', make sure that your project uses it. 'GemBox' threw an exception of type 'System.NotSupportedException' and with message 'Specified method is not supported.'.'

        Inner Exceptions:
        DllNotFoundException: Unable to load shared library 'libSkiaSharp' or one of its dependencies. In order to help diagnose loading problems, consider using a tool like strace. If you're using glibc, consider setting the LD_DEBUG environment variable: 
        libfontconfig.so.1: cannot open shared object file: No such file or directory
        /usr/share/dotnet/shared/Microsoft.NETCore.App/7.0.10/libSkiaSharp.so: cannot open shared object file: No such file or directory
        /app/bin/Debug/net7.0/libSkiaSharp.so: cannot open shared object file: No such file or directory
        /app/bin/Debug/net7.0/runtimes/linux-x64/native/liblibSkiaSharp.so: cannot open shared object file: No such file or directory
        /usr/share/dotnet/shared/Microsoft.NETCore.App/7.0.10/liblibSkiaSharp.so: cannot open shared object file: No such file or directory
        /app/bin/Debug/net7.0/liblibSkiaSharp.so: cannot open shared object file: No such file or directory
        /app/bin/Debug/net7.0/runtimes/linux-x64/native/libSkiaSharp: cannot open shared object file: No such file or directory
        /usr/share/dotnet/shared/Microsoft.NETCore.App/7.0.10/libSkiaSharp: cannot open shared object file: No such file or directory
        /app/bin/Debug/net7.0/libSkiaSharp: cannot open shared object file: No such file or directory
        /app/bin/Debug/net7.0/runtimes/linux-x64/native/liblibSkiaSharp: cannot open shared object file: No such file or directory
        /usr/share/dotnet/shared/Microsoft.NETCore.App/7.0.10/liblibSkiaSharp: cannot open shared object file: No such file or directory
        /app/bin/Debug/net7.0/liblibSkiaSharp: cannot open shared object file: No such file or directory

        */
        document.Content.End.InsertRange(new Picture(document, "https://picsum.photos/200/300", isLink: true).Content);

        // works just fine with a local image
        // document.Content.End.InsertRange(new Picture(document, "dices.png").Content);

        var pathToFile = "output.pdf";

        // Save document in DOCX and PDF format.
        // document.Save("output.docx");
        document.Save(pathToFile);

        var stream = new FileStream(pathToFile, FileMode.Open, FileAccess.Read, FileShare.None, bufferSize: 4096, FileOptions.DeleteOnClose);

        return File(stream, "application/pdf", Path.GetFileName(pathToFile));
    }
}